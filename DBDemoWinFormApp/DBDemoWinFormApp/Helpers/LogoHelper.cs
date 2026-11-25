using System.Drawing.Drawing2D;

namespace DBDemoWinFormApp.Helpers
{
    /// <summary>
    /// Helper xử lý chèn Logo mặc định, ẩn/hiện Logo theo CheckBox và cho phép nhấp vào Logo để đổi ảnh từ máy tính.
    /// </summary>
    internal static class LogoHelper
    {
        public static void KhoiTaoLogo(PictureBox picLogo, CheckBox chkHienLogo)
        {
            picLogo.SizeMode = PictureBoxSizeMode.Zoom;
            picLogo.Cursor = Cursors.Hand;
            picLogo.Image = TaoLogoMacDinh();
            picLogo.Visible = chkHienLogo.Checked;

            var tooltip = new ToolTip();
            tooltip.SetToolTip(picLogo, "Nhấp vào Logo để chọn ảnh khác từ máy tính");

            chkHienLogo.CheckedChanged += (_, _) =>
            {
                picLogo.Visible = chkHienLogo.Checked;
            };

            picLogo.Click += (_, _) =>
            {
                using var dialog = new OpenFileDialog
                {
                    Title = "Chọn ảnh Logo",
                    Filter = "Ảnh (*.png;*.jpg;*.jpeg;*.bmp)|*.png;*.jpg;*.jpeg;*.bmp"
                };

                if (dialog.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                try
                {
                    using var stream = File.OpenRead(dialog.FileName);
                    using var loaded = Image.FromStream(stream);
                    var oldImage = picLogo.Image;
                    picLogo.Image = new Bitmap(loaded);
                    oldImage?.Dispose();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không thể đọc ảnh Logo:\n" + ex.Message, "Lỗi ảnh", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };
        }

        private static Bitmap TaoLogoMacDinh()
        {
            var bmp = new Bitmap(64, 64);
            using Graphics g = Graphics.FromImage(bmp);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(Color.Transparent);

            using var bgBrush = new LinearGradientBrush(
                new Rectangle(2, 2, 60, 60),
                Color.FromArgb(30, 64, 175),
                Color.FromArgb(37, 99, 235),
                45f);
            using var borderPen = new Pen(Color.White, 2f);
            using var path = TaoBoGoc(new Rectangle(2, 2, 60, 60), 12);
            g.FillPath(bgBrush, path);
            g.DrawPath(borderPen, path);

            using var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
            using var fontTop = new Font("Segoe UI", 11F, FontStyle.Bold);
            using var fontBottom = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            g.DrawString("CSDL", fontTop, Brushes.White, new RectangleF(2, 10, 60, 26), sf);
            g.DrawString("SQL", fontBottom, Brushes.LightGoldenrodYellow, new RectangleF(2, 34, 60, 20), sf);

            return bmp;
        }

        private static GraphicsPath TaoBoGoc(Rectangle rect, int radius)
        {
            int d = radius * 2;
            var path = new GraphicsPath();
            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }
    }
}
