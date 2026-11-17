using DBDemoWinFormApp.Data;
using Microsoft.EntityFrameworkCore;

namespace DBDemoWinFormApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            txtConnectionString.Text = QuanLyDbContext.DefaultConnectionString;
        }

        private string GetActiveConnectionString()
        {
            string connStr = txtConnectionString.Text.Trim();
            if (string.IsNullOrEmpty(connStr))
            {
                connStr = QuanLyDbContext.DefaultConnectionString;
                txtConnectionString.Text = connStr;
            }
            return connStr;
        }

        private void btnKetNoi_Click(object? sender, EventArgs e)
        {
            string connStr = GetActiveConnectionString();

            try
            {
                using var db = new QuanLyDbContext(connStr);
                db.Database.OpenConnection();
                db.Database.CloseConnection();

                lblTrangThai.Text = "Trạng thái: Đã kết nối thành công CSDL QUANLY";
                lblTrangThai.ForeColor = Color.DarkGreen;

                MessageBox.Show(
                    "Đã kết nối thành công CSDL QUANLY!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                lblTrangThai.Text = "Trạng thái: Kết nối thất bại";
                lblTrangThai.ForeColor = Color.Firebrick;

                MessageBox.Show(
                    "Lỗi kết nối CSDL: " + ex.Message,
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnSangForm2_Click(object? sender, EventArgs e)
        {
            var form2 = new Form2(this, GetActiveConnectionString());
            form2.Show();
            this.Hide();
        }

        private void btnThoat_Click(object? sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
