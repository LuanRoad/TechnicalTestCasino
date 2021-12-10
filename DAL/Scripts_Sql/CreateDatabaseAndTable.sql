
Create database Casino

Create Table Players(
	PlayerId int not null primary key identity(1,1),
	FirstName varchar(50),
	MiddleName varchar(50),
	LastName varchar(50),
	Age int
)


