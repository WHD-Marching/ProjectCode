USE [master]
GO
/****** Object:  Database [WarehouseDB]    Script Date: 2024/6/2 15:57:03 ******/
CREATE DATABASE [WarehouseDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'WarehouseDB', FILENAME = N'D:\Sql Sever\xxx\cangchu\WarehouseDB.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'WarehouseDB_log', FILENAME = N'D:\Sql Sever\xxx\cangchu\WarehouseDB_log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [WarehouseDB] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [WarehouseDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [WarehouseDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [WarehouseDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [WarehouseDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [WarehouseDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [WarehouseDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [WarehouseDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [WarehouseDB] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [WarehouseDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [WarehouseDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [WarehouseDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [WarehouseDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [WarehouseDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [WarehouseDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [WarehouseDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [WarehouseDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [WarehouseDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [WarehouseDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [WarehouseDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [WarehouseDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [WarehouseDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [WarehouseDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [WarehouseDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [WarehouseDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [WarehouseDB] SET RECOVERY FULL 
GO
ALTER DATABASE [WarehouseDB] SET  MULTI_USER 
GO
ALTER DATABASE [WarehouseDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [WarehouseDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [WarehouseDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [WarehouseDB] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
EXEC sys.sp_db_vardecimal_storage_format N'WarehouseDB', N'ON'
GO
USE [WarehouseDB]
GO
/****** Object:  Table [dbo].[Cargo]    Script Date: 2024/6/2 15:57:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Cargo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Tag] [varchar](250) NULL,
	[InsertDate] [datetime] NULL,
 CONSTRAINT [PK_CargoType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 2024/6/2 15:57:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Company] [nvarchar](250) NULL,
	[Telephone] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[Tag] [nvarchar](250) NULL,
	[InsertDate] [datetime] NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Instore]    Script Date: 2024/6/2 15:57:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Instore](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[InstoreOrderId] [int] NOT NULL,
	[CargoId] [int] NOT NULL,
	[LocationId] [int] NOT NULL,
	[Number] [int] NOT NULL,
	[Price] [float] NOT NULL,
	[Tag] [nvarchar](250) NULL,
	[InsertDate] [datetime] NULL,
 CONSTRAINT [PK_Instore] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[InstoreOrder]    Script Date: 2024/6/2 15:57:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InstoreOrder](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[WarehouseId] [int] NOT NULL,
	[SupplierId] [int] NOT NULL,
	[MemberId] [int] NOT NULL,
	[InsertDate] [datetime] NULL,
 CONSTRAINT [PK_InstoreList] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Location]    Script Date: 2024/6/2 15:57:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Location](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[WarehouseId] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[InsertDate] [datetime] NULL,
 CONSTRAINT [PK_Location] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Member]    Script Date: 2024/6/2 15:57:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Member](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Password] [varchar](50) NOT NULL,
	[Role] [int] NOT NULL,
	[InsertDate] [datetime] NULL,
 CONSTRAINT [PK_Member] PRIMARY KEY CLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Outstore]    Script Date: 2024/6/2 15:57:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Outstore](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OutstoreOrderId] [int] NOT NULL,
	[CargoId] [int] NOT NULL,
	[LocationId] [int] NOT NULL,
	[Number] [int] NOT NULL,
	[Price] [float] NOT NULL,
	[Tag] [nvarchar](250) NULL,
	[InsertDate] [datetime] NULL,
 CONSTRAINT [PK_Outstore] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[OutstoreOrder]    Script Date: 2024/6/2 15:57:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OutstoreOrder](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[WarehouseId] [int] NOT NULL,
	[CustmoerId] [int] NOT NULL,
	[MemberId] [int] NOT NULL,
	[InsertDate] [datetime] NULL,
 CONSTRAINT [PK_OutstoreOrder] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Supplier]    Script Date: 2024/6/2 15:57:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Supplier](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Company] [nvarchar](50) NULL,
	[Telephone] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[Tag] [nvarchar](50) NULL,
	[InsertDate] [datetime] NULL,
 CONSTRAINT [PK_Supplier] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Warehouse]    Script Date: 2024/6/2 15:57:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Warehouse](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Telephone] [nvarchar](50) NULL,
	[InsertDate] [datetime] NULL,
	[Tag] [nvarchar](50) NULL,
 CONSTRAINT [PK_Warehouse] PRIMARY KEY CLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Instore] ADD  CONSTRAINT [DF_Instore_InstoreListId]  DEFAULT ((0)) FOR [InstoreOrderId]
GO
ALTER TABLE [dbo].[Instore] ADD  CONSTRAINT [DF_Instore_CargoId]  DEFAULT ((0)) FOR [CargoId]
GO
ALTER TABLE [dbo].[Instore] ADD  CONSTRAINT [DF_Instore_LocationId]  DEFAULT ((0)) FOR [LocationId]
GO
ALTER TABLE [dbo].[Instore] ADD  CONSTRAINT [DF_Instore_Number]  DEFAULT ((0)) FOR [Number]
GO
ALTER TABLE [dbo].[Instore] ADD  CONSTRAINT [DF_Instore_Price]  DEFAULT ((0)) FOR [Price]
GO
ALTER TABLE [dbo].[InstoreOrder] ADD  CONSTRAINT [DF_InstoreList_WarehouseId]  DEFAULT ((0)) FOR [WarehouseId]
GO
ALTER TABLE [dbo].[Location] ADD  CONSTRAINT [DF_Location_WarehouseId]  DEFAULT ((0)) FOR [WarehouseId]
GO
ALTER TABLE [dbo].[Member] ADD  CONSTRAINT [DF_Member_Role]  DEFAULT ((1)) FOR [Role]
GO
ALTER TABLE [dbo].[Outstore] ADD  CONSTRAINT [DF_Outstore_OutstoreOrderId]  DEFAULT ((0)) FOR [OutstoreOrderId]
GO
ALTER TABLE [dbo].[Outstore] ADD  CONSTRAINT [DF_Outstore_CargoId]  DEFAULT ((0)) FOR [CargoId]
GO
ALTER TABLE [dbo].[Outstore] ADD  CONSTRAINT [DF_Outstore_LocationId]  DEFAULT ((0)) FOR [LocationId]
GO
ALTER TABLE [dbo].[Outstore] ADD  CONSTRAINT [DF_Outstore_Number]  DEFAULT ((0)) FOR [Number]
GO
ALTER TABLE [dbo].[Outstore] ADD  CONSTRAINT [DF_Outstore_Price]  DEFAULT ((0)) FOR [Price]
GO
ALTER TABLE [dbo].[OutstoreOrder] ADD  CONSTRAINT [DF_OutstoreOrder_WarehouseId]  DEFAULT ((0)) FOR [WarehouseId]
GO
ALTER TABLE [dbo].[OutstoreOrder] ADD  CONSTRAINT [DF_OutstoreOrder_CustmoerId]  DEFAULT ((0)) FOR [CustmoerId]
GO
ALTER TABLE [dbo].[OutstoreOrder] ADD  CONSTRAINT [DF_OutstoreOrder_MemberId]  DEFAULT ((0)) FOR [MemberId]
GO
USE [master]
GO
ALTER DATABASE [WarehouseDB] SET  READ_WRITE 
GO
