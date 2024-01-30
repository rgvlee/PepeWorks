-- Enable system versioning for the database
ALTER DATABASE PepeWorks
SET CHANGE_TRACKING = ON
(CHANGE_RETENTION = 2 DAYS, AUTO_CLEANUP = ON);

-- Create Location table with temporal support
CREATE TABLE Location (
                          Id UNIQUEIDENTIFIER CONSTRAINT PK_Location PRIMARY KEY DEFAULT NEWID() NOT NULL,
                          Code NVARCHAR(10) NOT NULL,
                          Name NVARCHAR(50) NOT NULL,
                          CreatedAt DATETIME NOT NULL,
                          CreatedBy NVARCHAR(50) NOT NULL,
                          UpdatedAt DATETIME NOT NULL,
                          UpdatedBy NVARCHAR(50) NOT NULL,
                          SysStartTime DATETIME2 GENERATED ALWAYS AS ROW START HIDDEN NOT NULL,
                          SysEndTime DATETIME2 GENERATED ALWAYS AS ROW END HIDDEN NOT NULL,
                          PERIOD FOR SYSTEM_TIME (SysStartTime, SysEndTime)
)
    WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = [dbo].[LocationHistory]));

-- Create Camp table with temporal support
CREATE TABLE Camp (
                      Id UNIQUEIDENTIFIER CONSTRAINT PK_Camp PRIMARY KEY DEFAULT NEWID() NOT NULL,
                      Code NVARCHAR(10) NOT NULL,
                      Name NVARCHAR(50) NOT NULL,
                      LocationId UNIQUEIDENTIFIER CONSTRAINT FK_Camp_Location FOREIGN KEY REFERENCES Location(Id) NOT NULL,
                      CreatedAt DATETIME NOT NULL,
                      CreatedBy NVARCHAR(50) NOT NULL,
                      UpdatedAt DATETIME NOT NULL,
                      UpdatedBy NVARCHAR(50) NOT NULL,
                      SysStartTime DATETIME2 GENERATED ALWAYS AS ROW START HIDDEN NOT NULL,
                      SysEndTime DATETIME2 GENERATED ALWAYS AS ROW END HIDDEN NOT NULL,
                      PERIOD FOR SYSTEM_TIME (SysStartTime, SysEndTime)
)
    WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = [dbo].[CampHistory]));

-- Create Room table with temporal support
CREATE TABLE Room (
                      Id UNIQUEIDENTIFIER CONSTRAINT PK_Room PRIMARY KEY DEFAULT NEWID() NOT NULL,
                      Code NVARCHAR(10) NOT NULL,
                      CampId UNIQUEIDENTIFIER CONSTRAINT FK_Room_Camp FOREIGN KEY REFERENCES Camp(Id) NOT NULL,
                      Capacity SMALLINT NOT NULL,
                      CreatedAt DATETIME NOT NULL,
                      CreatedBy NVARCHAR(50) NOT NULL,
                      UpdatedAt DATETIME NOT NULL,
                      UpdatedBy NVARCHAR(50) NOT NULL,
                      SysStartTime DATETIME2 GENERATED ALWAYS AS ROW START HIDDEN NOT NULL,
                      SysEndTime DATETIME2 GENERATED ALWAYS AS ROW END HIDDEN NOT NULL,
                      PERIOD FOR SYSTEM_TIME (SysStartTime, SysEndTime)
)
    WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = [dbo].[RoomHistory]));
