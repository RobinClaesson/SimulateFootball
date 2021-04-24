using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulateFootball
{
    class Season : IComparable
    {
        List<Team> _teams = new List<Team>();
        List<Match> _matches = new List<Match>();
        int _seasonNumber = 0;

        public Team[] Teams { get { return _teams.ToArray(); } }
        public int SeasonNumber { get { return _seasonNumber; } }

        public Season(List<Match> playedMatches, int seasonNumber)
        {
            _seasonNumber = seasonNumber;

            foreach (Match match in playedMatches)
            {
                AddMatch(match);
            }
            _teams.Sort();

            _matches.AddRange(playedMatches);
        }
        private void AddMatch(Match match)
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
        }

        public Team TeamWithMostScored
        {
            get
            {
                int t = 0;

                for (int i = 1; i < _teams.Count; i++)
                    if (_teams[i].GoalsScored > _teams[t].GoalsScored)
                        t = i;

                return _teams[t];
            }
        }
        public Team TeamWithLeastScored
        {
            get
            {
                int t = 0;

                for (int i = 1; i < _teams.Count; i++)
                    if (_teams[i].GoalsScored < _teams[t].GoalsScored)
                        t = i;

                return _teams[t];
            }
        }

        public Team TeamWithMostAdmitted
        {
            get
            {
                int t = 0;

                for (int i = 1; i < _teams.Count; i++)
                    if (_teams[i].GoalsAdmitted > _teams[t].GoalsAdmitted)
                        t = i;

                return _teams[t];
            }
        }
        public Team TeamWithLeastAdmitted
        {
            get
            {
                int t = 0;

                for (int i = 1; i < _teams.Count; i++)
                    if (_teams[i].GoalsAdmitted < _teams[t].GoalsAdmitted)
                        t = i;

                return _teams[t];
            }
        }

        public Team TeamWithBestDiff
        {
            get
            {
                int t = 0;

                for (int i = 1; i < _teams.Count; i++)
                    if (_teams[i].GoalDiff > _teams[t].GoalDiff)
                        t = i;

                return _teams[t];
            }
        }
        
        public Team TeamWithWorstDiff
        {
            get
            {
                int t = 0;

                for (int i = 1; i < _teams.Count; i++)
                    if (_teams[i].GoalDiff < _teams[t].GoalDiff)
                        t = i;

                return _teams[t];
            }
        }


        private string HeaderString()
        {
            return "#---------------------------------\n#\t Season " + _seasonNumber + "\n#---------------------------------";
        }
        public string TableString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(HeaderString());
            sb.AppendLine("#Placement Team gp w d l scored admitted diff points");

            for (int i = 0; i < _teams.Count; i++)
            {
                sb.Append((i + 1) + "\t");
                sb.Append(_teams[i].FixedLengthName + "\t");
                sb.Append(_teams[i].GamesPlayed + "\t");
                sb.Append(_teams[i].Wins + "\t");
                sb.Append(_teams[i].Draws + "\t");
                sb.Append(_teams[i].Losses + "\t");
                sb.Append(_teams[i].GoalsScored + "\t");
                sb.Append(_teams[i].GoalsAdmitted + "\t");
                sb.Append(_teams[i].GoalDiff + "\t");
                sb.AppendLine(_teams[i].Points + "");
            }

            return sb.ToString();
        }

        public string MatchesString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(HeaderString());

            foreach (Match match in _matches)
                sb.AppendLine(match.ToString());

            return sb.ToString();
        }

        //Sorts the seasons from smallest to largest season number
        public int CompareTo(object obj)
        {
            Season other = (Season)obj;

            return this.SeasonNumber - other.SeasonNumber;
        }
    }
}
