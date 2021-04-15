using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SimulateAllsvenskan
{
    class Simulator
    {
        static readonly Random rnd = Program.rnd;
        public static string SimulateMatch(Team homeTeam, Team awayTeam)
        {
            double homeExpected = (homeTeam.AvrgScored + awayTeam.AvrgAdmitted) * 100;
            double awayExpected = (awayTeam.AvrgScored + homeTeam.AvrgAdmitted) * 100;

            int homeScored = rnd.Next((int)homeExpected) / 100;
            int awayScored = rnd.Next((int)awayExpected) / 100;

            return homeTeam.Name + " " + homeScored + "-" + awayScored + " " + awayTeam.Name;
        }

        public static void SimulateSeason(List<Team> teams)
        {
            List<string> matches = new List<string>();

            foreach(Team homeTeam in teams)
            {
                foreach(Team awayTeam in teams)
                {
                    if(homeTeam.Name != awayTeam.Name)
                    {
                        matches.Add(SimulateMatch(homeTeam, awayTeam));
                    }
                }
            }

            StreamWriter writer = new StreamWriter(Program.outputFolderSeasons + "\\Seaon " + (SimulationsMade + 1) + ".txt");
            foreach (string match in matches)
                writer.WriteLine(match);
            writer.Close();
            Console.WriteLine("Simulated season " + SimulationsMade);
        }

        public static int SimulationsMade
        {
            get
            {
                return Directory.GetFiles(Program.outputFolderSeasons).Length;
            }
        }

        public static void AnalyzeSeason(int seasonNumber)
        {
            
        }
    }
}
