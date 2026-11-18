using System.Globalization;
using DBDemoWinFormApp.Data;
using DBDemoWinFormApp.Models;

namespace DBDemoWinFormApp
{
    public partial class Form3 : Form
    {
        /// <summary>
        /// Hàm khởi tạo (Constructor) của Form3.
        /// Dựng giao diện quản lý bảng DIEM(hoten, toan, van) đối xứng với Form2.
        /// </summary>
        public Form3()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Sự kiện tự động chạy ngay khi Form3 vừa được mở lên.
        /// Tải toàn bộ danh sách điểm từ SQL Server lên DataGridView.
        /// </summary>
        private void Form3_Load(object? sender, EventArgs e)
        {
            TaiDuLieuDiem("");
        }

        /// <summary>
        /// Sự kiện khi người dùng nhấn nút "Tìm kiếm".
        /// Lọc bảng DIEM theo họ tên (nhập một phần tên như "An" cũng tìm được).
        /// </summary>
        private void btnTimKiem_Click(object? sender, EventArgs e)
        {
            string tuKhoa = txtTimHoTen.Text.Trim();
            TaiDuLieuDiem(tuKhoa);
        }

        /// <summary>
        /// Hàm đọc và tìm kiếm dữ liệu bảng DIEM thông qua QuanLyDbContext:
        ///   Bước 1: Mở DbContext kết nối tới SQL Server.
        ///   Bước 2: Nếu từ khóa trống -> lấy toàn bộ; nếu có từ khóa -> lọc theo Hoten.Contains(tuKhoa).
        ///   Bước 3: Đổ danh sách vào DataGridView.
        /// </summary>
        private void TaiDuLieuDiem(string tuKhoa)
        {
            try
            {
                // Bước 1: Mở phiên làm việc với CSDL QUANLY.
                using var db = new QuanLyDbContext();

                List<Diem> danhSach;

                // Bước 2: Truy vấn bảng DIEM.
                if (string.IsNullOrWhiteSpace(tuKhoa))
                {
                    // EF Core dịch thành: SELECT [hoten], [toan], [van] FROM [DIEM]
                    danhSach = db.Diems.ToList();
                }
                else
                {
                    // Dùng .Contains(tuKhoa) giúp gõ một phần họ tên (ví dụ "An") vẫn tìm ra kết quả.
                    // EF Core tự động dịch thành câu SQL có Parameter chống SQL Injection:
                    //   SELECT [hoten], [toan], [van] FROM [DIEM] WHERE [hoten] LIKE N'%' + @__tuKhoa_0 + N'%'
                    danhSach = db.Diems
                        .Where(diem => diem.Hoten.Contains(tuKhoa))
                        .ToList();
                }

                // Bước 3: Hiển thị dữ liệu lên bảng.
                dgvKetQua.DataSource = danhSach;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không thể truy vấn bảng DIEM từ SQL Server:\n" + ex.Message,
                    "Lỗi CSDL",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Sự kiện khi người dùng nhấp chuột vào 1 dòng trên bảng DataGridView.
        /// Điền dữ liệu của dòng được chọn xuống 3 ô nhập để tiện Sửa hoặc Xóa.
        /// </summary>
        private void dgvKetQua_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            if (dgvKetQua.Rows[e.RowIndex].DataBoundItem is Diem diemDangChon)
            {
                txtHoTen.Text = diemDangChon.Hoten;
                txtToan.Text = diemDangChon.Toan.ToString(CultureInfo.InvariantCulture);
                txtVan.Text = diemDangChon.Van.ToString(CultureInfo.InvariantCulture);
            }
        }

        /// <summary>
        /// Sự kiện khi người dùng nhấn nút "Thêm".
        /// Luồng thêm mới 1 bản ghi điểm vào bảng DIEM:
        ///   (1) Kiểm tra họ tên và điểm Toán/Văn hợp lệ (0 - 10).
        ///   (2) Kiểm tra trùng khóa chính (hoten) bằng db.Diems.Find(hoten).
        ///   (3) Gọi db.Diems.Add(...) và db.SaveChanges() để thực thi lệnh INSERT.
        ///   (4) Làm mới ô nhập, tải lại bảng và thông báo thành công.
        /// </summary>
        private void btnThem_Click(object? sender, EventArgs e)
        {
            // Bước 1: Đọc và kiểm tra dữ liệu nhập.
            string hoten = txtHoTen.Text.Trim();
            if (string.IsNullOrEmpty(hoten))
            {
                MessageBox.Show("Vui lòng nhập họ tên.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHoTen.Focus();
                return;
            }

            if (!DocDiemHopLe(txtToan.Text, out double toan))
            {
                MessageBox.Show("Điểm Toán phải là số từ 0 đến 10 (ví dụ: 8.5).", "Sai định dạng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtToan.Focus();
                return;
            }

            if (!DocDiemHopLe(txtVan.Text, out double van))
            {
                MessageBox.Show("Điểm Văn phải là số từ 0 đến 10 (ví dụ: 7.5).", "Sai định dạng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtVan.Focus();
                return;
            }

            try
            {
                // Bước 2: Mở DbContext và kiểm tra trùng khóa chính (hoten).
                using var db = new QuanLyDbContext();

                if (db.Diems.Find(hoten) != null)
                {
                    MessageBox.Show(
                        $"Đã có bảng điểm của '{hoten}' trong bảng DIEM (trùng khóa chính).",
                        "Trùng dữ liệu",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                // Bước 3: Thêm đối tượng Diem mới và gọi SaveChanges() -> sinh lệnh INSERT INTO [DIEM]...
                var diemMoi = new Diem
                {
                    Hoten = hoten,
                    Toan = toan,
                    Van = van
                };

                db.Diems.Add(diemMoi);
                db.SaveChanges();

                // Bước 4: Làm mới giao diện và tải lại bảng.
                XoaOThongTin();
                TaiDuLieuDiem(txtTimHoTen.Text.Trim());
                MessageBox.Show("Thêm điểm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm điểm:\n" + ex.Message, "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Sự kiện khi người dùng nhấn nút "Sửa".
        /// Luồng cập nhật điểm Toán / Văn theo khóa chính hoten:
        ///   (1) Kiểm tra dữ liệu hợp lệ.
        ///   (2) Tìm dòng cần sửa bằng db.Diems.Find(hoten).
        ///   (3) Gán điểm mới rồi gọi db.SaveChanges() để thực thi lệnh UPDATE.
        ///   (4) Tải lại bảng và thông báo thành công.
        /// </summary>
        private void btnSua_Click(object? sender, EventArgs e)
        {
            // Bước 1: Kiểm tra dữ liệu nhập.
            string hoten = txtHoTen.Text.Trim();
            if (string.IsNullOrEmpty(hoten))
            {
                MessageBox.Show("Vui lòng chọn 1 dòng trên bảng hoặc nhập họ tên cần sửa.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHoTen.Focus();
                return;
            }

            if (!DocDiemHopLe(txtToan.Text, out double toan))
            {
                MessageBox.Show("Điểm Toán phải là số từ 0 đến 10.", "Sai định dạng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtToan.Focus();
                return;
            }

            if (!DocDiemHopLe(txtVan.Text, out double van))
            {
                MessageBox.Show("Điểm Văn phải là số từ 0 đến 10.", "Sai định dạng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtVan.Focus();
                return;
            }

            try
            {
                using var db = new QuanLyDbContext();

                // Bước 2: Tìm bản ghi cần sửa theo khóa chính (hoten).
                Diem? diemCanSua = db.Diems.Find(hoten);
                if (diemCanSua == null)
                {
                    MessageBox.Show(
                        $"Không tìm thấy bảng điểm của '{hoten}' để sửa.\nLưu ý: Họ tên là khóa chính dùng để xác định dòng cần sửa.",
                        "Không tìm thấy",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                // Bước 3: Cập nhật điểm Toán, Văn và gọi SaveChanges() -> sinh lệnh UPDATE [DIEM]...
                diemCanSua.Toan = toan;
                diemCanSua.Van = van;
                db.SaveChanges();

                // Bước 4: Tải lại dữ liệu từ SQL Server.
                TaiDuLieuDiem(txtTimHoTen.Text.Trim());
                MessageBox.Show("Cập nhật điểm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật điểm:\n" + ex.Message, "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Sự kiện khi người dùng nhấn nút "Xóa".
        /// Luồng xóa 1 bản ghi khỏi bảng DIEM:
        ///   (1) Kiểm tra họ tên cần xóa và hỏi xác nhận.
        ///   (2) Tìm bản ghi theo khóa chính bằng db.Diems.Find(hoten).
        ///   (3) Gọi db.Diems.Remove(...) và db.SaveChanges() để thực thi lệnh DELETE.
        ///   (4) Xóa trắng ô nhập, tải lại bảng và thông báo thành công.
        /// </summary>
        private void btnXoa_Click(object? sender, EventArgs e)
        {
            // Bước 1: Kiểm tra tên cần xóa và xác nhận với người dùng.
            string hoten = txtHoTen.Text.Trim();
            if (string.IsNullOrEmpty(hoten))
            {
                MessageBox.Show("Vui lòng chọn 1 dòng trên bảng hoặc nhập họ tên cần xóa.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHoTen.Focus();
                return;
            }

            DialogResult xacNhan = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa bảng điểm của '{hoten}' khỏi bảng DIEM?",
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

                // Bước 2: Tìm bản ghi cần xóa trong bảng DIEM theo khóa chính (hoten).
                Diem? diemCanXoa = db.Diems.Find(hoten);
                if (diemCanXoa == null)
                {
                    MessageBox.Show($"Không tìm thấy '{hoten}' trong bảng DIEM.", "Không tìm thấy", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Bước 3: Đánh dấu Remove và gọi SaveChanges() -> sinh lệnh DELETE FROM [DIEM]...
                db.Diems.Remove(diemCanXoa);
                db.SaveChanges();

                // Bước 4: Làm mới ô nhập và tải lại bảng.
                XoaOThongTin();
                TaiDuLieuDiem(txtTimHoTen.Text.Trim());
                MessageBox.Show("Đã xóa bảng điểm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa dữ liệu:\n" + ex.Message, "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Hàm phụ trợ chuyển chuỗi điểm sang số thực hợp lệ (0 - 10).
        /// Đổi dấu phẩy ',' thành dấu chấm '.' để tránh lỗi khác biệt vùng miền trên Windows tiếng Việt.
        /// </summary>
        private static bool DocDiemHopLe(string chuoiDiem, out double ketQua)
        {
            string chuanHoa = chuoiDiem.Trim().Replace(',', '.');
            return double.TryParse(chuanHoa, NumberStyles.Float, CultureInfo.InvariantCulture, out ketQua)
                && ketQua >= 0
                && ketQua <= 10;
        }

        /// <summary>
        /// Hàm phụ trợ xóa trống 3 ô nhập sau khi Thêm hoặc Xóa.
        /// </summary>
        private void XoaOThongTin()
        {
            txtHoTen.Clear();
            txtToan.Clear();
            txtVan.Clear();
        }

        /// <summary>
        /// Sự kiện khi người dùng nhấn nút "Về Form 1".
        /// Đóng Form3 để quay về màn hình Trang chính.
        /// </summary>
        private void btnVeForm1_Click(object? sender, EventArgs e)
        {
            this.Close();
        }
    }
}
