Use master
	IF OBJECT_ID('manhphph01079_QuanLyBanHang') is null
	Drop database manhphph01079_QuanLyBanHang
GO
	Create Database manhphph01079_QuanLyBanHang
GO
	Use manhphph01079_QuanLyBanHang
GO

CREATE TABLE Chi_Tiet_Hoa_Don (
    Ma_HD nvarchar(10)  NOT NULL,
    Ma_SP nvarchar(10)  NOT NULL,
    So_Luong int  NOT NULL,
	CONSTRAINT Chi_Tiet_Hoa_Don_pk PRIMARY KEY  (Ma_HD,Ma_SP)
)
;





-- Table: Hoa_Don
CREATE TABLE Hoa_Don (
    Ma_HD nvarchar(10)  NOT NULL,
    Ngay_ban smalldatetime  NOT NULL,
    Ma_KH nvarchar(10)  NOT NULL,
    CONSTRAINT Hoa_Don_pk PRIMARY KEY  (Ma_HD)
)
;





-- Table: Khach_Hang
CREATE TABLE Khach_Hang (
    Ma_KH nvarchar(10)  NOT NULL,
    Ten_KH nvarchar(39)  NOT NULL,
    Email varchar(50),
    Gioi_Tinh nvarchar(15)  NOT NULL,
    CONSTRAINT Khach_Hang_pk PRIMARY KEY  (Ma_KH)
)
;





-- Table: Loai_San_Pham
CREATE TABLE Loai_San_Pham (
    Ma_Loai_SP nvarchar(10)  NOT NULL,
    Ten_Loai_SP nvarchar(30)  NOT NULL,
    Ghi_Chu nvarchar(200)  NULL,
    CONSTRAINT Loai_San_Pham_pk PRIMARY KEY  (Ma_Loai_SP)
)
;





-- Table: San_Pham
CREATE TABLE San_Pham (
    Ma_SP nvarchar(10)  NOT NULL,
    Ten_SP nvarchar(50)  NOT NULL,
    Ma_Loai_SP nvarchar(10)  NOT NULL,
    Don_Gia money  NOT NULL,
    CONSTRAINT San_Pham_pk PRIMARY KEY  (Ma_SP)
)
;

Create table Tai_Khoan(
Ma_TaiKhoan nvarchar(30) not null Primary key,
MatKhau varchar(20) not null,
)







-- foreign keys
-- Reference:  Chi_Tiet_Hoa_Don_Hoa_Don (table: Chi_Tiet_Hoa_Don)


ALTER TABLE Chi_Tiet_Hoa_Don ADD CONSTRAINT Chi_Tiet_Hoa_Don_Hoa_Don 
    FOREIGN KEY (Ma_HD)
    REFERENCES Hoa_Don (Ma_HD)
;

-- Reference:  Chi_Tiet_Hoa_Don_San_Pham (table: Chi_Tiet_Hoa_Don)


ALTER TABLE Chi_Tiet_Hoa_Don ADD CONSTRAINT Chi_Tiet_Hoa_Don_San_Pham 
    FOREIGN KEY (Ma_SP)
    REFERENCES San_Pham (Ma_SP)
;

-- Reference:  Hoa_Don_Khach_Hang (table: Hoa_Don)


ALTER TABLE Hoa_Don ADD CONSTRAINT Hoa_Don_Khach_Hang 
    FOREIGN KEY (Ma_KH)
    REFERENCES Khach_Hang (Ma_KH)
;

-- Reference:  San_Pham_Loai_San_Pham (table: San_Pham)


ALTER TABLE San_Pham ADD CONSTRAINT San_Pham_Loai_San_Pham 
    FOREIGN KEY (Ma_Loai_SP)
    REFERENCES Loai_San_Pham (Ma_Loai_SP)
;



Insert into Tai_Khoan Values ('admin','admin')
Go

Insert Into Khach_Hang Values ('KH01',N'Phạm Hoàng Mạnh','','Nam')
Insert Into Khach_Hang Values ('KH02',N'Phạm Hoàng Nam','','Nam')
Insert Into Khach_Hang Values ('KH03',N'Nguyễn Hoài Nam','','Nữ')
Insert Into Khach_Hang Values ('KH04',N'Trần Đại Ngĩa','','Nam')
Insert Into Khach_Hang Values ('KH05',N'Phan Văn Tùng','','Nam')
Go

Insert Into Loai_San_Pham Values ('L01',N'Bánh Kẹo','')
Insert Into Loai_San_Pham Values ('L02',N'Hoa Quả','')
Insert Into Loai_San_Pham Values ('L03',N'Nước Uống','')
Insert Into Loai_San_Pham Values ('L04',N'Văn Phòng Phẩm','')
Insert Into Loai_San_Pham Values ('L05',N'Gia Dụng','')
Go

Insert Into San_Pham Values ('SP01',N'Kẹo ChipChip','L01','10000.0000')
Insert Into San_Pham Values ('SP02',N'Táo','L02','15000')
Insert Into San_Pham Values ('SP03',N'Coca Chai To','L03','20000')
Insert Into San_Pham Values ('SP04',N'Bút Bi','L04','3000')
Insert Into San_Pham Values ('SP05',N'Nồi cơm điện SHARP KSH-219V','L05','450000')
Go

Insert Into Hoa_Don Values ('HD01','10/12/2014','KH01')
Insert Into Hoa_Don Values ('HD02','10/13/2014','KH02')
Insert Into Hoa_Don Values ('HD03','10/14/2014','KH03')
Insert Into Hoa_Don Values ('HD04','10/15/2014','KH04')
Insert Into Hoa_Don Values ('HD05','10/16/2014','KH05')
Go

Insert Into Chi_Tiet_Hoa_Don Values ('HD01','SP01','10')
Insert Into Chi_Tiet_Hoa_Don Values ('HD02','SP02','10')
Insert Into Chi_Tiet_Hoa_Don Values ('HD03','SP03','15')
Insert Into Chi_Tiet_Hoa_Don Values ('HD04','SP04','20')
Insert Into Chi_Tiet_Hoa_Don Values ('HD05','SP05','1')
Insert Into Chi_Tiet_Hoa_Don Values ('HD05','SP01','10')
Go


