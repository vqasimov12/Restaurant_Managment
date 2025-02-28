USE master;
CREATE DATABASE RestaurantManagementDB;
GO
USE RestaurantManagementDB;
GO

CREATE TABLE [dbo].[Categories] (
    [Id]          INT            IDENTITY (1,1) PRIMARY KEY,
    [Name]        NVARCHAR(255)  NOT NULL UNIQUE,
    [UpdatedBy]   INT            NULL,
    [CreatedBy]   INT            NULL,
    [DeletedBy]   INT            NULL,
    [CreatedDate] DATETIME2(7)   NOT NULL DEFAULT GETDATE(),
    [DeletedDate] DATETIME2(7)   NULL,
    [UpdatedDate] DATETIME2(7)   NULL,
    [IsDeleted]   BIT            NOT NULL DEFAULT 0
);
GO

CREATE TABLE [dbo].[Users] (
    [Id]           INT            IDENTITY (1,1) PRIMARY KEY,
    [Name]         NVARCHAR(255)  NOT NULL,
    [Surname]      NVARCHAR(50)   NOT NULL,
    [Email]        NVARCHAR(70)   NOT NULL UNIQUE,
    [Phone]        NVARCHAR(50)   NOT NULL,
    [PasswordHash] NVARCHAR(1000) NOT NULL,
    [UpdatedBy]    INT            NULL,
    [CreatedBy]    INT            NULL,
    [DeletedBy]    INT            NULL,
    [CreatedDate]  DATETIME2(7)   NOT NULL DEFAULT GETDATE(),
    [DeletedDate]  DATETIME2(7)   NULL,
    [UpdatedDate]  DATETIME2(7)   NULL,
    [IsDeleted]    BIT            NOT NULL DEFAULT 0
);
GO

CREATE TABLE [dbo].[Products] (
    [Id]          INT            IDENTITY (1,1) PRIMARY KEY,
    [Name]        NVARCHAR(255)  NOT NULL,
    [Description] NVARCHAR(255)  NULL,
    [Stock]       INT            NOT NULL DEFAULT 0 CHECK (Stock >= 0),
    [Price]       MONEY          NOT NULL DEFAULT 0 CHECK (Price >= 0),
    [UpdatedBy]   INT            NULL,
    [CreatedBy]   INT            NULL,
    [DeletedBy]   INT            NULL,
    [CreatedDate] DATETIME2(7)   NOT NULL DEFAULT GETDATE(),
    [DeletedDate] DATETIME2(7)   NULL,
    [UpdatedDate] DATETIME2(7)   NULL,
    [IsDeleted]   BIT            NOT NULL DEFAULT 0,
    [CategoryId]  INT            NOT NULL,
    CONSTRAINT [FK_Products_Categories] FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[Categories]([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [dbo].[Customers](
    [Id]          INT           IDENTITY (1,1) PRIMARY KEY,
    [Name]        NVARCHAR(25)  NOT NULL,
    [Surname]     NVARCHAR(25)  NOT NULL,
    [Email]       NVARCHAR(50)  NOT NULL,
    [Phone]       NVARCHAR(25)  NOT NULL,
    [Address]     NVARCHAR(255) NOT NULL,
    [UpdatedBy]   INT            NULL,
    [CreatedBy]   INT            NULL,
    [DeletedBy]   INT            NULL,
    [CreatedDate] DATETIME2(7)   NOT NULL DEFAULT GETDATE(),
    [DeletedDate] DATETIME2(7)   NULL,
    [UpdatedDate] DATETIME2(7)   NULL,
    [IsDeleted]   BIT            NOT NULL DEFAULT 0 
)