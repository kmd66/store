USE [MizeBaziStore]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('sp.LsitProductCategory'))
	DROP PROCEDURE sp.LsitProductCategory
GO

CREATE PROCEDURE sp.LsitProductCategory
	@Id BIGINT
WITH ENCRYPTION
AS
BEGIN
	SET NOCOUNT ON;
	
	SELECT * FROM ProductCategorys WHERE  ProductId = @Id
	OPTION(RECOMPILE);
END