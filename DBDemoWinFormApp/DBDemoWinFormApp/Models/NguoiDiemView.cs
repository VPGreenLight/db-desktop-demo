using System.ComponentModel;

namespace DBDemoWinFormApp.Models
{
    /// <summary>
    /// Lớp ViewModel (DTO) dùng để chứa dữ liệu gộp từ 2 bảng NGUOI và DIEM khi hiển thị trên Form4.
    /// Lưu ý: Class này KHÔNG gắn [Table(...)] vì nó không phải 1 bảng vật lý trong SQL Server,
    /// mà là kết quả ghép cột (JOIN) theo họ tên (NGUOI.ten = DIEM.hoten).
    /// </summary>
    public class NguoiDiemView
    {
        [DisplayName("Họ tên")]
        public string Ten { get; set; } = string.Empty;

        [DisplayName("Tuổi")]
        public int Tuoi { get; set; }

        [DisplayName("Địa chỉ")]
        public string Dchi { get; set; } = string.Empty;

        // Dùng kiểu nullable (double?) để nếu người này mới chỉ có ở bảng NGUOI mà chưa có điểm bên DIEM,
        // ô điểm trên DataGridView sẽ để trống thay vì gây lỗi.
        [DisplayName("Điểm Toán")]
        public double? Toan { get; set; }

        [DisplayName("Điểm Văn")]
        public double? Van { get; set; }
    }
}
