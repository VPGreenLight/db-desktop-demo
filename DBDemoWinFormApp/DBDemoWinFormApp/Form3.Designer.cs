namespace DBDemoWinFormApp
{
    partial class Form3
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
            lblNhapHoTen = new Label();
            txtTimHoTen = new TextBox();
            btnTimKiem = new Button();
            lblKetQua = new Label();
            dgvKetQua = new DataGridView();
            lblHoTen = new Label();
            txtHoTen = new TextBox();
            lblToan = new Label();
            txtToan = new TextBox();
            lblVan = new Label();
            txtVan = new TextBox();
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
            lblTieuDe.Size = new Size(198, 25);
            lblTieuDe.TabIndex = 0;
            lblTieuDe.Text = "QUẢN LÝ BẢNG DIEM";
            // 
            // lblNhapHoTen
            // 
            lblNhapHoTen.AutoSize = true;
            lblNhapHoTen.Font = new Font("Segoe UI", 10F);
            lblNhapHoTen.Location = new Point(28, 60);
            lblNhapHoTen.Name = "lblNhapHoTen";
            lblNhapHoTen.Size = new Size(88, 19);
            lblNhapHoTen.TabIndex = 1;
            lblNhapHoTen.Text = "Nhập họ tên:";
            // 
            // txtTimHoTen
            // 
            txtTimHoTen.Font = new Font("Segoe UI", 10F);
            txtTimHoTen.Location = new Point(135, 57);
            txtTimHoTen.Name = "txtTimHoTen";
            txtTimHoTen.PlaceholderText = "Ví dụ: An (để trống xem tất cả)";
            txtTimHoTen.Size = new Size(280, 25);
            txtTimHoTen.TabIndex = 2;
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
            // lblHoTen
            // 
            lblHoTen.AutoSize = true;
            lblHoTen.Font = new Font("Segoe UI", 9.5F);
            lblHoTen.Location = new Point(28, 350);
            lblHoTen.Name = "lblHoTen";
            lblHoTen.Size = new Size(133, 17);
            lblHoTen.TabIndex = 6;
            lblHoTen.Text = "Họ tên (Khóa chính):";
            // 
            // txtHoTen
            // 
            txtHoTen.Font = new Font("Segoe UI", 10F);
            txtHoTen.Location = new Point(28, 374);
            txtHoTen.Name = "txtHoTen";
            txtHoTen.Size = new Size(260, 25);
            txtHoTen.TabIndex = 7;
            // 
            // lblToan
            // 
            lblToan.AutoSize = true;
            lblToan.Font = new Font("Segoe UI", 9.5F);
            lblToan.Location = new Point(308, 350);
            lblToan.Name = "lblToan";
            lblToan.Size = new Size(116, 17);
            lblToan.TabIndex = 8;
            lblToan.Text = "Điểm Toán (0-10):";
            // 
            // txtToan
            // 
            txtToan.Font = new Font("Segoe UI", 10F);
            txtToan.Location = new Point(308, 374);
            txtToan.Name = "txtToan";
            txtToan.Size = new Size(142, 25);
            txtToan.TabIndex = 9;
            // 
            // lblVan
            // 
            lblVan.AutoSize = true;
            lblVan.Font = new Font("Segoe UI", 9.5F);
            lblVan.Location = new Point(470, 350);
            lblVan.Name = "lblVan";
            lblVan.Size = new Size(109, 17);
            lblVan.TabIndex = 10;
            lblVan.Text = "Điểm Văn (0-10):";
            // 
            // txtVan
            // 
            txtVan.Font = new Font("Segoe UI", 10F);
            txtVan.Location = new Point(470, 374);
            txtVan.Name = "txtVan";
            txtVan.Size = new Size(142, 25);
            txtVan.TabIndex = 11;
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
            // Form3
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(640, 500);
            Controls.Add(btnVeForm1);
            Controls.Add(btnXoa);
            Controls.Add(btnSua);
            Controls.Add(btnThem);
            Controls.Add(txtVan);
            Controls.Add(lblVan);
            Controls.Add(txtToan);
            Controls.Add(lblToan);
            Controls.Add(txtHoTen);
            Controls.Add(lblHoTen);
            Controls.Add(dgvKetQua);
            Controls.Add(lblKetQua);
            Controls.Add(btnTimKiem);
            Controls.Add(txtTimHoTen);
            Controls.Add(lblNhapHoTen);
            Controls.Add(lblTieuDe);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Form3";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form3 - Quản lý bảng DIEM";
            Load += Form3_Load;
            ((System.ComponentModel.ISupportInitialize)dgvKetQua).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTieuDe;
        private Label lblNhapHoTen;
        private TextBox txtTimHoTen;
        private Button btnTimKiem;
        private Label lblKetQua;
        private DataGridView dgvKetQua;
        private Label lblHoTen;
        private TextBox txtHoTen;
        private Label lblToan;
        private TextBox txtToan;
        private Label lblVan;
        private TextBox txtVan;
        private Button btnThem;
        private Button btnSua;
        private Button btnXoa;
        private Button btnVeForm1;
    }
}
