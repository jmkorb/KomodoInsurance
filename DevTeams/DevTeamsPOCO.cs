using Developers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTeams
{
    public class DevTeamsPOCO
    {
        public List<DevelopersPOCO> TeamMembers { get; set; } = new List<DevelopersPOCO>();
        public int TeamID {get; set;}
        public string TeamName { get; set; }

        public DevTeamsPOCO () { }

        public DevTeamsPOCO (List<DevelopersPOCO> teamMembers, int teamID, string teamName)
        {
            TeamMembers = teamMembers;
            TeamID = teamID;
            TeamName = teamName;
        }

    }
}
