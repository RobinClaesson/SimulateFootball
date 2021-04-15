using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SimulateAllsvenskan
{
    class Program
    {
        public static Random rnd = new Random();
        static string outputFolder = "Simulated Seasons";
        static void Main(string[] args)
        {
            List<Team> teams = TeamData.LoadStatsFromFile();

            Console.WriteLine("--- Simulate Allsvenskan ---\n");
            Console.WriteLine("Found {0} teams", teams.Count);
            
            if (Directory.Exists(outputFolder))
            {
                string[] files = Directory.GetFiles(outputFolder);
                Console.WriteLine("Found {0} previously simulated seasons", files.Length);
            }

            else
            {
                Directory.CreateDirectory(outputFolder);
                Console.WriteLine("Found 0 simulated seasons");
            }
            Console.WriteLine();

            while(true)
            {

            }

            //Console.Write("How many more seasons would you like to simulate: ");
            


            Console.ReadKey();
        }

    }
}
