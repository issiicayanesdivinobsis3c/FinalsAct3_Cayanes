-- Inventory Management System - MS Access Database Schema
-- This file contains the SQL DDL for creating tables in MS Access

-- Users Table
CREATE TABLE Users (
	UserID AUTOINCREMENT PRIMARY KEY,
	Username TEXT(50) UNIQUE NOT NULL,
	Email TEXT(100),
	PasswordHash TEXT(255) NOT NULL,
	Role TEXT(20) NOT NULL DEFAULT 'User',
	CreatedDate DATETIME DEFAULT Now(),
	IsActive YESNO DEFAULT True,
	LastLoginDate DATETIME
);

-- Categories Table
CREATE TABLE Categories (
	CategoryID AUTOINCREMENT PRIMARY KEY,
	CategoryName TEXT(100) UNIQUE NOT NULL,
	Description TEXT(255),
	CreatedDate DATETIME DEFAULT Now(),
	IsActive YESNO DEFAULT True
);

-- Items Table
CREATE TABLE Items (
	ItemID AUTOINCREMENT PRIMARY KEY,
	ItemCode TEXT(50) UNIQUE NOT NULL,
	ProductName TEXT(150) NOT NULL,
	CategoryID LONG NOT NULL,
	Description TEXT(500),
	UnitPrice CURRENCY NOT NULL,
	StockQuantity LONG NOT NULL DEFAULT 0,
	ReorderLevel LONG NOT NULL DEFAULT 10,
	CreatedDate DATETIME DEFAULT Now(),
	CreatedBy LONG,
	ModifiedDate DATETIME,
	ModifiedBy LONG,
	IsActive YESNO DEFAULT True,
	FOREIGN KEY (CategoryID) REFERENCES Categories(CategoryID),
	FOREIGN KEY (CreatedBy) REFERENCES Users(UserID),
	FOREIGN KEY (ModifiedBy) REFERENCES Users(UserID)
);

-- TransactionLogs Table
CREATE TABLE TransactionLogs (
	LogID AUTOINCREMENT PRIMARY KEY,
	UserID LONG NOT NULL,
	ActionType TEXT(50) NOT NULL,
	TableName TEXT(50) NOT NULL,
	RecordID LONG,
	OldValues MEMO,
	NewValues MEMO,
	ActionDate DATETIME DEFAULT Now(),
	IPAddress TEXT(50),
	Status TEXT(20) DEFAULT 'Success',
	ErrorMessage MEMO,
	FOREIGN KEY (UserID) REFERENCES Users(UserID)
);

-- Create Indexes for better performance
CREATE INDEX IX_Items_CategoryID ON Items(CategoryID);
CREATE INDEX IX_Items_ItemCode ON Items(ItemCode);
CREATE INDEX IX_Users_Username ON Users(Username);
CREATE INDEX IX_TransactionLogs_UserID ON TransactionLogs(UserID);
CREATE INDEX IX_TransactionLogs_ActionDate ON TransactionLogs(ActionDate);
