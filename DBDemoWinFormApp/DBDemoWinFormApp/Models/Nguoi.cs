using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBDemoWinFormApp.Models
{
    /// <summary>
    /// Lớp Model đại diện cho 1 dòng dữ liệu trong bảng NGUOI(ten, tuoi, dchi) của SQL Server.
    /// </summary>
    // [Table("NGUOI")]: Báo cho EF Core biết class Nguoi ánh xạ tới bảng tên "NGUOI" trong CSDL.
    [Table("NGUOI")]
    public class Nguoi
    {
        // [Key]: Đánh dấu đây là khóa chính (Primary Key) để EF Core phân biệt từng dòng dữ liệu.
        // [Column("ten")]: Ánh xạ thuộc tính Ten trong C# với cột "ten" trong SQL Server.
        // [DisplayName(...)]: Tên tiêu đề cột hiển thị trên bảng DataGridView trong WinForms.
        [Key]
        [Column("ten")]
        [StringLength(100)]
        [DisplayName("Họ tên")]
        public string Ten { get; set; } = string.Empty;

        [Column("tuoi")]
        [DisplayName("Tuổi")]
        public int Tuoi { get; set; }

        [Column("dchi")]
        [StringLength(200)]
        [DisplayName("Địa chỉ")]
        public string Dchi { get; set; } = string.Empty;
    }
}
