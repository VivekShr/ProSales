Create Database ECommerce


Create Table Product
(
	ProductId INT Identity Primary Key,
	Name Nvarchar(1000),
	CostPrice Decimal (20,2),
	SellingPrice Decimal (20,2)
)

Create Table Customer
(
	CustomerId INT Identity Primary Key,
	Name Nvarchar(500),
	ContactNo varchar(20)
)

Create Table SalesTransaction
(
	SalesTransactionId INT Identity Primary Key,
	ProductId INT Foreign Key References Product (ProductId),
	Price Decimal (20,2),
	Quantity INT,
	Total Decimal (20,2),
	CustomerId INT Foreign Key References Customer (CustomerId),
	SalesDate Datetime 
)

Create Table Invoice
(
	InvoiceId INT Identity Primary Key,
	InvoiceNumber INT NOT NULL,
	InvoiceDate Datetime NOT NULL,
	InvoiceTotal Decimal(20,2),

)

Alter Table Product
Add ProductImage Nvarchar(MAX)

Insert Into Product (Name,CostPrice, SellingPrice, ProductImage)
Values ('One Plus One', 350, 450, '')

Insert Into Product (Name,CostPrice, SellingPrice, ProductImage)
Values ('Samsung Galaxy Note 4', 300, 350, ''),
	('Samsung Galaxy S2', 200, 225, ''),
	('Apple Iphone 6', 600, 850, ''),
	('Apple Iphone 6+', 700, 900, '')

Insert Into Customer (Name, ContactNo)
Values ('Bidur Shrestha','9841222333'),
	('Kumar Shrestha','9841222444'),
	('Manisha Rajbhandari','9841222555'),
	('Goma Shakya','9841222666'),
	('Mithun Chaudhary','9841222777'),
	('Uday Karki','9841222883')

Insert Into SalesTransaction (ProductId, Price, Quantity, CustomerId, SalesDate)
Values (1, 450, 2, 2, '2017-3-5 11:34'),
	(4, 850, 3, 2, '2017-3-6 13:12'),
	(5, 900, 3, 2, '2017-3-6 13:12'),
	(2, 350, 1, 3, '2017-3-2 11:34'),
	(3, 225, 4, 3, '2017-3-2 13:12'),
	(5, 900, 6, 3, '2017-3-3 13:12'),
	(1, 450, 10, 5, '2017-3-4 13:10'),
	(3, 225, 3, 5, '2017-3-4 13:12'),
	(5, 900, 1, 6, '2017-3-7 13:12')


Alter Table SalesTransaction
Add InvoiceId INT Foreign Key References Invoice(InvoiceId)
Alter Table Invoice
Alter column InvoiceNumber VARCHAR(40)

Update SalesTransaction Set Total = Price * Quantity where Total is null