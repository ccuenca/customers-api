CREATE PROCEDURE [dbo].[stpGetCustomerByUniqueId]
    @userId INT,
	@uniqueId INT
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
WHERE UniqueId = @uniqueId

GO

