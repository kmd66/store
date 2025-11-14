USE [MizeBaziStore]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('sp.LsitBrand'))
	DROP PROCEDURE sp.LsitBrand
GO

CREATE PROCEDURE sp.LsitBrand
	@Name NVARCHAR(MAx),
	@IsDeleted BIT,
	@PageSize INT,
	@PageIndex INT
WITH ENCRYPTION
AS
BEGIN
	SET NOCOUNT ON;

	SELECT *,
		COUNT(Id) OVER() AS TotalCount
	FROM Brands
	WHERE (@IsDeleted IS NULL OR IsDeleted = @IsDeleted)
		AND (@Name IS NULL OR Name = @Name)
	ORDER BY Id DESC
	OFFSET ((@PageIndex - 1) * @PageSize) ROWS FETCH NEXT @PageSize ROWS ONLY
	--OPTION(RECOMPILE);
END