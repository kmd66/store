USE [MizeBaziStore]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('sp.GetProduct'))
	DROP PROCEDURE sp.GetProduct
GO

CREATE PROCEDURE sp.GetProduct
	@Id BIGINT,
	@UnicId UNIQUEIDENTIFIER,
	@sku NVARCHAR(MAx)
WITH ENCRYPTION
AS
BEGIN
	SET NOCOUNT ON;
	
	SELECT TOP 1
		p.*,
		b.[Name] BrandName,
        (
			SELECT TOP 5 c.Id, c.[Name]
		    FROM Categorys c
		    INNER JOIN ProductCategorys pc ON pc.CategoryId = c.Id
		    WHERE pc.ProductId = p.Id AND c.IsDeleted = 0
		    FOR JSON PATH
		) AS CategoriesJson
	FROM Products p 
	INNER JOIN Brands b ON b.Id = p.BrandId AND b.IsDeleted = 0
	WHERE p.IsDeleted = 0
		AND (
			p.Id = @Id OR p.UnicId = @UnicId OR p.SKU = @sku
		)

	--OPTION(RECOMPILE);
END