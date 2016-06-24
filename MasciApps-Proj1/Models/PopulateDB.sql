
DROP TABLE MATCHES;
DROP TABLE TEAMS;
DROP TABLE SPORTS;

CREATE TABLE Sports (
	SportID INT NOT NULL IDENTITY PRIMARY KEY,
	Name NVARCHAR(50) NOT NULL
);

INSERT INTO Sports
VALUES ('Basketball'), ('Hockey'), ('Soccer'), ('Football');

SELECT * FROM Sports;

CREATE TABLE Teams (
	TeamID INT NOT NULL IDENTITY PRIMARY KEY,
	SportID INT NOT NULL,
	Name NVARCHAR(50) NOT NULL,
	Country NVARCHAR(50),
	City NVARCHAR(50),
	FOREIGN KEY (SportID) REFERENCES Sports (SportID)
);

INSERT INTO Teams
VALUES (1, 'Toronto Raptors', 'Canada', 'Toronto'),
	   (1, 'Chicago Bulls', 'USA', 'Chicago'),
	   (1, 'Miami Heat', 'USA', 'Miami'),
	   (1, 'Cleveland Cavaliers', 'USA', 'Cleveland'),
	   (2, 'Pittsburgh Penguins', 'USA', 'Pittsburgh'),
	   (2, 'Toronto Maple Leafs', 'Canada', 'Toronto'),
	   (2, 'Boston Bruins', 'USA', 'Boston'),
	   (2, 'Montréal Canadiens', 'Canada', 'Montréal'),
	   (3, 'Manchester United FC', 'England', 'Manchester'),
	   (3, 'Barcelona FC', 'Spain', 'Barcelona'),
	   (3, 'Real Madrid CF', 'Spain', 'Madrid'),
	   (3, 'Manchester City FC', 'England', 'Manchester City'),
	   (4, 'Denver Broncos', 'USA', 'Denver'),
	   (4, 'Green Bay Packers', 'USA', 'Green Bay'),
	   (4, 'New England Patriots', 'USA', 'New England'),
	   (4, 'Dallas Cowboys', 'USA', 'Dallas');

SELECT * FROM Teams;

CREATE TABLE Matches (
	MatchID INT NOT NULL IDENTITY PRIMARY KEY,
	SportID INT NOT NULL,
	HomeTeamID INT NOT NULL,
	AwayTeamID INT NOT NULL,
	Winner INT,
	Name NVARCHAR(110) NOT NULL,
	DateTime DATETIME NOT NULL,
	SpecCount INT,
	HomeTeamScore INT,
	AwayTeamScore INT,
	CONSTRAINT fk_homeTeam FOREIGN KEY (HomeTeamID) REFERENCES Teams (TeamID),
	CONSTRAINT fk_awayTeam FOREIGN KEY (AwayTeamID) REFERENCES Teams (TeamID),
	FOREIGN KEY (SportID) REFERENCES Sports (SportID)
);

INSERT INTO Matches 
VALUES (1, 1, 2, 1, 'Toronto Raptors vs. Chicago Bulls', GETDATE(), 1923, 129, 108),
	   (1, 3, 4, 3, 'Miami Heat vs. Cleveland Cavaliers', GETDATE(), 2923, 119, 120),
	   (2, 5, 6, 5, 'Pittsburgh Penguins vs. Toronto Maple Leafs', GETDATE(), 1923, 2, 1),
	   (2, 7, 8, 7, 'Boston Bruins vs. Montréal Canadiens', GETDATE(), 1923, 5, 2),
	   (3, 9, 10, 9, 'Manchester United FC vs. Barcelona FC', GETDATE(), 1923, 4, 3),
	   (3, 11, 12, 11, 'Real Madrid CF vs. Manchester City FC', GETDATE(), 1923, 1, 0),
	   (4, 13, 14, 13, 'Denver Broncos vs. Green Bay Packers', GETDATE(), 1923, 38, 23),
	   (4, 15, 16, 15, 'New England Patiors vs. Dallas Cowboys', GETDATE(), 1923, 24, 20);

SELECT * FROM Matches;