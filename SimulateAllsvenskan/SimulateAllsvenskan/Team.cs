using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulateAllsvenskan
{
    public class Team
    {
        private string _name;
        private int _gamesPlayed = 0, _goalsScored = 0, _goalsAdmitted = 0;
        private int[] _results = new int[] { 0, 0, 0 }; //w d l
        //private double _avrgScored, _avrgAdmitted;


        public string Name { get { return _name; } set { _name = value; } }
        public int GamesPlayed { get { return _gamesPlayed; } set { _gamesPlayed = value; } }
        public int GoalsScored { get { return _goalsScored; } set { _goalsScored = value; } }
        public int GoalsAdmitted { get { return _goalsAdmitted; } set { _goalsAdmitted = value; } }

        public int[] Resuls { get { return _results; } set { _results = value; } }


        public double AvrgScored { get { return (double)_goalsScored / _gamesPlayed; } }
        public double AvrgAdmitted { get { return (double)_goalsAdmitted / _gamesPlayed; } }
        public int GoalDiff { get { return _goalsScored - GoalsAdmitted; } }

        public Team()
        {

        }

        public override string ToString()
        {
            return Name + "\tavgScore: " + Math.Round(AvrgScored, 2) + "\tavgAdmitted: " + Math.Round(AvrgAdmitted, 2);
        }
    }
}
