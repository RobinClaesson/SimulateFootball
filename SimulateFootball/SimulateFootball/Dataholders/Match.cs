using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulateFootball
{
    class Match
    {
        private string _homeTeam, _awayTeam;
        private int _homeScore, _awayScore;

        public string HomeTeam { get { return _homeTeam; } }
        public string AwayTeam { get { return _awayTeam; } }

        public int HomeTeamScore { get { return _homeScore; } }
        public int AwayTeamScore { get { return _awayScore; } }

        public Match(string homeTeam, int homeScore, string awayTeam, int awayScore)
        {
            _homeTeam = homeTeam;
            _awayTeam = awayTeam;
            _homeScore = homeScore;
            _awayScore = awayScore;
        }

        public override string ToString()
        {
            return _homeTeam + " " + _homeScore + " - " + _awayScore + " " + _awayTeam;
        }
    }
}
