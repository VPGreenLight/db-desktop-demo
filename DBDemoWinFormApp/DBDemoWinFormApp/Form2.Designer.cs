namespace DBDemoWinFormApp
{
    partial class Form2
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
            lblNhapDiaChi = new Label();
            txtDiaChi = new TextBox();
            btnTimKiem = new Button();
            lblKetQua = new Label();
            dgvKetQua = new DataGridView();
            lblTen = new Label();
            txtTen = new TextBox();
            lblTuoi = new Label();
            txtTuoi = new TextBox();
            lblDiaChiNhap = new Label();
            txtDiaChiNhap = new TextBox();
            btnThem = new Button();
            btnSua = new Button();
            btnXoa = new Button();
            btnVeForm1 = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvKetQua).BeginInit();
            SuspendLayout();
            // 
            // lblTieuDe
            // 
            lblTieuDe.AutoSize = true;
            lblTieuDe.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTieuDe.Location = new Point(28, 16);
            lblTieuDe.Name = "lblTieuDe";
            lblTieuDe.Size = new Size(211, 25);
            lblTieuDe.TabIndex = 0;
            lblTieuDe.Text = "QUẢN LÝ BẢNG NGUOI";
            // 
            // lblNhapDiaChi
            // 
            lblNhapDiaChi.AutoSize = true;
            lblNhapDiaChi.Font = new Font("Segoe UI", 10F);
            lblNhapDiaChi.Location = new Point(28, 60);
            lblNhapDiaChi.Name = "lblNhapDiaChi";
            lblNhapDiaChi.Size = new Size(95, 19);
            lblNhapDiaChi.TabIndex = 1;
            lblNhapDiaChi.Text = "Nhập địa chỉ:";
            // 
            // txtDiaChi
            // 
            txtDiaChi.Font = new Font("Segoe UI", 10F);
            txtDiaChi.Location = new Point(135, 57);
            txtDiaChi.Name = "txtDiaChi";
            txtDiaChi.PlaceholderText = "Ví dụ: HANOI (để trống xem tất cả)";
            txtDiaChi.Size = new Size(280, 25);
            txtDiaChi.TabIndex = 2;
            // 
            // btnTimKiem
            // 
            btnTimKiem.Font = new Font("Segoe UI", 10F);
            btnTimKiem.Location = new Point(430, 55);
            btnTimKiem.Name = "btnTimKiem";
            btnTimKiem.Size = new Size(110, 31);
            btnTimKiem.TabIndex = 3;
            btnTimKiem.Text = "Tìm kiếm";
            btnTimKiem.UseVisualStyleBackColor = true;
            btnTimKiem.Click += btnTimKiem_Click;
            // 
            // lblKetQua
            // 
            lblKetQua.AutoSize = true;
            lblKetQua.Font = new Font("Segoe UI", 10F);
            lblKetQua.Location = new Point(28, 98);
            lblKetQua.Name = "lblKetQua";
            lblKetQua.Size = new Size(283, 19);
            lblKetQua.TabIndex = 4;
            lblKetQua.Text = "Kết quả (nhấp vào 1 dòng để chọn Sửa / Xóa):";
            // 
            // dgvKetQua
            // 
            dgvKetQua.AllowUserToAddRows = false;
            dgvKetQua.AllowUserToDeleteRows = false;
            dgvKetQua.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvKetQua.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvKetQua.Location = new Point(28, 124);
            dgvKetQua.MultiSelect = false;
            dgvKetQua.Name = "dgvKetQua";
            dgvKetQua.ReadOnly = true;
            dgvKetQua.RowHeadersWidth = 48;
            dgvKetQua.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvKetQua.Size = new Size(584, 210);
            dgvKetQua.TabIndex = 5;
            dgvKetQua.CellClick += dgvKetQua_CellClick;
            // 
            // lblTen
            // 
            lblTen.AutoSize = true;
            lblTen.Font = new Font("Segoe UI", 9.5F);
            lblTen.Location = new Point(28, 350);
            lblTen.Name = "lblTen";
            lblTen.Size = new Size(133, 17);
            lblTen.TabIndex = 6;
            lblTen.Text = "Họ tên (Khóa chính):";
            // 
            // txtTen
            // 
            txtTen.Font = new Font("Segoe UI", 10F);
            txtTen.Location = new Point(28, 374);
            txtTen.Name = "txtTen";
            txtTen.Size = new Size(220, 25);
            txtTen.TabIndex = 7;
            // 
            // lblTuoi
            // 
            lblTuoi.AutoSize = true;
            lblTuoi.Font = new Font("Segoe UI", 9.5F);
            lblTuoi.Location = new Point(268, 350);
            lblTuoi.Name = "lblTuoi";
            lblTuoi.Size = new Size(37, 17);
            lblTuoi.TabIndex = 8;
            lblTuoi.Text = "Tuổi:";
            // 
            // txtTuoi
            // 
            txtTuoi.Font = new Font("Segoe UI", 10F);
            txtTuoi.Location = new Point(268, 374);
            txtTuoi.Name = "txtTuoi";
            txtTuoi.Size = new Size(110, 25);
            txtTuoi.TabIndex = 9;
            // 
            // lblDiaChiNhap
            // 
            lblDiaChiNhap.AutoSize = true;
            lblDiaChiNhap.Font = new Font("Segoe UI", 9.5F);
            lblDiaChiNhap.Location = new Point(398, 350);
            lblDiaChiNhap.Name = "lblDiaChiNhap";
            lblDiaChiNhap.Size = new Size(55, 17);
            lblDiaChiNhap.TabIndex = 10;
            lblDiaChiNhap.Text = "Địa chỉ:";
            // 
            // txtDiaChiNhap
            // 
            txtDiaChiNhap.Font = new Font("Segoe UI", 10F);
            txtDiaChiNhap.Location = new Point(398, 374);
            txtDiaChiNhap.Name = "txtDiaChiNhap";
            txtDiaChiNhap.Size = new Size(214, 25);
            txtDiaChiNhap.TabIndex = 11;
            // 
            // btnThem
            // 
            btnThem.Font = new Font("Segoe UI", 10F);
            btnThem.Location = new Point(28, 424);
            btnThem.Name = "btnThem";
            btnThem.Size = new Size(95, 38);
            btnThem.TabIndex = 12;
            btnThem.Text = "Thêm";
            btnThem.UseVisualStyleBackColor = true;
            btnThem.Click += btnThem_Click;
            // 
            // btnSua
            // 
            btnSua.Font = new Font("Segoe UI", 10F);
            btnSua.Location = new Point(135, 424);
            btnSua.Name = "btnSua";
            btnSua.Size = new Size(95, 38);
            btnSua.TabIndex = 13;
            btnSua.Text = "Sửa";
            btnSua.UseVisualStyleBackColor = true;
            btnSua.Click += btnSua_Click;
            // 
            // btnXoa
            // 
            btnXoa.Font = new Font("Segoe UI", 10F);
            btnXoa.Location = new Point(242, 424);
            btnXoa.Name = "btnXoa";
            btnXoa.Size = new Size(95, 38);
            btnXoa.TabIndex = 14;
            btnXoa.Text = "Xóa";
            btnXoa.UseVisualStyleBackColor = true;
            btnXoa.Click += btnXoa_Click;
            // 
            // btnVeForm1
            // 
            btnVeForm1.Font = new Font("Segoe UI", 10F);
            btnVeForm1.Location = new Point(492, 424);
            btnVeForm1.Name = "btnVeForm1";
            btnVeForm1.Size = new Size(120, 38);
            btnVeForm1.TabIndex = 15;
            btnVeForm1.Text = "Về Form 1";
            btnVeForm1.UseVisualStyleBackColor = true;
            btnVeForm1.Click += btnVeForm1_Click;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(640, 500);
            Controls.Add(btnVeForm1);
            Controls.Add(btnXoa);
            Controls.Add(btnSua);
            Controls.Add(btnThem);
            Controls.Add(txtDiaChiNhap);
            Controls.Add(lblDiaChiNhap);
            Controls.Add(txtTuoi);
            Controls.Add(lblTuoi);
            Controls.Add(txtTen);
            Controls.Add(lblTen);
            Controls.Add(dgvKetQua);
            Controls.Add(lblKetQua);
            Controls.Add(btnTimKiem);
            Controls.Add(txtDiaChi);
            Controls.Add(lblNhapDiaChi);
            Controls.Add(lblTieuDe);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Form2";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form2 - Quản lý bảng NGUOI";
            Load += Form2_Load;
            ((System.ComponentModel.ISupportInitialize)dgvKetQua).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTieuDe;
        private Label lblNhapDiaChi;
        private TextBox txtDiaChi;
        private Button btnTimKiem;
        private Label lblKetQua;
        private DataGridView dgvKetQua;
        private Label lblTen;
        private TextBox txtTen;
        private Label lblTuoi;
        private TextBox txtTuoi;
        private Label lblDiaChiNhap;
        private TextBox txtDiaChiNhap;
        private Button btnThem;
        private Button btnSua;
        private Button btnXoa;
        private Button btnVeForm1;
    }
}
