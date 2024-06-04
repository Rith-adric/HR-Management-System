```````````````````````````````````````````Make Database````````````````````````````````````
CREATE DATABASE NAME HRMS

```````````````````````````````````````````Make Table```````````````````````````````````````
CREATE TABLE Employees (
    EmployeeID INT PRIMARY KEY,
    FirstName NVARCHAR(50),
    LastName NVARCHAR(50),
    Department NVARCHAR(50),
    Position NVARCHAR(50),
    DateOfBirth DATE,
    Salary DECIMAL(10, 2)
);
CREATE TABLE Departments (
    DepartmentID INT PRIMARY KEY,
    DepartmentName NVARCHAR(50),
    ManagerID INT,
    CONSTRAINT FK_Manager FOREIGN KEY (ManagerID) REFERENCES Employees(EmployeeID)
);
CREATE TABLE Attendance (
    AttendanceID INT PRIMARY KEY,
    EmployeeID INT,
    AttendanceDate DATE,
    StartTime TIME,
    EndTime TIME,
    Status NVARCHAR(20),
    CONSTRAINT FK_Attendance_Employee FOREIGN KEY (EmployeeID) REFERENCES Employees(EmployeeID)
);
CREATE TABLE LeaveRecords (
    RequestID INT PRIMARY KEY,
    EmployeeID INT,
    LeaveStartDate DATE,
    LeaveEndDate DATE,
    LeaveType NVARCHAR(50),
    Status NVARCHAR(20),
    CONSTRAINT FK_LeaveRecords_Employee FOREIGN KEY (EmployeeID) REFERENCES Employees(EmployeeID)
);

CREATE TABLE Users (
    UserID INT PRIMARY KEY,
    Username NVARCHAR(50) UNIQUE,
    Password NVARCHAR(100),
    Email NVARCHAR(100) UNIQUE,
    FullName NVARCHAR(100)
);
CREATE TABLE Admins (
    AdminID INT PRIMARY KEY,
    Username NVARCHAR(50) UNIQUE,
    Password NVARCHAR(100),
    Email NVARCHAR(100) UNIQUE,
    FullName NVARCHAR(100)
);
```````````````````````````````````````````After done ```````````````````````````````````````
=> Go to our database HRMS than tables :
=> Right click on eacher table than click design and click id(Primary Keys)
  - Finding in column Properties:
  - Identity Specification than change:
      -Than change Identity Increment = 1
      -Than change Identity Speed = 1 click save
