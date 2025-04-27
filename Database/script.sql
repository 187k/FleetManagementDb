
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'TransportManagement')
BEGIN
    CREATE DATABASE TransportManagement;
END
GO


USE TransportManagement;
GO


CREATE TABLE Users (
    Id INT PRIMARY KEY IDENTITY(1,1),
    FullName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL UNIQUE,
    Password NVARCHAR(100) NOT NULL,
    Role INT NOT NULL CHECK (Role IN (1, 2)), -- 1 - Admin, 2 - User
    CreatedAt DATETIME DEFAULT GETDATE(),
    LastLogin DATETIME NULL
);

-- Таблица Manufacturers (Производители)
CREATE TABLE Manufacturers (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(50) NOT NULL UNIQUE,
    Country NVARCHAR(50) NULL
);


CREATE TABLE VehicleModels (
    Id INT PRIMARY KEY IDENTITY(1,1),
    ManufacturerId INT NOT NULL,
    ModelName NVARCHAR(50) NOT NULL,
    VehicleType NVARCHAR(30) NOT NULL,
    FOREIGN KEY (ManufacturerId) REFERENCES Manufacturers(Id),
    CONSTRAINT UQ_ManufacturerModel UNIQUE (ManufacturerId, ModelName)
);


CREATE TABLE Vehicles (
    Id INT PRIMARY KEY IDENTITY(1,1),
    LicensePlate NVARCHAR(20) NOT NULL UNIQUE,
    ModelId INT NOT NULL,
    YearOfManufacture INT NOT NULL,
    Mileage DECIMAL(10, 2) NOT NULL DEFAULT 0,
    Status NVARCHAR(20) NOT NULL CHECK (Status IN ('Доступен', 'В ремонте', 'Списано')),
    LastMaintenanceDate DATE NULL,
    NextMaintenanceMileage DECIMAL(10, 2) NULL,
    PurchaseDate DATE NOT NULL,
    PurchasePrice DECIMAL(12, 2) NULL,
    FOREIGN KEY (ModelId) REFERENCES VehicleModels(Id)
);


CREATE TABLE Drivers (
    Id INT PRIMARY KEY IDENTITY(1,1),
    UserId INT NULL UNIQUE, 
    FullName NVARCHAR(100) NOT NULL,
    LicenseNumber NVARCHAR(50) NOT NULL UNIQUE,
    LicenseCategory NVARCHAR(10) NOT NULL,
    PhoneNumber NVARCHAR(20) NOT NULL,
    Experience INT NOT NULL,
    BirthDate DATE NOT NULL,
    HireDate DATE NOT NULL,
    IsActive BIT DEFAULT 1,
    FOREIGN KEY (UserId) REFERENCES Users(Id)
);


CREATE TABLE Locations (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL UNIQUE,
    Address NVARCHAR(200) NULL,
    City NVARCHAR(50) NOT NULL,
    Latitude DECIMAL(9,6) NULL,
    Longitude DECIMAL(9,6) NULL
);


CREATE TABLE Routes (
    Id INT PRIMARY KEY IDENTITY(1,1),
    VehicleId INT NOT NULL,
    DriverId INT NOT NULL,
    StartDate DATETIME NOT NULL,
    EndDate DATETIME NOT NULL,
    StartLocationId INT NOT NULL,
    EndLocationId INT NOT NULL,
    Distance DECIMAL(10, 2) NOT NULL,
    Status NVARCHAR(20) NOT NULL CHECK (Status IN ('Запланирован', 'В пути', 'Завершен', 'Отменен')),
    CargoDescription NVARCHAR(200) NULL,
    FuelConsumption DECIMAL(8, 2) NULL,
    Notes NVARCHAR(500) NULL,
    CreatedBy INT NOT NULL,
    CreatedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (VehicleId) REFERENCES Vehicles(Id),
    FOREIGN KEY (DriverId) REFERENCES Drivers(Id),
    FOREIGN KEY (StartLocationId) REFERENCES Locations(Id),
    FOREIGN KEY (EndLocationId) REFERENCES Locations(Id),
    FOREIGN KEY (CreatedBy) REFERENCES Users(Id),
    CHECK (EndDate > StartDate)
);


CREATE TABLE Maintenance (
    Id INT PRIMARY KEY IDENTITY(1,1),
    VehicleId INT NOT NULL,
    MaintenanceDate DATETIME NOT NULL,
    MaintenanceType NVARCHAR(50) NOT NULL,
    Description NVARCHAR(500) NULL,
    Cost DECIMAL(10, 2) NULL,
    Mileage DECIMAL(10, 2) NOT NULL,
    NextMaintenanceDate DATETIME NULL,
    NextMaintenanceMileage DECIMAL(10, 2) NULL,
    PerformedBy NVARCHAR(100) NULL, 
    FOREIGN KEY (VehicleId) REFERENCES Vehicles(Id)
);


CREATE TABLE FuelRefills (
    Id INT PRIMARY KEY IDENTITY(1,1),
    VehicleId INT NOT NULL,
    RefillDate DATETIME NOT NULL,
    Amount DECIMAL(6, 2) NOT NULL, 
    Cost DECIMAL(10, 2) NOT NULL,
    Mileage DECIMAL(10, 2) NOT NULL,
    GasStation NVARCHAR(100) NULL,
    FOREIGN KEY (VehicleId) REFERENCES Vehicles(Id)
);


CREATE TABLE Violations (
    Id INT PRIMARY KEY IDENTITY(1,1),
    DriverId INT NULL,
    VehicleId INT NULL,
    ViolationDate DATETIME NOT NULL,
    Description NVARCHAR(500) NOT NULL,
    FineAmount DECIMAL(10, 2) NULL,
    IsPaid BIT DEFAULT 0,
    FOREIGN KEY (DriverId) REFERENCES Drivers(Id),
    FOREIGN KEY (VehicleId) REFERENCES Vehicles(Id)
);


CREATE TABLE DriverAssignments (
    Id INT PRIMARY KEY IDENTITY(1,1),
    DriverId INT NOT NULL,
    VehicleId INT NOT NULL,
    AssignmentDate DATE NOT NULL,
    UnassignmentDate DATE NULL,
    IsPrimary BIT DEFAULT 1,
    FOREIGN KEY (DriverId) REFERENCES Drivers(Id),
    FOREIGN KEY (VehicleId) REFERENCES Vehicles(Id),
    CHECK (UnassignmentDate IS NULL OR UnassignmentDate >= AssignmentDate)
);
