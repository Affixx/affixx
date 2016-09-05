SET NOCOUNT ON

IF NOT EXISTS (SELECT * FROM Universities)
BEGIN
	PRINT 'Inserting Universities data'
	INSERT INTO [dbo].[Universities] 
		(Name)
	VALUES
		('UCL'),
		('Imperial College'),
		('King''s College')
END
GO

SET NOCOUNT OFF