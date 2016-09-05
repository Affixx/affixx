SET NOCOUNT ON

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Documents' AND COLUMN_NAME = 'CategoryId')
BEGIN
    PRINT 'Adding Documents.CategoryId'
    ALTER TABLE Documents ADD CategoryId BIGINT NULL 

END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Documents' AND COLUMN_NAME = 'CategoryId') AND COLUMNPROPERTY(OBJECT_ID('Documents', 'U'), 'CategoryId', 'AllowsNull')=1
BEGIN
	PRINT 'Making Documents.CategoryId not nullable'
	UPDATE Documents SET CategoryId = (SELECT MIN(Id) from DocumentCategories) 
    ALTER TABLE Documents ADD Constraint FK_Documents_CategoryId FOREIGN KEY(CategoryId) REFERENCES DocumentCategories(Id)
    ALTER TABLE Documents ALTER COLUMN CategoryId BIGINT NOT NULL
END
GO

SET NOCOUNT OFF
