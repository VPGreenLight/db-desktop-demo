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
            chkHienLogo = new CheckBox();
            picLogo = new PictureBox();
            lblTenLoc = new Label();
            txtTimTen = new TextBox();
            lblDiaChiLoc = new Label();
            cboDiaChi = new ComboBox();
            lblTuoiLoc = new Label();
            txtTuoiTu = new TextBox();
            lblTuoiDen = new Label();
            txtTuoiDen = new TextBox();
            btnTimKiem = new Button();
            btnBoLoc = new Button();
            lblHienCot = new Label();
            chkCotTen = new CheckBox();
            chkCotTuoi = new CheckBox();
            chkCotDiaChi = new CheckBox();
            chkCotNhomTuoi = new CheckBox();
            lblSapXep = new Label();
            cboSapXep = new ComboBox();
            btnSapXep = new Button();
            dgvKetQua = new DataGridView();
            lblThongKe = new Label();
            lblDongTrang = new Label();
            cboKichThuocTrang = new ComboBox();
            btnTrangTruoc = new Button();
            lblTrang = new Label();
            btnTrangSau = new Button();
            lblTen = new Label();
            txtTen = new TextBox();
            lblTuoi = new Label();
            txtTuoi = new TextBox();
            lblDiaChiNhap = new Label();
            txtDiaChiNhap = new TextBox();
            btnThem = new Button();
            btnSua = new Button();
            btnXoa = new Button();
            btnBatch = new Button();
            btnNhapCsv = new Button();
            btnXuatCsv = new Button();
            btnBieuDo = new Button();
            btnVeForm1 = new Button();
            ((System.ComponentModel.ISupportInitialize)picLogo).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvKetQua).BeginInit();
            SuspendLayout();
            // 
            // lblTieuDe
            // 
            lblTieuDe.AutoSize = true;
            lblTieuDe.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTieuDe.Location = new Point(24, 18);
            lblTieuDe.Name = "lblTieuDe";
            lblTieuDe.Size = new Size(211, 25);
            lblTieuDe.TabIndex = 0;
            lblTieuDe.Text = "QUẢN LÝ BẢNG NGUOI";
            // 
            // chkHienLogo
            // 
            chkHienLogo.AutoSize = true;
            chkHienLogo.Checked = true;
            chkHienLogo.CheckState = CheckState.Checked;
            chkHienLogo.Font = new Font("Segoe UI", 9.5F);
            chkHienLogo.Location = new Point(680, 22);
            chkHienLogo.Name = "chkHienLogo";
            chkHienLogo.Size = new Size(88, 21);
            chkHienLogo.TabIndex = 1;
            chkHienLogo.Text = "Hiện Logo";
            chkHienLogo.UseVisualStyleBackColor = true;
            // 
            // picLogo
            // 
            picLogo.Location = new Point(788, 10);
            picLogo.Name = "picLogo";
            picLogo.Size = new Size(48, 48);
            picLogo.TabIndex = 2;
            picLogo.TabStop = false;
            // 
            // lblTenLoc
            // 
            lblTenLoc.AutoSize = true;
            lblTenLoc.Font = new Font("Segoe UI", 9.5F);
            lblTenLoc.Location = new Point(24, 67);
            lblTenLoc.Name = "lblTenLoc";
            lblTenLoc.Size = new Size(34, 17);
            lblTenLoc.TabIndex = 3;
            lblTenLoc.Text = "Tên:";
            // 
            // txtTimTen
            // 
            txtTimTen.Font = new Font("Segoe UI", 9.5F);
            txtTimTen.Location = new Point(64, 64);
            txtTimTen.Name = "txtTimTen";
            txtTimTen.PlaceholderText = "VD: An";
            txtTimTen.Size = new Size(155, 24);
            txtTimTen.TabIndex = 4;
            // 
            // lblDiaChiLoc
            // 
            lblDiaChiLoc.AutoSize = true;
            lblDiaChiLoc.Font = new Font("Segoe UI", 9.5F);
            lblDiaChiLoc.Location = new Point(230, 67);
            lblDiaChiLoc.Name = "lblDiaChiLoc";
            lblDiaChiLoc.Size = new Size(55, 17);
            lblDiaChiLoc.TabIndex = 5;
            lblDiaChiLoc.Text = "Địa chỉ:";
            // 
            // cboDiaChi
            // 
            cboDiaChi.DropDownStyle = ComboBoxStyle.DropDownList;
            cboDiaChi.Font = new Font("Segoe UI", 9.5F);
            cboDiaChi.FormattingEnabled = true;
            cboDiaChi.Location = new Point(288, 64);
            cboDiaChi.Name = "cboDiaChi";
            cboDiaChi.Size = new Size(145, 24);
            cboDiaChi.TabIndex = 6;
            // 
            // lblTuoiLoc
            // 
            lblTuoiLoc.AutoSize = true;
            lblTuoiLoc.Font = new Font("Segoe UI", 9.5F);
            lblTuoiLoc.Location = new Point(444, 67);
            lblTuoiLoc.Name = "lblTuoiLoc";
            lblTuoiLoc.Size = new Size(51, 17);
            lblTuoiLoc.TabIndex = 7;
            lblTuoiLoc.Text = "Tuổi từ:";
            // 
            // txtTuoiTu
            // 
            txtTuoiTu.Font = new Font("Segoe UI", 9.5F);
            txtTuoiTu.Location = new Point(500, 64);
            txtTuoiTu.Name = "txtTuoiTu";
            txtTuoiTu.PlaceholderText = "18";
            txtTuoiTu.Size = new Size(50, 24);
            txtTuoiTu.TabIndex = 8;
            // 
            // lblTuoiDen
            // 
            lblTuoiDen.AutoSize = true;
            lblTuoiDen.Font = new Font("Segoe UI", 9.5F);
            lblTuoiDen.Location = new Point(556, 67);
            lblTuoiDen.Name = "lblTuoiDen";
            lblTuoiDen.Size = new Size(34, 17);
            lblTuoiDen.TabIndex = 9;
            lblTuoiDen.Text = "đến:";
            // 
            // txtTuoiDen
            // 
            txtTuoiDen.Font = new Font("Segoe UI", 9.5F);
            txtTuoiDen.Location = new Point(592, 64);
            txtTuoiDen.Name = "txtTuoiDen";
            txtTuoiDen.PlaceholderText = "30";
            txtTuoiDen.Size = new Size(50, 24);
            txtTuoiDen.TabIndex = 10;
            // 
            // btnTimKiem
            // 
            btnTimKiem.Font = new Font("Segoe UI", 9.5F);
            btnTimKiem.Location = new Point(656, 62);
            btnTimKiem.Name = "btnTimKiem";
            btnTimKiem.Size = new Size(84, 29);
            btnTimKiem.TabIndex = 11;
            btnTimKiem.Text = "Lọc";
            btnTimKiem.UseVisualStyleBackColor = true;
            btnTimKiem.Click += btnTimKiem_Click;
            // 
            // btnBoLoc
            // 
            btnBoLoc.Font = new Font("Segoe UI", 9.5F);
            btnBoLoc.Location = new Point(748, 62);
            btnBoLoc.Name = "btnBoLoc";
            btnBoLoc.Size = new Size(88, 29);
            btnBoLoc.TabIndex = 12;
            btnBoLoc.Text = "Xóa lọc";
            btnBoLoc.UseVisualStyleBackColor = true;
            btnBoLoc.Click += btnBoLoc_Click;
            // 
            // lblHienCot
            // 
            lblHienCot.AutoSize = true;
            lblHienCot.Font = new Font("Segoe UI", 9.5F);
            lblHienCot.Location = new Point(24, 104);
            lblHienCot.Name = "lblHienCot";
            lblHienCot.Size = new Size(33, 17);
            lblHienCot.TabIndex = 13;
            lblHienCot.Text = "Cột:";
            // 
            // chkCotTen
            // 
            chkCotTen.AutoSize = true;
            chkCotTen.Checked = true;
            chkCotTen.CheckState = CheckState.Checked;
            chkCotTen.Font = new Font("Segoe UI", 9.5F);
            chkCotTen.Location = new Point(64, 103);
            chkCotTen.Name = "chkCotTen";
            chkCotTen.Size = new Size(51, 21);
            chkCotTen.TabIndex = 14;
            chkCotTen.Text = "Tên";
            chkCotTen.UseVisualStyleBackColor = true;
            // 
            // chkCotTuoi
            // 
            chkCotTuoi.AutoSize = true;
            chkCotTuoi.Checked = true;
            chkCotTuoi.CheckState = CheckState.Checked;
            chkCotTuoi.Font = new Font("Segoe UI", 9.5F);
            chkCotTuoi.Location = new Point(125, 103);
            chkCotTuoi.Name = "chkCotTuoi";
            chkCotTuoi.Size = new Size(54, 21);
            chkCotTuoi.TabIndex = 15;
            chkCotTuoi.Text = "Tuổi";
            chkCotTuoi.UseVisualStyleBackColor = true;
            // 
            // chkCotDiaChi
            // 
            chkCotDiaChi.AutoSize = true;
            chkCotDiaChi.Checked = true;
            chkCotDiaChi.CheckState = CheckState.Checked;
            chkCotDiaChi.Font = new Font("Segoe UI", 9.5F);
            chkCotDiaChi.Location = new Point(190, 103);
            chkCotDiaChi.Name = "chkCotDiaChi";
            chkCotDiaChi.Size = new Size(71, 21);
            chkCotDiaChi.TabIndex = 16;
            chkCotDiaChi.Text = "Địa chỉ";
            chkCotDiaChi.UseVisualStyleBackColor = true;
            // 
            // chkCotNhomTuoi
            // 
            chkCotNhomTuoi.AutoSize = true;
            chkCotNhomTuoi.Checked = true;
            chkCotNhomTuoi.CheckState = CheckState.Checked;
            chkCotNhomTuoi.Font = new Font("Segoe UI", 9.5F);
            chkCotNhomTuoi.Location = new Point(272, 103);
            chkCotNhomTuoi.Name = "chkCotNhomTuoi";
            chkCotNhomTuoi.Size = new Size(91, 21);
            chkCotNhomTuoi.TabIndex = 17;
            chkCotNhomTuoi.Text = "Nhóm tuổi";
            chkCotNhomTuoi.UseVisualStyleBackColor = true;
            // 
            // lblSapXep
            // 
            lblSapXep.AutoSize = true;
            lblSapXep.Font = new Font("Segoe UI", 9.5F);
            lblSapXep.Location = new Point(532, 104);
            lblSapXep.Name = "lblSapXep";
            lblSapXep.Size = new Size(60, 17);
            lblSapXep.TabIndex = 18;
            lblSapXep.Text = "Sắp xếp:";
            // 
            // cboSapXep
            // 
            cboSapXep.DropDownStyle = ComboBoxStyle.DropDownList;
            cboSapXep.Font = new Font("Segoe UI", 9.5F);
            cboSapXep.FormattingEnabled = true;
            cboSapXep.Location = new Point(596, 101);
            cboSapXep.Name = "cboSapXep";
            cboSapXep.Size = new Size(126, 24);
            cboSapXep.TabIndex = 19;
            cboSapXep.SelectedIndexChanged += cboSapXep_SelectedIndexChanged;
            // 
            // btnSapXep
            // 
            btnSapXep.Font = new Font("Segoe UI", 9.5F);
            btnSapXep.Location = new Point(730, 99);
            btnSapXep.Name = "btnSapXep";
            btnSapXep.Size = new Size(106, 29);
            btnSapXep.TabIndex = 20;
            btnSapXep.Text = "Tăng dần ▲";
            btnSapXep.UseVisualStyleBackColor = true;
            btnSapXep.Click += btnSapXep_Click;
            // 
            // dgvKetQua
            // 
            dgvKetQua.AllowUserToAddRows = false;
            dgvKetQua.AllowUserToDeleteRows = false;
            dgvKetQua.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvKetQua.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvKetQua.Location = new Point(24, 138);
            dgvKetQua.MultiSelect = false;
            dgvKetQua.Name = "dgvKetQua";
            dgvKetQua.ReadOnly = true;
            dgvKetQua.RowHeadersWidth = 48;
            dgvKetQua.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvKetQua.Size = new Size(812, 215);
            dgvKetQua.TabIndex = 21;
            dgvKetQua.CellClick += dgvKetQua_CellClick;
            dgvKetQua.ColumnHeaderMouseClick += dgvKetQua_ColumnHeaderMouseClick;
            // 
            // lblThongKe
            // 
            lblThongKe.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblThongKe.ForeColor = Color.FromArgb(30, 64, 175);
            lblThongKe.Location = new Point(24, 366);
            lblThongKe.Name = "lblThongKe";
            lblThongKe.Size = new Size(515, 22);
            lblThongKe.TabIndex = 22;
            lblThongKe.Text = "Tổng: 0 bản ghi";
            // 
            // lblDongTrang
            // 
            lblDongTrang.AutoSize = true;
            lblDongTrang.Font = new Font("Segoe UI", 9.5F);
            lblDongTrang.Location = new Point(545, 366);
            lblDongTrang.Name = "lblDongTrang";
            lblDongTrang.Size = new Size(76, 17);
            lblDongTrang.TabIndex = 23;
            lblDongTrang.Text = "Dòng/trang:";
            // 
            // cboKichThuocTrang
            // 
            cboKichThuocTrang.DropDownStyle = ComboBoxStyle.DropDownList;
            cboKichThuocTrang.Font = new Font("Segoe UI", 9.5F);
            cboKichThuocTrang.FormattingEnabled = true;
            cboKichThuocTrang.Location = new Point(626, 363);
            cboKichThuocTrang.Name = "cboKichThuocTrang";
            cboKichThuocTrang.Size = new Size(56, 24);
            cboKichThuocTrang.TabIndex = 24;
            cboKichThuocTrang.SelectedIndexChanged += cboKichThuocTrang_SelectedIndexChanged;
            // 
            // btnTrangTruoc
            // 
            btnTrangTruoc.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnTrangTruoc.Location = new Point(688, 361);
            btnTrangTruoc.Name = "btnTrangTruoc";
            btnTrangTruoc.Size = new Size(32, 29);
            btnTrangTruoc.TabIndex = 25;
            btnTrangTruoc.Text = "<";
            btnTrangTruoc.UseVisualStyleBackColor = true;
            btnTrangTruoc.Click += btnTrangTruoc_Click;
            // 
            // lblTrang
            // 
            lblTrang.Font = new Font("Segoe UI", 9.5F);
            lblTrang.Location = new Point(724, 366);
            lblTrang.Name = "lblTrang";
            lblTrang.Size = new Size(74, 20);
            lblTrang.TabIndex = 26;
            lblTrang.Text = "Trang 1/1";
            lblTrang.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnTrangSau
            // 
            btnTrangSau.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnTrangSau.Location = new Point(804, 361);
            btnTrangSau.Name = "btnTrangSau";
            btnTrangSau.Size = new Size(32, 29);
            btnTrangSau.TabIndex = 27;
            btnTrangSau.Text = ">";
            btnTrangSau.UseVisualStyleBackColor = true;
            btnTrangSau.Click += btnTrangSau_Click;
            // 
            // lblTen
            // 
            lblTen.AutoSize = true;
            lblTen.Font = new Font("Segoe UI", 9.5F);
            lblTen.Location = new Point(24, 404);
            lblTen.Name = "lblTen";
            lblTen.Size = new Size(133, 17);
            lblTen.TabIndex = 28;
            lblTen.Text = "Họ tên (Khóa chính):";
            // 
            // txtTen
            // 
            txtTen.Font = new Font("Segoe UI", 9.5F);
            txtTen.Location = new Point(24, 426);
            txtTen.Name = "txtTen";
            txtTen.Size = new Size(320, 24);
            txtTen.TabIndex = 29;
            // 
            // lblTuoi
            // 
            lblTuoi.AutoSize = true;
            lblTuoi.Font = new Font("Segoe UI", 9.5F);
            lblTuoi.Location = new Point(364, 404);
            lblTuoi.Name = "lblTuoi";
            lblTuoi.Size = new Size(37, 17);
            lblTuoi.TabIndex = 30;
            lblTuoi.Text = "Tuổi:";
            // 
            // txtTuoi
            // 
            txtTuoi.Font = new Font("Segoe UI", 9.5F);
            txtTuoi.Location = new Point(364, 426);
            txtTuoi.Name = "txtTuoi";
            txtTuoi.Size = new Size(130, 24);
            txtTuoi.TabIndex = 31;
            // 
            // lblDiaChiNhap
            // 
            lblDiaChiNhap.AutoSize = true;
            lblDiaChiNhap.Font = new Font("Segoe UI", 9.5F);
            lblDiaChiNhap.Location = new Point(514, 404);
            lblDiaChiNhap.Name = "lblDiaChiNhap";
            lblDiaChiNhap.Size = new Size(55, 17);
            lblDiaChiNhap.TabIndex = 32;
            lblDiaChiNhap.Text = "Địa chỉ:";
            // 
            // txtDiaChiNhap
            // 
            txtDiaChiNhap.Font = new Font("Segoe UI", 9.5F);
            txtDiaChiNhap.Location = new Point(514, 426);
            txtDiaChiNhap.Name = "txtDiaChiNhap";
            txtDiaChiNhap.Size = new Size(322, 24);
            txtDiaChiNhap.TabIndex = 33;
            // 
            // btnThem
            // 
            btnThem.Font = new Font("Segoe UI", 9.5F);
            btnThem.Location = new Point(24, 474);
            btnThem.Name = "btnThem";
            btnThem.Size = new Size(78, 36);
            btnThem.TabIndex = 34;
            btnThem.Text = "Thêm";
            btnThem.UseVisualStyleBackColor = true;
            btnThem.Click += btnThem_Click;
            // 
            // btnSua
            // 
            btnSua.Font = new Font("Segoe UI", 9.5F);
            btnSua.Location = new Point(108, 474);
            btnSua.Name = "btnSua";
            btnSua.Size = new Size(78, 36);
            btnSua.TabIndex = 35;
            btnSua.Text = "Sửa";
            btnSua.UseVisualStyleBackColor = true;
            btnSua.Click += btnSua_Click;
            // 
            // btnXoa
            // 
            btnXoa.Font = new Font("Segoe UI", 9.5F);
            btnXoa.Location = new Point(192, 474);
            btnXoa.Name = "btnXoa";
            btnXoa.Size = new Size(78, 36);
            btnXoa.TabIndex = 36;
            btnXoa.Text = "Xóa";
            btnXoa.UseVisualStyleBackColor = true;
            btnXoa.Click += btnXoa_Click;
            // 
            // btnBatch
            // 
            btnBatch.Font = new Font("Segoe UI", 9.5F);
            btnBatch.Location = new Point(282, 474);
            btnBatch.Name = "btnBatch";
            btnBatch.Size = new Size(132, 36);
            btnBatch.TabIndex = 37;
            btnBatch.Text = "Batch +1 tuổi";
            btnBatch.UseVisualStyleBackColor = true;
            btnBatch.Click += btnBatch_Click;
            // 
            // btnNhapCsv
            // 
            btnNhapCsv.Font = new Font("Segoe UI", 9.5F);
            btnNhapCsv.Location = new Point(424, 474);
            btnNhapCsv.Name = "btnNhapCsv";
            btnNhapCsv.Size = new Size(94, 36);
            btnNhapCsv.TabIndex = 38;
            btnNhapCsv.Text = "Nhập CSV";
            btnNhapCsv.UseVisualStyleBackColor = true;
            btnNhapCsv.Click += btnNhapCsv_Click;
            // 
            // btnXuatCsv
            // 
            btnXuatCsv.Font = new Font("Segoe UI", 9.5F);
            btnXuatCsv.Location = new Point(524, 474);
            btnXuatCsv.Name = "btnXuatCsv";
            btnXuatCsv.Size = new Size(94, 36);
            btnXuatCsv.TabIndex = 39;
            btnXuatCsv.Text = "Xuất CSV";
            btnXuatCsv.UseVisualStyleBackColor = true;
            btnXuatCsv.Click += btnXuatCsv_Click;
            // 
            // btnBieuDo
            // 
            btnBieuDo.Font = new Font("Segoe UI", 9.5F);
            btnBieuDo.Location = new Point(628, 474);
            btnBieuDo.Name = "btnBieuDo";
            btnBieuDo.Size = new Size(102, 36);
            btnBieuDo.TabIndex = 40;
            btnBieuDo.Text = "Biểu đồ (SP)";
            btnBieuDo.UseVisualStyleBackColor = true;
            btnBieuDo.Click += btnBieuDo_Click;
            // 
            // btnVeForm1
            // 
            btnVeForm1.Font = new Font("Segoe UI", 9.5F);
            btnVeForm1.Location = new Point(736, 474);
            btnVeForm1.Name = "btnVeForm1";
            btnVeForm1.Size = new Size(100, 36);
            btnVeForm1.TabIndex = 41;
            btnVeForm1.Text = "Về Form 1";
            btnVeForm1.UseVisualStyleBackColor = true;
            btnVeForm1.Click += btnVeForm1_Click;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(860, 534);
            Controls.Add(btnVeForm1);
            Controls.Add(btnBieuDo);
            Controls.Add(btnXuatCsv);
            Controls.Add(btnNhapCsv);
            Controls.Add(btnBatch);
            Controls.Add(btnXoa);
            Controls.Add(btnSua);
            Controls.Add(btnThem);
            Controls.Add(txtDiaChiNhap);
            Controls.Add(lblDiaChiNhap);
            Controls.Add(txtTuoi);
            Controls.Add(lblTuoi);
            Controls.Add(txtTen);
            Controls.Add(lblTen);
            Controls.Add(btnTrangSau);
            Controls.Add(lblTrang);
            Controls.Add(btnTrangTruoc);
            Controls.Add(cboKichThuocTrang);
            Controls.Add(lblDongTrang);
            Controls.Add(lblThongKe);
            Controls.Add(dgvKetQua);
            Controls.Add(btnSapXep);
            Controls.Add(cboSapXep);
            Controls.Add(lblSapXep);
            Controls.Add(chkCotNhomTuoi);
            Controls.Add(chkCotDiaChi);
            Controls.Add(chkCotTuoi);
            Controls.Add(chkCotTen);
            Controls.Add(lblHienCot);
            Controls.Add(btnBoLoc);
            Controls.Add(btnTimKiem);
            Controls.Add(txtTuoiDen);
            Controls.Add(lblTuoiDen);
            Controls.Add(txtTuoiTu);
            Controls.Add(lblTuoiLoc);
            Controls.Add(cboDiaChi);
            Controls.Add(lblDiaChiLoc);
            Controls.Add(txtTimTen);
            Controls.Add(lblTenLoc);
            Controls.Add(picLogo);
            Controls.Add(chkHienLogo);
            Controls.Add(lblTieuDe);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Form2";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form2 - Quản lý bảng NGUOI";
            Load += Form2_Load;
            ((System.ComponentModel.ISupportInitialize)picLogo).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvKetQua).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTieuDe;
        private CheckBox chkHienLogo;
        private PictureBox picLogo;
        private Label lblTenLoc;
        private TextBox txtTimTen;
        private Label lblDiaChiLoc;
        private ComboBox cboDiaChi;
        private Label lblTuoiLoc;
        private TextBox txtTuoiTu;
        private Label lblTuoiDen;
        private TextBox txtTuoiDen;
        private Button btnTimKiem;
        private Button btnBoLoc;
        private Label lblHienCot;
        private CheckBox chkCotTen;
        private CheckBox chkCotTuoi;
        private CheckBox chkCotDiaChi;
        private CheckBox chkCotNhomTuoi;
        private Label lblSapXep;
        private ComboBox cboSapXep;
        private Button btnSapXep;
        private DataGridView dgvKetQua;
        private Label lblThongKe;
        private Label lblDongTrang;
        private ComboBox cboKichThuocTrang;
        private Button btnTrangTruoc;
        private Label lblTrang;
        private Button btnTrangSau;
        private Label lblTen;
        private TextBox txtTen;
        private Label lblTuoi;
        private TextBox txtTuoi;
        private Label lblDiaChiNhap;
        private TextBox txtDiaChiNhap;
        private Button btnThem;
        private Button btnSua;
        private Button btnXoa;
        private Button btnBatch;
        private Button btnNhapCsv;
        private Button btnXuatCsv;
        private Button btnBieuDo;
        private Button btnVeForm1;
    }
}
