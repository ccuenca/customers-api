CREATE PROCEDURE [dbo].[stpUpdateCustomer]
	@userId INT,
    @uniqueId INT,
    @names NVARCHAR(200),
    @sureNames NVARCHAR(200),
    @birthDate SMALLDATETIME,
    @phoneNumber NVARCHAR(50),
    @address NVARCHAR(200),
    @emailAddress NVARCHAR(200),
    @identificationNumber NVARCHAR(100),
    @result INT OUTPUT
AS

SET NOCOUNT ON

SET @result = 0
	
UPDATE Customers
    SET
        [Names] = @names, 
        [SureNames] = @sureNames,  
        [BirthDate] = @birthDate,
        [PhoneNumber] = @phoneNumber,
        [Address] = @address, 
        [EmailAddress] = @emailAddress, 
        [IdentificationNumber] = @identificationNumber
WHERE [UniqueId] = @uniqueId

IF(@@ROWCOUNT > 0 )
	SET @result = 1

