-- SQL Server Instance:  smg-sql01
IF (@@SERVERNAME <> 'smg-sql01')
BEGIN
PRINT 'Invalid SQL Server Connection'
RETURN
END

USE [ControlledSubstanceLog];

-----------------------------------------------------------------------------------------------
-- STEP 001 of 004
-- Add Massey, Katherine K  NP
SELECT 1;
-- 1 record 

BEGIN TRANSACTION;

  INSERT INTO 
    [dbo].[AdUsers]
    ([user_name]
     ,[is_provider])
  VALUES
    ('Massey, Katherine K  NP'
    ,1);


IF @@ERROR > 0 BEGIN
  SELECT 0;
  ROLLBACK TRANSACTION;
END
ELSE BEGIN
  SELECT 1;
  COMMIT TRANSACTION;
END
-----------------------------------------------------------------------------------------------
-- STEP 002 of 004
-- Add Carr, Shalinka J
SELECT 1;
-- 1 record 

BEGIN TRANSACTION;

  INSERT INTO 
    [dbo].[AdUsers]
    ([user_name]
     ,[is_provider])
  VALUES
    ('Carr, Shalinka'
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
-- STEP 003 of 004
-- Add Lancaster, Brandon W  Dr.
SELECT 1;
-- 1 record 

BEGIN TRANSACTION;

  INSERT INTO 
    [dbo].[AdUsers]
    ([user_name]
     ,[is_provider])
  VALUES
    ('Lancaster, Brandon W  Dr.'
    ,1);


IF @@ERROR > 0 BEGIN
  SELECT 0;
  ROLLBACK TRANSACTION;
END
ELSE BEGIN
  SELECT 1;
  COMMIT TRANSACTION;
END
-----------------------------------------------------------------------------------------------
-- STEP 004 of 004
-- Add Russell, Traci M
SELECT 1;
-- 1 record 

BEGIN TRANSACTION;

  INSERT INTO 
    [dbo].[AdUsers]
    ([user_name]
     ,[is_provider])
  VALUES
    ('Russell, Traci M'
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