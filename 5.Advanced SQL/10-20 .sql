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


------------------------------------------------------------------------------------------------------------------------------------------------------------


-- 16 Write a SQL query to find the number of employees in the "Sales" department.

SELECT Count(EmployeeID) as [Count Employees in Sales] 
from Employees e WHERE e.DepartmentID =
(Select DepartmentID FROM Departments Where Name = 'sales')

------------------------------------------------------------------------------------------------------------------------------------------------------------


-- 17 Write a SQL query to find the number of all employees that have manager.

SELECT Count(EmployeeID) [Count Employees with manager] FROM Employees 
	WHERE ManagerID IS NOT NULL
------------------------------------------------------------------------------------------------------------------------------------------------------------

-- 18 Write a SQL query to find the number of all employees that have no manager.

SELECT Count(EmployeeID) [Count Employees without manager] FROM Employees 
	WHERE ManagerID IS NULL
------------------------------------------------------------------------------------------------------------------------------------------------------------


-- 19 Write a SQL query to find all departments and the average salary for each of them.

SELECT ROUND(AVG(e.Salary),2) as [Average Salary], 
	d.Name as Department
	FROM Employees e 
		JOIN Departments d
			ON e.DepartmentID = d.DepartmentID
GROUP BY e.DepartmentID, d.Name
ORDER BY [Average Salary]


------------------------------------------------------------------------------------------------------------------------------------------------------------

-- 20 Write a SQL query to find the count of all employees in each department and for each town.

SELECT COUNT(EmployeeId) as [Count Employees],
	DepartmentID,
	t.Name as City
	FROM Employees e	
		JOIN Addresses a
			ON e.AddressID = a.AddressID	
		JOIN Towns t
			ON a.TownID = t.TownID
GROUP BY DepartmentID, t.Name
Order BY t.Name


------------------------------------------------------------------------------------------------------------------------------------------------------------

