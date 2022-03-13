
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 03/13/2022 19:34:34
-- Generated from EDMX file: C:\Users\Bit\source\repos\Bitnik212\Sanatoriy\Entities\LabModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Sanatoriy];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Employees_Role]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Employees] DROP CONSTRAINT [FK_Employees_Role];
GO
IF OBJECT_ID(N'[dbo].[FK_Orders_Employees]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Orders] DROP CONSTRAINT [FK_Orders_Employees];
GO
IF OBJECT_ID(N'[dbo].[FK_Orders_Patients]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Orders] DROP CONSTRAINT [FK_Orders_Patients];
GO
IF OBJECT_ID(N'[dbo].[FK_Orders_Services1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Orders] DROP CONSTRAINT [FK_Orders_Services1];
GO
IF OBJECT_ID(N'[dbo].[FK_Patients_InsuranceCompany]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Patients] DROP CONSTRAINT [FK_Patients_InsuranceCompany];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Employees]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Employees];
GO
IF OBJECT_ID(N'[dbo].[InsuranceCompany]', 'U') IS NOT NULL
    DROP TABLE [dbo].[InsuranceCompany];
GO
IF OBJECT_ID(N'[dbo].[Orders]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Orders];
GO
IF OBJECT_ID(N'[dbo].[Patients]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Patients];
GO
IF OBJECT_ID(N'[dbo].[Role]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Role];
GO
IF OBJECT_ID(N'[dbo].[Services]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Services];
GO
IF OBJECT_ID(N'[dbo].[sysdiagrams]', 'U') IS NOT NULL
    DROP TABLE [dbo].[sysdiagrams];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Employees'
CREATE TABLE [dbo].[Employees] (
    [id] int  NOT NULL,
    [FIO] varchar(200)  NOT NULL,
    [Bday] datetime  NOT NULL,
    [Passport] varchar(10)  NOT NULL,
    [Phone] varchar(11)  NOT NULL,
    [id_Role] int  NOT NULL,
    [Login] nvarchar(10)  NOT NULL,
    [Password] nvarchar(10)  NOT NULL,
    [Lastenter] datetime  NOT NULL
);
GO

-- Creating table 'InsuranceCompanies'
CREATE TABLE [dbo].[InsuranceCompanies] (
    [id] int  NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [tariff] decimal(19,4)  NOT NULL
);
GO

-- Creating table 'Orders'
CREATE TABLE [dbo].[Orders] (
    [id] int  NOT NULL,
    [num_order] varchar(5)  NOT NULL,
    [date] datetime  NOT NULL,
    [id_patient] int  NULL,
    [id_services] int  NULL,
    [id_employee] int  NULL,
    [cost_order] decimal(19,4)  NOT NULL,
    [patient_FIO] varchar(max)  NOT NULL,
    [employee_FIO] varchar(max)  NOT NULL,
    [service_Name] varchar(max)  NOT NULL
);
GO

-- Creating table 'Patients'
CREATE TABLE [dbo].[Patients] (
    [id] int  NOT NULL,
    [FIO] varchar(200)  NOT NULL,
    [Bday] datetime  NOT NULL,
    [Passport] varchar(10)  NOT NULL,
    [Phone] varchar(11)  NOT NULL,
    [Email] nvarchar(150)  NOT NULL,
    [id_InsuranceCompany] int  NOT NULL,
    [Num_Insurance_policy] varchar(10)  NOT NULL
);
GO

-- Creating table 'Roles'
CREATE TABLE [dbo].[Roles] (
    [id] int  NOT NULL,
    [Name] nvarchar(30)  NOT NULL
);
GO

-- Creating table 'Services'
CREATE TABLE [dbo].[Services] (
    [id] int  NOT NULL,
    [Name] nvarchar(100)  NOT NULL,
    [Description] varchar(max)  NOT NULL,
    [Cost] decimal(19,4)  NOT NULL
);
GO

-- Creating table 'sysdiagrams'
CREATE TABLE [dbo].[sysdiagrams] (
    [name] nvarchar(128)  NOT NULL,
    [principal_id] int  NOT NULL,
    [diagram_id] int IDENTITY(1,1) NOT NULL,
    [version] int  NULL,
    [definition] varbinary(max)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [id] in table 'Employees'
ALTER TABLE [dbo].[Employees]
ADD CONSTRAINT [PK_Employees]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'InsuranceCompanies'
ALTER TABLE [dbo].[InsuranceCompanies]
ADD CONSTRAINT [PK_InsuranceCompanies]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'Orders'
ALTER TABLE [dbo].[Orders]
ADD CONSTRAINT [PK_Orders]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'Patients'
ALTER TABLE [dbo].[Patients]
ADD CONSTRAINT [PK_Patients]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'Roles'
ALTER TABLE [dbo].[Roles]
ADD CONSTRAINT [PK_Roles]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'Services'
ALTER TABLE [dbo].[Services]
ADD CONSTRAINT [PK_Services]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [diagram_id] in table 'sysdiagrams'
ALTER TABLE [dbo].[sysdiagrams]
ADD CONSTRAINT [PK_sysdiagrams]
    PRIMARY KEY CLUSTERED ([diagram_id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [id_Role] in table 'Employees'
ALTER TABLE [dbo].[Employees]
ADD CONSTRAINT [FK_Employees_Role]
    FOREIGN KEY ([id_Role])
    REFERENCES [dbo].[Roles]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Employees_Role'
CREATE INDEX [IX_FK_Employees_Role]
ON [dbo].[Employees]
    ([id_Role]);
GO

-- Creating foreign key on [id_employee] in table 'Orders'
ALTER TABLE [dbo].[Orders]
ADD CONSTRAINT [FK_Orders_Employees]
    FOREIGN KEY ([id_employee])
    REFERENCES [dbo].[Employees]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Orders_Employees'
CREATE INDEX [IX_FK_Orders_Employees]
ON [dbo].[Orders]
    ([id_employee]);
GO

-- Creating foreign key on [id_InsuranceCompany] in table 'Patients'
ALTER TABLE [dbo].[Patients]
ADD CONSTRAINT [FK_Patients_InsuranceCompany]
    FOREIGN KEY ([id_InsuranceCompany])
    REFERENCES [dbo].[InsuranceCompanies]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Patients_InsuranceCompany'
CREATE INDEX [IX_FK_Patients_InsuranceCompany]
ON [dbo].[Patients]
    ([id_InsuranceCompany]);
GO

-- Creating foreign key on [id_patient] in table 'Orders'
ALTER TABLE [dbo].[Orders]
ADD CONSTRAINT [FK_Orders_Patients]
    FOREIGN KEY ([id_patient])
    REFERENCES [dbo].[Patients]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Orders_Patients'
CREATE INDEX [IX_FK_Orders_Patients]
ON [dbo].[Orders]
    ([id_patient]);
GO

-- Creating foreign key on [id_services] in table 'Orders'
ALTER TABLE [dbo].[Orders]
ADD CONSTRAINT [FK_Orders_Services1]
    FOREIGN KEY ([id_services])
    REFERENCES [dbo].[Services]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Orders_Services1'
CREATE INDEX [IX_FK_Orders_Services1]
ON [dbo].[Orders]
    ([id_services]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------