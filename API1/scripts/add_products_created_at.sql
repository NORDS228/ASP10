IF COL_LENGTH(N'dbo.Products', N'created_at') IS NULL
BEGIN
    ALTER TABLE dbo.Products
    ADD created_at DATETIME2(7) NOT NULL
        CONSTRAINT DF_Products_created_at DEFAULT SYSUTCDATETIME() WITH VALUES;
END;
GO