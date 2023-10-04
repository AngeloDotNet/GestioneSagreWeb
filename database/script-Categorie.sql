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

CREATE TABLE [Categorie] (
    [Id] uniqueidentifier NOT NULL,
    [IdFesta] uniqueidentifier NOT NULL,
    [CategoriaVideo] nvarchar(30) NOT NULL,
    [CategoriaStampa] nvarchar(30) NOT NULL,
    CONSTRAINT [PK_Categorie] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230930170504_InitialMigration', N'7.0.11');
GO

COMMIT;
GO

