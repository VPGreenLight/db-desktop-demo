-- =============================================================
-- Script khởi tạo CSDL QUANLY cho bài tập WinForms + EF Core
-- Dựa theo tài liệu hướng dẫn và bài tập:
--   QUANLY = { NGUOI(ten, tuoi, dchi), DIEM(hoten, toan, van) }
-- =============================================================

IF DB_ID(N'QUANLY') IS NULL
BEGIN
    CREATE DATABASE QUANLY;
END
GO

USE QUANLY;
GO

-- 1. Tạo bảng NGUOI(ten, tuoi, dchi)
IF OBJECT_ID(N'dbo.NGUOI', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.NGUOI
    (
        ten  NVARCHAR(100) NOT NULL PRIMARY KEY,
        tuoi INT           NOT NULL,
        dchi NVARCHAR(200) NOT NULL
    );
END
GO

-- 2. Tạo bảng DIEM(hoten, toan, van)
IF OBJECT_ID(N'dbo.DIEM', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.DIEM
    (
        hoten NVARCHAR(100) NOT NULL PRIMARY KEY,
        toan  FLOAT         NOT NULL,
        van   FLOAT         NOT NULL
    );
END
GO

-- 3. Dữ liệu mẫu (chỉ thêm nếu bảng chưa có dữ liệu)
IF NOT EXISTS (SELECT 1 FROM dbo.NGUOI)
BEGIN
    INSERT INTO dbo.NGUOI (ten, tuoi, dchi) VALUES
        (N'Nguyen Van An', 20, N'HANOI'),
        (N'Tran Thi Binh', 21, N'HANOI'),
        (N'Le Hoang Cuong', 22, N'HAI PHONG'),
        (N'Pham Minh Duc', 19, N'DA NANG'),
        (N'Hoang Lan Anh', 20, N'HO CHI MINH');
END
GO

IF NOT EXISTS (SELECT 1 FROM dbo.DIEM)
BEGIN
    INSERT INTO dbo.DIEM (hoten, toan, van) VALUES
        (N'Nguyen Van An', 8.5, 7.5),
        (N'Tran Thi Binh', 9.0, 8.5),
        (N'Le Hoang Cuong', 7.0, 6.5),
        (N'Pham Minh Duc', 8.0, 8.0),
        (N'Hoang Lan Anh', 9.5, 9.0);
END
GO

-- Truy vấn kiểm tra mẫu như trong ghi chú bài tập (mục 2.3)
SELECT ten, tuoi, dchi FROM dbo.NGUOI WHERE dchi = N'HANOI';
SELECT hoten, toan, van FROM dbo.DIEM;
GO
