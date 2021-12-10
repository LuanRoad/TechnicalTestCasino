Create PROCEDURE [dbo].spGetAPlayer
@PlayerId int
   AS
BEGIN
	
	Select 
	PlayerId,
	FirstName,
	MiddleName,
	LastName,
	Age
	from Players
	where PlayerId = @PlayerId
END
GO