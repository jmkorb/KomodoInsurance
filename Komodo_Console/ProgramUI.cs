using Developers;
using DevTeams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Komodo_Console
{
    class ProgramUI
    {
        private DevelopersREPO _developersRepo = new DevelopersREPO();
        private DevTeamsREPO _devTeamRepo = new DevTeamsREPO();

        public void Run()
        {
            SeedContent();
            Menu();
        }

        public void Menu()
        {
            bool keepRunning = true;
            while (keepRunning)
            {
                //Display Menu Options
                Console.WriteLine("What would you like to do?\n" +
                    "1. View List of Current Developers\n" +
                    "2. Add a New Developer\n" +
                    "3. Update a Developer's Information\n" +
                    "4. Remove a Developer from the List\n" +
                    "5. View List Of Teams\n" +
                    "6. View a Team's List of Developers\n" +
                    "7. Add a Team\n" +
                    "8. Remove a Team\n" +
                    "9. Update a Team\n" +
                    "0. Exit");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        ViewListOfDevelopers();
                        break;
                    case "2":
                        CreateNewDeveloper();
                        break;
                    case "3":
                        UpdateDeveloper();
                        break;
                    case "4":
                        DeleteDeveloper();
                        break;
                    case "5":
                        ViewListOfTeams();
                        break;
                    case "6":
                        ViewListOfTeamMembers();
                        break;
                    case "7":
                        AddNewTeam();
                        break;
                    case "8":
                        RemoveTeam();
                        break;
                    case "9":
                        break;
                    //0 because zero is one less button to type for exiting
                    case "0":
                        Console.WriteLine("Closing. . .");
                        keepRunning = false;
                        break;
                }



            }
        }
        private void ViewListOfDevelopers()
        {
            Console.Clear();

            List<DevelopersPOCO> developers = _developersRepo.DisplayListOfDevelopers();

            foreach(DevelopersPOCO person in developers)
            {
                Console.WriteLine($"Name: {person.FirstName} {person.LastName}  ID: {person.PersonalID}  Has Pluralsight Access?: {person.PersonalID}");
            }

            if(developers.Count == 0)
            {
                Console.WriteLine("There are currently no developers.");
            }
            Console.WriteLine();
        }

        private void ViewListOfTeams()
        {
            Console.Clear();

            List<DevTeamsPOCO> teams = _devTeamRepo.ViewTeams();

            foreach (DevTeamsPOCO team in teams)
            {
                Console.WriteLine($"Team: {team.TeamName}  TeamID: {team.TeamID}");
            }

            if (teams.Count == 0)
            {
                Console.WriteLine("There are currently no teams.");
            }
            Console.WriteLine();
        }

        private void ViewListOfTeamMembers()
        {
            Console.Clear();

            //Display a list of all teams to choose from
            List<DevTeamsPOCO> teams = _devTeamRepo.ViewTeams();

            foreach (DevTeamsPOCO team in teams)
            {
                Console.WriteLine($"Team: {team.TeamName}  TeamID: {team.TeamID}");
            }

            Console.WriteLine("Enter the TeamID of the team you want to look at?");

            //Confirm and select the input team
            DevTeamsPOCO selectedTeam = new DevTeamsPOCO();
            bool teamSearch = true;
            while (teamSearch)
            {
                string input = Console.ReadLine().ToLower();
                int teamID = int.Parse(input);
                selectedTeam = _devTeamRepo.GetTeamByID(teamID);
                if (selectedTeam != null)
                {
                    teamSearch = false;
                }
                else
                {
                    Console.WriteLine("That TeamID does not currently exist. Please select an existing TeamID from the list above:");
                }
            }

            Console.Clear();

            Console.WriteLine($"Team Name: {selectedTeam.TeamName} TeamID: {selectedTeam.TeamID}");

            foreach (DevelopersPOCO person in selectedTeam.TeamMembers)
            {
                Console.WriteLine($"Name: {person.FirstName} {person.LastName}  ID: {person.PersonalID}  Has Pluralsight Access?: {person.PersonalID}");
            }
            Console.WriteLine();
        }

        private void CreateNewDeveloper()
        {
            Console.Clear();

            DevelopersPOCO newDeveloper = new DevelopersPOCO();

            //First Name
            Console.WriteLine("Enter the Developer's First Name:");
            newDeveloper.FirstName = Console.ReadLine();

            //Last Name
            Console.WriteLine("Enter the Developer's Last Name:");
            newDeveloper.LastName = Console.ReadLine();

            //ID(check if not already used)
            Console.WriteLine("Enter the Developer's Unique ID number: ");

            bool checkID = true;
            while (checkID)
            {
                string ID = Console.ReadLine();
                int numID = int.Parse(ID);
                if (_developersRepo.GetDeveloperByID(numID) == null)
                {
                    newDeveloper.PersonalID = int.Parse(ID);
                    checkID = false;
                }
                else
                {
                    Console.WriteLine("This ID has been taken. Please pick a new one:");
                }
            }

            bool keepChecking = true;
            //Bool Has PluralSight
            while (keepChecking)
            {
                Console.WriteLine("Does this Developer has access to Pluralsight? (y/n):");
                string answer = Console.ReadLine().ToLower();

                if (answer == "y")
                {
                    newDeveloper.HasPluralsight = true;
                    keepChecking = false;
                }
                else if (answer == "n")
                {
                    newDeveloper.HasPluralsight = false;
                    keepChecking = false;
                }
                else
                {
                    Console.WriteLine("Please enter either y or n.");
                }
            }

            _developersRepo.AddDeveloperToList(newDeveloper);
        }

        private void DeleteDeveloper()
        {
            Console.Clear();

            Console.WriteLine("Input the ID of the Developer you would like to remove:");

            int numID = new int();
            bool checkID = true;
            while (checkID)
            {
                string ID = Console.ReadLine();
                numID = int.Parse(ID);
                DevelopersPOCO selectedDeveloper = _developersRepo.GetDeveloperByID(numID);
                if (selectedDeveloper != null)
                {
                    checkID = false;
                }
                else
                {
                    Console.WriteLine("There is no Developer with this ID. Please provide an existing ID.");
                }
            }
            _developersRepo.DeleteItemFromList(numID);
        }

        private void UpdateDeveloper()
        {
            Console.Clear();

            //Get ID and verify they exist
            Console.WriteLine("Input the ID of the Developer you would like to update:");

            int numID = new int();
            bool checkID = true;
            while (checkID)
            {
                string ID = Console.ReadLine();
                numID = int.Parse(ID);
                DevelopersPOCO selectedDeveloper = _developersRepo.GetDeveloperByID(numID);
                if (selectedDeveloper != null)
                {
                    checkID = false;
                }
                else
                {
                    Console.WriteLine("There is no Developer with this ID. Please provide an existing ID.");
                }
            }

            //Create a new variable to input the new updated info
            DevelopersPOCO newDeveloper = new DevelopersPOCO();

            //First Name
            Console.WriteLine("Enter the Developer's First Name:");
            newDeveloper.FirstName = Console.ReadLine();

            //Last Name
            Console.WriteLine("Enter the Developer's Last Name:");
            newDeveloper.LastName = Console.ReadLine();

            //ID(check if not already used)
            Console.WriteLine("Enter the Developer's Unique ID number: ");

            bool checkNewID = true;
            while (checkNewID)
            {
                string ID = Console.ReadLine();
                int newID = int.Parse(ID);
                if (_developersRepo.GetDeveloperByID(newID) == null)
                {
                    newDeveloper.PersonalID = int.Parse(ID);
                    checkID = false;
                }
                else
                {
                    Console.WriteLine("This ID has been taken. Please pick a new one:");
                }
            }

            bool keepChecking = true;
            //Bool Has PluralSight
            while (keepChecking)
            {
                Console.WriteLine("Does this Developer has access to Pluralsight? (y/n):");
                string answer = Console.ReadLine().ToLower();

                if (answer == "y")
                {
                    newDeveloper.HasPluralsight = true;
                    keepChecking = false;
                }
                else if (answer == "n")
                {
                    newDeveloper.HasPluralsight = false;
                    keepChecking = false;
                }
                else
                {
                    Console.WriteLine("Please enter either y or n.");
                }
            }

            //update developer
            _developersRepo.UpdateListOfDevelopers(numID, newDeveloper);
        }
        
        private void AddNewTeam()
        {
            Console.Clear();

            DevTeamsPOCO newTeam = new DevTeamsPOCO();
            newTeam.TeamMembers = new List<DevelopersPOCO>();
            Console.WriteLine("Input the new team's name?");
            newTeam.TeamName = Console.ReadLine();

            Console.WriteLine("Input the new team's ID number?");
            bool teamSearch = true;
            while (teamSearch)
            {
                string input = Console.ReadLine().ToLower();
                int teamID = int.Parse(input);
                DevTeamsPOCO checkTeam = _devTeamRepo.GetTeamByID(teamID);
                if (checkTeam == null)
                {
                    newTeam.TeamID = teamID;
                    teamSearch = false;
                }
                else
                {
                    Console.WriteLine("That TeamID already exists. Please select an new TeamID:");
                }
            }

            _devTeamRepo.AddTeam(newTeam);

            Console.WriteLine("Your new team has been created!\n");
        }

        private void RemoveTeam()
        {
            Console.Clear();

            //Display a list of all teams to choose from
            ViewListOfTeams();

            Console.WriteLine("Enter the TeamID of the team you would like to remove");
            DevTeamsPOCO selectedTeam = new DevTeamsPOCO();
            bool teamSearch = true;
            while (teamSearch)
            {
                string input = Console.ReadLine().ToLower();
                int teamID = int.Parse(input);
                selectedTeam = _devTeamRepo.GetTeamByID(teamID);
                if (selectedTeam != null)
                {
                    teamSearch = false;
                }
                else
                {
                    Console.WriteLine("That TeamID does not currently exist. Please select an existing TeamID from the list above:");
                }
            }

            _devTeamRepo.DeleteTeam(selectedTeam.TeamID);
        }
        private void SeedContent()
        {

            DevelopersPOCO liz = new DevelopersPOCO("Liz", "Lemon", 2, true);
            DevelopersPOCO jack = new DevelopersPOCO("Jack", "Donaghy", 1, true);
            DevelopersPOCO lutz = new DevelopersPOCO("Kellan", "Lutz", 0, false);

            _developersRepo.AddDeveloperToList(liz);
            _developersRepo.AddDeveloperToList(jack);
            _developersRepo.AddDeveloperToList(lutz);

            List<DevelopersPOCO> dummies = _developersRepo.DisplayListOfDevelopers();

            DevTeamsPOCO dummydata = new DevTeamsPOCO(dummies, 30, "30 Rock");

            _devTeamRepo.AddTeam(dummydata);
        }
    }
}
