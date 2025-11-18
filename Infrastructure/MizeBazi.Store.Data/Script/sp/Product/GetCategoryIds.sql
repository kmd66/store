USE [MizeBaziStore]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('sp.GetCategoryIds'))
	DROP PROCEDURE sp.GetCategoryIds
GO

CREATE PROCEDURE sp.GetCategoryIds
	@Json NVARCHAR(max)
WITH ENCRYPTION
AS
BEGIN
	SET NOCOUNT ON;

	SELECT c.Id FROM OPENJSON(@Json) v
	INNER JOIN Categorys c ON c.Id = v.value

	OPTION(RECOMPILE);
END