IF OBJECT_ID(N'dbo.ProductReservations', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.ProductReservations
    (
        ID_reservation INT IDENTITY(1,1) NOT NULL
            CONSTRAINT PK_ProductReservations PRIMARY KEY,
        product_ID INT NOT NULL,
        user_ID INT NOT NULL,
        quantity INT NOT NULL
            CONSTRAINT CK_ProductReservations_Quantity CHECK (quantity > 0),
        reserved_at DATETIME2 NOT NULL
            CONSTRAINT DF_ProductReservations_ReservedAt DEFAULT SYSUTCDATETIME(),
        cancelled_at DATETIME2 NULL,
        CONSTRAINT FK_ProductReservations_Products
            FOREIGN KEY (product_ID) REFERENCES dbo.Products(ID_product),
        CONSTRAINT FK_ProductReservations_Users
            FOREIGN KEY (user_ID) REFERENCES dbo.Users(ID_user)
    );
END;
