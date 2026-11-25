using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBDemoWinFormApp.Models
{
    /// <summary>
    /// Lớp Model đại diện cho 1 dòng dữ liệu trong bảng NGUOI(ten, tuoi, dchi) của SQL Server.
    /// </summary>
    [Table("NGUOI")]
    public class Nguoi
    {
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

        /// <summary>
        /// Cột tính toán dẫn xuất ([NotMapped] - không lưu trong SQL Server, tự tính từ Tuoi khi hiển thị).
        /// </summary>
        [NotMapped]
        [DisplayName("Nhóm tuổi")]
        public string NhomTuoi => Tuoi < 20 ? "Dưới 20" : (Tuoi <= 25 ? "20 - 25 tuổi" : "Trên 25");
    }
}
