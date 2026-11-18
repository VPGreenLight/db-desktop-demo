using System.Globalization;

namespace DBDemoWinFormApp.Helpers
{
    /// <summary>
    /// Lớp phụ trợ dùng chung để kiểm tra dữ liệu người dùng nhập từ TextBox (Tuổi và Điểm).
    /// </summary>
    internal static class InputValidator
    {
        /// <summary>
        /// Kiểm tra chuỗi nhập vào có phải số nguyên tuổi hợp lệ (0 - 150) hay không.
        /// </summary>
        public static bool DocTuoiHopLe(string chuoiTuoi, out int tuoi)
        {
            return int.TryParse(chuoiTuoi.Trim(), out tuoi)
                && tuoi >= 0
                && tuoi <= 150;
        }

        /// <summary>
        /// Chuyển chuỗi điểm sang số thực hợp lệ (0 - 10).
        /// Đổi dấu phẩy ',' thành dấu chấm '.' để tránh lỗi khác biệt vùng miền trên Windows tiếng Việt.
        /// </summary>
        public static bool DocDiemHopLe(string chuoiDiem, out double ketQua)
        {
            string chuanHoa = chuoiDiem.Trim().Replace(',', '.');
            return double.TryParse(chuanHoa, NumberStyles.Float, CultureInfo.InvariantCulture, out ketQua)
                && ketQua >= 0
                && ketQua <= 10;
        }
    }
}
