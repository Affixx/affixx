SET NOCOUNT ON

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Users' AND COLUMN_NAME = 'AcademicField')
BEGIN
	PRINT 'Adding Users.AcademicField'
     
    ALTER TABLE Users ADD AcademicField Nvarchar(100) NULL
    
END
GO

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Users' AND COLUMN_NAME = 'AcademicResume')
BEGIN
	PRINT 'Adding Users.AcademicResume'
     
    ALTER TABLE Users ADD AcademicResume Nvarchar(100) NULL
	
END
GO

SET NOCOUNT OFF