namespace DBDemoWinFormApp
{
    internal static class Program
    {
        /// <summary>
        /// Hàm Main là điểm bắt đầu (Entry Point) khi chạy chương trình WinForms.
        /// Thuộc tính [STAThread] là bắt buộc cho các thành phần giao diện Windows Forms.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Bước 1: Khởi tạo các cấu hình giao diện mặc định của WinForms (DPI, font chữ...).
            ApplicationConfiguration.Initialize();

            // Bước 2: Mở Form1 (Trang chính) làm cửa sổ đầu tiên của ứng dụng
            // và duy trì vòng lặp lắng nghe thao tác người dùng cho tới khi đóng Form1
            // hoặc gọi Application.Exit().
            Application.Run(new Form1());
        }
    }
}
