-- SQL Server Instance:  smg-sql01
IF (@@SERVERNAME <> 'smg-sql01')
BEGIN
PRINT 'Invalid SQL Server Connection'
RETURN
END

USE [ControlledSubstanceLog];


-----------------------------------------------------------------------------------------------
-- STEP 001 of 003
-- Add Shewbrooks, Amanda L
SELECT 1;
-- 1 record 

BEGIN TRANSACTION;

  INSERT INTO 
    [dbo].[AdUsers]
    ([user_name]
     ,[is_provider])
  VALUES
    ('Shewbrooks, Amanda L'
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
-- STEP 002 of 003
-- Add Snow, Patty
SELECT 1;
-- 1 record 

BEGIN TRANSACTION;

  INSERT INTO 
    [dbo].[AdUsers]
    ([user_name]
     ,[is_provider])
  VALUES
    ('Snow, Patty'
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

-- STEP 003 of 003
-- Add Cluxton, Starr L
SELECT 1;
-- 1 record 

BEGIN TRANSACTION;

  INSERT INTO 
    [dbo].[AdUsers]
    ([user_name]
     ,[is_provider])
  VALUES
    ('Cluxton, Starr L'
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
