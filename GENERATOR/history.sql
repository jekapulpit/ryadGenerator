use  [E:\ЛАБОРАТОРНЫЕ И КОМПЛЕКТУЮЩИЕ\КУРСАЧ\GENERATOR\GENERATOR\DATABASE1.MDF]
 
 
  create PROCEDURE history
   @current nvarchar(20)
AS
	select * from RYADS 
		where	CREATOR  = @current