SET NOCOUNT ON

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Documents' AND COLUMN_NAME = 'IsFree')
BEGIN
     --want to make it explicit and avoid defaults, so updating in two steps
    ALTER TABLE Documents ADD IsFree BIT NULL 

    EXEC sp_executesql N'UPDATE Documents SET IsFree = 0' 
    EXEC sp_executesql N'ALTER TABLE Documents ALTER COLUMN IsFree BIT NOT NULL'
END
GO

SET NOCOUNT OFF