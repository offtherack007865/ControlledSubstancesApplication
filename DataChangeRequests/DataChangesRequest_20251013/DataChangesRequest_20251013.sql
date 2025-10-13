-- SQL Server Instance:  smg-sql01
IF (@@SERVERNAME <> 'smg-sql01')
BEGIN
PRINT 'Invalid SQL Server Connection'
RETURN
END

USE [ControlledSubstanceLog];

-----------------------------------------------------------------------------------------------
-- For Service Now ticket INC0179326
-----------------------------------------------------------------------------------------------
-- STEP 001 OF 004
-- Insert Carver, Robin L 

SELECT 1;
-- 1 record 

BEGIN TRANSACTION;

  INSERT INTO 
    [dbo].[AdUsers]
    ([user_name]
     ,[is_provider])
  VALUES
    ('Carver, Robin L'
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
-- STEP 002 OF 004
-- Insert Minton, Tina M

SELECT 1;
-- 1 record 

BEGIN TRANSACTION;

  INSERT INTO 
    [dbo].[AdUsers]
    ([user_name]
     ,[is_provider])
  VALUES
    ('Minton, Tina M'
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
-- STEP 003 OF 004
-- Insert Schmidt, Kristen L 

SELECT 1;
-- 1 record 

BEGIN TRANSACTION;

  INSERT INTO 
    [dbo].[AdUsers]
    ([user_name]
     ,[is_provider])
  VALUES
    ('Schmidt, Kristen L'
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
-- STEP 004 OF 004
-- Insert Schmittou, Lea A

SELECT 1;
-- 1 record 

BEGIN TRANSACTION;

  INSERT INTO 
    [dbo].[AdUsers]
    ([user_name]
     ,[is_provider])
  VALUES
    ('Schmittou, Lea A'
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