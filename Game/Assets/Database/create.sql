CREATE TABLE /*"SinglePlayer"*/ "Scores" (
	"Name"	TEXT NOT NULL,
	"Datetime"	REAL NOT NULL,
	"Score"	INTEGER NOT NULL,
	"Level"	INTEGER NOT NULL,
	PRIMARY KEY("Name","Datetime")
);

/* We decided we're not going to store the scores separately, so
   no need for a mutiplayer table

CREATE TABLE "MultiPlayer" (
	"Name"	TEXT NOT NULL,
	"Datetime"	REAL NOT NULL,
	"Score"	INTEGER NOT NULL,
	"Level"	INTEGER NOT NULL,
	PRIMARY KEY("Name","Datetime")
);
 */

CREATE TABLE "Admin" (
	"Username"	TEXT NOT NULL,
	"Hash"	TEXT NOT NULL,
	PRIMARY KEY("Username")
);
