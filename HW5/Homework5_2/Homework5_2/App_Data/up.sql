CREATE TABLE [dbo].[Homeworks]
(
    [ID]        INT IDENTITY (1,1)    NOT NULL,
    [HomeworkTitle] NVARCHAR(64)        NOT NULL,
    [Notes]    NVARCHAR(128)        NOT NULL,
    [DueDate]        DATE            NOT NULL,
	[DueTime]        TIME            NOT NULL,
    [Department] NVARCHAR(3)        NOT NULL,
	[CourseNum] INT        NOT NULL,
	[Priority] NVARCHAR(10)        NOT NULL,

    CONSTRAINT [PK_dbo.Homeworks] PRIMARY KEY CLUSTERED ([ID] ASC)
);

INSERT INTO [dbo].[Homeworks] (HomeworkTitle, Notes, DueDate, DueTime, Department, CourseNum, Priority) VALUES
    ('CS Project Num 3','Working with Python', '11/05/2019', '11:59', 'CS', '267', 'High'), 
    ('COM Essay','Has to be 12 pages long', '11/05/2019', '10:00', 'COM', '101', 'Medium'),
    ('MTH Take-Home Quiz','Open-note quiz', '11/06/2019', '08:00', 'MTH', '329', 'Low'),
	('Teaching Stuff Idk','Not sure', '11/03/2019', '11:59', 'ED', '218', 'Medium'),
    ('Geocaching Report','Find a geocache and then place one somewhere', '11/12/2019', '10:59', 'PE', '241', 'High')

GO
