
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 03/30/2021 17:00:51
-- Generated from EDMX file: C:\Users\User 1\source\repos\ResponseEmergencySystem\Entity Framework\sirem.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [SIREM];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Incident_Report]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Incident_Report];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Incident_Report'
CREATE TABLE [dbo].[Incident_Report] (
    [ID_Incident] uniqueidentifier  NOT NULL,
    [ID_Driver] uniqueidentifier  NULL,
    [ID_Location] uniqueidentifier  NULL,
    [ID_CargoType] uniqueidentifier  NULL,
    [ID_StatusDetail] uniqueidentifier  NULL,
    [Incident_No] nvarchar(20)  NULL,
    [Incident_Date] datetime  NULL,
    [Incident_CloseDate] datetime  NULL,
    [PoliceReport_No] nvarchar(50)  NULL,
    [PoliceReport_Bolean] bit  NULL,
    [CitationReport_No] nvarchar(50)  NULL,
    [Injuries] bit  NULL,
    [Name_Injuried] nvarchar(200)  NULL,
    [Truck_No] nvarchar(50)  NULL,
    [Trailer_No] nvarchar(50)  NULL,
    [Truck_Damage] bit  NULL,
    [Trailer_Damage] bit  NULL,
    [CargoSplill] bit  NULL,
    [Manifest_No] nvarchar(50)  NULL,
    [CreatedBy] uniqueidentifier  NULL,
    [UpdatedBy] uniqueidentifier  NULL,
    [CreatedDate] datetime  NULL,
    [UpdatedDate] datetime  NULL,
    [Status] bit  NULL
);
GO

-- Creating table 'Status_Detail'
CREATE TABLE [dbo].[Status_Detail] (
    [ID_StatusDetail] uniqueidentifier  NOT NULL,
    [name] nvarchar(max)  NOT NULL,
    [Incident_Report_ID_Incident] uniqueidentifier  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ID_Incident] in table 'Incident_Report'
ALTER TABLE [dbo].[Incident_Report]
ADD CONSTRAINT [PK_Incident_Report]
    PRIMARY KEY CLUSTERED ([ID_Incident] ASC);
GO

-- Creating primary key on [ID_StatusDetail] in table 'Status_Detail'
ALTER TABLE [dbo].[Status_Detail]
ADD CONSTRAINT [PK_Status_Detail]
    PRIMARY KEY CLUSTERED ([ID_StatusDetail] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Incident_Report_ID_Incident] in table 'Status_Detail'
ALTER TABLE [dbo].[Status_Detail]
ADD CONSTRAINT [FK_Incident_ReportStatus_Detail]
    FOREIGN KEY ([Incident_Report_ID_Incident])
    REFERENCES [dbo].[Incident_Report]
        ([ID_Incident])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Incident_ReportStatus_Detail'
CREATE INDEX [IX_FK_Incident_ReportStatus_Detail]
ON [dbo].[Status_Detail]
    ([Incident_Report_ID_Incident]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------