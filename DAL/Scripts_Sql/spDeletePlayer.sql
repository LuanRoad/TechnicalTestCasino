Create PROCEDURE [dbo].spDeletePlayer
@PlayerId int
   AS
BEGIN
	
	delete from Players
	where PlayerId = @PlayerId
END
GO