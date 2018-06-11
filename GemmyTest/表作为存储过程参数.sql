CREATE TABLE [dbo].[Orders]
(    
    [Orders_nbr] INT IDENTITY(1,1) PRIMARY KEY,
    [ItemCode] NVARCHAR(50) NOT NULL,
    [UM] NVARCHAR(20) NOT NULL,
    [Quantity] DECIMAL(18,6) NOT NULL,
    [UnitPrice] DECIMAL(18,6) NOT NULL
)
GO

CREATE TYPE [dbo].[OrdersTableType] AS TABLE
    (
    ItemCode NVARCHAR(50) NOT NULL,
    UM NVARCHAR(20) NOT NULL,
    Quantity DECIMAL(18,6) NOT NULL,
    UnitPrice DECIMAL(18,6) NOT NULL 
    )

GO

CREATE PROCEDURE [dbo].[usp_Orders_Insert]
(
    @OrdersCollection [OrdersTableType] READONLY
)
AS
INSERT INTO [dbo].[Orders] ([ItemCode],[UM],[Quantity],[UnitPrice])
    SELECT oc.[ItemCode],oc.[UM],[Quantity],oc.[UnitPrice] FROM @OrdersCollection AS oc;

GO