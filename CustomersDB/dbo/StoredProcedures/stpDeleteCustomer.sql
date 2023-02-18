CREATE PROCEDURE [dbo].[stpDeleteCustomer]
	@userId INT,
    @uniqueId INT,
	@result INT OUTPUT
AS

SET NOCOUNT ON

SET @result = 0

DELETE FROM dbo.Customers WHERE UniqueId = @uniqueId 

IF(@@ROWCOUNT > 0 )
	SET @result = 1

GO
