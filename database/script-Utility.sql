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

CREATE TABLE [StatoScontrino] (
    [Id] uniqueidentifier NOT NULL,
    [Valore] nvarchar(255) NOT NULL,
    CONSTRAINT [PK_StatoScontrino] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [TipoCliente] (
    [Id] uniqueidentifier NOT NULL,
    [Valore] nvarchar(255) NOT NULL,
    CONSTRAINT [PK_TipoCliente] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [TipoPagamento] (
    [Id] uniqueidentifier NOT NULL,
    [Valore] nvarchar(255) NOT NULL,
    CONSTRAINT [PK_TipoPagamento] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [TipoScontrino] (
    [Id] uniqueidentifier NOT NULL,
    [Valore] nvarchar(255) NOT NULL,
    CONSTRAINT [PK_TipoScontrino] PRIMARY KEY ([Id])
);
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Valore') AND [object_id] = OBJECT_ID(N'[StatoScontrino]'))
    SET IDENTITY_INSERT [StatoScontrino] ON;
INSERT INTO [StatoScontrino] ([Id], [Valore])
VALUES ('0a4bfb8a-7e75-46fe-a8c1-8d1eb3011686', N'ANNULLATO'),
('5ac9505a-726a-4106-89f8-a86639558d2d', N'DA INCASSARE'),
('80087fd8-09bb-45a5-a4b7-afb6007ed592', N'PAGATO'),
('828e7daa-f1cb-45eb-981a-983fe5cad00b', N'CHIUSO'),
('bec5963a-9a76-4610-a951-2e3e9e66fd82', N'APERTO'),
('dced34e3-0be1-4b04-9895-4816e9642644', N'IN ELABORAZIONE');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Valore') AND [object_id] = OBJECT_ID(N'[StatoScontrino]'))
    SET IDENTITY_INSERT [StatoScontrino] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Valore') AND [object_id] = OBJECT_ID(N'[TipoCliente]'))
    SET IDENTITY_INSERT [TipoCliente] ON;
INSERT INTO [TipoCliente] ([Id], [Valore])
VALUES ('0c86400f-4dee-4354-99b2-f680cc6e8a74', N'STAFF'),
('69ad8e35-b445-42a4-b3c2-8c3fb0a27750', N'CLIENTE');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Valore') AND [object_id] = OBJECT_ID(N'[TipoCliente]'))
    SET IDENTITY_INSERT [TipoCliente] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Valore') AND [object_id] = OBJECT_ID(N'[TipoPagamento]'))
    SET IDENTITY_INSERT [TipoPagamento] ON;
INSERT INTO [TipoPagamento] ([Id], [Valore])
VALUES ('6340fa14-271f-4565-ace2-3249e5f474ef', N'CONTANTI');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Valore') AND [object_id] = OBJECT_ID(N'[TipoPagamento]'))
    SET IDENTITY_INSERT [TipoPagamento] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Valore') AND [object_id] = OBJECT_ID(N'[TipoScontrino]'))
    SET IDENTITY_INSERT [TipoScontrino] ON;
INSERT INTO [TipoScontrino] ([Id], [Valore])
VALUES ('4981bcd3-6860-4545-8786-832abdbbe60d', N'OMAGGIO'),
('6e00a736-d5b4-4494-aa89-c2caa726d4e3', N'PAGAMENTO');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Valore') AND [object_id] = OBJECT_ID(N'[TipoScontrino]'))
    SET IDENTITY_INSERT [TipoScontrino] OFF;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230914133921_InitialMigration', N'7.0.11');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DELETE FROM [StatoScontrino]
WHERE [Id] = '0a4bfb8a-7e75-46fe-a8c1-8d1eb3011686';
SELECT @@ROWCOUNT;

GO

DELETE FROM [StatoScontrino]
WHERE [Id] = '5ac9505a-726a-4106-89f8-a86639558d2d';
SELECT @@ROWCOUNT;

GO

DELETE FROM [StatoScontrino]
WHERE [Id] = '80087fd8-09bb-45a5-a4b7-afb6007ed592';
SELECT @@ROWCOUNT;

GO

DELETE FROM [StatoScontrino]
WHERE [Id] = '828e7daa-f1cb-45eb-981a-983fe5cad00b';
SELECT @@ROWCOUNT;

GO

DELETE FROM [StatoScontrino]
WHERE [Id] = 'bec5963a-9a76-4610-a951-2e3e9e66fd82';
SELECT @@ROWCOUNT;

GO

DELETE FROM [StatoScontrino]
WHERE [Id] = 'dced34e3-0be1-4b04-9895-4816e9642644';
SELECT @@ROWCOUNT;

GO

DELETE FROM [TipoCliente]
WHERE [Id] = '0c86400f-4dee-4354-99b2-f680cc6e8a74';
SELECT @@ROWCOUNT;

GO

DELETE FROM [TipoCliente]
WHERE [Id] = '69ad8e35-b445-42a4-b3c2-8c3fb0a27750';
SELECT @@ROWCOUNT;

GO

DELETE FROM [TipoPagamento]
WHERE [Id] = '6340fa14-271f-4565-ace2-3249e5f474ef';
SELECT @@ROWCOUNT;

GO

DELETE FROM [TipoScontrino]
WHERE [Id] = '4981bcd3-6860-4545-8786-832abdbbe60d';
SELECT @@ROWCOUNT;

GO

DELETE FROM [TipoScontrino]
WHERE [Id] = '6e00a736-d5b4-4494-aa89-c2caa726d4e3';
SELECT @@ROWCOUNT;

GO

CREATE TABLE [Currency] (
    [Id] uniqueidentifier NOT NULL,
    [Valore] nvarchar(3) NOT NULL,
    [Descrizione] nvarchar(max) NOT NULL,
    [Simbolo] nvarchar(1) NOT NULL,
    CONSTRAINT [PK_Currency] PRIMARY KEY ([Id])
);
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Descrizione', N'Simbolo', N'Valore') AND [object_id] = OBJECT_ID(N'[Currency]'))
    SET IDENTITY_INSERT [Currency] ON;
INSERT INTO [Currency] ([Id], [Descrizione], [Simbolo], [Valore])
VALUES ('1062f8a7-07ae-4ff3-9122-e1061344e968', N'Sterlina UK', N'£', N'GBP'),
('39b42616-38bf-4ca8-ac47-e7ab5415247e', N'Euro', N'€', N'EUR'),
('4cb5ba1f-0a26-4904-9335-a4e7a6df957b', N'Dollaro USA', N'$', N'USD');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Descrizione', N'Simbolo', N'Valore') AND [object_id] = OBJECT_ID(N'[Currency]'))
    SET IDENTITY_INSERT [Currency] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Valore') AND [object_id] = OBJECT_ID(N'[StatoScontrino]'))
    SET IDENTITY_INSERT [StatoScontrino] ON;
INSERT INTO [StatoScontrino] ([Id], [Valore])
VALUES ('3cc4bff5-0e3a-48e5-bc0a-a6518b4f9227', N'ANNULLATO'),
('7a449f0c-773a-4e1b-b195-48765c93b64a', N'PAGATO'),
('ada6fe1c-1cff-41fa-88d5-adde721ccb6e', N'DA INCASSARE'),
('ba8d1d47-2980-4081-adfa-59a8a283c168', N'APERTO'),
('cd7435a1-8352-451e-9640-4b55bcec0d3f', N'CHIUSO'),
('d3bca587-1d4b-478f-a1cf-77283cacad54', N'IN ELABORAZIONE');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Valore') AND [object_id] = OBJECT_ID(N'[StatoScontrino]'))
    SET IDENTITY_INSERT [StatoScontrino] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Valore') AND [object_id] = OBJECT_ID(N'[TipoCliente]'))
    SET IDENTITY_INSERT [TipoCliente] ON;
INSERT INTO [TipoCliente] ([Id], [Valore])
VALUES ('21a3647c-ebf6-4b3e-afd5-054ee7e53343', N'STAFF'),
('b8fbba5b-8395-42be-bafb-c1bdffc141c0', N'CLIENTE');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Valore') AND [object_id] = OBJECT_ID(N'[TipoCliente]'))
    SET IDENTITY_INSERT [TipoCliente] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Valore') AND [object_id] = OBJECT_ID(N'[TipoPagamento]'))
    SET IDENTITY_INSERT [TipoPagamento] ON;
INSERT INTO [TipoPagamento] ([Id], [Valore])
VALUES ('f88348a3-6cbb-4108-a79e-e38bc07824d2', N'CONTANTI');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Valore') AND [object_id] = OBJECT_ID(N'[TipoPagamento]'))
    SET IDENTITY_INSERT [TipoPagamento] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Valore') AND [object_id] = OBJECT_ID(N'[TipoScontrino]'))
    SET IDENTITY_INSERT [TipoScontrino] ON;
INSERT INTO [TipoScontrino] ([Id], [Valore])
VALUES ('4fd0dba1-d49e-4d33-8ce2-fc35bb607141', N'OMAGGIO'),
('c1b77737-f730-4220-a556-92ca67203779', N'PAGAMENTO');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Valore') AND [object_id] = OBJECT_ID(N'[TipoScontrino]'))
    SET IDENTITY_INSERT [TipoScontrino] OFF;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230916220420_AddCurrencyTable', N'7.0.11');
GO

COMMIT;
GO

