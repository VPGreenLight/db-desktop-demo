using System.Globalization;
using DBDemoWinFormApp.Data;
using DBDemoWinFormApp.Helpers;
using DBDemoWinFormApp.Models;
using Microsoft.EntityFrameworkCore;

namespace DBDemoWinFormApp
{
    public partial class Form4 : Form
    {
        private static readonly string[] CotSapXep = ["Ten", "Tuoi", "Dchi", "Toan", "Van", "DiemTB"];
        private bool _isInitializing = true;
        private bool _sapXepTangDan = true;
        private int _trangHienTai = 1;
        private int _tongTrang = 1;

        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object? sender, EventArgs e)
        {
            _isInitializing = true;

            LogoHelper.KhoiTaoLogo(picLogo, chkHienLogo);
            GridHelper.GanCheckBoxAnHienCot(
                dgvKetQua,
                (chkCotTen, "Ten"),
                (chkCotTuoi, "Tuoi"),
                (chkCotDiaChi, "Dchi"),
                (chkCotToan, "Toan"),
                (chkCotVan, "Van"),
                (chkCotDiemTB, "DiemTB"),
                (chkCotXepLoai, "XepLoai"));

            cboSapXep.Items.AddRange(["Họ tên", "Tuổi", "Địa chỉ", "Điểm Toán", "Điểm Văn", "Điểm TB"]);
            cboSapXep.SelectedIndex = 0;

            cboKichThuocTrang.Items.AddRange(["20", "50", "200"]);
            cboKichThuocTrang.SelectedIndex = 0;

            NapDanhSachDiaChi();
            _isInitializing = false;

            TaiDuLieuGop();
        }

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
        /// Tạo câu truy vấn đa điều kiện trực tiếp trên SQL VIEW dbo.vw_NguoiDiem (db.NguoiDiemViews).
        /// </summary>
        private IQueryable<NguoiDiemView> XayDungTruyVanLoc(QuanLyDbContext db)
        {
            IQueryable<NguoiDiemView> query = db.NguoiDiemViews.AsNoTracking();

            string tenLoc = txtTimKiem.Text.Trim();
            if (!string.IsNullOrEmpty(tenLoc))
            {
                query = query.Where(x => x.Ten.Contains(tenLoc) || x.Dchi.Contains(tenLoc));
            }

            string? diaChiLoc = cboDiaChi.SelectedItem?.ToString();
            if (!string.IsNullOrEmpty(diaChiLoc) && diaChiLoc != "-- Tất cả --")
            {
                query = query.Where(x => x.Dchi == diaChiLoc);
            }

            if (int.TryParse(txtTuoiTu.Text.Trim(), out int tuoiTu))
            {
                query = query.Where(x => x.Tuoi >= tuoiTu);
            }

            if (int.TryParse(txtTuoiDen.Text.Trim(), out int tuoiDen))
            {
                query = query.Where(x => x.Tuoi <= tuoiDen);
            }

            if (InputValidator.DocDiemHopLe(txtDiemTu.Text, out double diemTu))
            {
                query = query.Where(x => x.Toan != null && x.Van != null && (x.Toan + x.Van) / 2.0 >= diemTu);
            }

            if (InputValidator.DocDiemHopLe(txtDiemDen.Text, out double diemDen))
            {
                query = query.Where(x => x.Toan != null && x.Van != null && (x.Toan + x.Van) / 2.0 <= diemDen);
            }

            return query;
        }

        /// <summary>
        /// Tải dữ liệu gộp từ SQL VIEW dbo.vw_NguoiDiem kèm Thống kê tổng hợp, Sắp xếp Tăng/Giảm và Phân trang.
        /// </summary>
        private void TaiDuLieuGop()
        {
            try
            {
                using var db = new QuanLyDbContext();
                IQueryable<NguoiDiemView> query = XayDungTruyVanLoc(db);

                // 1. Thống kê tổng hợp
                int tongBanGhi = query.Count();
                if (tongBanGhi > 0)
                {
                    double tuoiTB = query.Average(x => (double)x.Tuoi);
                    IQueryable<NguoiDiemView> coDiem = query.Where(x => x.Toan != null && x.Van != null);
                    if (coDiem.Any())
                    {
                        double dtb = coDiem.Average(x => (x.Toan!.Value + x.Van!.Value) / 2.0);
                        double dtbMax = coDiem.Max(x => (x.Toan!.Value + x.Van!.Value) / 2.0);
                        double dtbMin = coDiem.Min(x => (x.Toan!.Value + x.Van!.Value) / 2.0);
                        lblThongKe.Text = $"Tổng: {tongBanGhi} | Tuổi TB: {tuoiTB:0.0} | ĐTB: {dtb:0.00} (Max {dtbMax:0.00} / Min {dtbMin:0.00})";
                    }
                    else
                    {
                        lblThongKe.Text = $"Tổng: {tongBanGhi} bản ghi | Tuổi TB: {tuoiTB:0.0}";
                    }
                }
                else
                {
                    lblThongKe.Text = "Tổng: 0 bản ghi phù hợp";
                }

                // 2. Sắp xếp Tăng dần / Giảm dần
                int sortIdx = Math.Max(0, cboSapXep.SelectedIndex);
                query = sortIdx switch
                {
                    1 => _sapXepTangDan ? query.OrderBy(x => x.Tuoi).ThenBy(x => x.Ten) : query.OrderByDescending(x => x.Tuoi).ThenBy(x => x.Ten),
                    2 => _sapXepTangDan ? query.OrderBy(x => x.Dchi).ThenBy(x => x.Ten) : query.OrderByDescending(x => x.Dchi).ThenBy(x => x.Ten),
                    3 => _sapXepTangDan ? query.OrderBy(x => x.Toan).ThenBy(x => x.Ten) : query.OrderByDescending(x => x.Toan).ThenBy(x => x.Ten),
                    4 => _sapXepTangDan ? query.OrderBy(x => x.Van).ThenBy(x => x.Ten) : query.OrderByDescending(x => x.Van).ThenBy(x => x.Ten),
                    5 => _sapXepTangDan ? query.OrderBy(x => (x.Toan + x.Van) / 2.0).ThenBy(x => x.Ten) : query.OrderByDescending(x => (x.Toan + x.Van) / 2.0).ThenBy(x => x.Ten),
                    _ => _sapXepTangDan ? query.OrderBy(x => x.Ten) : query.OrderByDescending(x => x.Ten)
                };

                // 3. Phân trang
                int kichThuocTrang = int.TryParse(cboKichThuocTrang.SelectedItem?.ToString(), out int size) ? size : 20;
                GridHelper.CapNhatPhanTrang(tongBanGhi, ref _trangHienTai, kichThuocTrang, out _tongTrang, lblTrang, btnTrangTruoc, btnTrangSau);

                dgvKetQua.DataSource = query
                    .Skip((_trangHienTai - 1) * kichThuocTrang)
                    .Take(kichThuocTrang)
                    .ToList();

                GridHelper.ApDungAnHienCot(
                    dgvKetQua,
                    (chkCotTen, "Ten"),
                    (chkCotTuoi, "Tuoi"),
                    (chkCotDiaChi, "Dchi"),
                    (chkCotToan, "Toan"),
                    (chkCotVan, "Van"),
                    (chkCotDiemTB, "DiemTB"),
                    (chkCotXepLoai, "XepLoai"));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể tải bảng gộp vw_NguoiDiem từ SQL Server:\n" + ex.Message, "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTimKiem_Click(object? sender, EventArgs e)
        {
            _trangHienTai = 1;
            TaiDuLieuGop();
        }

        private void btnBoLoc_Click(object? sender, EventArgs e)
        {
            _isInitializing = true;
            txtTimKiem.Clear();
            txtTuoiTu.Clear();
            txtTuoiDen.Clear();
            txtDiemTu.Clear();
            txtDiemDen.Clear();
            if (cboDiaChi.Items.Count > 0)
            {
                cboDiaChi.SelectedIndex = 0;
            }
            _isInitializing = false;
            _trangHienTai = 1;
            TaiDuLieuGop();
        }

        private void cboSapXep_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (_isInitializing) return;
            _trangHienTai = 1;
            TaiDuLieuGop();
        }

        private void btnSapXep_Click(object? sender, EventArgs e)
        {
            _sapXepTangDan = !_sapXepTangDan;
            GridHelper.CapNhatNutSapXep(btnSapXep, _sapXepTangDan);
            _trangHienTai = 1;
            TaiDuLieuGop();
        }

        private void dgvKetQua_ColumnHeaderMouseClick(object? sender, DataGridViewCellMouseEventArgs e)
        {
            _trangHienTai = 1;
            GridHelper.XuLyClickTieuDeCot(dgvKetQua, e.ColumnIndex, cboSapXep, CotSapXep, ref _sapXepTangDan, btnSapXep, TaiDuLieuGop);
        }

        private void cboKichThuocTrang_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (_isInitializing) return;
            _trangHienTai = 1;
            TaiDuLieuGop();
        }

        private void btnTrangTruoc_Click(object? sender, EventArgs e)
        {
            if (_trangHienTai > 1)
            {
                _trangHienTai--;
                TaiDuLieuGop();
            }
        }

        private void btnTrangSau_Click(object? sender, EventArgs e)
        {
            if (_trangHienTai < _tongTrang)
            {
                _trangHienTai++;
                TaiDuLieuGop();
            }
        }

        private void dgvKetQua_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgvKetQua.Rows[e.RowIndex].DataBoundItem is NguoiDiemView dongDangChon)
            {
                txtTen.Text = dongDangChon.Ten;
                txtTuoi.Text = dongDangChon.Tuoi.ToString();
                txtDiaChiNhap.Text = dongDangChon.Dchi;
                txtToan.Text = dongDangChon.Toan?.ToString(CultureInfo.InvariantCulture) ?? "";
                txtVan.Text = dongDangChon.Van?.ToString(CultureInfo.InvariantCulture) ?? "";
            }
        }

        private void btnThem_Click(object? sender, EventArgs e)
        {
            if (!TryDocDuLieuNhap(out string ten, out int tuoi, out string dchi, out double toan, out double van))
            {
                return;
            }

            try
            {
                using var db = new QuanLyDbContext();

                if (db.Nguois.Find(ten) != null || db.Diems.Find(ten) != null)
                {
                    MessageBox.Show($"Đã tồn tại '{ten}' trong bảng NGUOI hoặc DIEM (trùng khóa chính).", "Trùng dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                db.Nguois.Add(new Nguoi { Ten = ten, Tuoi = tuoi, Dchi = dchi });
                db.Diems.Add(new Diem { Hoten = ten, Toan = toan, Van = van });
                db.SaveChanges();

                XoaOThongTin();
                NapDanhSachDiaChi();
                TaiDuLieuGop();
                MessageBox.Show("Đã thêm mới dữ liệu vào cả NGUOI và DIEM!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm dữ liệu:\n" + (ex.InnerException?.Message ?? ex.Message), "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object? sender, EventArgs e)
        {
            if (!TryDocDuLieuNhap(out string ten, out int tuoi, out string dchi, out double toan, out double van))
            {
                return;
            }

            try
            {
                using var db = new QuanLyDbContext();

                Nguoi? nguoiCanSua = db.Nguois.Find(ten);
                if (nguoiCanSua == null)
                {
                    MessageBox.Show($"Không tìm thấy '{ten}' trong bảng NGUOI để sửa.", "Không tìm thấy", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                nguoiCanSua.Tuoi = tuoi;
                nguoiCanSua.Dchi = dchi;

                Diem? diemCanSua = db.Diems.Find(ten);
                if (diemCanSua != null)
                {
                    diemCanSua.Toan = toan;
                    diemCanSua.Van = van;
                }
                else
                {
                    db.Diems.Add(new Diem { Hoten = ten, Toan = toan, Van = van });
                }

                db.SaveChanges();

                NapDanhSachDiaChi();
                TaiDuLieuGop();
                MessageBox.Show("Đã cập nhật dữ liệu cho cả NGUOI và DIEM!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật dữ liệu:\n" + (ex.InnerException?.Message ?? ex.Message), "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            if (MessageBox.Show($"Bạn có chắc chắn muốn xóa '{ten}' khỏi cả 2 bảng NGUOI và DIEM?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            try
            {
                using var db = new QuanLyDbContext();

                Nguoi? nguoiCanXoa = db.Nguois.Find(ten);
                Diem? diemCanXoa = db.Diems.Find(ten);

                if (nguoiCanXoa == null && diemCanXoa == null)
                {
                    MessageBox.Show($"Không tìm thấy '{ten}' trong NGUOI hay DIEM.", "Không tìm thấy", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (diemCanXoa != null) db.Diems.Remove(diemCanXoa);
                if (nguoiCanXoa != null) db.Nguois.Remove(nguoiCanXoa);
                db.SaveChanges();

                XoaOThongTin();
                NapDanhSachDiaChi();
                TaiDuLieuGop();
                MessageBox.Show("Đã xóa dữ liệu khỏi cả NGUOI và DIEM!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa dữ liệu:\n" + (ex.InnerException?.Message ?? ex.Message), "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Giao dịch hàng loạt & Rollback: Cộng +0.5 điểm Toán cho các bản ghi đang lọc trên Form4 trong 1 Transaction.
        /// </summary>
        private void btnBatch_Click(object? sender, EventArgs e)
        {
            try
            {
                using var db = new QuanLyDbContext();
                List<string> dsTen = XayDungTruyVanLoc(db).Select(x => x.Ten).ToList();

                if (dsTen.Count == 0)
                {
                    MessageBox.Show("Không có bản ghi nào trong danh sách lọc.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                DialogResult confirm = MessageBox.Show(
                    $"Cộng +0.5 điểm Toán cho các học sinh có trong {dsTen.Count} dòng đang lọc?\n(Nếu có ai vượt quá 10 điểm, Trigger SQL Server sẽ chặn và Rollback toàn bộ!).",
                    "Xác nhận Giao dịch hàng loạt",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirm != DialogResult.Yes) return;

                using var transaction = db.Database.BeginTransaction();
                try
                {
                    List<Diem> entities = db.Diems.Where(d => dsTen.Contains(d.Hoten)).ToList();
                    foreach (Diem d in entities)
                    {
                        d.Toan = Math.Round(d.Toan + 0.5, 2);
                    }

                    db.SaveChanges();
                    transaction.Commit();

                    TaiDuLieuGop();
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
        /// Nhập hàng loạt từ file CSV (5 cột: ten, tuoi, dchi, toan, van) vào cả 2 bảng trong 1 Transaction.
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
                    if (cols.Length < 5)
                    {
                        throw new InvalidOperationException($"Dòng dữ liệu số {i + 1} thiếu cột (cần ít nhất 5 cột: Họ tên, Tuổi, Địa chỉ, Toán, Văn).");
                    }

                    string ten = cols[0].Trim();
                    string dchi = cols[2].Trim();

                    if (string.IsNullOrEmpty(ten) || string.IsNullOrEmpty(dchi))
                    {
                        throw new InvalidOperationException($"Dòng dữ liệu số {i + 1} có Họ tên hoặc Địa chỉ trống.");
                    }

                    if (!int.TryParse(cols[1].Trim(), out int tuoi))
                    {
                        throw new InvalidOperationException($"Dòng dữ liệu số {i + 1} ('{ten}') có Tuổi '{cols[1]}' không hợp lệ.");
                    }

                    if (!double.TryParse(cols[3].Trim().Replace(',', '.'), NumberStyles.Float, CultureInfo.InvariantCulture, out double toan) ||
                        !double.TryParse(cols[4].Trim().Replace(',', '.'), NumberStyles.Float, CultureInfo.InvariantCulture, out double van))
                    {
                        throw new InvalidOperationException($"Dòng dữ liệu số {i + 1} ('{ten}') có điểm Toán/Văn không phải số.");
                    }

                    if (db.Nguois.Find(ten) != null || db.Diems.Find(ten) != null)
                    {
                        throw new InvalidOperationException($"Dòng dữ liệu số {i + 1}: '{ten}' đã tồn tại trong CSDL (trùng khóa chính).");
                    }

                    db.Nguois.Add(new Nguoi { Ten = ten, Tuoi = tuoi, Dchi = dchi });
                    db.Diems.Add(new Diem { Hoten = ten, Toan = toan, Van = van });
                    soDongThem++;
                }

                db.SaveChanges();
                transaction.Commit();

                NapDanhSachDiaChi();
                TaiDuLieuGop();
                MessageBox.Show($"Đã Import thành công {soDongThem} bản ghi vào cả NGUOI và DIEM!", "Nhập CSV thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            CsvHelper.XuatRaCsv(dgvKetQua, "DanhSach_NGUOI_DIEM.csv");
        }

        private void btnBieuDo_Click(object? sender, EventArgs e)
        {
            using var formBieuDo = new FormBieuDo();
            formBieuDo.ShowDialog(this);
        }

        private bool TryDocDuLieuNhap(out string ten, out int tuoi, out string dchi, out double toan, out double van)
        {
            ten = txtTen.Text.Trim();
            dchi = txtDiaChiNhap.Text.Trim();
            tuoi = 0;
            toan = 0;
            van = 0;

            if (string.IsNullOrEmpty(ten))
            {
                MessageBox.Show("Vui lòng nhập họ tên.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTen.Focus();
                return false;
            }

            if (!InputValidator.DocTuoiHopLe(txtTuoi.Text, out tuoi))
            {
                MessageBox.Show("Tuổi phải là số nguyên từ 0 đến 150.", "Sai định dạng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTuoi.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(dchi))
            {
                MessageBox.Show("Vui lòng nhập địa chỉ.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDiaChiNhap.Focus();
                return false;
            }

            if (!InputValidator.DocDiemHopLe(txtToan.Text, out toan))
            {
                MessageBox.Show("Điểm Toán phải là số từ 0 đến 10 (ví dụ: 8.5).", "Sai định dạng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtToan.Focus();
                return false;
            }

            if (!InputValidator.DocDiemHopLe(txtVan.Text, out van))
            {
                MessageBox.Show("Điểm Văn phải là số từ 0 đến 10 (ví dụ: 7.5).", "Sai định dạng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtVan.Focus();
                return false;
            }

            return true;
        }

        private void XoaOThongTin()
        {
            txtTen.Clear();
            txtTuoi.Clear();
            txtDiaChiNhap.Clear();
            txtToan.Clear();
            txtVan.Clear();
        }

        private void btnVeForm1_Click(object? sender, EventArgs e)
        {
            this.Close();
        }
    }
}
