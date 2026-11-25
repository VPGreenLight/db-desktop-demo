using DBDemoWinFormApp.Data;
using DBDemoWinFormApp.Helpers;
using DBDemoWinFormApp.Models;
using Microsoft.EntityFrameworkCore;

namespace DBDemoWinFormApp
{
    public partial class Form2 : Form
    {
        private static readonly string[] CotSapXep = ["Ten", "Tuoi", "Dchi"];
        private bool _isInitializing = true;
        private bool _sapXepTangDan = true;
        private int _trangHienTai = 1;
        private int _tongTrang = 1;

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object? sender, EventArgs e)
        {
            _isInitializing = true;

            LogoHelper.KhoiTaoLogo(picLogo, chkHienLogo);
            GridHelper.GanCheckBoxAnHienCot(
                dgvKetQua,
                (chkCotTen, "Ten"),
                (chkCotTuoi, "Tuoi"),
                (chkCotDiaChi, "Dchi"),
                (chkCotNhomTuoi, "NhomTuoi"));

            cboSapXep.Items.AddRange(["Họ tên", "Tuổi", "Địa chỉ"]);
            cboSapXep.SelectedIndex = 0;

            cboKichThuocTrang.Items.AddRange(["20", "50", "200"]);
            cboKichThuocTrang.SelectedIndex = 0;

            NapDanhSachDiaChi();
            _isInitializing = false;

            TaiDuLieuNguoi();
        }

        /// <summary>
        /// Nạp danh sách các Địa chỉ duy nhất (DISTINCT) từ SQL Server vào ComboBox lọc.
        /// </summary>
        private void NapDanhSachDiaChi()
        {
            try
            {
                string luaChonCu = cboDiaChi.SelectedItem?.ToString() ?? "-- Tất cả --";
                using var db = new QuanLyDbContext();

                List<string> dsDiaChi = db.Nguois
                    .AsNoTracking()
                    .Select(n => n.Dchi)
                    .Distinct()
                    .OrderBy(d => d)
                    .ToList();

                cboDiaChi.Items.Clear();
                cboDiaChi.Items.Add("-- Tất cả --");
                foreach (string dchi in dsDiaChi)
                {
                    cboDiaChi.Items.Add(dchi);
                }

                cboDiaChi.SelectedItem = cboDiaChi.Items.Contains(luaChonCu) ? luaChonCu : "-- Tất cả --";
            }
            catch
            {
                if (cboDiaChi.Items.Count == 0)
                {
                    cboDiaChi.Items.Add("-- Tất cả --");
                    cboDiaChi.SelectedIndex = 0;
                }
            }
        }

        /// <summary>
        /// Tạo câu truy vấn LINQ đa điều kiện (Tên, Địa chỉ, Tuổi từ..đến) đẩy xuống SQL Server với Parameter.
        /// </summary>
        private IQueryable<Nguoi> XayDungTruyVanLoc(QuanLyDbContext db)
        {
            IQueryable<Nguoi> query = db.Nguois.AsNoTracking();

            string tenLoc = txtTimTen.Text.Trim();
            if (!string.IsNullOrEmpty(tenLoc))
            {
                query = query.Where(n => n.Ten.Contains(tenLoc));
            }

            string? diaChiLoc = cboDiaChi.SelectedItem?.ToString();
            if (!string.IsNullOrEmpty(diaChiLoc) && diaChiLoc != "-- Tất cả --")
            {
                query = query.Where(n => n.Dchi == diaChiLoc);
            }

            if (int.TryParse(txtTuoiTu.Text.Trim(), out int tuoiTu))
            {
                query = query.Where(n => n.Tuoi >= tuoiTu);
            }

            if (int.TryParse(txtTuoiDen.Text.Trim(), out int tuoiDen))
            {
                query = query.Where(n => n.Tuoi <= tuoiDen);
            }

            return query;
        }

        /// <summary>
        /// Tải dữ liệu bảng NGUOI với Bộ lọc đa điều kiện, Thống kê tổng hợp (COUNT/AVG/MAX/MIN),
        /// Sắp xếp Tăng/Giảm (ORDER BY) và Phân trang ở tầng SQL Server (Skip/Take).
        /// </summary>
        private void TaiDuLieuNguoi()
        {
            try
            {
                using var db = new QuanLyDbContext();
                IQueryable<Nguoi> query = XayDungTruyVanLoc(db);

                // 1. Thống kê tổng hợp trên toàn bộ tập dữ liệu khớp điều kiện lọc
                int tongBanGhi = query.Count();
                if (tongBanGhi > 0)
                {
                    double tuoiTB = query.Average(n => (double)n.Tuoi);
                    int tuoiMax = query.Max(n => n.Tuoi);
                    int tuoiMin = query.Min(n => n.Tuoi);
                    lblThongKe.Text = $"Tổng: {tongBanGhi} bản ghi | Tuổi TB: {tuoiTB:0.0} | Cao nhất: {tuoiMax} | Thấp nhất: {tuoiMin}";
                }
                else
                {
                    lblThongKe.Text = "Tổng: 0 bản ghi phù hợp";
                }

                // 2. Sắp xếp Tăng dần / Giảm dần
                int sortIdx = Math.Max(0, cboSapXep.SelectedIndex);
                query = sortIdx switch
                {
                    1 => _sapXepTangDan ? query.OrderBy(n => n.Tuoi).ThenBy(n => n.Ten) : query.OrderByDescending(n => n.Tuoi).ThenBy(n => n.Ten),
                    2 => _sapXepTangDan ? query.OrderBy(n => n.Dchi).ThenBy(n => n.Ten) : query.OrderByDescending(n => n.Dchi).ThenBy(n => n.Ten),
                    _ => _sapXepTangDan ? query.OrderBy(n => n.Ten) : query.OrderByDescending(n => n.Ten)
                };

                // 3. Phân trang (Server-side Pagination: OFFSET ... FETCH NEXT ...)
                int kichThuocTrang = int.TryParse(cboKichThuocTrang.SelectedItem?.ToString(), out int size) ? size : 20;
                GridHelper.CapNhatPhanTrang(tongBanGhi, ref _trangHienTai, kichThuocTrang, out _tongTrang, lblTrang, btnTrangTruoc, btnTrangSau);

                List<Nguoi> danhSachTrang = query
                    .Skip((_trangHienTai - 1) * kichThuocTrang)
                    .Take(kichThuocTrang)
                    .ToList();

                dgvKetQua.DataSource = danhSachTrang;
                GridHelper.ApDungAnHienCot(
                    dgvKetQua,
                    (chkCotTen, "Ten"),
                    (chkCotTuoi, "Tuoi"),
                    (chkCotDiaChi, "Dchi"),
                    (chkCotNhomTuoi, "NhomTuoi"));
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không thể truy vấn bảng NGUOI từ SQL Server:\n" + ex.Message,
                    "Lỗi CSDL",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnTimKiem_Click(object? sender, EventArgs e)
        {
            _trangHienTai = 1;
            TaiDuLieuNguoi();
        }

        private void btnBoLoc_Click(object? sender, EventArgs e)
        {
            _isInitializing = true;
            txtTimTen.Clear();
            txtTuoiTu.Clear();
            txtTuoiDen.Clear();
            if (cboDiaChi.Items.Count > 0)
            {
                cboDiaChi.SelectedIndex = 0;
            }
            _isInitializing = false;
            _trangHienTai = 1;
            TaiDuLieuNguoi();
        }

        private void cboSapXep_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (_isInitializing) return;
            _trangHienTai = 1;
            TaiDuLieuNguoi();
        }

        private void btnSapXep_Click(object? sender, EventArgs e)
        {
            _sapXepTangDan = !_sapXepTangDan;
            GridHelper.CapNhatNutSapXep(btnSapXep, _sapXepTangDan);
            _trangHienTai = 1;
            TaiDuLieuNguoi();
        }

        private void dgvKetQua_ColumnHeaderMouseClick(object? sender, DataGridViewCellMouseEventArgs e)
        {
            _trangHienTai = 1;
            GridHelper.XuLyClickTieuDeCot(dgvKetQua, e.ColumnIndex, cboSapXep, CotSapXep, ref _sapXepTangDan, btnSapXep, TaiDuLieuNguoi);
        }

        private void cboKichThuocTrang_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (_isInitializing) return;
            _trangHienTai = 1;
            TaiDuLieuNguoi();
        }

        private void btnTrangTruoc_Click(object? sender, EventArgs e)
        {
            if (_trangHienTai > 1)
            {
                _trangHienTai--;
                TaiDuLieuNguoi();
            }
        }

        private void btnTrangSau_Click(object? sender, EventArgs e)
        {
            if (_trangHienTai < _tongTrang)
            {
                _trangHienTai++;
                TaiDuLieuNguoi();
            }
        }

        private void dgvKetQua_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgvKetQua.Rows[e.RowIndex].DataBoundItem is Nguoi nguoiDangChon)
            {
                txtTen.Text = nguoiDangChon.Ten;
                txtTuoi.Text = nguoiDangChon.Tuoi.ToString();
                txtDiaChiNhap.Text = nguoiDangChon.Dchi;
            }
        }

        private void btnThem_Click(object? sender, EventArgs e)
        {
            string ten = txtTen.Text.Trim();
            string dchi = txtDiaChiNhap.Text.Trim();

            if (string.IsNullOrEmpty(ten))
            {
                MessageBox.Show("Vui lòng nhập họ tên.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTen.Focus();
                return;
            }

            if (!InputValidator.DocTuoiHopLe(txtTuoi.Text, out int tuoi))
            {
                MessageBox.Show("Tuổi phải là số nguyên từ 0 đến 150.", "Sai định dạng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTuoi.Focus();
                return;
            }

            if (string.IsNullOrEmpty(dchi))
            {
                MessageBox.Show("Vui lòng nhập địa chỉ.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDiaChiNhap.Focus();
                return;
            }

            try
            {
                using var db = new QuanLyDbContext();

                if (db.Nguois.Find(ten) != null)
                {
                    MessageBox.Show($"Đã tồn tại người tên '{ten}' trong bảng NGUOI (trùng khóa chính).", "Trùng dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                db.Nguois.Add(new Nguoi { Ten = ten, Tuoi = tuoi, Dchi = dchi });
                db.SaveChanges();

                XoaOThongTin();
                NapDanhSachDiaChi();
                TaiDuLieuNguoi();
                MessageBox.Show("Thêm dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm dữ liệu:\n" + (ex.InnerException?.Message ?? ex.Message), "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object? sender, EventArgs e)
        {
            string ten = txtTen.Text.Trim();
            string dchi = txtDiaChiNhap.Text.Trim();

            if (string.IsNullOrEmpty(ten))
            {
                MessageBox.Show("Vui lòng chọn 1 dòng trên bảng hoặc nhập họ tên cần sửa.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTen.Focus();
                return;
            }

            if (!InputValidator.DocTuoiHopLe(txtTuoi.Text, out int tuoi))
            {
                MessageBox.Show("Tuổi phải là số nguyên từ 0 đến 150.", "Sai định dạng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTuoi.Focus();
                return;
            }

            if (string.IsNullOrEmpty(dchi))
            {
                MessageBox.Show("Vui lòng nhập địa chỉ mới.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDiaChiNhap.Focus();
                return;
            }

            try
            {
                using var db = new QuanLyDbContext();

                Nguoi? nguoiCanSua = db.Nguois.Find(ten);
                if (nguoiCanSua == null)
                {
                    MessageBox.Show($"Không tìm thấy người tên '{ten}' để sửa.", "Không tìm thấy", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                nguoiCanSua.Tuoi = tuoi;
                nguoiCanSua.Dchi = dchi;
                db.SaveChanges();

                NapDanhSachDiaChi();
                TaiDuLieuNguoi();
                MessageBox.Show("Cập nhật dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi sửa dữ liệu:\n" + (ex.InnerException?.Message ?? ex.Message), "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object? sender, EventArgs e)
        {
            string ten = txtTen.Text.Trim();
            if (string.IsNullOrEmpty(ten))
            {
                MessageBox.Show("Vui lòng chọn 1 dòng trên bảng hoặc nhập họ tên cần xóa.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTen.Focus();
                return;
            }

            if (MessageBox.Show($"Bạn có chắc chắn muốn xóa '{ten}' khỏi bảng NGUOI?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            try
            {
                using var db = new QuanLyDbContext();

                Nguoi? nguoiCanXoa = db.Nguois.Find(ten);
                if (nguoiCanXoa == null)
                {
                    MessageBox.Show($"Không tìm thấy '{ten}' trong bảng NGUOI.", "Không tìm thấy", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                db.Nguois.Remove(nguoiCanXoa);
                db.SaveChanges();

                XoaOThongTin();
                NapDanhSachDiaChi();
                TaiDuLieuNguoi();
                MessageBox.Show("Đã xóa dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa dữ liệu:\n" + (ex.InnerException?.Message ?? ex.Message), "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Giao dịch hàng loạt & Rollback: Tăng +1 tuổi cho toàn bộ các dòng đang lọc trong 1 Transaction.
        /// Nếu bất kỳ dòng nào vượt quá 150 tuổi, Trigger trg_KiemTraTuoiHopLe trên SQL Server sẽ chặn và Rollback toàn bộ.
        /// </summary>
        private void btnBatch_Click(object? sender, EventArgs e)
        {
            try
            {
                using var db = new QuanLyDbContext();
                List<string> danhSachTen = XayDungTruyVanLoc(db).Select(n => n.Ten).ToList();

                if (danhSachTen.Count == 0)
                {
                    MessageBox.Show("Không có bản ghi nào trong danh sách lọc để cập nhật hàng loạt.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                DialogResult confirm = MessageBox.Show(
                    $"Tăng +1 tuổi cho {danhSachTen.Count} người đang lọc?\n(Thao tác chạy trong 1 Transaction: lỗi ở bất kỳ dòng nào sẽ Rollback toàn bộ).",
                    "Xác nhận Giao dịch hàng loạt",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirm != DialogResult.Yes) return;

                using var transaction = db.Database.BeginTransaction();
                try
                {
                    List<Nguoi> entities = db.Nguois.Where(n => danhSachTen.Contains(n.Ten)).ToList();
                    foreach (Nguoi n in entities)
                    {
                        n.Tuoi += 1;
                    }

                    db.SaveChanges();
                    transaction.Commit();

                    TaiDuLieuNguoi();
                    MessageBox.Show($"Đã Commit thành công: tăng +1 tuổi cho {entities.Count} bản ghi!", "Transaction Commit", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        /// Nhập hàng loạt từ file CSV (định dạng cột: ten, tuoi, dchi) trong 1 Transaction có Rollback khi gặp dòng lỗi.
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
                        throw new InvalidOperationException($"Dòng dữ liệu số {i + 1} thiếu cột (cần ít nhất 3 cột: Họ tên, Tuổi, Địa chỉ).");
                    }

                    string ten = cols[0].Trim();
                    string dchi = cols[2].Trim();

                    if (string.IsNullOrEmpty(ten) || string.IsNullOrEmpty(dchi))
                    {
                        throw new InvalidOperationException($"Dòng dữ liệu số {i + 1} có Họ tên hoặc Địa chỉ trống.");
                    }

                    if (!int.TryParse(cols[1].Trim(), out int tuoi))
                    {
                        throw new InvalidOperationException($"Dòng dữ liệu số {i + 1} ('{ten}') có Tuổi '{cols[1]}' không phải số nguyên.");
                    }

                    if (db.Nguois.Find(ten) != null)
                    {
                        throw new InvalidOperationException($"Dòng dữ liệu số {i + 1}: Họ tên '{ten}' đã tồn tại (trùng khóa chính).");
                    }

                    db.Nguois.Add(new Nguoi { Ten = ten, Tuoi = tuoi, Dchi = dchi });
                    soDongThem++;
                }

                db.SaveChanges();
                transaction.Commit();

                NapDanhSachDiaChi();
                TaiDuLieuNguoi();
                MessageBox.Show($"Đã Import thành công {soDongThem} bản ghi vào bảng NGUOI!", "Nhập CSV thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                MessageBox.Show(
                    "Import CSV thất bại — đã Rollback toàn bộ file để bảo toàn CSDL:\n" + (ex.InnerException?.Message ?? ex.Message),
                    "Import Rollback",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnXuatCsv_Click(object? sender, EventArgs e)
        {
            CsvHelper.XuatRaCsv(dgvKetQua, "DanhSach_NGUOI.csv");
        }

        private void btnBieuDo_Click(object? sender, EventArgs e)
        {
            using var formBieuDo = new FormBieuDo();
            formBieuDo.ShowDialog(this);
        }

        private void XoaOThongTin()
        {
            txtTen.Clear();
            txtTuoi.Clear();
            txtDiaChiNhap.Clear();
        }

        private void btnVeForm1_Click(object? sender, EventArgs e)
        {
            this.Close();
        }
    }
}
