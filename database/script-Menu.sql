IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Portate] (
    [Id] uniqueidentifier NOT NULL,
    [IdFesta] uniqueidentifier NOT NULL,
    [IdProdotto] uniqueidentifier NOT NULL,
    [DataMenu] datetime2 NOT NULL,
    CONSTRAINT [PK_Portate] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20231228190936_InitialMigration', N'7.0.14');
GO

COMMIT;
GO

