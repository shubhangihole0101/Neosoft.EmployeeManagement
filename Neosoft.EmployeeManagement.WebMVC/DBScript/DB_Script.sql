
DROP TABLE  [dbo].[Country]

GO

CREATE TABLE [dbo].[Country](
	[Row_Id] [int] IDENTITY(1,1) primary key ,
	[CountryName] [nvarchar](50) Not NULL
)


GO

DROP TABLE dbo.[State]

GO

CREATE TABLE [dbo].[State](
	[Row_Id] [int] IDENTITY(1,1) primary key,
	[StateName] [nvarchar](50) Not NULL,
	[CountryId] INT NOT NULL Foreign key REFERENCES dbo.Country (Row_id)
)

GO

DROP TABLE [dbo].[City]

GO

CREATE TABLE [dbo].[City](
	[Row_Id] [int] IDENTITY(1,1) primary key,
	[CityName] [nvarchar](50) Not NULL,
	[StateId] INT NOT NULL Foreign key REFERENCES dbo.State (Row_id)
)

GO

DROP TABLE [dbo].[EmployeeMaster]

CREATE TABLE [dbo].[EmployeeMaster](
	[Row_Id] [int] IDENTITY(1,1) Primary Key,
	[EmployeeCode] AS (RIGHT('00' + CAST(Row_id AS VARCHAR(8)), 8)) PERSISTED NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[CountryId] [int]NOT NULL Foreign Key references dbo.Country(ROW_Id),
	[StateId] [int]NOT NULL Foreign Key references dbo.State(ROW_Id),
	[CityId] [int]NOT Null Foreign Key references dbo.City(ROW_Id),
	[EmailAddress] [varchar](100) NOT NULL UNIQUE,
	[MobileNumber] [varchar](100) NOT NULL UNIQUE,
	[PanNumber] [varchar](12) NULL UNIQUE,
	[PassportNumber] [varchar](20) NOT NULL UNIQUE,
	[ProfileImage] [varchar](100),
	[Gender] [tinyint] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[DateOfBirth] [date]NOT NULL,
	[DateOfJoinee] [date] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL,
	[DeletedDate] [datetime] NULL
)

GO

CREATE Proc [dbo].[stp_Emp_InsertEmployeeMaster](
	@FirstName nvarchar(50),
	@LastName nvarchar(50),
	@CountryId INT,
	@StateId INT,
	@CityId INT,
	@EmailAddress varchar(100),
	@MobileNumber  varchar(100),
	@PanNumber varchar(12),
	@PassportNumber varchar(20),
	@ProfileImage varchar(100),
	@Gender Bit,
	@IsActive bit,
	@dateOfBirth date,
	@dateOfJoinee Date,
	@CreatedDate datetime
)
AS
BEGIN
IF NOT EXISTS (SELECT 1 FROM dbo.EmployeeMaster WHERE PanNumber= @PanNumber
				 or   
				 PassportNumber=@PassportNumber 
				 or EmailAddress = @EmailAddress or MobileNumber = @MobileNumber
				 )
BEGIN
	INSERT INTO dbo.EmployeeMaster
	(FirstName,LastName,CountryId,StateId, CityId, EmailAddress,MobileNumber,PanNumber,PassportNumber,ProfileImage,	GENDER, ISACTIVE, DateOfBirth,DateOfJoinee,CreatedDate) values
	(@FirstName, @LastName,@CountryId,@StateId, @CityId, @EmailAddress,@MobileNumber, @PanNumber, @PassportNumber,@ProfileImage, @Gender,@IsActive, @dateOfBirth, @dateOfJoinee, @CreatedDate )
	
	SELECT SCOPE_IDENTITY()
 END
 ELSE
  BEGIN
	SELECT -1
  END
END


GO


CREATE Proc [dbo].[stp_Emp_UpdateEmployeeMaster](
	@Row_Id INT,
	@FirstName nvarchar(50),
	@LastName nvarchar(50),
	@CountryId INT,
	@StateId INT,
	@CityId INT,
	@EmailAddress varchar(100),
	@MobileNumber  varchar(100),
	@PanNumber varchar(12),
	@PassportNumber varchar(20),
	@ProfileImage varchar(100),
	@Gender Bit,
	@IsActive bit,
	@dateOfBirth date,
	@dateOfJoinee Date,
	@UpdateDate datetime
)
AS
BEGIN
IF NOT EXISTS (SELECT 1 FROM dbo.EmployeeMaster WHERE (PanNumber= @PanNumber
				 or   
				 PassportNumber=@PassportNumber 
				 or EmailAddress = @EmailAddress or MobileNumber = @MobileNumber) 
				 AND Row_Id != @Row_Id
				 )
BEGIN
	UPDATE [dbo].[EmployeeMaster]
	   SET [FirstName] = @FirstName
		  ,[LastName] =@LastName
		  ,[CountryId] = @CountryId
		  ,[StateId] = @StateId
		  ,[CityId] = @CityId
		  ,[EmailAddress] = @EmailAddress
		  ,[MobileNumber] = @MobileNumber
		  ,[PanNumber] = @PanNumber
		  ,[PassportNumber] = @PassportNumber
		  ,[ProfileImage] = @ProfileImage
		  ,[Gender] = @Gender
		  ,[IsActive] = @IsActive
		  ,[DateOfBirth] = @dateOfBirth
		  ,[DateOfJoinee] = @dateOfJoinee
		  ,[UpdatedDate] = @UpdateDate
	 WHERE Row_Id = @Row_Id
 END
END

GO

CREATE Proc [dbo].[stp_Emp_DeleteEmployeeMaster]
	(
		@Row_Id int
	)
AS
BEGIN
  UPDATE dbo.EmployeeMaster
  SET [IsDeleted]=1,[DeletedDate]=GETDATE()
  WHERE Row_Id=@Row_Id
END

GO

CREATE Proc [dbo].[stp_Emp_GetAllEmployeeMaster]
AS
BEGIN
	SELECT * FROM dbo.EmployeeMaster
END

GO

CREATE Proc [dbo].[stp_Emp_GetAllCountry]
as
begin
select * from dbo.Country
End
GO

CREATE Proc [dbo].[stp_Emp_GetAllState]
as
begin
select * from dbo.State
End

GO


CREATE Proc [dbo].[stp_Emp_GetAllCity]
as
begin
select * from dbo.City
End