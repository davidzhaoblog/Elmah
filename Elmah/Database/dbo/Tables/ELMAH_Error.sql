CREATE TABLE [dbo].[ELMAH_Error] (
    [ErrorId]     UNIQUEIDENTIFIER CONSTRAINT [DF_ELMAH_Error_ErrorId] DEFAULT (newid()) NOT NULL,
    [Application] NVARCHAR (60)    NOT NULL,
    [Host]        NVARCHAR (50)    NOT NULL,
    [Type]        NVARCHAR (100)   NOT NULL,
    [Source]      NVARCHAR (60)    NOT NULL,
    [Message]     NVARCHAR (500)   NOT NULL,
    [User]        NVARCHAR (50)    NOT NULL,
    [StatusCode]  INT              NOT NULL,
    [TimeUtc]     DATETIME         NOT NULL,
    [Sequence]    INT              IDENTITY (1, 1) NOT NULL,
    [AllXml]      NTEXT            NOT NULL,
    CONSTRAINT [PK_ELMAH_Error] PRIMARY KEY NONCLUSTERED ([ErrorId] ASC) WITH (FILLFACTOR = 90),
    CONSTRAINT [FK_ELMAH_Error_ElmahApplication] FOREIGN KEY ([Application]) REFERENCES [dbo].[ElmahApplication] ([Application]),
    CONSTRAINT [FK_ELMAH_Error_ElmahHost] FOREIGN KEY ([Host]) REFERENCES [dbo].[ElmahHost] ([Host]),
    CONSTRAINT [FK_ELMAH_Error_ElmahSource] FOREIGN KEY ([Source]) REFERENCES [dbo].[ElmahSource] ([Source]),
    CONSTRAINT [FK_ELMAH_Error_ElmahStatusCode] FOREIGN KEY ([StatusCode]) REFERENCES [dbo].[ElmahStatusCode] ([StatusCode]),
    CONSTRAINT [FK_ELMAH_Error_ElmahType] FOREIGN KEY ([Type]) REFERENCES [dbo].[ElmahType] ([Type]),
    CONSTRAINT [FK_ELMAH_Error_ElmahUser] FOREIGN KEY ([User]) REFERENCES [dbo].[ElmahUser] ([User])
);


GO
CREATE NONCLUSTERED INDEX [IX_ELMAH_Error_App_Time_Seq]
    ON [dbo].[ELMAH_Error]([Application] ASC, [TimeUtc] DESC, [Sequence] DESC) WITH (FILLFACTOR = 90);

