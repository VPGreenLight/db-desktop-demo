# Hướng dẫn kết nối WinForms với SQL Server (Phiên bản tối giản cho người mới học)

Tài liệu này được thiết kế gọn nhẹ theo [huongdan SQLSERVER udung.pdf](./huongdan%20SQLSERVER%20udung.pdf) và sơ đồ vở bài tập để giúp người mới dễ học và dễ giảng giải cơ chế kết nối giữa **C# WinForms** và **SQL Server** qua **`DbContext` (Entity Framework Core)**.

---

## 1. Luồng tương tác tổng thể giữa WinForms và Database

Khi người dùng bấm nút trên giao diện, dữ liệu đi qua các tầng theo luồng sau:

```text
[WinForms UI] (Form1 / Form2)
      │  1. Người dùng bấm nút ("Nối với CSDL" hoặc "Tìm kiếm")
      ▼
[Sự kiện Click] (btnKetNoi_Click / btnTimKiem_Click)
      │  2. Khởi tạo: using var db = new QuanLyDbContext();
      ▼
[QuanLyDbContext] (DbSet<Nguoi>, DbSet<Diem> + ConnectionString)
      │  3. EF Core dịch câu lệnh C# (LINQ) sang SQL có Parameter (@__diaChi_0)
      ▼
[SQL Server - CSDL QUANLY] (Bảng NGUOI, DIEM)
      │  4. Trả về danh sách các đối tượng Nguoi (List<Nguoi>)
      ▼
[DataGridView] (dgvKetQua.DataSource = danhSach)
```

---

## 2. Cấu trúc thư mục & vai trò từng file

```text
db-desktop-demo/
├── database/
│   └── init_quanly.sql              # Script tạo CSDL QUANLY, 2 bảng NGUOI & DIEM và dữ liệu mẫu
├── document/
│   ├── huongdan SQLSERVER udung.pdf # Tài liệu hướng dẫn gốc
│   └── README.md                    # Tài liệu bài giảng này
└── DBDemoWinFormApp/
    ├── DBDemoWinFormApp.slnx        # File mở Solution trong Visual Studio
    └── DBDemoWinFormApp/
        ├── Program.cs               # Điểm chạy đầu tiên, mở Form1
        ├── Models/
        │   ├── Nguoi.cs             # Mô tả 1 dòng của bảng NGUOI(ten, tuoi, dchi)
        │   └── Diem.cs              # Mô tả 1 dòng của bảng DIEM(hoten, toan, van)
        ├── Data/
        │   └── QuanLyDbContext.cs   # Cầu nối trung tâm tới SQL Server (ConnectionString + DbSet)
        ├── Form1.cs / .Designer.cs  # Trang chính: Nối với CSDL, Form 2, Thoát
        └── Form2.cs / .Designer.cs  # Kết quả: Nhập địa chỉ, Tìm kiếm, DataGridView, Về Form 1
```

---

## 3. Hướng dẫn từng bước

### Bước 1: Tạo CSDL `QUANLY` trong SQL Server (`database/init_quanly.sql`)

1. Mở **SQL Server Management Studio (SSMS)** và kết nối vào `.\SQLEXPRESS` (Windows Authentication).
2. Mở file `database/init_quanly.sql` và nhấn **Execute (F5)**.
3. Script sẽ tạo:
   - Cơ sở dữ liệu `QUANLY`
   - Bảng `NGUOI(ten, tuoi, dchi)` với `ten` làm khóa chính (`PRIMARY KEY`)
   - Bảng `DIEM(hoten, toan, van)` với `hoten` làm khóa chính (`PRIMARY KEY`)
   - Dữ liệu mẫu (bao gồm các dòng có `dchi = N'HANOI'` giống ví dụ trong vở ghi).

### Bước 2: Thư viện kết nối (`Microsoft.EntityFrameworkCore.SqlServer`)

Project đã cài sẵn gói NuGet `Microsoft.EntityFrameworkCore.SqlServer` phiên bản `10.0.15` (tương thích `net10.0-windows`). Nếu dựng project mới trong Visual Studio:
- Vào **Project ➔ Manage NuGet Packages... ➔ Browse** ➔ tìm `Microsoft.EntityFrameworkCore.SqlServer` và cài phiên bản khớp với .NET của project.

### Bước 3: Hiểu tầng dữ liệu (`Models/Nguoi.cs` và `Data/QuanLyDbContext.cs`)

* **Model `Nguoi.cs`**: Mỗi thuộc tính trong class C# tương ứng với 1 cột trong bảng SQL Server:
  - `[Table("NGUOI")]` chỉ định tên bảng.
  - `[Key]` đánh dấu cột khóa chính (`ten`).
  - `[Column("ten")]`, `[Column("tuoi")]`, `[Column("dchi")]` nối đúng tên cột trong SQL Server.
  - `[DisplayName("...")]` đặt tên tiêu đề cột tiếng Việt hiển thị trên `DataGridView`.
* **DbContext `QuanLyDbContext.cs`**: Chứa đúng 3 phần cốt lõi:
  1. `ConnectionString`: địa chỉ trỏ tới SQL Server (`Server=.\SQLEXPRESS; Database=QUANLY; Integrated Security=True; TrustServerCertificate=True;`).
  2. `DbSet<Nguoi> Nguois` và `DbSet<Diem> Diems`: đại diện cho 2 bảng dữ liệu để C# truy vấn.
  3. `OnConfiguring(...)`: gọi `optionsBuilder.UseSqlServer(ConnectionString);` để cấu hình EF Core kết nối vào SQL Server.

### Bước 4: Luồng chạy trên `Form1` (`TRANG CHÍNH`)

Giao diện gồm đúng 3 nút như sơ đồ vở bài tập:
1. **`Nối với CSDL` (`btnKetNoi_Click`)**:
   - Tạo `using var db = new QuanLyDbContext();` (từ khóa `using` giúp tự đóng/giải phóng kết nối sau khi chạy xong).
   - Gọi `db.Database.OpenConnection();` và `db.Database.CloseConnection();`.
   - Thành công ➔ hiện `"Đã kết nối CSDL QUANLY"`. Thất bại ➔ bắt `Exception` và hiện nguyên nhân lỗi.
2. **`Form 2` (`btnSangForm2_Click`)**:
   - Tạo `Form2 form2 = new Form2();`
   - Gắn `form2.FormClosed += (s, args) => this.Show();` để khi đóng `Form2` sẽ hiện lại chính `Form1` cũ.
   - Gọi `form2.Show();` và `this.Hide();`.
3. **`Thoát` (`btnThoat_Click`)**: gọi `Application.Exit();` để đóng hẳn chương trình.

### Bước 5: Truy vấn có Parameter trên `Form2` (`KẾT QUẢ`)

Khi vừa mở `Form2` (`Form2_Load`), hàm `TaiDuLieuNguoi("")` tải toàn bộ bảng `NGUOI` lên `DataGridView`.
Khi nhập địa chỉ (ví dụ `HANOI`) vào ô `txtDiaChi` và bấm **`Tìm kiếm`**:

```csharp
danhSach = db.Nguois
    .Where(nguoi => nguoi.Dchi == diaChi)
    .ToList();
```

* EF Core dịch câu lệnh C# trên thành câu SQL có **Parameter** (minh họa mục `2.3` và `3.2 Có PARAMETER` trong vở):
  ```sql
  SELECT [n].[ten], [n].[dchi], [n].[tuoi]
  FROM [NGUOI] AS [n]
  WHERE [n].[dchi] = @__diaChi_0
  ```
* Việc dùng tham số `@__diaChi_0` thay vì nối chuỗi trực tiếp giúp tránh lỗi cú pháp khi nhập ký tự đặc biệt và phòng chống **SQL Injection**.

---

## 4. Đọc thêm: Cách Thêm / Sửa / Xóa bằng `QuanLyDbContext`

Khi đã nắm vững luồng kết nối và tìm kiếm ở trên, bạn có thể mở rộng thêm thao tác ghi dữ liệu vào SQL Server với 3 hàm `Add`, `Find`, `Remove` và `SaveChanges()`:

```csharp
using var db = new QuanLyDbContext();

// 1. Thêm mới 1 người
db.Nguois.Add(new Nguoi { Ten = "Nguyen Van B", Tuoi = 20, Dchi = "HANOI" });
db.SaveChanges();

// 2. Cập nhật theo khóa chính (ten)
var nguoi = db.Nguois.Find("Nguyen Van B");
if (nguoi != null) { nguoi.Tuoi = 21; db.SaveChanges(); }

// 3. Xóa theo khóa chính (ten)
var canXoa = db.Nguois.Find("Nguyen Van B");
if (canXoa != null) { db.Nguois.Remove(canXoa); db.SaveChanges(); }
```

---

## 5. Chạy thử & Khắc phục lỗi thường gặp

* **Chạy bằng Visual Studio**: Mở `DBDemoWinFormApp/DBDemoWinFormApp.slnx` và nhấn **F5**.
* **Chạy bằng CLI**:
  ```powershell
  dotnet run --project DBDemoWinFormApp/DBDemoWinFormApp/DBDemoWinFormApp.csproj
  ```
* **Lỗi `A network-related or instance-specific error occurred...`**:
  - Kiểm tra trong Windows Services (`services.msc`) xem dịch vụ **SQL Server (SQLEXPRESS)** đã `Running` chưa.
  - Nếu máy bạn cài SQL Server bản mặc định (không phải Express), hãy đổi `Server=.\SQLEXPRESS` trong `Data/QuanLyDbContext.cs` thành `Server=.` hoặc `Server=localhost`.
* **Lỗi `Cannot open database "QUANLY"`**: Bạn chưa chạy file `database/init_quanly.sql` trong SSMS.
