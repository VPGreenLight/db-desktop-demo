using DBDemoWinFormApp.Data;
using DBDemoWinFormApp.Models;

namespace DBDemoWinFormApp
{
    public partial class Form2 : Form
    {
        /// <summary>
        /// Hàm khởi tạo (Constructor) của Form2.
        /// Dựng giao diện quản lý bảng NGUOI gồm Tìm kiếm, bảng DataGridView và cụm Thêm / Sửa / Xóa.
        /// </summary>
        public Form2()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Sự kiện tự động chạy ngay khi Form2 vừa được mở lên.
        /// Truyền chuỗi rỗng "" để hiển thị toàn bộ danh sách bảng NGUOI ngay lúc mới vào màn hình.
        /// </summary>
        private void Form2_Load(object? sender, EventArgs e)
        {
            TaiDuLieuNguoi("");
        }

        /// <summary>
        /// Sự kiện khi người dùng nhấn nút "Tìm kiếm".
        /// Lấy giá trị địa chỉ từ ô nhập (txtDiaChi) và gọi hàm truy vấn CSDL qua DbContext.
        /// </summary>
        private void btnTimKiem_Click(object? sender, EventArgs e)
        {
            string diaChi = txtDiaChi.Text.Trim();
            TaiDuLieuNguoi(diaChi);
        }

        /// <summary>
        /// Hàm đọc và lọc dữ liệu bảng NGUOI từ SQL Server thông qua QuanLyDbContext:
        ///   Bước 1: Mở phiên làm việc mới với CSDL bằng "using var db = new QuanLyDbContext();".
        ///   Bước 2: Truy vấn bảng NGUOI (lấy tất cả hoặc lọc theo địa chỉ có Parameter).
        ///   Bước 3: Gán danh sách kết quả vào dgvKetQua.DataSource để hiển thị lên bảng.
        /// </summary>
        private void TaiDuLieuNguoi(string diaChi)
        {
            try
            {
                // Bước 1: Tạo DbContext cho lần truy vấn này (từ khóa "using" tự giải phóng kết nối sau khi dùng).
                using var db = new QuanLyDbContext();

                List<Nguoi> danhSach;

                // Bước 2: Kiểm tra người dùng có nhập địa chỉ cần tìm hay không.
                if (string.IsNullOrWhiteSpace(diaChi))
                {
                    // Trường hợp để trống: lấy toàn bộ các dòng trong bảng NGUOI.
                    // EF Core dịch thành SQL: SELECT [ten], [tuoi], [dchi] FROM [NGUOI]
                    danhSach = db.Nguois.ToList();
                }
                else
                {
                    // Trường hợp có nhập địa chỉ (ví dụ: "HANOI"):
                    // Minh họa mục 2.3 & 3.2 trong vở ghi (truy vấn có PARAMETER).
                    // EF Core tự động biến "diaChi" thành tham số SQL (@__diaChi_0):
                    //   SELECT [ten], [tuoi], [dchi] FROM [NGUOI] WHERE [dchi] = @__diaChi_0
                    // Cách dùng tham số giúp an toàn trước lỗi SQL Injection.
                    danhSach = db.Nguois
                        .Where(nguoi => nguoi.Dchi == diaChi)
                        .ToList();
                }

                // Bước 3: Đổ danh sách kết quả vào DataGridView.
                dgvKetQua.DataSource = danhSach;
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

        /// <summary>
        /// Sự kiện khi người dùng nhấp chuột vào một dòng trên bảng DataGridView.
        /// Đọc đối tượng Nguoi gắn với dòng đó và điền sẵn vào 3 ô nhập bên dưới để tiện Sửa hoặc Xóa.
        /// </summary>
        private void dgvKetQua_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            // Bỏ qua nếu người dùng nhấp vào dòng tiêu đề cột (RowIndex = -1).
            if (e.RowIndex < 0)
            {
                return;
            }

            // DataBoundItem chính là đối tượng Nguoi tương ứng với dòng đang được chọn trên bảng.
            if (dgvKetQua.Rows[e.RowIndex].DataBoundItem is Nguoi nguoiDangChon)
            {
                txtTen.Text = nguoiDangChon.Ten;
                txtTuoi.Text = nguoiDangChon.Tuoi.ToString();
                txtDiaChiNhap.Text = nguoiDangChon.Dchi;
            }
        }

        /// <summary>
        /// Sự kiện khi người dùng nhấn nút "Thêm".
        /// Luồng thêm mới 1 bản ghi vào bảng NGUOI:
        ///   (1) Kiểm tra dữ liệu hợp lệ trên 3 ô nhập.
        ///   (2) Mở QuanLyDbContext và kiểm tra trùng khóa chính (ten).
        ///   (3) Gọi db.Nguois.Add(...) rồi db.SaveChanges() để ghi lệnh INSERT xuống SQL Server.
        ///   (4) Xóa trắng ô nhập, tải lại bảng DataGridView và thông báo thành công.
        /// </summary>
        private void btnThem_Click(object? sender, EventArgs e)
        {
            // Bước 1: Đọc và kiểm tra dữ liệu từ 3 TextBox.
            string ten = txtTen.Text.Trim();
            string dchi = txtDiaChiNhap.Text.Trim();

            if (string.IsNullOrEmpty(ten))
            {
                MessageBox.Show("Vui lòng nhập họ tên.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTen.Focus();
                return;
            }

            if (!int.TryParse(txtTuoi.Text.Trim(), out int tuoi) || tuoi < 0 || tuoi > 150)
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
                // Bước 2: Mở DbContext và kiểm tra xem khóa chính "ten" đã tồn tại trong bảng NGUOI chưa.
                using var db = new QuanLyDbContext();

                if (db.Nguois.Find(ten) != null)
                {
                    MessageBox.Show(
                        $"Đã tồn tại người tên '{ten}' trong bảng NGUOI (trùng khóa chính).",
                        "Trùng dữ liệu",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                // Bước 3: Tạo đối tượng Nguoi mới, đánh dấu Add và gọi SaveChanges().
                // SaveChanges() sẽ gửi câu lệnh SQL có tham số xuống SQL Server:
                //   INSERT INTO [NGUOI] ([ten], [dchi], [tuoi]) VALUES (@p0, @p1, @p2)
                var nguoiMoi = new Nguoi
                {
                    Ten = ten,
                    Tuoi = tuoi,
                    Dchi = dchi
                };

                db.Nguois.Add(nguoiMoi);
                db.SaveChanges();

                // Bước 4: Làm mới ô nhập, tải lại bảng để thấy ngay dòng vừa thêm và báo thành công.
                XoaOThongTin();
                TaiDuLieuNguoi(txtDiaChi.Text.Trim());
                MessageBox.Show("Thêm dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm dữ liệu:\n" + ex.Message, "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Sự kiện khi người dùng nhấn nút "Sửa".
        /// Luồng cập nhật bản ghi trong bảng NGUOI:
        ///   (1) Kiểm tra dữ liệu hợp lệ trên 3 ô nhập.
        ///   (2) Dùng db.Nguois.Find(ten) tìm bản ghi cần sửa theo khóa chính.
        ///   (3) Cập nhật thuộc tính Tuoi, Dchi và gọi db.SaveChanges() để sinh lệnh UPDATE.
        ///   (4) Tải lại bảng DataGridView và thông báo thành công.
        /// </summary>
        private void btnSua_Click(object? sender, EventArgs e)
        {
            // Bước 1: Đọc và kiểm tra dữ liệu nhập.
            string ten = txtTen.Text.Trim();
            string dchi = txtDiaChiNhap.Text.Trim();

            if (string.IsNullOrEmpty(ten))
            {
                MessageBox.Show("Vui lòng chọn 1 dòng trên bảng hoặc nhập họ tên cần sửa.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTen.Focus();
                return;
            }

            if (!int.TryParse(txtTuoi.Text.Trim(), out int tuoi) || tuoi < 0 || tuoi > 150)
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

                // Bước 2: Tìm đúng bản ghi trong CSDL dựa theo khóa chính (ten).
                Nguoi? nguoiCanSua = db.Nguois.Find(ten);
                if (nguoiCanSua == null)
                {
                    MessageBox.Show(
                        $"Không tìm thấy người tên '{ten}' để sửa.\nLưu ý: Họ tên là khóa chính dùng để xác định dòng cần sửa.",
                        "Không tìm thấy",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                // Bước 3: Thay đổi giá trị trên đối tượng đang được DbContext theo dõi rồi gọi SaveChanges().
                // EF Core tự động phát hiện thay đổi và sinh câu SQL:
                //   UPDATE [NGUOI] SET [dchi] = @p0, [tuoi] = @p1 WHERE [ten] = @p2
                nguoiCanSua.Tuoi = tuoi;
                nguoiCanSua.Dchi = dchi;
                db.SaveChanges();

                // Bước 4: Tải lại danh sách để hiển thị dữ liệu mới từ SQL Server.
                TaiDuLieuNguoi(txtDiaChi.Text.Trim());
                MessageBox.Show("Cập nhật dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi sửa dữ liệu:\n" + ex.Message, "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Sự kiện khi người dùng nhấn nút "Xóa".
        /// Luồng xóa bản ghi trong bảng NGUOI:
        ///   (1) Kiểm tra họ tên cần xóa và hỏi lại người dùng để tránh xóa nhầm.
        ///   (2) Tìm bản ghi theo khóa chính bằng db.Nguois.Find(ten).
        ///   (3) Gọi db.Nguois.Remove(...) rồi db.SaveChanges() để sinh lệnh DELETE.
        ///   (4) Xóa trắng ô nhập, tải lại bảng DataGridView và thông báo thành công.
        /// </summary>
        private void btnXoa_Click(object? sender, EventArgs e)
        {
            // Bước 1: Kiểm tra tên cần xóa và yêu cầu xác nhận.
            string ten = txtTen.Text.Trim();
            if (string.IsNullOrEmpty(ten))
            {
                MessageBox.Show("Vui lòng chọn 1 dòng trên bảng hoặc nhập họ tên cần xóa.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTen.Focus();
                return;
            }

            DialogResult xacNhan = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa '{ten}' khỏi bảng NGUOI?",
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

                // Bước 2: Tìm bản ghi cần xóa trong bảng NGUOI theo khóa chính (ten).
                Nguoi? nguoiCanXoa = db.Nguois.Find(ten);
                if (nguoiCanXoa == null)
                {
                    MessageBox.Show($"Không tìm thấy '{ten}' trong bảng NGUOI.", "Không tìm thấy", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Bước 3: Đánh dấu Remove và gọi SaveChanges().
                // EF Core sẽ gửi lệnh: DELETE FROM [NGUOI] WHERE [ten] = @p0
                db.Nguois.Remove(nguoiCanXoa);
                db.SaveChanges();

                // Bước 4: Xóa trắng ô nhập và tải lại bảng.
                XoaOThongTin();
                TaiDuLieuNguoi(txtDiaChi.Text.Trim());
                MessageBox.Show("Đã xóa dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa dữ liệu:\n" + ex.Message, "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Hàm phụ trợ xóa trống 3 ô nhập sau khi Thêm hoặc Xóa.
        /// </summary>
        private void XoaOThongTin()
        {
            txtTen.Clear();
            txtTuoi.Clear();
            txtDiaChiNhap.Clear();
        }

        /// <summary>
        /// Sự kiện khi người dùng nhấn nút "Về Form 1".
        /// Đóng Form2 lại; sự kiện FormClosed đã gắn bên Form1 sẽ tự động hiện lại Form1.
        /// </summary>
        private void btnVeForm1_Click(object? sender, EventArgs e)
        {
            this.Close();
        }
    }
}
