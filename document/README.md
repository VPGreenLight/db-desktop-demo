# Hướng dẫn kết nối WinForms với SQL Server bằng EF Core `DbContext`

Tài liệu này tổng hợp và mở rộng từ [huongdan SQLSERVER udung.pdf](./huongdan%20SQLSERVER%20udung.pdf) và đề bài tập thực hành CSDL:
- **Cơ sở dữ liệu**: `QUANLY` trên SQL Server (`.\SQLEXPRESS`)
- **2 bảng dữ liệu**:
  - `NGUOI(ten, tuoi, dchi)`
  - `DIEM(hoten, toan, van)`
- **Ứng dụng WinForms (`DBDemoWinFormApp`)**:
  - **Form1 (Trang chính)**: Kiểm tra kết nối CSDL, chuyển sang `Form2`, nút Thoát.
  - **Form2**: Chọn bảng (`NGUOI` / `DIEM`), tìm kiếm có tham số (`Parameter`), hiển thị kết quả trên `DataGridView` và thực hiện Thêm / Cập nhật / Xóa (CRUD), nút Quay về `Form1`.

---

## 1. Cấu trúc thư mục dự án

```text
db-desktop-demo/
├── database/
│   └── init_quanly.sql                  # Script tạo DB QUANLY, 2 bảng NGUOI & DIEM và dữ liệu mẫu
├── document/
│   ├── huongdan SQLSERVER udung.pdf     # Tài liệu hướng dẫn gốc
│   └── README.md                        # Tài liệu hướng dẫn này
└── DBDemoWinFormApp/
    ├── DBDemoWinFormApp.slnx            # File Solution
    └── DBDemoWinFormApp/
        ├── DBDemoWinFormApp.csproj      # Khai báo .NET 10 WinForms & NuGet EF Core
        ├── Data/
        │   └── QuanLyDbContext.cs       # Lớp DbContext kết nối WinForms <-> SQL Server
        ├── Models/
        │   ├── Nguoi.cs                 # Model ánh xạ bảng NGUOI
        │   └── Diem.cs                  # Model ánh xạ bảng DIEM
        ├── Form1.cs / Form1.Designer.cs # Trang chính: Test kết nối, sang Form2, Thoát
        ├── Form2.cs / Form2.Designer.cs # Quản lý tìm kiếm & CRUD cho NGUOI / DIEM
        └── Program.cs                   # Điểm khởi chạy ứng dụng (mở Form1)
```

---

## 2. Bước 1 — Tạo cơ sở dữ liệu `QUANLY` trong SQL Server

1. Mở **SQL Server Management Studio (SSMS)** và đăng nhập vào server (mặc định `.\SQLEXPRESS`, Windows Authentication).
2. Mở file `database/init_quanly.sql` và nhấn **Execute (F5)**.

Script sẽ tự động tạo:
- Database `QUANLY`
- Bảng `dbo.NGUOI(ten, tuoi, dchi)` với `ten` là `PRIMARY KEY`
- Bảng `dbo.DIEM(hoten, toan, van)` với `hoten` là `PRIMARY KEY`
- Dữ liệu mẫu (bao gồm các bản ghi có `dchi = N'HANOI'` giống ví dụ truy vấn `SELECT TUOI FROM NGUOI WHERE DCHI = 'HANOI'`).

> **Ghi chú thiết kế khóa chính:** Để giữ nguyên đúng 3 cột như sơ đồ bài tập (`NGUOI(ten, tuoi, dchi)` và `DIEM(hoten, toan, van)`) mà vẫn hỗ trợ EF Core theo dõi cập nhật/xóa (`Find`, `Update`, `Remove`), cột `ten` và `hoten` được đặt làm khóa chính (`[Key]`). Trong dự án thực tế cho phép trùng họ tên, bạn có thể bổ sung thêm cột `id INT IDENTITY(1,1) PRIMARY KEY`.

---

## 3. Bước 2 — Cài đặt thư viện Entity Framework Core cho SQL Server

Project sử dụng gói **`Microsoft.EntityFrameworkCore.SqlServer`** (đã cấu hình sẵn trong `DBDemoWinFormApp.csproj` với phiên bản `10.0.15` tương thích `net10.0-windows`).

Nếu tự dựng lại từ đầu trong Visual Studio:
1. Chuột phải vào project `DBDemoWinFormApp` ➔ **Manage NuGet Packages...**
2. Chọn tab **Browse**, tìm `Microsoft.EntityFrameworkCore.SqlServer`.
3. Chọn phiên bản khớp với phiên bản .NET của project (ví dụ `.NET 10` dùng bản `10.0.x`; `.NET 8` dùng bản `8.0.x`) rồi nhấn **Install**.

Hoặc cài qua dòng lệnh (.NET CLI):

```powershell
dotnet add DBDemoWinFormApp/DBDemoWinFormApp/DBDemoWinFormApp.csproj package Microsoft.EntityFrameworkCore.SqlServer --version 10.0.15
```

> Gói này tự động kéo theo `Microsoft.Data.SqlClient` (driver kết nối SQL Server của tài liệu PDF) cùng toàn bộ nhân EF Core (`DbContext`, `DbSet<T>`, LINQ-to-SQL).

---

## 4. Bước 3 — Dựng Models và `QuanLyDbContext`

### 4.1. Tạo các lớp Entity (`Models/Nguoi.cs` và `Models/Diem.cs`)

Mỗi bảng trong SQL Server tương ứng với một class C#:
- `[Table("NGUOI")]`, `[Column("ten")]`: ánh xạ đúng tên bảng và tên cột trong SQL Server.
- `[Key]`: đánh dấu thuộc tính khóa chính để EF Core thực hiện `Find`, `Update`, `Remove`.
- `[DisplayName("...")]`: giúp `DataGridView` trong WinForms tự hiển thị tiêu đề cột tiếng Việt thân thiện thay vì tên thuộc tính thô.

### 4.2. Tạo lớp `QuanLyDbContext` (`Data/QuanLyDbContext.cs`)

`QuanLyDbContext` kế thừa `DbContext` đóng vai trò cầu nối giữa WinForms và SQL Server:

```csharp
public class QuanLyDbContext : DbContext
{
    public const string DefaultConnectionString =
        @"Server=.\SQLEXPRESS; Database=QUANLY; Integrated Security=True; TrustServerCertificate=True;";

    public DbSet<Nguoi> Nguois => Set<Nguoi>();
    public DbSet<Diem> Diems => Set<Diem>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}
```

**Ý nghĩa chuỗi kết nối (`ConnectionString`)**:
- `Server=.\SQLEXPRESS`: tên SQL Server instance trên máy (nếu dùng bản mặc định thì đổi thành `Server=.` hoặc `Server=localhost`; nếu dùng LocalDB của Visual Studio thì đổi thành `Server=(localdb)\MSSQLLocalDB`).
- `Database=QUANLY`: tên CSDL cần làm việc.
- `Integrated Security=True`: đăng nhập bằng tài khoản Windows hiện tại (Windows Authentication).
- `TrustServerCertificate=True`: bỏ qua lỗi kiểm tra chứng chỉ SSL tự ký trên SQL Server cài cục bộ.

---

## 5. Bước 4 — Kết nối từ `Form1` (Trang chính) và chuyển sang `Form2`

### 5.1. Kiểm tra kết nối bằng `DbContext` (`btnKetNoi_Click`)

Thay vì gọi `SqlConnection` rời rạc trong từng Form, `Form1` tạo `QuanLyDbContext` và gọi `db.Database.OpenConnection()`:

```csharp
using var db = new QuanLyDbContext(connStr);
db.Database.OpenConnection();
db.Database.CloseConnection();
```

- Nếu kết nối thành công: cập nhật `lblTrangThai` và hiện `MessageBox` `"Đã kết nối thành công CSDL QUANLY!"`.
- Nếu thất bại (sai tên Server, chưa bật dịch vụ SQL Server hoặc chưa tạo DB `QUANLY`): khối `catch (Exception ex)` bắt lỗi và hiển thị nguyên nhân chi tiết (`ex.Message`).

### 5.2. Chuyển qua lại giữa `Form1` và `Form2`

- Ở `Form1`: `var form2 = new Form2(this, connStr); form2.Show(); this.Hide();`
- Ở `Form2`: khi bấm `QUAY VỀ FORM1` (`Close()`) hoặc đóng cửa sổ `[X]`, sự kiện `OnFormClosed` tự gọi `_parentForm.Show()` để quay về **chính `Form1` ban đầu** (khắc phục tình trạng tạo nhiều `Form1` ẩn gây kẹt tiến trình khi đóng ứng dụng).
- Nút `THOÁT` trên `Form1` gọi `Application.Exit()`.

---

## 6. Bước 5 — Truy vấn có tham số (Parameter) và CRUD trên `Form2`

Mỗi thao tác trên `Form2` đều mở một `QuanLyDbContext` mới bằng `using var db = CreateDbContext();` (mô hình *Unit-of-Work ngắn hạn*, giải phóng kết nối ngay sau khi xử lý xong).

### 6.1. Tìm kiếm có Parameter (chống SQL Injection)

Khi người dùng nhập từ khóa (ví dụ `HANOI`) vào ô tìm kiếm:
- Chế độ **Tìm chính xác (`=`)**:
  ```csharp
  query = query.Where(n => n.Dchi == term || n.Ten == term);
  ```
- Chế độ **Tìm gần đúng (`Contains`)**:
  ```csharp
  query = query.Where(n => n.Dchi.Contains(term) || n.Ten.Contains(term));
  ```

EF Core sẽ tự động biên dịch biến `term` thành **SQL Parameter** (ví dụ `@__term_0`), đáp ứng yêu cầu *“Có PARAMETER”* trong bài giảng mà không lo lỗi cú pháp dấu nháy hay SQL Injection.

### 6.2. Các thao tác CRUD

| Thao tác | Luồng EF Core trong `Form2.cs` |
|---|---|
| **Đọc (Read)** | `db.Nguois.AsNoTracking().OrderBy(n => n.Ten).ToList()` ➔ gán vào `dgvKetQua.DataSource` |
| **Thêm (Create)** | Kiểm tra trùng khóa ➔ `db.Nguois.Add(input)` ➔ `db.SaveChanges()` |
| **Cập nhật (Update)** | `var existing = db.Nguois.Find(ten)` ➔ sửa `Tuoi`, `Dchi` ➔ `db.SaveChanges()` |
| **Xóa (Delete)** | `var existing = db.Nguois.Find(ten)` ➔ `db.Nguois.Remove(existing)` ➔ `db.SaveChanges()` |

---

## 7. Chạy thử & Khắc phục lỗi thường gặp

### Chạy ứng dụng
- **Trong Visual Studio**: Mở `DBDemoWinFormApp/DBDemoWinFormApp.slnx` và nhấn **F5**.
- **Bằng .NET CLI**:
  ```powershell
  dotnet run --project DBDemoWinFormApp/DBDemoWinFormApp/DBDemoWinFormApp.csproj
  ```

### Các lỗi kết nối phổ biến
1. **`A network-related or instance-specific error occurred...` (Error 26 / 40)**:
   - Kiểm tra Windows Services (`services.msc`) xem **SQL Server (SQLEXPRESS)** đã ở trạng thái `Running` chưa.
   - Nếu máy cài SQL Server bản mặc định (không phải Express), hãy sửa `Server=.\SQLEXPRESS` trên giao diện `Form1` (hoặc trong `QuanLyDbContext.DefaultConnectionString`) thành `Server=.` hoặc `Server=localhost`.
2. **`Cannot open database "QUANLY" requested by the login`**:
   - Bạn chưa tạo DB `QUANLY`. Hãy chạy file `database/init_quanly.sql` trong SSMS.
3. **`Login failed for user...`**:
   - Đảm bảo tài khoản Windows đang dùng có quyền truy cập vào SQL Server Express (hoặc cấu hình SQL Authentication `User Id=...;Password=...;` thay cho `Integrated Security=True`).
