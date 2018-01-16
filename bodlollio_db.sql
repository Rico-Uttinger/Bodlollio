USE bodlollio_db;
CREATE TABLE [dbo].[Comment] (
    [Id]        INT      IDENTITY (1, 1) NOT NULL,
    [PostId]    INT      NULL,
    [UserId]    INT      NULL,
    [Commet]    TEXT     NULL,
    [CreatedOn] DATETIME NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
 CREATE TABLE [dbo].[Post] (
     [Id]          INT           IDENTITY (1, 1) NOT NULL,
     [UserId]      INT           NOT NULL,
     [Title]       VARCHAR (50)  NOT NULL,
     [Description] VARCHAR (500) NOT NULL,
     [Content]     TEXT          NOT NULL,
     [CreatedOn]   DATETIME      NULL,
     [ModifiedOn]  DATETIME      NULL,
     [DeletedOn]   DATETIME      NULL,
     PRIMARY KEY CLUSTERED ([Id] ASC)
 );
 CREATE TABLE [dbo].[Token] (
     [Id]        INT          IDENTITY (1, 1) NOT NULL,
     [Token]     VARCHAR (50) NOT NULL,
     [UserId]    INT          NOT NULL,
     [Expiry]    DATETIME     NOT NULL,
     [DeletedOn] DATETIME     NULL,
     PRIMARY KEY CLUSTERED ([Id] ASC)
 );
 CREATE TABLE [dbo].[User] (
     [Id]                INT          IDENTITY (1, 1) NOT NULL,
     [Username]          VARCHAR (50) NOT NULL,
     [Password]          VARCHAR (50) NOT NULL,
     [Firstname]         VARCHAR (50) NOT NULL,
     [Familyname]        VARCHAR (50) NOT NULL,
     [Mobilephonenumber] VARCHAR (50) NULL,
     [Role]              VARCHAR (20) NULL,
     [Status]            VARCHAR (20) NULL,
     PRIMARY KEY CLUSTERED ([Id] ASC)
 );
 CREATE TABLE [dbo].[UserLog] (
     [Id]     INT           IDENTITY (1, 1) NOT NULL,
     [UserId] INT           NULL,
     [Action] VARCHAR (500) NULL,
     PRIMARY KEY CLUSTERED ([Id] ASC)
 );
 CREATE TABLE [dbo].[Userlogin] (
     [Id]         INT          IDENTITY (1, 1) NOT NULL,
     [UserId]     INT          NOT NULL,
     [IP]         VARCHAR (50) NULL,
     [SessionId]  VARCHAR (50) NULL,
     [CreatedOn]  DATETIME     NULL,
     [ModifiedOn] DATETIME     NULL,
     [DeletedOn]  DATETIME     NULL,
     PRIMARY KEY CLUSTERED ([Id] ASC)
 );
