CREATE TABLE [dbo].[Customers]
(
	[UniqueId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Names] NVARCHAR(200) NOT NULL, 
    [SureNames] NVARCHAR(200) NOT NULL, 
    [CreatedDate] SMALLDATETIME NOT NULL DEFAULT getDate(), 
    [BirthDate] SMALLDATETIME NOT NULL, 
    [PhoneNumber] NVARCHAR(50) NOT NULL,
    [Address] NVARCHAR(200) NOT NULL,
    [EmailAddress] NVARCHAR(200) NOT NULL, 
    [IdentificationNumber] NVARCHAR(100) NOT NULL
  
)
