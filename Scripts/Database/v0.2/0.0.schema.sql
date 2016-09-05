SET NOCOUNT ON

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Users' AND COLUMN_NAME = 'Role')
BEGIN
	PRINT 'Adding Users.Role'
     
    ALTER TABLE Users ADD Role Varchar(50) NULL
	
    EXEC sp_executesql N'UPDATE Users SET Role = ''Student'' where IsAdmin = 0'
    EXEC sp_executesql N'UPDATE Users SET Role = ''Admin'' where IsAdmin = 1'
    EXEC sp_executesql N'ALTER TABLE Users ALTER COLUMN Role VARCHAR(50) NOT NULL'
    EXEC sp_executesql N'ALTER TABLE Users ADD Constraint CHK_Users_Role CHECK (Role = ''Student'' OR Role= ''Admin'' OR Role = ''Professor'')' 
    EXEC sp_executesql N'ALTER TABLE Users Drop Column IsAdmin'
END
GO

SET NOCOUNT OFF