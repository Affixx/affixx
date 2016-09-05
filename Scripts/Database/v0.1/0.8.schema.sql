SET NOCOUNT ON

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'UniversityCourses')
BEGIN
	PRINT 'Creating UniversityCourses table'
	CREATE TABLE [dbo].[UniversityCourses] (
		Id BIGINT CONSTRAINT PK_UniversityCourses_Id PRIMARY KEY IDENTITY(1,1) NOT NULL,
		UniversityId BIGINT NOT NULL,
		[Group] NVARCHAR(50) NOT NULL,
		Name NVARCHAR(150) NOT NULL,
		Code VARCHAR(50) NOT NULL,
		CreatedBy BIGINT NOT NULL,
		CreatedAt DATETIME NOT NULL,
		DeletedAt DATETIME NULL,
		--CONSTRAINT UQ_UniversityCourses_UniversityId_Name_Code UNIQUE (UniversityId, Name, Code),
		CONSTRAINT FK_UniversityCourses_UniversityId FOREIGN KEY(UniversityId) REFERENCES Universities(Id),
		CONSTRAINT FK_UniversityCourses_CreatedBy FOREIGN KEY(CreatedBy) REFERENCES Users(Id)
	)
END
GO

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Users' AND COLUMN_NAME = 'UniversityId')
BEGIN
    PRINT 'Adding Users.UniversityId'

    ALTER TABLE Users ADD UniversityId BIGINT NULL 
    ALTER TABLE Users ADD CONSTRAINT FK_Users_UniversityId FOREIGN KEY (UniversityId) REFERENCES Universities(Id)
END
GO

        
SET NOCOUNT OFF


