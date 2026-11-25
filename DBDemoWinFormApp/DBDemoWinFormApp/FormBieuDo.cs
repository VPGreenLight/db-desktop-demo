using System.Drawing.Drawing2D;
using DBDemoWinFormApp.Data;
using DBDemoWinFormApp.Models;
using Microsoft.EntityFrameworkCore;

namespace DBDemoWinFormApp
{
    public partial class FormBieuDo : Form
    {
        private List<ThongKeDiaChiView> _thongKeDiaChi = [];
        private readonly Dictionary<string, int> _thongKeXepLoai = new()
        {
            ["Giỏi"] = 0,
            ["Khá"] = 0,
            ["Trung bình"] = 0,
            ["Yếu"] = 0
        };

        public FormBieuDo()
        {
            InitializeComponent();
        }

        private void FormBieuDo_Load(object? sender, EventArgs e)
        {
            TaiDuLieuThongKe();
        }

        private void btnLamMoi_Click(object? sender, EventArgs e)
        {
            TaiDuLieuThongKe();
        }

        /// <summary>
        /// Gọi Stored Procedure dbo.sp_ThongKeTheoDiaChi và đọc SQL View dbo.vw_NguoiDiem
        /// để lấy số liệu vẽ 2 biểu đồ và nạp bảng thống kê bên dưới.
        /// </summary>
        private void TaiDuLieuThongKe()
        {
            try
            {
                using var db = new QuanLyDbContext();

                // 1. Gọi Stored Procedure trong SQL Server bằng EF Core SqlQueryRaw
                _thongKeDiaChi = db.Database
                    .SqlQueryRaw<ThongKeDiaChiView>("EXEC dbo.sp_ThongKeTheoDiaChi")
                    .ToList();

                // 2. Đọc từ SQL View dbo.vw_NguoiDiem để thống kê phân bố Xếp loại học lực
                List<NguoiDiemView> tatCa = db.NguoiDiemViews.AsNoTracking().ToList();
                foreach (string key in _thongKeXepLoai.Keys.ToList())
                {
                    _thongKeXepLoai[key] = 0;
                }

                foreach (NguoiDiemView item in tatCa)
                {
                    if (_thongKeXepLoai.ContainsKey(item.XepLoai))
                    {
                        _thongKeXepLoai[item.XepLoai]++;
                    }
                }

                dgvThongKe.DataSource = _thongKeDiaChi;
                pnlBieuDoCot.Invalidate();
                pnlBieuDoXepLoai.Invalidate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không thể tải dữ liệu thống kê từ SQL Server:\n" + ex.Message,
                    "Lỗi CSDL",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Vẽ biểu đồ cột bằng GDI+: Số người (cột xanh) và Điểm TB theo từng Tỉnh/Thành.
        /// </summary>
        private void pnlBieuDoCot_Paint(object? sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            using var titleFont = new Font("Segoe UI", 10F, FontStyle.Bold);
            using var labelFont = new Font("Segoe UI", 8F);
            using var valueFont = new Font("Segoe UI", 7.5F, FontStyle.Bold);

            g.DrawString("Biểu đồ số người & Điểm TB theo Tỉnh/Thành", titleFont, Brushes.Black, 12, 10);

            if (_thongKeDiaChi.Count == 0)
            {
                g.DrawString("Chưa có dữ liệu.", labelFont, Brushes.Gray, 12, 42);
                return;
            }

            int left = 36;
            int top = 42;
            int right = pnlBieuDoCot.ClientSize.Width - 16;
            int bottom = pnlBieuDoCot.ClientSize.Height - 48;
            int chartWidth = right - left;
            int chartHeight = bottom - top;

            using var axisPen = new Pen(Color.Gray, 1F);
            g.DrawLine(axisPen, left, top, left, bottom);
            g.DrawLine(axisPen, left, bottom, right, bottom);

            int maxCount = Math.Max(1, _thongKeDiaChi.Max(x => x.SoNguoi));
            int count = _thongKeDiaChi.Count;
            float slotWidth = (float)chartWidth / count;
            float barWidth = Math.Min(32F, slotWidth * 0.58F);

            using var barBrush = new SolidBrush(Color.FromArgb(37, 99, 235));
            using var sfCenter = new StringFormat { Alignment = StringAlignment.Center };

            for (int i = 0; i < count; i++)
            {
                ThongKeDiaChiView item = _thongKeDiaChi[i];
                float barHeight = ((float)item.SoNguoi / maxCount) * (chartHeight - 20);
                float x = left + i * slotWidth + (slotWidth - barWidth) / 2F;
                float y = bottom - barHeight;

                g.FillRectangle(barBrush, x, y, barWidth, barHeight);

                float centerX = left + i * slotWidth + slotWidth / 2F;
                g.DrawString($"{item.SoNguoi} ({item.DiemTB:0.0})", valueFont, Brushes.DarkBlue, centerX, y - 16, sfCenter);

                string shortCity = item.DiaChi.Length > 7 ? item.DiaChi[..7] : item.DiaChi;
                g.DrawString(shortCity, labelFont, Brushes.Black, centerX, bottom + 6, sfCenter);
            }
        }

        /// <summary>
        /// Vẽ biểu đồ thanh ngang bằng GDI+: Tỷ lệ phân bố học lực (Giỏi / Khá / Trung bình / Yếu).
        /// </summary>
        private void pnlBieuDoXepLoai_Paint(object? sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            using var titleFont = new Font("Segoe UI", 10F, FontStyle.Bold);
            using var labelFont = new Font("Segoe UI", 9F);

            g.DrawString("Phân bố Xếp loại (vw_NguoiDiem)", titleFont, Brushes.Black, 12, 10);

            int total = Math.Max(1, _thongKeXepLoai.Values.Sum());
            (string Key, Color Color)[] categories =
            [
                ("Giỏi", Color.FromArgb(22, 163, 74)),
                ("Khá", Color.FromArgb(37, 99, 235)),
                ("Trung bình", Color.FromArgb(217, 119, 6)),
                ("Yếu", Color.FromArgb(220, 38, 38))
            ];

            int y = 52;
            int barMaxWidth = pnlBieuDoXepLoai.ClientSize.Width - 32;

            foreach ((string key, Color color) in categories)
            {
                int val = _thongKeXepLoai[key];
                double pct = (double)val * 100.0 / total;
                float barWidth = (float)(val / (double)total) * barMaxWidth;

                g.DrawString($"{key}: {val} ({pct:0.0}%)", labelFont, Brushes.Black, 14, y);
                using var bgBrush = new SolidBrush(Color.FromArgb(235, 238, 245));
                using var fillBrush = new SolidBrush(color);
                g.FillRectangle(bgBrush, 14, y + 20, barMaxWidth, 18);
                if (barWidth > 0)
                {
                    g.FillRectangle(fillBrush, 14, y + 20, barWidth, 18);
                }

                y += 52;
            }
        }

        private void btnDong_Click(object? sender, EventArgs e)
        {
            Close();
        }
    }
}
