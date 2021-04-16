using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace SimulateFootball
{
    class TeamData
    {
        //TODO: Choose from several files
        public static void GenerateStatsToFile()
        {
            if (File.Exists("Input Data.txt"))
            {
                StreamReader reader = new StreamReader("Input Data.txt");

                List<Team> teams = new List<Team>();

                string inputRow;
                while ((inputRow = reader.ReadLine()) != null)
                {
                    if (inputRow != "" && inputRow[0] != '#') //Filters out empty rows ans comments
                    {
                        string[] teamInfo = inputRow.Split('\t');

                        Team team = new Team();
                        team.Name = teamInfo[1];


                        team.GamesPlayed = int.Parse(teamInfo[2]);
                        team.GoalsScored = int.Parse(teamInfo[6]);
                        team.GoalsAdmitted = int.Parse(teamInfo[7]);

                        team.Resuls = new int[] {int.Parse(teamInfo[3]), int.Parse(teamInfo[4]), int.Parse(teamInfo[5])};

                        teams.Add(team);
                    }
                }
                reader.Close();

                WriteTeamsToFile(teams, "Teams.xml");

                Console.WriteLine("Analyzed {0} teams from \"Input Data.txt\" and wrote to Teams.xml: ", teams.Count);

                Program.PressAnyKey();
            }

            else
            {
                Console.WriteLine("No \"Input Data.txt\" file found!");

                Program.PressAnyKey();
            }
        }

        public static void WriteTeamsToFile(List<Team> teams, string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Team>));
            StreamWriter writer = new StreamWriter(filePath, false);
            serializer.Serialize(writer, teams);
            writer.Close();
        }

        public static List<Team> LoadStatsFromFile()
        {
            if (File.Exists("Teams.xml"))
            {
                //Loads file
                XmlSerializer serializer = new XmlSerializer(typeof(List<Team>));
                StreamReader reader = new StreamReader("Teams.xml");

                List<Team> teams = (List<Team>)serializer.Deserialize(reader);
                reader.Close();

                //Normalises team name length for printing
                int longest = 0;
                foreach (Team team in teams)
                        if (team.Name.Length > longest)
                            longest = team.Name.Length;

                foreach (Team team in teams)
                    while (team.Name.Length < longest)
                        team.Name += " ";


                Console.WriteLine("Loaded {0} teams from Teams.xml", teams.Count);
                Program.PressAnyKey();

                return teams;
            }
            else
            {

                bool generate = Program.YesNoCheck("Teams.xml where not found, would you like to generate now?");
                if (generate)
                {

                    GenerateStatsToFile();
                    return LoadStatsFromFile();
                }
                else
                {
                    Console.WriteLine("No teams loaded");
                    Program.PressAnyKey();

                    return new List<Team>();
                }
                  
            }
        }
    }
}
