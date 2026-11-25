namespace DBDemoWinFormApp
{
    partial class FormBieuDo
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
            btnLamMoi = new Button();
            btnDong = new Button();
            pnlBieuDoCot = new Panel();
            pnlBieuDoXepLoai = new Panel();
            lblBangThongKe = new Label();
            dgvThongKe = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvThongKe).BeginInit();
            SuspendLayout();
            // 
            // lblTieuDe
            // 
            lblTieuDe.AutoSize = true;
            lblTieuDe.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblTieuDe.ForeColor = Color.FromArgb(30, 64, 175);
            lblTieuDe.Location = new Point(20, 16);
            lblTieuDe.Name = "lblTieuDe";
            lblTieuDe.Size = new Size(540, 25);
            lblTieuDe.TabIndex = 0;
            lblTieuDe.Text = "DASHBOARD THỐNG KÊ & BIỂU ĐỒ (STORED PROCEDURE + VIEW)";
            // 
            // btnLamMoi
            // 
            btnLamMoi.Font = new Font("Segoe UI", 9.5F);
            btnLamMoi.Location = new Point(625, 13);
            btnLamMoi.Name = "btnLamMoi";
            btnLamMoi.Size = new Size(130, 32);
            btnLamMoi.TabIndex = 1;
            btnLamMoi.Text = "Làm mới dữ liệu";
            btnLamMoi.UseVisualStyleBackColor = true;
            btnLamMoi.Click += btnLamMoi_Click;
            // 
            // btnDong
            // 
            btnDong.Font = new Font("Segoe UI", 9.5F);
            btnDong.Location = new Point(765, 13);
            btnDong.Name = "btnDong";
            btnDong.Size = new Size(95, 32);
            btnDong.TabIndex = 2;
            btnDong.Text = "Đóng";
            btnDong.UseVisualStyleBackColor = true;
            btnDong.Click += btnDong_Click;
            // 
            // pnlBieuDoCot
            // 
            pnlBieuDoCot.BackColor = Color.White;
            pnlBieuDoCot.BorderStyle = BorderStyle.FixedSingle;
            pnlBieuDoCot.Location = new Point(20, 56);
            pnlBieuDoCot.Name = "pnlBieuDoCot";
            pnlBieuDoCot.Size = new Size(530, 275);
            pnlBieuDoCot.TabIndex = 3;
            pnlBieuDoCot.Paint += pnlBieuDoCot_Paint;
            // 
            // pnlBieuDoXepLoai
            // 
            pnlBieuDoXepLoai.BackColor = Color.White;
            pnlBieuDoXepLoai.BorderStyle = BorderStyle.FixedSingle;
            pnlBieuDoXepLoai.Location = new Point(565, 56);
            pnlBieuDoXepLoai.Name = "pnlBieuDoXepLoai";
            pnlBieuDoXepLoai.Size = new Size(295, 275);
            pnlBieuDoXepLoai.TabIndex = 4;
            pnlBieuDoXepLoai.Paint += pnlBieuDoXepLoai_Paint;
            // 
            // lblBangThongKe
            // 
            lblBangThongKe.AutoSize = true;
            lblBangThongKe.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblBangThongKe.Location = new Point(20, 342);
            lblBangThongKe.Name = "lblBangThongKe";
            lblBangThongKe.Size = new Size(462, 17);
            lblBangThongKe.TabIndex = 5;
            lblBangThongKe.Text = "Bảng số liệu trả về trực tiếp từ Stored Procedure dbo.sp_ThongKeTheoDiaChi:";
            // 
            // dgvThongKe
            // 
            dgvThongKe.AllowUserToAddRows = false;
            dgvThongKe.AllowUserToDeleteRows = false;
            dgvThongKe.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvThongKe.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvThongKe.Location = new Point(20, 366);
            dgvThongKe.MultiSelect = false;
            dgvThongKe.Name = "dgvThongKe";
            dgvThongKe.ReadOnly = true;
            dgvThongKe.RowHeadersWidth = 48;
            dgvThongKe.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvThongKe.Size = new Size(840, 174);
            dgvThongKe.TabIndex = 6;
            // 
            // FormBieuDo
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(880, 560);
            Controls.Add(dgvThongKe);
            Controls.Add(lblBangThongKe);
            Controls.Add(pnlBieuDoXepLoai);
            Controls.Add(pnlBieuDoCot);
            Controls.Add(btnDong);
            Controls.Add(btnLamMoi);
            Controls.Add(lblTieuDe);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "FormBieuDo";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Dashboard Thống kê & Biểu đồ (QUANLY)";
            Load += FormBieuDo_Load;
            ((System.ComponentModel.ISupportInitialize)dgvThongKe).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTieuDe;
        private Button btnLamMoi;
        private Button btnDong;
        private Panel pnlBieuDoCot;
        private Panel pnlBieuDoXepLoai;
        private Label lblBangThongKe;
        private DataGridView dgvThongKe;
    }
}
