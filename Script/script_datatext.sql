USE [ManagementDataDb]
GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 10/1/2020 10:25:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__MigrationHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ContextKey] [nvarchar](300) NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 10/1/2020 10:25:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 10/1/2020 10:25:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 10/1/2020 10:25:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 10/1/2020 10:25:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](128) NOT NULL,
	[RoleId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 10/1/2020 10:25:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](128) NOT NULL,
	[Email] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEndDateUtc] [datetime] NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[UserName] [nvarchar](256) NOT NULL,
	[Avatar] [nvarchar](max) NULL,
	[ApiToken] [nvarchar](max) NULL,
	[FullName] [nvarchar](100) NULL,
	[CreateDate] [datetime] NULL,
 CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DataInserts]    Script Date: 10/1/2020 10:25:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DataInserts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Text] [nvarchar](max) NULL,
	[TimeCreate] [datetime] NOT NULL,
	[UserId] [nvarchar](128) NULL,
 CONSTRAINT [PK_dbo.DataInserts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DataInsertTmps]    Script Date: 10/1/2020 10:25:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DataInsertTmps](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Text] [nvarchar](max) NULL,
	[TimeCreate] [datetime] NOT NULL,
	[UserId] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.DataInsertTmps] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]
GO
/****** Object:  StoredProcedure [dbo].[GetData]    Script Date: 10/1/2020 10:25:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--------------Proc get  Data-------------------------------
Create Proc [dbo].[GetData] @userId nvarchar(128)
as
begin
select Top(1)* from DataInserts
where UserId=@userId
order by TimeCreate
end
GO
/****** Object:  StoredProcedure [dbo].[GetDataById]    Script Date: 10/1/2020 10:25:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--------------Proc get  DatabyId-------------------------------
Create Proc [dbo].[GetDataById] @Id int, @userId nvarchar(128)
as
begin
select Top(1)* from DataInserts
where Id=@Id and UserId=@userId
order by TimeCreate
end
GO
/****** Object:  StoredProcedure [dbo].[Proc_DeleteData]    Script Date: 10/1/2020 10:25:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

 CREATE PROC [dbo].[Proc_DeleteData]
 (
	@id int
 )
 AS
 BEGIN
	DELETE FROM DataInserts WHERE ID=@id
 END
GO
/****** Object:  StoredProcedure [dbo].[PROC_GetData_Paging]    Script Date: 10/1/2020 10:25:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[PROC_GetData_Paging] 
 (
 @page INT,
 @size INT,
 @userId nvarchar(128),
 @orderId INT,
 @search nvarchar(MAX)
 )
 as
 begin

	IF (@orderId=1)
		BEGIN
			IF (@search = '')
				BEGIN
						SELECT * FROM DataInserts
						where UserId=@userId
						ORDER BY TimeCreate ASC , ID ASC
						OFFSET (@page -1) * @size ROWS
						FETCH NEXT @size ROWS ONLY
				END
			ELSE
				BEGIN
						SELECT * FROM DataInserts
						where UserId=@userId AND Text LIKE N'%' + @search+'%'
						ORDER BY TimeCreate ASC , ID ASC
						OFFSET (@page -1) * @size ROWS
						FETCH NEXT @size ROWS ONLY
				END
			
		END
	ELSE
		BEGIN
			IF(@search = '')
				BEGIN
					SELECT * FROM DataInserts
					where UserId=@userId
					ORDER BY TimeCreate desc 
					OFFSET (@page -1) * @size ROWS
					FETCH NEXT @size ROWS ONLY
				END
			ELSE
				BEGIN
					SELECT * FROM DataInserts
					where UserId=@userId AND Text LIKE N'%' + @search+'%'
					ORDER BY TimeCreate desc 
					OFFSET (@page -1) * @size ROWS
					FETCH NEXT @size ROWS ONLY
				END
		END
	
 end
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetLastData]    Script Date: 10/1/2020 10:25:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proc_GetLastData]
(
	@userId nvarchar(128)
)
AS
BEGIN
	SELECT TOP(1) * 
	FROM DataInserts
	WHERE UserId = @userId
	ORDER BY TimeCreate
END
GO
/****** Object:  StoredProcedure [dbo].[Proc_InserDataPost]    Script Date: 10/1/2020 10:25:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proc_InserDataPost]
(
	@userId nvarchar(128),
	@text nvarchar(MAX),
	@timeCreate Datetime
)
AS
BEGIN
	INSERT INTO DataInserts(Text, TimeCreate, UserId)
	VALUES (@text,@timeCreate,@userId);
END
GO
/****** Object:  StoredProcedure [dbo].[Proc_InsertData]    Script Date: 10/1/2020 10:25:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

 CREATE PROC [dbo].[Proc_InsertData]
 (
	@userId nvarchar(128),
	@text nvarchar(MAX)
 )
 AS
 BEGIN
	INSERT INTO DataInserts(Text, TimeCreate, UserId)
	VALUES (@text,GETDATE(),@userId);
 END

GO
/****** Object:  StoredProcedure [dbo].[Proc_InsertDataPost]    Script Date: 10/1/2020 10:25:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proc_InsertDataPost]
(
	@userId nvarchar(128),
	@text nvarchar(MAX),
	@timeCreate Datetime
)
AS
BEGIN
	INSERT INTO DataInserts(Text, TimeCreate, UserId)
	VALUES (@text,@timeCreate,@userId);
END
GO
/****** Object:  StoredProcedure [dbo].[Proc_Paging_demo]    Script Date: 10/1/2020 10:25:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[Proc_Paging_demo] (@top int, @userid nvarchar(128),@idlast int) as
Begin
	IF (@idlast>0)
		BEGIN
			SELECT Top(@top) * FROM DataInserts  D
				WHERE  D.UserId=@userid  and Id<@idlast
				ORDER BY TimeCreate  desc
		END
	ELSE
		BEGIN
			SELECT Top(@top) * FROM DataInserts  D
				WHERE  D.UserId=@userid  
				ORDER BY TimeCreate  desc
		END
END
GO
/****** Object:  StoredProcedure [dbo].[Proc_UpdateData]    Script Date: 10/1/2020 10:25:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

  CREATE PROC [dbo].[Proc_UpdateData]
 (
	@id int,
	@text nvarchar(MAX)
 )
 AS
 BEGIN
	UPDATE DataInserts
	SET Text = @text
	WHERE Id = @id
 END
GO
