USE [Soccer]
GO

/****** Object:  StoredProcedure [dbo].[AgeRange]    Script Date: 04.07.2016 2:46:53 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Maxim
-- Create date: 03.07.2016
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[AgeRange] 
	-- Add the parameters for the stored procedure here
	@minimumAge int,
	@maximumAge int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	BEGIN TRY
		SELECT *
		FROM Players
		WHERE Players.Age >= @minimumAge and Players.Age <= @maximumAge
		DECLARE @MyCount int
		SELECT @MyCount = @@ROWCOUNT
		RETURN @MyCount
	END TRY
	BEGIN CATCH
		SELECT   
			ERROR_NUMBER() AS ErrorNumber  
			,ERROR_MESSAGE() AS ErrorMessage;
	END CATCH
END

GO


