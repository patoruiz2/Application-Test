CREATE DATABASE TestDb

--select * from [dbo].[Users]

CREATE TABLE Users(
	Id int,
	Name varchar(20),
	LastName varchar(20),
	Age int,
	Dni int
)

INSERT INTO Users(
	Id,
	Name,
	LastName,
	Age,
	Dni
)
VALUES(
	1,
	'Patricio',
	'Ruiz',
	22,
	41579698
),
(
	2,
	'Roberto',
	'Gomez',
	28,
	35586878
)
