/*
Create a function that accepts as parameters – sum, yearly interest
 rate and number of months. It should calculate and return the new sum. 
Write a SELECT to test whether the function works as expected
*/


use [Transact-SQL]
IF OBJECT_ID('ufn_CalculateSum') is NOT NULL DROP FUNCTION ufn_CalculateSum
GO

CREATE FUNCTION ufn_CalculateSum
	(@sum money,
	@interestOnYear float = 10.5,
	@numbermonths int)
RETURNS
	money
AS
BEGIN
	DECLARE @interestOnMonth float
	SET @interestOnMonth = @interestOnYear / 12;
	RETURN @numbermonths * @interestOnMonth * @sum / 100
END

GO

SELECT dbo.ufn_CalculateSum(1000, 12.5, 3)
