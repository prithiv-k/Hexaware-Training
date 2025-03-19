-- Creating the database 
Create Database Courier_Management_System

-- Creating the Table User
CREATE TABLE UserTable (
    UserID INT PRIMARY KEY,
    Name VARCHAR(255),
    Email VARCHAR(255) UNIQUE,
    Password VARCHAR(255),
    ContactNumber VARCHAR(20),
    Address TEXT
);

-- Creating the Table Courier
CREATE TABLE Courier (
    CourierID INT PRIMARY KEY,
    SenderName VARCHAR(255),
    SenderAddress TEXT,
    ReceiverName VARCHAR(255),
    ReceiverAddress TEXT,
    Weight DECIMAL(5,2),
    Status VARCHAR(50),
    TrackingNumber VARCHAR(20) UNIQUE,
    DeliveryDate DATE
);

--Creating the Table CourierServices
CREATE TABLE CourierServices (
    ServiceID INT PRIMARY KEY,
    ServiceName VARCHAR(100),
    Cost DECIMAL(8,2)
);

--creating the Table Employee

CREATE TABLE Employee (
    EmployeeID INT PRIMARY KEY,
    Name VARCHAR(255),
    Email VARCHAR(255) UNIQUE,
    ContactNumber VARCHAR(20),
    Role VARCHAR(50),
    Salary DECIMAL(10,2)
);
--Creating the Table Location
CREATE TABLE Location (
    LocationID INT PRIMARY KEY,
    LocationName VARCHAR(100),
    Address TEXT
);

--Creating the Table Payment
CREATE TABLE Payment (
    PaymentID INT PRIMARY KEY,
    CourierID INT,
    LocationID INT,
    Amount DECIMAL(10,2),
    PaymentDate DATE,
    FOREIGN KEY (CourierID) REFERENCES Courier(CourierID),
    FOREIGN KEY (LocationID) REFERENCES Location(LocationID)
);
-- Inserting values into UserTable
INSERT INTO UserTable (UserID, Name, Email, Password, ContactNumber, Address) VALUES
(1, 'Raj', 'raj@gmail.com', 'raj@123', '9876543210', 'Chennai, Tamil Nadu'),
(2, 'Amit', 'amit@gmail.com', 'amit@321', '8765432109', 'Mumbai, Maharashtra'),
(3, 'Priya', 'priya@gmail.com', 'priya@pass', '7654321098', 'Kolkata, West Bengal'),
(4, 'Neha', 'neha@gmail.com', 'neha@123', '6543210987', 'Bangalore, Karnataka'),
(5, 'Vikram', 'vikram@gmail.com', 'vikram@987', '5432109876', 'Delhi'),
(6, 'Sneha', 'sneha@gmail.com', 'sneha@456', '4321098765', 'Hyderabad, Telangana'),
(7, 'Arun', 'arun@gmail.com', 'arun@abc', '3210987654', 'Pune, Maharashtra'),
(8, 'Kiran', 'kiran@gmail.com', 'kiran@789', '2109876543', 'Ahmedabad, Gujarat'),
(9, 'Deepak', 'deepak@gmail.com', 'deepak@pass', '1098765432', 'Jaipur, Rajasthan'),
(10, 'Meera', 'meera@gmail.com', 'meera@101', '9876501234', 'Coimbatore, Tamil Nadu');

-- Inserting values into Courier
INSERT INTO Courier (CourierID, SenderName, SenderAddress, ReceiverName, ReceiverAddress, Weight, Status, TrackingNumber, DeliveryDate) VALUES
(1, 'Raj', 'Chennai, Tamil Nadu', 'Amit', 'Mumbai, Maharashtra', 2.5, 'In Transit', 'TRK1001', '2025-03-20'),
(2, 'Amit', 'Mumbai, Maharashtra', 'Priya', 'Kolkata, West Bengal', 1.2, 'Delivered', 'TRK1002', '2025-03-18'),
(3, 'Priya', 'Kolkata, West Bengal', 'Neha', 'Bangalore, Karnataka', 3.0, 'Pending', 'TRK1003', '2025-03-22'),
(4, 'Neha', 'Bangalore, Karnataka', 'Vikram', 'Delhi', 2.8, 'Dispatched', 'TRK1004', '2025-03-21'),
(5, 'Vikram', 'Delhi', 'Sneha', 'Hyderabad, Telangana', 1.5, 'In Transit', 'TRK1005', '2025-03-23'),
(6, 'Sneha', 'Hyderabad, Telangana', 'Arun', 'Pune, Maharashtra', 2.3, 'Delivered', 'TRK1006', '2025-03-19'),
(7, 'Arun', 'Pune, Maharashtra', 'Kiran', 'Ahmedabad, Gujarat', 3.2, 'Pending', 'TRK1007', '2025-03-24'),
(8, 'Kiran', 'Ahmedabad, Gujarat', 'Deepak', 'Jaipur, Rajasthan', 1.8, 'Dispatched', 'TRK1008', '2025-03-20'),
(9, 'Deepak', 'Jaipur, Rajasthan', 'Meera', 'Coimbatore, Tamil Nadu', 2.0, 'In Transit', 'TRK1009', '2025-03-22'),
(10, 'Meera', 'Coimbatore, Tamil Nadu', 'Raj', 'Chennai, Tamil Nadu', 1.7, 'Delivered', 'TRK1010', '2025-03-18');

-- Inserting values into CourierServices
INSERT INTO CourierServices (ServiceID, ServiceName, Cost) VALUES
(1, 'Standard', 100.00),
(2, 'Express', 200.00),
(3, 'Overnight', 500.00),
(4, 'Same Day', 800.00),
(5, 'International', 1500.00),
(6, 'Economy', 50.00),
(7, 'Premium', 250.00),
(8, 'Super Express', 600.00),
(9, 'Bulk Shipping', 1200.00),
(10, 'Local Delivery', 30.00);

-- Inserting values into Employee
INSERT INTO Employee (EmployeeID, Name, Email, ContactNumber, Role, Salary) VALUES
(1, 'Suresh', 'suresh@gmail.com', '9812345678', 'Delivery Boy', 20000.00),
(2, 'Ramesh', 'ramesh@gmail.com', '9823456789', 'Manager', 50000.00),
(3, 'Karthik', 'karthik@gmail.com', '9834567890', 'Dispatcher', 25000.00),
(4, 'Manoj', 'manoj@gmail.com', '9845678901', 'Customer Support', 22000.00),
(5, 'Anil', 'anil@gmail.com', '9856789012', 'Accountant', 30000.00),
(6, 'Vivek', 'vivek@gmail.com', '9867890123', 'Warehouse Staff', 18000.00),
(7, 'Surya', 'surya@gmail.com', '9878901234', 'Driver', 22000.00),
(8, 'Rajesh', 'rajesh@gmail.com', '9889012345', 'Security', 17000.00),
(9, 'Ajay', 'ajay@gmail.com', '9890123456', 'Admin', 40000.00),
(10, 'Prakash', 'prakash@gmail.com', '9901234567', 'HR', 45000.00);

-- Inserting values into Location
INSERT INTO Location (LocationID, LocationName, Address) VALUES
(1, 'Chennai Hub', 'Chennai, Tamil Nadu'),
(2, 'Mumbai Hub', 'Mumbai, Maharashtra'),
(3, 'Kolkata Hub', 'Kolkata, West Bengal'),
(4, 'Bangalore Hub', 'Bangalore, Karnataka'),
(5, 'Delhi Hub', 'Delhi'),
(6, 'Hyderabad Hub', 'Hyderabad, Telangana'),
(7, 'Pune Hub', 'Pune, Maharashtra'),
(8, 'Ahmedabad Hub', 'Ahmedabad, Gujarat'),
(9, 'Jaipur Hub', 'Jaipur, Rajasthan'),
(10, 'Coimbatore Hub', 'Coimbatore, Tamil Nadu');

-- Inserting values into Payment
INSERT INTO Payment (PaymentID, CourierID, LocationID, Amount, PaymentDate) VALUES
(1, 1, 1, 200.00, '2025-03-20'),
(2, 2, 2, 150.00, '2025-03-18'),
(3, 3, 3, 300.00, '2025-03-22'),
(4, 4, 4, 250.00, '2025-03-21'),
(5, 5, 5, 180.00, '2025-03-23'),
(6, 6, 6, 220.00, '2025-03-19'),
(7, 7, 7, 400.00, '2025-03-24'),
(8, 8, 8, 320.00, '2025-03-20'),
(9, 9, 9, 210.00, '2025-03-22'),
(10, 10, 10, 170.00, '2025-03-18');

Select * from UserTable
Select * from Courier
Select * from CourierServices
Select * from Employee
Select * from Location
Select * from Payment
