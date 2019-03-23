-- get subreddits for user
select subreddit from reddit where UserID = @userID;

-- add new subreddit
insert into reddit (UserID, subreddit)
values (@userID, @subreddit);

--DB
USE [AccountLess]
GO
/****** Object:  Table [dbo].[Reddit]    Script Date: 3/23/2019 12:45:14 PM ******/
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
/****** Object:  Table [dbo].[User]    Script Date: 3/23/2019 12:45:15 PM ******/
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
