    CREATE TABLE Departments(

    DepartmentId int Identity(1,1) primary key,
    DepartmentName	varchar(100) unique NOT NULL)

    CREATE TABLE Employees
(
    EmployeeId INT IDENTITY(1,1) CONSTRAINT PK_Employees PRIMARY KEY,
    FirstName VARCHAR(50) NOT NULL,
    LastName VARCHAR(50) NOT NULL,
    Phone VARCHAR(15) UNIQUE NOT NULL,
    Gender VARCHAR(10) NOT NULL,
    DateOfBirth DATE NOT NULL,
    JoiningDate DATE NOT NULL,
    DepartmentId INT NOT NULL,

    CONSTRAINT FK_Employee_Department
        FOREIGN KEY (DepartmentId) REFERENCES Departments(DepartmentId),

    CONSTRAINT CHK_Employee_DOB_Join CHECK (DateOfBirth < JoiningDate),
    CONSTRAINT CHK_Employee_Gender CHECK (Gender IN ('Male','Female'))
);

    CREATE TABLE Roles
    (
        RoleId INT IDENTITY(1,1) CONSTRAINT PK_Roles PRIMARY KEY,

        RoleName VARCHAR(50) unique NOT NULL
    );

    CREATE TABLE Users
(
    UserId INT IDENTITY(1,1) PRIMARY KEY,
    EmployeeId INT UNIQUE NOT NULL,
    Email VARCHAR(100) UNIQUE NOT NULL,
    Password VARCHAR(255) NOT NULL,
    RoleId INT NOT NULL,

    CONSTRAINT FK_User_Employee
        FOREIGN KEY (EmployeeId) REFERENCES Employees(EmployeeId),

    CONSTRAINT FK_User_Role
        FOREIGN KEY (RoleId) REFERENCES Roles(RoleId)
);

CREATE TABLE LeaveTypes
(
    LeaveTypeId INT IDENTITY(1,1) PRIMARY KEY,
    LeaveTypeName VARCHAR(50) UNIQUE NOT NULL,
    MaximumLeaves INT NOT NULL
);

    CREATE TABLE LeaveRequests
(
    LeaveRequestId INT IDENTITY(1,1) PRIMARY KEY,
    EmployeeId INT NOT NULL,
    LeaveTypeId INT NOT NULL,
    FromDate DATE NOT NULL,
    ToDate DATE NOT NULL,
    Reason VARCHAR(500) NOT NULL,
    AppliedDate DATETIME NOT NULL DEFAULT GETDATE(),
    Status VARCHAR(20) NOT NULL,

    CONSTRAINT FK_LeaveRequest_Employee
        FOREIGN KEY (EmployeeId) REFERENCES Employees(EmployeeId),

    CONSTRAINT FK_LeaveRequest_LeaveType
        FOREIGN KEY (LeaveTypeId) REFERENCES LeaveTypes(LeaveTypeId),

    CONSTRAINT CHK_LeaveDates CHECK (ToDate >= FromDate),
    CONSTRAINT CHK_Status CHECK (Status IN ('Pending','Approved','Rejected'))
);

INSERT INTO Departments (DepartmentName)
VALUES 
('IT'),
('HR'),
('Finance'),
('Operations');

INSERT INTO Roles (RoleName)
VALUES 
('Admin'),
('Employee');


INSERT INTO Employees (FirstName, LastName, Phone, Gender, DateOfBirth, JoiningDate, DepartmentId)
VALUES
('Amit','Sharma','9876543210','Male','1998-05-12','2022-06-01',1),
('Priya','Reddy','9876543211','Female','1997-08-21','2021-03-15',2),
('Rahul','Verma','9876543212','Male','1996-11-10','2020-01-10',3),
('Sneha','Patel','9876543213','Female','1999-02-18','2023-02-01',1),
('Kiran','Kumar','9876543214','Male','1995-07-25','2019-07-20',4);

INSERT INTO Users (EmployeeId, Email, Password, RoleId)
VALUES
(1,'amit@gmail.com','pass123',2),
(2,'priya@gmail.com','pass123',2),
(3,'rahul@gmail.com','pass123',2),
(4,'sneha@gmail.com','pass123',2),
(5,'kiran@gmail.com','pass123',2);


INSERT INTO LeaveTypes (LeaveTypeName, MaximumLeaves)
VALUES
('Casual Leave', 12),
('Sick Leave', 10),
('Earned Leave', 15),
('Maternity Leave', 180),
('Unpaid Leave', 30);

INSERT INTO LeaveRequests (EmployeeId, LeaveTypeId, FromDate, ToDate, Reason, Status)
VALUES
(1, 1, '2026-01-10', '2026-01-12', 'Personal work', 'Approved'),
(2, 2, '2026-01-15', '2026-01-16', 'Fever', 'Approved'),
(3, 1, '2026-02-01', '2026-02-03', 'Travel', 'Pending'),
(4, 3, '2026-02-05', '2026-02-10', 'Vacation', 'Rejected'),
(5, 2, '2026-02-12', '2026-02-13', 'Health issue', 'Approved'),
(1, 1, '2026-02-15', '2026-02-16', 'Function', 'Pending'),
(2, 3, '2026-03-01', '2026-03-05', 'Trip', 'Approved'),
(3, 2, '2026-03-10', '2026-03-11', 'Cold', 'Approved'),
(4, 1, '2026-03-15', '2026-03-16', 'Personal', 'Rejected'),
(5, 3, '2026-03-20', '2026-03-25', 'Holiday', 'Pending');


select * from employees;
select * from users;
select * from departments;
select * from roles;
select * from leavetypes;
select * from leaverequests;

