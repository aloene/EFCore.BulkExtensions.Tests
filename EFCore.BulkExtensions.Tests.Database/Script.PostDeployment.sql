/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

IF NOT EXISTS(SELECT 1 FROM [dbo].[Application])
BEGIN
	INSERT INTO [dbo].[Application]([Name])
	VALUES ('Application 1'), ('Application 2'), ('Application 3');

	INSERT INTO [dbo].[ActivityType]([Id], [Name])
	VALUES (1, 'Off'), (2, 'Project');
END;
