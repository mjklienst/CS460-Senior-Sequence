CREATE TABLE [dbo].[Coaches]
(
    [CoachID]        INT IDENTITY (1,1)    NOT NULL,
    [Name] NVARCHAR(50)        NOT NULL,

    CONSTRAINT [PK_dbo.Coaches] PRIMARY KEY CLUSTERED ([CoachID] ASC)
);

CREATE TABLE [dbo].[Teams]
(
    [TeamID]        INT IDENTITY (1,1)    NOT NULL,
    [Name] NVARCHAR(50)        NOT NULL,
	[CoachID] INT NOT NULL,

    CONSTRAINT [PK_dbo.Teams] PRIMARY KEY CLUSTERED ([TeamID] ASC),
	CONSTRAINT[FK_dbo.Teams] FOREIGN KEY (CoachID) REFERENCES Coaches(CoachID)

);

CREATE TABLE [dbo].[Athletes]
(
    [AthleteID]        INT IDENTITY (1,1)    NOT NULL,
    [Name] NVARCHAR(50)        NOT NULL,
	[Gender] NVARCHAR(20)        NOT NULL,
	[TeamID] INT NOT NULL,

    CONSTRAINT [PK_dbo.Athletes] PRIMARY KEY CLUSTERED ([AthleteID] ASC),
	CONSTRAINT[FK_dbo.Athletes] FOREIGN KEY (TeamID) REFERENCES Teams(TeamID)

);

CREATE TABLE [dbo].[Meets]
(
    [MeetID]        INT IDENTITY (1,1)    NOT NULL,
    [Location] NVARCHAR(50)        NOT NULL,
	[MeetDate] DATETIME        NOT NULL,

    CONSTRAINT [PK_dbo.Meets] PRIMARY KEY CLUSTERED ([MeetID] ASC)
);

CREATE TABLE [dbo].[Events]
(
    [EventID]        INT IDENTITY (1,1)    NOT NULL,
    [Event] NVARCHAR(20)        NOT NULL,

    CONSTRAINT [PK_dbo.Events] PRIMARY KEY CLUSTERED ([EventID] ASC)
);

CREATE TABLE [dbo].[EventAthleteResults]
(
    [EARID]        INT IDENTITY (1,1)    NOT NULL,
    [RaceTime] REAL        NOT NULL,
	[EventID] INT NOT NULL,
	[AthleteID] INT NOT NULL,
	[MeetID] INT NOT NULL,

    CONSTRAINT [PK_dbo.EventAthleteResults] PRIMARY KEY CLUSTERED ([EARID] ASC),
	CONSTRAINT[FK1_dbo.EventAthleteResults] FOREIGN KEY (EventID) REFERENCES Events(EventID),
	CONSTRAINT[FK2_dbo.EventAthleteResults] FOREIGN KEY (AthleteID) REFERENCES Athletes(AthleteID),
	CONSTRAINT[FK3_dbo.EventAthleteResults] FOREIGN KEY (MeetID) REFERENCES Meets(MeetID)


);