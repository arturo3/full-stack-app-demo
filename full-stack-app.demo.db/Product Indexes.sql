CREATE INDEX IX_Product_CategoryId_CreatedDate
ON dbo.Product (CategoryId, CreatedDate DESC)
INCLUDE ([Name], [Description], Price, StockQuantity, IsActive);

CREATE INDEX IX_Product_CategoryId_CreatedDate
ON dbo.Product (CategoryId, Price)
INCLUDE ([Name], [Description], Price, StockQuantity, IsActive);