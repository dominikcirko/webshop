CREATE PROCEDURE sp_CreateUser 
    @Username varchar(50),
	@FirstName varchar(50),
	@LastName varchar(50),
	@Password varchar(50),
	@Email varchar(100),
	@PhoneNumber varchar(50),
	@IsAdmin bit,
    @IDUser int OUTPUT 
AS
	BEGIN
		INSERT INTO [User](Username, FirstName, LastName, [Password], Email, PhoneNumber, IsAdmin)
		VALUES(@Username, @FirstName, @LastName, @Password, @Email, @PhoneNumber, @IsAdmin)
		SET @IDUser = SCOPE_IDENTITY();
	END
GO

CREATE PROCEDURE sp_GetUserById
    @IDUser int 
AS
    SELECT  * FROM [User] where IDUser = @IDUser
GO

CREATE PROCEDURE sp_GetAllUsers
AS
	SELECT * FROM [USER]
GO

CREATE PROCEDURE sp_UpdateUser
    @IDUser INT,
    @Username NVARCHAR(50),
    @Password NVARCHAR(100),
    @Email NVARCHAR(100),
    @FirstName NVARCHAR(50),
    @LastName NVARCHAR(50),
    @PhoneNumber NVARCHAR(50),
    @IsAdmin BIT
AS
BEGIN
    UPDATE [User]
    SET 
        Username = @Username,
        [Password] = @Password,
        Email = @Email,
        FirstName = @FirstName,
        LastName = @LastName,
        PhoneNumber = @PhoneNumber,
        IsAdmin = @IsAdmin
    WHERE IDUser = @IDUser;
END;
GO

CREATE PROCEDURE sp_DeleteUser
    @IDUser int 
AS
    DELETE FROM [User] where IDUser = @IDUser
GO

CREATE PROCEDURE sp_CreateItemCategory
    @CategoryName NVARCHAR(50),
    @IDItemCategory INT OUTPUT
AS
BEGIN
    INSERT INTO ItemCategory (CategoryName)
    VALUES (@CategoryName);
    SET @IDItemCategory = SCOPE_IDENTITY();
END;
GO

CREATE PROCEDURE sp_GetItemCategoryById
    @IDItemCategory INT
AS
BEGIN
    SELECT * 
    FROM ItemCategory
    WHERE IDItemCategory = @IDItemCategory;
END;
GO

CREATE PROCEDURE sp_GetAllItemCategories
AS
BEGIN
    SELECT * 
    FROM ItemCategory;
END;
GO

CREATE PROCEDURE sp_UpdateItemCategory
    @IDItemCategory INT,
    @CategoryName NVARCHAR(50)
AS
BEGIN
    UPDATE ItemCategory
    SET CategoryName = @CategoryName
    WHERE IDItemCategory = @IDItemCategory;
END;
GO

CREATE PROCEDURE sp_DeleteItemCategory
    @IDItemCategory INT
AS
BEGIN
    DELETE FROM ItemCategory
    WHERE IDItemCategory = @IDItemCategory;
END;
GO

CREATE PROCEDURE sp_CreateTag
    @Name NVARCHAR(50),
    @IDTag INT OUTPUT
AS
BEGIN
    INSERT INTO Tag ([Name])
    VALUES (@Name);
    SET @IDTag = SCOPE_IDENTITY();
END;
GO

CREATE PROCEDURE sp_GetTagById
    @IDTag INT
AS
BEGIN
    SELECT * 
    FROM Tag
    WHERE IDTag = @IDTag;
END;
GO

CREATE PROCEDURE sp_GetAllTags
AS
BEGIN
    SELECT * 
    FROM Tag;
END;
GO

CREATE PROCEDURE sp_UpdateTag
    @IDTag INT,
    @Name NVARCHAR(50)
AS
BEGIN
    UPDATE Tag
    SET [Name] = @Name
    WHERE IDTag = @IDTag;
END;
GO

CREATE PROCEDURE sp_DeleteTag
    @IDTag INT
AS
BEGIN
    DELETE FROM Tag
    WHERE IDTag = @IDTag;
END;
GO

CREATE PROCEDURE sp_CreateItem
    @ItemCategoryID INT,
    @TagID INT,
    @Title NVARCHAR(50),
    @Description NVARCHAR(MAX),
    @StockQuantity INT,
    @InStock BIT,
    @Price DECIMAL(10,2),
    @Weight DECIMAL(10,2),
    @IDItem INT OUTPUT
AS
BEGIN
    INSERT INTO Item (ItemCategoryID, TagID, Title, [Description], StockQuantity, InStock, Price, [Weight])
    VALUES (@ItemCategoryID, @TagID, @Title, @Description, @StockQuantity, @InStock, @Price, @Weight);
    SET @IDItem = SCOPE_IDENTITY();
END;
GO

CREATE PROCEDURE sp_GetItemById
    @IDItem INT
AS
BEGIN
    SELECT * 
    FROM Item
    WHERE IDItem = @IDItem;
END;
GO

CREATE PROCEDURE sp_GetAllItems
AS
BEGIN
    SELECT * 
    FROM Item;
END;
GO

CREATE PROCEDURE sp_UpdateItem
    @IDItem INT,
    @ItemCategoryID INT,
    @TagID INT,
    @Title NVARCHAR(50),
    @Description NVARCHAR(MAX),
    @StockQuantity INT,
    @InStock BIT,
    @Price DECIMAL(10,2),
    @Weight DECIMAL(10,2)
AS
BEGIN
    UPDATE Item
    SET 
        ItemCategoryID = @ItemCategoryID,
        TagID = @TagID,
        Title = @Title,
        [Description] = @Description,
        StockQuantity = @StockQuantity,
        InStock = @InStock,
        Price = @Price,
        [Weight] = @Weight
    WHERE IDItem = @IDItem;
END;
GO

CREATE PROCEDURE sp_DeleteItem
    @IDItem INT
AS
BEGIN
    DELETE FROM Item
    WHERE IDItem = @IDItem;
END;
GO

CREATE PROCEDURE sp_CreateStatus
    @Name NVARCHAR(50),
    @Description NVARCHAR(MAX),
    @IDStatus INT OUTPUT
AS
BEGIN
    INSERT INTO [Status] ([Name], [Description])
    VALUES (@Name, @Description);
    SET @IDStatus = SCOPE_IDENTITY();
END;
GO

CREATE PROCEDURE sp_GetStatusById
    @IDStatus INT
AS
BEGIN
    SELECT * 
    FROM [Status]
    WHERE IDStatus = @IDStatus;
END;
GO

CREATE PROCEDURE sp_GetAllStatuses
AS
BEGIN
    SELECT * 
    FROM [Status];
END;
GO

CREATE PROCEDURE sp_UpdateStatus
    @IDStatus INT,
    @Name NVARCHAR(50),
    @Description NVARCHAR(MAX)
AS
BEGIN
    UPDATE [Status]
    SET 
        [Name] = @Name,
        [Description] = @Description
    WHERE IDStatus = @IDStatus;
END;
GO

CREATE PROCEDURE sp_DeleteStatus
    @IDStatus INT
AS
BEGIN
    DELETE FROM [Status]
    WHERE IDStatus = @IDStatus;
END;
GO

CREATE PROCEDURE sp_CreateOrder
    @UserID INT,
    @StatusID INT,
    @OrderDate DATETIME = NULL,
    @TotalAmount DECIMAL(10, 2),
    @IDOrder INT OUTPUT
AS
BEGIN
    IF @OrderDate IS NULL
        SET @OrderDate = GETDATE();

    INSERT INTO [Order] (UserID, StatusID, OrderDate, TotalAmount)
    VALUES (@UserID, @StatusID, @OrderDate, @TotalAmount);

    SET @IDOrder = SCOPE_IDENTITY();
END;
GO

CREATE PROCEDURE sp_GetOrderById
    @IDOrder INT
AS
BEGIN
    SELECT * 
    FROM [Order]
    WHERE IDOrder = @IDOrder;
END;
GO

CREATE PROCEDURE sp_GetAllOrders
AS
BEGIN
    SELECT * 
    FROM [Order];
END;
GO

CREATE PROCEDURE sp_UpdateOrder
    @IDOrder INT,
    @UserID INT,
    @StatusID INT,
    @OrderDate DATETIME,
    @TotalAmount DECIMAL(10, 2)
AS
BEGIN
    UPDATE [Order]
    SET 
        UserID = @UserID,
        StatusID = @StatusID,
        OrderDate = @OrderDate,
        TotalAmount = @TotalAmount
    WHERE IDOrder = @IDOrder;
END;
GO

CREATE PROCEDURE sp_DeleteOrder
    @IDOrder INT
AS
BEGIN
    DELETE FROM [Order]
    WHERE IDOrder = @IDOrder;
END;
GO

CREATE PROCEDURE sp_CreateOrderItem
    @OrderID INT,
    @ItemID INT,
    @Quantity INT,
    @IDOrderItem INT OUTPUT
AS
BEGIN
    INSERT INTO OrderItem (OrderID, ItemID, Quantity)
    VALUES (@OrderID, @ItemID, @Quantity);

    SET @IDOrderItem = SCOPE_IDENTITY();
END;
GO

CREATE PROCEDURE sp_GetOrderItemById
    @IDOrderItem INT
AS
BEGIN
    SELECT * 
    FROM OrderItem
    WHERE IDOrderItem = @IDOrderItem;
END;
GO

CREATE PROCEDURE sp_GetAllOrderItems
AS
BEGIN
    SELECT * 
    FROM OrderItem;
END;
GO

CREATE PROCEDURE sp_UpdateOrderItem
    @IDOrderItem INT,
    @OrderID INT,
    @ItemID INT,
    @Quantity INT
AS
BEGIN
    UPDATE OrderItem
    SET 
        OrderID = @OrderID,
        ItemID = @ItemID,
        Quantity = @Quantity
    WHERE IDOrderItem = @IDOrderItem;
END;
GO

CREATE PROCEDURE sp_DeleteOrderItem
    @IDOrderItem INT
AS
BEGIN
    DELETE FROM OrderItem
    WHERE IDOrderItem = @IDOrderItem;
END;
GO

CREATE PROCEDURE sp_CreateCart
    @UserID INT,
    @IDCart INT OUTPUT
AS
BEGIN
    INSERT INTO Cart (UserID)
    VALUES (@UserID);

    SET @IDCart = SCOPE_IDENTITY();
END;
GO

CREATE PROCEDURE sp_GetCartById
    @IDCart INT
AS
BEGIN
    SELECT * 
    FROM Cart
    WHERE IDCart = @IDCart;
END;
GO

CREATE PROCEDURE sp_GetAllCarts
AS
BEGIN
    SELECT * 
    FROM Cart;
END;
GO

CREATE PROCEDURE sp_UpdateCart
    @IDCart INT,
    @UserID INT
AS
BEGIN
    UPDATE Cart
    SET UserID = @UserID
    WHERE IDCart = @IDCart;
END;
GO

CREATE PROCEDURE sp_DeleteCart
    @IDCart INT
AS
BEGIN
    DELETE FROM Cart
    WHERE IDCart = @IDCart;
END;
GO

CREATE PROCEDURE sp_CreateCartItem
    @CartID INT,
    @ItemID INT,
    @Quantity INT,
    @IDCartItem INT OUTPUT
AS
BEGIN
    INSERT INTO CartItem (CartID, ItemID, Quantity)
    VALUES (@CartID, @ItemID, @Quantity);

    SET @IDCartItem = SCOPE_IDENTITY();
END;
GO

CREATE PROCEDURE sp_GetCartItemById
    @IDCartItem INT
AS
BEGIN
    SELECT * 
    FROM CartItem
    WHERE IDCartItem = @IDCartItem;
END;
GO

CREATE PROCEDURE sp_GetAllCartItems
AS
BEGIN
    SELECT * 
    FROM CartItem;
END;
GO

CREATE PROCEDURE sp_UpdateCartItem
    @IDCartItem INT,
    @CartID INT,
    @ItemID INT,
    @Quantity INT
AS
BEGIN
    UPDATE CartItem
    SET CartID = @CartID,
        ItemID = @ItemID,
        Quantity = @Quantity
    WHERE IDCartItem = @IDCartItem;
END;
GO


CREATE PROCEDURE sp_DeleteCartItem
    @IDCartItem INT
AS
BEGIN
    DELETE FROM CartItem
    WHERE IDCartItem = @IDCartItem;
END;
GO