using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBDemoWinFormApp.Models
{
    /// <summary>
    /// Lớp ánh xạ tới SQL VIEW dbo.vw_NguoiDiem (gộp NGUOI + DIEM theo tên).
    /// Bổ sung thêm 2 cột tính toán dẫn xuất [NotMapped]: DiemTB và XepLoai.
    /// </summary>
    public class NguoiDiemView
    {
        [DisplayName("Họ tên")]
        public string Ten { get; set; } = string.Empty;

        [DisplayName("Tuổi")]
        public int Tuoi { get; set; }

        [DisplayName("Địa chỉ")]
        public string Dchi { get; set; } = string.Empty;

        [DisplayName("Điểm Toán")]
        public double? Toan { get; set; }

        [DisplayName("Điểm Văn")]
        public double? Van { get; set; }

        /// <summary>
        /// Cột dẫn xuất ([NotMapped]): Điểm trung bình nếu đã có đủ điểm Toán và Văn.
        /// </summary>
        [NotMapped]
        [DisplayName("Điểm TB")]
        public double? DiemTB => (Toan.HasValue && Van.HasValue)
            ? Math.Round((Toan.Value + Van.Value) / 2.0, 2)
            : null;

        /// <summary>
        /// Cột dẫn xuất ([NotMapped]): Xếp loại dựa trên Điểm TB.
        /// </summary>
        [NotMapped]
        [DisplayName("Xếp loại")]
        public string XepLoai => !DiemTB.HasValue
            ? "Chưa có điểm"
            : (DiemTB.Value >= 8.0 ? "Giỏi" : (DiemTB.Value >= 6.5 ? "Khá" : (DiemTB.Value >= 5.0 ? "Trung bình" : "Yếu")));
    }
}
