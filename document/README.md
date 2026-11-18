# Hướng dẫn kết nối WinForms với SQL Server (Tìm kiếm & Thêm / Sửa / Xóa)

Tài liệu này được thiết kế gọn nhẹ theo [huongdan SQLSERVER udung.pdf](./huongdan%20SQLSERVER%20udung.pdf) và vở bài tập để giúp người mới dễ học và giảng giải cơ chế kết nối giữa **C# WinForms** và **SQL Server** qua **`DbContext` (Entity Framework Core)** cho cả 2 bảng `NGUOI` và `DIEM`.

---

## 1. Sơ đồ điều hướng & Luồng tương tác với Database

### Sơ đồ điều hướng giữa các Form

```text
                    ┌────────────────────────────────────────┐
                    │       Form1 (TRANG CHÍNH)              │
                    │  • Nối với CSDL (OpenConnection)       │
                    │  • Form 2 - Bảng NGUOI                 │
                    │  • Form 3 - Bảng DIEM                  │
                    │  • Thoát (Application.Exit)            │
                    └───────┬────────────────────────┬───────┘
                            │                        │
            Bấm "Form 2"    ▼                        ▼    Bấm "Form 3"
   ┌─────────────────────────────────┐      ┌─────────────────────────────────┐
   │  Form2 - Quản lý bảng NGUOI     │      │  Form3 - Quản lý bảng DIEM      │
   │  • Tìm theo địa chỉ (dchi)      │      │  • Tìm theo họ tên (hoten)      │
   │  • DataGridView (click chọn)    │      │  • DataGridView (click chọn)    │
   │  • Thêm / Sửa / Xóa (CRUD)      │      │  • Thêm / Sửa / Xóa (CRUD)      │
   │  • Về Form 1 (Close -> Show)    │      │  • Về Form 1 (Close -> Show)    │
   └─────────────────────────────────┘      └─────────────────────────────────┘
```

### Luồng đi của dữ liệu trong mỗi thao tác

```text
[WinForms UI] (Nhập ô TextBox / Nhấp dòng DataGridView / Bấm nút Thêm, Sửa, Xóa, Tìm)
      │
      ▼
[Sự kiện Click] (btnTimKiem_Click, btnThem_Click, btnSua_Click, btnXoa_Click)
      │  using var db = new QuanLyDbContext();
      ▼
[QuanLyDbContext] (DbSet<Nguoi>, DbSet<Diem> + ConnectionString)
      │  EF Core dịch lệnh C# sang SQL có Parameter (@p0, @p1...)
      ▼
[SQL Server - CSDL QUANLY] (Bảng NGUOI, DIEM)
      │
      ▼
[Cập nhật lại DataGridView] (TaiDuLieuNguoi / TaiDuLieuDiem)
```

---

## 2. Cấu trúc thư mục & vai trò từng file

```text
db-desktop-demo/
├── database/
│   └── init_quanly.sql                 # Script tạo DB QUANLY, 2 bảng NGUOI & DIEM và 200 bản ghi mẫu
├── document/
│   ├── huongdan SQLSERVER udung.pdf    # Tài liệu hướng dẫn gốc
│   └── README.md                       # Tài liệu bài giảng này
└── DBDemoWinFormApp/
    ├── DBDemoWinFormApp.slnx           # File mở Solution trong Visual Studio
    └── DBDemoWinFormApp/
        ├── Program.cs                  # Điểm chạy đầu tiên, mở Form1
        ├── Models/
        │   ├── Nguoi.cs                # Mô tả 1 dòng của bảng NGUOI(ten, tuoi, dchi)
        │   └── Diem.cs                 # Mô tả 1 dòng của bảng DIEM(hoten, toan, van)
        ├── Data/
        │   └── QuanLyDbContext.cs      # Cầu nối trung tâm tới SQL Server (ConnectionString + DbSet)
        ├── Form1.cs / .Designer.cs     # Trang chính: Nối với CSDL, sang Form2/Form3, Thoát
        ├── Form2.cs / .Designer.cs     # Tìm kiếm & Thêm / Sửa / Xóa bảng NGUOI
        └── Form3.cs / .Designer.cs     # Tìm kiếm & Thêm / Sửa / Xóa bảng DIEM
```

---

## 3. Hướng dẫn từng bước

### Bước 1: Tạo CSDL `QUANLY` và 200 bản ghi mẫu (`database/init_quanly.sql`)

1. Mở **SQL Server Management Studio (SSMS)** và kết nối vào SQL Server trên máy (ví dụ `localhost` hoặc `.\SQLEXPRESS`).
2. Mở file `database/init_quanly.sql` và nhấn **Execute (F5)**.
3. Script sẽ tạo:
   - Cơ sở dữ liệu `QUANLY`
   - Bảng `NGUOI(ten, tuoi, dchi)` với `ten` là khóa chính (`PRIMARY KEY`)
   - Bảng `DIEM(hoten, toan, van)` với `hoten` là khóa chính (`PRIMARY KEY`)
   - 200 dòng dữ liệu mẫu cho mỗi bảng (bao gồm nhiều dòng `dchi = N'HANOI'` giống ví dụ trong vở ghi).

### Bước 2: Thư viện kết nối (`Microsoft.EntityFrameworkCore.SqlServer`)

Project đã cài sẵn gói NuGet `Microsoft.EntityFrameworkCore.SqlServer` phiên bản `10.0.15` (khớp với `net10.0-windows`). Nếu tạo project mới trong Visual Studio:
- Vào **Project ➔ Manage NuGet Packages... ➔ Browse** ➔ tìm `Microsoft.EntityFrameworkCore.SqlServer` và cài phiên bản khớp với phiên bản .NET của project.

### Bước 3: Hiểu tầng dữ liệu (`Models/` và `Data/QuanLyDbContext.cs`)

* **Models (`Nguoi.cs`, `Diem.cs`)**: Mỗi thuộc tính C# ứng với 1 cột SQL Server:
  - `[Table("NGUOI")]` / `[Table("DIEM")]`: nối class tới đúng tên bảng.
  - `[Key]`: đánh dấu **khóa chính** (`ten` hoặc `hoten`). EF Core dựa vào khóa chính này để chạy `Find(...)` tìm đúng dòng cần **Sửa** hoặc **Xóa**.
  - `[Column("...")]`: ánh xạ tên cột trong SQL Server.
  - `[DisplayName("...")]`: tên tiêu đề cột hiển thị trên `DataGridView`.
* **`QuanLyDbContext.cs`**: Cầu nối chứa chuỗi `ConnectionString`, hai bảng `DbSet<Nguoi> Nguois` / `DbSet<Diem> Diems`, và `OnConfiguring(...)` cấu hình `UseSqlServer(ConnectionString)`.

---

## 4. Bảng đối chiếu 4 thao tác C# EF Core ↔ SQL Server (CRUD)

Trên `Form2` (bảng `NGUOI`) và `Form3` (bảng `DIEM`), cả 4 chức năng **Đọc / Tìm kiếm, Thêm, Sửa, Xóa** đều tuân theo cùng một quy tắc dễ nhớ:

| Thao tác | Code C# trong `Form2.cs` / `Form3.cs` | Câu lệnh SQL có Parameter do EF Core sinh ra |
|---|---|---|
| **Đọc / Tìm kiếm** (`SELECT`) | `db.Nguois.ToList()` hoặc<br>`db.Nguois.Where(n => n.Dchi == diaChi).ToList()` | `SELECT [ten], [tuoi], [dchi] FROM [NGUOI]`<br>`WHERE [dchi] = @__diaChi_0` |
| **Thêm mới** (`INSERT`) | `db.Nguois.Add(nguoiMoi);`<br>`db.SaveChanges();` | `INSERT INTO [NGUOI] ([ten], [dchi], [tuoi])`<br>`VALUES (@p0, @p1, @p2)` |
| **Cập nhật** (`UPDATE`) | `var x = db.Nguois.Find(ten);`<br>`x.Tuoi = tuoi; x.Dchi = dchi;`<br>`db.SaveChanges();` | `UPDATE [NGUOI] SET [dchi] = @p0, [tuoi] = @p1`<br>`WHERE [ten] = @p2` |
| **Xóa** (`DELETE`) | `var x = db.Nguois.Find(ten);`<br>`db.Nguois.Remove(x);`<br>`db.SaveChanges();` | `DELETE FROM [NGUOI] WHERE [ten] = @p0` |

### 2 điểm quan trọng cần nhấn mạnh khi giảng giải

1. **Vai trò của `db.SaveChanges()`**:
   - Các lệnh `Add(...)`, sửa thuộc tính sau `Find(...)`, hoặc `Remove(...)` **mới chỉ đánh dấu thay đổi trong bộ nhớ C#**.
   - Chỉ khi gọi `db.SaveChanges()`, EF Core mới thật sự mở giao dịch và gửi câu lệnh `INSERT` / `UPDATE` / `DELETE` xuống SQL Server.
2. **Vai trò của khóa chính `[Key]` (`ten` và `hoten`)**:
   - Khi nhấp vào 1 dòng trên `DataGridView`, sự kiện `CellClick` điền sẵn dữ liệu xuống 3 ô nhập.
   - Khi bấm **Sửa** hoặc **Xóa**, hàm `db.Nguois.Find(ten)` / `db.Diems.Find(hoten)` dùng **khóa chính** này để tìm chính xác dòng trong SQL Server. Vì vậy không được đổi tên khóa chính khi bấm **Sửa** (muốn đổi tên thì **Xóa** dòng cũ hoặc **Thêm** dòng mới).

---

## 5. Chạy thử & Khắc phục lỗi thường gặp

* **Chạy bằng Visual Studio**: Mở `DBDemoWinFormApp/DBDemoWinFormApp.slnx` và nhấn **F5**.
* **Chạy bằng CLI**:
  ```powershell
  dotnet run --project DBDemoWinFormApp/DBDemoWinFormApp/DBDemoWinFormApp.csproj
  ```
* **Lỗi `A network-related or instance-specific error occurred...`**:
  - Kiểm tra dịch vụ **SQL Server** trong `services.msc` đã chạy (`Running`) chưa.
  - Kiểm tra `ConnectionString` trong `Data/QuanLyDbContext.cs` đã đúng tên server máy bạn chưa (`Server=localhost` cho bản mặc định hoặc `Server=.\SQLEXPRESS` cho bản Express).
* **Lỗi `Cannot open database "QUANLY"`**: Chạy lại file `database/init_quanly.sql` trong SSMS.
