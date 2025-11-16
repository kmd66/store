USE [MizeBaziStore]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('sp.GetCategory'))
	DROP PROCEDURE sp.GetCategory
GO

CREATE PROCEDURE sp.GetCategory
	@Id BIGINT,
	@UnicId UNIQUEIDENTIFIER
WITH ENCRYPTION
AS
BEGIN
	SET NOCOUNT ON;

	SELECT TOP 1 * FROM Categorys
	WHERE Id = @Id OR UnicId = @UnicId
END