namespace DBDemoWinFormApp
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblTieuDe = new Label();
            lblServer = new Label();
            txtConnectionString = new TextBox();
            lblTrangThai = new Label();
            btnKetNoi = new Button();
            btnSangForm2 = new Button();
            btnThoat = new Button();
            lblGhiChu = new Label();
            SuspendLayout();
            // 
            // lblTieuDe
            // 
            lblTieuDe.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTieuDe.ForeColor = Color.FromArgb(30, 64, 175);
            lblTieuDe.Location = new Point(30, 20);
            lblTieuDe.Name = "lblTieuDe";
            lblTieuDe.Size = new Size(500, 36);
            lblTieuDe.TabIndex = 0;
            lblTieuDe.Text = "TRANG CHÍNH - QUẢN LÝ CSDL";
            lblTieuDe.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblServer
            // 
            lblServer.AutoSize = true;
            lblServer.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblServer.Location = new Point(35, 72);
            lblServer.Name = "lblServer";
            lblServer.Size = new Size(197, 21);
            lblServer.TabIndex = 1;
            lblServer.Text = "Chuỗi kết nối SQL Server:";
            // 
            // txtConnectionString
            // 
            txtConnectionString.Font = new Font("Consolas", 9.5F);
            txtConnectionString.Location = new Point(35, 98);
            txtConnectionString.Name = "txtConnectionString";
            txtConnectionString.Size = new Size(490, 26);
            txtConnectionString.TabIndex = 2;
            // 
            // lblTrangThai
            // 
            lblTrangThai.Font = new Font("Segoe UI", 9.5F, FontStyle.Italic);
            lblTrangThai.ForeColor = Color.DimGray;
            lblTrangThai.Location = new Point(35, 134);
            lblTrangThai.Name = "lblTrangThai";
            lblTrangThai.Size = new Size(490, 26);
            lblTrangThai.TabIndex = 3;
            lblTrangThai.Text = "Trạng thái: Chưa kiểm tra kết nối";
            // 
            // btnKetNoi
            // 
            btnKetNoi.BackColor = Color.FromArgb(37, 99, 235);
            btnKetNoi.FlatStyle = FlatStyle.Flat;
            btnKetNoi.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnKetNoi.ForeColor = Color.White;
            btnKetNoi.Location = new Point(35, 178);
            btnKetNoi.Name = "btnKetNoi";
            btnKetNoi.Size = new Size(230, 52);
            btnKetNoi.TabIndex = 4;
            btnKetNoi.Text = "KẾT NỐI CSDL";
            btnKetNoi.UseVisualStyleBackColor = false;
            btnKetNoi.Click += btnKetNoi_Click;
            // 
            // btnSangForm2
            // 
            btnSangForm2.BackColor = Color.FromArgb(15, 118, 110);
            btnSangForm2.FlatStyle = FlatStyle.Flat;
            btnSangForm2.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnSangForm2.ForeColor = Color.White;
            btnSangForm2.Location = new Point(295, 178);
            btnSangForm2.Name = "btnSangForm2";
            btnSangForm2.Size = new Size(230, 52);
            btnSangForm2.TabIndex = 5;
            btnSangForm2.Text = "SANG FORM2";
            btnSangForm2.UseVisualStyleBackColor = false;
            btnSangForm2.Click += btnSangForm2_Click;
            // 
            // btnThoat
            // 
            btnThoat.BackColor = Color.FromArgb(220, 38, 38);
            btnThoat.FlatStyle = FlatStyle.Flat;
            btnThoat.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnThoat.ForeColor = Color.White;
            btnThoat.Location = new Point(185, 254);
            btnThoat.Name = "btnThoat";
            btnThoat.Size = new Size(190, 46);
            btnThoat.TabIndex = 6;
            btnThoat.Text = "THOÁT";
            btnThoat.UseVisualStyleBackColor = false;
            btnThoat.Click += btnThoat_Click;
            // 
            // lblGhiChu
            // 
            lblGhiChu.Font = new Font("Segoe UI", 8.5F);
            lblGhiChu.ForeColor = Color.Gray;
            lblGhiChu.Location = new Point(35, 320);
            lblGhiChu.Name = "lblGhiChu";
            lblGhiChu.Size = new Size(490, 36);
            lblGhiChu.TabIndex = 7;
            lblGhiChu.Text = "Gợi ý: Chạy script database/init_quanly.sql trong SQL Server trước khi kết nối.";
            lblGhiChu.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(560, 375);
            Controls.Add(lblGhiChu);
            Controls.Add(btnThoat);
            Controls.Add(btnSangForm2);
            Controls.Add(btnKetNoi);
            Controls.Add(lblTrangThai);
            Controls.Add(txtConnectionString);
            Controls.Add(lblServer);
            Controls.Add(lblTieuDe);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1 - Trang chính (QUANLY)";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTieuDe;
        private Label lblServer;
        private TextBox txtConnectionString;
        private Label lblTrangThai;
        private Button btnKetNoi;
        private Button btnSangForm2;
        private Button btnThoat;
        private Label lblGhiChu;
    }
}
