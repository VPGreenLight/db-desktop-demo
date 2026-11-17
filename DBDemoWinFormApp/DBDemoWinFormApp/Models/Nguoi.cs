using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBDemoWinFormApp.Models
{
    /// <summary>
    /// Thực thể ánh xạ tới bảng NGUOI(ten, tuoi, dchi) trong CSDL QUANLY.
    /// </summary>
    [Table("NGUOI")]
    public class Nguoi
    {
        [Key]
        [Column("ten")]
        [StringLength(100)]
        [DisplayName("Họ tên (ten)")]
        public string Ten { get; set; } = string.Empty;

        [Column("tuoi")]
        [DisplayName("Tuổi (tuoi)")]
        public int Tuoi { get; set; }

        [Column("dchi")]
        [StringLength(200)]
        [DisplayName("Địa chỉ (dchi)")]
        public string Dchi { get; set; } = string.Empty;
    }
}
