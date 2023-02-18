CREATE PROCEDURE [dbo].[stpCreateCustomer]
	@userId INT,
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
	
INSERT INTO [dbo].[Customers] ( 
    [Names], 
    [SureNames],  
    [BirthDate],
    [PhoneNumber],
    [Address], 
    [EmailAddress], 
    [IdentificationNumber])

VALUES (@names, 
        @sureNames, 
        @birthDate, 
        @phoneNumber, 
        @address, 
        @emailAddress, 
        @identificationNumber  )

IF(@@ROWCOUNT > 0 )
	SET @result = SCOPE_IDENTITY();

GO