Create Database Masivian
Go

Use Masivian
Go

Create Table [Status]
(
	ID uniqueidentifier primary key default newid(),
	Name varchar(10)
)

Create Table [Roulettes]
(
	ID uniqueidentifier primary key default newid(),
	StateId uniqueidentifier not null foreign key references [Status](ID)
)

Create Table [Bets]
(
	ID uniqueidentifier primary key default newid(),
	[Money] int not null,
	constraint Money_Ck check ([Money] between 1 and 10000),
	UserId uniqueidentifier not null,
	Prize int,
	constraint Prize_Ck check (Prize between 0 and 50000),
	RouletteId uniqueidentifier not null foreign key references Roulettes(ID),
	StateId uniqueidentifier not null foreign key references [Status](ID)
)