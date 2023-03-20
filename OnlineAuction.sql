CREATE DATABASE OnlineAuction
COLLATE Cyrillic_General_CI_AS
GO

CREATE TABLE Users
([ID] [int] NOT NULL IDENTITY (1,1),
[Name] [nvarchar] (50) NOT NULL,
[SurName] [nvarchar] (50) NOT NULL,
[Birthday] [DateTime] NOT NULL,
[PhoneNumber] [nvarchar] (50) NOT NULL,
[Email] [nvarchar] (50) NOT NULL,
[Password] [nvarchar] (50) NOT NULL,
)
GO

CREATE TABLE Lots
([ID] [int] NOT NULL IDENTITY (1,1),
[Name] [nvarchar] (50) NOT NULL,
[Description] [nvarchar] (100) NOT NULL,
[Author] [nvarchar] (100) NOT NULL,
[Genre] [nvarchar] (100) NOT NULL,
[Price] [int] NOT NULL,
[Seller] [int] NOT NULL,
[Customer] [int],
)
GO

ALTER TABLE Users
ADD 
PRIMARY KEY (ID)
go

ALTER TABLE Lots
ADD 
PRIMARY KEY (ID),
FOREIGN KEY (Seller) REFERENCES Users (ID),
FOREIGN KEY (Customer) REFERENCES Users (ID)
go

CREATE PROCEDURE AddUser
@ID int OUTPUT,
@Name varchar(50),
@SurName varchar(50),
@Birthday DateTime,
@PhoneNumber varchar(50),
@Email varchar (50),
@Password varchar (50)
AS
INSERT INTO Users ([Name], [SurName], [Birthday], [PhoneNumber], [Email], [Password])
values (@Name, @SurName, @Birthday, @PhoneNumber, @Email, @Password) 
SELECT CAST(scope_identity() AS INT) AS ID
GO

CREATE PROCEDURE AddLot
@ID int OUTPUT,
@Name varchar(50),
@Author varchar(100),
@Genre varchar (100),
@Description varchar(100),
@Price int,
@Seller int
AS
INSERT INTO Lots([Name], [Price], [Author], [Genre], [Description], [Seller])
values (@Name, @Price, @Author, @Genre, @Description, @Seller)
SELECT CAST(scope_identity() AS INT) AS ID
GO

CREATE PROCEDURE BuyLot
@UserID int,
@LotID int
AS
update Lots set Customer = @UserID where ID = @LotID
GO

CREATE PROCEDURE GetListUsers
AS
SELECT ID, Name, SurName, Birthday, PhoneNumber, Email, Password from Users
GO

CREATE PROCEDURE GetListLots
AS
SELECT ID, Name, Price, Author, Genre, Description, Seller, Customer from Lots
GO

CREATE PROCEDURE GetBoughtLots
@UserID int
AS
SELECT ID, Name, Price, Author, Genre, Description, Seller, Customer from Lots
WHERE Customer=@UserID
GO

CREATE PROCEDURE GetSellLots
@UserID int
AS
SELECT ID, Name, Price, Author, Genre, Description, Seller, Customer from Lots
WHERE Seller=@UserID
GO

CREATE PROCEDURE DeleteUser
@UserID int
AS
delete from Users where ID = @UserID
delete from Lots where Seller = @UserID
delete from Lots where Customer = @UserID
go

CREATE PROCEDURE DeleteLot
@LotID int
AS
delete from Lots where ID = @LotID
go

CREATE PROCEDURE GetUserByID
@UserID int
AS
SELECT ID, Name, SurName, Birthday, PhoneNumber, Email, Password from Users 
where ID = @UserID
go 

CREATE PROCEDURE GetLotByID
@LotID int
AS
SELECT ID, Name, Price, Author, Genre, Description, Seller, Customer from Lots
where ID = @LotID
go

CREATE PROCEDURE GetNoUserLotForSellByID
@UserID int
AS
SELECT ID, Name, Price, Author, Genre, Description, Seller, Customer from Lots
WHERE Seller <> @UserID 
GO

CREATE PROCEDURE GetNoUserLotForSellByIDAndGenre
@UserID int,
@Genre varchar (100)
AS
SELECT ID, Name, Price, Author, Genre, Description, Seller, Customer from Lots
WHERE Seller<>@UserID AND Genre = @Genre 
GO

CREATE PROCEDURE GetAllLotsByGenre
@Genre varchar (100)
AS
SELECT ID, Name, Price, Author, Genre, Description, Seller, Customer from Lots
WHERE Genre = @Genre 
GO