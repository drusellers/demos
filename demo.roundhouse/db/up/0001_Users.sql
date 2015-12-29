CREATE TABLE [dbo].[Users] 
(
    id int NOT NULL,
	username nvarchar(MAX) NOT NULL,
	passward_hash binary(64) NOT NULL
)
GO

ALTER TABLE [dbo].[Users] ADD CONSTRAINT
	PK_Users PRIMARY KEY CLUSTERED 
	(
	id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO