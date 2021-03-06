CREATE TABLE [Countries] (
  [Alpha2] nchar(2) NOT NULL
, [Alpha3] nchar(3) NOT NULL
, [Numeric] smallint NOT NULL
, [Obsolete] bit NOT NULL
);
GO
CREATE TABLE [Translations] (
  [Alpha2] nchar(2) NOT NULL
, [Language] nvarchar(16) NOT NULL
, [Name] nvarchar(128) NOT NULL
);
GO
ALTER TABLE [Countries] ADD CONSTRAINT [PK_Countries] PRIMARY KEY ([Alpha2]);
GO
CREATE UNIQUE INDEX [UQ__Countries__000000000000000E] ON [Countries] ([Alpha2] ASC);
GO
CREATE UNIQUE INDEX [UQ__Countries__0000000000000013] ON [Countries] ([Alpha3] ASC);
GO
CREATE UNIQUE INDEX [UQ__Countries__0000000000000018] ON [Countries] ([Numeric] ASC);
GO
ALTER TABLE [Translations] ADD CONSTRAINT [FK_Alpha2] FOREIGN KEY ([Alpha2]) REFERENCES [Countries]([Alpha2]) ON DELETE CASCADE ON UPDATE NO ACTION;
GO

CREATE TABLE [Staged_Translations] (
  [Alpha2] nchar(2) NOT NULL
, [Language] nvarchar(16) NOT NULL
, [Name] nvarchar(128) NOT NULL
);
GO
ALTER TABLE [Staged_Translations] ADD CONSTRAINT [FK_Alpha2] FOREIGN KEY ([Alpha2]) REFERENCES [Countries]([Alpha2]) ON DELETE CASCADE ON UPDATE NO ACTION;
GO
