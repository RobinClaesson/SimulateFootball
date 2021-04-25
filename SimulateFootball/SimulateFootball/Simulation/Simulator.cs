using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace SimulateFootball
{
    class Simulator
    {
        static readonly Random rnd = Program.rnd;
        public static Match SimulateMatch(Team homeTeam, Team awayTeam)
        {
            //I multiplied with 100 before the random and devide with 100 to get a little better
            //rounding of the double. In this way I can see it as randoming a decimal number and then rounding it.
            //The more I think of it i think it acctually doesnt makes a difference. But it doesn't make it worse imo.  
            double homeExpected = (homeTeam.AvrgScored + awayTeam.AvrgAdmitted) * 100;
            double awayExpected = (awayTeam.AvrgScored + homeTeam.AvrgAdmitted) * 100;

            int homeScored = rnd.Next((int)homeExpected) / 100;
            int awayScored = rnd.Next((int)awayExpected) / 100;

            return new Match(homeTeam.Name, homeScored, awayTeam.Name, awayScored);
        }


        public static List<Match> SimulateSeason(List<Team> teams)
        {
            List<Match> matches = new List<Match>();

            foreach (Team homeTeam in teams)
            {
                foreach (Team awayTeam in teams)
                {
                    if (homeTeam.Name != awayTeam.Name)
                    {
                        matches.Add(SimulateMatch(homeTeam, awayTeam));
                    }
                }
            }

            return matches;
        }

        static string allMatchesFilePath = Program.outputFolder + "\\All Matches.txt";
        static string recordMatchesFilePath = Program.outputFolder + "\\Matches From Record Seasons.txt";

        static string allTablesFilePath = Program.outputFolder + "\\All Tables.txt";
        static string recordTablesFilePath = Program.outputFolder + "\\Tables From Record Seasons.txt";

        static string statsFilePath = Program.outputFolder + "\\Sumulation Stats.txt";
        static string placementFilePath = Program.outputFolder + "\\Team placements.txt";
        public static void SimulateSeasons(List<Team> teams, int numOfseasons, bool saveMatches, bool saveTables)
        {
            DateTime startTime = DateTime.Now;

            StreamWriter matchWriter = new StreamWriter(allMatchesFilePath, false);
            StreamWriter tableWriter = new StreamWriter(allTablesFilePath, false);

            DateTime lastPrint = DateTime.Now;
            Console.Clear();
            Console.WriteLine("Simulating Season 1/{0}", numOfseasons);

            Analyzer analyzer = new Analyzer(teams);

            //Simulation loop
            for (int i = 1; i <= numOfseasons; i++)
            {
                if ((DateTime.Now - lastPrint).TotalMilliseconds > 500)
                {
                    lastPrint = DateTime.Now;
                    double percent = (double)i / numOfseasons * 100;

                    Console.Clear();
                    Console.WriteLine("Simulating Season {0}/{1}", i, numOfseasons);
                    Console.Write("|");

                    int x = 0;
                    while (x <= (int)percent / 2)
                    {
                        Console.Write("#");
                        x++;
                    }
                    while (x < 50)
                    {
                        Console.Write(" ");
                        x++;
                    }
                    Console.WriteLine("|{0}%", (int)percent);
                }

                List<Match> matches = SimulateSeason(teams);
                Season season = new Season(matches, i);
                analyzer.AddSeasonStats(season);

                if (saveMatches)
                    matchWriter.WriteLine(season.MatchesString());

                if (saveTables)
                    tableWriter.WriteLine(season.TableString());
            }

            matchWriter.Close();
            tableWriter.Close();


            //Saving record seasons to file
            matchWriter = new StreamWriter(recordMatchesFilePath, false);
            tableWriter = new StreamWriter(recordTablesFilePath, false);

            matchWriter.Write(analyzer.RecordMatchesString());
            tableWriter.Write(analyzer.RecordTablesString());

            matchWriter.Close();
            tableWriter.Close();

            //Saving all teams positions to file
            StreamWriter placeWriter = new StreamWriter(placementFilePath, false);
            placeWriter.Write(analyzer.PlacementString());
            placeWriter.Close();


            //Saving stats to file
            StreamWriter statsWriter = new StreamWriter(statsFilePath, false);

            string metadata = "Number of Seasons simulated: " + numOfseasons + "\n";
            metadata += "Total time (h:m:s): " + (DateTime.Now - startTime);
            string analytics = analyzer.StatsString();

            Console.Clear();
            Console.WriteLine("Simulations Done!");
            Console.WriteLine(metadata);
            Console.WriteLine();
            Console.WriteLine(analytics);

            statsWriter.WriteLine(metadata + "\n");
            statsWriter.WriteLine(analytics);
            statsWriter.Close();

            Program.PressAnyKey();

            //bool open = Program.YesNoCheck("Do you want to open simulation output directory? ");

            //if (open)
            //    OpenOutputFolder();

        }

        public static void OpenOutputFolder()
        {
            Process.Start(Program.outputFolder + "\\");
        }
    }
}
