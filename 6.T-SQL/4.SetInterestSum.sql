/*
Create a stored procedure that uses the function from the previous example
 to give an interest to a person's account for one month. 
It should take the AccountId and the interest rate as parameters


CREATE FUNCTION ufn_CalculateSum
	(@sum money,
	@interestOnYear float = 10.5,
	@numbermonths int)


*/

use [Transact-SQL]
GO

/*Creates a View for demonstration of effect*/

IF (OBJECT_ID('V_BALANCE', 'V') IS NOT NULL) DROP VIEW V_Balance
GO

GO

CREATE VIEW V_Balance 
AS
SELECT Balance
FROM Persons p 
	JOIN accounts a 
	ON p.Id = a.PersonId 
GO


IF OBJECT_ID('usp_AddOneMonthInterest') is NOT NULL DROP PROC usp_AddOneMonthInterest
GO

/** Updates account - changes ammount of money on executing!!! **/
CREATE PROC usp_AddOneMonthInterest
	(@accountId int,
	@interestOnYear float)
AS
	DECLARE @numberMonths int = 1 /* to avoid magic number*/
	UPDATE Accounts	
	SET Balance = (
		SELECT Balance = (Balance + dbo.ufn_CalculateSum(Balance, @interestOnYear, @numberMonths))
		FROM Persons p 
			JOIN accounts a 
			ON p.Id = a.PersonId 
		WHERE p.Id = @accountId
		)
	WHERE Accounts.Id = @accountId
GO

 /**Before*/
 SELECT * FROM V_Balance

EXEC dbo.usp_AddOneMonthInterest 1 ,12.5

/*AFTER**/
 SELECT * FROM V_Balance
