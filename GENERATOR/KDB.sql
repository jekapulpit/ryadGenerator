
drop database D_GENERATOR
create database D_GENERATOR;
use [D_GENERATOR];


create table USERS 
(  USERNAME  nvarchar(20) constraint USERNAME_PK  primary key,  
     PASSWORD_D  nvarchar(50),
	 USERTYPE  nvarchar(50) check(USERTYPE in ('NEW', 'MEDIUM', 'PRO')),
	 
	 ); --будет использована хеш-функция для пароля


 create table RYADS 
( 
  RYADID  int  constraint RYADID_PK  primary key,     
  RYADTYPE  char(10), 
  CREATOR  nvarchar(20) constraint RYADS_CREATOR_FK foreign key         
                      references USERS(USERNAME),  	
  IMGURL nvarchar(100),
  CTEATED     date,                                     
);

create table TEST
(
	MARK int,
	Passed date,
	passer nvarchar(20) constraint passer_FK foreign key         
                      references USERS(USERNAME),
)


