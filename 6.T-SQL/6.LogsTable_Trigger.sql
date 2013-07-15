/*
Create another table – Logs(LogID, AccountID, OldSum, NewSum). 
Add a trigger to the Accounts table that enters a new entry into 
the Logs table every time the sum on an account changes.
*/

use [Transact-SQL]

IF (OBJECT_ID('Logs') IS NULL)
BEGIN
	CREATE TABLE Logs(
		LogID int IDENTITY,
		AccountID int NOT NULL,
		OldSum money NOT NULL,
		NewSum money NOT NULL,
		CONSTRAINT Pk_Logs PRIMARY KEY(LogID)
	)
END
GO

IF (OBJECT_ID('tr_OnAccountBalanceChange') IS NOT NULL) DROP TRIGGER tr_OnAccountBalanceChange
GO

CREATE TRIGGER tr_OnAccountBalanceChange
ON Accounts FOR UPDATE 
AS
DECLARE 	
	@sumBefore int,  
	@accId int,  
	@sumAfter int
	
	/****** SELECTS the row that the inserted is different than 
	deleted = and takes the old new and accountId of this row *******/
	SELECT @sumBefore = d.Balance,
		@sumAfter = i.Balance,
		@accId = i.PersonId
	From deleted d 
		JOIN inserted i   
		ON d.Id = i.Id
	WHERE d.Balance <> i.Balance
	
INSERT INTO LOGS (AccountID, OldSum, NewSum)
	VALUES (@accId, @sumBefore, @sumAfter)
GO

Select *, 'first time' from dbo.Accounts
SELECT l.LogID,l.OldSum, l.NewSum , 'last row' FROM dbo.Logs l WHERE l.LogID in (SELECT max(Logs.LogID) from Logs)

EXEC [dbo].[usb_DepositMoney] 1, 100

Select *, 'second time' from dbo.Accounts
SELECT l.LogID,l.OldSum, l.NewSum , 'last row' FROM dbo.Logs l WHERE l.LogID in (SELECT max(Logs.LogID) from Logs)


EXEC [dbo].[usb_DepositMoney] 1, 100

Select *, 'third time' from dbo.Accounts
SELECT l.LogID,l.OldSum, l.NewSum , 'last row' FROM dbo.Logs l WHERE l.LogID in (SELECT max(Logs.LogID) from Logs)

