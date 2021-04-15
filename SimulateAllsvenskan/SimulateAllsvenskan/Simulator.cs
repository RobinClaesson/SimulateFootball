using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulateAllsvenskan
{
    class Simulator
    {
        static Random rnd = Program.rnd;
        public static string SimulateMatch(Team homeTeam, Team awayTeam)
        {
            double homeExpected = (homeTeam.AvrgScored + awayTeam.AvrgAdmitted) * 100;
            double awayExpected = (awayTeam.AvrgScored + homeTeam.AvrgAdmitted) * 100;

            int homeScored = rnd.Next((int)homeExpected) / 100;
            int awayScored = rnd.Next((int)awayExpected) / 100;

            return homeTeam.Name + " " + homeScored + "-" + awayScored + " " + awayTeam.Name;
        }
    }
}
