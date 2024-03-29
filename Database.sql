USE [master]
GO
/****** Object:  Database [ScanMachine]    Script Date: 23/01/2024 12:37:21 CH ******/
CREATE DATABASE [ScanMachine]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ScanMachine', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\ScanMachine.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ScanMachine_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\ScanMachine_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [ScanMachine] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ScanMachine].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ScanMachine] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ScanMachine] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ScanMachine] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ScanMachine] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ScanMachine] SET ARITHABORT OFF 
GO
ALTER DATABASE [ScanMachine] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ScanMachine] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ScanMachine] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ScanMachine] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ScanMachine] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ScanMachine] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ScanMachine] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ScanMachine] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ScanMachine] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ScanMachine] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ScanMachine] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ScanMachine] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ScanMachine] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ScanMachine] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ScanMachine] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ScanMachine] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ScanMachine] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ScanMachine] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [ScanMachine] SET  MULTI_USER 
GO
ALTER DATABASE [ScanMachine] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ScanMachine] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ScanMachine] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ScanMachine] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ScanMachine] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [ScanMachine] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [ScanMachine] SET QUERY_STORE = ON
GO
ALTER DATABASE [ScanMachine] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [ScanMachine]
GO
/****** Object:  Table [dbo].[Brand]    Script Date: 23/01/2024 12:37:21 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Brand](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NameLogo] [nvarchar](max) NOT NULL,
	[Address] [nvarchar](max) NOT NULL,
	[UserID] [int] NOT NULL,
	[Status] [int] NOT NULL,
	[Code] [nvarchar](max) NULL,
	[CreationDate] [datetime] NULL,
	[ModificationDate] [datetime] NULL,
	[ModificationBy] [nvarchar](max) NULL,
	[DeletionDate] [datetime] NULL,
	[DeleteBy] [nvarchar](max) NULL,
	[IsDeleted] [nvarchar](max) NULL,
 CONSTRAINT [PK_Brand] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 23/01/2024 12:37:21 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NOT NULL,
	[BrandID] [int] NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Status] [int] NOT NULL,
	[Code] [nvarchar](max) NULL,
	[CreationDate] [datetime] NULL,
	[ModificationDate] [datetime] NULL,
	[ModificationBy] [nvarchar](max) NULL,
	[DeletionDate] [datetime] NULL,
	[DeleteBy] [nvarchar](max) NULL,
	[IsDeleted] [nvarchar](max) NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Image]    Script Date: 23/01/2024 12:37:21 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Image](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Status] [int] NOT NULL,
	[Code] [nvarchar](max) NULL,
	[CreationDate] [datetime] NULL,
	[ModificationDate] [datetime] NULL,
	[ModificationBy] [nvarchar](max) NULL,
	[DeletionDate] [datetime] NULL,
	[DeleteBy] [nvarchar](max) NULL,
	[IsDeleted] [nvarchar](max) NULL,
 CONSTRAINT [PK_Image] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Machine]    Script Date: 23/01/2024 12:37:21 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Machine](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[StoreID] [int] NOT NULL,
	[Code] [nvarchar](max) NULL,
	[CreateDate] [datetime] NULL,
	[ModificationDate] [datetime] NULL,
 CONSTRAINT [PK_Machine] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order]    Script Date: 23/01/2024 12:37:21 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MachineID] [int] NOT NULL,
	[StoreID] [int] NOT NULL,
	[OrderDetailsID] [int] NOT NULL,
	[OrderImageID] [int] NOT NULL,
	[Status] [int] NOT NULL,
	[TotalPrice] [float] NOT NULL,
	[Code] [nvarchar](max) NULL,
	[CreationDate] [datetime] NULL,
	[ModificationDate] [datetime] NULL,
	[ModificationBy] [nvarchar](max) NULL,
	[DeletionDate] [datetime] NULL,
	[DeleteBy] [nvarchar](max) NULL,
	[IsDeleted] [nvarchar](max) NULL,
 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderDetails]    Script Date: 23/01/2024 12:37:21 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetails](
	[Id] [int] NOT NULL,
	[OrderID] [int] NOT NULL,
	[ProductID] [int] IDENTITY(1,1) NOT NULL,
	[Quantity] [int] NOT NULL,
	[Price] [float] NOT NULL,
	[Status] [int] NOT NULL,
	[Code] [nvarchar](max) NULL,
	[CreationDate] [datetime] NULL,
	[ModificationDate] [datetime] NULL,
	[ModificationBy] [nvarchar](max) NULL,
	[DeletionDate] [datetime] NULL,
	[DeleteBy] [nvarchar](max) NULL,
	[IsDeleted] [nvarchar](max) NULL,
 CONSTRAINT [PK_OrderDetails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderImage]    Script Date: 23/01/2024 12:37:21 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderImage](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ImageDetailsID] [int] NOT NULL,
	[Code] [nvarchar](max) NULL,
	[CreationDate] [datetime] NULL,
	[ModificationDate] [datetime] NULL,
	[ModificationBy] [nvarchar](max) NULL,
	[DeletionDate] [datetime] NULL,
	[DeleteBy] [nvarchar](max) NULL,
	[IsDeleted] [nvarchar](max) NULL,
 CONSTRAINT [PK_OrderImage] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 23/01/2024 12:37:21 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProductName] [nvarchar](max) NOT NULL,
	[Price] [float] NOT NULL,
	[Quantity] [int] NOT NULL,
	[Discounts] [float] NULL,
	[Title] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[Status] [int] NOT NULL,
	[CategoryID] [int] NOT NULL,
	[ImageID] [int] NOT NULL,
	[Code] [nvarchar](max) NULL,
	[CreationDate] [datetime] NULL,
	[ModificationDate] [datetime] NULL,
	[ModificationBy] [nvarchar](max) NULL,
	[DeletionDate] [datetime] NULL,
	[DeleteBy] [nvarchar](max) NULL,
	[IsDeleted] [nvarchar](max) NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ShopStore]    Script Date: 23/01/2024 12:37:21 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ShopStore](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StoreName] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Address] [nvarchar](max) NOT NULL,
	[Status] [int] NOT NULL,
	[BrandID] [int] NOT NULL,
	[Code] [nvarchar](max) NULL,
	[CreationDate] [datetime] NULL,
	[ModificationDate] [datetime] NULL,
	[ModificationBy] [nvarchar](max) NULL,
	[DeletionDate] [datetime] NULL,
	[DeleteBy] [nvarchar](max) NULL,
	[IsDeleted] [nvarchar](max) NULL,
 CONSTRAINT [PK_ShopStore] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 23/01/2024 12:37:21 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FullName] [nvarchar](max) NOT NULL,
	[PhoneNumber] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[Address] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Status] [int] NOT NULL,
	[Code] [nvarchar](max) NULL,
	[CreationDate] [datetime] NULL,
	[ModificationDate] [datetime] NULL,
	[ModificationBy] [nvarchar](max) NULL,
	[IsDeleted] [nvarchar](max) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Brand]  WITH CHECK ADD  CONSTRAINT [FK_Brand_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[Brand] CHECK CONSTRAINT [FK_Brand_User]
GO
ALTER TABLE [dbo].[Category]  WITH CHECK ADD  CONSTRAINT [FK_Category_Brand] FOREIGN KEY([BrandID])
REFERENCES [dbo].[Brand] ([Id])
GO
ALTER TABLE [dbo].[Category] CHECK CONSTRAINT [FK_Category_Brand]
GO
ALTER TABLE [dbo].[Machine]  WITH CHECK ADD  CONSTRAINT [FK_Machine_ShopStore] FOREIGN KEY([StoreID])
REFERENCES [dbo].[ShopStore] ([Id])
GO
ALTER TABLE [dbo].[Machine] CHECK CONSTRAINT [FK_Machine_ShopStore]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_Machine] FOREIGN KEY([MachineID])
REFERENCES [dbo].[Machine] ([ID])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_Machine]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_OrderDetails] FOREIGN KEY([OrderDetailsID])
REFERENCES [dbo].[OrderDetails] ([Id])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_OrderDetails]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_OrderImage] FOREIGN KEY([OrderImageID])
REFERENCES [dbo].[OrderImage] ([Id])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_OrderImage]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_ShopStore] FOREIGN KEY([StoreID])
REFERENCES [dbo].[ShopStore] ([Id])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_ShopStore]
GO
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetails_Order] FOREIGN KEY([OrderID])
REFERENCES [dbo].[Order] ([Id])
GO
ALTER TABLE [dbo].[OrderDetails] CHECK CONSTRAINT [FK_OrderDetails_Order]
GO
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetails_Product] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Product] ([Id])
GO
ALTER TABLE [dbo].[OrderDetails] CHECK CONSTRAINT [FK_OrderDetails_Product]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Category] FOREIGN KEY([CategoryID])
REFERENCES [dbo].[Category] ([Id])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Category]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Image] FOREIGN KEY([ImageID])
REFERENCES [dbo].[Image] ([Id])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Image]
GO
ALTER TABLE [dbo].[ShopStore]  WITH CHECK ADD  CONSTRAINT [FK_ShopStore_Brand] FOREIGN KEY([BrandID])
REFERENCES [dbo].[Brand] ([Id])
GO
ALTER TABLE [dbo].[ShopStore] CHECK CONSTRAINT [FK_ShopStore_Brand]
GO
USE [master]
GO
ALTER DATABASE [ScanMachine] SET  READ_WRITE 
GO
