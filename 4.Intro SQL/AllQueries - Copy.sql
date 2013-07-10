--4 AllDepartments
SELECT * FROM [TelerikAcademy].[dbo].[Departments]
/*----------------------------------------------*/

--5 AllDepartmentNames
SELECT Name FROM [TelerikAcademy].[dbo].[Departments]
/*----------------------------------------------*/


-- 6 AllEmployee salaries with last name
SELECT LastName, Salary FROM [TelerikAcademy].[dbo].[Employees]
/*----------------------------------------------*/



-- 7 All Employee Full Names
SELECT FirstName, MiddleName, LastName FROM [TelerikAcademy].[dbo].[Employees]
/*----------------------------------------------*/



-- 8 Generates all emails based ot first and last name
SELECT FirstName + '.' + LastName + '@telerik.com' as 'Full Email Addresses'
	FROM [TelerikAcademy].[dbo].[EmpLoyees]	
/*----------------------------------------------*/	
	
	
	
-- 9 All distinct salaries
SELECT DISTINCT Salary From [TelerikAcademy].[dbo].[Employees]
/*----------------------------------------------*/



-- 10 Write a SQL query to find all information about the employees whose job title is -“Sales Representative“.

SELECT *  FROM [TelerikAcademy].[dbo].[Employees]
where JobTitle = 'Sales Representative'

/*----------------------------------------------*/

/*THE full information can be taken from all tables joined as shown below:*/
/*First set the columns from which table and as name */
SELECT e.[EmployeeID]
      ,e.[FirstName]
      ,e.[LastName]
      ,e.[MiddleName]
      ,e.[JobTitle]
      ,e.[HireDate]
      ,e.[Salary]
      ,a.[AddressText] as Address
	  ,d.[Name] as Department
	  ,m.[FirstName] + ' ' + m.LastName as ManagerName
	  ,t.[Name] as Town
/** Then give the tables and each join (returns new table) will be joined with the next one**/
  FROM [TelerikAcademy].[dbo].[Employees] as e
	JOIN [TelerikAcademy].[dbo].[Departments] as d
		ON e.DepartmentID = d.DepartmentID
			JOIN [TelerikAcademy].[dbo].[Employees] m
				ON e.ManagerID = m.EmployeeID
					JOIN [TelerikAcademy].[dbo].[Addresses] a
						ON e.AddressID = a.AddressID
							JOIN [TelerikAcademy].[dbo].[Towns] as t
								ON a.TownID = t.TownID
								

								
								
/*-------------------------------------------------------------------------------------------*/								


-- 11 Write a SQL query to find the names of all employees whose first name starts with "SA".

SELECT e.[FirstName] + ' ' + e.[FirstName] + ' ' + e.[LastName]	as [Full Name] 
  FROM [TelerikAcademy].[dbo].[Employees] as e
	WHERE FirstName LIKE 'Sa%'
/*----------------------------------------------*/	
	
	
	
--12 Write a SQL query to find the names of all employees whose last name contains "ei".

SELECT e.[FirstName] + ' ' + e.[FirstName] + ' ' + e.[LastName]	as [Full Name] 
  FROM [TelerikAcademy].[dbo].[Employees] as e
	WHERE LastName LIKE '%ei%'
/*----------------------------------------------*/	
	
	

-- 13 Write a SQL query to find the salary of all employees whose salary is in the range [20000…30000].

SELECT e.[Salary], e.[FirstName] + ' ' + e.[FirstName] + ' ' + e.[LastName]	as [Full Name] 
  FROM [TelerikAcademy].[dbo].[Employees] as e
	WHERE Salary BETWEEN 20000 AND 30000
		ORDER BY e.Salary, e.FirstName, e.LastName
/*----------------------------------------------*/	

								
-- 14 Write a SQL query to find the names of all employees whose salary is 25000, 14000, 12500 or 23600.

SELECT e.[Salary], e.[FirstName] + ' ' + e.[FirstName] + ' ' + e.[LastName]	as [Full Name] 
  FROM [TelerikAcademy].[dbo].[Employees] as e
	WHERE Salary IN (25000, 14000, 12500, 23600)
		ORDER BY e.Salary, e.FirstName, e.LastName
/*----------------------------------------------*/	
		
-- 15 Write a SQL query to find all employees that do not have manager.

SELECT e.[ManagerID], e.[FirstName] + ' ' + e.[FirstName] + ' ' + e.[LastName] as [Full Name] 
  FROM [TelerikAcademy].[dbo].[Employees] as e
	WHERE e.[ManagerID] IS NULL
/*----------------------------------------------*/	

	
--16 Write a SQL query to find all employees that have salary more than 50000. Order them in decreasing order by salary.

SELECT e.[Salary], e.[FirstName] + ' ' + e.[FirstName] + ' ' + e.[LastName]	as [Full Name] 
  FROM [TelerikAcademy].[dbo].[Employees] as e
	WHERE Salary > 50000
		ORDER BY e.Salary desc
/*----------------------------------------------*/

								
--17 Write a SQL query to find the top 5 best paid employees.

SELECT TOP 5 e.[Salary], e.[FirstName] + ' ' + e.[FirstName] + ' ' + e.[LastName]	as [Full Name] 
  FROM [TelerikAcademy].[dbo].[Employees] as e
	WHERE Salary > 50000
		ORDER BY e.Salary desc

/*----------------------------------------------*/	

---18 Write a SQL query to find all employees along with their address. Use inner join with ON clause.

SELECT e.[FirstName] + ' ' + e.[FirstName] + ' ' + e.[LastName]	as [Full Name],
		t.[Name] as [Town],
		a.[AddressText] as [Address]
  FROM [TelerikAcademy].[dbo].[Employees] as e
	JOIN [TelerikAcademy].[dbo].[Addresses] as a
		ON e.AddressID = a.AddressID
		JOIN [TelerikAcademy].[dbo].[Towns] as t
			ON a.TownID = t.TownID
			
/*----------------------------------------------*/	


---19 Write a SQL query to find all employees and their address. Use equijoins (conditions in the WHERE clause).

/*First set the columns from which table and as name */
SELECT e.[FirstName] + ' ' + e.[FirstName] + ' ' + e.[LastName]	as [Full Name],
		t.Name as Town,
		a.[AddressText] as Address
/* Then specifiy the tables (and their aliases i.e. e === Eployees)from which to take the columns */
  FROM [TelerikAcademy].[dbo].[Employees] as e, 
		[TelerikAcademy].[dbo].[Addresses] as a, 
		[TelerikAcademy].[dbo].[Towns] as t

		/*Check the cvondition and only show where true*/
		WHERE e.AddressID = a.AddressID AND a.TownID = t.TownID
		
/*----------------------------------------------*/	


---20 Write a SQL query to find all employees along with their manager.
 
 SELECT e.[FirstName] + ' ' + e.[FirstName] + ' ' + e.[LastName]	as [Full Name],
		m.[FirstName] + ' ' + m.[FirstName] + ' ' + m.[LastName]	as [Manager Full Name]

	FROM [TelerikAcademy].[dbo].[Employees] as e 
		LEFT JOIN [TelerikAcademy].[dbo].[Employees] as m
			ON e.ManagerID = m.EmployeeID
			
/*----------------------------------------------*/	


---21 Write a SQL query to find all employees, along with their manager and their address. Join the 3 tables: Employees e, Employees m and Addresses a.

SELECT e.[FirstName] + ' ' + e.[FirstName] + ' ' + e.[LastName]	as [Full Name],
		a.AddressText as [Emplyee Address],
		m.[FirstName] + ' ' + m.[FirstName] + ' ' + m.[LastName]	as [Manager Full Name]
		

	FROM [TelerikAcademy].[dbo].[Employees] as e 
		LEFT JOIN [TelerikAcademy].[dbo].[Employees] as m
			ON e.ManagerID = m.EmployeeID
				JOIN [TelerikAcademy].[dbo].[Addresses] as a
					ON e.AddressID = a.AddressID
					
/*----------------------------------------------*/	

---22 Write a SQL query to find all departments and all region names (?? no region names - used addresses instead ), country names(!no country names used Project names instead) and city names as a single list. Use UNION.


SELECT d.Name as AllNames
	FROM [TelerikAcademy].[dbo].[Departments] as d
UNION
SELECT t.Name
	FROM [TelerikAcademy].[dbo].[Towns] as t
UNION	
SELECT p.Name
	FROM [TelerikAcademy].[dbo].[Projects] as p
UNION
SELECT a.AddressText
	FROM [TelerikAcademy].[dbo].[Addresses] as a
	
/*----------------------------------------------*/	


---23 Write a SQL query to find all the employees and the manager for each of them along with the employees that do not have manager. User right outer join. Rewrite the query to use left outer join.
/*RIGHT OUTER JOIN*/
SELECT 
	e.LastName as Employee,
	m.LastName as Manager

	FROM Employees as m
		RIGHT JOIN Employees as e
			ON e.ManagerID = m.EmployeeID


/* using LEFT OUTER JOIN*/
SELECT 
	e.LastName as Employee,
	m.LastName as Manager

	FROM Employees as e
		LEFT JOIN Employees as m
			ON e.ManagerID = m.EmployeeID


/*----------------------------------------------*/	


---24 Write a SQL query to find the names of all employees from the departments "Sales" and "Finance" whose hire year is between 1995 and 2000.

SELECT e.[FirstName] + ' ' + e.[LastName] as EmployeeName,
		e.HireDate,
		d.Name as Department
	FROM [TelerikAcademy].[dbo].[Employees] as e
		JOIN [TelerikAcademy].[dbo].[Departments] as d
			ON  e.DepartmentID = d.DepartmentID
			WHERE d.Name IN ('Sales','Finance') 
				--AND e.HireDate BETWEEN '1/1/1994' AND '1/1/2005' -- no people in range 1995 - 2000
				-- AND e.HireDate BETWEEN '1/1/1994' AND '12/31/2000'
				
/*----------------------------------------------*/	
