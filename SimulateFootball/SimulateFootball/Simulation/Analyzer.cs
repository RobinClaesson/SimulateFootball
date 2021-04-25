using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimulateFootball.Analyze;

namespace SimulateFootball
{
    class Analyzer
    {
        int _numOfTeams, _numOfSeasons = 0;

        Dictionary<string, Record> _records = new Dictionary<string, Record>();
        List<Placements> _placements = new List<Placements>();

        int[] _placementPointSums; // Used for average point per placement

        public Analyzer(List<Team> teams)
        {
            _numOfTeams = teams.Count(); ;
            _placementPointSums = new int[_numOfTeams];

            for (int i = 0; i < teams.Count; i++)
                _placements.Add(new Placements(teams[i].Name, _numOfTeams));


            _records.Add("HighestPoints", new Record(int.MinValue));
            _records.Add("LowestPoints", new Record(int.MaxValue));

            _records.Add("LowestPointsByWinner", new Record(int.MaxValue));
            _records.Add("HighestPointsByLastPlace", new Record(int.MinValue));

            _records.Add("LowestNeededToWin", new Record(int.MaxValue));
            _records.Add("HighestNeededToToWin", new Record(int.MinValue));

            _records.Add("MostScored", new Record(int.MinValue));
            _records.Add("LeastScored", new Record(int.MaxValue));

            _records.Add("MostAdmitted", new Record(int.MinValue));
            _records.Add("LeastAdmitted", new Record(int.MaxValue));

            _records.Add("BestGoalDiff", new Record(int.MinValue));
            _records.Add("WorstGoalDiff", new Record(int.MaxValue));

            _records.Add("LeastScoredByWinner", new Record(int.MaxValue));
            _records.Add("MostScoredByLastPlace", new Record(int.MinValue));

            _records.Add("MostAdmittedByWinner", new Record(int.MinValue));
            _records.Add("LeastAdmittedByLastPlace", new Record(int.MaxValue));

            _records.Add("BestGoalDiffByLastPlace", new Record(int.MinValue));
            _records.Add("WorstGoalDiffByWinner", new Record(int.MaxValue));
        }

        public void AddSeasonStats(Season season)
        {
            _numOfSeasons++;
            CheckRecords(season);
            AddPlacements(season);
        }

        private void CheckRecords(Season season)
        {
            //Highest total points
            if (season.Teams[0].Points > _records["HighestPoints"].Value)
                _records["HighestPoints"].BeatRecord(season.Teams[0].Points, season.Teams[0].Name, season);

            //Lowest total points
            if (season.Teams.Last().Points < _records["LowestPoints"].Value)
                _records["LowestPoints"].BeatRecord(season.Teams.Last().Points, season.Teams.Last().Name, season);

            //Lowest points by a winner
            if (season.Teams[0].Points < _records["LowestPointsByWinner"].Value)
                _records["LowestPointsByWinner"].BeatRecord(season.Teams[0].Points, season.Teams[0].Name, season);

            //Highest points by last place
            if (season.Teams.Last().Points > _records["HighestPointsByLastPlace"].Value)
                _records["HighestPointsByLastPlace"].BeatRecord(season.Teams.Last().Points, season.Teams.Last().Name, season);


            int neededToWin = season.Teams[1].Points + 1; //1 point more then 2nd place is needed to win
            //Highest amount of points needed to win
            if (neededToWin > _records["HighestNeededToToWin"].Value)
                _records["HighestNeededToToWin"].BeatRecord(neededToWin, "", season);

            //Lowest amount of points needed to win
            if (neededToWin < _records["LowestNeededToWin"].Value)
                _records["LowestNeededToWin"].BeatRecord(neededToWin, "", season);

            //Most Goals scored in a season
            Team currentTeam = season.TeamWithMostScored;
            if (currentTeam.GoalsScored > _records["MostScored"].Value)
                _records["MostScored"].BeatRecord(currentTeam.GoalsScored, currentTeam.Name, season);

            //Least Goals scored in a season
            currentTeam = season.TeamWithLeastScored;
            if (currentTeam.GoalsScored < _records["LeastScored"].Value)
                _records["LeastScored"].BeatRecord(currentTeam.GoalsScored, currentTeam.Name, season);

            //Most Admitted in a season
            currentTeam = season.TeamWithMostAdmitted;
            if (currentTeam.GoalsAdmitted > _records["MostAdmitted"].Value)
                _records["MostAdmitted"].BeatRecord(currentTeam.GoalsAdmitted, currentTeam.Name, season);

            //Least Admitted in a season
            currentTeam = season.TeamWithLeastAdmitted;
            if (currentTeam.GoalsAdmitted < _records["LeastAdmitted"].Value)
                _records["LeastAdmitted"].BeatRecord(currentTeam.GoalsAdmitted, currentTeam.Name, season);

            //Best Goal difference
            currentTeam = season.TeamWithBestDiff;
            if (currentTeam.GoalDiff > _records["BestGoalDiff"].Value)
                _records["BestGoalDiff"].BeatRecord(currentTeam.GoalDiff, currentTeam.Name, season);

            //Worst Goal difference
            currentTeam = season.TeamWithWorstDiff;
            if (currentTeam.GoalDiff < _records["WorstGoalDiff"].Value)
                _records["WorstGoalDiff"].BeatRecord(currentTeam.GoalDiff, currentTeam.Name, season);

            //Least scored by winner
            if (season.Teams[0].GoalsScored < _records["LeastScoredByWinner"].Value)
                _records["LeastScoredByWinner"].BeatRecord(season.Teams[0].GoalsScored, season.Teams[0].Name, season);

            //Most scored by last place
            if (season.Teams.Last().GoalsScored > _records["MostScoredByLastPlace"].Value)
                _records["MostScoredByLastPlace"].BeatRecord(season.Teams.Last().GoalsScored, season.Teams.Last().Name, season);

            //Most admitted by winner
            if (season.Teams[0].GoalsAdmitted > _records["MostAdmittedByWinner"].Value)
                _records["MostAdmittedByWinner"].BeatRecord(season.Teams[0].GoalsAdmitted, season.Teams[0].Name, season);

            //Most scored by last place
            if (season.Teams.Last().GoalsAdmitted < _records["LeastAdmittedByLastPlace"].Value)
                _records["LeastAdmittedByLastPlace"].BeatRecord(season.Teams.Last().GoalsAdmitted, season.Teams.Last().Name, season);

            //Worst Goal difference by winner
            if (season.Teams[0].GoalDiff < _records["WorstGoalDiffByWinner"].Value)
                _records["WorstGoalDiffByWinner"].BeatRecord(season.Teams[0].GoalDiff, season.Teams[0].Name, season);

            //Best goal difference by last place 
            if (season.Teams.Last().GoalDiff > _records["BestGoalDiffByLastPlace"].Value)
                _records["BestGoalDiffByLastPlace"].BeatRecord(season.Teams.Last().GoalDiff, season.Teams.Last().Name, season);


            //Placement point sums 
            for (int i = 0; i < _numOfTeams; i++)
            {
                _placementPointSums[i] += season.Teams[i].Points;
            }
        }

        private void AddPlacements(Season season)
        {
            Team[] teams = season.Teams;
            foreach (Placements placement in _placements)
                for (int i = 0; i < teams.Length; i++)
                {
                    if (teams[i].Name == placement.Team)
                        placement.AddPlacement(i + 1);
                }
        }

        public string StatsString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("---------------------------------");
            sb.AppendLine("|       Best Performances:      |");
            sb.AppendLine("---------------------------------");

            sb.Append("Highest points:       ");
            sb.AppendLine(_records["HighestPoints"].RecordString("p", true));
            sb.Append("Most goals scored:    ");
            sb.AppendLine(_records["MostScored"].RecordString(" goals", true));
            sb.Append("Least goals admitted: ");
            sb.AppendLine(_records["LeastAdmitted"].RecordString(" goals", true));
            sb.Append("Best goal difference: ");
            sb.AppendLine(_records["BestGoalDiff"].RecordString(" goals", true));
            sb.AppendLine();


            sb.AppendLine("---------------------------------");
            sb.AppendLine("|      Worst Performances:      |");
            sb.AppendLine("---------------------------------");

            sb.Append("Lowest points:         ");
            sb.AppendLine(_records["LowestPoints"].RecordString("p", true));
            sb.Append("Least goals scored:    ");
            sb.AppendLine(_records["LeastScored"].RecordString(" goals", true));
            sb.Append("Most goals admitted:   ");
            sb.AppendLine(_records["MostAdmitted"].RecordString(" goals", true));
            sb.Append("Worst goal difference: ");
            sb.AppendLine(_records["WorstGoalDiff"].RecordString(" goals", true));
            sb.AppendLine();


            sb.AppendLine("---------------------------------");
            sb.AppendLine("|  Worst teams in first place:  |");
            sb.AppendLine("---------------------------------");

            sb.Append("Lowest points by a team in first place:         ");
            sb.AppendLine(_records["LowestPointsByWinner"].RecordString("p", true));
            sb.Append("Least goals scored by a team in first place:    ");
            sb.AppendLine(_records["LeastScoredByWinner"].RecordString(" goals", true));
            sb.Append("Most goals admitted by a team in first place:   ");
            sb.AppendLine(_records["MostAdmittedByWinner"].RecordString(" goals", true));
            sb.Append("Worst goal difference by a team in first place: ");
            sb.AppendLine(_records["WorstGoalDiffByWinner"].RecordString(" goals", true));
            sb.AppendLine();

            sb.AppendLine("---------------------------------");
            sb.AppendLine("|   Best teams in last place:   |");
            sb.AppendLine("---------------------------------");

            sb.Append("Higest points by a team in last place:         ");
            sb.AppendLine(_records["HighestPointsByLastPlace"].RecordString("p", true));
            sb.Append("Most goals scored by a team in last place:     ");
            sb.AppendLine(_records["MostScoredByLastPlace"].RecordString(" goals", true));
            sb.Append("Least goals admitted by a team in last place:  ");
            sb.AppendLine(_records["LeastAdmittedByLastPlace"].RecordString(" goals", true));
            sb.Append("Best goal difference by a team in last place: ");
            sb.AppendLine(_records["BestGoalDiffByLastPlace"].RecordString(" goals", true));
            sb.AppendLine();


            sb.AppendLine("---------------------------------");
            sb.AppendLine("|     Points needed to win:     |");
            sb.AppendLine("---------------------------------");

            sb.Append("Highest points needed to win: ");
            sb.AppendLine(_records["HighestNeededToToWin"].RecordString("p", false));
            sb.Append("Lowest points needed to win:  ");
            sb.AppendLine(_records["LowestNeededToWin"].RecordString("p", false));
            sb.Append("Average points needed to win: ");
            sb.AppendLine((Math.Floor((double)_placementPointSums[1] / _numOfSeasons) + 1) + "p");
            sb.AppendLine();


            sb.AppendLine("---------------------------------");
            sb.AppendLine("| Average points per placement: |");
            sb.AppendLine("---------------------------------");

            for (int i = 0; i < _numOfTeams; i++)
                sb.AppendLine((i + 1) + ": " + Math.Round(((double)_placementPointSums[i] / _numOfSeasons), 1) + "p");
            sb.AppendLine();

            sb.AppendLine("---------------------------------");
            sb.AppendLine("|      Most league titles       |");
            sb.AppendLine("---------------------------------");
            Placements.SortAfter = 1;
            _placements.Sort(); //Sorts so that team with most victories

            for (int i = 0; i < 5; i++)
            {
                if (_placements[i].GetPlacement(1) != 0)
                {
                    sb.Append((i + 1) + ": ");
                    sb.Append(_placements[i].Team);
                    sb.AppendLine(", " + _placements[i].GetPlacement(1) + " times"); //Adds how many times the team got in first place
                }
            }


            sb.AppendLine("---------------------------------");
            sb.AppendLine("|      Most league jumbos       |");
            sb.AppendLine("---------------------------------");
            Placements.SortAfter = _numOfTeams;
            _placements.Sort(); //Sorts so that team with most jumbos
            for (int i = 0; i < 5; i++)
            {
                if (_placements[i].GetPlacement(_numOfTeams) != 0)
                {
                    sb.Append((i + 1) + ": ");
                    sb.Append(_placements[i].Team);
                    sb.AppendLine(", " + _placements[i].GetPlacement(_numOfTeams) + " times");//Adds how many times the team got in last plac
                }
            }

            return sb.ToString();
        }

        public string PlacementString()
        {
            StringBuilder sb = new StringBuilder();

            foreach (Placements placement in _placements)
                sb.AppendLine(placement.PlacementString());

            return sb.ToString();
        }

        private List<Season> RecordsToPrint()
        {
            List<Season> toPrint = new List<Season>();
            foreach (Record record in _records.Values)
            {
                if (!toPrint.Contains(record.Season))
                {
                    toPrint.Add(record.Season);
                }
            }

            toPrint.Sort();

            return toPrint;
        }
        public string RecordMatchesString()
        {
            List<Season> toPrint = RecordsToPrint();
            StringBuilder sb = new StringBuilder();
            foreach (Season season in toPrint)
                sb.AppendLine(season.MatchesString());

            return sb.ToString();
        }
        public string RecordTablesString()
        {
            List<Season> toPrint = RecordsToPrint();
            StringBuilder sb = new StringBuilder();
            foreach (Season season in toPrint)
                sb.AppendLine(season.TableString());

            return sb.ToString();
        }
    }
}
