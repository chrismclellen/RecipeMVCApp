# RecipeMVCApp
ASP.NET C# MVC 




sql script for DB________________________________________

USE [master]
GO
/****** Object:  Database [MVCRecipes]    Script Date: 11/21/2020 12:21:03 AM ******/
CREATE DATABASE [MVCRecipes]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'MVCRecipes', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\MVCRecipes.mdf' , SIZE = 4096KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'MVCRecipes_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\MVCRecipes_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [MVCRecipes] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MVCRecipes].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [MVCRecipes] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [MVCRecipes] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [MVCRecipes] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [MVCRecipes] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [MVCRecipes] SET ARITHABORT OFF 
GO
ALTER DATABASE [MVCRecipes] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [MVCRecipes] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [MVCRecipes] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [MVCRecipes] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [MVCRecipes] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [MVCRecipes] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [MVCRecipes] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [MVCRecipes] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [MVCRecipes] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [MVCRecipes] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [MVCRecipes] SET  DISABLE_BROKER 
GO
ALTER DATABASE [MVCRecipes] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [MVCRecipes] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [MVCRecipes] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [MVCRecipes] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [MVCRecipes] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [MVCRecipes] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [MVCRecipes] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [MVCRecipes] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [MVCRecipes] SET  MULTI_USER 
GO
ALTER DATABASE [MVCRecipes] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [MVCRecipes] SET DB_CHAINING OFF 
GO
ALTER DATABASE [MVCRecipes] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [MVCRecipes] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [MVCRecipes]
GO
/****** Object:  Table [dbo].[Ingredients]    Script Date: 11/21/2020 12:21:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Ingredients](
	[IngredientID] [int] IDENTITY(1,1) NOT NULL,
	[RecipeID] [int] NOT NULL,
	[IngredientName] [varchar](50) NULL,
	[Quantity] [int] NULL,
	[UOM] [varchar](50) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Recipes]    Script Date: 11/21/2020 12:21:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Recipes](
	[RecipeID] [int] IDENTITY(1,1) NOT NULL,
	[RecipeName] [varchar](50) NULL,
	[RecipeInstructions] [varchar](2056) NULL,
 CONSTRAINT [PK_Recipes] PRIMARY KEY CLUSTERED 
(
	[RecipeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
USE [master]
GO
ALTER DATABASE [MVCRecipes] SET  READ_WRITE 
GO

