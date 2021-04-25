using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulateFootball
{
    class Placements : IComparable
    {
        string _team;
        int[] _placements;
        public string Team { get { return _team; } }

        public static int SortAfter { get; set; }
      
        public int AveragePlacement //This is a bit buggy for small number of simulatios, but it's close enough for large
        {
            get
            {
                int sum = 0, pSum = 0; 
                for(int i = 0; i < _placements.Length; i++)
                {
                    sum += _placements[i];
                    pSum += _placements[i] * i; 
                }
                return pSum / sum;
            }
        }
        public Placements(string teamName, int numOfTeams)
        {
            _team = teamName;
            _placements = new int[numOfTeams];
            SortAfter = 1;
        }

        public void AddPlacement(int placement)
        {
            _placements[placement - 1]++;
        }

        public string PlacementString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("---------------------------------");
            sb.AppendLine(_team);
            sb.AppendLine("---------------------------------");
            sb.AppendLine("Average placement: " + AveragePlacement);
            for(int i = 0; i < _placements.Length; i++)
            {
                sb.AppendLine((i + 1) + ": " + _placements[i]);
            }
            sb.AppendLine();
            return sb.ToString();
        }


        public int CompareTo(object obj)
        {
            Placements other = (Placements)obj;
            return other._placements[SortAfter-1] - _placements[SortAfter-1];
        }

        public int GetPlacement(int placement)
        {
            return _placements[placement-1];
        }
    }
}
