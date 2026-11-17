using DBDemoWinFormApp.Models;
using Microsoft.EntityFrameworkCore;

namespace DBDemoWinFormApp.Data
{
    /// <summary>
    /// DbContext quản lý kết nối và thao tác dữ liệu giữa WinForms và CSDL QUANLY (SQL Server).
    /// </summary>
    public class QuanLyDbContext : DbContext
    {
        /// <summary>
        /// Chuỗi kết nối mặc định tới SQL Server Express (theo tài liệu hướng dẫn).
        /// Có thể thay đổi Server nếu máy cài SQL Server Default Instance (.) hoặc LocalDB.
        /// </summary>
        public const string DefaultConnectionString =
            @"Server=.\SQLEXPRESS; Database=QUANLY; Integrated Security=True; TrustServerCertificate=True;";

        private readonly string _connectionString;

        public QuanLyDbContext()
        {
            _connectionString = DefaultConnectionString;
        }

        public QuanLyDbContext(string connectionString)
        {
            _connectionString = string.IsNullOrWhiteSpace(connectionString)
                ? DefaultConnectionString
                : connectionString;
        }

        public QuanLyDbContext(DbContextOptions<QuanLyDbContext> options)
            : base(options)
        {
            _connectionString = DefaultConnectionString;
        }

        public DbSet<Nguoi> Nguois => Set<Nguoi>();

        public DbSet<Diem> Diems => Set<Diem>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Nguoi>(entity =>
            {
                entity.ToTable("NGUOI");
                entity.HasKey(e => e.Ten);
                entity.Property(e => e.Ten).HasColumnName("ten").HasMaxLength(100).IsRequired();
                entity.Property(e => e.Tuoi).HasColumnName("tuoi").IsRequired();
                entity.Property(e => e.Dchi).HasColumnName("dchi").HasMaxLength(200).IsRequired();
            });

            modelBuilder.Entity<Diem>(entity =>
            {
                entity.ToTable("DIEM");
                entity.HasKey(e => e.Hoten);
                entity.Property(e => e.Hoten).HasColumnName("hoten").HasMaxLength(100).IsRequired();
                entity.Property(e => e.Toan).HasColumnName("toan").IsRequired();
                entity.Property(e => e.Van).HasColumnName("van").IsRequired();
            });
        }
    }
}
