using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulateAllsvenskan
{
    public class Team
    {
        private string _name;
        private double _avrgScored, _avrgAdmitted;

        public string Name { get { return _name; } set { _name = value; } }
        public double AvrgScored { get { return _avrgScored; } set { _avrgScored = value; } }
        public double AvrgAdmitted { get { return _avrgAdmitted; } set { _avrgAdmitted = value; } }

        public Team()
        {

        }

        public override string ToString()
        {
            return Name + "\tavgScore: " + AvrgScored + "\tavgAdmitted: " + AvrgAdmitted;
        }
    }
}
