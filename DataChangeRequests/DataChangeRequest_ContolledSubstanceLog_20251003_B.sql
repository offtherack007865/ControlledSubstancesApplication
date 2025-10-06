-- SQL Server Instance:  smg-sql01
IF (@@SERVERNAME <> 'smg-sql01')
BEGIN
PRINT 'Invalid SQL Server Connection'
RETURN
END

USE [ControlledSubstanceLog];

-----------------------------------------------------------------------------------------------
-- STEP 001 of 001
-- Update  Carr, Shalinka to Carr, Shalinka J
SELECT COUNT(*)
FROM [dbo].[AdUsers]
WHERE [id] = 2740;
-- 1 record 

BEGIN TRANSACTION;

  UPDATE [dbo].[AdUsers]
  SET [user_name] = 'Carr, Shalinka J'
  WHERE [id] = 2740;


IF @@ERROR > 0 BEGIN
  SELECT 0;
  ROLLBACK TRANSACTION;
END
ELSE BEGIN
  SELECT 1;
  COMMIT TRANSACTION;
END
-----------------------------------------------------------------------------------------------
