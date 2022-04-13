SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Pedro Henrique Moreira
-- Create date: March 31, 2022
-- Description:	Retrieves [dbo].[NOTA_FICAL] filtered by a month
-- =============================================
CREATE PROCEDURE GetNotasFiscaisPorMes
	@Mes int
	
AS
BEGIN
	SET NOCOUNT ON;

	SELECT *
	  FROM [dbo].[NOTA_FISCAL]
	  WHERE MONTH(DTEMISSAO) = @Mes
END
GO
