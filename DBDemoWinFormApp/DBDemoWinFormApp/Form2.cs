using DBDemoWinFormApp.Data;
using DBDemoWinFormApp.Models;

namespace DBDemoWinFormApp
{
    public partial class Form2 : Form
    {
        /// <summary>
        /// Hàm khởi tạo (Constructor) của Form2.
        /// Dựng giao diện cơ bản gồm ô nhập địa chỉ, nút Tìm kiếm, bảng DataGridView và nút Về Form 1.
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
        /// Hàm trung tâm thực hiện luồng tương tác giữa WinForms và SQL Server thông qua QuanLyDbContext:
        ///   Bước 1: Mở phiên làm việc mới với CSDL bằng "using var db = new QuanLyDbContext();".
        ///   Bước 2: Truy vấn bảng NGUOI (có hoặc không có điều kiện lọc theo địa chỉ).
        ///   Bước 3: Gán danh sách kết quả vào dgvKetQua.DataSource để hiển thị lên màn hình.
        /// </summary>
        private void TaiDuLieuNguoi(string diaChi)
        {
            try
            {
                // Bước 1: Tạo DbContext cho lần truy vấn này (dùng "using" để tự đóng kết nối sau khi lấy xong dữ liệu).
                using var db = new QuanLyDbContext();

                List<Nguoi> danhSach;

                // Bước 2: Kiểm tra người dùng có nhập địa chỉ cần tìm hay không.
                if (string.IsNullOrWhiteSpace(diaChi))
                {
                    // Trường hợp để trống: lấy toàn bộ các dòng trong bảng NGUOI.
                    // EF Core dịch thành câu SQL: SELECT [ten], [tuoi], [dchi] FROM [NGUOI]
                    danhSach = db.Nguois.ToList();
                }
                else
                {
                    // Trường hợp có nhập địa chỉ (ví dụ: "HANOI"):
                    // Minh họa mục 2.3 & 3.2 trong vở ghi (truy vấn có PARAMETER).
                    // EF Core tự động biến "diaChi" thành tham số SQL (@__diaChi_0):
                    //   SELECT [ten], [tuoi], [dchi] FROM [NGUOI] WHERE [dchi] = @__diaChi_0
                    // Cách dùng tham số giúp an toàn trước lỗi SQL Injection và xử lý tốt ký tự đặc biệt.
                    danhSach = db.Nguois
                        .Where(nguoi => nguoi.Dchi == diaChi)
                        .ToList();
                }

                // Bước 3: Đổ danh sách kết quả vào DataGridView.
                // DataGridView tự đọc các thuộc tính của class Nguoi (Ten, Tuoi, Dchi)
                // và lấy [DisplayName] làm tên cột hiển thị.
                dgvKetQua.DataSource = danhSach;
            }
            catch (Exception ex)
            {
                // Nếu SQL Server chưa bật hoặc chưa chạy script init_quanly.sql, báo lỗi rõ ràng cho người dùng.
                MessageBox.Show(
                    "Không thể truy vấn bảng NGUOI từ SQL Server:\n" + ex.Message,
                    "Lỗi CSDL",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
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
