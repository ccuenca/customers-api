CREATE PROCEDURE [dbo].[stpGetCustomerByIdentification]
    @userId INT,
	@identificaionNumber VARCHAR(100)
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
WHERE IdentificationNumber = @identificaionNumber

GO