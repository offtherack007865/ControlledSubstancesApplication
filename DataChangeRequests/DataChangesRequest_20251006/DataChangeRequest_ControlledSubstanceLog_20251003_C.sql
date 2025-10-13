-- SQL Server Instance:  smg-sql01
IF (@@SERVERNAME <> 'smg-sql01')
BEGIN
PRINT 'Invalid SQL Server Connection'
RETURN
END

USE [ControlledSubstanceLog];

-----------------------------------------------------------------------------------------------
-- STEP 001 of 001
-- Add Callahan, Amanda Mary
SELECT 1;
-- 1 record 

BEGIN TRANSACTION;

  INSERT INTO 
    [dbo].[AdUsers]
    ([user_name]
     ,[is_provider])
  VALUES
    ('Callahan, Amanda Mary'
    ,0);


IF @@ERROR > 0 BEGIN
  SELECT 0;
  ROLLBACK TRANSACTION;
END
ELSE BEGIN
  SELECT 1;
  COMMIT TRANSACTION;
END
