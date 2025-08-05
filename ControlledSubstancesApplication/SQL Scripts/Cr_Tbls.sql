USE MASTER
--ALTER DATABASE ControlledSubstanceLog SET SINGLE_USER WITH ROLLBACK IMMEDIATE
--IF OBJECT_ID('ControlledSubstanceLog', 'U') IS NOT NULL
--DROP DATABASE ControlledSubstanceLog
GO

CREATE DATABASE ControlledSubstanceLog
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER OFF
GO

USE ControlledSubstanceLog
GO

CREATE TABLE Sites(
	site_number INT PRIMARY KEY,
	site_name VARCHAR(255)
)
GO

CREATE TABLE Lots(
	id INT IDENTITY PRIMARY KEY,
	date_entered DATETIME NOT NULL,
	site_number INT NOT NULL,
	lot_number VARCHAR(50) NOT NULL,
	entry_code VARCHAR(50) NOT NULL,

	CONSTRAINT fk_site_number FOREIGN KEY(site_number) REFERENCES Sites(site_number)
)
GO

CREATE TABLE Entries(
	id INT IDENTITY PRIMARY KEY,
	entry_date DATETIME NOT NULL,
	lot_id INT NOT NULL,
	patient_mrn VARCHAR(20) NOT NULL,
	provider VARCHAR(255) NOT NULL,
	amt_given DECIMAL(8, 4) NOT NULL,
	amt_wasted DECIMAL(8,4) NOT NULL DEFAULT 0,
	administered_by VARCHAR(255) NOT NULL,
	witnessed_by VARCHAR(255),
	amt_to_count DECIMAL(8,4)NOT NULL, 
	is_error BIT NOT NULL DEFAULT 0 

	CONSTRAINT fk_lot_id FOREIGN KEY(lot_id) REFERENCES Lots(id)
)
GO

--CREATE AND EXECUTE STORED PROCS
CREATE PROCEDURE dbo.PullMedList

AS
BEGIN

	SET NOCOUNT ON;
	IF OBJECT_ID('Medications', 'U') IS NOT NULL
	DROP TABLE Medications
	  SELECT m.[ID]
      ,m.[EnterpriseEntryID]
      ,m.[Entry]
      ,m.[EntryCode]
      ,m.[EntryName]
	  ,m.form
	  ,r.EntryName AS RouteOfAdmin
  INTO Medications
  FROM psdw01.Works_Reporting.dbo.Medication_DE m
     LEFT JOIN psdw01.Works_Reporting.dbo.route_of_admin_de r ON m.routeofadminde = r.id
  WHERE ControlSubstanceCode IN('C3','C2','c4','C1','C5')
     AND m.IsInactiveFLAG <> 'Y'
	 AND m.form = 'soln'
	 AND r.Entryname IN('Injection', 'Intramuscular','Intravenous')
END
GO

EXECUTE PullMedList
GO

CREATE PROCEDURE PopulateAdUsers
AS
BEGIN
SET NOCOUNT ON;

IF OBJECT_ID('AdUsers', 'U') IS NOT NULL
  DROP TABLE AdUsers; 

CREATE TABLE AdUsers(
id INT IDENTITY PRIMARY KEY,
user_name nvarchar(255),
is_provider bit DEFAULT 0
)

DECLARE @sChar char(1)
DECLARE @body nvarchar(4000)
DECLARE @nAsciiValue smallint
DECLARE @cmdstr nvarchar(512)


SELECT @nAsciiValue = 65

WHILE @nAsciiValue < 91
BEGIN

SELECT @sChar = CHAR(@nAsciiValue)

EXEC xp_sprintf @cmdstr OUTPUT,'SELECT * FROM OpenQuery(
ADSI, "SELECT displayName
		FROM ''LDAP://ad.sumg.int''
		WHERE objectCategory = ''User''
		AND SAMAccountName = ''%s*''
		AND ''userAccountControl:1.2.840.113556.1.4.803:''<>2
		 ")', @sChar 


INSERT AdUsers (user_name)
EXEC(@cmdstr)

SELECT @nAsciiValue = @nAsciiValue + 1
END
END
GO

EXEC PopulateAdUsers
GO

CREATE PROCEDURE MarkProviders
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;



  DECLARE @numrows int
IF OBJECT_ID('tempUsers', 'U') IS NOT NULL
  DROP TABLE tempUsers; 

CREATE TABLE tempUsers(
id int Identity(1,1),
user_name nvarchar(255)
)
DELETE FROM AdUsers WHERE user_name IS NULL
INSERT INTO tempUsers (user_name)
SELECT * FROM OpenQuery(
ADSI, 'SELECT displayName
		FROM ''LDAP://ad.sumg.int''
		WHERE objectCategory = ''user''	
		AND ''userAccountControl:1.2.840.113556.1.4.803:''<>2
		AND memberOf = ''CN="SGPhysician",OU="Security Groups",DC="ad",DC="sumg",DC="int"''
		OR memberOf = ''CN="Non Physician Practitioners",OU="Security Groups",DC="ad",DC="sumg",DC="int"''	
		 ')

SET @numrows = (SELECT COUNT(*) FROM tempUsers)
DECLARE @i int = 0
DECLARE @username nvarchar(255)

IF @numrows > 0
    WHILE (@i <= (SELECT MAX(id) FROM tempUsers))
    BEGIN
	SET @username = (SELECT user_name FROM tempUsers WHERE id = @i)

	IF EXISTS (SELECT user_name FROM AdUsers WHERE user_name = @username)
	UPDATE AdUsers SET AdUsers.is_provider = 1
	WHERE user_name = @username

	SET @i = @i + 1
	END


DROP TABLE tempUsers

END
GO


EXECUTE MarkProviders
GO

CREATE UNIQUE INDEX uq_lot_number ON LOTS(site_number, lot_number, entry_code)
GO

--ALTER DATABASE ControlledSubstanceLog SET MULTI_USER
--GO
