SET NOCOUNT ON

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Users' AND COLUMN_NAME = 'InviteCode')
BEGIN
    PRINT 'Adding Users.InviteCode'

    --want to make it explicit and avoid defaults, so updating in two steps
    ALTER TABLE Users ADD InviteCode VARCHAR(6) NULL 
    
    EXEC sp_executesql N'UPDATE Users SET InviteCode = LEFT(NEWID(), 6)' 
    EXEC sp_executesql N'ALTER TABLE Users ALTER COLUMN InviteCode VARCHAR(6) NOT NULL'
    
    ALTER TABLE Users ADD CONSTRAINT UQ_Users_InviteCode UNIQUE NONCLUSTERED (InviteCode)
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Referrals' AND COLUMN_NAME = 'PromoCode')
BEGIN
    PRINT 'Dropping Referrals.PromoCode'
    ALTER TABLE Referrals DROP CONSTRAINT UQ_Referrals_PromoCode
    ALTER TABLE Referrals DROP COLUMN PromoCode
END
GO

        
SET NOCOUNT OFF


