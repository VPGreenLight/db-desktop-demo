using System.Text;

namespace DBDemoWinFormApp.Helpers
{
    /// <summary>
    /// Helper Xuất (Export) dữ liệu từ DataGridView ra file CSV (mở trực tiếp bằng Excel)
    /// và Nhập (Import) dữ liệu từ file CSV vào ứng dụng.
    /// </summary>
    internal static class CsvHelper
    {
        /// <summary>
        /// Xuất toàn bộ các cột đang hiển thị trên DataGridView ra file CSV (UTF-8 BOM để không lỗi font tiếng Việt trong Excel).
        /// </summary>
        public static void XuatRaCsv(DataGridView dgv, string tenFileMacDinh)
        {
            if (dgv.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu trên bảng để xuất.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using var dialog = new SaveFileDialog
            {
                Title = "Xuất dữ liệu ra file CSV (Excel)",
                Filter = "CSV files (*.csv)|*.csv",
                FileName = tenFileMacDinh
            };

            if (dialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            try
            {
                List<DataGridViewColumn> cotHienThi = dgv.Columns
                    .Cast<DataGridViewColumn>()
                    .Where(c => c.Visible)
                    .OrderBy(c => c.DisplayIndex)
                    .ToList();

                var sb = new StringBuilder();
                sb.AppendLine(string.Join(",", cotHienThi.Select(c => ThoatCsv(c.HeaderText))));

                foreach (DataGridViewRow row in dgv.Rows)
                {
                    if (row.IsNewRow)
                    {
                        continue;
                    }

                    IEnumerable<string> cells = cotHienThi.Select(c =>
                        ThoatCsv(row.Cells[c.Index].Value?.ToString() ?? string.Empty));
                    sb.AppendLine(string.Join(",", cells));
                }

                // UTF-8 BOM giúp Microsoft Excel tự nhận diện đúng bảng mã tiếng Việt khi mở file.
                File.WriteAllText(dialog.FileName, sb.ToString(), new UTF8Encoding(encoderShouldEmitUTF8Identifier: true));
                MessageBox.Show($"Đã xuất {dgv.Rows.Count} dòng ra file:\n{dialog.FileName}", "Xuất CSV thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xuất file CSV:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Mở hộp thoại chọn file CSV và đọc thành danh sách các dòng (tự bỏ qua dòng tiêu đề đầu tiên).
        /// </summary>
        public static bool DocFileCsv(out List<string[]> cacDong)
        {
            cacDong = [];

            using var dialog = new OpenFileDialog
            {
                Title = "Chọn file CSV để nhập hàng loạt",
                Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*"
            };

            if (dialog.ShowDialog() != DialogResult.OK)
            {
                return false;
            }

            try
            {
                string[] lines = File.ReadAllLines(dialog.FileName, Encoding.UTF8);
                bool daBoTieuDe = false;

                foreach (string rawLine in lines)
                {
                    if (string.IsNullOrWhiteSpace(rawLine))
                    {
                        continue;
                    }

                    if (!daBoTieuDe)
                    {
                        daBoTieuDe = true;
                        continue;
                    }

                    cacDong.Add(TachDongCsv(rawLine));
                }

                if (cacDong.Count == 0)
                {
                    MessageBox.Show("File CSV không có dòng dữ liệu nào (sau dòng tiêu đề).", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể đọc file CSV:\n" + ex.Message, "Lỗi đọc file", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private static string ThoatCsv(string value)
        {
            if (value.Contains(',') || value.Contains('"') || value.Contains('\n') || value.Contains('\r'))
            {
                return $"\"{value.Replace("\"", "\"\"")}\"";
            }

            return value;
        }

        private static string[] TachDongCsv(string line)
        {
            var result = new List<string>();
            var current = new StringBuilder();
            bool inQuotes = false;

            for (int i = 0; i < line.Length; i++)
            {
                char ch = line[i];
                if (inQuotes)
                {
                    if (ch == '"')
                    {
                        if (i + 1 < line.Length && line[i + 1] == '"')
                        {
                            current.Append('"');
                            i++;
                        }
                        else
                        {
                            inQuotes = false;
                        }
                    }
                    else
                    {
                        current.Append(ch);
                    }
                }
                else if (ch == '"')
                {
                    inQuotes = true;
                }
                else if (ch == ',')
                {
                    result.Add(current.ToString().Trim());
                    current.Clear();
                }
                else
                {
                    current.Append(ch);
                }
            }

            result.Add(current.ToString().Trim());
            return [.. result];
        }
    }
}
