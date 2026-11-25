namespace DBDemoWinFormApp.Helpers
{
    /// <summary>
    /// Helper dùng chung cho DataGridView: ẩn/hiện cột bằng CheckBox, click tiêu đề cột để sắp xếp và tính toán phân trang.
    /// </summary>
    internal static class GridHelper
    {
        public static void GanCheckBoxAnHienCot(DataGridView dgv, params (CheckBox Check, string ColumnName)[] danhSachCot)
        {
            foreach ((CheckBox check, string _) in danhSachCot)
            {
                check.CheckedChanged += (_, _) => ApDungAnHienCot(dgv, danhSachCot);
            }
        }

        public static void ApDungAnHienCot(DataGridView dgv, params (CheckBox Check, string ColumnName)[] danhSachCot)
        {
            foreach ((CheckBox check, string columnName) in danhSachCot)
            {
                if (dgv.Columns[columnName] is DataGridViewColumn col)
                {
                    col.Visible = check.Checked;
                }
            }

            foreach (DataGridViewColumn col in dgv.Columns)
            {
                col.SortMode = DataGridViewColumnSortMode.Programmatic;
            }
        }

        public static void CapNhatPhanTrang(
            int tongBanGhi,
            ref int trangHienTai,
            int kichThuocTrang,
            out int tongTrang,
            Label lblTrang,
            Button btnTruoc,
            Button btnSau)
        {
            tongTrang = Math.Max(1, (int)Math.Ceiling((double)tongBanGhi / Math.Max(1, kichThuocTrang)));
            if (trangHienTai > tongTrang)
            {
                trangHienTai = tongTrang;
            }
            if (trangHienTai < 1)
            {
                trangHienTai = 1;
            }

            lblTrang.Text = $"Trang {trangHienTai}/{tongTrang}";
            btnTruoc.Enabled = trangHienTai > 1;
            btnSau.Enabled = trangHienTai < tongTrang;
        }

        public static void XuLyClickTieuDeCot(
            DataGridView dgv,
            int columnIndex,
            ComboBox cboSapXep,
            IReadOnlyList<string> cotChoPhepSapXep,
            ref bool tangDan,
            Button btnSapXep,
            Action taiLai)
        {
            if (columnIndex < 0 || columnIndex >= dgv.Columns.Count)
            {
                return;
            }

            string propName = dgv.Columns[columnIndex].DataPropertyName;
            int idx = -1;
            for (int i = 0; i < cotChoPhepSapXep.Count; i++)
            {
                if (string.Equals(cotChoPhepSapXep[i], propName, StringComparison.Ordinal))
                {
                    idx = i;
                    break;
                }
            }

            if (idx < 0)
            {
                return;
            }

            if (cboSapXep.SelectedIndex == idx)
            {
                tangDan = !tangDan;
            }
            else
            {
                cboSapXep.SelectedIndex = idx;
                tangDan = true;
            }

            CapNhatNutSapXep(btnSapXep, tangDan);
            taiLai();

            if (dgv.Columns[propName] is DataGridViewColumn sortedCol)
            {
                sortedCol.HeaderCell.SortGlyphDirection = tangDan ? SortOrder.Ascending : SortOrder.Descending;
            }
        }

        public static void CapNhatNutSapXep(Button btnSapXep, bool tangDan)
        {
            btnSapXep.Text = tangDan ? "Tăng dần ▲" : "Giảm dần ▼";
        }
    }
}
