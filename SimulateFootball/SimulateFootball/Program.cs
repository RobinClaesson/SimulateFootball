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
            List<Team> teams = TeamData.LoadStatsFromFile();
            
            if (!Directory.Exists(outputFolder))          
                Directory.CreateDirectory(outputFolder);
  
            ConsoleKey choice;
            bool run = true;
            do
            {
                Console.WriteLine("--- Simulate Allsvenskan ---");
                Console.WriteLine("S: Simulate Seasons");
                Console.WriteLine("G: Generate team.xml");
                Console.WriteLine("D: Delete teams.xml");
                Console.WriteLine("L: Load teams from teams.xml");
                Console.WriteLine("T: List loaded teams average stats");


                Console.WriteLine("E: Exit");

                choice = Console.ReadKey().Key;
                Console.Clear();
                

                switch (choice)
                {
                    case ConsoleKey.S:

                        Console.Clear();
                        Console.Write("How many seasons do you want to simulate?: ");
                        int simulations = int.Parse(Console.ReadLine());

                        Simulator.SimulateSeasons(teams, simulations);


                        break;

                    case ConsoleKey.G:
                        TeamData.GenerateStatsToFile();
                        break;

                    case ConsoleKey.L:
                        teams = TeamData.LoadStatsFromFile();
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
