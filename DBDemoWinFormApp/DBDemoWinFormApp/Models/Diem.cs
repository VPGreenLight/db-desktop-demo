using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBDemoWinFormApp.Models
{
    /// <summary>
    /// Lớp Model đại diện cho 1 dòng dữ liệu trong bảng DIEM(hoten, toan, van) của SQL Server.
    /// </summary>
    [Table("DIEM")]
    public class Diem
    {
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

        /// <summary>
        /// Cột tính toán dẫn xuất ([NotMapped]): Điểm trung bình = (Toán + Văn) / 2.
        /// </summary>
        [NotMapped]
        [DisplayName("Điểm TB")]
        public double DiemTB => Math.Round((Toan + Van) / 2.0, 2);

        /// <summary>
        /// Cột tính toán dẫn xuất ([NotMapped]): Xếp loại học lực theo Điểm TB.
        /// </summary>
        [NotMapped]
        [DisplayName("Xếp loại")]
        public string XepLoai => DiemTB >= 8.0 ? "Giỏi" : (DiemTB >= 6.5 ? "Khá" : (DiemTB >= 5.0 ? "Trung bình" : "Yếu"));
    }
}
