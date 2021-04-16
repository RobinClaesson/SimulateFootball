using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SimulateFootball
{
    public class Team : IComparable
    {
        private string _name;
        private int _gamesPlayed = 0, _goalsScored = 0, _goalsAdmitted = 0;
        private int[] _results = new int[] { 0, 0, 0 }; //w d l
        //private double _avrgScored, _avrgAdmitted;


        public string Name { get { return _name; } set { _name = value; } }
        public string FixedLengthName
        {
            get
            {
                string s = _name;

                while (s.Length < 20)
                    s += " ";

                return s;
            }
        }
        public int GamesPlayed { get { return _gamesPlayed; } set { _gamesPlayed = value; } }
        public int GoalsScored { get { return _goalsScored; } set { _goalsScored = value; } }
        public int GoalsAdmitted { get { return _goalsAdmitted; } set { _goalsAdmitted = value; } }
        public int[] Resuls { get { return _results; } set { _results = value; } }

        public int Wins { get { return _results[0]; } }
        public int Draws { get { return _results[1]; } }
        public int Losses { get { return _results[2]; } }

        public int Points { get { return 3 * _results[0] + _results[1]; } }


        public double AvrgScored { get { return (double)_goalsScored / _gamesPlayed; } }
        public double AvrgAdmitted { get { return (double)_goalsAdmitted / _gamesPlayed; } }
        public int GoalDiff { get { return _goalsScored - GoalsAdmitted; } }

        public Team()
        {

        }

        public override string ToString()
        {
            return FixedLengthName + "\tavgScore: " + Math.Round(AvrgScored, 2) + "\tavgAdmitted: " + Math.Round(AvrgAdmitted, 2);
        }

        public void AddGame(int scoredGoals, int admittedGoals)
        {
            _gamesPlayed++;

            _goalsScored += scoredGoals;
            _goalsAdmitted += admittedGoals;

            if (scoredGoals > admittedGoals) //win
                _results[0]++;
            else if (scoredGoals < admittedGoals) //Lose
                _results[2]++;
            else
                _results[1]++;  //draw
        }


        public int CompareTo(object obj)
        {
            Team other = (Team)obj;

            if (this.Points < other.Points) //Other team has more points
                return 1;

            else if (this.Points > other.Points) //This team has more points
                return -1;

            //Same amount of points
            else
            {
                if (this.GoalDiff < other.GoalDiff) //Other team has better goaldiff
                    return 1;

                else if (this.GoalDiff > other.GoalDiff) //This team has better gaoldiff
                    return -1;

                //Same goaldiff
                else
                {
                    if (this.GoalsScored < other.GoalsScored)
                        return 1;
                    else if (this.GoalsScored > other.GoalsScored)
                        return -1;

                    else return 0;
                }
            }
        }

    }
}
