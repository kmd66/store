USE [MizeBaziStore]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('sp.EditeProductCategory'))
	DROP PROCEDURE sp.EditeProductCategory
GO

CREATE PROCEDURE sp.EditeProductCategory
	@Json NVARCHAR(max),
	@ProductId BIGINT
WITH ENCRYPTION
AS
BEGIN
	SET NOCOUNT ON;
	
	--DELETE pc  FROM OPENJSON(@Json) v
	--INNER JOIN ProductCategory pc ON v.value = pc.CategoryId
	DELETE ProductCategorys WHERE ProductId = @ProductId
	
	INSERT INTO ProductCategorys (ProductId, CategoryId)
	SELECT @ProductId, value
	FROM OPENJSON(@Json)

	OPTION(RECOMPILE);
END