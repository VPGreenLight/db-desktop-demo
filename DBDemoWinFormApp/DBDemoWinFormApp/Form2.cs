using System.Globalization;
using DBDemoWinFormApp.Data;
using DBDemoWinFormApp.Models;
using Microsoft.EntityFrameworkCore;

namespace DBDemoWinFormApp
{
    public partial class Form2 : Form
    {
        private readonly Form? _parentForm;
        private readonly string _connectionString;

        public Form2()
        {
            InitializeComponent();
            _connectionString = QuanLyDbContext.DefaultConnectionString;
        }

        public Form2(Form? parentForm, string connectionString) : this()
        {
            _parentForm = parentForm;
            _connectionString = string.IsNullOrWhiteSpace(connectionString)
                ? QuanLyDbContext.DefaultConnectionString
                : connectionString;
        }

        private QuanLyDbContext CreateDbContext() => new(_connectionString);

        private bool IsNguoiMode => rdoNguoi.Checked;

        private void Form2_Load(object? sender, EventArgs e)
        {
            UpdateFieldLabels();
            LoadData();
        }

        private void Bang_CheckedChanged(object? sender, EventArgs e)
        {
            if (sender is RadioButton rb && !rb.Checked)
            {
                return;
            }

            UpdateFieldLabels();
            ClearInputs();
            txtTimKiem.Clear();
            LoadData();
        }

        private void UpdateFieldLabels()
        {
            if (IsNguoiMode)
            {
                lblTimKiem.Text = "Cho địa chỉ / tên:";
                txtTimKiem.PlaceholderText = "Ví dụ: HANOI";
                lblCot1.Text = "Họ tên (ten):";
                lblCot2.Text = "Tuổi (tuoi):";
                lblCot3.Text = "Địa chỉ (dchi):";
            }
            else
            {
                lblTimKiem.Text = "Cho họ tên:";
                txtTimKiem.PlaceholderText = "Ví dụ: Nguyen Van An";
                lblCot1.Text = "Họ tên (hoten):";
                lblCot2.Text = "Điểm Toán (toan):";
                lblCot3.Text = "Điểm Văn (van):";
            }
        }

        private void ClearInputs()
        {
            txtCot1.Clear();
            txtCot2.Clear();
            txtCot3.Clear();
            txtCot1.Focus();
        }

        private void LoadData(string? keyword = null)
        {
            try
            {
                using var db = CreateDbContext();
                string? term = string.IsNullOrWhiteSpace(keyword) ? null : keyword.Trim();
                bool exactMatch = chkTimChinhXac.Checked;

                if (IsNguoiMode)
                {
                    IQueryable<Nguoi> query = db.Nguois.AsNoTracking();
                    if (term is not null)
                    {
                        query = exactMatch
                            ? query.Where(n => n.Dchi == term || n.Ten == term)
                            : query.Where(n => n.Dchi.Contains(term) || n.Ten.Contains(term));
                    }

                    var list = query.OrderBy(n => n.Ten).ToList();
                    dgvKetQua.DataSource = list;
                    lblKetQuaTrangThai.Text = $"Tổng: {list.Count} bản ghi (NGUOI)";
                }
                else
                {
                    IQueryable<Diem> query = db.Diems.AsNoTracking();
                    if (term is not null)
                    {
                        query = exactMatch
                            ? query.Where(d => d.Hoten == term)
                            : query.Where(d => d.Hoten.Contains(term));
                    }

                    var list = query.OrderBy(d => d.Hoten).ToList();
                    dgvKetQua.DataSource = list;
                    lblKetQuaTrangThai.Text = $"Tổng: {list.Count} bản ghi (DIEM)";
                }

                lblKetQuaTrangThai.ForeColor = Color.DimGray;
            }
            catch (Exception ex)
            {
                lblKetQuaTrangThai.Text = "Lỗi tải dữ liệu CSDL";
                lblKetQuaTrangThai.ForeColor = Color.Firebrick;
                ShowError("Không thể tải dữ liệu từ SQL Server", ex);
            }
        }

        private void dgvKetQua_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            object? item = dgvKetQua.Rows[e.RowIndex].DataBoundItem;
            if (item is Nguoi nguoi)
            {
                txtCot1.Text = nguoi.Ten;
                txtCot2.Text = nguoi.Tuoi.ToString(CultureInfo.InvariantCulture);
                txtCot3.Text = nguoi.Dchi;
            }
            else if (item is Diem diem)
            {
                txtCot1.Text = diem.Hoten;
                txtCot2.Text = diem.Toan.ToString(CultureInfo.InvariantCulture);
                txtCot3.Text = diem.Van.ToString(CultureInfo.InvariantCulture);
            }
        }

        private void btnTimKiem_Click(object? sender, EventArgs e)
        {
            LoadData(txtTimKiem.Text);
        }

        private void btnTaiLai_Click(object? sender, EventArgs e)
        {
            txtTimKiem.Clear();
            LoadData();
        }

        private void btnLamMoi_Click(object? sender, EventArgs e)
        {
            ClearInputs();
        }

        private void btnThem_Click(object? sender, EventArgs e)
        {
            if (IsNguoiMode)
            {
                if (!TryReadNguoiInput(out Nguoi? input, out string error) || input is null)
                {
                    MessageBox.Show(error, "Thiếu/sai dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                ExecuteDbAction("Thêm người thành công!", db =>
                {
                    if (db.Nguois.Any(n => n.Ten == input.Ten))
                    {
                        throw new InvalidOperationException($"Đã tồn tại người có tên '{input.Ten}'.");
                    }

                    db.Nguois.Add(input);
                    db.SaveChanges();
                });
            }
            else
            {
                if (!TryReadDiemInput(out Diem? input, out string error) || input is null)
                {
                    MessageBox.Show(error, "Thiếu/sai dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                ExecuteDbAction("Thêm điểm thành công!", db =>
                {
                    if (db.Diems.Any(d => d.Hoten == input.Hoten))
                    {
                        throw new InvalidOperationException($"Đã tồn tại bảng điểm của '{input.Hoten}'.");
                    }

                    db.Diems.Add(input);
                    db.SaveChanges();
                });
            }
        }

        private void btnCapNhat_Click(object? sender, EventArgs e)
        {
            if (IsNguoiMode)
            {
                if (!TryReadNguoiInput(out Nguoi? input, out string error) || input is null)
                {
                    MessageBox.Show(error, "Thiếu/sai dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                ExecuteDbAction("Cập nhật thông tin thành công!", db =>
                {
                    Nguoi? existing = db.Nguois.Find(input.Ten);
                    if (existing is null)
                    {
                        throw new InvalidOperationException($"Không tìm thấy '{input.Ten}' trong bảng NGUOI để cập nhật.");
                    }

                    existing.Tuoi = input.Tuoi;
                    existing.Dchi = input.Dchi;
                    db.SaveChanges();
                });
            }
            else
            {
                if (!TryReadDiemInput(out Diem? input, out string error) || input is null)
                {
                    MessageBox.Show(error, "Thiếu/sai dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                ExecuteDbAction("Cập nhật điểm thành công!", db =>
                {
                    Diem? existing = db.Diems.Find(input.Hoten);
                    if (existing is null)
                    {
                        throw new InvalidOperationException($"Không tìm thấy '{input.Hoten}' trong bảng DIEM để cập nhật.");
                    }

                    existing.Toan = input.Toan;
                    existing.Van = input.Van;
                    db.SaveChanges();
                });
            }
        }

        private void btnXoa_Click(object? sender, EventArgs e)
        {
            string key = txtCot1.Text.Trim();
            if (string.IsNullOrEmpty(key))
            {
                MessageBox.Show("Vui lòng chọn một dòng hoặc nhập tên cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa '{key}' khỏi bảng {(IsNguoiMode ? "NGUOI" : "DIEM")}?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes)
            {
                return;
            }

            if (IsNguoiMode)
            {
                ExecuteDbAction("Đã xóa bản ghi khỏi NGUOI!", db =>
                {
                    Nguoi? existing = db.Nguois.Find(key);
                    if (existing is null)
                    {
                        throw new InvalidOperationException($"Không tìm thấy '{key}' trong bảng NGUOI.");
                    }

                    db.Nguois.Remove(existing);
                    db.SaveChanges();
                });
            }
            else
            {
                ExecuteDbAction("Đã xóa bản ghi khỏi DIEM!", db =>
                {
                    Diem? existing = db.Diems.Find(key);
                    if (existing is null)
                    {
                        throw new InvalidOperationException($"Không tìm thấy '{key}' trong bảng DIEM.");
                    }

                    db.Diems.Remove(existing);
                    db.SaveChanges();
                });
            }
        }

        private void ExecuteDbAction(string successMessage, Action<QuanLyDbContext> action)
        {
            try
            {
                using var db = CreateDbContext();
                action(db);
                MessageBox.Show(successMessage, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearInputs();
                LoadData(txtTimKiem.Text);
            }
            catch (Exception ex)
            {
                ShowError("Thao tác CSDL thất bại", ex);
            }
        }

        private bool TryReadNguoiInput(out Nguoi? nguoi, out string error)
        {
            nguoi = null;
            string ten = txtCot1.Text.Trim();
            string dchi = txtCot3.Text.Trim();

            if (string.IsNullOrEmpty(ten))
            {
                error = "Vui lòng nhập họ tên (ten).";
                return false;
            }

            if (!int.TryParse(txtCot2.Text.Trim(), out int tuoi) || tuoi < 0 || tuoi > 150)
            {
                error = "Tuổi phải là số nguyên hợp lệ (0 - 150).";
                return false;
            }

            if (string.IsNullOrEmpty(dchi))
            {
                error = "Vui lòng nhập địa chỉ (dchi).";
                return false;
            }

            nguoi = new Nguoi { Ten = ten, Tuoi = tuoi, Dchi = dchi };
            error = string.Empty;
            return true;
        }

        private bool TryReadDiemInput(out Diem? diem, out string error)
        {
            diem = null;
            string hoten = txtCot1.Text.Trim();

            if (string.IsNullOrEmpty(hoten))
            {
                error = "Vui lòng nhập họ tên (hoten).";
                return false;
            }

            if (!TryParseScore(txtCot2.Text, out double toan))
            {
                error = "Điểm Toán phải là số từ 0 đến 10.";
                return false;
            }

            if (!TryParseScore(txtCot3.Text, out double van))
            {
                error = "Điểm Văn phải là số từ 0 đến 10.";
                return false;
            }

            diem = new Diem { Hoten = hoten, Toan = toan, Van = van };
            error = string.Empty;
            return true;
        }

        private static bool TryParseScore(string raw, out double score)
        {
            string normalized = raw.Trim().Replace(',', '.');
            return double.TryParse(normalized, NumberStyles.Float, CultureInfo.InvariantCulture, out score)
                && score >= 0
                && score <= 10;
        }

        private static void ShowError(string title, Exception ex)
        {
            string detail = ex.InnerException?.Message ?? ex.Message;
            MessageBox.Show($"{title}:\n{detail}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnVeForm1_Click(object? sender, EventArgs e)
        {
            Close();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            if (_parentForm is { IsDisposed: false })
            {
                _parentForm.Show();
            }
        }
    }
}
