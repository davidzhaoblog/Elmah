DECLARE @EntityTypeID SMALLINT;
DECLARE @EntityStatusCodeID TINYINT;
DECLARE @AddressTypeID INT;
--DECLARE @IndustryClassificationID INT;
DECLARE @IndustrySubClassificationID INT;
DECLARE @ParentBusinessEntityID BIGINT;
DECLARE @RecursiveScheduleTypeID INT;
DECLARE @AddressResourceTypeID INT;

DECLARE @MasterEntityTypeID SMALLINT;
DECLARE @SlaveEntityTypeID SMALLINT;

DECLARE @ProgramEntityTypeID SMALLINT;
DECLARE @ProgramStatusID TINYINT;

DECLARE @BusinessRoleID INT;

DECLARE @NtierOnTimeAdmin BIGINT;
SET @NtierOnTimeAdmin = 1000;


DECLARE @NTierOnTimeBuiltInEntity BIGINT;
SET @NTierOnTimeBuiltInEntity = 1;

DECLARE @NTierOnTimeBuiltInBusinessEntity BIGINT;
SET @NTierOnTimeBuiltInBusinessEntity = 2;

DECLARE @NTierOnTimeBuiltInEducationBusinessEntity BIGINT;
SET @NTierOnTimeBuiltInEducationBusinessEntity = 3;

DECLARE @CourseCategory BIGINT;
SET @CourseCategory = 4;