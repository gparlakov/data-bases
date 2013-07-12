-- 22 Write SQL statements to insert in the Users table the names of all
-- employees from the Employees table. Combine the first and last names as 
-- a full name. For username use the first letter of the first name + the 
-- last name (in lowercase). Use the same for the password, and NULL for last login time.

INSERT INTO Users(Username, FullName, LastLoginDate) 
SELECT Lower(substring(e.FirstName, 1,3) + e.Lastname) as Username,
		e.FirstName + ' ' + e.Lastname as FullName,
		NULL as LastLoginDate
 FROM Employees e

------------------------------------------------------------------------------------------------------------------------------------------------------------


-- 25 Write a SQL query to display the average employee salary by department and job title.

SELECT AVG(Salary) as [Average Salary],
	 DepartmentID ,
	 JobTitle 
FROM Employees e 	
GROUP BY DepartmentID, JobTitle
	ORDER BY DepartmentID

------------------------------------------------------------------------------------------------------------------------------------------------------------
 


-- 26 Write a SQL query to display the minimal employee salary by department and job title along with the name of some of the employees that take it.

SELECT MIN(Salary) as [Min Salary],
	 DepartmentID ,
	 JobTitle ,
	 MIN(e.LastName) -- or max(e.lastname) or just anyone
FROM Employees e 	
GROUP BY DepartmentID, JobTitle
	ORDER BY DepartmentID


------------------------------------------------------------------------------------------------------------------------------------------------------------
 -- 27 Write a SQL query to display the town where maximal number of employees work.
 
DECLARE @maxCount int;

Select @maxCount = max(EmployeeCount) from 
(SELECT COUNT(e.EmployeeID)	as EmployeeCount,
t.Name
FROM Employees e
	JOIN Addresses a
ON e.AddressID = a.AddressID
	JOIN Towns t
On a.TownID = t.TownID
GROUP BY T.Name) q1

SELECT q2.Name from 
(SELECT COUNT(e.EmployeeID)	as EmployeeCount,
	t.Name
FROM Employees e
	JOIN Addresses a
ON e.AddressID = a.AddressID
	JOIN Towns t
On a.TownID = t.TownID
GROUP BY T.Name) q2
WHERE q2.EmployeeCount = @maxCount
 ------------------------------------------------------------------------------------------------------------------------------------------------------
 
 -- 28 Write a SQL query to display the number of managers from each town.
SELECT COUNT(DISTINCT m.LastName) as [Managers Count],
	t.Name as Town
FROM Employees m
	JOIN Employees e
	ON m.EmployeeID = e.ManagerID
	JOIN Addresses a
	ON m.AddressID = a.AddressID
	JOIN Towns t
	ON a.TownID = t.TownID
GROUP BY t.Name
ORDER BY [Managers Count] desc
 
 ------------------------------------------------------------------------------------------------------------------------------------------------------
 -- 31 Start a database transaction and drop the table EmployeesProjects. Now how you could restore back the lost table data?
 -- with backup only