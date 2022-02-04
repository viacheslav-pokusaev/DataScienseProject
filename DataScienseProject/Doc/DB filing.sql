
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
	('Image'),
	('Header Description'),
	('Header Image');
GO


INSERT INTO [dbo].[Groups](GroupName, IsDeleted) VALUES('all', 0);
-- All excist reports should be automatically assigned to this group after creating

-------------------------------------------------------------
		--Basic filing end
-------------------------------------------------------------


-------------------------------------------------------------
		--Testing filing start
-------------------------------------------------------------

DECLARE @PasswordValue VARCHAR(50) = 'test';
DECLARE @CreateDate DATETIME = GETDATE();
DECLARE @ExpirationDate DATETIME = GETDATE() + 7; --Password has been expired after 7 days

INSERT INTO [dbo].Passwords(GroupKey, PasswordValue, CreatedDate, ExpirationDate, IsDeleted) Values(1, @PasswordValue, @CreateDate, @ExpirationDate, 0);

-- In current example we are adding admin email for get info of expiration password inserted
DECLARE @Key VARCHAR(MAX) = 'AdminEmail';
DECLARE @Value VARCHAR(MAX) = 'kdaniilm@gmail.com';

INSERT INTO ConfigValues([Key], [Value], IsEnabled) VALUES('AdminEmail', 'kdaniilm@gmail.com', 1);

-------------------------------------------------------------
		--Testing filing start
-------------------------------------------------------------