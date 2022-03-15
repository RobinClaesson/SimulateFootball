# Simulate Fotball
This is a program made for simple simulations of entire seasons of football (soccer for non europeans).
Inspired by a [Numberphile video](https://www.youtube.com/watch?v=Vv9wpQIGZDw)

## What does it do? 
The program uses a basic algoritm to decide how a certrain game will end. 

Every season the program uses that algoritm to let each team play all their home games in a season and calculates the leagu table from the matches. 
It then takes the table and checks for some interesting statistics like most points and most scored goals and keeps track of these records over all simulations.

It does this over and over until it has reached the set number of seasons you enterd. 
The records and stats are then saved into textfiles for you to read. 
All tables and matches from "record holding seasons" are saved to files automaticly. 
It is also possible to save every match played in every seasons, and every seasons table. Though these textfiles grow quite large. 

## Simulation Output 
The program always creates and writes to these files in a folder called "Simulations", each further explained in sections following.
* Sumulation Stats.txt
* Team placements.txt
* Tables From Record Seasons.txt
* Matches From Record Seasons.txt

It can also by choice write to these files, which are just all the simulated games and tables.
* All Tables
* All Matches 

The tables outputted py the program should be easy use as bases for new simulations if desired. 
### Simulation Stats.txt
This is a copy of what is shown in the console after the simulation is complete and prints stats from following categories:
1. Best Performances: Most points, most scored goals etc. 
2. Worst Performances: Least points, most admitted goals etc. 
3. Worst teams in first place: Stats from teams that performed not great but still won the league
4. Best teams in last place: Stats from the team that got last but maby didn't totally suck. 
5. Points needed to win: The number of points needed to win the league, also known as 2nd place + 1 point.
6. Average points per placement: Average point of all positions in the leage.
7. Most league titles: How many times the the top teams won the league.
8. Most league jumbos: How many times the bottom teams got last place. 

### Team placements.txt
This is how many times all teams got every placement in the league, and also their averagement placement. 

### Tables From Record Seasons.txt and Matches From Record Seasons.txt
This is the league table / all the matches from the seasons that holds a record in Simulations Stats.txt.
Its sorted by season number.

## How does it work? 
### What is the simulation based on?
To start of you feed the program a table from a season of your league of choice from a textfile. 
The formatting for this file is explained in more details lower down, but it's basicly just copy/paste from almost every wikipedia page from a given season. 

From this table the program calculates the average goales scored and admitted in a game for every team, this data is whats drives the simulation. 

### How is a game simulated? 
The program takes a teams average scored goals and adds the opponents average admitted goals, and vice versa. This gives a base for the expected number of goals from both teams. 
It then uses the C# built in random to generate the score for each team. 
On average "Team 1" will score this many goals: 

![Average goals for Team 1](https://i.imgur.com/4NaWjKH.png)

### Issues with algoritm
The biggest problem with this algoritm is that i won't give any unexpecedly large victories. The max number of goals a team can make is the avrg scored + opponends avrg admitted rounded upp. A large victory like 8-1 is impossible for most simulations in the program, but is possible in reality (even if unlikely). 

The [Numberphile video](https://www.youtube.com/watch?v=Vv9wpQIGZDw) that inspired me to do this I believe uses poisson distribution to generate scored goals, and this is a superior way to do it. 

One small strength that my algoritm do have vs the poisson distribution is that my algoritm is not unlikely to generate a 0-0 game, which Tony Padilla mention in the video their alogritm didn't

## Format for input stats file 
Here is a link for an [Example](https://pastebin.com/kMw1kDn9) of a file that I used to simulate. (It's also in the repository).
The program ignores all empty lines.
Any line that starts with a '#' is also ignored and can be used as comments. 

The format is made to be able to be copied from most wikipedia sites about any given season. [Example](https://en.wikipedia.org/wiki/2019%E2%80%9320_Premier_League#League_table)

Each row in the table should contain these collums seperaded by a singe tab (**bold is checked by program**):
1. Placement in the league (Not checked by program but kept it to simpify copy/pase from wiki) 
2. **Team Name**
3. **Games played**
4. Games Won
5. Games Drawn
6. Games Lost
7. **Goals For**
8. **Goals Against**
9. Goal difference
10. Points


## Build
This is a Console Aplication build on .NET Framework 4.7.2 and should be buildable with any compiler compatible with that. 
Or just download the latest release [Here](https://github.com/RobinClaesson/SimulateFootball/releases/tag/v1.0). 
