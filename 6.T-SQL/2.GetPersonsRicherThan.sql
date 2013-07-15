/*
Create a stored procedure that accepts a number as a parameter 
and returns all persons who have more money in their accounts than
 the supplied number.
*/


use [Transact-SQL]

IF OBJECT_ID('usp_GetPersonsRicherThan', 'U') is NOT NULL DROP PROC usp_GetPersonsRicherThan
GO

CREATE PROC usp_GetPersonsRicherThan
(@money money = 1000)
AS
SELECT p.LaststName,
	a.Balance
FROM Persons p 
	JOIN Accounts a ON p.Id = a.PersonId
	WHERE a.Balance > @money

GO

EXEC usp_GetPersonsRicherThan 2000


