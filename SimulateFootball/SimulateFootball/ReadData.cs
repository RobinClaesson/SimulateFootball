using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;
using System.Windows.Forms;
using System.Threading;

namespace SimulateFootball
{
    class ReadData
    {
        public static List<Team> LoadTeamsFromTableFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                StreamReader reader = new StreamReader(filePath);

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

                        team.Resuls = new int[] { int.Parse(teamInfo[3]), int.Parse(teamInfo[4]), int.Parse(teamInfo[5]) };

                        teams.Add(team);
                    }
                }
                reader.Close();
                return teams;

            }

            else
                return null;

        }

        public static string SelectTableFile()
        {
            string path = "";

            //Creates a new thread because OpenFileDialog requiers Thread to be STA 
            Thread t = new Thread((ThreadStart)(() =>
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Text files (*.txt)|*.txt";
                openFileDialog.FileName = "Table textfile";
                openFileDialog.Title = "Select a table textfile to get stats from";
                openFileDialog.Multiselect = false;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    path = openFileDialog.FileName;
                }

            }));

            // Run your code from a thread that joins the STA Thread
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
            t.Join();


            return path;      
        }

        public static void WriteTeamsToFile(List<Team> teams, string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Team>));
            StreamWriter writer = new StreamWriter(filePath, false);
            serializer.Serialize(writer, teams);
            writer.Close();
        }

        public static List<Team> LoadTeamsFromXml(string pathToXml)
        {
            if (File.Exists(pathToXml))
            {
                //Loads file
                XmlSerializer serializer = new XmlSerializer(typeof(List<Team>));
                StreamReader reader = new StreamReader(pathToXml);

                List<Team> teams = (List<Team>)serializer.Deserialize(reader);
                reader.Close();

                Console.WriteLine("Loaded {0} teams from Teams file", teams.Count);
                Program.PressAnyKey();

                return teams;
            }
            else
                return new List<Team>();
        }


    }
}
