-- SQL Server Instance:  smg-sql01
IF (@@SERVERNAME <> 'smg-sql01')
BEGIN
PRINT 'Invalid SQL Server Connection'
RETURN
END

USE [ControlledSubstanceLog];


-----------------------------------------------------------------------------------------------
-- STEP 001 of 002
-- Add Gregory, Sasha
SELECT 1;
-- 1 record 

BEGIN TRANSACTION;

  INSERT INTO 
    [dbo].[AdUsers]
    ([user_name]
     ,[is_provider])
  VALUES
    ('Gregory, Sasha'
    ,0);


IF @@ERROR > 0 BEGIN
  SELECT 0;
  ROLLBACK TRANSACTION;
END
ELSE BEGIN
  SELECT 1;
  COMMIT TRANSACTION;
END
-----------------------------------------------------------------------------------------------
-- STEP 002 of 002
-- Add Goldhammer, Karis
SELECT 1;
-- 1 record 

BEGIN TRANSACTION;

  INSERT INTO 
    [dbo].[AdUsers]
    ([user_name]
     ,[is_provider])
  VALUES
    ('Goldhammer, Karis'
    ,0);


IF @@ERROR > 0 BEGIN
  SELECT 0;
  ROLLBACK TRANSACTION;
END
ELSE BEGIN
  SELECT 1;
  COMMIT TRANSACTION;
END
-----------------------------------------------------------------------------------------------
