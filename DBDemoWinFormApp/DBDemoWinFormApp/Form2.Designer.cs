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
            btnVeForm1 = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvKetQua).BeginInit();
            SuspendLayout();
            // 
            // lblTieuDe
            // 
            lblTieuDe.AutoSize = true;
            lblTieuDe.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTieuDe.Location = new Point(28, 18);
            lblTieuDe.Name = "lblTieuDe";
            lblTieuDe.Size = new Size(99, 25);
            lblTieuDe.TabIndex = 0;
            lblTieuDe.Text = "KẾT QUẢ";
            // 
            // lblNhapDiaChi
            // 
            lblNhapDiaChi.AutoSize = true;
            lblNhapDiaChi.Font = new Font("Segoe UI", 10F);
            lblNhapDiaChi.Location = new Point(28, 66);
            lblNhapDiaChi.Name = "lblNhapDiaChi";
            lblNhapDiaChi.Size = new Size(95, 19);
            lblNhapDiaChi.TabIndex = 1;
            lblNhapDiaChi.Text = "Nhập địa chỉ:";
            // 
            // txtDiaChi
            // 
            txtDiaChi.Font = new Font("Segoe UI", 10F);
            txtDiaChi.Location = new Point(135, 63);
            txtDiaChi.Name = "txtDiaChi";
            txtDiaChi.PlaceholderText = "Ví dụ: HANOI";
            txtDiaChi.Size = new Size(265, 25);
            txtDiaChi.TabIndex = 2;
            // 
            // btnTimKiem
            // 
            btnTimKiem.Font = new Font("Segoe UI", 10F);
            btnTimKiem.Location = new Point(415, 61);
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
            lblKetQua.Location = new Point(28, 106);
            lblKetQua.Name = "lblKetQua";
            lblKetQua.Size = new Size(64, 19);
            lblKetQua.TabIndex = 4;
            lblKetQua.Text = "Kết quả:";
            // 
            // dgvKetQua
            // 
            dgvKetQua.AllowUserToAddRows = false;
            dgvKetQua.AllowUserToDeleteRows = false;
            dgvKetQua.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvKetQua.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvKetQua.Location = new Point(28, 132);
            dgvKetQua.Name = "dgvKetQua";
            dgvKetQua.ReadOnly = true;
            dgvKetQua.RowHeadersWidth = 48;
            dgvKetQua.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvKetQua.Size = new Size(544, 200);
            dgvKetQua.TabIndex = 5;
            // 
            // btnVeForm1
            // 
            btnVeForm1.Font = new Font("Segoe UI", 10F);
            btnVeForm1.Location = new Point(452, 346);
            btnVeForm1.Name = "btnVeForm1";
            btnVeForm1.Size = new Size(120, 36);
            btnVeForm1.TabIndex = 6;
            btnVeForm1.Text = "Về Form 1";
            btnVeForm1.UseVisualStyleBackColor = true;
            btnVeForm1.Click += btnVeForm1_Click;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(600, 400);
            Controls.Add(btnVeForm1);
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
            Text = "Form2 - Kết quả tìm kiếm";
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
        private Button btnVeForm1;
    }
}
