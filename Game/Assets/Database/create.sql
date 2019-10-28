CREATE TABLE "SinglePlayer" (
	"Name"	TEXT NOT NULL,
	"Datetime"	REAL NOT NULL,
	"Score"	INTEGER NOT NULL,
	"Level"	INTEGER NOT NULL,
	PRIMARY KEY("Name","Datetime")
);

CREATE TABLE "MultiPlayer" (
	"Name"	TEXT NOT NULL,
	"Datetime"	REAL NOT NULL,
	"Score"	INTEGER NOT NULL,
	"Level"	INTEGER NOT NULL,
	PRIMARY KEY("Name","Datetime")
);

CREATE TABLE "Admin" (
	"Username"	TEXT NOT NULL,
	"Hash"	TEXT NOT NULL,
	PRIMARY KEY("Username")
);
