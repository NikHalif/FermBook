
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 09/29/2021 13:58:04
-- Generated from EDMX file: C:\Users\trust\source\repos\FermBook\FermModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [DatabaseSQL];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_calve_ToCowchildren]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[calve] DROP CONSTRAINT [FK_calve_ToCowchildren];
GO
IF OBJECT_ID(N'[dbo].[FK_calve_ToCowMam]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[calve] DROP CONSTRAINT [FK_calve_ToCowMam];
GO
IF OBJECT_ID(N'[dbo].[FK_disease_ToCow]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[disease] DROP CONSTRAINT [FK_disease_ToCow];
GO
IF OBJECT_ID(N'[dbo].[FK_insemination_ToCow]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[insemination] DROP CONSTRAINT [FK_insemination_ToCow];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[calve]', 'U') IS NOT NULL
    DROP TABLE [dbo].[calve];
GO
IF OBJECT_ID(N'[dbo].[cows]', 'U') IS NOT NULL
    DROP TABLE [dbo].[cows];
GO
IF OBJECT_ID(N'[dbo].[disease]', 'U') IS NOT NULL
    DROP TABLE [dbo].[disease];
GO
IF OBJECT_ID(N'[dbo].[insemination]', 'U') IS NOT NULL
    DROP TABLE [dbo].[insemination];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'calve'
CREATE TABLE [dbo].[calve] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Id_cow_mam] int  NOT NULL,
    [Id_cow_children] int  NULL,
    [data] datetime  NOT NULL,
    [comment] nvarchar(500)  NOT NULL
);
GO

-- Creating table 'cows'
CREATE TABLE [dbo].[cows] (
    [Id] int  NOT NULL,
    [name] nvarchar(50)  NOT NULL,
    [birthday] datetime  NOT NULL,
    [gender] bit  NOT NULL
);
GO

-- Creating table 'disease'
CREATE TABLE [dbo].[disease] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Id_cow] int  NOT NULL,
    [date] datetime  NOT NULL,
    [comment] nvarchar(500)  NOT NULL
);
GO

-- Creating table 'insemination'
CREATE TABLE [dbo].[insemination] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Id_cow] int  NOT NULL,
    [data] datetime  NOT NULL,
    [comment] nvarchar(500)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'calve'
ALTER TABLE [dbo].[calve]
ADD CONSTRAINT [PK_calve]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'cows'
ALTER TABLE [dbo].[cows]
ADD CONSTRAINT [PK_cows]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'disease'
ALTER TABLE [dbo].[disease]
ADD CONSTRAINT [PK_disease]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'insemination'
ALTER TABLE [dbo].[insemination]
ADD CONSTRAINT [PK_insemination]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Id_cow_children] in table 'calve'
ALTER TABLE [dbo].[calve]
ADD CONSTRAINT [FK_calve_ToCowchildren]
    FOREIGN KEY ([Id_cow_children])
    REFERENCES [dbo].[cows]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_calve_ToCowchildren'
CREATE INDEX [IX_FK_calve_ToCowchildren]
ON [dbo].[calve]
    ([Id_cow_children]);
GO

-- Creating foreign key on [Id_cow_mam] in table 'calve'
ALTER TABLE [dbo].[calve]
ADD CONSTRAINT [FK_calve_ToCowMam]
    FOREIGN KEY ([Id_cow_mam])
    REFERENCES [dbo].[cows]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_calve_ToCowMam'
CREATE INDEX [IX_FK_calve_ToCowMam]
ON [dbo].[calve]
    ([Id_cow_mam]);
GO

-- Creating foreign key on [Id_cow] in table 'disease'
ALTER TABLE [dbo].[disease]
ADD CONSTRAINT [FK_disease_ToCow]
    FOREIGN KEY ([Id_cow])
    REFERENCES [dbo].[cows]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_disease_ToCow'
CREATE INDEX [IX_FK_disease_ToCow]
ON [dbo].[disease]
    ([Id_cow]);
GO

-- Creating foreign key on [Id_cow] in table 'insemination'
ALTER TABLE [dbo].[insemination]
ADD CONSTRAINT [FK_insemination_ToCow]
    FOREIGN KEY ([Id_cow])
    REFERENCES [dbo].[cows]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_insemination_ToCow'
CREATE INDEX [IX_FK_insemination_ToCow]
ON [dbo].[insemination]
    ([Id_cow]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------