
-- ################### SEED DATA ######################

-- Extract data from .csv file and load into our db

-- Create a staging table to hold all the seed data.  We'll query this 
-- table in order to extract what we need to then insert into our real
-- tables.
CREATE TABLE [dbo].[AllData]
(
	[Location] NVARCHAR(50),
	[MeetDate] DATETIME,
	[Team] NVARCHAR(50),
	[Coach] NVARCHAR(50),
	[Event] NVARCHAR(20),
	[Gender] NVARCHAR(20),
	[Athlete] NVARCHAR(50),
	[RaceTime] REAL
);

-- Hard code the full path to the csv file.  It'll be better this way 
-- when we run this in HW 9 to build an Azure db 
BULK INSERT [dbo].[AllData]
	FROM 'C:\Users\mjkli\Documents\CS460\CS460-F19-mjklienst\HW8\Homework8\Homework8\racetimes.csv'
	WITH
	(
		FIELDTERMINATOR = ',',
		ROWTERMINATOR = '\n',
		FIRSTROW = 2
	);

-- Load coaches data, no foreign keys here to worry about so we can 
-- do a straight insert of just the distinct values
INSERT INTO [dbo].[Coaches] ([Name])
	SELECT DISTINCT Coach from [dbo].[AllData];

-- Load Team data, a team has a coach so we need to find the correct entry in the 
-- Coaches table so we can set the foreign key appropriately
INSERT INTO [dbo].[Teams]
	(Name,CoachID)
	SELECT DISTINCT ad.Team,cs.CoachID
		FROM dbo.AllData as ad, dbo.Coaches as cs
		WHERE ad.Coach = cs.Name;

-- Load all the other tables in a similar fashion.  Race results is the hardest since
-- it has several FK's.  Think joins.

INSERT INTO [dbo].[Athletes]
	(Name,Gender,TeamID)
	SELECT DISTINCT ad.Athlete, ad.Gender, ts.TeamID
	FROM dbo.AllData as ad, dbo.Teams as ts
	WHERE ad.Team = ts.Name;

INSERT INTO [dbo].[Meets]
	(Location,MeetDate)
	SELECT DISTINCT ad.Location, ad.MeetDate
	FROM dbo.AllData as ad;

INSERT INTO [dbo].[Events]
	(Event)
	SELECT DISTINCT ad.Event
	FROM dbo.AllData as ad;

INSERT INTO [dbo].[EventAthleteResults]
	(RaceTime,EventID,AthleteID,MeetID)
	SELECT DISTINCT ad.RaceTime, es.EventID, aths.AthleteID, ms.MeetID
	FROM dbo.AllData as ad, dbo.Events as es, dbo.Athletes as aths, dbo.Meets as ms
	WHERE ad.Event = es.Event AND ad.Athlete = aths.Name AND ad.Location = ms.Location AND ad.MeetDate = ms.MeetDate;

-- We don't need this staging table anymore so clear it away
DROP TABLE [dbo].[AllData];
