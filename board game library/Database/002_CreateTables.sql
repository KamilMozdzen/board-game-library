USE BoardGameLibrary;
GO

CREATE TABLE dbo.Category
(
	CategoryId INT IDENTITY(1,1) PRIMARY KEY,
	Name NVARCHAR(100) NOT NULL,
	Description NVARCHAR(500) NULL
);

CREATE TABLE dbo.Publisher
(
	PublisherId INT IDENTITY(1,1) PRIMARY KEY,
	Name NVARCHAR(100) NOT NULL,
	Country NVARCHAR(100) NULL,
);

CREATE TABLE dbo.Customer
(
	CustomerId INT IDENTITY(1,1) PRIMARY KEY,
	FirstName NVARCHAR(100) NOT NULL,
	LastName NVARCHAR(100) NOT NULL,
	Email NVARCHAR(100) NOT NULL UNIQUE,
	PhoneNumber NVARCHAR(20) NULL
);

CREATE TABLE dbo.Employee
(
	EmployeeId INT IDENTITY(1,1) PRIMARY KEY,
	FirstName NVARCHAR(100) NOT NULL,
	LastName NVARCHAR(100) NOT NULL,
	Email NVARCHAR(100) NOT NULL UNIQUE,
);
CREATE TABLE dbo.Game
(
    GameId INT IDENTITY(1,1) PRIMARY KEY,
    Title NVARCHAR(150) NOT NULL,
    MinPlayers INT NOT NULL,
    MaxPlayers INT NOT NULL,
    PlayingTimeMinutes INT NOT NULL,
    DailyRate DECIMAL(10,2) NOT NULL,
    CategoryId INT NOT NULL,
    PublisherId INT NOT NULL,

    CONSTRAINT FK_Game_Category
        FOREIGN KEY (CategoryId)
        REFERENCES dbo.Category (CategoryId),

    CONSTRAINT FK_Game_Publisher
        FOREIGN KEY (PublisherId)
        REFERENCES dbo.Publisher (PublisherId),

    CONSTRAINT CK_Game_Players
        CHECK (MinPlayers >= 1 AND MaxPlayers >= MinPlayers),

    CONSTRAINT CK_Game_PlayingTime
        CHECK (PlayingTimeMinutes > 0),

    CONSTRAINT CK_Game_DailyRate
        CHECK (DailyRate >= 0)
);

CREATE TABLE dbo.GameCopy
(
    GameCopyId INT IDENTITY(1,1) PRIMARY KEY,
    InventoryNumber NVARCHAR(30) NOT NULL,
    ConditionDescription NVARCHAR(500) NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    GameId INT NOT NULL,

    CONSTRAINT UQ_GameCopy_InventoryNumber
        UNIQUE (InventoryNumber),

    CONSTRAINT FK_GameCopy_Game
        FOREIGN KEY (GameId)
        REFERENCES dbo.Game (GameId)
);

CREATE TABLE dbo.Rental
(
    RentalId INT IDENTITY(1,1) PRIMARY KEY,
    RentalDate DATE NOT NULL,
    DueDate DATE NOT NULL,
    CustomerId INT NOT NULL,
    EmployeeId INT NOT NULL,

    CONSTRAINT FK_Rental_Customer
        FOREIGN KEY (CustomerId)
        REFERENCES dbo.Customer (CustomerId),

    CONSTRAINT FK_Rental_Employee
        FOREIGN KEY (EmployeeId)
        REFERENCES dbo.Employee (EmployeeId),

    CONSTRAINT CK_Rental_Dates
        CHECK (DueDate >= RentalDate)
);

CREATE TABLE dbo.RentalItem
(
    RentalItemId INT IDENTITY(1,1) PRIMARY KEY,
    DailyRate DECIMAL(10,2) NOT NULL,
    ReturnDate DATE NULL,
    RentalId INT NOT NULL,
    GameCopyId INT NOT NULL,

    CONSTRAINT FK_RentalItem_Rental
        FOREIGN KEY (RentalId)
        REFERENCES dbo.Rental (RentalId),

    CONSTRAINT FK_RentalItem_GameCopy
        FOREIGN KEY (GameCopyId)
        REFERENCES dbo.GameCopy (GameCopyId),

    CONSTRAINT UQ_RentalItem_Rental_GameCopy
        UNIQUE (RentalId, GameCopyId),

    CONSTRAINT CK_RentalItem_DailyRate
        CHECK (DailyRate >= 0)
);

CREATE TABLE dbo.Payment
(
    PaymentId INT IDENTITY(1,1) PRIMARY KEY,
    Amount DECIMAL(10,2) NOT NULL,
    PaymentDate DATE NOT NULL,
    PaymentMethod NVARCHAR(30) NOT NULL,
    RentalId INT NOT NULL,

    CONSTRAINT FK_Payment_Rental
        FOREIGN KEY (RentalId)
        REFERENCES dbo.Rental (RentalId),

    CONSTRAINT CK_Payment_Amount
        CHECK (Amount > 0),

    CONSTRAINT CK_Payment_Method
        CHECK (PaymentMethod IN (N'Cash', N'Card', N'BankTransfer'))
);
GO

CREATE UNIQUE INDEX UX_RentalItem_ActiveGameCopy
ON dbo.RentalItem (GameCopyId)
WHERE ReturnDate IS NULL;
GO