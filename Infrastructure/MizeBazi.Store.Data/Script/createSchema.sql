USE [MizeBaziStore]
GO

IF NOT EXISTS (SELECT 1 FROM sys.schemas WHERE name = 'sp')
    EXEC('CREATE SCHEMA sp');

IF NOT EXISTS (SELECT 1 FROM sys.schemas WHERE name = 'fn')
    EXEC('CREATE SCHEMA fn');