using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBDemoWinFormApp.Models
{
    /// <summary>
    /// Thực thể ánh xạ tới bảng DIEM(hoten, toan, van) trong CSDL QUANLY.
    /// </summary>
    [Table("DIEM")]
    public class Diem
    {
        [Key]
        [Column("hoten")]
        [StringLength(100)]
        [DisplayName("Họ tên (hoten)")]
        public string Hoten { get; set; } = string.Empty;

        [Column("toan")]
        [DisplayName("Toán (toan)")]
        public double Toan { get; set; }

        [Column("van")]
        [DisplayName("Văn (van)")]
        public double Van { get; set; }
    }
}
