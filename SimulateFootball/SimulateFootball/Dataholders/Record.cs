using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulateFootball.Analyze
{
    class Record 
    {
        int _value;
        string _team;
        Season _season;

        public Record(int startValue)
        {
            _value = startValue;
        }

        public int Value { get { return _value; } set { _value = value; } }
        public string Team { get { return _team; } set { _team = value; } }
        public Season Season { get { return _season; } set { _season = value; } }

        public void BeatRecord(int newValue, string newTeam, Season newSeason)
        {
            _value = newValue;
            _team = newTeam;
            _season = newSeason;
        }

        public string RecordString(string valueSuffix, bool addTeamName)
        {
            string s = _value + valueSuffix + ", ";

            if (addTeamName)
                s += "by " + _team + " ";

            s += "in season " + _season.SeasonNumber;

            return s;
        }

        
    }
}
