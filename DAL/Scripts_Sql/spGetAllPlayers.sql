Create PROCEDURE [dbo].spGetAllPlayers
   AS
BEGIN
	
	Select 
	PlayerId,
	FirstName,
	MiddleName,
	LastName,
	Age
	from Players

END
GO
