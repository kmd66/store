USE [MizeBaziStore]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('sp.LsitCategory'))
	DROP PROCEDURE sp.LsitCategory
GO

CREATE PROCEDURE sp.LsitCategory
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
	FROM Categorys
	WHERE (@IsDeleted IS NULL OR IsDeleted = @IsDeleted)
		AND (@Name IS NULL OR [Name] Like '%'+ @Name +'%')
	ORDER BY Id DESC
	OFFSET ((@PageIndex - 1) * @PageSize) ROWS FETCH NEXT @PageSize ROWS ONLY
	--OPTION(RECOMPILE);
END