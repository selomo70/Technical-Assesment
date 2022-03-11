Create Database AutoEDITest;



Use AutoEDITest
Go

--Create a Tables--

Create Table AccessLevel(
Id  int  identity(1,1) not null unique, 
Name varchar(255), 
Description varchar(255),
CONSTRAINT PK_AccessLevel PRIMARY KEY (Id)
);

Create Table Users(
Id  int  identity(1,1) not null unique, 
Username varchar(255), 
Password varchar(255),
AccessLevelId int, 
FirstName varchar(255), 
LastName varchar(255), 
LastLogin DateTime, 
Registered bit
CONSTRAINT PK_User PRIMARY KEY (Id),
CONSTRAINT  PK_User_AccessLevel FOREIGN KEY (AccessLevelId) REFERENCES AccessLevel(Id)
);

Create Table Communication(
Id  int  identity(1,1) not null unique, 
Action varchar(255) ,
Result varchar(255), 
CreatedAt DateTime,
CONSTRAINT PK_Communication PRIMARY KEY (Id),
);

insert into AccessLevel
Values(
'ADMIN', 'Super User'),
('USER', 'Standard User'),
('READONLY',  'Read Only User');

SELECT * FROM AccessLevel;

CREATE VIEW Communication_View AS
SELECT Action, Result, CreatedAt
FROM Communication
