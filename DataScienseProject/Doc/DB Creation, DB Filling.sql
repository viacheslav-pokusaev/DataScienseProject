USE master; 
GO 


DROP DATABASE CS_DS_Portfolio
GO
------------------------------------------------------------------------------------------
--								Database creating
------------------------------------------------------------------------------------------

CREATE DATABASE CS_DS_Portfolio;
GO

USE CS_DS_Portfolio;
GO

CREATE TABLE dbo.ViewTypes (
	ViewTypeKey INT NOT NULL PRIMARY KEY IDENTITY (1,1),
	ViewTypeName VARCHAR(MAX)
);

CREATE TABLE dbo.[Views] (
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

CREATE TABLE dbo.ElementTypes (
	ElementTypeKey INT NOT NULL PRIMARY KEY IDENTITY (1,1),
	ElementTypeName VARCHAR(MAX)
);

CREATE TABLE dbo.LinkTypes (
	LinkTypeKey INT NOT NULL PRIMARY KEY IDENTITY (1,1),
	LinkTypeName VARCHAR(MAX)
);

CREATE TABLE dbo.[Elements] (
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

CREATE TABLE dbo.ElementParameters (
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

CREATE TABLE dbo.ConfigValues(ConfigValuesKey BIGINT NOT NULL PRIMARY KEY IDENTITY (1,1), [Key] VARCHAR(100), [Value] VARCHAR(100), IsEnabled BIT);

------------------------------------------------------------------------------------------
--								Database filling
------------------------------------------------------------------------------------------


------------------------------------------------------------------------------------------
--								Basic values
------------------------------------------------------------------------------------------

INSERT INTO dbo.Executors ( ExecutorName, ExecutorProfileLink)
VALUES
	('Pavel Pikuza', 'https://www.upwork.com/o/profiles/users/~01e07dc990b0919b3c/'),
	('Viktoriia Hrynchenko', 'https://www.upwork.com/o/profiles/users/~01db92a08836893bff/'),
	('Alexandr Belyaev','https://www.upwork.com/o/profiles/users/~01f639f5656cb7c1f2/');

INSERT INTO dbo.ExecutorRoles (RoleName) 
VALUES 
	('Development'),
	('Mentoring'),
	('Requirament preparations'),
	('Testing'),
	('Support');

INSERT INTO dbo.Directions ( Name, Link)
VALUES
	('Programming', NULL),
	('Dashboarding', NULL),
	('Machine Learning', 'https://en.wikipedia.org/wiki/Machine_learning'),
	('Data processing', 'https://en.wikipedia.org/wiki/Data_processing'),
	('Data visualisation','https://en.wikipedia.org/wiki/Data_visualization'),
	('Deployment',NULL);

INSERT INTO dbo.ViewTypes (ViewTypeName) 
VALUES
	('Test task'),
	('Study task'),
	('Project');

DECLARE @Programming INT = (SELECT DirectionKey FROM dbo.Directions WHERE Name = 'Programming');
DECLARE @Dashboarding INT = (SELECT DirectionKey FROM dbo.Directions WHERE Name = 'Dashboarding');
DECLARE @MachineLearning INT = (SELECT DirectionKey FROM dbo.Directions WHERE Name = 'Machine Learning');
DECLARE @DataProcessing INT = (SELECT DirectionKey FROM dbo.Directions WHERE Name = 'Data processing');
DECLARE @DataVisualisation INT = (SELECT DirectionKey FROM dbo.Directions WHERE Name = 'Data visualisation');
DECLARE @Deployment INT = (SELECT DirectionKey FROM dbo.Directions WHERE Name = 'Deployment');


INSERT INTO dbo.Tags (Name, DirectionKey)
VALUES
	('R', @Programming),
	('R Shany', @Dashboarding),
	('Tidyverse', @DataProcessing),
	('Ggplot', @DataVisualisation),
	('Plotly', @DataVisualisation),
	('Python', @Programming),
	('Pandas', @DataProcessing),
	('Numpy', @Programming),
	('Scikit-learn', @MachineLearning),
	('Tensoflow', @MachineLearning),
	('Keras', @MachineLearning),
	('PowerBi', @Dashboarding),
	('API integration', @Programming),
	('DAX', @DataProcessing),
	('PowerQuery', @DataProcessing),
	('Excel', @DataProcessing),
	('Map', @DataVisualisation),
	('Tableau Desktop', @Dashboarding),
	('Tableau Prep Builder', @DataProcessing),
	('MSSQL', @DataProcessing),
	('Data Cleaning', @DataProcessing),
	('Stored Procedure', @Programming),
	('.net console', @Programming),
	('DOS batch', @Programming),
	('Google Sheet', @DataProcessing),
	('Google Drive', @DataProcessing),
	('Technical analysis', @MachineLearning),
	('Classification', @MachineLearning),
	('Regression', @MachineLearning),
	('XG-boost', @MachineLearning),
	('KNN', @MachineLearning),
	('SVM', @MachineLearning),
	('Neural Network', @MachineLearning),
	('Decision tree', @MachineLearning),
	('Docker', @Deployment);

INSERT INTO LinkTypes (LinkTypeName)
VALUES
	('Docker (Shiny)'),
	('Docker (RMarkdown)'),
	('Docker (Plotly)'),
	('Tableau Public'),
	('PowerBi Online');

INSERT INTO ElementTypes (ElementTypeName)
VALUES
	('Sentence'),
	('Html paragraph'),
	('Header Description'), 
	('Iframe'),
	('Image'),
	('Header Image');
	
--------------------------------------------------------------------------------------------
--						View's adding
--------------------------------------------------------------------------------------------


--------------------------------------------------------------------------------------------
--						Support vector method engine
--------------------------------------------------------------------------------------------

DECLARE @ViewName VARCHAR(MAX) = 'Support vector method engine';
DECLARE @ViewTypeName VARCHAR(MAX) = 'Study task';


-- Don't change
DECLARE @ElementTypeKey_HeaderDescription INT = (SELECT ElementTypeKey FROM dbo.ElementTypes WHERE ElementTypeName = 'Header Description');
DECLARE @ElementTypeKey_HtmlParagraph INT = (SELECT ElementTypeKey FROM dbo.ElementTypes WHERE ElementTypeName = 'Html paragraph');
DECLARE @ElementTypeKey_Iframe INT = (SELECT ElementTypeKey FROM dbo.ElementTypes WHERE ElementTypeName = 'Iframe');
DECLARE @ElementTypeKey_Image INT = (SELECT ElementTypeKey FROM dbo.ElementTypes WHERE ElementTypeName = 'Image');
DECLARE @ElementTypeKey_HeaderImage INT = (SELECT ElementTypeKey FROM dbo.ElementTypes WHERE ElementTypeName = 'Header Image');

DECLARE @LinkType_DockerShiny INt = (SELECT LinkTypeKey FROM dbo.LinkTypes WHERE LinkTypeName = 'Docker (Shiny)');
-- Don't change END

INSERT INTO [Views] (ViewTypeKey, ViewName, Description, Link, LogoPath, CreatedDate, ModifiedDate, OrderNumber, isDeleted)
VALUES
	((SELECT ViewTypeKey FROM dbo.ViewTypes WHERE ViewTypeName = @ViewTypeName), @ViewName, NULL, NULL,	NULL, (SELECT GETDATE()), (SELECT GETDATE()), 1, 0);

DECLARE @ViewKey INT = (SELECT ViewKey FROM [Views] WHERE ViewName = @ViewName);

INSERT INTO Elements (ElementTypeKey, LinkTypeKey, ElementName, Path, Text, Value, IsShowElementName, IsDeleted)
VALUES
	(@ElementTypeKey_HeaderDescription, NULL, 'Header', NULL, NULL, 'Header test', 1, 0),
	(@ElementTypeKey_HeaderImage, NULL, 'Header Img', 'https://image.shutterstock.com/image-vector/picture-icon-600w-323592404.jpg', 'SVM Fisher''s Irises linear models', NULL, 0, 0),
	(@ElementTypeKey_Image, @LinkType_DockerShiny, 'Img1', 'D:\OneDrive\OneDrive - PiK Case, SIA\wrk\cstm_sltns\proj_galery\Proj_example\svme-logo.png', 'SVM Fisher''s Irises linear models', NULL, 0, 0),
	(@ElementTypeKey_HtmlParagraph, NULL, 'Introduction', NULL, NULL, 'SVM is one of the most popular methods for model building. Instead of reading scientific books, just look at how it really works. You need only just click 2 buttons: Show example -> Create Model<br>The example uses data from the very famous dataset - <a href="https://en.wikipedia.org/wiki/Iris_flower_data_set">Fisher''s Irises</a>. The goal - identify the type of iris by the size of the leaf. Check it,  is it possible?!', 0, 0 ),
	(@ElementTypeKey_HtmlParagraph, NULL, 'How does SVM work?', NULL, NULL, 'There are a lot of articles with detailed math explanations like <a href="https://scikit-learn.org/stable/modules/svm.html">this </a> or <a href = "https://monkeylearn.com/blog/introduction-to-support-vector-machines-svm/">that</a>. We want to say only a general idea - the SVM method tries to find the answer to two questions:<br> 1. Could two or more groups of points be split correctly using some geometric figure? The simplest case is using line.<br> 2. Which figure (line) is the best one?<br> How to determine "the best line"? SVM thinks that the best line should be as far from both groups of points as possible.<br>In plot #2 you can see 5 lines that SVM found after the first iteration. Legend contains distance from each line to the points. Obviously that now the best one line has 0.145 (virtual unites) to the nearest point. Now time to improve our model.', 1, 0 ),
	(@ElementTypeKey_Iframe, @LinkType_DockerShiny, 'Embed1', NULL, NULL, NULL, 0, 0),
	(@ElementTypeKey_HtmlParagraph, NULL, 'Model improving', NULL, NULL, 'Before the improving model I want notice that you can change the count of resulted lines. I think that best count of models is from 5 to 10. I select 10 models, but you can try your''s.<br> Click button ''Improve model''.<br> Now the best distance is 0.1575. Cool!<br> That''s time to explain the last plot. Mathematicians say that each line in the Decart coordinate system could be determined by the slope to the Ox axis and the intercept with Oy axis. More details see <a href="http://www.math.com/school/subject2/lessons/S2U4L2GL.html">here</a>. So each point on this plot is a potential line. The color shows the distance from this line to the two groups of points. Then the lightest point is better. Red points are our solutions.<br> As you can see Sentosa and Versicolor were easily separated one from another because there is a big distance between two groups of points. What happened if two groups of points are mixed up? Good question! Let''s try to do that.<br>', 1, 0),
	(@ElementTypeKey_HtmlParagraph, NULL, 'Mixed groups', NULL, NULL, 'Let''s back to our Shiny application, to the Step#3 and change Sentosa to the Virginica in the first drop-down list. Now we can recreate and improve model using familiar buttons.', 1, 0);

INSERT INTO ElementParameters (ElementKey, [Key], Value, IsDeleted)
VALUES
	((SELECT ElementKey FROM dbo.Elements WHERE ElementName = 'Img1'), 'height', '90%', 0),
	((SELECT ElementKey FROM dbo.Elements WHERE ElementName = 'Img1'), 'width', '90%', 0),
	((SELECT ElementKey FROM dbo.Elements WHERE ElementName = 'Embed1'), 'src', 'http://212.3.101.119:3820', 0),
	((SELECT ElementKey FROM dbo.Elements WHERE ElementName = 'Embed1'), 'width', '100%', 1),
	((SELECT ElementKey FROM dbo.Elements WHERE ElementName = 'Embed1'), 'height', '600px', 0),
	((SELECT ElementKey FROM dbo.Elements WHERE ElementName = 'Embed1'), 'frameborder', '1', 0);

INSERT INTO ViewElements (ViewKey, ElementKey, OrderNumber, IsDeleted)
VALUES 
	(@ViewKey, (SELECT ElementKey FROM dbo.Elements WHERE ElementName = 'Header'), 1, 0),
	(@ViewKey, (SELECT ElementKey FROM dbo.Elements WHERE ElementName = 'Header Img'), 1, 0),
	(@ViewKey, (SELECT ElementKey FROM dbo.Elements WHERE ElementName = 'Img1'), 1, 0),
	(@ViewKey, (SELECT ElementKey FROM dbo.Elements WHERE ElementName = 'Introduction'), 2, 0),
	(@ViewKey, (SELECT ElementKey FROM dbo.Elements WHERE ElementName = 'How does SVM work?'), 3, 0),
	(@ViewKey, (SELECT ElementKey FROM dbo.Elements WHERE ElementName = 'Embed1'), 4, 0),
	(@ViewKey, (SELECT ElementKey FROM dbo.Elements WHERE ElementName = 'Model improving'), 5, 0),
	(@ViewKey, (SELECT ElementKey FROM dbo.Elements WHERE ElementName = 'Mixed groups'), 6, 1);

INSERT INTO ViewTags (ViewKey, TagKey, OrderNumber, IsDeleted)
VALUES 
	(@ViewKey,(SELECT TagKey FROM dbo.Tags WHERE Name = 'Classification'),1,0),
	(@ViewKey,(SELECT TagKey FROM dbo.Tags WHERE Name = 'SVM'),2,0),
	(@ViewKey,(SELECT TagKey FROM dbo.Tags WHERE Name = 'R Shany'),3,0),
	(@ViewKey,(SELECT TagKey FROM dbo.Tags WHERE Name = 'Ggplot'),4,0),
	(@ViewKey,(SELECT TagKey FROM dbo.Tags WHERE Name = 'Docker'),5,0);

INSERT INTO ViewExecutors (ViewKey, ExecutorKey, ExecutorRoleKey, OrderNumber, IsDeleted)
VALUES
	(@ViewKey, (SELECT ExecutorKey FROM dbo.Executors WHERE ExecutorName = 'Pavel Pikuza'),(SELECT ExecutorRoleKey FROM dbo.ExecutorRoles WHERE RoleName = 'Development'), 1, 0);


---------------------------------------------------------------------------------------------
--
---------------------------------------------------------------------------------------------
GO

--SP GetViewFullInfo start

DECLARE @ViewKey INT = 1;

--1
SELECT 
	ViewName 
	, ViewTypeName
FROM [dbo].[Views] v
LEFT JOIN dbo.ViewTypes vt ON vt.ViewTypeKey = v.ViewTypeKey
WHERE
	ViewKey = @ViewKey
	AND v.IsDeleted = 0

--2
SELECT 
	e.ExecutorName
	, e.ExecutorProfileLink
	, er.RoleName
	, ve.OrderNumber
FROM dbo.ViewExecutors ve
LEFT JOIN dbo.Executors e ON e.ExecutorKey = ve.ExecutorKey
LEFT JOIN dbo.ExecutorRoles er ON er.ExecutorRoleKey = ve.ExecutorRoleKey
WHERE
	ViewKey = @ViewKey
	AND ve.IsDeleted = 0
ORDER BY
	ve.OrderNumber
	
--3
SELECT  
	t.Name
	, t.Link
	, d.Name
	, d.Link
	,vt.OrderNumber
FROM dbo.ViewTags vt 
LEFT JOIN dbo.Tags t ON t.TagKey = vt.TagKey
LEFT JOIN dbo.Directions d ON t.DirectionKey = d.DirectionKey
WHERE
	vt.ViewKey = @ViewKey
	AND vt.IsDeleted = 0
ORDER BY
	vt.OrderNumber

--4
SELECT 
	e.ElementName
	, e.Value
	, e.Path
	, e.Text
	, et.ElementTypeName
	, ve.OrderNumber
	, e.IsShowElementName
FROM [dbo].[Views] v
LEFT JOIN dbo.ViewElements ve ON ve.ViewKey = v.ViewKey AND ve.IsDeleted = 0
LEFT JOIN dbo.Elements e ON e.ElementKey = ve.ElementKey AND e.IsDeleted = 0
LEFT JOIN dbo.ElementTypes et ON et.ElementTypeKey = e.ElementTypeKey
WHERE
	v.ViewKey = @ViewKey
ORDER BY
	ve.OrderNumber

--5
SELECT 
	e.ElementName
	, et.ElementTypeName
	, ep.[Key]
	, ep.Value
FROM dbo.Elements e
LEFT JOIN dbo.ViewElements ve ON ve.ElementKey = e.ElementKey
LEFT JOIN dbo.ElementTypes et ON et.ElementTypeKey = e.ElementTypeKey
RIGHT JOIN dbo.ElementParameters ep ON ep.ElementKey = e.ElementKey AND ep.IsDeleted = 0
WHERE
	ve.ViewKey = @ViewKey
	AND e.IsDeleted = 0

--SP GetViewFullInfo end


GO

--GroupViews filing end


--Views filling start
--------------------------------------------------------------------------------------------
--						Test1
--------------------------------------------------------------------------------------------

DECLARE @ViewName VARCHAR(MAX) = 'Test1';
DECLARE @ViewTypeName VARCHAR(MAX) = 'Study task';


-- Don't change
DECLARE @ElementTypeKey_HtmlParagraph INT = (SELECT ElementTypeKey FROM dbo.ElementTypes WHERE ElementTypeName = 'Html paragraph');
DECLARE @ElementTypeKey_Iframe INT = (SELECT ElementTypeKey FROM dbo.ElementTypes WHERE ElementTypeName = 'Iframe');
DECLARE @ElementTypeKey_Image INT = (SELECT ElementTypeKey FROM dbo.ElementTypes WHERE ElementTypeName = 'Image');

DECLARE @LinkType_DockerShiny INt = (SELECT LinkTypeKey FROM dbo.LinkTypes WHERE LinkTypeName = 'Docker (Shiny)');
-- Don't change END

INSERT INTO [Views] (ViewTypeKey, ViewName, Description, Link, LogoPath, CreatedDate, ModifiedDate, OrderNumber, isDeleted)
VALUES
	((SELECT ViewTypeKey FROM dbo.ViewTypes WHERE ViewTypeName = @ViewTypeName), @ViewName, NULL, NULL,	NULL, (SELECT GETDATE()), (SELECT GETDATE()), 1, 0);

DECLARE @ViewKey INT = (SELECT ViewKey FROM [Views] WHERE ViewName = @ViewName);

INSERT INTO Elements (ElementTypeKey, LinkTypeKey, ElementName, Path, Text, Value, IsShowElementName, IsDeleted)
VALUES
	(@ElementTypeKey_Image, @LinkType_DockerShiny, 'Img2', NULL, 'Test text', NULL, 0, 0),
	(@ElementTypeKey_HtmlParagraph, NULL, 'Introduction2', NULL, NULL, 'SVM is one of the most popular methods for model building. Instead of reading scientific books, just look at how it really works. You need only just click 2 buttons: Show example -> Create Model<br>The example uses data from the very famous dataset - <a href="https://en.wikipedia.org/wiki/Iris_flower_data_set">Fisher''s Irises</a>. The goal - identify the type of iris by the size of the leaf. Check it,  is it possible?!', 0, 0 ),
	(@ElementTypeKey_HtmlParagraph, NULL, 'How does SVM work2?', NULL, NULL, 'There are a lot of articles with detailed math explanations like <a href="https://scikit-learn.org/stable/modules/svm.html">this </a> or <a href = "https://monkeylearn.com/blog/introduction-to-support-vector-machines-svm/">that</a>. We want to say only a general idea - the SVM method tries to find the answer to two questions:<br> 1. Could two or more groups of points be split correctly using some geometric figure? The simplest case is using line.<br> 2. Which figure (line) is the best one?<br> How to determine "the best line"? SVM thinks that the best line should be as far from both groups of points as possible.<br>In plot #2 you can see 5 lines that SVM found after the first iteration. Legend contains distance from each line to the points. Obviously that now the best one line has 0.145 (virtual unites) to the nearest point. Now time to improve our model.', 1, 0 ),
	(@ElementTypeKey_Iframe, @LinkType_DockerShiny, 'Embed2', NULL, NULL, NULL, 0, 0),
	(@ElementTypeKey_HtmlParagraph, NULL, 'Model improving2', NULL, NULL, 'Before the improving model I want notice that you can change the count of resulted lines. I think that best count of models is from 5 to 10. I select 10 models, but you can try your''s.<br> Click button ''Improve model''.<br> Now the best distance is 0.1575. Cool!<br> That''s time to explain the last plot. Mathematicians say that each line in the Decart coordinate system could be determined by the slope to the Ox axis and the intercept with Oy axis. More details see <a href="http://www.math.com/school/subject2/lessons/S2U4L2GL.html">here</a>. So each point on this plot is a potential line. The color shows the distance from this line to the two groups of points. Then the lightest point is better. Red points are our solutions.<br> As you can see Sentosa and Versicolor were easily separated one from another because there is a big distance between two groups of points. What happened if two groups of points are mixed up? Good question! Let''s try to do that.<br>', 1, 0),
	(@ElementTypeKey_HtmlParagraph, NULL, 'Mixed groups2', NULL, NULL, 'Let''s back to our Shiny application, to the Step#3 and change Sentosa to the Virginica in the first drop-down list. Now we can recreate and improve model using familiar buttons.', 1, 0);

INSERT INTO ElementParameters (ElementKey, [Key], Value, IsDeleted)
VALUES 
	((SELECT ElementKey FROM dbo.Elements WHERE ElementName = 'Img2'), 'height', '80%', 0),
	((SELECT ElementKey FROM dbo.Elements WHERE ElementName = 'Img2'), 'width', '80%', 0),
	((SELECT ElementKey FROM dbo.Elements WHERE ElementName = 'Embed2'), 'src', 'http://212.3.101.119:3820', 0),
	((SELECT ElementKey FROM dbo.Elements WHERE ElementName = 'Embed2'), 'width', '100%', 0),
	((SELECT ElementKey FROM dbo.Elements WHERE ElementName = 'Embed2'), 'height', '400px', 0),
	((SELECT ElementKey FROM dbo.Elements WHERE ElementName = 'Embed2'), 'frameborder', '1', 0);

INSERT INTO ViewElements (ViewKey, ElementKey, OrderNumber, IsDeleted)
VALUES 
	(@ViewKey, (SELECT ElementKey FROM dbo.Elements WHERE ElementName = 'Img2'), 1, 0),
	(@ViewKey, (SELECT ElementKey FROM dbo.Elements WHERE ElementName = 'Introduction2'), 2, 0),
	(@ViewKey, (SELECT ElementKey FROM dbo.Elements WHERE ElementName = 'How does SVM work2?'), 3, 0),
	(@ViewKey, (SELECT ElementKey FROM dbo.Elements WHERE ElementName = 'Embed2'), 4, 0),
	(@ViewKey, (SELECT ElementKey FROM dbo.Elements WHERE ElementName = 'Model improving2'), 5, 0),
	(@ViewKey, (SELECT ElementKey FROM dbo.Elements WHERE ElementName = 'Mixed groups2'), 6, 1);

INSERT INTO ViewTags (ViewKey, TagKey, OrderNumber, IsDeleted)
VALUES 
	(@ViewKey,(SELECT TagKey FROM dbo.Tags WHERE Name = 'Classification'),1,0),
	(@ViewKey,(SELECT TagKey FROM dbo.Tags WHERE Name = 'SVM'),2,0),
	(@ViewKey,(SELECT TagKey FROM dbo.Tags WHERE Name = 'R Shany'),3,0),
	(@ViewKey,(SELECT TagKey FROM dbo.Tags WHERE Name = 'Ggplot'),4,0),
	(@ViewKey,(SELECT TagKey FROM dbo.Tags WHERE Name = 'Docker'),5,0);

INSERT INTO ViewExecutors (ViewKey, ExecutorKey, ExecutorRoleKey, OrderNumber, IsDeleted)
VALUES
	(@ViewKey, (SELECT ExecutorKey FROM dbo.Executors WHERE ExecutorName = 'Pavel Pikuza'),(SELECT ExecutorRoleKey FROM dbo.ExecutorRoles WHERE RoleName = 'Development'), 1, 0);
GO

--------------------------------------------------------------------------------------------
--						Test2
--------------------------------------------------------------------------------------------

DECLARE @ViewName VARCHAR(MAX) = 'Test2';
DECLARE @ViewTypeName VARCHAR(MAX) = 'Study task';


-- Don't change
DECLARE @ElementTypeKey_HtmlParagraph INT = (SELECT ElementTypeKey FROM dbo.ElementTypes WHERE ElementTypeName = 'Html paragraph');
DECLARE @ElementTypeKey_Iframe INT = (SELECT ElementTypeKey FROM dbo.ElementTypes WHERE ElementTypeName = 'Iframe');
DECLARE @ElementTypeKey_Image INT = (SELECT ElementTypeKey FROM dbo.ElementTypes WHERE ElementTypeName = 'Image');

DECLARE @LinkType_DockerShiny INt = (SELECT LinkTypeKey FROM dbo.LinkTypes WHERE LinkTypeName = 'Docker (Shiny)');
-- Don't change END

INSERT INTO [Views] (ViewTypeKey, ViewName, Description, Link, LogoPath, CreatedDate, ModifiedDate, OrderNumber, isDeleted)
VALUES
	((SELECT ViewTypeKey FROM dbo.ViewTypes WHERE ViewTypeName = @ViewTypeName), @ViewName, NULL, NULL,	NULL, (SELECT GETDATE()), (SELECT GETDATE()), 1, 0);

DECLARE @ViewKey INT = (SELECT ViewKey FROM [Views] WHERE ViewName = @ViewName);

INSERT INTO [Elements] (ElementTypeKey, LinkTypeKey, ElementName, Path, Text, Value, IsShowElementName, IsDeleted)
VALUES
	(@ElementTypeKey_Image, @LinkType_DockerShiny, 'Img3', NULL, 'Test text', NULL, 0, 0),
	(@ElementTypeKey_HtmlParagraph, NULL, 'Introduction3', NULL, NULL, 'SVM is one of the most popular methods for model building. Instead of reading scientific books, just look at how it really works. You need only just click 2 buttons: Show example -> Create Model<br>The example uses data from the very famous dataset - <a href="https://en.wikipedia.org/wiki/Iris_flower_data_set">Fisher''s Irises</a>. The goal - identify the type of iris by the size of the leaf. Check it,  is it possible?!', 0, 0 ),
	(@ElementTypeKey_HtmlParagraph, NULL, 'How does SVM work3?', NULL, NULL, 'There are a lot of articles with detailed math explanations like <a href="https://scikit-learn.org/stable/modules/svm.html">this </a> or <a href = "https://monkeylearn.com/blog/introduction-to-support-vector-machines-svm/">that</a>. We want to say only a general idea - the SVM method tries to find the answer to two questions:<br> 1. Could two or more groups of points be split correctly using some geometric figure? The simplest case is using line.<br> 2. Which figure (line) is the best one?<br> How to determine "the best line"? SVM thinks that the best line should be as far from both groups of points as possible.<br>In plot #2 you can see 5 lines that SVM found after the first iteration. Legend contains distance from each line to the points. Obviously that now the best one line has 0.145 (virtual unites) to the nearest point. Now time to improve our model.', 1, 0 ),
	(@ElementTypeKey_Iframe, @LinkType_DockerShiny, 'Embed3', NULL, NULL, NULL, 0, 0),
	(@ElementTypeKey_HtmlParagraph, NULL, 'Model improving3', NULL, NULL, 'Before the improving model I want notice that you can change the count of resulted lines. I think that best count of models is from 5 to 10. I select 10 models, but you can try your''s.<br> Click button ''Improve model''.<br> Now the best distance is 0.1575. Cool!<br> That''s time to explain the last plot. Mathematicians say that each line in the Decart coordinate system could be determined by the slope to the Ox axis and the intercept with Oy axis. More details see <a href="http://www.math.com/school/subject2/lessons/S2U4L2GL.html">here</a>. So each point on this plot is a potential line. The color shows the distance from this line to the two groups of points. Then the lightest point is better. Red points are our solutions.<br> As you can see Sentosa and Versicolor were easily separated one from another because there is a big distance between two groups of points. What happened if two groups of points are mixed up? Good question! Let''s try to do that.<br>', 1, 0),
	(@ElementTypeKey_HtmlParagraph, NULL, 'Mixed groups3', NULL, NULL, 'Let''s back to our Shiny application, to the Step#3 and change Sentosa to the Virginica in the first drop-down list. Now we can recreate and improve model using familiar buttons.', 1, 0);

INSERT INTO ElementParameters (ElementKey, [Key], Value, IsDeleted)
VALUES 
	((SELECT ElementKey FROM dbo.Elements WHERE ElementName = 'Img3'), 'height', '80%', 0),
	((SELECT ElementKey FROM dbo.Elements WHERE ElementName = 'Img3'), 'width', '80%', 0),
	((SELECT ElementKey FROM dbo.Elements WHERE ElementName = 'Embed3'), 'src', 'http://212.3.101.119:3820', 0),
	((SELECT ElementKey FROM dbo.Elements WHERE ElementName = 'Embed3'), 'width', '100%', 0),
	((SELECT ElementKey FROM dbo.Elements WHERE ElementName = 'Embed3'), 'height', '400px', 0),
	((SELECT ElementKey FROM dbo.Elements WHERE ElementName = 'Embed3'), 'frameborder', '1', 0);

INSERT INTO ViewElements (ViewKey, ElementKey, OrderNumber, IsDeleted)
VALUES 
	(@ViewKey, (SELECT ElementKey FROM dbo.Elements WHERE ElementName = 'Img3'), 1, 0),
	(@ViewKey, (SELECT ElementKey FROM dbo.Elements WHERE ElementName = 'Introduction3'), 2, 0),
	(@ViewKey, (SELECT ElementKey FROM dbo.Elements WHERE ElementName = 'How does SVM work3?'), 3, 0),
	(@ViewKey, (SELECT ElementKey FROM dbo.Elements WHERE ElementName = 'Embed3'), 4, 0),
	(@ViewKey, (SELECT ElementKey FROM dbo.Elements WHERE ElementName = 'Model improving3'), 5, 0),
	(@ViewKey, (SELECT ElementKey FROM dbo.Elements WHERE ElementName = 'Mixed groups3'), 6, 1);

INSERT INTO ViewTags (ViewKey, TagKey, OrderNumber, IsDeleted)
VALUES 
	(@ViewKey,(SELECT TagKey FROM dbo.Tags WHERE Name = 'Classification'),1,0),
	(@ViewKey,(SELECT TagKey FROM dbo.Tags WHERE Name = 'SVM'),2,0),
	(@ViewKey,(SELECT TagKey FROM dbo.Tags WHERE Name = 'R Shany'),3,0),
	(@ViewKey,(SELECT TagKey FROM dbo.Tags WHERE Name = 'Ggplot'),4,0),
	(@ViewKey,(SELECT TagKey FROM dbo.Tags WHERE Name = 'Docker'),5,0);

INSERT INTO ViewExecutors (ViewKey, ExecutorKey, ExecutorRoleKey, OrderNumber, IsDeleted)
VALUES
	(@ViewKey, (SELECT ExecutorKey FROM dbo.Executors WHERE ExecutorName = 'Alexandr Belyaev'),(SELECT ExecutorRoleKey FROM dbo.ExecutorRoles WHERE RoleName = 'Development'), 1, 0);

GO

--Views filling end

--Group filing start
INSERT INTO [dbo].[Groups](GroupName, IsDeleted) VALUES('Group1', 0);
INSERT INTO [dbo].[Groups](GroupName, IsDeleted) VALUES('Group2', 0);
GO
--Group filing end


--GroupViews filing start
INSERT INTO [dbo].[GroupViews](ViewKey, GroupKey, IsDeleted) VALUES(1, 1, 0);
INSERT INTO [dbo].[GroupViews](ViewKey, GroupKey, IsDeleted) VALUES(2, 1, 0);
INSERT INTO [dbo].[GroupViews](ViewKey, GroupKey, IsDeleted) VALUES(3, 2, 0);
GO
--Group select start

--Password filing start

INSERT INTO [dbo].Passwords(GroupKey, PasswordValue, CreatedDate, ExpirationDate, IsDeleted) Values(1, 'test', GETDATE() - 2, GETDATE() - 1, 0);
	INSERT INTO [dbo].Passwords(GroupKey, PasswordValue, CreatedDate, ExpirationDate, IsDeleted) Values(1, 'group1', GETDATE(), GETDATE() + 7, 0);
	INSERT INTO [dbo].Passwords(GroupKey, PasswordValue, CreatedDate, ExpirationDate, IsDeleted) Values(2, 'group2', GETDATE(), GETDATE() + 7, 0);
	INSERT INTO [dbo].Passwords(GroupKey, PasswordValue, CreatedDate, ExpirationDate, IsDeleted) Values(1, 'group1.2', GETDATE(), GETDATE() + 30, 0);
	GO
--Password filing end

--
	SELECT * FROM ConfigValues;

	INSERT INTO ConfigValues([Key], [Value], IsEnabled) VALUES('AdminEmail', 'kdaniilm@gmail.com', 1);

--1
DECLARE @GroupName Varchar(50) = 'Group1';

SELECT
	g.GroupName,
	v.ViewName,
	e.ExecutorName,
	t.Name
	FROM [dbo].GroupViews gv
	LEFT JOIN dbo.Groups g ON g.GroupName = @GroupName AND g.IsDeleted = 0
	LEFT JOIN dbo.[Views] v ON v.ViewKey = gv.ViewKey
	LEFT JOIN dbo.[ViewExecutors] ve ON ve.ViewKey = v.ViewKey
	LEFT JOIN dbo.[Executors] e ON e.ExecutorKey = ve.ExecutorKey
	LEFT JOIN dbo.[ViewTags] vt ON vt.ViewKey = v.ViewKey
	LEFT JOIN dbo.[Tags] t ON t.TagKey = vt.ViewTagKey
	WHERE gv.IsDeleted = 0 AND gv.GroupKey = g.GroupKey AND gv.ViewKey = v.ViewKey
	ORDER BY v.OrderNumber
	GO
--2
DECLARE @GroupName Varchar(50) = 'Group1';

SELECT
	v.ViewName,
	v.ViewKey,
	v.OrderNumber
	FROM [dbo].GroupViews gv
	LEFT JOIN dbo.Groups g ON g.GroupName = @GroupName AND g.IsDeleted = 0
	LEFT JOIN dbo.[Views] v ON v.ViewKey = gv.ViewKey
	WHERE gv.GroupKey = g.GroupKey
	ORDER BY v.OrderNumber 

DECLARE @VIewKey INT = 1
SELECT
	e.ExecutorName,
	er.RoleName
	FROM [dbo].ViewExecutors ve
	LEFT JOIN Executors e ON e.ExecutorKey = ve.ExecutorKey
	LEFT JOIN ExecutorRoles er ON er.ExecutorRoleKey = ve.ExecutorRoleKey
	WHERE ve.ViewKey = @VIewKey AND ve.IsDeleted = 0

SELECT 
	t.[Name]
	FROM [dbo].ViewTags vt
	LEFT JOIN Tags t ON t.TagKey = vt.TagKey
	WHERE vt.ViewKey = @VIewKey AND vt.IsDeleted = 0


DECLARE @ElementName Varchar(50) = 'Introduction'

SELECT 
	 e.Value	
FROM [dbo].[Views] v
LEFT JOIN dbo.ViewElements ve ON ve.ViewKey = v.ViewKey AND ve.IsDeleted = 0
LEFT JOIN dbo.Elements e ON e.ElementKey = ve.ElementKey 
WHERE
	e.ElementName = @ElementName AND 
	v.ViewKey = @VIewKey


	select * from Passwords

	

--Group select end