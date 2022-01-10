
USE CS_DS_Portfolio;
GO
-------------------------------------------------------------
		--Basic filing start
-------------------------------------------------------------

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

INSERT INTO dbo.Directions ([Name], Link)
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

DECLARE @Programming INT = (SELECT DirectionKey FROM dbo.Directions WHERE [Name] = 'Programming');
DECLARE @Dashboarding INT = (SELECT DirectionKey FROM dbo.Directions WHERE [Name] = 'Dashboarding');
DECLARE @MachineLearning INT = (SELECT DirectionKey FROM dbo.Directions WHERE [Name] = 'Machine Learning');
DECLARE @DataProcessing INT = (SELECT DirectionKey FROM dbo.Directions WHERE [Name] = 'Data processing');
DECLARE @DataVisualisation INT = (SELECT DirectionKey FROM dbo.Directions WHERE [Name] = 'Data visualisation');
DECLARE @Deployment INT = (SELECT DirectionKey FROM dbo.Directions WHERE [Name] = 'Deployment');


INSERT INTO dbo.Tags ([Name], DirectionKey)
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
	('Iframe'),
	('Image');
GO
-------------------------------------------------------------
		--Basic filing end
-------------------------------------------------------------





-------------------------------------------------------------
		--View filing start
-------------------------------------------------------------

DECLARE @ViewName VARCHAR(MAX) = 'Support vector method engine';
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
GO
-------------------------------------------------------------
		--View filing end
-------------------------------------------------------------





-------------------------------------------------------------
		--Group filing start
-------------------------------------------------------------

DECLARE @GroupName VARCHAR(50) = 'Group1';

INSERT INTO [dbo].[Groups](GroupName, IsDeleted) VALUES(@GroupName, 0);

-------------------------------------------------------------
		--Group filing end
-------------------------------------------------------------




-------------------------------------------------------------
		--GroupViews filing start
-------------------------------------------------------------

INSERT INTO [dbo].[GroupViews](ViewKey, GroupKey, IsDeleted) VALUES(1, 1, 0);

-------------------------------------------------------------
		--GroupViews filing end
-------------------------------------------------------------




-------------------------------------------------------------
		--Password filing start
-------------------------------------------------------------

DECLARE @PasswordValue VARCHAR(50) = 'test';
DECLARE @CreateDate DATETIME = GETDATE();
DECLARE @ExpirationDate DATETIME = GETDATE() + 7; --Password has been expired after 7 days

INSERT INTO [dbo].Passwords(GroupKey, PasswordValue, CreatedDate, ExpirationDate, IsDeleted) Values(1, @PasswordValue, @CreateDate, @ExpirationDate, 0);

-------------------------------------------------------------
		--Password filing end
-------------------------------------------------------------




-------------------------------------------------------------
		--ConfigValues filing start
-------------------------------------------------------------

-- In current example we are adding admin email for get info of expiration password inserted
DECLARE @Key VARCHAR(MAX) = 'AdminEmail';
DECLARE @Value VARCHAR(MAX) = 'kdaniilm@gmail.com';

INSERT INTO ConfigValues([Key], [Value], IsEnabled) VALUES('AdminEmail', 'kdaniilm@gmail.com', 1);

-------------------------------------------------------------
		--ConfigValues filing end
-------------------------------------------------------------