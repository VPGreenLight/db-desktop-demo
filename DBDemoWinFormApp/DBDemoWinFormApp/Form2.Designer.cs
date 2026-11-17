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
            lblThongBao = new Label();
            grpTimKiem = new GroupBox();
            lblChonBang = new Label();
            rdoNguoi = new RadioButton();
            rdoDiem = new RadioButton();
            chkTimChinhXac = new CheckBox();
            lblTimKiem = new Label();
            txtTimKiem = new TextBox();
            btnTimKiem = new Button();
            btnTaiLai = new Button();
            dgvKetQua = new DataGridView();
            grpNhapLieu = new GroupBox();
            lblCot1 = new Label();
            txtCot1 = new TextBox();
            lblCot2 = new Label();
            txtCot2 = new TextBox();
            lblCot3 = new Label();
            txtCot3 = new TextBox();
            btnThem = new Button();
            btnCapNhat = new Button();
            btnXoa = new Button();
            btnLamMoi = new Button();
            lblKetQuaTrangThai = new Label();
            btnVeForm1 = new Button();
            grpTimKiem.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvKetQua).BeginInit();
            grpNhapLieu.SuspendLayout();
            SuspendLayout();
            // 
            // lblThongBao
            // 
            lblThongBao.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblThongBao.ForeColor = Color.FromArgb(30, 64, 175);
            lblThongBao.Location = new Point(24, 16);
            lblThongBao.Name = "lblThongBao";
            lblThongBao.Size = new Size(832, 32);
            lblThongBao.TabIndex = 0;
            lblThongBao.Text = "ĐÂY LÀ FORM2 - KẾT QUẢ TÌM KIẾM & QUẢN LÝ CSDL QUANLY";
            lblThongBao.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // grpTimKiem
            // 
            grpTimKiem.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            grpTimKiem.Controls.Add(lblChonBang);
            grpTimKiem.Controls.Add(rdoNguoi);
            grpTimKiem.Controls.Add(rdoDiem);
            grpTimKiem.Controls.Add(chkTimChinhXac);
            grpTimKiem.Controls.Add(lblTimKiem);
            grpTimKiem.Controls.Add(txtTimKiem);
            grpTimKiem.Controls.Add(btnTimKiem);
            grpTimKiem.Controls.Add(btnTaiLai);
            grpTimKiem.Font = new Font("Segoe UI", 9.5F);
            grpTimKiem.Location = new Point(24, 56);
            grpTimKiem.Name = "grpTimKiem";
            grpTimKiem.Size = new Size(832, 94);
            grpTimKiem.TabIndex = 1;
            grpTimKiem.TabStop = false;
            grpTimKiem.Text = "Lọc & Tìm kiếm (Parameter Query)";
            // 
            // lblChonBang
            // 
            lblChonBang.AutoSize = true;
            lblChonBang.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblChonBang.Location = new Point(16, 26);
            lblChonBang.Name = "lblChonBang";
            lblChonBang.Size = new Size(88, 21);
            lblChonBang.TabIndex = 0;
            lblChonBang.Text = "Chọn bảng:";
            // 
            // rdoNguoi
            // 
            rdoNguoi.AutoSize = true;
            rdoNguoi.Checked = true;
            rdoNguoi.Location = new Point(112, 24);
            rdoNguoi.Name = "rdoNguoi";
            rdoNguoi.Size = new Size(182, 25);
            rdoNguoi.TabIndex = 1;
            rdoNguoi.TabStop = true;
            rdoNguoi.Text = "NGUOI (ten, tuoi, dchi)";
            rdoNguoi.UseVisualStyleBackColor = true;
            rdoNguoi.CheckedChanged += Bang_CheckedChanged;
            // 
            // rdoDiem
            // 
            rdoDiem.AutoSize = true;
            rdoDiem.Location = new Point(312, 24);
            rdoDiem.Name = "rdoDiem";
            rdoDiem.Size = new Size(181, 25);
            rdoDiem.TabIndex = 2;
            rdoDiem.Text = "DIEM (hoten, toan, van)";
            rdoDiem.UseVisualStyleBackColor = true;
            rdoDiem.CheckedChanged += Bang_CheckedChanged;
            // 
            // chkTimChinhXac
            // 
            chkTimChinhXac.AutoSize = true;
            chkTimChinhXac.Location = new Point(512, 25);
            chkTimChinhXac.Name = "chkTimChinhXac";
            chkTimChinhXac.Size = new Size(161, 25);
            chkTimChinhXac.TabIndex = 3;
            chkTimChinhXac.Text = "Tìm chính xác (=)";
            chkTimChinhXac.UseVisualStyleBackColor = true;
            // 
            // lblTimKiem
            // 
            lblTimKiem.AutoSize = true;
            lblTimKiem.Location = new Point(16, 59);
            lblTimKiem.Name = "lblTimKiem";
            lblTimKiem.Size = new Size(121, 21);
            lblTimKiem.TabIndex = 4;
            lblTimKiem.Text = "Cho địa chỉ / tên:";
            // 
            // txtTimKiem
            // 
            txtTimKiem.Location = new Point(150, 56);
            txtTimKiem.Name = "txtTimKiem";
            txtTimKiem.PlaceholderText = "Ví dụ: HANOI";
            txtTimKiem.Size = new Size(343, 27);
            txtTimKiem.TabIndex = 5;
            // 
            // btnTimKiem
            // 
            btnTimKiem.BackColor = Color.FromArgb(37, 99, 235);
            btnTimKiem.FlatStyle = FlatStyle.Flat;
            btnTimKiem.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnTimKiem.ForeColor = Color.White;
            btnTimKiem.Location = new Point(512, 54);
            btnTimKiem.Name = "btnTimKiem";
            btnTimKiem.Size = new Size(130, 31);
            btnTimKiem.TabIndex = 6;
            btnTimKiem.Text = "Tìm kiếm";
            btnTimKiem.UseVisualStyleBackColor = false;
            btnTimKiem.Click += btnTimKiem_Click;
            // 
            // btnTaiLai
            // 
            btnTaiLai.Location = new Point(652, 54);
            btnTaiLai.Name = "btnTaiLai";
            btnTaiLai.Size = new Size(160, 31);
            btnTaiLai.TabIndex = 7;
            btnTaiLai.Text = "Tải lại tất cả";
            btnTaiLai.UseVisualStyleBackColor = true;
            btnTaiLai.Click += btnTaiLai_Click;
            // 
            // dgvKetQua
            // 
            dgvKetQua.AllowUserToAddRows = false;
            dgvKetQua.AllowUserToDeleteRows = false;
            dgvKetQua.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvKetQua.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvKetQua.BackgroundColor = Color.White;
            dgvKetQua.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvKetQua.Location = new Point(24, 162);
            dgvKetQua.MultiSelect = false;
            dgvKetQua.Name = "dgvKetQua";
            dgvKetQua.ReadOnly = true;
            dgvKetQua.RowHeadersWidth = 48;
            dgvKetQua.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvKetQua.Size = new Size(832, 218);
            dgvKetQua.TabIndex = 2;
            dgvKetQua.CellClick += dgvKetQua_CellClick;
            // 
            // grpNhapLieu
            // 
            grpNhapLieu.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            grpNhapLieu.Controls.Add(lblCot1);
            grpNhapLieu.Controls.Add(txtCot1);
            grpNhapLieu.Controls.Add(lblCot2);
            grpNhapLieu.Controls.Add(txtCot2);
            grpNhapLieu.Controls.Add(lblCot3);
            grpNhapLieu.Controls.Add(txtCot3);
            grpNhapLieu.Font = new Font("Segoe UI", 9.5F);
            grpNhapLieu.Location = new Point(24, 392);
            grpNhapLieu.Name = "grpNhapLieu";
            grpNhapLieu.Size = new Size(832, 102);
            grpNhapLieu.TabIndex = 3;
            grpNhapLieu.TabStop = false;
            grpNhapLieu.Text = "Nhập liệu (NHAP - Thêm / Cập nhật / Xóa)";
            // 
            // lblCot1
            // 
            lblCot1.AutoSize = true;
            lblCot1.Location = new Point(16, 28);
            lblCot1.Name = "lblCot1";
            lblCot1.Size = new Size(92, 21);
            lblCot1.TabIndex = 0;
            lblCot1.Text = "Họ tên (ten):";
            // 
            // txtCot1
            // 
            txtCot1.Location = new Point(16, 54);
            txtCot1.Name = "txtCot1";
            txtCot1.Size = new Size(230, 27);
            txtCot1.TabIndex = 1;
            // 
            // lblCot2
            // 
            lblCot2.AutoSize = true;
            lblCot2.Location = new Point(266, 28);
            lblCot2.Name = "lblCot2";
            lblCot2.Size = new Size(76, 21);
            lblCot2.TabIndex = 2;
            lblCot2.Text = "Tuổi (tuoi):";
            // 
            // txtCot2
            // 
            txtCot2.Location = new Point(266, 54);
            txtCot2.Name = "txtCot2";
            txtCot2.Size = new Size(150, 27);
            txtCot2.TabIndex = 3;
            // 
            // lblCot3
            // 
            lblCot3.AutoSize = true;
            lblCot3.Location = new Point(436, 28);
            lblCot3.Name = "lblCot3";
            lblCot3.Size = new Size(99, 21);
            lblCot3.TabIndex = 4;
            lblCot3.Text = "Địa chỉ (dchi):";
            // 
            // txtCot3
            // 
            txtCot3.Location = new Point(436, 54);
            txtCot3.Name = "txtCot3";
            txtCot3.Size = new Size(376, 27);
            txtCot3.TabIndex = 5;
            // 
            // btnThem
            // 
            btnThem.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnThem.BackColor = Color.FromArgb(22, 163, 74);
            btnThem.FlatStyle = FlatStyle.Flat;
            btnThem.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnThem.ForeColor = Color.White;
            btnThem.Location = new Point(24, 508);
            btnThem.Name = "btnThem";
            btnThem.Size = new Size(105, 36);
            btnThem.TabIndex = 4;
            btnThem.Text = "Thêm";
            btnThem.UseVisualStyleBackColor = false;
            btnThem.Click += btnThem_Click;
            // 
            // btnCapNhat
            // 
            btnCapNhat.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnCapNhat.BackColor = Color.FromArgb(217, 119, 6);
            btnCapNhat.FlatStyle = FlatStyle.Flat;
            btnCapNhat.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnCapNhat.ForeColor = Color.White;
            btnCapNhat.Location = new Point(139, 508);
            btnCapNhat.Name = "btnCapNhat";
            btnCapNhat.Size = new Size(105, 36);
            btnCapNhat.TabIndex = 5;
            btnCapNhat.Text = "Cập nhật";
            btnCapNhat.UseVisualStyleBackColor = false;
            btnCapNhat.Click += btnCapNhat_Click;
            // 
            // btnXoa
            // 
            btnXoa.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnXoa.BackColor = Color.FromArgb(220, 38, 38);
            btnXoa.FlatStyle = FlatStyle.Flat;
            btnXoa.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnXoa.ForeColor = Color.White;
            btnXoa.Location = new Point(254, 508);
            btnXoa.Name = "btnXoa";
            btnXoa.Size = new Size(105, 36);
            btnXoa.TabIndex = 6;
            btnXoa.Text = "Xóa";
            btnXoa.UseVisualStyleBackColor = false;
            btnXoa.Click += btnXoa_Click;
            // 
            // btnLamMoi
            // 
            btnLamMoi.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnLamMoi.Font = new Font("Segoe UI", 9.5F);
            btnLamMoi.Location = new Point(369, 508);
            btnLamMoi.Name = "btnLamMoi";
            btnLamMoi.Size = new Size(115, 36);
            btnLamMoi.TabIndex = 7;
            btnLamMoi.Text = "Làm mới ô";
            btnLamMoi.UseVisualStyleBackColor = true;
            btnLamMoi.Click += btnLamMoi_Click;
            // 
            // lblKetQuaTrangThai
            // 
            lblKetQuaTrangThai.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblKetQuaTrangThai.Font = new Font("Segoe UI", 9.5F, FontStyle.Italic);
            lblKetQuaTrangThai.ForeColor = Color.DimGray;
            lblKetQuaTrangThai.Location = new Point(495, 514);
            lblKetQuaTrangThai.Name = "lblKetQuaTrangThai";
            lblKetQuaTrangThai.Size = new Size(200, 24);
            lblKetQuaTrangThai.TabIndex = 8;
            lblKetQuaTrangThai.Text = "Tổng: 0 bản ghi";
            // 
            // btnVeForm1
            // 
            btnVeForm1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnVeForm1.BackColor = Color.FromArgb(51, 65, 85);
            btnVeForm1.FlatStyle = FlatStyle.Flat;
            btnVeForm1.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnVeForm1.ForeColor = Color.White;
            btnVeForm1.Location = new Point(706, 508);
            btnVeForm1.Name = "btnVeForm1";
            btnVeForm1.Size = new Size(150, 36);
            btnVeForm1.TabIndex = 9;
            btnVeForm1.Text = "QUAY VỀ FORM1";
            btnVeForm1.UseVisualStyleBackColor = false;
            btnVeForm1.Click += btnVeForm1_Click;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(880, 560);
            Controls.Add(btnVeForm1);
            Controls.Add(lblKetQuaTrangThai);
            Controls.Add(btnLamMoi);
            Controls.Add(btnXoa);
            Controls.Add(btnCapNhat);
            Controls.Add(btnThem);
            Controls.Add(grpNhapLieu);
            Controls.Add(dgvKetQua);
            Controls.Add(grpTimKiem);
            Controls.Add(lblThongBao);
            MinimumSize = new Size(896, 599);
            Name = "Form2";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form2 - Quản lý CSDL QUANLY";
            Load += Form2_Load;
            grpTimKiem.ResumeLayout(false);
            grpTimKiem.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvKetQua).EndInit();
            grpNhapLieu.ResumeLayout(false);
            grpNhapLieu.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Label lblThongBao;
        private GroupBox grpTimKiem;
        private Label lblChonBang;
        private RadioButton rdoNguoi;
        private RadioButton rdoDiem;
        private CheckBox chkTimChinhXac;
        private Label lblTimKiem;
        private TextBox txtTimKiem;
        private Button btnTimKiem;
        private Button btnTaiLai;
        private DataGridView dgvKetQua;
        private GroupBox grpNhapLieu;
        private Label lblCot1;
        private TextBox txtCot1;
        private Label lblCot2;
        private TextBox txtCot2;
        private Label lblCot3;
        private TextBox txtCot3;
        private Button btnThem;
        private Button btnCapNhat;
        private Button btnXoa;
        private Button btnLamMoi;
        private Label lblKetQuaTrangThai;
        private Button btnVeForm1;
    }
}
