SET NOCOUNT ON

-- create deployment helper store proc
IF EXISTS (SELECT * FROM sysobjects  WHERE id = object_id(N'dbo.spDeploymentHelperAddUniversity'))
BEGIN
    DROP PROCEDURE dbo.spDeploymentHelperAddUniversity
END
GO
CREATE PROCEDURE spDeploymentHelperAddUniversity (@universityName VARCHAR(50))
AS
BEGIN
	IF NOT EXISTS (SELECT * FROM Universities WHERE Name = @universityName)
	BEGIN
		PRINT 'Inserting "' + @universityName + '" to Universities table'
		INSERT INTO [dbo].[Universities] 
			(Name)
		VALUES
			(@universityName)
	END
END
GO



EXEC dbo.spDeploymentHelperAddUniversity 'University of Pennsylvania'
EXEC dbo.spDeploymentHelperAddUniversity 'Penn State University'
EXEC dbo.spDeploymentHelperAddUniversity 'Brandeis University'
EXEC dbo.spDeploymentHelperAddUniversity 'Princeton University'
EXEC dbo.spDeploymentHelperAddUniversity 'Michigan University'
EXEC dbo.spDeploymentHelperAddUniversity 'New York University'




-- drop deployment helper store proc
IF EXISTS (SELECT * FROM sysobjects  WHERE id = object_id(N'dbo.spDeploymentHelperAddUniversity'))
BEGIN
    DROP PROCEDURE dbo.spDeploymentHelperAddUniversity
END


SET NOCOUNT OFF