CREATE TABLE [dbo].[Activity]
(
	[Id] INT IDENTITY NOT NULL CONSTRAINT PK_Activity PRIMARY KEY,
	[ParentId] INT NULL CONSTRAINT FK_Activity_Activity_Parent FOREIGN KEY REFERENCES [Activity]([Id]),
	[ApplicationId] INT NULL CONSTRAINT FK_Activity_Application FOREIGN KEY REFERENCES [Application]([Id]),
	[PublicId] VARCHAR(36) NOT NULL,
	[Name] VARCHAR(500) NOT NULL,
	[ActivityTypeId] TINYINT NOT NULL CONSTRAINT FK_Actitvity_ActivityType REFERENCES [ActivityType](Id),
	[UpdatedOn] DATETIME NOT NULL CONSTRAINT DF_Activity_UpdatedOn DEFAULT (GETDATE()),
	[ClosedOn] DATETIME,
	[DeletedOn] DATETIME,
	CONSTRAINT UQ_Activity_ActivityTypeId_PublicId UNIQUE ([ActivityTypeId], [PublicId])
)
