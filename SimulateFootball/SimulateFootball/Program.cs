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
            List<Team> teams = ReadData.LoadStatsFromFile();

            if (!Directory.Exists(outputFolder))
                Directory.CreateDirectory(outputFolder);

            ConsoleKey choice;
            bool run = true;
            do
            {
                //TODO: Redo this to not use team file, just load teams directly from stats file. This is a good redo before selecting from files
                Console.WriteLine("--- Simulate Football ---");
                Console.WriteLine("S: Simulate seasons");
                Console.WriteLine("O: Open simulation files");
                Console.WriteLine("G: Generate team file from season table file");
                Console.WriteLine("D: Delete teams file");
                Console.WriteLine("L: Load teams from teams file");
                Console.WriteLine("U: Unload teams");
                Console.WriteLine("T: List loaded teams average stats");
                Console.WriteLine("H: Help");

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

                            bool saveMatches = YesNoCheck("Save all matches to file?");
                            bool saveTables = YesNoCheck("Save all tables to file?");

                            Simulator.SimulateSeasons(teams, simulations, saveMatches, saveTables);
                        }

                        break;

                    case ConsoleKey.O:
                        Simulator.OpenFiles();
                        break;

                    case ConsoleKey.G:
                        ReadData.GenerateStatsToFile();
                        break;

                    case ConsoleKey.D:
                        bool delete = YesNoCheck("Do you want to delete Teams file?");

                        if (delete)
                        {
                            if (File.Exists("Teams.xml"))
                                File.Delete("Teams.xml");

                            Console.Clear();
                            Console.WriteLine("Teams file deleted");
                            PressAnyKey();
                        }
                        break;

                    case ConsoleKey.L:
                        teams = ReadData.LoadStatsFromFile();
                        break;

                    case ConsoleKey.U:
                        teams.Clear();
                        break;

                    case ConsoleKey.T:
                        ListTeams(teams);
                        break;

                    case ConsoleKey.H:
                        Console.WriteLine("This software reads the results from a season of football from the file called \"Input Data.txt\"");
                        Console.WriteLine("Check the Input Data file or https://github.com/RobinClaesson/SimulateFootball for the format");
                        Console.WriteLine("It saves every teams expected goal for and agains to a Teams file.");
                        Console.WriteLine("This file can loaded into the program (done automaticly at start if it exists, else promt to generate and load is given).");
                        Console.WriteLine("The stats for every team can then be used to simulate several new seasons.");
                        Console.WriteLine("The output is saved in a stats textfile, and seasons that hold records are saved to individual match and table files.");
                        Console.WriteLine("It's also possible to save every match and/or seasons table to files, though match files can be quite large.");
                        Console.WriteLine("(For 200 000 seasons match-file is ca 1.5GB)");
                        Console.WriteLine("For really big simulations (known at 1 000 000) the files becomes to big to open, and also may eventually fail the writing and break the program.");
                        Console.WriteLine("So it's suggested to turn those off during large simulations. Though it should be noted that regular notepad semes to open bigger files than ex notepad++");
                        Console.WriteLine("First to fail will always be the matche-file, so turn that of first.");
                        
                        PressAnyKey();
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
