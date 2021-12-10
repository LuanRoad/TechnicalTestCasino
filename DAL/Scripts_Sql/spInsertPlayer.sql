Alter PROCEDURE [dbo].spInsertPlayer
   @FirstName varchar(50),
   @MiddleName varchar(50),
   @LastName varchar(50),
   @Age Int = 0
   AS
BEGIN

Insert into Players (FirstName,MiddleName,LastName,Age) 
values (@FirstName, @MiddleName, @LastName, @Age)

END
GO


--exec spInsertPlayer 'Angel','Rodriguez','Angeles',32
--exec spInsertPlayer 'Issael','Mazcorro','',35
--exec spInsertPlayer 'Daniel','Sandoval','xx',26
--exec spInsertPlayer 'Pepe',null,null,0
