using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace SimulateAllsvenskan
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

        public static void SimulateSeasons(List<Team> teams, int numOfseasons)
        {
            DateTime startTime = DateTime.Now;
            string matchesFilePath = Program.putputFolder + "\\Simulated Matches.txt";
            StreamWriter matchWriter = new StreamWriter(matchesFilePath, false);

            DateTime lastPrint = DateTime.Now;
            Console.Clear();
            Console.WriteLine("Simulating Season 1/{0}", numOfseasons);

            for (int i = 1; i <= numOfseasons; i++)
            {
                if ((DateTime.Now - lastPrint).TotalMilliseconds > 500)
                {
                    Console.Clear();
                    Console.WriteLine("Simulating Season {0}/{1}", i, numOfseasons);
                    lastPrint = DateTime.Now;
                }

                List<Match> season = SimulateSeason(teams);

                matchWriter.WriteLine("----------------------------------");
                matchWriter.WriteLine("\tSeason {0}", i);
                matchWriter.WriteLine("----------------------------------");

                foreach (Match match in season)
                    matchWriter.WriteLine(match.ToString());

            }

            matchWriter.Close();

            Console.Clear();
            Console.WriteLine("Simulations Done!");
            Console.WriteLine("Number of Seasons simulated: {0}", numOfseasons);
            Console.WriteLine("Total time: {0}", (DateTime.Now - startTime));

            Program.PressAnyKey();
            bool open = Program.YesNoCheck("Do you want to open simulation files now? ");

            if (open)
            {
                Process.Start(matchesFilePath);
            }
        }



    }
}
