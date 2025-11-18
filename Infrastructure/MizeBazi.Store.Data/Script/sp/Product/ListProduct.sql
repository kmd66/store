USE [MizeBaziStore]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('sp.ListProduct'))
	DROP PROCEDURE sp.ListProduct
GO

CREATE PROCEDURE sp.ListProduct
	@Name NVARCHAR(MAx),
	@Description NVARCHAR(MAx),
    @MaxPrice DECIMAL,
    @MinPrice DECIMAL,
    @BrandId    BIGINT,
    @CategoryId BIGINT,
	@HasDiscount BIT,
	@HasQuantity BIT,
	@IsPublished BIT,
	@IsDeleted BIT,
	@PageSize INT,
	@PageIndex INT
WITH ENCRYPTION
AS
BEGIN
	SET NOCOUNT ON;

	SELECT 
		p.*,
		b.[Name] BrandName,
        (
			SELECT TOP 5 c.Id, c.[Name]
		    FROM Categorys c
		    INNER JOIN ProductCategorys pc ON pc.CategoryId = c.Id
		    WHERE pc.ProductId = p.Id AND c.IsDeleted = 0
		    FOR JSON PATH
		) AS CategoriesJson,
		COUNT(p.Id) OVER() AS TotalCount
	FROM Products p 
	INNER JOIN Brands b ON b.Id = p.BrandId AND b.IsDeleted = 0
	WHERE (@IsDeleted IS NULL OR p.IsDeleted = @IsDeleted)
		AND (@IsPublished IS NULL OR IsPublished = @IsPublished)
		AND (@Name IS NULL OR p.[Name] Like '%'+ @Name +'%')
		AND (@Description IS NULL OR p.[Description] Like '%'+ @Description +'%')
		AND (@MaxPrice IS NULL OR Price <= @MaxPrice)
		AND (@MinPrice IS NULL OR Price >= @MinPrice)
		AND (@BrandId IS NULL OR BrandId = @BrandId)
		AND (@HasDiscount IS NULL OR [CompareAtPrice] > 0)
		AND (@HasQuantity IS NULL OR Quantity > 0)
	ORDER BY Id DESC
	OFFSET ((@PageIndex - 1) * @PageSize) ROWS FETCH NEXT @PageSize ROWS ONLY

	--OPTION(RECOMPILE);
END