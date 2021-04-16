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

        static string matchesFilePath = Program.outputFolder + "\\Simulated Matches.txt";
        static string tablesFilePath = Program.outputFolder + "\\Simulated Tables.txt";
        static string statsFilePath = Program.outputFolder + "\\Sumulation Stats.txt";
        public static void SimulateSeasons(List<Team> teams, int numOfseasons, bool saveMatches, bool saveTables)
        {
            DateTime startTime = DateTime.Now;

            StreamWriter matchWriter = new StreamWriter(matchesFilePath, false);
            StreamWriter tableWriter = new StreamWriter(tablesFilePath, false);
            StreamWriter statsWriter = new StreamWriter(statsFilePath, false);

            DateTime lastPrint = DateTime.Now;
            Console.Clear();
            Console.WriteLine("Simulating Season 1/{0}", numOfseasons);

            Analyzer analyzer = new Analyzer(teams.Count);
            for (int i = 1; i <= numOfseasons; i++)
            {
                if ((DateTime.Now - lastPrint).TotalMilliseconds > 500)
                {
                    Console.Clear();
                    Console.WriteLine("Simulating Season {0}/{1}", i, numOfseasons);
                    lastPrint = DateTime.Now;
                }

                List<Match> matches = SimulateSeason(teams);

                if (saveMatches)
                {
                    matchWriter.WriteLine("#----------------------------------");
                    matchWriter.WriteLine("#\tSeason {0}", i);
                    matchWriter.WriteLine("#----------------------------------");

                    foreach (Match match in matches)
                        matchWriter.WriteLine(match.ToString());
                }

                Season season = new Season(matches);
                analyzer.AddSeasonStats(season, i);
                if (saveTables)
                {
                    tableWriter.WriteLine("#----------------------------------");
                    tableWriter.WriteLine("#\tSeason {0}", i);
                    tableWriter.WriteLine("#----------------------------------");
                    tableWriter.WriteLine(season.TableString());
                }

                
            }

            matchWriter.Close();
            tableWriter.Close();


            string metadata = "Number of Seasons simulated: " + numOfseasons + "\n";
            metadata += "Total time (h:m:s): " + (DateTime.Now - startTime);
            string analytics = analyzer.OutputData();

            Console.Clear();
            Console.WriteLine("Simulations Done!");
            Console.WriteLine(metadata);
            Console.WriteLine();
            Console.WriteLine(analytics);

            statsWriter.WriteLine(metadata + "\n");
            statsWriter.WriteLine(analytics);
            statsWriter.Close();

            Program.PressAnyKey();
            bool open = Program.YesNoCheck("Do you want to open simulation files now? ");

            if (open)
            {
                OpenFiles();
            }
        }


        public static void OpenFiles()
        {
            if (File.Exists(matchesFilePath))
                Process.Start(matchesFilePath);
            if (File.Exists(tablesFilePath))
                Process.Start(tablesFilePath);
            if (File.Exists(statsFilePath))
                Process.Start(statsFilePath);
        }
    }
}
