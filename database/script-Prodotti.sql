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

CREATE TABLE [Prodotti] (
    [Id] uniqueidentifier NOT NULL,
    [IdFesta] uniqueidentifier NOT NULL,
    [Descrizione] nvarchar(100) NOT NULL,
    [ProdottoAttivo] bit NOT NULL,
    [IdCategoria] uniqueidentifier NOT NULL,
    [Prezzo_Currency] nvarchar(max) NOT NULL,
    [Prezzo_Amount] decimal(18,2) NOT NULL,
    [Quantita] int NOT NULL,
    [QuantitaAttiva] bit NOT NULL,
    [QuantitaScorta] int NOT NULL,
    [AvvisoScorta] bit NOT NULL,
    [Prenotazione] bit NOT NULL,
    CONSTRAINT [PK_Prodotti] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230921095423_InitialMigration', N'7.0.11');
GO

COMMIT;
GO

