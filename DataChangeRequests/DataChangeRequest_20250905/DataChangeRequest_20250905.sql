-- SQL Server Instance:  smg-sql01
IF (@@SERVERNAME <> 'smg-sql01')
BEGIN
PRINT 'Invalid SQL Server Connection'
RETURN
END

USE [ControlledSubstanceLog];


-- 001 - Insert Crystal McPhail Active Directory display name
-- into AdUsers table, allowing them the use of the 
-- Controlled Substance Log application.

SELECT 1;
-- 1 record 


BEGIN TRAN
INSERT INTO [dbo].[AdUsers]
           ([user_name]
           ,[is_provider])
     VALUES
           ('McPhail, Crystal P'
           ,0);

-- COMMIT
-- ROLLBACK
-----------------------------------------------------------------------------------------------