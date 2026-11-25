using System.Globalization;
using DBDemoWinFormApp.Data;
using DBDemoWinFormApp.Helpers;
using DBDemoWinFormApp.Models;
using Microsoft.EntityFrameworkCore;

namespace DBDemoWinFormApp
{
    public partial class Form3 : Form
    {
        private static readonly string[] CotSapXep = ["Hoten", "Toan", "Van", "DiemTB"];
        private bool _isInitializing = true;
        private bool _sapXepTangDan = true;
        private int _trangHienTai = 1;
        private int _tongTrang = 1;

        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object? sender, EventArgs e)
        {
            _isInitializing = true;

            LogoHelper.KhoiTaoLogo(picLogo, chkHienLogo);
            GridHelper.GanCheckBoxAnHienCot(
                dgvKetQua,
                (chkCotHoTen, "Hoten"),
                (chkCotToan, "Toan"),
                (chkCotVan, "Van"),
                (chkCotDiemTB, "DiemTB"),
                (chkCotXepLoai, "XepLoai"));

            cboXepLoai.Items.AddRange(["-- Tất cả --", "Giỏi", "Khá", "Trung bình", "Yếu"]);
            cboXepLoai.SelectedIndex = 0;

            cboSapXep.Items.AddRange(["Họ tên", "Điểm Toán", "Điểm Văn", "Điểm TB"]);
            cboSapXep.SelectedIndex = 0;

            cboKichThuocTrang.Items.AddRange(["20", "50", "200"]);
            cboKichThuocTrang.SelectedIndex = 0;

            _isInitializing = false;
            TaiDuLieuDiem();
        }

        /// <summary>
        /// Tạo câu truy vấn đa điều kiện trên bảng DIEM.
        /// Lưu ý: dùng công thức (d.Toan + d.Van) / 2.0 thay cho thuộc tính [NotMapped] DiemTB
        /// để EF Core dịch trực tiếp xuống SQL Server (tránh lỗi dịch biểu thức).
        /// </summary>
        private IQueryable<Diem> XayDungTruyVanLoc(QuanLyDbContext db)
        {
            IQueryable<Diem> query = db.Diems.AsNoTracking();

            string hoTenLoc = txtTimHoTen.Text.Trim();
            if (!string.IsNullOrEmpty(hoTenLoc))
            {
                query = query.Where(d => d.Hoten.Contains(hoTenLoc));
            }

            if (InputValidator.DocDiemHopLe(txtDiemTu.Text, out double diemTu))
            {
                query = query.Where(d => (d.Toan + d.Van) / 2.0 >= diemTu);
            }

            if (InputValidator.DocDiemHopLe(txtDiemDen.Text, out double diemDen))
            {
                query = query.Where(d => (d.Toan + d.Van) / 2.0 <= diemDen);
            }

            string? xepLoai = cboXepLoai.SelectedItem?.ToString();
            query = xepLoai switch
            {
                "Giỏi" => query.Where(d => (d.Toan + d.Van) / 2.0 >= 8.0),
                "Khá" => query.Where(d => (d.Toan + d.Van) / 2.0 >= 6.5 && (d.Toan + d.Van) / 2.0 < 8.0),
                "Trung bình" => query.Where(d => (d.Toan + d.Van) / 2.0 >= 5.0 && (d.Toan + d.Van) / 2.0 < 6.5),
                "Yếu" => query.Where(d => (d.Toan + d.Van) / 2.0 < 5.0),
                _ => query
            };

            return query;
        }

        private void TaiDuLieuDiem()
        {
            try
            {
                using var db = new QuanLyDbContext();
                IQueryable<Diem> query = XayDungTruyVanLoc(db);

                // 1. Thống kê tổng hợp trên tập kết quả lọc
                int tongBanGhi = query.Count();
                if (tongBanGhi > 0)
                {
                    double dtbChung = query.Average(d => (d.Toan + d.Van) / 2.0);
                    double dtbMax = query.Max(d => (d.Toan + d.Van) / 2.0);
                    double dtbMin = query.Min(d => (d.Toan + d.Van) / 2.0);
                    lblThongKe.Text = $"Tổng: {tongBanGhi} bản ghi | ĐTB chung: {dtbChung:0.00} | Cao nhất: {dtbMax:0.00} | Thấp nhất: {dtbMin:0.00}";
                }
                else
                {
                    lblThongKe.Text = "Tổng: 0 bản ghi phù hợp";
                }

                // 2. Sắp xếp Tăng dần / Giảm dần
                int sortIdx = Math.Max(0, cboSapXep.SelectedIndex);
                query = sortIdx switch
                {
                    1 => _sapXepTangDan ? query.OrderBy(d => d.Toan).ThenBy(d => d.Hoten) : query.OrderByDescending(d => d.Toan).ThenBy(d => d.Hoten),
                    2 => _sapXepTangDan ? query.OrderBy(d => d.Van).ThenBy(d => d.Hoten) : query.OrderByDescending(d => d.Van).ThenBy(d => d.Hoten),
                    3 => _sapXepTangDan ? query.OrderBy(d => (d.Toan + d.Van) / 2.0).ThenBy(d => d.Hoten) : query.OrderByDescending(d => (d.Toan + d.Van) / 2.0).ThenBy(d => d.Hoten),
                    _ => _sapXepTangDan ? query.OrderBy(d => d.Hoten) : query.OrderByDescending(d => d.Hoten)
                };

                // 3. Phân trang tại tầng SQL Server
                int kichThuocTrang = int.TryParse(cboKichThuocTrang.SelectedItem?.ToString(), out int size) ? size : 20;
                GridHelper.CapNhatPhanTrang(tongBanGhi, ref _trangHienTai, kichThuocTrang, out _tongTrang, lblTrang, btnTrangTruoc, btnTrangSau);

                dgvKetQua.DataSource = query
                    .Skip((_trangHienTai - 1) * kichThuocTrang)
                    .Take(kichThuocTrang)
                    .ToList();

                GridHelper.ApDungAnHienCot(
                    dgvKetQua,
                    (chkCotHoTen, "Hoten"),
                    (chkCotToan, "Toan"),
                    (chkCotVan, "Van"),
                    (chkCotDiemTB, "DiemTB"),
                    (chkCotXepLoai, "XepLoai"));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể truy vấn bảng DIEM từ SQL Server:\n" + ex.Message, "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTimKiem_Click(object? sender, EventArgs e)
        {
            _trangHienTai = 1;
            TaiDuLieuDiem();
        }

        private void btnBoLoc_Click(object? sender, EventArgs e)
        {
            _isInitializing = true;
            txtTimHoTen.Clear();
            txtDiemTu.Clear();
            txtDiemDen.Clear();
            cboXepLoai.SelectedIndex = 0;
            _isInitializing = false;
            _trangHienTai = 1;
            TaiDuLieuDiem();
        }

        private void cboSapXep_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (_isInitializing) return;
            _trangHienTai = 1;
            TaiDuLieuDiem();
        }

        private void btnSapXep_Click(object? sender, EventArgs e)
        {
            _sapXepTangDan = !_sapXepTangDan;
            GridHelper.CapNhatNutSapXep(btnSapXep, _sapXepTangDan);
            _trangHienTai = 1;
            TaiDuLieuDiem();
        }

        private void dgvKetQua_ColumnHeaderMouseClick(object? sender, DataGridViewCellMouseEventArgs e)
        {
            _trangHienTai = 1;
            GridHelper.XuLyClickTieuDeCot(dgvKetQua, e.ColumnIndex, cboSapXep, CotSapXep, ref _sapXepTangDan, btnSapXep, TaiDuLieuDiem);
        }

        private void cboKichThuocTrang_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (_isInitializing) return;
            _trangHienTai = 1;
            TaiDuLieuDiem();
        }

        private void btnTrangTruoc_Click(object? sender, EventArgs e)
        {
            if (_trangHienTai > 1)
            {
                _trangHienTai--;
                TaiDuLieuDiem();
            }
        }

        private void btnTrangSau_Click(object? sender, EventArgs e)
        {
            if (_trangHienTai < _tongTrang)
            {
                _trangHienTai++;
                TaiDuLieuDiem();
            }
        }

        private void dgvKetQua_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgvKetQua.Rows[e.RowIndex].DataBoundItem is Diem diemDangChon)
            {
                txtHoTen.Text = diemDangChon.Hoten;
                txtToan.Text = diemDangChon.Toan.ToString(CultureInfo.InvariantCulture);
                txtVan.Text = diemDangChon.Van.ToString(CultureInfo.InvariantCulture);
            }
        }

        private void btnThem_Click(object? sender, EventArgs e)
        {
            string hoten = txtHoTen.Text.Trim();
            if (string.IsNullOrEmpty(hoten))
            {
                MessageBox.Show("Vui lòng nhập họ tên.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHoTen.Focus();
                return;
            }

            if (!InputValidator.DocDiemHopLe(txtToan.Text, out double toan))
            {
                MessageBox.Show("Điểm Toán phải là số từ 0 đến 10 (ví dụ: 8.5).", "Sai định dạng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtToan.Focus();
                return;
            }

            if (!InputValidator.DocDiemHopLe(txtVan.Text, out double van))
            {
                MessageBox.Show("Điểm Văn phải là số từ 0 đến 10 (ví dụ: 7.5).", "Sai định dạng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtVan.Focus();
                return;
            }

            try
            {
                using var db = new QuanLyDbContext();

                if (db.Diems.Find(hoten) != null)
                {
                    MessageBox.Show($"Đã có bảng điểm của '{hoten}' trong bảng DIEM (trùng khóa chính).", "Trùng dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                db.Diems.Add(new Diem { Hoten = hoten, Toan = toan, Van = van });
                db.SaveChanges();

                XoaOThongTin();
                TaiDuLieuDiem();
                MessageBox.Show("Thêm điểm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm điểm:\n" + (ex.InnerException?.Message ?? ex.Message), "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object? sender, EventArgs e)
        {
            string hoten = txtHoTen.Text.Trim();
            if (string.IsNullOrEmpty(hoten))
            {
                MessageBox.Show("Vui lòng chọn 1 dòng trên bảng hoặc nhập họ tên cần sửa.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHoTen.Focus();
                return;
            }

            if (!InputValidator.DocDiemHopLe(txtToan.Text, out double toan))
            {
                MessageBox.Show("Điểm Toán phải là số từ 0 đến 10.", "Sai định dạng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtToan.Focus();
                return;
            }

            if (!InputValidator.DocDiemHopLe(txtVan.Text, out double van))
            {
                MessageBox.Show("Điểm Văn phải là số từ 0 đến 10.", "Sai định dạng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtVan.Focus();
                return;
            }

            try
            {
                using var db = new QuanLyDbContext();

                Diem? diemCanSua = db.Diems.Find(hoten);
                if (diemCanSua == null)
                {
                    MessageBox.Show($"Không tìm thấy bảng điểm của '{hoten}' để sửa.", "Không tìm thấy", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                diemCanSua.Toan = toan;
                diemCanSua.Van = van;
                db.SaveChanges();

                TaiDuLieuDiem();
                MessageBox.Show("Cập nhật điểm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật điểm:\n" + (ex.InnerException?.Message ?? ex.Message), "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object? sender, EventArgs e)
        {
            string hoten = txtHoTen.Text.Trim();
            if (string.IsNullOrEmpty(hoten))
            {
                MessageBox.Show("Vui lòng chọn 1 dòng trên bảng hoặc nhập họ tên cần xóa.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHoTen.Focus();
                return;
            }

            if (MessageBox.Show($"Bạn có chắc chắn muốn xóa bảng điểm của '{hoten}' khỏi bảng DIEM?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            try
            {
                using var db = new QuanLyDbContext();

                Diem? diemCanXoa = db.Diems.Find(hoten);
                if (diemCanXoa == null)
                {
                    MessageBox.Show($"Không tìm thấy '{hoten}' trong bảng DIEM.", "Không tìm thấy", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                db.Diems.Remove(diemCanXoa);
                db.SaveChanges();

                XoaOThongTin();
                TaiDuLieuDiem();
                MessageBox.Show("Đã xóa bảng điểm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa dữ liệu:\n" + (ex.InnerException?.Message ?? ex.Message), "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Giao dịch hàng loạt & Rollback: Cộng +0.5 điểm Toán cho toàn bộ các dòng đang lọc trong 1 Transaction.
        /// Nếu có học sinh có điểm Toán > 9.5 (sau khi cộng > 10.0), Trigger trg_KiemTraDiemHopLe
        /// trên SQL Server sẽ ném lỗi và Transaction tự Rollback toàn bộ.
        /// </summary>
        private void btnBatch_Click(object? sender, EventArgs e)
        {
            try
            {
                using var db = new QuanLyDbContext();
                List<string> dsHoTen = XayDungTruyVanLoc(db).Select(d => d.Hoten).ToList();

                if (dsHoTen.Count == 0)
                {
                    MessageBox.Show("Không có bản ghi nào trong danh sách lọc để cộng điểm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                DialogResult confirm = MessageBox.Show(
                    $"Cộng +0.5 điểm Toán cho {dsHoTen.Count} bản ghi đang lọc?\n(Nếu có ai vượt quá 10 điểm, Trigger SQL Server sẽ chặn và Rollback toàn bộ!).",
                    "Xác nhận Giao dịch hàng loạt",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirm != DialogResult.Yes) return;

                using var transaction = db.Database.BeginTransaction();
                try
                {
                    List<Diem> entities = db.Diems.Where(d => dsHoTen.Contains(d.Hoten)).ToList();
                    foreach (Diem d in entities)
                    {
                        d.Toan = Math.Round(d.Toan + 0.5, 2);
                    }

                    db.SaveChanges();
                    transaction.Commit();

                    TaiDuLieuDiem();
                    MessageBox.Show($"Đã Commit thành công: cộng +0.5 điểm Toán cho {entities.Count} bản ghi!", "Transaction Commit", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show(
                        "Giao dịch hàng loạt đã bị HỦY (Rollback) toàn bộ!\nNguyên nhân: " + (ex.InnerException?.Message ?? ex.Message),
                        "Transaction Rollback",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khởi tạo giao dịch:\n" + ex.Message, "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Nhập hàng loạt từ file CSV (định dạng cột: hoten, toan, van) trong 1 Transaction có Rollback khi lỗi.
        /// </summary>
        private void btnNhapCsv_Click(object? sender, EventArgs e)
        {
            if (!CsvHelper.DocFileCsv(out List<string[]> cacDong))
            {
                return;
            }

            using var db = new QuanLyDbContext();
            using var transaction = db.Database.BeginTransaction();
            try
            {
                int soDongThem = 0;
                for (int i = 0; i < cacDong.Count; i++)
                {
                    string[] cols = cacDong[i];
                    if (cols.Length < 3)
                    {
                        throw new InvalidOperationException($"Dòng dữ liệu số {i + 1} thiếu cột (cần ít nhất 3 cột: Họ tên, Điểm Toán, Điểm Văn).");
                    }

                    string hoten = cols[0].Trim();
                    if (string.IsNullOrEmpty(hoten))
                    {
                        throw new InvalidOperationException($"Dòng dữ liệu số {i + 1} có Họ tên trống.");
                    }

                    if (!double.TryParse(cols[1].Trim().Replace(',', '.'), NumberStyles.Float, CultureInfo.InvariantCulture, out double toan) ||
                        !double.TryParse(cols[2].Trim().Replace(',', '.'), NumberStyles.Float, CultureInfo.InvariantCulture, out double van))
                    {
                        throw new InvalidOperationException($"Dòng dữ liệu số {i + 1} ('{hoten}') có điểm không phải số hợp lệ.");
                    }

                    if (db.Diems.Find(hoten) != null)
                    {
                        throw new InvalidOperationException($"Dòng dữ liệu số {i + 1}: '{hoten}' đã tồn tại trong bảng DIEM.");
                    }

                    db.Diems.Add(new Diem { Hoten = hoten, Toan = toan, Van = van });
                    soDongThem++;
                }

                db.SaveChanges();
                transaction.Commit();

                TaiDuLieuDiem();
                MessageBox.Show($"Đã Import thành công {soDongThem} bản ghi vào bảng DIEM!", "Nhập CSV thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                MessageBox.Show(
                    "Import CSV thất bại — đã Rollback toàn bộ file:\n" + (ex.InnerException?.Message ?? ex.Message),
                    "Import Rollback",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnXuatCsv_Click(object? sender, EventArgs e)
        {
            CsvHelper.XuatRaCsv(dgvKetQua, "DanhSach_DIEM.csv");
        }

        private void btnBieuDo_Click(object? sender, EventArgs e)
        {
            using var formBieuDo = new FormBieuDo();
            formBieuDo.ShowDialog(this);
        }

        private void XoaOThongTin()
        {
            txtHoTen.Clear();
            txtToan.Clear();
            txtVan.Clear();
        }

        private void btnVeForm1_Click(object? sender, EventArgs e)
        {
            this.Close();
        }
    }
}
