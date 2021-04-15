using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace SimulateAllsvenskan
{
    class TeamData
    {
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

                        double scored = double.Parse(teamInfo[6]);
                        double admitted = double.Parse(teamInfo[7]);
                        int gamesPlayed = int.Parse(teamInfo[2]);

                        team.AvrgScored = scored / gamesPlayed;
                        team.AvrgAdmitted = admitted / gamesPlayed;

                        teams.Add(team);
                    }
                }
                reader.Close();

                XmlSerializer serializer = new XmlSerializer(typeof(List<Team>));
                StreamWriter writer = new StreamWriter("Teams.xml", false);
                serializer.Serialize(writer, teams);
                writer.Close();

                Console.WriteLine("Analyzed {0} teams and wrote to Teams.xml: ", teams.Count);
            }

            else
            {
                Console.WriteLine("No Input Data file found!");
            }
        }

        public static List<Team> LoadStatsFromFile()
        {
            if (File.Exists("Teams.xml"))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Team>));
                StreamReader reader = new StreamReader("Teams.xml");

                List<Team> teams = (List<Team>)serializer.Deserialize(reader);
                reader.Close();

                return teams;
            }
            else
            {
                
                char answer = ' ';

                do
                {
                    Console.Clear();
                    Console.WriteLine("No team stats where found, would you like to generate now?");
                    Console.Write("y/n? ");
                    answer = Console.ReadKey().KeyChar;
                } while (answer != 'y' && answer != 'n');

                Console.Clear();


                if (answer == 'y')
                {
                    
                    GenerateStatsToFile();
                    return LoadStatsFromFile();
                }
                else
                    return new List<Team>();
            }
        }
    }
}
