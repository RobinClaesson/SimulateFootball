using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SimulateFootball
{
    class Program
    {
        public static Random rnd = new Random();
        public static string outputFolder = "Simulations";
        static void Main(string[] args)
        {
            List<Team> teams = new List<Team>();
            //teams = LoadTeamsFromFile(teams);

            if (!Directory.Exists(outputFolder))
                Directory.CreateDirectory(outputFolder);

            ConsoleKey choice;
            bool run = true;
            do
            {
                //TODO: Redo this to not use team file, just load teams directly from stats file. This is a good redo before selecting from files
                Console.WriteLine("--- Simulate Football ---");
                Console.WriteLine("S: Simulate seasons");
                Console.WriteLine("O: Open simulation output folder");
                Console.WriteLine("L: Load teams from table file");
                Console.WriteLine("T: List loaded teams");
                Console.WriteLine("E: Exit");

                choice = Console.ReadKey().Key;
                Console.Clear();


                switch (choice)
                {
                    case ConsoleKey.S:

                        Console.Clear();

                        if (teams.Count == 0)
                        {
                            Console.WriteLine("No teams are loaded");
                            PressAnyKey();
                        }
                        else
                        {
                            Console.Write("How many seasons do you want to simulate?: ");
                            int simulations = int.Parse(Console.ReadLine());

                            bool saveMatches = YesNoCheck("Save all matches to file? ");
                            bool saveTables = YesNoCheck("Save all tables to file?");

                            Simulator.SimulateSeasons(teams, simulations, saveMatches, saveTables);
                        }

                        break;

                    case ConsoleKey.O:
                        Simulator.OpenOutputFolder();
                        break;


                    case ConsoleKey.L:
                        teams = LoadTeamsFromFile(teams);
                        break;

                    case ConsoleKey.T:
                        ListTeams(teams);
                        break;

                    case ConsoleKey.E:
                        run = !YesNoCheck("Do you want to exit?");
                        break;
                }


            } while (run);

        }

        private static List<Team> LoadTeamsFromFile(List<Team> teams)
        {
            Console.Clear();
            Console.WriteLine("Choose file to load table from: ");
            
            string loadPath = ReadData.SelectTableFile();
            Console.Clear();
            if (loadPath == "")
                Console.WriteLine("No file selected...");
            else
            {
                teams = ReadData.LoadTeamsFromTableFile(loadPath);
                Console.WriteLine("Loaded stats from {0} teams!", teams.Count);
            }
            PressAnyKey();
            return teams;
        }

        private static void ListTeams(List<Team> teams)
        {
            if (teams.Count == 0)
                Console.WriteLine("No teams are loaded");

            foreach (Team team in teams)
                Console.WriteLine(team.ToString());

            PressAnyKey();
        }

        public static bool YesNoCheck(string message)
        {
            ConsoleKey answer;

            do
            {
                Console.Clear();
                Console.WriteLine(message);
                Console.Write("y/n? ");
                answer = Console.ReadKey().Key;
            } while (answer != ConsoleKey.Y && answer != ConsoleKey.N);

            Console.Clear();
            return answer == ConsoleKey.Y;

        }

        public static void PressAnyKey()
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
