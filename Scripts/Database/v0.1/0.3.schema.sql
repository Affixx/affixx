SET NOCOUNT ON

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Users' AND COLUMN_NAME = 'IsAdmin')
BEGIN
    PRINT 'Adding Users.IsAdmin'

    --want to make it explicit and avoid defaults, so updating in two steps
    ALTER TABLE Users ADD IsAdmin BIT NULL 

    EXEC sp_executesql N'UPDATE Users SET IsAdmin = 0' 
    EXEC sp_executesql N'ALTER TABLE Users ALTER COLUMN IsAdmin BIT NOT NULL'
END
GO

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Documents' AND COLUMN_NAME = 'IsApproved')
BEGIN
    PRINT 'Adding Documents.IsApproved'
    ALTER TABLE Documents ADD IsApproved BIT NULL 
END

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Documents' AND COLUMN_NAME = 'Hash')
BEGIN
    PRINT 'Adding Documents.Hash'
    ALTER TABLE Documents ADD Hash CHAR(32) NULL 
END

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Documents' AND COLUMN_NAME = 'MinHashSignature')
BEGIN
    PRINT 'Adding Documents.MinHashSignature'
    ALTER TABLE Documents ADD MinHashSignature VARCHAR(1000) NULL 
END

SET NOCOUNT OFF


