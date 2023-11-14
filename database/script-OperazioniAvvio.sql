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

CREATE TABLE [Feste] (
    [Id] uniqueidentifier NOT NULL,
    [DataInizio] date NOT NULL,
    [DataFine] date NOT NULL,
    [StatusFesta] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Feste] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Impostazioni] (
    [Id] uniqueidentifier NOT NULL,
    [IdFesta] uniqueidentifier NOT NULL,
    [GestioneMenu] bit NOT NULL,
    [GestioneCategorie] bit NOT NULL,
    [StampaCarta] bit NOT NULL,
    [StampaRicevuta] bit NOT NULL,
    CONSTRAINT [PK_Impostazioni] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Impostazioni_Feste_IdFesta] FOREIGN KEY ([IdFesta]) REFERENCES [Feste] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Intestazioni] (
    [Id] uniqueidentifier NOT NULL,
    [IdFesta] uniqueidentifier NOT NULL,
    [Titolo] nvarchar(100) NOT NULL,
    [Edizione] nvarchar(100) NOT NULL,
    [Luogo] nvarchar(100) NOT NULL,
    CONSTRAINT [PK_Intestazioni] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Intestazioni_Feste_IdFesta] FOREIGN KEY ([IdFesta]) REFERENCES [Feste] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_Impostazioni_IdFesta] ON [Impostazioni] ([IdFesta]);
GO

CREATE INDEX [IX_Intestazioni_IdFesta] ON [Intestazioni] ([IdFesta]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20231013071526_InitialMigration', N'7.0.13');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Feste]') AND [c].[name] = N'DataInizio');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Feste] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Feste] ALTER COLUMN [DataInizio] datetime2 NOT NULL;
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Feste]') AND [c].[name] = N'DataFine');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Feste] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [Feste] ALTER COLUMN [DataFine] datetime2 NOT NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20231108183037_RemoveDateTimeOnly', N'7.0.13');
GO

COMMIT;
GO

