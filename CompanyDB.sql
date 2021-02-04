CREATE TABLE Employees 
(EmployeeID int,
FirstName varchar(50),
LastName varchar(50),
Department varchar(50)
)

INSERT INTO Employees VALUES
(1001, 'Matej','Kolak', 'Developer'),
(1002, 'Branko','Kockica', 'Developer'),
(1003, 'Mirko','C', 'UI'),
(1004, 'Josip','Z', 'Tester')

CREATE TABLE Departments
(
DepartmentID int,
DepartmentName varchar(50),
Salary int
)

INSERT INTO Departments VALUES
(5001, 'Developer', 24000),
(5002, 'UI', 22000),
(5003, 'Tester', 29000)

