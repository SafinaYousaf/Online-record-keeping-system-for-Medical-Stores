USE [DB53]
GO
/****** Object:  Table [dbo].[MedicinePotency]    Script Date: 5/16/2019 3:43:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MedicinePotency](
	[MedPot_Id] [int] IDENTITY(1,1) NOT NULL,
	[Medicine_Id] [int] NOT NULL,
	[Potency_Id] [int] NOT NULL,
	[Price] [money] NOT NULL,
	[ExpiryDate] [date] NOT NULL,
	[NoOfItem] [int] NOT NULL,
 CONSTRAINT [PK_MedicinePotency] PRIMARY KEY CLUSTERED 
(
	[MedPot_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 5/16/2019 3:43:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[Customer_Id] [int] IDENTITY(1,1) NOT NULL,
	[Customer_Name] [nvarchar](50) NOT NULL,
	[Contact] [nvarchar](20) NOT NULL,
	[Address] [nchar](10) NOT NULL,
	[Added_On] [datetime] NOT NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[Customer_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Person]    Script Date: 5/16/2019 3:43:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Person](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](100) NOT NULL,
	[LastName] [varchar](100) NULL,
	[Contact] [varchar](20) NOT NULL,
	[Email] [varchar](30) NOT NULL,
	[Address] [nvarchar](100) NOT NULL,
	[Country] [nchar](10) NOT NULL,
	[DateOfBirth] [datetime] NULL,
	[Gender] [char](1) NOT NULL,
 CONSTRAINT [PK_Person] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Potency]    Script Date: 5/16/2019 3:43:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Potency](
	[Potency_Id] [int] IDENTITY(1,1) NOT NULL,
	[Potency_mg] [int] NOT NULL,
 CONSTRAINT [PK_Potency] PRIMARY KEY CLUSTERED 
(
	[Potency_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sales]    Script Date: 5/16/2019 3:43:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sales](
	[Sold_Id] [int] IDENTITY(1,1) NOT NULL,
	[NoOfItem] [int] NOT NULL,
	[Date] [datetime] NOT NULL,
	[Staff_Id] [int] NOT NULL,
	[Customer_Id] [int] NOT NULL,
	[MedPot_Id] [int] NOT NULL,
 CONSTRAINT [PK_Sold] PRIMARY KEY CLUSTERED 
(
	[Sold_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Medicine]    Script Date: 5/16/2019 3:43:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Medicine](
	[Medicine_Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](30) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[ExpiryDate] [date] NOT NULL,
	[CategoryID] [int] NOT NULL,
	[SupplierID] [int] NOT NULL,
 CONSTRAINT [PK_Medicine] PRIMARY KEY CLUSTERED 
(
	[Medicine_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[DailySales]    Script Date: 5/16/2019 3:43:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[DailySales]
AS
SELECT        dbo.Person.FirstName + ' ' + dbo.Person.LastName AS Staff, dbo.Customer.Customer_Name AS Customer, dbo.Medicine.Name AS Medicine, dbo.Potency.Potency_mg, dbo.Sales.NoOfItem AS Quantity, 
                         dbo.Sales.Date AS SoldON
FROM            dbo.Medicine INNER JOIN
                         dbo.MedicinePotency AS MP ON dbo.Medicine.Medicine_Id = MP.Medicine_Id INNER JOIN
                         dbo.Potency ON MP.Potency_Id = dbo.Potency.Potency_Id INNER JOIN
                         dbo.Sales ON MP.MedPot_Id = dbo.Sales.MedPot_Id INNER JOIN
                         dbo.Customer ON dbo.Sales.Customer_Id = dbo.Customer.Customer_Id INNER JOIN
                         dbo.Person ON dbo.Person.Id = dbo.Sales.Staff_Id
WHERE        (dbo.Sales.Date = CONVERT(VARCHAR(10), getdate(), 111))
GO
/****** Object:  View [dbo].[Monthlysales]    Script Date: 5/16/2019 3:43:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[Monthlysales]
AS
SELECT        dbo.Person.FirstName + ' ' + dbo.Person.LastName AS Staff, dbo.Customer.Customer_Name AS Customer, dbo.Medicine.Name AS Medicine, dbo.Potency.Potency_mg, dbo.Sales.NoOfItem AS Quantity, 
                         dbo.Sales.Date AS SoldON
FROM            dbo.Medicine INNER JOIN
                         dbo.MedicinePotency AS MP ON dbo.Medicine.Medicine_Id = MP.Medicine_Id INNER JOIN
                         dbo.Potency ON MP.Potency_Id = dbo.Potency.Potency_Id INNER JOIN
                         dbo.Sales ON MP.MedPot_Id = dbo.Sales.MedPot_Id INNER JOIN
                         dbo.Customer ON dbo.Sales.Customer_Id = dbo.Customer.Customer_Id INNER JOIN
                         dbo.Person ON dbo.Person.Id = dbo.Sales.Staff_Id
WHERE        month([Date])  = MONTH(GETDATE())
GO
/****** Object:  View [dbo].[NoOfItemsInstock]    Script Date: 5/16/2019 3:43:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[NoOfItemsInstock]
AS
SELECT        Medicine.[Name] AS Medicine, dbo.Potency.Potency_mg, MP.NoOfItem
FROM         Medicine JOIN
             MedicinePotency AS MP ON Medicine.Medicine_Id = MP.Medicine_Id JOIN
             Potency ON MP.Potency_Id = Potency.Potency_Id 
GO
/****** Object:  Table [dbo].[Suppliers]    Script Date: 5/16/2019 3:43:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Suppliers](
	[SupplierID] [int] IDENTITY(1,1) NOT NULL,
	[CompanyName] [nvarchar](50) NOT NULL,
	[ContactName] [nvarchar](50) NULL,
	[ContactTitle] [nvarchar](50) NULL,
	[Address] [nvarchar](60) NULL,
	[City] [nvarchar](30) NULL,
	[Country] [nvarchar](30) NULL,
	[Fax] [nchar](10) NULL,
 CONSTRAINT [PK_Suppliers] PRIMARY KEY CLUSTERED 
(
	[SupplierID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[MedicineBySupplier]    Script Date: 5/16/2019 3:43:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[MedicineBySupplier]
AS
select  Medicine.[Name] as "Medicine" , Suppliers.CompanyName as "Company" , Suppliers.Fax as "Fax" from Medicine 
join Suppliers on Medicine.SupplierID = Suppliers.SupplierID 
GO
/****** Object:  View [dbo].[AllSales]    Script Date: 5/16/2019 3:43:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[AllSales]
AS 
Select Person.FirstName+' '+Person.LastName AS "Staff",Customer.Customer_Name AS "Customer",[Name] AS "Medicine",Potency_mg,Sales.NoOfItem AS "Quantity",Sales.[Date] AS "SoldON" from Medicine JOIN MedicinePotency AS MP ON Medicine.Medicine_Id = MP.Medicine_Id
JOIN Potency ON MP.Potency_Id = Potency.Potency_Id
JOIN Sales ON  MP.MedPot_Id = Sales.MedPot_Id
JOIN Customer ON Sales.Customer_Id = Customer.Customer_Id
JOIN  Person ON Person.ID = Sales.Staff_Id
GO
/****** Object:  Table [dbo].[Staff]    Script Date: 5/16/2019 3:43:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Staff](
	[Staff_Id] [int] NOT NULL,
	[Salary] [money] NOT NULL,
	[Designation] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Staff] PRIMARY KEY CLUSTERED 
(
	[Staff_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[StaffInfo]    Script Date: 5/16/2019 3:43:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[StaffInfo]
AS
SELECT Person.FirstName+' '+Person.LastName AS "Name",Designation,Salary FROM Staff 
JOIN Person ON Person.Id = Staff.Staff_Id 
GO
/****** Object:  View [dbo].[StaffCount]    Script Date: 5/16/2019 3:43:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[StaffCount]
AS
SELECT Designation, Count(Staff_Id) AS "Number Of Person"FROM Staff 
GROUP BY Designation 
GO
/****** Object:  View [dbo].[ExpiredMedicine]    Script Date: 5/16/2019 3:43:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[ExpiredMedicine]
AS
SELECT Medicine.Name as "Medicine Name" , Potency.Potency_mg as "Potency in mg", Medicine.ExpiryDate FROM Medicine
JOIN MedicinePotency AS MP ON Medicine.Medicine_Id = MP.Medicine_Id
JOIN Potency ON MP.Potency_Id = Potency.Potency_Id
WHERE MP.ExpiryDate < GETDATE();
GO
/****** Object:  View [dbo].[MedicineInfo]    Script Date: 5/16/2019 3:43:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[MedicineInfo]
AS
SELECT Medicine.Name as "Medicine Name" , Potency.Potency_mg as "Potency in mg", MP.NoOfItem FROM Medicine
JOIN MedicinePotency AS MP ON Medicine.Medicine_Id = MP.Medicine_Id
JOIN Potency ON MP.Potency_Id = Potency.Potency_Id
GO
/****** Object:  View [dbo].[GenderDiscrimination]    Script Date: 5/16/2019 3:43:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[GenderDiscrimination]
AS
SELECT Gender, COUNT(Id) AS "Number Of Person", AVG(Staff.Salary) AS "Average Salary" FROM Person
JOIN Staff ON Person.Id = Staff.Staff_Id GROUP BY Gender
GO
/****** Object:  View [dbo].[ItemSoldByStaff]    Script Date: 5/16/2019 3:43:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ItemSoldByStaff]
AS
Select Person.FirstName+' '+Person.LastName AS "Staff",[Name] AS "Medicine",Potency_mg,SUM(Sales.NoOfItem) AS "Total Sold Items" FROM Medicine 
JOIN MedicinePotency AS MP ON Medicine.Medicine_Id = MP.Medicine_Id
JOIN Potency ON MP.Potency_Id = Potency.Potency_Id
JOIN Sales ON  MP.MedPot_Id = Sales.MedPot_Id
JOIN  Person ON Person.ID = Sales.Staff_Id 
GROUP BY Person.FirstName+' '+Person.LastName, Medicine.[Name], Potency.Potency_mg
GO
/****** Object:  View [dbo].[SupplierInfo]    Script Date: 5/16/2019 3:43:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[SupplierInfo]
AS
SELECT CompanyName AS "Company", ContactName AS "Name", Fax,City,Country FROM Suppliers
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 5/16/2019 3:43:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[Category_Id] [int] IDENTITY(1,1) NOT NULL,
	[Category_Name] [nvarchar](50) NOT NULL,
	[Discription] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[Category_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Login]    Script Date: 5/16/2019 3:43:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Login](
	[Login_Id] [int] NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
	[Discriminator] [nchar](20) NOT NULL,
 CONSTRAINT [PK_Login] PRIMARY KEY CLUSTERED 
(
	[Login_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Categories] ON 

INSERT [dbo].[Categories] ([Category_Id], [Category_Name], [Discription]) VALUES (10, N'Antipyretics', N'reducing fever (pyrexia/pyresis)')
INSERT [dbo].[Categories] ([Category_Id], [Category_Name], [Discription]) VALUES (11, N'Analgesics', N'reducing pain (painkillers)')
INSERT [dbo].[Categories] ([Category_Id], [Category_Name], [Discription]) VALUES (12, N'Antimalarial drugs', N'treating malaria.')
INSERT [dbo].[Categories] ([Category_Id], [Category_Name], [Discription]) VALUES (13, N'Antibiotics', N'Inhibiting germ growth')
INSERT [dbo].[Categories] ([Category_Id], [Category_Name], [Discription]) VALUES (14, N'Antiseptics', N'Prevention of germ growth near burns, cuts and woundsk')
INSERT [dbo].[Categories] ([Category_Id], [Category_Name], [Discription]) VALUES (16, N'Mood stabilizers', N'Lithium and valpromide.')
INSERT [dbo].[Categories] ([Category_Id], [Category_Name], [Discription]) VALUES (20, N'Gestrointestinak', N'Related to Stomach and Intestine')
SET IDENTITY_INSERT [dbo].[Categories] OFF
SET IDENTITY_INSERT [dbo].[Customer] ON 

INSERT [dbo].[Customer] ([Customer_Id], [Customer_Name], [Contact], [Address], [Added_On]) VALUES (1, N'Sana', N'03001234567', N'47 Tail   ', CAST(N'2019-02-02T00:00:00.000' AS DateTime))
INSERT [dbo].[Customer] ([Customer_Id], [Customer_Name], [Contact], [Address], [Added_On]) VALUES (2, N'Savira', N'03001234567', N'Lahore    ', CAST(N'2019-02-02T00:00:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[Customer] OFF
INSERT [dbo].[Login] ([Login_Id], [Email], [Password], [Discriminator]) VALUES (11, N'beingsafina@gmail.com', N'12345678', N'Admin               ')
INSERT [dbo].[Login] ([Login_Id], [Email], [Password], [Discriminator]) VALUES (13, N'hina@gmail.com', N'12345678', N'Staff               ')
INSERT [dbo].[Login] ([Login_Id], [Email], [Password], [Discriminator]) VALUES (14, N'aliza@gmail.com', N'12345678', N'Staff               ')
INSERT [dbo].[Login] ([Login_Id], [Email], [Password], [Discriminator]) VALUES (15, N'anmol@gmail.com', N'12345678', N'Staff               ')
INSERT [dbo].[Login] ([Login_Id], [Email], [Password], [Discriminator]) VALUES (16, N'rida@gmail.com', N'12345678', N'Staff               ')
SET IDENTITY_INSERT [dbo].[Medicine] ON 

INSERT [dbo].[Medicine] ([Medicine_Id], [Name], [Description], [ExpiryDate], [CategoryID], [SupplierID]) VALUES (6, N'Paracetamol', N'Used for mild to moderate pain', CAST(N'2019-09-09' AS Date), 10, 1)
INSERT [dbo].[Medicine] ([Medicine_Id], [Name], [Description], [ExpiryDate], [CategoryID], [SupplierID]) VALUES (7, N'Ibuprefen', N'Used for fever and pain', CAST(N'2019-01-09' AS Date), 10, 1)
INSERT [dbo].[Medicine] ([Medicine_Id], [Name], [Description], [ExpiryDate], [CategoryID], [SupplierID]) VALUES (8, N'Amoxicillin', N'Used for middle ear infection, penumonia ,skin infection.', CAST(N'2019-09-09' AS Date), 13, 1)
INSERT [dbo].[Medicine] ([Medicine_Id], [Name], [Description], [ExpiryDate], [CategoryID], [SupplierID]) VALUES (9, N'Cipro', N'Used for bone and Joint infection, diarea', CAST(N'2019-09-09' AS Date), 13, 3)
INSERT [dbo].[Medicine] ([Medicine_Id], [Name], [Description], [ExpiryDate], [CategoryID], [SupplierID]) VALUES (10, N'Tagmet', N'used for peptic ulser', CAST(N'2019-09-09' AS Date), 10, 4)
INSERT [dbo].[Medicine] ([Medicine_Id], [Name], [Description], [ExpiryDate], [CategoryID], [SupplierID]) VALUES (11, N'Betadine', N'Used for skin disinfection ', CAST(N'2019-09-09' AS Date), 10, 4)
INSERT [dbo].[Medicine] ([Medicine_Id], [Name], [Description], [ExpiryDate], [CategoryID], [SupplierID]) VALUES (12, N'Asprine', N'Used for pain ,fever , inflammation etc..', CAST(N'2019-02-09' AS Date), 10, 2)
SET IDENTITY_INSERT [dbo].[Medicine] OFF
SET IDENTITY_INSERT [dbo].[MedicinePotency] ON 

INSERT [dbo].[MedicinePotency] ([MedPot_Id], [Medicine_Id], [Potency_Id], [Price], [ExpiryDate], [NoOfItem]) VALUES (9, 6, 1, 500.0000, CAST(N'2019-09-09' AS Date), 9992)
INSERT [dbo].[MedicinePotency] ([MedPot_Id], [Medicine_Id], [Potency_Id], [Price], [ExpiryDate], [NoOfItem]) VALUES (10, 7, 5, 200.0000, CAST(N'2019-09-09' AS Date), 99994)
INSERT [dbo].[MedicinePotency] ([MedPot_Id], [Medicine_Id], [Potency_Id], [Price], [ExpiryDate], [NoOfItem]) VALUES (11, 10, 6, 5000.0000, CAST(N'2019-09-09' AS Date), 100000)
INSERT [dbo].[MedicinePotency] ([MedPot_Id], [Medicine_Id], [Potency_Id], [Price], [ExpiryDate], [NoOfItem]) VALUES (12, 7, 6, 400.0000, CAST(N'2019-09-09' AS Date), 10000)
INSERT [dbo].[MedicinePotency] ([MedPot_Id], [Medicine_Id], [Potency_Id], [Price], [ExpiryDate], [NoOfItem]) VALUES (13, 11, 7, 800.0000, CAST(N'2019-09-09' AS Date), 10000)
SET IDENTITY_INSERT [dbo].[MedicinePotency] OFF
SET IDENTITY_INSERT [dbo].[Person] ON 

INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Contact], [Email], [Address], [Country], [DateOfBirth], [Gender]) VALUES (11, N'Safina', N'Yousaf', N'03001234562', N'beingsafina@gmail.com', N'Javaid colony 49 Tail sargodha', N'Pakistan  ', CAST(N'1998-04-11T00:00:00.000' AS DateTime), N'F')
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Contact], [Email], [Address], [Country], [DateOfBirth], [Gender]) VALUES (13, N'Hina', N'Maqsood', N'03001234560', N'hina@gmail.com', N'Laal Pull Miayn Colony Lahore', N'Pakistan  ', CAST(N'1998-07-03T00:00:00.000' AS DateTime), N'F')
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Contact], [Email], [Address], [Country], [DateOfBirth], [Gender]) VALUES (14, N'Aliza', N'Farrukh', N'03001234564', N'aliza@gmail.com', N'Harbans Pura Lahore', N'Pakistan  ', NULL, N'F')
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Contact], [Email], [Address], [Country], [DateOfBirth], [Gender]) VALUES (15, N'Anmol', N'Butt', N'03001234563', N'anmol@gmail.com', N'Harbans Pura Lahore', N'Pakistan  ', NULL, N'F')
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Contact], [Email], [Address], [Country], [DateOfBirth], [Gender]) VALUES (16, N'Rida', N'Mehmood', N'03001234511', N'rida@gmail.com', N'Model Town Lahore', N'Pakistan  ', CAST(N'2000-02-02T00:00:00.000' AS DateTime), N'F')
SET IDENTITY_INSERT [dbo].[Person] OFF
SET IDENTITY_INSERT [dbo].[Potency] ON 

INSERT [dbo].[Potency] ([Potency_Id], [Potency_mg]) VALUES (1, 15)
INSERT [dbo].[Potency] ([Potency_Id], [Potency_mg]) VALUES (5, 250)
INSERT [dbo].[Potency] ([Potency_Id], [Potency_mg]) VALUES (6, 500)
INSERT [dbo].[Potency] ([Potency_Id], [Potency_mg]) VALUES (7, 1000)
SET IDENTITY_INSERT [dbo].[Potency] OFF
SET IDENTITY_INSERT [dbo].[Sales] ON 

INSERT [dbo].[Sales] ([Sold_Id], [NoOfItem], [Date], [Staff_Id], [Customer_Id], [MedPot_Id]) VALUES (12, 2, CAST(N'2019-05-08T00:00:00.000' AS DateTime), 13, 1, 9)
INSERT [dbo].[Sales] ([Sold_Id], [NoOfItem], [Date], [Staff_Id], [Customer_Id], [MedPot_Id]) VALUES (13, 6, CAST(N'2019-09-09T00:00:00.000' AS DateTime), 11, 2, 10)
INSERT [dbo].[Sales] ([Sold_Id], [NoOfItem], [Date], [Staff_Id], [Customer_Id], [MedPot_Id]) VALUES (14, 2, CAST(N'2019-09-09T00:00:00.000' AS DateTime), 13, 1, 9)
SET IDENTITY_INSERT [dbo].[Sales] OFF
INSERT [dbo].[Staff] ([Staff_Id], [Salary], [Designation]) VALUES (11, 500000.0000, N'Manager')
INSERT [dbo].[Staff] ([Staff_Id], [Salary], [Designation]) VALUES (13, 30000.0000, N'Cashier')
INSERT [dbo].[Staff] ([Staff_Id], [Salary], [Designation]) VALUES (14, 40000.0000, N'Sales Girl')
INSERT [dbo].[Staff] ([Staff_Id], [Salary], [Designation]) VALUES (15, 35000.0000, N'pharmacist')
INSERT [dbo].[Staff] ([Staff_Id], [Salary], [Designation]) VALUES (16, 30000.0000, N'Sales Girl')
SET IDENTITY_INSERT [dbo].[Suppliers] ON 

INSERT [dbo].[Suppliers] ([SupplierID], [CompanyName], [ContactName], [ContactTitle], [Address], [City], [Country], [Fax]) VALUES (1, N'Roche', N'William Roon', N'Purchasing Manager', N'49 Gillbert st.', N'New York', N'USA', N'998896784 ')
INSERT [dbo].[Suppliers] ([SupplierID], [CompanyName], [ContactName], [ContactTitle], [Address], [City], [Country], [Fax]) VALUES (2, N'Pfizer', N'Jhon Robert', N'Order Administration', N'707 Oxford Road', N'Melbourne', N'Australia', N'568435678 ')
INSERT [dbo].[Suppliers] ([SupplierID], [CompanyName], [ContactName], [ContactTitle], [Address], [City], [Country], [Fax]) VALUES (3, N'GSK (GlaxoSmithKline)', N'Yoshi Nagase', N'Marketing Representative', N'9-8 skemai mushiro-al', N'Tokyo', N'Japan', N'987653246 ')
INSERT [dbo].[Suppliers] ([SupplierID], [CompanyName], [ContactName], [ContactTitle], [Address], [City], [Country], [Fax]) VALUES (4, N'AstraZeneca.', N'Willam Jain', N'Marketing Manager', N'29 King''s way', N'Manchester', N'UK', N'0985467326')
SET IDENTITY_INSERT [dbo].[Suppliers] OFF
ALTER TABLE [dbo].[Login]  WITH CHECK ADD  CONSTRAINT [FK_Login_Person] FOREIGN KEY([Login_Id])
REFERENCES [dbo].[Person] ([Id])
GO
ALTER TABLE [dbo].[Login] CHECK CONSTRAINT [FK_Login_Person]
GO
ALTER TABLE [dbo].[Medicine]  WITH CHECK ADD  CONSTRAINT [FK_Medicine_Category] FOREIGN KEY([CategoryID])
REFERENCES [dbo].[Categories] ([Category_Id])
GO
ALTER TABLE [dbo].[Medicine] CHECK CONSTRAINT [FK_Medicine_Category]
GO
ALTER TABLE [dbo].[Medicine]  WITH CHECK ADD  CONSTRAINT [FK_Medicine_Suppliers] FOREIGN KEY([SupplierID])
REFERENCES [dbo].[Suppliers] ([SupplierID])
GO
ALTER TABLE [dbo].[Medicine] CHECK CONSTRAINT [FK_Medicine_Suppliers]
GO
ALTER TABLE [dbo].[MedicinePotency]  WITH CHECK ADD  CONSTRAINT [FK_MedicinePotency_Medicine] FOREIGN KEY([Medicine_Id])
REFERENCES [dbo].[Medicine] ([Medicine_Id])
GO
ALTER TABLE [dbo].[MedicinePotency] CHECK CONSTRAINT [FK_MedicinePotency_Medicine]
GO
ALTER TABLE [dbo].[MedicinePotency]  WITH CHECK ADD  CONSTRAINT [FK_MedicinePotency_Potency] FOREIGN KEY([Potency_Id])
REFERENCES [dbo].[Potency] ([Potency_Id])
GO
ALTER TABLE [dbo].[MedicinePotency] CHECK CONSTRAINT [FK_MedicinePotency_Potency]
GO
ALTER TABLE [dbo].[Sales]  WITH CHECK ADD  CONSTRAINT [FK_Sales_Customer] FOREIGN KEY([Customer_Id])
REFERENCES [dbo].[Customer] ([Customer_Id])
GO
ALTER TABLE [dbo].[Sales] CHECK CONSTRAINT [FK_Sales_Customer]
GO
ALTER TABLE [dbo].[Sales]  WITH CHECK ADD  CONSTRAINT [FK_Sales_MedicinePotency] FOREIGN KEY([MedPot_Id])
REFERENCES [dbo].[MedicinePotency] ([MedPot_Id])
GO
ALTER TABLE [dbo].[Sales] CHECK CONSTRAINT [FK_Sales_MedicinePotency]
GO
ALTER TABLE [dbo].[Sales]  WITH CHECK ADD  CONSTRAINT [FK_Sold_Staff] FOREIGN KEY([Staff_Id])
REFERENCES [dbo].[Staff] ([Staff_Id])
GO
ALTER TABLE [dbo].[Sales] CHECK CONSTRAINT [FK_Sold_Staff]
GO
ALTER TABLE [dbo].[Staff]  WITH CHECK ADD  CONSTRAINT [FK_Staff_Person] FOREIGN KEY([Staff_Id])
REFERENCES [dbo].[Person] ([Id])
GO
ALTER TABLE [dbo].[Staff] CHECK CONSTRAINT [FK_Staff_Person]
GO
ALTER TABLE [dbo].[Person]  WITH CHECK ADD  CONSTRAINT [CHK_Gen] CHECK  (([Gender]='F' OR [Gender]='M'))
GO
ALTER TABLE [dbo].[Person] CHECK CONSTRAINT [CHK_Gen]
GO
