/*Create a database with two tables: Persons(Id(PK), FirstName, LastName, SSN)
 and Accounts(Id(PK), PersonId(FK), Balance). 
 Insert few records for testing. 
 Write a stored procedure that selects the full names of all persons.
*/

USE [master]
GO

CREATE DATABASE [Transact-SQL]
Go

use [Transact-SQL]

CREATE TABLE Persons(
	Id int IDENTITY,
	FirstName nvarchar(30),
	LaststName nvarchar(30),
	SSN char(10) NOT NULL,
	CONSTRAINT PK_Persons PRIMARY KEY(ID),
)

GO

CREATE TABLE Accounts(
	Id int IDENTITY,
	PersonId int,
	Balance money,
	CONSTRAINT Pk_Account PRIMARY KEY(ID),
	CONSTRAINT Fk_Account_PersonId FOREIGN KEY(PersonId)
		REFERENCES Persons(Id) 
)

GO
/** inserts few persons***/
INSERT INTO Persons(FirstName, LaststName, SSN)
VALUES (N'George', N'Georgiev', '2013070800'),
		(N'Parvan', N'Ivanov', '2013070801'),
	(N'Mariq', N'Georgieva', '2013070802'),	
	(N'Hlorofil', N'Ivanow', '2013070803'),
	(N'Kiro', N'Petrov', '2013070804'),
	(N'Mariq', N'Ivanova', '2013070805'),
	(N'Hera', N'Hlamidieva', '2013070806');
GO

INSERT INTO Accounts(PersonId, Balance)
/* takes the newly created persons id-s in a table and puts some random values in them
ant puts the result into accounts */
Select ID, convert(int,(RAND(ID)*1000000000)) % 10000  From Persons

GO

/** procedure for getting full names**/
CREATE PROC dbo.usp_SelectPersonFullName 
AS 
	SELECT p.FirstName + ' ' + p.LaststName as [Person Name]
	From Persons p
GO

exec dbo.usp_SelectPersonFullName

