-- Inventory Management System Database Schema
-- SQL Server Script

-- Create Database
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'InventoryManagementDB')
BEGIN
	CREATE DATABASE InventoryManagementDB;
END
GO

USE InventoryManagementDB;
GO

-- Create Users Table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Users')
BEGIN
	CREATE TABLE Users (
		UserID INT PRIMARY KEY IDENTITY(1,1),
		Username NVARCHAR(50) UNIQUE NOT NULL,
		Email NVARCHAR(100),
		PasswordHash NVARCHAR(255) NOT NULL,
		Role NVARCHAR(20) NOT NULL DEFAULT 'User', -- 'Admin' or 'User'
		CreatedDate DATETIME DEFAULT GETDATE(),
		IsActive BIT DEFAULT 1,
		LastLoginDate DATETIME NULL
	);
END
GO

-- Create Categories Table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Categories')
BEGIN
	CREATE TABLE Categories (
		CategoryID INT PRIMARY KEY IDENTITY(1,1),
		CategoryName NVARCHAR(100) NOT NULL UNIQUE,
		Description NVARCHAR(255),
		CreatedDate DATETIME DEFAULT GETDATE(),
		IsActive BIT DEFAULT 1
	);
END
GO

-- Create Items Table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Items')
BEGIN
	CREATE TABLE Items (
		ItemID INT PRIMARY KEY IDENTITY(1,1),
		ItemCode NVARCHAR(50) UNIQUE NOT NULL,
		ProductName NVARCHAR(150) NOT NULL,
		CategoryID INT NOT NULL,
		Description NVARCHAR(500),
		UnitPrice DECIMAL(18, 2) NOT NULL,
		StockQuantity INT NOT NULL DEFAULT 0,
		ReorderLevel INT NOT NULL DEFAULT 10,
		CreatedDate DATETIME DEFAULT GETDATE(),
		CreatedBy INT,
		ModifiedDate DATETIME,
		ModifiedBy INT,
		IsActive BIT DEFAULT 1,
		FOREIGN KEY (CategoryID) REFERENCES Categories(CategoryID),
		FOREIGN KEY (CreatedBy) REFERENCES Users(UserID),
		FOREIGN KEY (ModifiedBy) REFERENCES Users(UserID)
	);
END
GO

-- Create TransactionLogs Table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'TransactionLogs')
BEGIN
	CREATE TABLE TransactionLogs (
		LogID INT PRIMARY KEY IDENTITY(1,1),
		UserID INT NOT NULL,
		ActionType NVARCHAR(50) NOT NULL, -- 'ADD', 'UPDATE', 'DELETE', 'SEARCH'
		TableName NVARCHAR(50) NOT NULL, -- 'Items', 'Users', 'Categories'
		RecordID INT,
		OldValues NVARCHAR(MAX),
		NewValues NVARCHAR(MAX),
		ActionDate DATETIME DEFAULT GETDATE(),
		IPAddress NVARCHAR(50),
		Status NVARCHAR(20) DEFAULT 'Success', -- 'Success' or 'Failed'
		ErrorMessage NVARCHAR(MAX),
		FOREIGN KEY (UserID) REFERENCES Users(UserID)
	);
END
GO

-- Create Indexes for better performance
CREATE NONCLUSTERED INDEX IX_Items_CategoryID ON Items(CategoryID);
CREATE NONCLUSTERED INDEX IX_Items_ItemCode ON Items(ItemCode);
CREATE NONCLUSTERED INDEX IX_Users_Username ON Users(Username);
CREATE NONCLUSTERED INDEX IX_TransactionLogs_UserID ON TransactionLogs(UserID);
CREATE NONCLUSTERED INDEX IX_TransactionLogs_ActionDate ON TransactionLogs(ActionDate);
GO

-- Insert sample data
-- Insert default categories
INSERT INTO Categories (CategoryName, Description)
VALUES 
	('Electronics', 'Electronic devices and gadgets'),
	('Office Supplies', 'Items for office use'),
	('Furniture', 'Office and home furniture'),
	('Software', 'Software licenses and products'),
	('Hardware', 'Computer hardware components');
GO

-- Insert default admin user (Username: admin, Password: 123456)
INSERT INTO Users (Username, Email, PasswordHash, Role, IsActive)
VALUES ('admin', 'admin@inventory.com', 'fEqNCco3Yq9h5ZUglD3CZJT4lBs=', 'Admin', 1);
GO

-- Insert sample user (Username: user, Password: User@123)
INSERT INTO Users (Username, Email, PasswordHash, Role, IsActive)
VALUES ('user', 'user@inventory.com', 'O3YR5+Y/G8VVU9hZ6zZ5Q5Y+qI8=', 'User', 1);
GO

-- Insert sample items
INSERT INTO Items (ItemCode, ProductName, CategoryID, Description, UnitPrice, StockQuantity, ReorderLevel, CreatedBy)
VALUES 
	('IT001', 'Laptop Dell Inspiron', 1, 'Dell Inspiron 15 Laptop', 45000.00, 15, 5, 1),
	('IT002', 'Mouse Logitech', 1, 'Wireless Mouse', 1500.00, 50, 20, 1),
	('IT003', 'Office Chair', 3, 'Ergonomic office chair', 12000.00, 8, 3, 1),
	('IT004', 'Printer HP LaserJet', 1, 'HP LaserJet Printer', 25000.00, 5, 2, 1),
	('IT005', 'Paper Pack A4', 2, 'Box of 500 sheets A4 paper', 800.00, 100, 50, 1);
GO

-- Print success message
PRINT 'Database schema created successfully!';
PRINT 'Default credentials:';
PRINT 'Admin - Username: admin, Password: 123456';
PRINT 'User - Username: user, Password: User@123';
GO
