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
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250927030722_InitialCreate'
)
BEGIN
    CREATE TABLE [Admin] (
        [Id] int NOT NULL IDENTITY,
        [UserName] nvarchar(50) NOT NULL,
        [EmailAddress] nvarchar(100) NOT NULL,
        [PasswordHash] nvarchar(256) NOT NULL,
        [IsActive] bit NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        CONSTRAINT [PK_Admin] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250927030722_InitialCreate'
)
BEGIN
    CREATE TABLE [Author] (
        [Id] int NOT NULL IDENTITY,
        [FirstName] nvarchar(max) NOT NULL,
        [LastName] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Author] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250927030722_InitialCreate'
)
BEGIN
    CREATE TABLE [Book] (
        [Id] int NOT NULL IDENTITY,
        [Title] nvarchar(max) NOT NULL,
        [AuthorId] int NOT NULL,
        [PublishDate] datetime2 NOT NULL,
        [Genre] nvarchar(max) NOT NULL,
        [Available] bit NOT NULL,
        CONSTRAINT [PK_Book] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Book_Author_AuthorId] FOREIGN KEY ([AuthorId]) REFERENCES [Author] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250927030722_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_Book_AuthorId] ON [Book] ([AuthorId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250927030722_InitialCreate'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250927030722_InitialCreate', N'9.0.9');
END;

COMMIT;
GO