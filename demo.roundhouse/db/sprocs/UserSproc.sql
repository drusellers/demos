IF OBJECT_ID('dbo.GetUser') IS NULL -- Check if SP Exists
 EXEC('CREATE PROCEDURE dbo.GetUser AS SET NOCOUNT ON;') -- Create dummy/empty SP
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

ALTER PROCEDURE dbo.GetUser -- Alter the SP Always
 @username INT
AS
BEGIN
 SET NOCOUNT ON;
  
 SELECT username, passward_hash
 FROM dbo.Users AS u
 WHERE u.username = @username
 
END
GO