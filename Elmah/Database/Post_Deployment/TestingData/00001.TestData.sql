DECLARE @Application NVARCHAR(60);
DECLARE @Host NVARCHAR(30);
DECLARE @Type NVARCHAR(100);
DECLARE @Source NVARCHAR(60);
DECLARE @User NVARCHAR(50);
DECLARE @StatusCode INT;

DECLARE @ErrorId UNIQUEIDENTIFIER;
DECLARE @Message NVARCHAR(500);
DECLARE @AllXml NVARCHAR(MAX);
DECLARE @TimeUtc DATETIME;

SET @Application='Test Application 1';
SET @Host='Test Host 1';
SET @Type='Test Type 1';
SET @Source='Test Source 1';
SET @User='Test User 1';
SET @StatusCode=1;

SET @ErrorId=NEWID();
SET @Message='Test Message #00001';
SET @AllXml='<error   application="Application#1"   host="WWW.YouDomain.com"   type="ApplicationException"   message="Order list is null or empty."   source="DataFactory"   detail="ApplicationException: Order list is null or empty.&#xD;&#xA;   at Whatever"   user="Test User 1"   time="2020-01-01T15:59:15.2985776Z"/>';
SET @TimeUtc='2021-01-01';
EXEC [dbo].[ELMAH_LogError] @ErrorId, @Application, @Host, @Type, @Source, @Message, @User, @AllXml, @StatusCode, @TimeUtc

SET @ErrorId=NEWID();
SET @Message='Test Message #00002';
SET @AllXml='<error   application="Application#2"   host="WWW.YouDomain.com"   type="ApplicationException"   message="Order list is null or empty."   source="DataFactory"   detail="ApplicationException: Order list is null or empty.&#xD;&#xA;   at Whatever"   user="Test User 1"   time="2020-01-01T15:59:15.2985776Z"/>';
SET @TimeUtc='2021-01-01';
EXEC [dbo].[ELMAH_LogError] @ErrorId, @Application, @Host, @Type, @Source, @Message, @User, @AllXml, @StatusCode, @TimeUtc

SET @ErrorId=NEWID();
SET @Message='Test Message #00003';
SET @AllXml='<error   application="Application#2"   host="WWW.YouDomain.com"   type="ApplicationException"   message="Order list is null or empty."   source="DataFactory"   detail="ApplicationException: Order list is null or empty.&#xD;&#xA;   at Whatever"   user="Test User 1"   time="2020-01-01T15:59:15.2985776Z"/>';
SET @TimeUtc='2021-01-01';
EXEC [dbo].[ELMAH_LogError] @ErrorId, @Application, @Host, @Type, @Source, @Message, @User, @AllXml, @StatusCode, @TimeUtc
