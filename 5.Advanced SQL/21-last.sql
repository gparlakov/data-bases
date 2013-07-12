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
 
 
 -- 31 Start a database transaction and drop the table EmployeesProjects. Now how you could restore back the lost table data?
 -- with backup only