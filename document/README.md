# Hướng dẫn WinForms & SQL Server (Từ Cơ bản đến Nâng cao)

Tài liệu này tổng hợp toàn bộ bài thực hành kết nối **C# WinForms** với **SQL Server (`QUANLY`)** qua **EF Core (`QuanLyDbContext`)** trên `Form1`, `Form2`, `Form3`, `Form4` và `FormBieuDo`.

---

## 1. Cấu trúc thư mục & thành phần dự án

```text
db-desktop-demo/
├── database/
│   └── init_quanly.sql                 # Script tạo DB QUANLY, 2 bảng NGUOI & DIEM, 200 bản ghi,
│                                       # VIEW vw_NguoiDiem, SP sp_ThongKeTheoDiaChi và 2 TRIGGER
├── document/
│   ├── huongdan SQLSERVER udung.pdf    # Tài liệu hướng dẫn gốc
│   └── README.md                       # Tài liệu tổng hợp này
└── DBDemoWinFormApp/
    ├── DBDemoWinFormApp.slnx           # Solution Visual Studio
    └── DBDemoWinFormApp/
        ├── Program.cs                  # Điểm khởi chạy ứng dụng (mở Form1)
        ├── Data/
        │   └── QuanLyDbContext.cs      # Kết nối SQL Server, ánh xạ bảng có Trigger & SQL View
        ├── Models/
        │   ├── Nguoi.cs                # Bảng NGUOI + cột dẫn xuất [NotMapped] NhomTuoi
        │   ├── Diem.cs                 # Bảng DIEM + cột dẫn xuất [NotMapped] DiemTB, XepLoai
        │   ├── NguoiDiemView.cs        # Ánh xạ SQL View vw_NguoiDiem + cột dẫn xuất
        │   └── ThongKeDiaChiView.cs    # Kết quả trả về từ Stored Procedure sp_ThongKeTheoDiaChi
        ├── Helpers/
        │   ├── InputValidator.cs       # Kiểm tra tuổi (0-150) và điểm (0-10)
        │   ├── LogoHelper.cs           # Chèn Logo mặc định, ẩn/hiện & đổi ảnh Logo từ máy
        │   ├── CsvHelper.cs            # Xuất CSV (UTF-8 BOM cho Excel) & Nhập hàng loạt từ CSV
        │   └── GridHelper.cs           # Ẩn/hiện cột, click tiêu đề sắp xếp, phân trang
        ├── Form1.cs / .Designer.cs     # Trang chính: Test kết nối, mở Form2/Form3/Form4/FormBieuDo
        ├── Form2.cs / .Designer.cs     # Quản lý bảng NGUOI (đủ 10 tính năng)
        ├── Form3.cs / .Designer.cs     # Quản lý bảng DIEM (đủ 10 tính năng)
        ├── Form4.cs / .Designer.cs     # Quản lý bảng gộp vw_NguoiDiem (đủ 10 tính năng)
        └── FormBieuDo.cs / .Designer.cs# Dashboard vẽ biểu đồ GDI+ từ Stored Procedure & View
```

---

## 2. Bản đồ 10 tính năng nâng cao áp dụng trên `Form2`, `Form3`, `Form4`

| # | Tính năng | Cách triển khai trong project |
|---|---|---|
| 1 | **Chèn Logo & Ẩn/Hiện Logo** | `PictureBox picLogo` + `CheckBox chkHienLogo` qua [LogoHelper.cs](../DBDemoWinFormApp/DBDemoWinFormApp/Helpers/LogoHelper.cs). Nhấp trực tiếp vào Logo để chọn ảnh `.png`/`.jpg` từ máy. |
| 2 | **Ẩn / Hiện từng cột DataGridView** | Hàng CheckBox phía trên bảng gắn qua `GridHelper.GanCheckBoxAnHienCot(...)` giúp bật/tắt `Columns[name].Visible`. |
| 3 | **Sắp xếp Tăng / Giảm** | Hỗ trợ cả 2 cách: chọn cột trong `ComboBox` + nút `Tăng dần ▲ / Giảm dần ▼`, hoặc nhấp trực tiếp vào dòng tiêu đề cột (`ColumnHeaderMouseClick`). |
| 4 | **Bộ lọc đa điều kiện** | Kết hợp lọc Tên + `ComboBox` Địa chỉ (`DISTINCT`) + Khoảng Tuổi (`Từ..Đến`) + Khoảng Điểm TB + Xếp loại (`XayDungTruyVanLoc`). |
| 5 | **Cột tính toán dẫn xuất & Thống kê tổng hợp** | Thuộc tính `[NotMapped]` (`NhomTuoi`, `DiemTB`, `XepLoai`) tự tính theo từng dòng; dòng `lblThongKe` hiển thị `COUNT`, `AVG`, `MAX`, `MIN` trên toàn tập lọc. |
| 6 | **Xử lý Phân trang** | Chọn số dòng/trang (`20`, `50`, `200`) và chuyển trang `<` / `>` bằng `.Skip((trang - 1) * size).Take(size)` tại SQL Server. |
| 7 | **Stored Procedure, View & Trigger** | • `Form4` đọc trực tiếp từ SQL View `dbo.vw_NguoiDiem`.<br>• `FormBieuDo` gọi `EXEC dbo.sp_ThongKeTheoDiaChi`.<br>• Bảng `NGUOI` và `DIEM` gắn Trigger `trg_KiemTraTuoiHopLe`, `trg_KiemTraDiemHopLe` (kèm `HasTrigger` trong `QuanLyDbContext`). |
| 8 | **Giao dịch hàng loạt & Rollback** | Nút `Batch +1 tuổi` (`Form2`) và `Batch +0.5đ Toán` (`Form3`, `Form4`) dùng `BeginTransaction()`. Nếu có bản ghi vượt ngưỡng `> 10` điểm hoặc `> 150` tuổi, Trigger SQL Server chặn lại và tự `Rollback()` toàn bộ. |
| 9 | **Import / Export CSV (Excel)** | Nút `Xuất CSV` xuất các cột đang hiển thị kèm UTF-8 BOM mở trực tiếp bằng Excel; `Nhập CSV` nạp hàng loạt trong 1 Transaction (tự `Rollback` nếu có dòng lỗi). |
| 10 | **Biểu đồ trực quan (Dashboard)** | Nút `Biểu đồ (SP)` mở [FormBieuDo.cs](../DBDemoWinFormApp/DBDemoWinFormApp/FormBieuDo.cs), vẽ biểu đồ cột Số người & Điểm TB theo Tỉnh/Thành và tỷ lệ Xếp loại bằng GDI+. |

---

## 3. Cách chạy thử & Demo trên lớp

1. Đảm bảo đã chạy script [init_quanly.sql](../database/init_quanly.sql) trên SQL Server để khởi tạo 200 bản ghi, `vw_NguoiDiem`, `sp_ThongKeTheoDiaChi` và 2 Trigger.
2. Mở [DBDemoWinFormApp.slnx](../DBDemoWinFormApp/DBDemoWinFormApp.slnx) trong Visual Studio và nhấn **F5**.
3. **Gợi ý kịch bản test Rollback + Trigger ngay**:
   - Vào **Form 3** (hoặc **Form 4**), bấm thẳng **Batch +0.5đ Toán** khi chưa lọc (trong 200 người có những bạn đã đạt `9.75` hoặc `10.0`).
   - SQL Server Trigger `trg_KiemTraDiemHopLe` sẽ báo lỗi vượt `10` điểm và ứng dụng sẽ **Rollback toàn bộ**, không làm sai dữ liệu của những bạn khác.
   - Sau đó lọc `ĐTB từ 0 đến 7` rồi bấm **Batch +0.5đ Toán** ➔ Giao dịch **Commit** thành công!
