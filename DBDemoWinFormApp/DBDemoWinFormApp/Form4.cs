using System.Globalization;
using DBDemoWinFormApp.Data;
using DBDemoWinFormApp.Helpers;
using DBDemoWinFormApp.Models;

namespace DBDemoWinFormApp
{
    public partial class Form4 : Form
    {
        /// <summary>
        /// Hàm khởi tạo (Constructor) của Form4.
        /// Dựng giao diện giống Form2 nhưng hiển thị bảng gộp cả NGUOI + DIEM (5 cột)
        /// và cho phép Thêm / Sửa / Xóa đồng thời trên cả 2 bảng.
        /// </summary>
        public Form4()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Sự kiện tự động chạy khi Form4 vừa mở lên: tải toàn bộ bảng gộp NGUOI + DIEM.
        /// </summary>
        private void Form4_Load(object? sender, EventArgs e)
        {
            TaiDuLieuGop("");
        }

        /// <summary>
        /// Sự kiện khi nhấn nút "Tìm kiếm": lọc theo địa chỉ hoặc họ tên.
        /// </summary>
        private void btnTimKiem_Click(object? sender, EventArgs e)
        {
            string tuKhoa = txtTimKiem.Text.Trim();
            TaiDuLieuGop(tuKhoa);
        }

        /// <summary>
        /// Hàm ghép (LEFT JOIN) bảng NGUOI và bảng DIEM qua QuanLyDbContext:
        ///   Bước 1: Mở DbContext kết nối SQL Server.
        ///   Bước 2: Viết câu LINQ ghép bảng NGUOI với bảng DIEM theo điều kiện NGUOI.ten = DIEM.hoten.
        ///           EF Core sẽ dịch sang SQL:
        ///             SELECT n.ten, n.tuoi, n.dchi, d.toan, d.van
        ///             FROM NGUOI AS n LEFT JOIN DIEM AS d ON n.ten = d.hoten
        ///   Bước 3: Nếu có từ khóa, thêm điều kiện lọc (Parameter hóa tự động để chống SQL Injection).
        ///   Bước 4: Đổ kết quả NguoiDiemView vào DataGridView.
        /// </summary>
        private void TaiDuLieuGop(string tuKhoa)
        {
            try
            {
                // Bước 1: Tạo DbContext (tự giải phóng kết nối khi thoát khối using).
                using var db = new QuanLyDbContext();

                // Bước 2: Ghép (LEFT JOIN) NGUOI và DIEM để kể cả người chưa có điểm vẫn hiện lên bảng.
                IQueryable<NguoiDiemView> query =
                    from n in db.Nguois
                    join d in db.Diems on n.Ten equals d.Hoten into nhomDiem
                    from d in nhomDiem.DefaultIfEmpty()
                    select new NguoiDiemView
                    {
                        Ten = n.Ten,
                        Tuoi = n.Tuoi,
                        Dchi = n.Dchi,
                        Toan = d == null ? null : (double?)d.Toan,
                        Van = d == null ? null : (double?)d.Van
                    };

                // Bước 3: Lọc theo địa chỉ hoặc họ tên bằng Parameter (@__tuKhoa_0) nếu người dùng nhập từ khóa.
                if (!string.IsNullOrWhiteSpace(tuKhoa))
                {
                    query = query.Where(x => x.Dchi.Contains(tuKhoa) || x.Ten.Contains(tuKhoa));
                }

                // Bước 4: Thực thi truy vấn sang SQL Server và gán danh sách cho DataGridView.
                dgvKetQua.DataSource = query.OrderBy(x => x.Ten).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không thể tải bảng gộp NGUOI & DIEM từ SQL Server:\n" + ex.Message,
                    "Lỗi CSDL",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Sự kiện khi người dùng nhấp vào 1 dòng trên DataGridView.
        /// Nạp toàn bộ 5 thông tin (Họ tên, Tuổi, Địa chỉ, Điểm Toán, Điểm Văn) xuống các ô nhập để Sửa / Xóa.
        /// </summary>
        private void dgvKetQua_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            if (dgvKetQua.Rows[e.RowIndex].DataBoundItem is NguoiDiemView dongDangChon)
            {
                txtTen.Text = dongDangChon.Ten;
                txtTuoi.Text = dongDangChon.Tuoi.ToString();
                txtDiaChiNhap.Text = dongDangChon.Dchi;
                txtToan.Text = dongDangChon.Toan?.ToString(CultureInfo.InvariantCulture) ?? "";
                txtVan.Text = dongDangChon.Van?.ToString(CultureInfo.InvariantCulture) ?? "";
            }
        }

        /// <summary>
        /// Sự kiện khi nhấn nút "Thêm".
        /// Thêm mới đồng thời 1 dòng vào bảng NGUOI và 1 dòng vào bảng DIEM trong CÙNG 1 transaction:
        ///   (1) Kiểm tra 5 ô nhập hợp lệ.
        ///   (2) Kiểm tra khóa chính "ten" chưa tồn tại ở cả NGUOI lẫn DIEM.
        ///   (3) Gọi db.Nguois.Add(...) và db.Diems.Add(...), rồi gọi 1 lần db.SaveChanges().
        ///       EF Core tự gộp cả 2 lệnh INSERT vào 1 giao dịch (transaction) xuống SQL Server.
        ///   (4) Xóa trắng ô nhập và tải lại bảng gộp.
        /// </summary>
        private void btnThem_Click(object? sender, EventArgs e)
        {
            // Bước 1: Kiểm tra dữ liệu nhập của cả 5 ô.
            if (!TryDocDuLieuNhap(out string ten, out int tuoi, out string dchi, out double toan, out double van))
            {
                return;
            }

            try
            {
                // Bước 2: Kiểm tra trùng khóa chính trên cả 2 bảng.
                using var db = new QuanLyDbContext();

                if (db.Nguois.Find(ten) != null || db.Diems.Find(ten) != null)
                {
                    MessageBox.Show(
                        $"Đã tồn tại '{ten}' trong bảng NGUOI hoặc DIEM (trùng khóa chính).",
                        "Trùng dữ liệu",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                // Bước 3: Thêm đối tượng mới vào cả 2 DbSet và lưu cùng lúc bằng 1 lần SaveChanges().
                db.Nguois.Add(new Nguoi { Ten = ten, Tuoi = tuoi, Dchi = dchi });
                db.Diems.Add(new Diem { Hoten = ten, Toan = toan, Van = van });
                db.SaveChanges();

                // Bước 4: Làm mới giao diện và tải lại bảng gộp.
                XoaOThongTin();
                TaiDuLieuGop(txtTimKiem.Text.Trim());
                MessageBox.Show("Đã thêm mới dữ liệu vào cả NGUOI và DIEM!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm dữ liệu:\n" + ex.Message, "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Sự kiện khi nhấn nút "Sửa".
        /// Cập nhật thông tin đồng thời trên cả 2 bảng NGUOI và DIEM theo khóa chính họ tên:
        ///   (1) Kiểm tra 5 ô nhập hợp lệ.
        ///   (2) Tìm bản ghi bên NGUOI bằng db.Nguois.Find(ten).
        ///   (3) Tìm bản ghi bên DIEM bằng db.Diems.Find(ten) (nếu người này chưa có điểm thì tự tạo mới ở DIEM).
        ///   (4) Gọi db.SaveChanges() để cập nhật cả 2 bảng trong cùng 1 transaction.
        /// </summary>
        private void btnSua_Click(object? sender, EventArgs e)
        {
            // Bước 1: Kiểm tra dữ liệu nhập.
            if (!TryDocDuLieuNhap(out string ten, out int tuoi, out string dchi, out double toan, out double van))
            {
                return;
            }

            try
            {
                using var db = new QuanLyDbContext();

                // Bước 2: Tìm dòng cần sửa trong bảng NGUOI theo khóa chính (ten).
                Nguoi? nguoiCanSua = db.Nguois.Find(ten);
                if (nguoiCanSua == null)
                {
                    MessageBox.Show(
                        $"Không tìm thấy '{ten}' trong bảng NGUOI để sửa.\nLưu ý: Họ tên là khóa chính dùng để định danh dòng cần sửa.",
                        "Không tìm thấy",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                // Cập nhật thông tin bảng NGUOI.
                nguoiCanSua.Tuoi = tuoi;
                nguoiCanSua.Dchi = dchi;

                // Bước 3: Tìm dòng điểm tương ứng bên bảng DIEM; nếu chưa có thì thêm mới vào DIEM.
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

                // Bước 4: Ghi thay đổi của cả 2 bảng xuống SQL Server trong 1 lần SaveChanges().
                db.SaveChanges();

                TaiDuLieuGop(txtTimKiem.Text.Trim());
                MessageBox.Show("Đã cập nhật dữ liệu cho cả NGUOI và DIEM!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật dữ liệu:\n" + ex.Message, "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Sự kiện khi nhấn nút "Xóa".
        /// Xóa bản ghi có họ tên tương ứng ở cả bảng DIEM và bảng NGUOI trong cùng 1 transaction.
        /// </summary>
        private void btnXoa_Click(object? sender, EventArgs e)
        {
            // Bước 1: Kiểm tra họ tên cần xóa và hỏi xác nhận.
            string ten = txtTen.Text.Trim();
            if (string.IsNullOrEmpty(ten))
            {
                MessageBox.Show("Vui lòng chọn 1 dòng trên bảng hoặc nhập họ tên cần xóa.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTen.Focus();
                return;
            }

            DialogResult xacNhan = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa '{ten}' khỏi cả 2 bảng NGUOI và DIEM?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (xacNhan != DialogResult.Yes)
            {
                return;
            }

            try
            {
                using var db = new QuanLyDbContext();

                // Bước 2: Tìm bản ghi ở cả 2 bảng theo khóa chính.
                Nguoi? nguoiCanXoa = db.Nguois.Find(ten);
                Diem? diemCanXoa = db.Diems.Find(ten);

                if (nguoiCanXoa == null && diemCanXoa == null)
                {
                    MessageBox.Show($"Không tìm thấy '{ten}' trong NGUOI hay DIEM.", "Không tìm thấy", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Bước 3: Đánh dấu Remove ở cả 2 bảng rồi lưu bằng 1 lần SaveChanges().
                if (diemCanXoa != null)
                {
                    db.Diems.Remove(diemCanXoa);
                }
                if (nguoiCanXoa != null)
                {
                    db.Nguois.Remove(nguoiCanXoa);
                }

                db.SaveChanges();

                // Bước 4: Xóa trắng ô nhập và tải lại bảng.
                XoaOThongTin();
                TaiDuLieuGop(txtTimKiem.Text.Trim());
                MessageBox.Show("Đã xóa dữ liệu khỏi cả NGUOI và DIEM!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa dữ liệu:\n" + ex.Message, "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Hàm phụ trợ đọc và kiểm tra tính hợp lệ của cả 5 ô nhập (dùng chung cho Thêm và Sửa).
        /// </summary>
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

        /// <summary>
        /// Hàm phụ trợ xóa trống toàn bộ 5 ô nhập sau khi Thêm hoặc Xóa.
        /// </summary>
        private void XoaOThongTin()
        {
            txtTen.Clear();
            txtTuoi.Clear();
            txtDiaChiNhap.Clear();
            txtToan.Clear();
            txtVan.Clear();
        }

        /// <summary>
        /// Sự kiện khi nhấn nút "Về Form 1": đóng Form4 để quay về Trang chính.
        /// </summary>
        private void btnVeForm1_Click(object? sender, EventArgs e)
        {
            this.Close();
        }
    }
}
