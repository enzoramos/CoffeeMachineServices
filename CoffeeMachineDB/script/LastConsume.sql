CREATE TABLE LastConsume (
	Id INTEGER primary key NOT NULL,
	Drink INTEGER  NOT NULL FOREIGN KEY REFERENCES Drink(Id),
	Uid VARCHAR NOT NULL,
	SugarLevel INTEGER NOT NULL,
	UseMug BIT NOT NULL
);