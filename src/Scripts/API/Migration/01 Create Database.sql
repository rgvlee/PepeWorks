-- Enable system versioning for the database
ALTER
DATABASE PepeWorks
SET CHANGE_TRACKING = ON
(CHANGE_RETENTION = 2 DAYS, AUTO_CLEANUP = ON);

-- Create Location table with temporal support
CREATE TABLE Location
(
    Id           UNIQUEIDENTIFIER PRIMARY KEY,
    Code         NVARCHAR(10),
    Name         NVARCHAR(50),
    CreatedAt    DATETIME,
    CreatedBy    NVARCHAR(50),
    UpdatedAt    DATETIME,
    UpdatedBy    NVARCHAR(50),
    SysStartTime DATETIME2 GENERATED ALWAYS AS ROW START HIDDEN,
    SysEndTime   DATETIME2 GENERATED ALWAYS AS ROW END HIDDEN,
    PERIOD FOR SYSTEM_TIME (SysStartTime, SysEndTime)
)
    WITH (SYSTEM_VERSIONING = ON);

-- Create Camp table with temporal support
CREATE TABLE Camp
(
    Id           UNIQUEIDENTIFIER PRIMARY KEY,
    Code         NVARCHAR(10),
    Name         NVARCHAR(50),
    LocationId   UNIQUEIDENTIFIER FOREIGN KEY REFERENCES Location(Id),
    CreatedAt    DATETIME,
    CreatedBy    NVARCHAR(50),
    UpdatedAt    DATETIME,
    UpdatedBy    NVARCHAR(50),
    SysStartTime DATETIME2 GENERATED ALWAYS AS ROW START HIDDEN,
    SysEndTime   DATETIME2 GENERATED ALWAYS AS ROW END HIDDEN,
    PERIOD FOR SYSTEM_TIME (SysStartTime, SysEndTime)
)
    WITH (SYSTEM_VERSIONING = ON);

-- Create Room table with temporal support
CREATE TABLE Room
(
    Id           UNIQUEIDENTIFIER PRIMARY KEY,
    Code         NVARCHAR(10),
    CampId       UNIQUEIDENTIFIER FOREIGN KEY REFERENCES Camp(Id),
    Capacity     SMALLINT,
    CreatedAt    DATETIME,
    CreatedBy    NVARCHAR(50),
    UpdatedAt    DATETIME,
    UpdatedBy    NVARCHAR(50),
    SysStartTime DATETIME2 GENERATED ALWAYS AS ROW START HIDDEN,
    SysEndTime   DATETIME2 GENERATED ALWAYS AS ROW END HIDDEN,
    PERIOD FOR SYSTEM_TIME (SysStartTime, SysEndTime)
)
    WITH (SYSTEM_VERSIONING = ON);
