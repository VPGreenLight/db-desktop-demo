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
            chkHienLogo = new CheckBox();
            picLogo = new PictureBox();
            lblHoTenLoc = new Label();
            txtTimHoTen = new TextBox();
            lblDiemLoc = new Label();
            txtDiemTu = new TextBox();
            lblDiemDen = new Label();
            txtDiemDen = new TextBox();
            lblXepLoaiLoc = new Label();
            cboXepLoai = new ComboBox();
            btnTimKiem = new Button();
            btnBoLoc = new Button();
            lblHienCot = new Label();
            chkCotHoTen = new CheckBox();
            chkCotToan = new CheckBox();
            chkCotVan = new CheckBox();
            chkCotDiemTB = new CheckBox();
            chkCotXepLoai = new CheckBox();
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
            lblHoTen = new Label();
            txtHoTen = new TextBox();
            lblToan = new Label();
            txtToan = new TextBox();
            lblVan = new Label();
            txtVan = new TextBox();
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
            lblTieuDe.Size = new Size(198, 25);
            lblTieuDe.TabIndex = 0;
            lblTieuDe.Text = "QUẢN LÝ BẢNG DIEM";
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
            // lblHoTenLoc
            // 
            lblHoTenLoc.AutoSize = true;
            lblHoTenLoc.Font = new Font("Segoe UI", 9.5F);
            lblHoTenLoc.Location = new Point(24, 67);
            lblHoTenLoc.Name = "lblHoTenLoc";
            lblHoTenLoc.Size = new Size(52, 17);
            lblHoTenLoc.TabIndex = 3;
            lblHoTenLoc.Text = "Họ tên:";
            // 
            // txtTimHoTen
            // 
            txtTimHoTen.Font = new Font("Segoe UI", 9.5F);
            txtTimHoTen.Location = new Point(82, 64);
            txtTimHoTen.Name = "txtTimHoTen";
            txtTimHoTen.PlaceholderText = "VD: An";
            txtTimHoTen.Size = new Size(165, 24);
            txtTimHoTen.TabIndex = 4;
            // 
            // lblDiemLoc
            // 
            lblDiemLoc.AutoSize = true;
            lblDiemLoc.Font = new Font("Segoe UI", 9.5F);
            lblDiemLoc.Location = new Point(258, 67);
            lblDiemLoc.Name = "lblDiemLoc";
            lblDiemLoc.Size = new Size(78, 17);
            lblDiemLoc.TabIndex = 5;
            lblDiemLoc.Text = "Điểm TB từ:";
            // 
            // txtDiemTu
            // 
            txtDiemTu.Font = new Font("Segoe UI", 9.5F);
            txtDiemTu.Location = new Point(340, 64);
            txtDiemTu.Name = "txtDiemTu";
            txtDiemTu.PlaceholderText = "0";
            txtDiemTu.Size = new Size(50, 24);
            txtDiemTu.TabIndex = 6;
            // 
            // lblDiemDen
            // 
            lblDiemDen.AutoSize = true;
            lblDiemDen.Font = new Font("Segoe UI", 9.5F);
            lblDiemDen.Location = new Point(396, 67);
            lblDiemDen.Name = "lblDiemDen";
            lblDiemDen.Size = new Size(34, 17);
            lblDiemDen.TabIndex = 7;
            lblDiemDen.Text = "đến:";
            // 
            // txtDiemDen
            // 
            txtDiemDen.Font = new Font("Segoe UI", 9.5F);
            txtDiemDen.Location = new Point(432, 64);
            txtDiemDen.Name = "txtDiemDen";
            txtDiemDen.PlaceholderText = "10";
            txtDiemDen.Size = new Size(50, 24);
            txtDiemDen.TabIndex = 8;
            // 
            // lblXepLoaiLoc
            // 
            lblXepLoaiLoc.AutoSize = true;
            lblXepLoaiLoc.Font = new Font("Segoe UI", 9.5F);
            lblXepLoaiLoc.Location = new Point(492, 67);
            lblXepLoaiLoc.Name = "lblXepLoaiLoc";
            lblXepLoaiLoc.Size = new Size(60, 17);
            lblXepLoaiLoc.TabIndex = 9;
            lblXepLoaiLoc.Text = "Xếp loại:";
            // 
            // cboXepLoai
            // 
            cboXepLoai.DropDownStyle = ComboBoxStyle.DropDownList;
            cboXepLoai.Font = new Font("Segoe UI", 9.5F);
            cboXepLoai.FormattingEnabled = true;
            cboXepLoai.Location = new Point(556, 64);
            cboXepLoai.Name = "cboXepLoai";
            cboXepLoai.Size = new Size(92, 24);
            cboXepLoai.TabIndex = 10;
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
            // chkCotHoTen
            // 
            chkCotHoTen.AutoSize = true;
            chkCotHoTen.Checked = true;
            chkCotHoTen.CheckState = CheckState.Checked;
            chkCotHoTen.Font = new Font("Segoe UI", 9.5F);
            chkCotHoTen.Location = new Point(64, 103);
            chkCotHoTen.Name = "chkCotHoTen";
            chkCotHoTen.Size = new Size(68, 21);
            chkCotHoTen.TabIndex = 14;
            chkCotHoTen.Text = "Họ tên";
            chkCotHoTen.UseVisualStyleBackColor = true;
            // 
            // chkCotToan
            // 
            chkCotToan.AutoSize = true;
            chkCotToan.Checked = true;
            chkCotToan.CheckState = CheckState.Checked;
            chkCotToan.Font = new Font("Segoe UI", 9.5F);
            chkCotToan.Location = new Point(136, 103);
            chkCotToan.Name = "chkCotToan";
            chkCotToan.Size = new Size(57, 21);
            chkCotToan.TabIndex = 15;
            chkCotToan.Text = "Toán";
            chkCotToan.UseVisualStyleBackColor = true;
            // 
            // chkCotVan
            // 
            chkCotVan.AutoSize = true;
            chkCotVan.Checked = true;
            chkCotVan.CheckState = CheckState.Checked;
            chkCotVan.Font = new Font("Segoe UI", 9.5F);
            chkCotVan.Location = new Point(198, 103);
            chkCotVan.Name = "chkCotVan";
            chkCotVan.Size = new Size(52, 21);
            chkCotVan.TabIndex = 16;
            chkCotVan.Text = "Văn";
            chkCotVan.UseVisualStyleBackColor = true;
            // 
            // chkCotDiemTB
            // 
            chkCotDiemTB.AutoSize = true;
            chkCotDiemTB.Checked = true;
            chkCotDiemTB.CheckState = CheckState.Checked;
            chkCotDiemTB.Font = new Font("Segoe UI", 9.5F);
            chkCotDiemTB.Location = new Point(256, 103);
            chkCotDiemTB.Name = "chkCotDiemTB";
            chkCotDiemTB.Size = new Size(78, 21);
            chkCotDiemTB.TabIndex = 17;
            chkCotDiemTB.Text = "Điểm TB";
            chkCotDiemTB.UseVisualStyleBackColor = true;
            // 
            // chkCotXepLoai
            // 
            chkCotXepLoai.AutoSize = true;
            chkCotXepLoai.Checked = true;
            chkCotXepLoai.CheckState = CheckState.Checked;
            chkCotXepLoai.Font = new Font("Segoe UI", 9.5F);
            chkCotXepLoai.Location = new Point(340, 103);
            chkCotXepLoai.Name = "chkCotXepLoai";
            chkCotXepLoai.Size = new Size(77, 21);
            chkCotXepLoai.TabIndex = 18;
            chkCotXepLoai.Text = "Xếp loại";
            chkCotXepLoai.UseVisualStyleBackColor = true;
            // 
            // lblSapXep
            // 
            lblSapXep.AutoSize = true;
            lblSapXep.Font = new Font("Segoe UI", 9.5F);
            lblSapXep.Location = new Point(532, 104);
            lblSapXep.Name = "lblSapXep";
            lblSapXep.Size = new Size(60, 17);
            lblSapXep.TabIndex = 19;
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
            cboSapXep.TabIndex = 20;
            cboSapXep.SelectedIndexChanged += cboSapXep_SelectedIndexChanged;
            // 
            // btnSapXep
            // 
            btnSapXep.Font = new Font("Segoe UI", 9.5F);
            btnSapXep.Location = new Point(730, 99);
            btnSapXep.Name = "btnSapXep";
            btnSapXep.Size = new Size(106, 29);
            btnSapXep.TabIndex = 21;
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
            dgvKetQua.TabIndex = 22;
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
            lblThongKe.TabIndex = 23;
            lblThongKe.Text = "Tổng: 0 bản ghi";
            // 
            // lblDongTrang
            // 
            lblDongTrang.AutoSize = true;
            lblDongTrang.Font = new Font("Segoe UI", 9.5F);
            lblDongTrang.Location = new Point(545, 366);
            lblDongTrang.Name = "lblDongTrang";
            lblDongTrang.Size = new Size(76, 17);
            lblDongTrang.TabIndex = 24;
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
            cboKichThuocTrang.TabIndex = 25;
            cboKichThuocTrang.SelectedIndexChanged += cboKichThuocTrang_SelectedIndexChanged;
            // 
            // btnTrangTruoc
            // 
            btnTrangTruoc.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnTrangTruoc.Location = new Point(688, 361);
            btnTrangTruoc.Name = "btnTrangTruoc";
            btnTrangTruoc.Size = new Size(32, 29);
            btnTrangTruoc.TabIndex = 26;
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
            lblTrang.TabIndex = 27;
            lblTrang.Text = "Trang 1/1";
            lblTrang.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnTrangSau
            // 
            btnTrangSau.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnTrangSau.Location = new Point(804, 361);
            btnTrangSau.Name = "btnTrangSau";
            btnTrangSau.Size = new Size(32, 29);
            btnTrangSau.TabIndex = 28;
            btnTrangSau.Text = ">";
            btnTrangSau.UseVisualStyleBackColor = true;
            btnTrangSau.Click += btnTrangSau_Click;
            // 
            // lblHoTen
            // 
            lblHoTen.AutoSize = true;
            lblHoTen.Font = new Font("Segoe UI", 9.5F);
            lblHoTen.Location = new Point(24, 404);
            lblHoTen.Name = "lblHoTen";
            lblHoTen.Size = new Size(133, 17);
            lblHoTen.TabIndex = 29;
            lblHoTen.Text = "Họ tên (Khóa chính):";
            // 
            // txtHoTen
            // 
            txtHoTen.Font = new Font("Segoe UI", 9.5F);
            txtHoTen.Location = new Point(24, 426);
            txtHoTen.Name = "txtHoTen";
            txtHoTen.Size = new Size(380, 24);
            txtHoTen.TabIndex = 30;
            // 
            // lblToan
            // 
            lblToan.AutoSize = true;
            lblToan.Font = new Font("Segoe UI", 9.5F);
            lblToan.Location = new Point(424, 404);
            lblToan.Name = "lblToan";
            lblToan.Size = new Size(116, 17);
            lblToan.TabIndex = 31;
            lblToan.Text = "Điểm Toán (0-10):";
            // 
            // txtToan
            // 
            txtToan.Font = new Font("Segoe UI", 9.5F);
            txtToan.Location = new Point(424, 426);
            txtToan.Name = "txtToan";
            txtToan.Size = new Size(196, 24);
            txtToan.TabIndex = 32;
            // 
            // lblVan
            // 
            lblVan.AutoSize = true;
            lblVan.Font = new Font("Segoe UI", 9.5F);
            lblVan.Location = new Point(640, 404);
            lblVan.Name = "lblVan";
            lblVan.Size = new Size(109, 17);
            lblVan.TabIndex = 33;
            lblVan.Text = "Điểm Văn (0-10):";
            // 
            // txtVan
            // 
            txtVan.Font = new Font("Segoe UI", 9.5F);
            txtVan.Location = new Point(640, 426);
            txtVan.Name = "txtVan";
            txtVan.Size = new Size(196, 24);
            txtVan.TabIndex = 34;
            // 
            // btnThem
            // 
            btnThem.Font = new Font("Segoe UI", 9.5F);
            btnThem.Location = new Point(24, 474);
            btnThem.Name = "btnThem";
            btnThem.Size = new Size(78, 36);
            btnThem.TabIndex = 35;
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
            btnSua.TabIndex = 36;
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
            btnXoa.TabIndex = 37;
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
            btnBatch.TabIndex = 38;
            btnBatch.Text = "Batch +0.5đ Toán";
            btnBatch.UseVisualStyleBackColor = true;
            btnBatch.Click += btnBatch_Click;
            // 
            // btnNhapCsv
            // 
            btnNhapCsv.Font = new Font("Segoe UI", 9.5F);
            btnNhapCsv.Location = new Point(424, 474);
            btnNhapCsv.Name = "btnNhapCsv";
            btnNhapCsv.Size = new Size(94, 36);
            btnNhapCsv.TabIndex = 39;
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
            btnXuatCsv.TabIndex = 40;
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
            btnBieuDo.TabIndex = 41;
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
            btnVeForm1.TabIndex = 42;
            btnVeForm1.Text = "Về Form 1";
            btnVeForm1.UseVisualStyleBackColor = true;
            btnVeForm1.Click += btnVeForm1_Click;
            // 
            // Form3
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
            Controls.Add(txtVan);
            Controls.Add(lblVan);
            Controls.Add(txtToan);
            Controls.Add(lblToan);
            Controls.Add(txtHoTen);
            Controls.Add(lblHoTen);
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
            Controls.Add(chkCotXepLoai);
            Controls.Add(chkCotDiemTB);
            Controls.Add(chkCotVan);
            Controls.Add(chkCotToan);
            Controls.Add(chkCotHoTen);
            Controls.Add(lblHienCot);
            Controls.Add(btnBoLoc);
            Controls.Add(btnTimKiem);
            Controls.Add(cboXepLoai);
            Controls.Add(lblXepLoaiLoc);
            Controls.Add(txtDiemDen);
            Controls.Add(lblDiemDen);
            Controls.Add(txtDiemTu);
            Controls.Add(lblDiemLoc);
            Controls.Add(txtTimHoTen);
            Controls.Add(lblHoTenLoc);
            Controls.Add(picLogo);
            Controls.Add(chkHienLogo);
            Controls.Add(lblTieuDe);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Form3";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form3 - Quản lý bảng DIEM";
            Load += Form3_Load;
            ((System.ComponentModel.ISupportInitialize)picLogo).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvKetQua).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTieuDe;
        private CheckBox chkHienLogo;
        private PictureBox picLogo;
        private Label lblHoTenLoc;
        private TextBox txtTimHoTen;
        private Label lblDiemLoc;
        private TextBox txtDiemTu;
        private Label lblDiemDen;
        private TextBox txtDiemDen;
        private Label lblXepLoaiLoc;
        private ComboBox cboXepLoai;
        private Button btnTimKiem;
        private Button btnBoLoc;
        private Label lblHienCot;
        private CheckBox chkCotHoTen;
        private CheckBox chkCotToan;
        private CheckBox chkCotVan;
        private CheckBox chkCotDiemTB;
        private CheckBox chkCotXepLoai;
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
        private Label lblHoTen;
        private TextBox txtHoTen;
        private Label lblToan;
        private TextBox txtToan;
        private Label lblVan;
        private TextBox txtVan;
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
