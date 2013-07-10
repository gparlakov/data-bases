-- 1 Write a SQL query to find the names and salaries of the employees that take the minimal salary in the company. Use a nested SELECT statement.

SELECT e.FirstName, e.LastName, e.Salary from Employees e
where e.Salary IN
(SELECT MIN(SALARY) from Employees)

------------------------------------------------------------------------------------------------------------------------------------------------------------


-- 2 Write a SQL query to find the names and salaries of the employees that have a salary that is up to 10% higher than the minimal salary for the company.

SELECT e.FirstName, e.LastName, e.Salary from Employees e
where e.Salary <=
(SELECT MIN(SALARY) from Employees) * 1.1
------------------------------------------------------------------------------------------------------------------------------------------------------------

-- 3 Write a SQL query to find the full name, salary and department of the employees that take the minimal salary in their department. Use a nested SELECT statement.

SELECT e.FirstName, e.LastName, e.Salary, e.DepartmentID from Employees e
where e.Salary =
	(SELECT MIN(SALARY) from Employees d WHERE d.DepartmentID = e.DepartmentID )
	
	
------------------------------------------------------------------------------------------------------------------------------------------------------------



-- 4 Write a SQL query to find the average salary in the department #1.

SELECT AVG(SALARY) as [Average Salary] 
from Employees d WHERE d.DepartmentID = 1

------------------------------------------------------------------------------------------------------------------------------------------------------------


-- 5 Write a SQL query to find the average salary  in the "Sales" department.

SELECT AVG(SALARY) as [Average Salary in Sales] 
from Employees d WHERE d.DepartmentID =
(Select DepartmentID FROM Departments Where Name = 'sales')

------------------------------------------------------------------------------------------------------------------------------------------------------------


-- 6 Write a SQL query to find the number of employees in the "Sales" department.

SELECT Count(EmployeeID) as [Count Employees in Sales] 
from Employees e WHERE e.DepartmentID =
(Select DepartmentID FROM Departments Where Name = 'sales')

------------------------------------------------------------------------------------------------------------------------------------------------------------


-- 7 Write a SQL query to find the number of all employees that have manager.


------------------------------------------------------------------------------------------------------------------------------------------------------------

-- 8 Write a SQL query to find the number of all employees that have no manager.

SELECT Count(EmployeeID)  FROM Employees 
	WHERE ManagerID IS NOT NULL
------------------------------------------------------------------------------------------------------------------------------------------------------------


-- 9 Write a SQL query to find all departments and the average salary for each of them.

SELECT ROUND(AVG(e.Salary),2) as [Average Salary], 
	d.Name as Department
	FROM Employees e 
		JOIN Departments d
			ON e.DepartmentID = d.DepartmentID
GROUP BY e.DepartmentID, d.Name
ORDER BY [Average Salary]


------------------------------------------------------------------------------------------------------------------------------------------------------------


------------------------------------------------------------------------------------------------------------------------------------------------------------


------------------------------------------------------------------------------------------------------------------------------------------------------------

------------------------------------------------------------------------------------------------------------------------------------------------------------