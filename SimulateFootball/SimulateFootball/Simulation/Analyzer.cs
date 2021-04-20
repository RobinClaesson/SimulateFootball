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

        public string OutputData()
        {
            string s = "";

            s += "Highest points by any team: " + _records["HighestPoints"].RecordString("p", true) + "\n";
            s += "Lowest points by any team: " + _records["LowestPoints"].RecordString("p", true) + "\n";
            s += "\n";

            s += "Lowest points by a team in first place: " + _records["LowestPointsByWinner"].RecordString("p", true) + "\n";
            s += "Higest points by a team in last place: " + _records["HighestPointsByLastPlace"].RecordString("p", true) + "\n";
            s += "\n";

            s += "Highest points needed to win: " + _records["HighestNeededToToWin"].RecordString("p", false) + "\n";
            s += "Lowest points needed to win: " + _records["LowestNeededToWin"].RecordString("p", false) + "\n";
            s += "Average points needed to win: " + (Math.Floor((double)_placementPointSums[1] / _numOfSeasons) + 1) + "p\n";
            s += "\n";

            //TODO: Highest and lowest to win with record class
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
