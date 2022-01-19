CREATE DATABASE CS_DS_Portfolio;
GO

USE CS_DS_Portfolio;
GO

CREATE TABLE [dbo].[ViewTypes] (
	ViewTypeKey INT NOT NULL PRIMARY KEY IDENTITY (1,1),
	ViewTypeName VARCHAR(MAX)
);

CREATE TABLE [dbo].[Views] (
	ViewKey BIGINT NOT NULL PRIMARY KEY IDENTITY (1,1),
	ViewTypeKey INT FOREIGN KEY REFERENCES ViewTypes(ViewTypeKey),
	ViewName VARCHAR(MAX),
	Description VARCHAR(MAX),
	Link VARCHAR(MAX),
	LogoPath VARCHAR(MAX),
	CreatedDate DATETIME,
	CreatedBy INT,
	ModifiedDate DATETIME,
	OrderNumber INT,
	IsDeleted BIT
);

CREATE TABLE [dbo].[ElementTypes] (
	ElementTypeKey INT NOT NULL PRIMARY KEY IDENTITY (1,1),
	ElementTypeName VARCHAR(MAX)
);

CREATE TABLE [dbo].[LinkTypes] (
	LinkTypeKey INT NOT NULL PRIMARY KEY IDENTITY (1,1),
	LinkTypeName VARCHAR(MAX)
);

CREATE TABLE [dbo].[Elements] (
	ElementKey BIGINT NOT NULL PRIMARY KEY IDENTITY (1,1),
	ElementTypeKey INT NOT NULL FOREIGN KEY REFERENCES ElementTypes(ElementTypeKey),
	LinkTypeKey INT FOREIGN KEY REFERENCES LinkTypes(LinkTypeKey),
	ElementName VARCHAR(MAX),
	Path VARCHAR(MAX),
	Text VARCHAR(MAX),
	Value VARCHAR(MAX),
	IsShowElementName BIT,
	IsDeleted BIT
);

CREATE TABLE [dbo].[ElementParameters] (
	ElementParameterKey BIGINT NOT NULL PRIMARY KEY IDENTITY (1,1),
	ElementKey BIGINT FOREIGN KEY REFERENCES Elements(ElementKey),
	[Key] VARCHAR(MAX),
	Value VARCHAR(MAX),
	IsDeleted BIT
);

CREATE TABLE dbo.ViewElements (
	ViewElementKey BIGINT NOT NULL PRIMARY KEY IDENTITY (1,1),
	ViewKey BIGINT FOREIGN KEY REFERENCES Views(ViewKey),
	ElementKey BIGINT FOREIGN KEY REFERENCES Elements(ElementKey),
	OrderNumber INT,
	IsDeleted BIT
);

CREATE TABLE dbo.Directions (
	DirectionKey INT NOT NULL PRIMARY KEY IDENTITY (1,1),
	Name VARCHAR(MAX),
	Link VARCHAR(MAX)
);

CREATE TABLE dbo.Tags (
	TagKey INT NOT NULL PRIMARY KEY IDENTITY (1,1),
	DirectionKey INT FOREIGN KEY REFERENCES Directions(DirectionKey),
	Name VARCHAR(50),
	Link VARCHAR(MAX)
);

CREATE TABLE dbo.ViewTags (
	ViewTagKey BIGINT NOT NULL PRIMARY KEY IDENTITY (1,1),
	ViewKey BIGINT FOREIGN KEY REFERENCES Views(ViewKey),
	TagKey INT FOREIGN KEY REFERENCES Tags(TagKey),
	OrderNumber INT,
	IsDeleted BIT
);

CREATE TABLE dbo.Executors (
	ExecutorKey INT NOT NULL PRIMARY KEY IDENTITY (1,1),
	ExecutorName VARCHAR(MAX),
	ExecutorProfileLink VARCHAR(MAX)
);

CREATE TABLE dbo.ExecutorRoles (
	ExecutorRoleKey INT NOT NULL PRIMARY KEY IDENTITY (1,1),
	RoleName VARCHAR(100)
);

CREATE TABLE dbo.ViewExecutors (
	ViewExecutorKey BIGINT NOT NULL PRIMARY KEY IDENTITY (1,1),
	ViewKey BIGINT FOREIGN KEY REFERENCES Views(ViewKey),
	ExecutorKey INT FOREIGN KEY REFERENCES  Executors(ExecutorKey),
	ExecutorRoleKey INT FOREIGN KEY REFERENCES ExecutorRoles(ExecutorRoleKey),
	OrderNumber INT,
	IsDeleted BIT
);

CREATE TABLE dbo.Groups (
	GroupKey INT NOT NULL PRIMARY KEY IDENTITY (1,1),
	GroupName VARCHAR(MAX),
	IsDeleted BIT
);

CREATE TABLE dbo.GroupViews (
	GroupViewKey BIGINT NOT NULL PRIMARY KEY IDENTITY (1,1),
	ViewKey BIGINT FOREIGN KEY REFERENCES Views(ViewKey),
	GroupKey INT FOREIGN KEY REFERENCES Groups(GroupKey),
	IsDeleted BIT
);

CREATE TABLE dbo.Passwords (
	PasswordKey BIGINT NOT NULL PRIMARY KEY IDENTITY (1,1),
	GroupKey INT FOREIGN KEY REFERENCES Groups(GroupKey),
	PasswordValue Varchar(50),
	CreatedDate Date,
	ExpirationDate Date,
	IsDeleted BIT
);

CREATE TABLE dbo.VisitLogs (
	VisitKey BIGINT NOT NULL PRIMARY KEY IDENTITY (1,1),
	PasswordKey BIGINT FOREIGN KEY REFERENCES Passwords(PasswordKey),
	VisitDate DATETIME,
	IsVisitSuccess BIT,
	VisitLastClickDate DATETIME,
	IpAddress VARCHAR(MAX)
);

CREATE TABLE dbo.VisitViews (
	VisitViewKey BIGINT NOT NULL PRIMARY KEY IDENTITY(1,1),
	VisitKey BIGINT FOREIGN KEY REFERENCES VisitLogs(VisitKey),
	ViewKey BIGINT FOREIGN KEY REFERENCES Views(ViewKey),
	VisitDate DATETIME
);

CREATE TABLE dbo.Feedback (
	FeedbackKey BIGINT NOT NULL PRIMARY KEY IDENTITY (1,1),
	VisitKey BIGINT FOREIGN KEY REFERENCES VisitLogs(VisitKey),
	ViewKey BIGINT FOREIGN KEY REFERENCES [Views](ViewKey),
	Email VARCHAR(MAX),
	[Text] VARCHAR(MAX)
);

CREATE TABLE dbo.ConfigValues(
	ConfigValuesKey BIGINT NOT NULL PRIMARY KEY IDENTITY (1,1), 
	[Key] VARCHAR(100), 
	[Value] VARCHAR(100), 
	IsEnabled BIT
);