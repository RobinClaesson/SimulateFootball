using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulateFootball
{
    class Season
    {
        List<Team> _teams = new List<Team>();

        public Team[] Teams { get { return _teams.ToArray(); } }
        public Season(List<Match> playedMatches)
        {
            foreach (Match match in playedMatches)
            {
                AddMatch(match, false);
            }

            _teams.Sort();
        }

        public void AddMatch(Match match)
        {
            AddMatch(match, true);
        }

        private void AddMatch(Match match, bool sort)
        {
            bool foundHomeTeam = false;
            bool foundAwayTeam = false;
            foreach (Team team in _teams)
            {
                if (team.Name == match.HomeTeam)
                {
                    foundHomeTeam = true;
                    team.AddGame(match.HomeTeamScore, match.AwayTeamScore);
                }
                else if (team.Name == match.AwayTeam)
                {
                    foundAwayTeam = true;
                    team.AddGame(match.AwayTeamScore, match.HomeTeamScore);
                }
            }

            if (!foundHomeTeam)
            {
                Team homeTeam = new Team();
                homeTeam.Name = match.HomeTeam;
                homeTeam.AddGame(match.HomeTeamScore, match.AwayTeamScore);
                _teams.Add(homeTeam);
            }

            if (!foundAwayTeam)
            {
                Team awayTeam = new Team();
                awayTeam.Name = match.AwayTeam;
                awayTeam.AddGame(match.AwayTeamScore, match.HomeTeamScore);
                _teams.Add(awayTeam);
            }

            if (sort)
                _teams.Sort();
        }
        public string TableString()
        {
            string s = "#Placement Team gp w d l scored admitted diff points\n";

            for (int i = 0; i < _teams.Count; i++)
            {
                s += (i + 1) + "\t";
                s += _teams[i].Name + "\t";
                s += _teams[i].GamesPlayed + "\t";
                s += _teams[i].Wins + "\t";
                s += _teams[i].Draws + "\t";
                s += _teams[i].Losses + "\t";
                s += _teams[i].GoalsScored + "\t";
                s += _teams[i].GoalsAdmitted + "\t";
                s += _teams[i].GoalDiff + "\t";
                s += _teams[i].Points + "\n";

            }

            return s;
        }
    }
}
