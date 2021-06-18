using Developers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTeams
{
    public class DevTeamsREPO
    {
        private List<DevTeamsPOCO> _teamList = new List<DevTeamsPOCO>();
        private List<DevelopersPOCO> _membersList = new List<DevelopersPOCO>();
        //Create

        public void AddTeam(DevTeamsPOCO team)
        {
            _teamList.Add(team);
        }

        //Read
        public List<DevTeamsPOCO> ViewTeams()
        {
            return _teamList;
        }
        

        //Update
        public void UpdateTeam(DevTeamsPOCO newTeam, DevTeamsPOCO oldTeam)
        {
            oldTeam.TeamName = newTeam.TeamName;
            oldTeam.TeamID = newTeam.TeamID;
        }
        public void MoveMember(int oldID, int newID, DevelopersPOCO developer)
        {
            DevTeamsPOCO oldTeam = GetTeamByID(oldID);
            DevTeamsPOCO newTeam = GetTeamByID(newID);

            //add developer to new team
            //remove developer from first team
            if(oldTeam != null)
            {
                newTeam.TeamMembers.Add(developer);
                oldTeam.TeamMembers.Remove(developer);
                //could come back and add a check based on list count
            }
            else
            {
                newTeam.TeamMembers.Add(developer);
            }
        }

        public bool AddNewMember(DevelopersPOCO person, int teamID)
        {
            DevTeamsPOCO team = GetTeamByID(teamID);

            if(team != null)
            {
                team.TeamMembers.Add(person);

                return true;
            }
            else
            {
                return false;
            }
        }
        public bool RemoveTeamMembers(DevelopersPOCO person, int teamID)
        {
            DevTeamsPOCO team = GetTeamByID(teamID);

            if (team != null)
            {
                team.TeamMembers.Remove(person);

                return true;
            }
            else
            {
                return false;
            }
        }

        //Delete
        public bool DeleteTeam(int teamID)
        {
            DevTeamsPOCO team = GetTeamByID(teamID);

            if (team != null)
            {
                _teamList.Remove(team);

                return true;
            }
            else
            {
                return false;
            }
        }


        //Helper method for searching
        public DevTeamsPOCO GetTeamByID(int id)
        {
            foreach(DevTeamsPOCO team in _teamList)
            {
                if(id == team.TeamID)
                {
                    return team;
                }
            }
            return null;
        }
    }
}
