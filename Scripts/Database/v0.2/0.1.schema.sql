SET NOCOUNT ON

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Users' AND COLUMN_NAME = 'InviteCode') AND COLUMNPROPERTY(OBJECT_ID('Users', 'U'), 'InviteCode', 'AllowsNull')=0
BEGIN
	PRINT 'Making Users.InviteCode nullable'
     
    ALTER TABLE Users ALTER COLUMN InviteCode VARCHAR(6) NULL
	
    UPDATE Users SET InviteCode = NULL where Role != 'Professor'
    
END
GO

SET NOCOUNT OFF