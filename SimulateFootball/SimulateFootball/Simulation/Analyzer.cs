using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulateFootball
{
    class Analyzer
    {
        int _numOfTeams, _numOfSeasons = 0;

        int _highestPoints = int.MinValue, _highestPoints_Season;
        string _highestPoint_Team;

        int _lowestPoints = int.MaxValue, _lowestPoints_Season;
        string _lowestPoints_Team;

        int _lowestPointsByWinner = int.MaxValue, _lowestPointsByWinner_Season;
        string _lowestPointsByWinner_Team;

        int _highestPointsByLastPlace = int.MinValue, _highestPointsByLastPlace_Season;
        string _highestPointsByLastPlace_Team;

        int _highestPointsNeededToWin = int.MinValue, _highestPointsNeededToWin_Season;
        int _lowestPointsNeededToWin = int.MaxValue, _lowestPointsNeededToWin_Season;


        int[] _placementPointSums; // Used for average point per placement

        //TODO: Save seasons that hold "records"

        public int HighesPoints { get { return _highestPoints; } }
        public Analyzer(int numOfTeams)
        {
            _numOfTeams = numOfTeams;
            _placementPointSums = new int[_numOfTeams];
        }

        public void AddSeasonStats(Season season, int seasonNum)
        {
            _numOfSeasons++;

            //Highest total points
            if (season.Teams[0].Points > _highestPoints)
            {
                _highestPoints = season.Teams[0].Points;
                _highestPoint_Team = season.Teams[0].Name;
                _highestPoints_Season = seasonNum;
            }

            //Lowest total points
            if (season.Teams.Last().Points < _lowestPoints)
            {
                _lowestPoints = season.Teams.Last().Points;
                _lowestPoints_Team = season.Teams.Last().Name;
                _lowestPoints_Season = seasonNum;
            }

            //Lowest points by a winner
            if (season.Teams[0].Points < _lowestPointsByWinner)
            {
                _lowestPointsByWinner = season.Teams[0].Points;
                _lowestPointsByWinner_Team = season.Teams[0].Name;
                _lowestPointsByWinner_Season = seasonNum;
            }

            //Highest points by last place
            if (season.Teams.Last().Points > _highestPointsByLastPlace)
            {
                _highestPointsByLastPlace = season.Teams.Last().Points;
                _highestPointsByLastPlace_Team = season.Teams.Last().Name;
                _highestPointsByLastPlace_Season = seasonNum;
            }

                int neededToWin = season.Teams[1].Points + 1; //1 point more then 2nd place is needed to win
            //Highest amount of points needed to win
            if (neededToWin > _highestPointsNeededToWin)
            {
                _highestPointsNeededToWin = neededToWin;
                _highestPointsNeededToWin_Season = seasonNum;
            }
            //Lowest amount of points needed to win
            if (neededToWin < _lowestPointsNeededToWin)
            {
                _lowestPointsNeededToWin = neededToWin;
                _lowestPointsNeededToWin_Season = seasonNum;
            }

            //Placement point sums 
            for (int i = 0; i < _numOfTeams; i++)
            {
                _placementPointSums[i] += season.Teams[i].Points;
            }
        }

        public string OutputData()
        {
            string s = "";

            s += "Highest points by any team: " + _highestPoints + "p, by " + _highestPoint_Team + " in season " + _highestPoints_Season + "\n";
            s += "Lowest points by any team: " + _lowestPoints + "p, by " + _lowestPoints_Team + " in season " + _lowestPoints_Season + "\n";
            s += "\n";

            s += "Lowest points by a winning team: " + _lowestPointsByWinner + "p, by " + _lowestPointsByWinner_Team + " in season " + _lowestPointsByWinner_Season + "\n";
            s += "Highest points by a team in last place: " + _highestPointsByLastPlace + "p, by " + _highestPointsByLastPlace_Team + " in season " + _highestPointsByLastPlace_Season + "\n";
            s += "\n";

            s += "Highest points needed to win: " + _highestPointsNeededToWin + "p, in season " + _highestPointsNeededToWin_Season + "\n";
            s += "Lowest points needed to win: " + _lowestPointsNeededToWin + "p, in season " + _lowestPointsNeededToWin_Season + "\n";
            s += "Average points needed to win: " + Math.Ceiling(1 + (double)_placementPointSums[1] / _numOfSeasons) + "p\n";
            s += "\n";


            //TODO: Most scored, least scored
            //TODO: Most admitted, least admitted
            //TODO: Best goaldiff, worst goaldiff
            //TODO: Least scored by winner
            //TODO: Most scored by last place

            s += "Average point gained by table position: \n";
            for (int i = 0; i < _numOfTeams; i++)
                s += (i + 1) + ":\t" + Math.Round(((double)_placementPointSums[i] / _numOfSeasons), 1) + "p\n";

            return s;
        }
    }
}
