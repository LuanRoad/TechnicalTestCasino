CREATE PROCEDURE [dbo].spUpdatePlayer
	@PlayerId int,
	@FirstName varchar(50),
	@MiddleName varchar(50),
	@LastName varchar(50),
	@Age Int = 0
	AS
BEGIN

	Update Players 
	set 
	FirstName = @FirstName,
	MiddleName = @MiddleName,
	LastName = @LastName,
	Age = @Age 
	where PlayerId = @PlayerId
END
GO
