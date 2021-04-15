using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SimulateAllsvenskan.Common;
using System.Xml.Serialization;

namespace SimulateAllsvenskan.GetStats
{
    class GetStats
    {
        static void Main(string[] args)
        {
            StreamReader reader = new StreamReader("Input.txt");

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

            Console.ReadKey();
        }
    }
}
