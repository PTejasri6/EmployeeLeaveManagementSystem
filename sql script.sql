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






GO

CREATE FUNCTION fn_GetLeaveDays
(
    @FromDate DATE,
    @ToDate DATE
)
RETURNS INT
AS
BEGIN

    DECLARE @LeaveDays INT;

    IF (@FromDate IS NULL OR @ToDate IS NULL)
        SET @LeaveDays = 0

    ELSE
        SET @LeaveDays = DATEDIFF(DAY,@FromDate,@ToDate) + 1;


    RETURN @LeaveDays;

END

GO


GO
CREATE FUNCTION fn_GetRemainingLeaves
(
    @EmployeeId INT,
    @LeaveTypeId INT
)
RETURNS INT
AS
BEGIN

    DECLARE @MaximumLeaves INT;
    DECLARE @UsedLeaves INT;
    DECLARE @RemainingLeaves INT;

    SELECT @MaximumLeaves = MaximumLeaves
    FROM LeaveTypes
    WHERE LeaveTypeId = @LeaveTypeId;


    SELECT @UsedLeaves = ISNULL
    (
        SUM(dbo.fn_GetLeaveDays(FromDate,ToDate)),
        0
    )
    FROM LeaveRequests
    WHERE EmployeeId = @EmployeeId
    AND LeaveTypeId = @LeaveTypeId
    AND Status = 'Approved';


    SET @RemainingLeaves =
        ISNULL(@MaximumLeaves,0) - ISNULL(@UsedLeaves,0);


    RETURN @RemainingLeaves;

END;
GO




GO
CREATE PROCEDURE sp_AddEmployee
(
    @FirstName VARCHAR(50),
    @LastName VARCHAR(50),
    @Phone VARCHAR(15),
    @Gender VARCHAR(10),
    @DateOfBirth DATE,
    @JoiningDate DATE,
    @DepartmentId INT
)
AS
BEGIN
    DECLARE @retval INT;

    BEGIN TRY

        IF (@FirstName IS NULL OR LEN(LTRIM(RTRIM(@FirstName)))=0)
            SET @retval=-1

        ELSE IF (@LastName IS NULL OR LEN(LTRIM(RTRIM(@LastName)))=0)
            SET @retval=-2
        ELSE IF (@Phone IS NULL OR LEN(LTRIM(RTRIM(@Phone)))=0 OR LEN(LTRIM(RTRIM(@Phone))) <> 10 OR LTRIM(RTRIM(@Phone)) LIKE '%[^0-9]%')
            SET @retval=-3

        ELSE IF EXISTS(SELECT 1 FROM Employees WHERE Phone = LTRIM(RTRIM(@Phone)))
            SET @retval=-4
        

        ELSE IF (@Gender IS NULL OR @Gender NOT IN ('Male','Female'))
            SET @retval=-5

        ELSE IF ( @JoiningDate IS NULL OR @DateOfBirth IS NULL OR @DateOfBirth>=@JoiningDate )
            SET @retval=-6

        ELSE IF (@DepartmentId IS NULL OR NOT EXISTS
        (
            SELECT 1
            FROM Departments
            WHERE DepartmentId=@DepartmentId
        ))
            SET @retval=-7
        ELSE IF (@DateOfBirth > DATEADD(YEAR,-18,CAST(GETDATE() AS DATE)))
            SET @retval = -8
        ELSE
        BEGIN

            INSERT INTO Employees
            (
                FirstName,
                LastName,
                Phone,
                Gender,
                DateOfBirth,
                JoiningDate,
                DepartmentId
            )
            VALUES
            (
                LTRIM(RTRIM(@FirstName)),
    LTRIM(RTRIM(@LastName)),
    LTRIM(RTRIM(@Phone)),
                @Gender,
                @DateOfBirth,
                @JoiningDate,
                @DepartmentId
            );

            SET @retval=1;

        END

        SELECT @retval as ReturnValue;

    END TRY

    BEGIN CATCH

        SET @retval=-99;
        SELECT @retval as ReturnValue;

    END CATCH
END

GO





GO


CREATE PROCEDURE sp_UpdateEmployee
(
    @EmployeeId INT,
    @FirstName VARCHAR(50),
    @LastName VARCHAR(50),
    @Phone VARCHAR(15),
    @Gender VARCHAR(10),
    @DateOfBirth DATE,
    @JoiningDate DATE,
    @DepartmentId INT
)
AS
BEGIN
    DECLARE @retval INT;

    BEGIN TRY

        IF (@EmployeeId IS NULL 
            OR NOT EXISTS
            (
                SELECT 1 
                FROM Employees 
                WHERE EmployeeId=@EmployeeId
            ))
            SET @retval=-1

        ELSE IF (@FirstName IS NULL 
                 OR LEN(LTRIM(RTRIM(@FirstName)))=0)
            SET @retval=-2

        ELSE IF (@LastName IS NULL 
                 OR LEN(LTRIM(RTRIM(@LastName)))=0)
            SET @retval=-3

        ELSE IF (@Phone IS NULL 
             OR LEN(LTRIM(RTRIM(@Phone)))=0
             OR LEN(LTRIM(RTRIM(@Phone))) <> 10
             OR LTRIM(RTRIM(@Phone)) LIKE '%[^0-9]%')
            SET @retval=-4

        ELSE IF EXISTS
        (
            SELECT 1 
            FROM Employees
            WHERE Phone = LTRIM(RTRIM(@Phone))
            AND EmployeeId <> @EmployeeId
        )
            SET @retval=-5

        ELSE IF (@Gender IS NULL 
                 OR @Gender NOT IN ('Male','Female'))
            SET @retval=-6

        ELSE IF (@DateOfBirth IS NULL 
              OR @JoiningDate IS NULL 
              OR @DateOfBirth >= @JoiningDate)
            SET @retval=-7

        ELSE IF (@DepartmentId IS NULL 
              OR NOT EXISTS
              (
                    SELECT 1
                    FROM Departments
                    WHERE DepartmentId=@DepartmentId
              ))
            SET @retval=-8

        ELSE IF (@DateOfBirth > DATEADD(YEAR,-18,CAST(GETDATE() AS DATE)))
            SET @retval=-9

        ELSE
        BEGIN

            UPDATE Employees
            SET
                FirstName = LTRIM(RTRIM(@FirstName)),
                LastName = LTRIM(RTRIM(@LastName)),
                Phone = LTRIM(RTRIM(@Phone)),
                Gender = @Gender,
                DateOfBirth = @DateOfBirth,
                JoiningDate = @JoiningDate,
                DepartmentId = @DepartmentId
            WHERE EmployeeId=@EmployeeId;

            SET @retval=1;

        END

        SELECT @retval AS ReturnValue;

    END TRY

    BEGIN CATCH

        SET @retval=-99;
        SELECT @retval AS ReturnValue;

    END CATCH
END

GO


GO

CREATE PROCEDURE sp_DeleteEmployee
(
    @EmployeeId INT
)
AS
BEGIN
    DECLARE @retval INT;

    BEGIN TRY

        IF (@EmployeeId IS NULL 
            OR NOT EXISTS
            (
                SELECT 1
                FROM Employees
                WHERE EmployeeId=@EmployeeId
            ))
            SET @retval=-1

        ELSE
        BEGIN

            DELETE FROM LeaveRequests
            WHERE EmployeeId=@EmployeeId;


            DELETE FROM Users
            WHERE EmployeeId=@EmployeeId;


            DELETE FROM Employees
            WHERE EmployeeId=@EmployeeId;


            SET @retval=1;

        END


        SELECT @retval AS ReturnValue;


    END TRY

    BEGIN CATCH

        SET @retval=-99;
        SELECT @retval AS ReturnValue;

    END CATCH

END

GO




GO

CREATE PROCEDURE sp_ApplyLeave
(
    @EmployeeId INT,
    @LeaveTypeId INT,
    @FromDate DATE,
    @ToDate DATE,
    @Reason VARCHAR(500)
)
AS
BEGIN

    DECLARE @retval INT;

    BEGIN TRY

        IF (@EmployeeId IS NULL 
            OR NOT EXISTS
            (
                SELECT 1 
                FROM Employees 
                WHERE EmployeeId=@EmployeeId
            ))
            SET @retval=-1


        ELSE IF (@LeaveTypeId IS NULL 
              OR NOT EXISTS
              (
                    SELECT 1
                    FROM LeaveTypes
                    WHERE LeaveTypeId=@LeaveTypeId
              ))
            SET @retval=-2


        ELSE IF (@FromDate IS NULL 
              OR @ToDate IS NULL
              OR @FromDate > @ToDate)
            SET @retval=-3


        ELSE IF (@FromDate < CAST(GETDATE() AS DATE))
            SET @retval=-4


        ELSE IF (@Reason IS NULL 
              OR LEN(LTRIM(RTRIM(@Reason)))=0)
            SET @retval=-5


        ELSE IF EXISTS
        (
            SELECT 1
            FROM LeaveRequests
            WHERE EmployeeId=@EmployeeId
            AND Status IN ('Pending','Approved')
            AND
            (
                @FromDate BETWEEN FromDate AND ToDate
                OR
                @ToDate BETWEEN FromDate AND ToDate
                OR
                FromDate BETWEEN @FromDate AND @ToDate
            )
        )
            SET @retval=-6


        ELSE IF
        (
            dbo.fn_GetRemainingLeaves(@EmployeeId,@LeaveTypeId)
            <
            dbo.fn_GetLeaveDays(@FromDate,@ToDate)
        )
            SET @retval=-7


        ELSE
        BEGIN

            INSERT INTO LeaveRequests
            (
                EmployeeId,
                LeaveTypeId,
                FromDate,
                ToDate,
                Reason,
                Status
            )
            VALUES
            (
                @EmployeeId,
                @LeaveTypeId,
                @FromDate,
                @ToDate,
                LTRIM(RTRIM(@Reason)),
                'Pending'
            );


            SET @retval=1;

        END


        SELECT @retval AS ReturnValue;


    END TRY


    BEGIN CATCH

        SET @retval=-99;
        SELECT @retval AS ReturnValue;

    END CATCH

END

GO




GO

CREATE PROCEDURE sp_UpdateLeaveStatus
(
    @LeaveRequestId INT,
    @Status VARCHAR(20)
)
AS
BEGIN

    DECLARE @retval INT;

    BEGIN TRY

        IF (@LeaveRequestId IS NULL 
            OR NOT EXISTS
            (
                SELECT 1
                FROM LeaveRequests
                WHERE LeaveRequestId=@LeaveRequestId
            ))
            SET @retval=-1


        ELSE IF (@Status IS NULL 
              OR @Status NOT IN ('Approved','Rejected'))
            SET @retval=-2


        ELSE IF EXISTS
        (
            SELECT 1
            FROM LeaveRequests
            WHERE LeaveRequestId=@LeaveRequestId
            AND Status <> 'Pending'
        )
            SET @retval=-3


        ELSE
        BEGIN

            UPDATE LeaveRequests
            SET Status=@Status
            WHERE LeaveRequestId=@LeaveRequestId;


            SET @retval=1;

        END


        SELECT @retval AS ReturnValue;


    END TRY


    BEGIN CATCH

        SET @retval=-99;
        SELECT @retval AS ReturnValue;

    END CATCH

END

GO


GO

CREATE PROCEDURE sp_CancelLeaveRequest
(
    @LeaveRequestId INT,
    @EmployeeId INT
)
AS
BEGIN

    DECLARE @retval INT;

    BEGIN TRY

        IF (@EmployeeId IS NULL
            OR NOT EXISTS
            (
                SELECT 1
                FROM Employees
                WHERE EmployeeId=@EmployeeId
            ))
            SET @retval=-1


        ELSE IF (@LeaveRequestId IS NULL
              OR NOT EXISTS
              (
                    SELECT 1
                    FROM LeaveRequests
                    WHERE LeaveRequestId=@LeaveRequestId
              ))
            SET @retval=-2


        ELSE IF NOT EXISTS
        (
            SELECT 1
            FROM LeaveRequests
            WHERE LeaveRequestId=@LeaveRequestId
            AND EmployeeId=@EmployeeId
        )
            SET @retval=-3


        ELSE IF EXISTS
        (
            SELECT 1
            FROM LeaveRequests
            WHERE LeaveRequestId=@LeaveRequestId
            AND Status <> 'Pending'
        )
            SET @retval=-4


        ELSE
        BEGIN

            UPDATE LeaveRequests
            SET Status='Cancelled'
            WHERE LeaveRequestId=@LeaveRequestId
            AND EmployeeId=@EmployeeId;


            SET @retval=1;

        END


        SELECT @retval AS ReturnValue;


    END TRY


    BEGIN CATCH

        SET @retval=-99;
        SELECT @retval AS ReturnValue;

    END CATCH

END

GO

INSERT INTO Employees
(
FirstName,
LastName,
Phone,
Gender,
DateOfBirth,
JoiningDate,
DepartmentId
)
VALUES
(
'Admin',
'User',
'9999999999',
'Male',
'1990-01-01',
'2020-01-01',
1
);


INSERT INTO Users
(
EmployeeId,
Email,
Password,
RoleId
)
VALUES
(
8,
'admin@gmail.com',
'admin123',
1
);