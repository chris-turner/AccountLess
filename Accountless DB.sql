USE [master]
GO
/****** Object:  Database [AccountLess]    Script Date: 5/24/2019 4:20:09 PM ******/
CREATE DATABASE [AccountLess]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'AccountLess', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\AccountLess.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'AccountLess_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\AccountLess_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [AccountLess] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [AccountLess].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [AccountLess] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [AccountLess] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [AccountLess] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [AccountLess] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [AccountLess] SET ARITHABORT OFF 
GO
ALTER DATABASE [AccountLess] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [AccountLess] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [AccountLess] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [AccountLess] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [AccountLess] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [AccountLess] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [AccountLess] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [AccountLess] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [AccountLess] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [AccountLess] SET  DISABLE_BROKER 
GO
ALTER DATABASE [AccountLess] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [AccountLess] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [AccountLess] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [AccountLess] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [AccountLess] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [AccountLess] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [AccountLess] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [AccountLess] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [AccountLess] SET  MULTI_USER 
GO
ALTER DATABASE [AccountLess] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [AccountLess] SET DB_CHAINING OFF 
GO
ALTER DATABASE [AccountLess] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [AccountLess] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [AccountLess] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [AccountLess] SET QUERY_STORE = OFF
GO
USE [AccountLess]
GO
ALTER DATABASE SCOPED CONFIGURATION SET IDENTITY_CACHE = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [AccountLess]
GO
/****** Object:  Table [dbo].[Reddit]    Script Date: 5/24/2019 4:20:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reddit](
	[UserID] [uniqueidentifier] NOT NULL,
	[Subreddit] [varchar](300) NOT NULL,
 CONSTRAINT [PK_Reddit] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC,
	[Subreddit] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 5/24/2019 4:20:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserID] [uniqueidentifier] NOT NULL,
	[Username] [varchar](25) NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[YouTube]    Script Date: 5/24/2019 4:20:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[YouTube](
	[ChannelID] [varchar](150) NOT NULL,
	[UserID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_YouTube] PRIMARY KEY CLUSTERED 
(
	[ChannelID] ASC,
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Reddit] ([UserID], [Subreddit]) VALUES (N'184f82c9-f5ae-41e4-8121-0b938b3b3de3', N'cscareerquestions')
INSERT [dbo].[Reddit] ([UserID], [Subreddit]) VALUES (N'184f82c9-f5ae-41e4-8121-0b938b3b3de3', N'learnprogramming')
INSERT [dbo].[Reddit] ([UserID], [Subreddit]) VALUES (N'227126ef-e405-4302-aa80-82773149da1d', N'android')
INSERT [dbo].[Reddit] ([UserID], [Subreddit]) VALUES (N'227126ef-e405-4302-aa80-82773149da1d', N'androidgaming')
INSERT [dbo].[Reddit] ([UserID], [Subreddit]) VALUES (N'227126ef-e405-4302-aa80-82773149da1d', N'announcements')
INSERT [dbo].[Reddit] ([UserID], [Subreddit]) VALUES (N'227126ef-e405-4302-aa80-82773149da1d', N'battlestations')
INSERT [dbo].[Reddit] ([UserID], [Subreddit]) VALUES (N'227126ef-e405-4302-aa80-82773149da1d', N'bitcoin')
INSERT [dbo].[Reddit] ([UserID], [Subreddit]) VALUES (N'227126ef-e405-4302-aa80-82773149da1d', N'buildapc')
INSERT [dbo].[Reddit] ([UserID], [Subreddit]) VALUES (N'227126ef-e405-4302-aa80-82773149da1d', N'cheap_meals')
INSERT [dbo].[Reddit] ([UserID], [Subreddit]) VALUES (N'227126ef-e405-4302-aa80-82773149da1d', N'collegebasketball')
INSERT [dbo].[Reddit] ([UserID], [Subreddit]) VALUES (N'227126ef-e405-4302-aa80-82773149da1d', N'compsci')
INSERT [dbo].[Reddit] ([UserID], [Subreddit]) VALUES (N'227126ef-e405-4302-aa80-82773149da1d', N'computers')
INSERT [dbo].[Reddit] ([UserID], [Subreddit]) VALUES (N'227126ef-e405-4302-aa80-82773149da1d', N'cookingforbeginners')
INSERT [dbo].[Reddit] ([UserID], [Subreddit]) VALUES (N'227126ef-e405-4302-aa80-82773149da1d', N'coolgithubprojects')
INSERT [dbo].[Reddit] ([UserID], [Subreddit]) VALUES (N'227126ef-e405-4302-aa80-82773149da1d', N'cordcutters')
INSERT [dbo].[Reddit] ([UserID], [Subreddit]) VALUES (N'227126ef-e405-4302-aa80-82773149da1d', N'cscareerquestions')
INSERT [dbo].[Reddit] ([UserID], [Subreddit]) VALUES (N'227126ef-e405-4302-aa80-82773149da1d', N'csharp')
INSERT [dbo].[Reddit] ([UserID], [Subreddit]) VALUES (N'227126ef-e405-4302-aa80-82773149da1d', N'documentaries')
INSERT [dbo].[Reddit] ([UserID], [Subreddit]) VALUES (N'227126ef-e405-4302-aa80-82773149da1d', N'dotnet')
INSERT [dbo].[Reddit] ([UserID], [Subreddit]) VALUES (N'227126ef-e405-4302-aa80-82773149da1d', N'ea_nhl')
INSERT [dbo].[Reddit] ([UserID], [Subreddit]) VALUES (N'227126ef-e405-4302-aa80-82773149da1d', N'eagles')
INSERT [dbo].[Reddit] ([UserID], [Subreddit]) VALUES (N'227126ef-e405-4302-aa80-82773149da1d', N'fantasyfootball')
INSERT [dbo].[Reddit] ([UserID], [Subreddit]) VALUES (N'227126ef-e405-4302-aa80-82773149da1d', N'flyers')
INSERT [dbo].[Reddit] ([UserID], [Subreddit]) VALUES (N'227126ef-e405-4302-aa80-82773149da1d', N'fossdroid')
INSERT [dbo].[Reddit] ([UserID], [Subreddit]) VALUES (N'227126ef-e405-4302-aa80-82773149da1d', N'frugal')
INSERT [dbo].[Reddit] ([UserID], [Subreddit]) VALUES (N'227126ef-e405-4302-aa80-82773149da1d', N'gaming')
INSERT [dbo].[Reddit] ([UserID], [Subreddit]) VALUES (N'227126ef-e405-4302-aa80-82773149da1d', N'golf')
INSERT [dbo].[Reddit] ([UserID], [Subreddit]) VALUES (N'227126ef-e405-4302-aa80-82773149da1d', N'hacking')
INSERT [dbo].[Reddit] ([UserID], [Subreddit]) VALUES (N'227126ef-e405-4302-aa80-82773149da1d', N'hockey')
INSERT [dbo].[Reddit] ([UserID], [Subreddit]) VALUES (N'227126ef-e405-4302-aa80-82773149da1d', N'internetisbeautiful')
INSERT [dbo].[Reddit] ([UserID], [Subreddit]) VALUES (N'227126ef-e405-4302-aa80-82773149da1d', N'itcareerquestions')
INSERT [dbo].[Reddit] ([UserID], [Subreddit]) VALUES (N'227126ef-e405-4302-aa80-82773149da1d', N'jeopardy')
INSERT [dbo].[Reddit] ([UserID], [Subreddit]) VALUES (N'227126ef-e405-4302-aa80-82773149da1d', N'jobs')
INSERT [dbo].[Reddit] ([UserID], [Subreddit]) VALUES (N'227126ef-e405-4302-aa80-82773149da1d', N'learnprogramming')
INSERT [dbo].[Reddit] ([UserID], [Subreddit]) VALUES (N'227126ef-e405-4302-aa80-82773149da1d', N'lineageos')
INSERT [dbo].[Reddit] ([UserID], [Subreddit]) VALUES (N'227126ef-e405-4302-aa80-82773149da1d', N'linux')
INSERT [dbo].[Reddit] ([UserID], [Subreddit]) VALUES (N'227126ef-e405-4302-aa80-82773149da1d', N'news')
INSERT [dbo].[Reddit] ([UserID], [Subreddit]) VALUES (N'227126ef-e405-4302-aa80-82773149da1d', N'nyc')
INSERT [dbo].[Reddit] ([UserID], [Subreddit]) VALUES (N'227126ef-e405-4302-aa80-82773149da1d', N'personalfinance')
INSERT [dbo].[Reddit] ([UserID], [Subreddit]) VALUES (N'227126ef-e405-4302-aa80-82773149da1d', N'phillies')
INSERT [dbo].[Reddit] ([UserID], [Subreddit]) VALUES (N'227126ef-e405-4302-aa80-82773149da1d', N'phillyunion')
INSERT [dbo].[Reddit] ([UserID], [Subreddit]) VALUES (N'227126ef-e405-4302-aa80-82773149da1d', N'pics')
INSERT [dbo].[Reddit] ([UserID], [Subreddit]) VALUES (N'227126ef-e405-4302-aa80-82773149da1d', N'poker')
INSERT [dbo].[Reddit] ([UserID], [Subreddit]) VALUES (N'227126ef-e405-4302-aa80-82773149da1d', N'privacy')
INSERT [dbo].[Reddit] ([UserID], [Subreddit]) VALUES (N'227126ef-e405-4302-aa80-82773149da1d', N'productivity')
INSERT [dbo].[Reddit] ([UserID], [Subreddit]) VALUES (N'227126ef-e405-4302-aa80-82773149da1d', N'programmerreactions')
INSERT [dbo].[Reddit] ([UserID], [Subreddit]) VALUES (N'227126ef-e405-4302-aa80-82773149da1d', N'programming')
INSERT [dbo].[Reddit] ([UserID], [Subreddit]) VALUES (N'227126ef-e405-4302-aa80-82773149da1d', N'sixers')
INSERT [dbo].[Reddit] ([UserID], [Subreddit]) VALUES (N'227126ef-e405-4302-aa80-82773149da1d', N'soccer')
INSERT [dbo].[Reddit] ([UserID], [Subreddit]) VALUES (N'227126ef-e405-4302-aa80-82773149da1d', N'sql')
INSERT [dbo].[Reddit] ([UserID], [Subreddit]) VALUES (N'227126ef-e405-4302-aa80-82773149da1d', N'sysadmin')
INSERT [dbo].[Reddit] ([UserID], [Subreddit]) VALUES (N'227126ef-e405-4302-aa80-82773149da1d', N'talesfromtechsupport')
INSERT [dbo].[Reddit] ([UserID], [Subreddit]) VALUES (N'227126ef-e405-4302-aa80-82773149da1d', N'technology')
INSERT [dbo].[Reddit] ([UserID], [Subreddit]) VALUES (N'227126ef-e405-4302-aa80-82773149da1d', N'techsupport')
INSERT [dbo].[Reddit] ([UserID], [Subreddit]) VALUES (N'227126ef-e405-4302-aa80-82773149da1d', N'thesimpsons')
INSERT [dbo].[Reddit] ([UserID], [Subreddit]) VALUES (N'227126ef-e405-4302-aa80-82773149da1d', N'ubuntu')
INSERT [dbo].[Reddit] ([UserID], [Subreddit]) VALUES (N'227126ef-e405-4302-aa80-82773149da1d', N'videos')
INSERT [dbo].[Reddit] ([UserID], [Subreddit]) VALUES (N'227126ef-e405-4302-aa80-82773149da1d', N'vinyl')
INSERT [dbo].[Reddit] ([UserID], [Subreddit]) VALUES (N'227126ef-e405-4302-aa80-82773149da1d', N'webdev')
INSERT [dbo].[Reddit] ([UserID], [Subreddit]) VALUES (N'227126ef-e405-4302-aa80-82773149da1d', N'windows10')
INSERT [dbo].[Reddit] ([UserID], [Subreddit]) VALUES (N'227126ef-e405-4302-aa80-82773149da1d', N'xboxone')
INSERT [dbo].[User] ([UserID], [Username]) VALUES (N'184f82c9-f5ae-41e4-8121-0b938b3b3de3', N'toby')
INSERT [dbo].[User] ([UserID], [Username]) VALUES (N'227126ef-e405-4302-aa80-82773149da1d', N'chris')
INSERT [dbo].[YouTube] ([ChannelID], [UserID]) VALUES (N'UC2WHjPDvbE6O328n17ZGcfg', N'227126ef-e405-4302-aa80-82773149da1d')
INSERT [dbo].[YouTube] ([ChannelID], [UserID]) VALUES (N'UC4xKdmAXFh4ACyhpiQ_3qBw', N'227126ef-e405-4302-aa80-82773149da1d')
INSERT [dbo].[YouTube] ([ChannelID], [UserID]) VALUES (N'UC6Om9kAkl32dWlDSNlDS9Iw', N'227126ef-e405-4302-aa80-82773149da1d')
INSERT [dbo].[YouTube] ([ChannelID], [UserID]) VALUES (N'UC9x0AN7BWHpCDHSm9NiJFJQ', N'227126ef-e405-4302-aa80-82773149da1d')
INSERT [dbo].[YouTube] ([ChannelID], [UserID]) VALUES (N'UCBJycsmduvYEL83R_U4JriQ', N'227126ef-e405-4302-aa80-82773149da1d')
INSERT [dbo].[YouTube] ([ChannelID], [UserID]) VALUES (N'UCFCEuCsyWP0YkP3CZ3Mr01Q', N'227126ef-e405-4302-aa80-82773149da1d')
INSERT [dbo].[YouTube] ([ChannelID], [UserID]) VALUES (N'UCLNWyduFVhxjj0r1tPrE_-A', N'227126ef-e405-4302-aa80-82773149da1d')
INSERT [dbo].[YouTube] ([ChannelID], [UserID]) VALUES (N'UCqFzWxSCi39LnW1JKFR3efg', N'227126ef-e405-4302-aa80-82773149da1d')
INSERT [dbo].[YouTube] ([ChannelID], [UserID]) VALUES (N'UCsTcErHg8oDvUnTzoqsYeNw', N'227126ef-e405-4302-aa80-82773149da1d')
INSERT [dbo].[YouTube] ([ChannelID], [UserID]) VALUES (N'UCV0qA-eDDICsRR9rPcnG7tw', N'227126ef-e405-4302-aa80-82773149da1d')
INSERT [dbo].[YouTube] ([ChannelID], [UserID]) VALUES (N'UCvOEO35ieBuL-KdV0fXiuag', N'227126ef-e405-4302-aa80-82773149da1d')
INSERT [dbo].[YouTube] ([ChannelID], [UserID]) VALUES (N'UCXuqSBlHAE6Xw-yeJA0Tunw', N'227126ef-e405-4302-aa80-82773149da1d')
USE [master]
GO
ALTER DATABASE [AccountLess] SET  READ_WRITE 
GO
