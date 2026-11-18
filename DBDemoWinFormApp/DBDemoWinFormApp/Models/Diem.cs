using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBDemoWinFormApp.Models
{
    /// <summary>
    /// Lớp Model đại diện cho 1 dòng dữ liệu trong bảng DIEM(hoten, toan, van) của SQL Server.
    /// </summary>
    // [Table("DIEM")]: Ánh xạ class Diem với bảng "DIEM" trong CSDL QUANLY.
    [Table("DIEM")]
    public class Diem
    {
        // [Key]: Khóa chính giúp EF Core định danh từng bản ghi khi đọc, sửa hoặc xóa.
        // [Column("hoten")]: Ánh xạ thuộc tính Hoten với cột "hoten" trong SQL Server.
        // [DisplayName(...)]: Tên cột hiển thị thân thiện khi gắn dữ liệu vào DataGridView.
        [Key]
        [Column("hoten")]
        [StringLength(100)]
        [DisplayName("Họ tên")]
        public string Hoten { get; set; } = string.Empty;

        [Column("toan")]
        [DisplayName("Điểm Toán")]
        public double Toan { get; set; }

        [Column("van")]
        [DisplayName("Điểm Văn")]
        public double Van { get; set; }
    }
}
