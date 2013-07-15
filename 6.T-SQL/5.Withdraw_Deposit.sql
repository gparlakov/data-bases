/*
Add two more stored procedures WithdrawMoney( AccountId, money) 
and DepositMoney (AccountId, money) that operate in transactions.
*/

use [Transact-SQL]
GO


IF (OBJECT_ID('V_BALANCE', 'V') IS NOT NULL) DROP VIEW V_Balance
GO

		/*Creates a View for demonstration of effect*/
		CREATE VIEW V_Balance 
		AS
		SELECT Balance
		FROM Persons p 
			JOIN accounts a 
			ON p.Id = a.PersonId 
		GO

IF (OBJECT_ID('usb_WithdrawMoney') IS NOT NULL) DROP PROC usb_WithdrawMoney
GO

IF (OBJECT_ID('usb_DepositMoney') IS NOT NULL) DROP PROC usb_DepositMoney
GO

CREATE PROC usb_WithdrawMoney(
	@AccountId int,
	@money money)
AS
	UPDATE Accounts
	SET Balance = Balance - @money
	WHERE @AccountId = @AccountId
GO

CREATE PROC usb_DepositMoney(
	@AccountId int,
	@money money)
AS
	UPDATE Accounts
	SET Balance = Balance + @money
	WHERE @AccountId = @AccountId
GO




 /**Before*/
 SELECT * FROM V_Balance

 Exec usb_WithdrawMoney 1, 100 

/*AFTER**/
 SELECT * FROM V_Balance

 EXEC usb_DepositMoney 1, 1000 

 SELECT * FROM V_Balance