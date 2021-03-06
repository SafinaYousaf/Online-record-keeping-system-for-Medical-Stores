USE [DB53]
GO
SET IDENTITY_INSERT [dbo].[Suppliers] ON 

INSERT [dbo].[Suppliers] ([SupplierID], [CompanyName], [ContactName], [ContactTitle], [Address], [City], [Country], [Fax]) VALUES (1, N'Convex', N'Lala', N'Lili', N'jhug', N'LHR', N'Pak', N'99889     ')
SET IDENTITY_INSERT [dbo].[Suppliers] OFF
SET IDENTITY_INSERT [dbo].[Categories] ON 

INSERT [dbo].[Categories] ([Category_Id], [Category_Name], [Discription]) VALUES (3, N'Pain Killer', N'relieve from pain')
INSERT [dbo].[Categories] ([Category_Id], [Category_Name], [Discription]) VALUES (4, N'Antibiotic', N'relieve from Allergy')
INSERT [dbo].[Categories] ([Category_Id], [Category_Name], [Discription]) VALUES (5, N'Mood stabilizers', N'Relieve From anxiety')
INSERT [dbo].[Categories] ([Category_Id], [Category_Name], [Discription]) VALUES (6, N'Antipyretics', N'reducing fever ')
INSERT [dbo].[Categories] ([Category_Id], [Category_Name], [Discription]) VALUES (7, N'Antimalarial drugs', N'Treating malaria')
INSERT [dbo].[Categories] ([Category_Id], [Category_Name], [Discription]) VALUES (8, N'Antiseptics', N'prevention of germ growth near burns, cuts and wounds.')
INSERT [dbo].[Categories] ([Category_Id], [Category_Name], [Discription]) VALUES (9, N'Analgesics', N' reducing pain (painkillers)')
SET IDENTITY_INSERT [dbo].[Categories] OFF
SET IDENTITY_INSERT [dbo].[Medicine] ON 

INSERT [dbo].[Medicine] ([Medicine_Id], [Name], [Description], [ExpiryDate], [CategoryID], [SupplierID]) VALUES (1, N'Paracetamol', N'Painkiller', CAST(N'2020-03-03' AS Date), 3, 1)
INSERT [dbo].[Medicine] ([Medicine_Id], [Name], [Description], [ExpiryDate], [CategoryID], [SupplierID]) VALUES (2, N'Codeine', N'Strongest Painkiller', CAST(N'2019-09-09' AS Date), 3, 1)
SET IDENTITY_INSERT [dbo].[Medicine] OFF
SET IDENTITY_INSERT [dbo].[Person] ON 

INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Contact], [Email], [Address], [Country], [DateOfBirth], [Gender]) VALUES (5, N'Ayaz', N'Khan', N'03001234563', N'ayaz_khan777@hotmail.com', N'tyuy', N'Pakistan  ', CAST(N'2000-02-02T00:00:00.000' AS DateTime), N'M')
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Contact], [Email], [Address], [Country], [DateOfBirth], [Gender]) VALUES (6, N'Safina', N'Yousaf', N'03001234563', N'beingsafina@gmail.com', N'ertu 78', N'Pakistan  ', CAST(N'2000-02-02T00:00:00.000' AS DateTime), N'F')
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Contact], [Email], [Address], [Country], [DateOfBirth], [Gender]) VALUES (7, N'Aliza', N'Farrukh', N'03001234566', N'aliza@gmail.com', N'hywgdg', N'Pakistan  ', CAST(N'2000-02-02T00:00:00.000' AS DateTime), N'F')
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Contact], [Email], [Address], [Country], [DateOfBirth], [Gender]) VALUES (8, N'Sana', N'Safina', N'03001234569', N'alina@gmail.com', N'hywgdg', N'Pakistan  ', CAST(N'2000-02-02T00:00:00.000' AS DateTime), N'F')
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Contact], [Email], [Address], [Country], [DateOfBirth], [Gender]) VALUES (9, N'Saleena', N'Gomez', N'03001234511', N'saleena@gmail.com', N'hywgdg', N'Pakistan  ', CAST(N'2000-02-02T00:00:00.000' AS DateTime), N'F')
SET IDENTITY_INSERT [dbo].[Person] OFF
INSERT [dbo].[Staff] ([Staff_Id], [Salary], [Designation]) VALUES (5, 30000000.0000, N'Manager')
INSERT [dbo].[Staff] ([Staff_Id], [Salary], [Designation]) VALUES (6, 30000000.0000, N'Manager')
INSERT [dbo].[Staff] ([Staff_Id], [Salary], [Designation]) VALUES (9, 30000000.0000, N'Sales Girl')
INSERT [dbo].[Login] ([Login_Id], [Email], [Password], [Discriminator]) VALUES (5, N'ayaz_khan777@hotmail.com', N'87654321', N'Staff               ')
INSERT [dbo].[Login] ([Login_Id], [Email], [Password], [Discriminator]) VALUES (6, N'beingsafina@gmail.com', N'12345678', N'Customer            ')
INSERT [dbo].[Login] ([Login_Id], [Email], [Password], [Discriminator]) VALUES (7, N'aliza@gmail.com', N'12345678', N'Staff               ')
INSERT [dbo].[Login] ([Login_Id], [Email], [Password], [Discriminator]) VALUES (8, N'alina@gmail.com', N'12345678', N'Staff               ')
INSERT [dbo].[Login] ([Login_Id], [Email], [Password], [Discriminator]) VALUES (9, N'saleena@gmail.com', N'12345678', N'Admin               ')
SET IDENTITY_INSERT [dbo].[Customer] ON 

INSERT [dbo].[Customer] ([Customer_Id], [Customer_Name], [Contact], [Address], [Added_On]) VALUES (5, N'Safina', 0, N'ertu 78   ', CAST(N'2019-09-09T00:00:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[Customer] OFF
SET IDENTITY_INSERT [dbo].[Potency] ON 

INSERT [dbo].[Potency] ([Potency_Id], [Potency_mg]) VALUES (1, 15)
INSERT [dbo].[Potency] ([Potency_Id], [Potency_mg]) VALUES (5, 67)
SET IDENTITY_INSERT [dbo].[Potency] OFF
SET IDENTITY_INSERT [dbo].[MedicinePotency] ON 

INSERT [dbo].[MedicinePotency] ([MedPot_Id], [Medicine_Id], [Potency_Id], [Price], [ExpiryDate], [NoOfItem]) VALUES (8, 1, 1, 500.0000, CAST(N'2019-09-09' AS Date), 4)
SET IDENTITY_INSERT [dbo].[MedicinePotency] OFF
SET IDENTITY_INSERT [dbo].[Sales] ON 

INSERT [dbo].[Sales] ([Sold_Id], [NoOfItem], [Date], [Staff_Id], [Customer_Id], [MedPot_Id]) VALUES (4, 2, CAST(N'2019-09-09T00:00:00.000' AS DateTime), 5, 5, 8)
SET IDENTITY_INSERT [dbo].[Sales] OFF
