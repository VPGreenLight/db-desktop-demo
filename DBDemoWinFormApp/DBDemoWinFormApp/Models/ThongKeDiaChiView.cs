using System.ComponentModel;

namespace DBDemoWinFormApp.Models
{
    /// <summary>
    /// Lớp chứa dữ liệu thống kê trả về từ Stored Procedure dbo.sp_ThongKeTheoDiaChi.
    /// </summary>
    public class ThongKeDiaChiView
    {
        [DisplayName("Địa chỉ")]
        public string DiaChi { get; set; } = string.Empty;

        [DisplayName("Số người")]
        public int SoNguoi { get; set; }

        [DisplayName("Tuổi TB")]
        public double TuoiTB { get; set; }

        [DisplayName("Điểm TB")]
        public double DiemTB { get; set; }
    }
}
