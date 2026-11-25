using DBDemoWinFormApp.Data;
using Microsoft.EntityFrameworkCore;

namespace DBDemoWinFormApp
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// Hàm khởi tạo (Constructor) của Form1.
        /// Gọi InitializeComponent() để dựng các nút bấm và nhãn hiển thị từ Form1.Designer.cs.
        /// </summary>
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Sự kiện khi người dùng nhấn nút "Nối với CSDL".
        /// Luồng xử lý kiểm tra kết nối tới SQL Server qua QuanLyDbContext:
        ///   (1) Khởi tạo DbContext (kèm từ khóa using để tự giải phóng kết nối khi dùng xong).
        ///   (2) Mở thử kết nối tới SQL Server và đóng lại ngay nếu thành công.
        ///   (3) Thông báo kết quả thành công cho người dùng.
        ///   (4) Nếu có lỗi (sai tên Server, chưa bật SQL Server, chưa tạo DB QUANLY...), bắt lỗi và hiển thị nguyên nhân.
        /// </summary>
        private void btnKetNoi_Click(object? sender, EventArgs e)
        {
            try
            {
                // Bước 1: Tạo đối tượng QuanLyDbContext.
                // Từ khóa "using" đảm bảo bộ nhớ và kết nối SQL Server tự động được giải phóng khi kết thúc khối try.
                using var db = new QuanLyDbContext();

                // Bước 2: Gọi OpenConnection() để kiểm tra kết nối thực tế tới CSDL QUANLY trên SQL Server.
                // (EF Core sẽ chạy hàm OnConfiguring trong QuanLyDbContext để lấy ConnectionString).
                db.Database.OpenConnection();
                db.Database.CloseConnection();

                // Bước 3: Kết nối thông suốt -> báo thành công theo hướng dẫn.
                MessageBox.Show(
                    "Đã kết nối CSDL QUANLY",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                // Bước 4: Kết nối thất bại -> hiển thị nguyên nhân lỗi cụ thể để người học dễ kiểm tra lại SQL Server.
                MessageBox.Show(
                    "Lỗi kết nối CSDL:\n" + ex.Message,
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Sự kiện khi người dùng nhấn nút "Form 2 - Bảng NGUOI".
        /// Luồng chuyển màn hình: tạo Form2, gắn sự kiện tự hiện lại Form1 khi Form2 đóng, rồi ẩn Form1 đi.
        /// </summary>
        private void btnSangForm2_Click(object? sender, EventArgs e)
        {
            Form2 form2 = new Form2();

            // Khi người dùng bấm "Về Form 1" hoặc nút [X] trên Form2, sự kiện FormClosed sẽ chạy
            // và gọi this.Show() để hiện lại đúng cửa sổ Form1 ban đầu (tránh tạo nhiều Form1 mới bị ẩn trong bộ nhớ).
            form2.FormClosed += (s, args) => this.Show();

            form2.Show();
            this.Hide();
        }

        /// <summary>
        /// Sự kiện khi người dùng nhấn nút "Form 3 - Bảng DIEM".
        /// Luồng chuyển màn hình tương tự Form 2: mở Form3 quản lý bảng DIEM và tự hiện lại Form1 khi đóng.
        /// </summary>
        private void btnSangForm3_Click(object? sender, EventArgs e)
        {
            Form3 form3 = new Form3();

            // Đóng Form3 thì tự động mở lại đúng cửa sổ Form1 ban đầu.
            form3.FormClosed += (s, args) => this.Show();

            form3.Show();
            this.Hide();
        }

        /// <summary>
        /// Sự kiện khi người dùng nhấn nút "Form 4 - Gộp NGUOI & DIEM".
        /// Mở Form4 hiển thị bảng gộp cả thông tin người và điểm, kèm thêm/sửa/xóa đồng thời trên 2 bảng.
        /// </summary>
        private void btnSangForm4_Click(object? sender, EventArgs e)
        {
            Form4 form4 = new Form4();

            // Đóng Form4 thì tự động mở lại đúng cửa sổ Form1 ban đầu.
            form4.FormClosed += (s, args) => this.Show();

            form4.Show();
            this.Hide();
        }

        /// <summary>
        /// Sự kiện khi người dùng nhấn nút "Biểu đồ & Thống kê (SP / View)".
        /// Mở Dashboard trực quan gọi Stored Procedure sp_ThongKeTheoDiaChi và SQL View vw_NguoiDiem.
        /// </summary>
        private void btnBieuDo_Click(object? sender, EventArgs e)
        {
            using var formBieuDo = new FormBieuDo();
            formBieuDo.ShowDialog(this);
        }

        /// <summary>
        /// Sự kiện khi người dùng nhấn nút "Thoát".
        /// Kết thúc toàn bộ ứng dụng WinForms.
        /// </summary>
        private void btnThoat_Click(object? sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
