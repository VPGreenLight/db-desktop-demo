using DBDemoWinFormApp.Models;
using Microsoft.EntityFrameworkCore;

namespace DBDemoWinFormApp.Data
{
    /// <summary>
    /// Lớp DbContext là "cầu nối" trung tâm giữa ứng dụng WinForms và CSDL QUANLY trong SQL Server.
    /// Mọi thao tác kiểm tra kết nối, truy vấn, thêm/sửa/xóa đều đi qua lớp này.
    /// </summary>
    public class QuanLyDbContext : DbContext
    {
        /// <summary>
        /// Chuỗi kết nối (Connection String) tới SQL Server:
        /// - Server=.\SQLEXPRESS : Tên máy chủ SQL Server Express cài trên máy
        ///   (Nếu máy cài SQL Server bản mặc định thì đổi thành Server=. hoặc Server=localhost).
        /// - Database=QUANLY     : Tên cơ sở dữ liệu cần làm việc.
        /// - Integrated Security=True : Đăng nhập bằng tài khoản Windows hiện tại (Windows Authentication).
        /// - TrustServerCertificate=True : Bỏ qua lỗi chứng chỉ SSL khi kết nối SQL Server cục bộ.
        /// </summary>
        public const string ConnectionString =
            @"Server=localhost; Database=QUANLY; User Id=sa; Password=123; TrustServerCertificate=True;";

        // Mỗi DbSet<T> đại diện cho 1 bảng trong SQL Server:
        // - Nguois ánh xạ tới bảng NGUOI
        // - Diems  ánh xạ tới bảng DIEM
        public DbSet<Nguoi> Nguois { get; set; } = null!;
        public DbSet<Diem> Diems { get; set; } = null!;

        /// <summary>
        /// EF Core tự động gọi hàm OnConfiguring mỗi khi khởi tạo "new QuanLyDbContext()"
        /// để biết ứng dụng đang kết nối tới hệ quản trị CSDL nào (SQL Server) và bằng chuỗi kết nối nào.
        /// </summary>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
        }
    }
}
