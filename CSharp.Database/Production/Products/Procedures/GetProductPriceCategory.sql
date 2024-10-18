
-- =============================================
-- Author:		Osmar Martinez
-- Create date: 2024/10/18
-- Description:	Geting the list of products by category
-- Exec: 		EXEC [Production].[GetProductPriceCategory] 319
-- =============================================
CREATE OR ALTER PROC [Production].[GetProductPriceCategory]
    @ProductID INT = NULL
AS
BEGIN

    SELECT 
        [pro].[ProductID],
        [pro].[Name],
        [pro].[ProductNumber],
        [pro].[Color],
        [pro].[StandardCost],
        [pro].[ListPrice],
        [pro].[Size],
        [pro].[SizeUnitMeasureCode],
        [cat].[ProductCategoryID],
        [cat].[Name] [ProductCategory],
        [pro].[ProductSubcategoryID],
        [sub].[Name] [ProductSubcategory]
    FROM [Production].[Product] pro
    LEFT JOIN Production.ProductSubcategory sub ON [pro].[ProductSubcategoryID] = [sub].[ProductSubcategoryID]
    LEFT JOIN Production.ProductCategory cat ON [sub].[ProductCategoryID] = [cat].[ProductCategoryID]
    WHERE 
        (@ProductID IS NULL OR @ProductID = [pro].[ProductID])

END


