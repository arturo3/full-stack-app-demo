CREATE OR ALTER PROCEDURE dbo.GetProductsByFilterPaginated
(
    @SearchTerm NVARCHAR(250) = NULL,
    @CategoryId INT = NULL,
    @MinPrice DECIMAL(18,2) = NULL,
    @MaxPrice DECIMAL(18,2) = NULL,
    @InStock BIT = NULL,
    @SortBy NVARCHAR(50) = 'createddate',  -- 'name' | 'price' | 'createdDate' | 'stockQuantity' (case-insensitive)
    @SortOrder NVARCHAR(4) = 'asc',  -- 'asc' | 'desc'
    @PageNumber INT = 1,
    @PageSize INT = 25
)
AS
BEGIN
    SET NOCOUNT ON;

	--
    -- Validate parameters
	--
    IF (@PageSize > 200) SET @PageSize = 200;

	--
	-- Helper variables
	--
	DECLARE @TotalCount INT;
    DECLARE @Offset INT = (@PageNumber - 1) * @PageSize;

	--
	-- Get list of product IDs given filter criteria
	--
    CREATE TABLE #FilteredProductIDs
    (
        ProductId INT NOT NULL PRIMARY KEY
    );

    INSERT INTO #FilteredProductIDs (ProductId)
		SELECT P.Id
		FROM dbo.Product AS P
		WHERE 1 = 1
		  AND (@CategoryId IS NULL OR P.CategoryId = @CategoryId)
		  AND (@MinPrice IS NULL OR P.Price >= @MinPrice)
		  AND (@MaxPrice IS NULL OR P.Price <= @MaxPrice)
		  AND (@InStock IS NULL OR (@InStock = 0) OR (P.StockQuantity > 0))
		  AND (
				@SearchTerm IS NULL
				OR p.[Name]        LIKE N'%' + @SearchTerm + N'%'
				OR p.[Description] LIKE N'%' + @SearchTerm + N'%'
			  );

    
    SELECT @TotalCount = COUNT(*) FROM #FilteredProductIDs;

	-- 
	-- Final result set
	-- 
    SELECT
        P.Id,
        P.[Name],
        P.[Description],
        P.Price,
        P.StockQuantity,
        P.CreatedDate,
        P.IsActive,
        P.CategoryId,
        C.[Name] AS CategoryName
    FROM #FilteredProductIDs AS F
		INNER JOIN dbo.Product AS P ON P.Id = F.ProductId
		INNER JOIN dbo.Category AS C ON C.Id = P.CategoryId
    ORDER BY
        CASE WHEN @SortBy = 'name'          AND @SortOrder = 'asc'  THEN p.[Name] END ASC,
        CASE WHEN @SortBy = 'name'          AND @SortOrder = 'desc' THEN p.[Name] END DESC,
        CASE WHEN @SortBy = 'price'         AND @SortOrder = 'asc'  THEN p.Price END ASC,
        CASE WHEN @SortBy = 'price'         AND @SortOrder = 'desc' THEN p.Price END DESC,
        CASE WHEN @SortBy = 'createddate'   AND @SortOrder = 'asc'  THEN p.CreatedDate END ASC,
        CASE WHEN @SortBy = 'createddate'   AND @SortOrder = 'desc' THEN p.CreatedDate END DESC,
        CASE WHEN @SortBy = 'stockquantity' AND @SortOrder = 'asc'  THEN p.StockQuantity END ASC,
        CASE WHEN @SortBy = 'stockquantity' AND @SortOrder = 'desc' THEN p.StockQuantity END DESC
    OFFSET @Offset ROWS
    FETCH NEXT @PageSize ROWS ONLY;

    SELECT @TotalCount AS TotalCount;
END;
