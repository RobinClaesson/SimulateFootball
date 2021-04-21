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


        int[] _placementPointSums; // Used for average point per placement

        //TODO: Save seasons that hold "records"

        public Analyzer(int numOfTeams)
        {
            _numOfTeams = numOfTeams;
            _placementPointSums = new int[_numOfTeams];

            _records.Add("HighestPoints", new Record(int.MinValue));
            _records.Add("LowestPoints", new Record(int.MaxValue));

            _records.Add("LowestPointsByWinner", new Record(int.MaxValue));
            _records.Add("HighestPointsByLastPlace", new Record(int.MinValue));

            _records.Add("LowestNeededToWin", new Record(int.MaxValue));
            _records.Add("HighestNeededToToWin", new Record(int.MinValue));
        }

        public void AddSeasonStats(Season season)
        {
            _numOfSeasons++;

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

            //Placement point sums 
            for (int i = 0; i < _numOfTeams; i++)
            {
                _placementPointSums[i] += season.Teams[i].Points;
            }
        }

        public string StatsString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("Highest points by any team: ");
            sb.AppendLine(_records["HighestPoints"].RecordString("p", true));
            sb.Append("Lowest points by any team: ");
            sb.AppendLine( _records["LowestPoints"].RecordString("p", true));
            sb.AppendLine();

            sb.Append("Lowest points by a team in first place: ");
            sb.AppendLine(_records["LowestPointsByWinner"].RecordString("p", true));
            sb.Append("Higest points by a team in last place: ");
            sb.AppendLine(_records["HighestPointsByLastPlace"].RecordString("p", true));
            sb.AppendLine();

            sb.Append("Highest points needed to win: ");
            sb.AppendLine(_records["HighestNeededToToWin"].RecordString("p", false));
            sb.Append("Lowest points needed to win: ");
            sb.AppendLine(_records["LowestNeededToWin"].RecordString("p", false));
            sb.Append("Average points needed to win: ");
            sb.AppendLine((Math.Floor((double)_placementPointSums[1] / _numOfSeasons) + 1) + "p");
            sb.AppendLine();

            //TODO: Highest and lowest to win with record class
            //TODO: Most scored, least scored
            //TODO: Most admitted, least admitted
            //TODO: Best goaldiff, worst goaldiff
            //TODO: Least scored by winner
            //TODO: Most scored by last place

            sb.AppendLine("Average point gained by table position:");
            for (int i = 0; i < _numOfTeams; i++)
                sb.AppendLine((i + 1) + ":\t" + Math.Round(((double)_placementPointSums[i] / _numOfSeasons), 1) + "p"); //Noone died from a little string concatenation

            return sb.ToString();
        }

        public string RecordMatchesString()
        {
            StringBuilder sb = new StringBuilder();
            List<int> printed = new List<int>(); // To not print the same seasons twice

            foreach(Record record in _records.Values)
            {
                if(!printed.Contains(record.Season.SeasonNumber))
                {
                    sb.AppendLine(record.Season.MatchesString());
                    printed.Add(record.Season.SeasonNumber);
                }
            }

            return sb.ToString();
        }
        public string RecordTablesString()
        {
            StringBuilder sb = new StringBuilder();
            List<int> printed = new List<int>(); // To not print the same seasons twice

            foreach(Record record in _records.Values)
            {
                if(!printed.Contains(record.Season.SeasonNumber))
                {
                    sb.AppendLine(record.Season.TableString());
                    printed.Add(record.Season.SeasonNumber);
                }
            }

            return sb.ToString();
        }
    }
}
