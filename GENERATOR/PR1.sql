use  [E:\ЛАБОРАТОРНЫЕ И КОМПЛЕКТУЮЩИЕ\КУРСАЧ\GENERATOR\GENERATOR\DATABASE1.MDF]
 
 
  create PROCEDURE Addinnewbee
   @id  INT,
   @type CHAR (10) ,
   @us  NVARCHAR (20) ,
   @picture   NVARCHAR (100)  
   

AS
	insert into RYADS (RYADID, RYADTYPE, CREATOR, IMGURL, CTEATED) values(@id, 'usual', @us, @picture, default )