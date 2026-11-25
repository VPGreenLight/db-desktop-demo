using DBDemoWinFormApp.Models;
using Microsoft.EntityFrameworkCore;

namespace DBDemoWinFormApp.Data
{
    /// <summary>
    /// Lớp DbContext là "cầu nối" trung tâm giữa ứng dụng WinForms và CSDL QUANLY trong SQL Server.
    /// Mọi thao tác kiểm tra kết nối, truy vấn bảng/View, gọi Stored Procedure, thêm/sửa/xóa và Transaction đều đi qua lớp này.
    /// </summary>
    public class QuanLyDbContext : DbContext
    {
        /// <summary>
        /// Chuỗi kết nối (Connection String) tới SQL Server cục bộ.
        /// </summary>
        public const string ConnectionString =
            @"Server=localhost; Database=QUANLY; User Id=sa; Password=123; TrustServerCertificate=True;";

        // Mỗi DbSet<T> đại diện cho 1 bảng hoặc SQL View trong SQL Server:
        // - Nguois         ánh xạ tới bảng NGUOI
        // - Diems          ánh xạ tới bảng DIEM
        // - NguoiDiemViews ánh xạ tới SQL View dbo.vw_NguoiDiem
        public DbSet<Nguoi> Nguois { get; set; } = null!;
        public DbSet<Diem> Diems { get; set; } = null!;
        public DbSet<NguoiDiemView> NguoiDiemViews { get; set; } = null!;

        /// <summary>
        /// EF Core tự động gọi hàm OnConfiguring mỗi khi khởi tạo "new QuanLyDbContext()"
        /// để biết ứng dụng đang kết nối tới SQL Server bằng chuỗi kết nối nào.
        /// </summary>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
        }

        /// <summary>
        /// Khai báo cấu hình ánh xạ bảng có Trigger (tránh lỗi SQL Error 334 của EF Core khi INSERT/UPDATE có OUTPUT)
        /// và ánh xạ SQL View dbo.vw_NguoiDiem (không có khóa chính).
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Nguoi>().ToTable("NGUOI", tb => tb.HasTrigger("trg_KiemTraTuoiHopLe"));
            modelBuilder.Entity<Diem>().ToTable("DIEM", tb => tb.HasTrigger("trg_KiemTraDiemHopLe"));
            modelBuilder.Entity<NguoiDiemView>().HasNoKey().ToView("vw_NguoiDiem");
        }
    }
}
