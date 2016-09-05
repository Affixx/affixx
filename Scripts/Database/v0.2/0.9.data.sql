SET NOCOUNT ON

IF NOT EXISTS (SELECT * FROM Users WHERE Email='addminn@affixx.co')
BEGIN
	PRINT 'Inserting superadmin'
	INSERT INTO [dbo].[Users] 
		(Name, Email, Password, CreatedAt, InviteCode, Role)
	VALUES
		('Super Admin', 'addminn@affixx.co','ALKth8wPsqGamE3WVOwPkY1oXIklfBIg67TEttp6lEhXZdKzOVItDNAMGyI2Ci8l2Q==',GETDATE(),'xxxx','Admin')  --qweqwe101
END
GO

SET NOCOUNT OFF