SET NOCOUNT ON

IF NOT EXISTS (SELECT * FROM DocumentCategories)
BEGIN
	PRINT 'Inserting DocumentCategories data'
	INSERT INTO [dbo].[DocumentCategories] 
		(Name)
	VALUES
		('Engineering'),
		('Business'),
		('Psychology'),
		('Biology'),
		('Economics')
END
GO

SET NOCOUNT OFF