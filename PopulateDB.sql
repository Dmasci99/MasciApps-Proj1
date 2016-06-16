
DROP TABLE MATCHES;
DROP TABLE TEAMS;
DROP TABLE SPORTS;

CREATE TABLE Sports (
	SportID INT NOT NULL IDENTITY PRIMARY KEY,
	Name NVARCHAR(50) NOT NULL
);

INSERT INTO Sports
VALUES ('Basketball'), ('Volleyball'), ('Soccer'), ('Football');

SELECT * FROM Sports;

CREATE TABLE Teams (
	TeamID INT NOT NULL IDENTITY PRIMARY KEY,
	SportID INT NOT NULL,
	Name NVARCHAR(50) NOT NULL,
	Country NVARCHAR(50),
	City NVARCHAR(50),
	Logo IMAGE,
	FOREIGN KEY (SportID) REFERENCES Sports (SportID)
);

INSERT INTO Teams
VALUES (1, 'Toronto Raptors', 'Canada', 'Toronto', NULL),
	   (1, 'Chicago Bulls', 'USA', 'Chicago', NULL),
	   (1, 'BBall Team', 'Canada', 'Vancouver', NULL),
	   (2, 'VBall Team', 'USA', 'New York', NULL);

SELECT * FROM Teams;

CREATE TABLE Matches (
	MatchID INT NOT NULL IDENTITY PRIMARY KEY,
	SportID INT NOT NULL,
	HomeTeamID INT NOT NULL,
	AwayTeamID INT NOT NULL,
	Winner INT,
	Name NVARCHAR(110),
	Date DATETIME,
	SpecCount INT,
	HomeTeamScore INT,
	AwayTeamScore INT,
	CONSTRAINT fk_homeTeam FOREIGN KEY (HomeTeamID) REFERENCES Teams (TeamID),
	CONSTRAINT fk_awayTeam FOREIGN KEY (AwayTeamID) REFERENCES Teams (TeamID),
	FOREIGN KEY (SportID) REFERENCES Sports (SportID)
);

INSERT INTO Matches 
VALUES (1, 1, 2, 1, 'Raptors vs. Bulls', GETDATE(), 1923, 129, 108),
	   (1, 1, 3, 3, 'Raptors vs. BBall Team', GETDATE(), 2923, 119, 120),
	   (1, 3, 2, 2, 'BBall Team vs. Bulls', GETDATE(), 1923, 89, 106);

SELECT * FROM Matches;