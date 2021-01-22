Create Database Masivian
Go

Use Masivian
Go

Create Table [Status]
(
	ID uniqueidentifier primary key default newid(),
	IsOpen bit not null,
	[Name] varchar(10)
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

INSERT INTO [Status] (ID, [IsOpen], [Name])
VALUES
	('42290071-BD11-4089-9483-45612311FF52', 1, 'Open'),
	('D38D2437-92B8-4C0C-BDE3-8C1029403F03', 0, 'Close');