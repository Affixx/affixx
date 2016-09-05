SET NOCOUNT ON

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'DocumentRatings')
BEGIN
	PRINT 'Creating DocumentRatings table'
	CREATE TABLE [dbo].[DocumentRatings] (
		Id BIGINT CONSTRAINT PK_DocumentRatings_Id PRIMARY KEY IDENTITY(1,1) NOT NULL,
		DocumentId BIGINT NOT NULL,
		Rating INT NOT NULL,
		CreatedBy BIGINT NOT NULL,
		CreatedAt DATETIME NOT NULL,
		CONSTRAINT FK_DocumentRatings_DocumentId FOREIGN KEY(DocumentId) REFERENCES Documents(Id),
		CONSTRAINT FK_DocumentRatings_CreatedBy FOREIGN KEY(CreatedBy) REFERENCES Users(Id)
	)
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Documents' AND COLUMN_NAME = 'Description') AND COLUMNPROPERTY(OBJECT_ID('Documents', 'U'), 'Description', 'AllowsNull')=0
BEGIN
	PRINT 'Making Documents.Descripion nullable'
	ALTER TABLE Documents ALTER COLUMN Description VARCHAR(4000) NULL
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'UserLogins')
BEGIN
	PRINT 'Creating UserLogins table'
	CREATE TABLE [dbo].UserLogins (
		Id BIGINT CONSTRAINT PK_UserLogins_Id PRIMARY KEY IDENTITY(1, 1) NOT NULL,
		LoginProvider [VARCHAR](50) NOT NULL,
		ProviderKey [VARCHAR](50) NOT NULL,
		UserId BIGINT NOT NULL,
	        CONSTRAINT UQ_UserLogins UNIQUE (LoginProvider, ProviderKey, UserId),
		CONSTRAINT FK_UserLogins_Users_UserId FOREIGN KEY (UserId) REFERENCES Users (Id) 
	) 
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Users' AND COLUMN_NAME = 'Password') AND COLUMNPROPERTY(OBJECT_ID('Users', 'U'), 'Password', 'AllowsNull')=0
BEGIN
	PRINT 'Making Users.Password nullable'
	ALTER TABLE Users ALTER COLUMN Password VARCHAR(100) NULL
END
GO

        
SET NOCOUNT OFF

