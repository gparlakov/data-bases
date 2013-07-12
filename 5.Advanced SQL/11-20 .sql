-- 11 Write a SQL query to find all managers that have exactly 5 employees. Display their first name and last name.

SELECT DISTINCT m.ManagerID,
	e.FirstName + ' ' + e.LastName [Manger Name]
FROM Employees m
	JOIN Employees e
		ON m.ManagerID = e.EmployeeID
	where m.ManagerID in
	(SELECT ManagerID FROM Employees  GROUP BY ManagerID Having Count(*) = 5)

------------------------------------------------------------------------------------------------------------------------------------------------------------


-- 12 Write a SQL query to find all employees along with their managers. For employees that do not have manager display the value "(no manager)".


SELECT e.LastName as Employee,
	 ISNULL(m.LastName, '(no manager)') as Manager
FROM Employees e
	LEFT JOIN Employees m
		ON e.ManagerID = m.EmployeeID

------------------------------------------------------------------------------------------------------------------------------------------------------------

-- 13 Write a SQL query to find the names of all employees whose last name is exactly 5 characters long. Use the built-in LEN(str) function.

SELECT e.LastName as Employee	 
	FROM Employees e
	where LEN(e.LastName) = 5
	
------------------------------------------------------------------------------------------------------------------------------------------------------------



-- 14 Write a SQL query to display the current date and time in the following format "day.month.year hour:minutes:seconds:milliseconds". Search in  Google to find how to format dates in SQL Server.

SELECT convert(varchar, getdate(), 113)

------------------------------------------------------------------------------------------------------------------------------------------------------------


-- 15 Write a SQL statement to create a table Users. Users should have username, password, full name and last login time. Choose appropriate data types for the table fields. Define a primary key column with a primary key constraint. Define the primary key column as identity to facilitate inserting records. Define unique constraint to avoid repeating usernames. Define a check constraint to ensure the password is at least 5 characters long.

CREATE TABLE Users(
	Id int IDENTITY,
	Username varchar(50) NOT NULL,
	FullName varchar(100),
	LastLoginDate DateTime,
	CONSTRAINT PK_Users PRIMARY KEY(Id),
	CONSTRAINT UC_Username UNIQUE(Username),
	CONSTRAINT UC_UsernameMin_6 CHECK(LEN(Username) >= 6),
)

------------------------------------------------------------------------------------------------------------------------------------------------------------


-- 16 Write a SQL statement to create a view that displays the users from the Users
-- table that have been in the system today. Test if the view works correctly.

CREATE VIEW V_UsersLoggedInToday AS
SELECT * 
FROM USERS 
where (DATEDIFF(DAY, LastLoginDate, GETDATE()) < 1)

--test

INSERT INTO Users(Username, LastLoginDate) Values ('user123', GetDate())
INSERT INTO Users(Username, LastLoginDate) Values ('user1231', GetDate())
INSERT INTO Users(Username, LastLoginDate) Values ('user1232', '20051010')

select * from V_UsersLoggedInToday 
------------------------------------------------------------------------------------------------------------------------------------------------------------


-- 17 Write a SQL statement to create a table Groups. Groups 
--should have unique name (use unique constraint). Define primary key and identity column.

CREATE TABLE Groups(
	Id int IDENTITY,
	Name varchar(40) NOT NULL,
	CONSTRAINT PK_Id PRIMARY KEY (Id),
	CONSTRAINT UK_Name UNIQUE(Name)
)


------------------------------------------------------------------------------------------------------------------------------------------------------------


--18 Write a SQL statement to add a column GroupID to the table Users. Fill some data in 
--this new column and as well in the Groups table. Write a SQL statement to add a foreign 
--key constraint between tables Users and Groups tables.

ALTER TABLE Users	
	ADD CONSTRAINT FK_GroupId FOREIGN KEY(GroupId) REFERENCES Groups (Id)
	ADD GroupId int


------------------------------------------------------------------------------------------------------------------------------------------------------------


-- 19 



------------------------------------------------------------------------------------------------------------------------------------------------------------


