-- Currency Exchange Office Database Schema

-- Users table
CREATE TABLE Users (
    UserId INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(50) NOT NULL UNIQUE,
    Email NVARCHAR(100) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(255) NOT NULL,
    CreatedAt DATETIME DEFAULT GETDATE()
);

-- Balances table
CREATE TABLE Balances (
    BalanceId INT IDENTITY(1,1) PRIMARY KEY,
    UserId INT FOREIGN KEY REFERENCES Users(UserId),
    Currency NVARCHAR(10) NOT NULL,
    Amount DECIMAL(18,2) DEFAULT 0,
    UpdatedAt DATETIME DEFAULT GETDATE()
);

-- Transactions table
CREATE TABLE Transactions (
    TransactionId INT IDENTITY(1,1) PRIMARY KEY,
    UserId INT FOREIGN KEY REFERENCES Users(UserId),
    FromCurrency NVARCHAR(10) NOT NULL,
    ToCurrency NVARCHAR(10) NOT NULL,
    Amount DECIMAL(18,2) NOT NULL,
    ExchangeRate DECIMAL(18,6) NOT NULL,
    Result DECIMAL(18,2) NOT NULL,
    TransactionDate DATETIME DEFAULT GETDATE()
);

-- Sample data
INSERT INTO Users (Username, Email, PasswordHash) 
VALUES ('testuser', 'test@example.com', 'hash123');

INSERT INTO Balances (UserId, Currency, Amount) 
VALUES (1, 'PLN', 10000), (1, 'USD', 500), (1, 'EUR', 300); 
