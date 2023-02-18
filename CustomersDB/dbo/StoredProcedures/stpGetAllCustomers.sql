CREATE PROCEDURE [dbo].[stpGetAllCustomers]	
    @userId INT
AS

SET NOCOUNT ON

SELECT 
    [UniqueId], 
    [Names], 
    [SureNames], 
    [CreatedDate], 
    [BirthDate], 
    [PhoneNumber],
    [Address], 
    [EmailAddress], 
    [IdentificationNumber]
FROM [dbo].[Customers]


GO