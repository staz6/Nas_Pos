BEGIN TRANSACTION;
CREATE TABLE IF NOT EXISTS "Transactions" (
	"Id"	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	"Amount"	REAL NOT NULL,
	"TransactionType"	TEXT,
	"Description"	TEXT,
	"status"	INTEGER NOT NULL,
	"LedgerId"	INTEGER,
	"CreatedAt"	TEXT NOT NULL,
	CONSTRAINT "FK_Transactions_Ledgers_LedgerId" FOREIGN KEY("LedgerId") REFERENCES "Ledgers"("Id") ON DELETE CASCADE
);
CREATE TABLE IF NOT EXISTS "OrderItems" (
	"Id"	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	"ItemOrdered_ProductId"	INTEGER,
	"ItemOrdered_ProductName"	TEXT,
	"ItemOrdered_PictureUrl"	TEXT,
	"Price"	REAL NOT NULL,
	"Quantity"	REAL NOT NULL,
	"OrderId"	INTEGER,
	"CreatedAt"	TEXT NOT NULL,
	CONSTRAINT "FK_OrderItems_Orders_OrderId" FOREIGN KEY("OrderId") REFERENCES "Orders"("Id") ON DELETE CASCADE
);
CREATE TABLE IF NOT EXISTS "Ledgers" (
	"Id"	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	"OrderId"	INTEGER,
	"TotalAmount"	REAL NOT NULL,
	"AmountPaid"	REAL NOT NULL,
	"AmountRemaining"	REAL NOT NULL,
	"IsDebit"	INTEGER NOT NULL,
	"CreatedAt"	TEXT NOT NULL,
	CONSTRAINT "FK_Ledgers_Orders_OrderId" FOREIGN KEY("OrderId") REFERENCES "Orders"("Id") ON DELETE RESTRICT
);
CREATE TABLE IF NOT EXISTS "Products" (
	"Id"	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	"Title"	TEXT,
	"UnitOfMeasure"	INTEGER NOT NULL,
	"PictureUrl"	TEXT,
	"PurchasedPrice"	REAL NOT NULL,
	"Price"	REAL NOT NULL,
	"Stock"	REAL NOT NULL,
	"Description"	TEXT,
	"MinimumThreshold"	INTEGER NOT NULL,
	"ProductTypeId"	INTEGER NOT NULL,
	"ProductShelvesId"	INTEGER,
	"CreatedAt"	TEXT NOT NULL,
	CONSTRAINT "FK_Products_ProductTypes_ProductTypeId" FOREIGN KEY("ProductTypeId") REFERENCES "ProductTypes"("Id") ON DELETE CASCADE,
	CONSTRAINT "FK_Products_ProductShelves_ProductShelvesId" FOREIGN KEY("ProductShelvesId") REFERENCES "ProductShelves"("Id") ON DELETE RESTRICT
);
CREATE TABLE IF NOT EXISTS "Orders" (
	"Id"	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	"CustomerId"	INTEGER,
	"OrderDate"	TEXT NOT NULL,
	"ShipToAddress_FirstName"	TEXT,
	"ShipToAddress_LastName"	TEXT,
	"ShipToAddress_street"	TEXT,
	"ShipToAddress_City"	TEXT,
	"ShipToAddress_State"	TEXT,
	"ShipToAddress_Zipcode"	TEXT,
	"DeliveryMethodId"	INTEGER,
	"PaymentMethodId"	INTEGER,
	"Subtotal"	REAL NOT NULL,
	"Status"	TEXT NOT NULL,
	"CreatedAt"	TEXT NOT NULL,
	CONSTRAINT "FK_Orders_PaymentMethod_PaymentMethodId" FOREIGN KEY("PaymentMethodId") REFERENCES "PaymentMethod"("Id") ON DELETE RESTRICT,
	CONSTRAINT "FK_Orders_DeliveryMethods_DeliveryMethodId" FOREIGN KEY("DeliveryMethodId") REFERENCES "DeliveryMethods"("Id") ON DELETE RESTRICT,
	CONSTRAINT "FK_Orders_Customer_CustomerId" FOREIGN KEY("CustomerId") REFERENCES "Customer"("Id") ON DELETE RESTRICT
);
CREATE TABLE IF NOT EXISTS "BasketItems" (
	"Id"	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	"ProductId"	INTEGER NOT NULL,
	"ProductName"	TEXT,
	"Price"	REAL NOT NULL,
	"Quantity"	REAL NOT NULL,
	"PictureUrl"	TEXT,
	"BasketId"	INTEGER,
	"CreatedAt"	TEXT NOT NULL,
	CONSTRAINT "FK_BasketItems_Baskets_BasketId" FOREIGN KEY("BasketId") REFERENCES "Baskets"("Id") ON DELETE CASCADE
);
CREATE TABLE IF NOT EXISTS "ProductTypes" (
	"Id"	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	"Title"	TEXT,
	"CreatedAt"	TEXT NOT NULL
);
CREATE TABLE IF NOT EXISTS "ProductShelves" (
	"Id"	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	"Title"	TEXT,
	"CreatedAt"	TEXT NOT NULL
);
CREATE TABLE IF NOT EXISTS "PaymentMethod" (
	"Id"	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	"Type"	TEXT,
	"Description"	TEXT,
	"CreatedAt"	TEXT NOT NULL
);
CREATE TABLE IF NOT EXISTS "DeliveryMethods" (
	"Id"	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	"ShortName"	TEXT,
	"DeliveryTime"	TEXT,
	"Description"	TEXT,
	"price"	REAL NOT NULL,
	"CreatedAt"	TEXT NOT NULL
);
CREATE TABLE IF NOT EXISTS "Customer" (
	"Id"	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	"FirstName"	TEXT,
	"LastName"	TEXT,
	"Email"	TEXT,
	"ContactNo"	TEXT,
	"Occupation"	TEXT,
	"street"	TEXT,
	"City"	TEXT,
	"State"	TEXT,
	"Zipcode"	TEXT,
	"CreatedAt"	TEXT NOT NULL
);
CREATE TABLE IF NOT EXISTS "Baskets" (
	"Id"	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	"EmployeeId"	TEXT,
	"CreatedAt"	TEXT NOT NULL
);
CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
	"MigrationId"	TEXT NOT NULL,
	"ProductVersion"	TEXT NOT NULL,
	CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY("MigrationId")
);
INSERT INTO "__EFMigrationsHistory" VALUES ('20210922095700_initCreate','5.0.7');
CREATE INDEX IF NOT EXISTS "IX_Transactions_LedgerId" ON "Transactions" (
	"LedgerId"
);
CREATE INDEX IF NOT EXISTS "IX_Products_ProductTypeId" ON "Products" (
	"ProductTypeId"
);
CREATE INDEX IF NOT EXISTS "IX_Products_ProductShelvesId" ON "Products" (
	"ProductShelvesId"
);
CREATE INDEX IF NOT EXISTS "IX_Orders_PaymentMethodId" ON "Orders" (
	"PaymentMethodId"
);
CREATE INDEX IF NOT EXISTS "IX_Orders_DeliveryMethodId" ON "Orders" (
	"DeliveryMethodId"
);
CREATE INDEX IF NOT EXISTS "IX_Orders_CustomerId" ON "Orders" (
	"CustomerId"
);
CREATE INDEX IF NOT EXISTS "IX_OrderItems_OrderId" ON "OrderItems" (
	"OrderId"
);
CREATE INDEX IF NOT EXISTS "IX_Ledgers_OrderId" ON "Ledgers" (
	"OrderId"
);
CREATE INDEX IF NOT EXISTS "IX_BasketItems_BasketId" ON "BasketItems" (
	"BasketId"
);
COMMIT;
