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
            btnKetNoi = new Button();
            btnSangForm2 = new Button();
            btnSangForm3 = new Button();
            btnSangForm4 = new Button();
            btnBieuDo = new Button();
            btnThoat = new Button();
            SuspendLayout();
            // 
            // lblTieuDe
            // 
            lblTieuDe.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            lblTieuDe.Location = new Point(30, 20);
            lblTieuDe.Name = "lblTieuDe";
            lblTieuDe.Size = new Size(400, 36);
            lblTieuDe.TabIndex = 0;
            lblTieuDe.Text = "TRANG CHÍNH";
            lblTieuDe.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnKetNoi
            // 
            btnKetNoi.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            btnKetNoi.Location = new Point(60, 75);
            btnKetNoi.Name = "btnKetNoi";
            btnKetNoi.Size = new Size(270, 44);
            btnKetNoi.TabIndex = 1;
            btnKetNoi.Text = "Nối với CSDL";
            btnKetNoi.UseVisualStyleBackColor = true;
            btnKetNoi.Click += btnKetNoi_Click;
            // 
            // btnSangForm2
            // 
            btnSangForm2.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            btnSangForm2.Location = new Point(60, 130);
            btnSangForm2.Name = "btnSangForm2";
            btnSangForm2.Size = new Size(270, 44);
            btnSangForm2.TabIndex = 2;
            btnSangForm2.Text = "Form 2 - Bảng NGUOI";
            btnSangForm2.UseVisualStyleBackColor = true;
            btnSangForm2.Click += btnSangForm2_Click;
            // 
            // btnSangForm3
            // 
            btnSangForm3.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            btnSangForm3.Location = new Point(60, 185);
            btnSangForm3.Name = "btnSangForm3";
            btnSangForm3.Size = new Size(270, 44);
            btnSangForm3.TabIndex = 3;
            btnSangForm3.Text = "Form 3 - Bảng DIEM";
            btnSangForm3.UseVisualStyleBackColor = true;
            btnSangForm3.Click += btnSangForm3_Click;
            // 
            // btnSangForm4
            // 
            btnSangForm4.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            btnSangForm4.Location = new Point(60, 240);
            btnSangForm4.Name = "btnSangForm4";
            btnSangForm4.Size = new Size(270, 44);
            btnSangForm4.TabIndex = 4;
            btnSangForm4.Text = "Form 4 - Gộp NGUOI & DIEM";
            btnSangForm4.UseVisualStyleBackColor = true;
            btnSangForm4.Click += btnSangForm4_Click;
            // 
            // btnBieuDo
            // 
            btnBieuDo.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            btnBieuDo.Location = new Point(60, 295);
            btnBieuDo.Name = "btnBieuDo";
            btnBieuDo.Size = new Size(270, 44);
            btnBieuDo.TabIndex = 5;
            btnBieuDo.Text = "Biểu đồ & Thống kê (SP / View)";
            btnBieuDo.UseVisualStyleBackColor = true;
            btnBieuDo.Click += btnBieuDo_Click;
            // 
            // btnThoat
            // 
            btnThoat.Font = new Font("Segoe UI", 10F);
            btnThoat.Location = new Point(330, 360);
            btnThoat.Name = "btnThoat";
            btnThoat.Size = new Size(100, 38);
            btnThoat.TabIndex = 6;
            btnThoat.Text = "Thoát";
            btnThoat.UseVisualStyleBackColor = true;
            btnThoat.Click += btnThoat_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(460, 420);
            Controls.Add(btnThoat);
            Controls.Add(btnBieuDo);
            Controls.Add(btnSangForm4);
            Controls.Add(btnSangForm3);
            Controls.Add(btnSangForm2);
            Controls.Add(btnKetNoi);
            Controls.Add(lblTieuDe);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1 - Trang chính";
            ResumeLayout(false);
        }

        #endregion

        private Label lblTieuDe;
        private Button btnKetNoi;
        private Button btnSangForm2;
        private Button btnSangForm3;
        private Button btnSangForm4;
        private Button btnBieuDo;
        private Button btnThoat;
    }
}
