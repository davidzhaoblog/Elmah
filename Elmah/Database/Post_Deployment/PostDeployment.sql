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
:r .\VariableDeclaration.sql

--:r .\PostDeployment_DisableCheckContraints.sql

----Load the Initial System Data (status codes, ddl, etc.)
--IF ('$(SqlCmdVar_LoadInitData)' = 'TRUE')
--BEGIN
--END 

IF ('$(SqlCmdVar_LoadTestingData)' = 'TRUE')
BEGIN
	:r .\TestingData\00001.TestData.sql	
END

--:r .\PostDeployment_EnableCheckContraints.sql
