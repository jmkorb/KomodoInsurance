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
                    "4. Remove a Developer\n" +
                    "5. See who needs PluralSight\n" +
                    "6. View Team Options\n" + 
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
                        CheckForPluralsight();
                        break;
                    case "6":
                        TeamMenu();
                        break;
                    //0 because zero is one less button to type for exiting
                    case "0":
                        Console.WriteLine("Closing. . .");
                        keepRunning = false;
                        break;
                    default:
                        Console.WriteLine("Please pick and option from the menu.");
                        break;
                }



            }
        }
        private void ViewListOfDevelopers()
        {
            Console.Clear();

            List<DevelopersPOCO> developers = _developersRepo.DisplayListOfDevelopers();

            foreach (DevelopersPOCO person in developers)
            {
                Console.WriteLine($"Name: {person.FirstName} {person.LastName}  ID: {person.PersonalID}  Has Pluralsight Access?: {person.HasPluralsight}");
            }

            if (developers.Count == 0)
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
                Console.WriteLine($"Name: {person.FirstName} {person.LastName}  ID: {person.PersonalID}  Has Pluralsight Access?: {person.HasPluralsight}");
            }
            if(selectedTeam.TeamMembers.Count == 0)
            {
                Console.WriteLine("This team has no developers currently.");
            }
            Console.WriteLine();
        }
        private void CreateNewDeveloper()
        {
            Console.Clear();

            bool keepAddingDevelopers = true;
            while (keepAddingDevelopers)
            {
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
                        newDeveloper.HasPluralsight = YesNo.Yes;
                        keepChecking = false;
                    }
                    else if (answer == "n")
                    {
                        newDeveloper.HasPluralsight = YesNo.No;
                        keepChecking = false;
                    }
                    else
                    {
                        Console.WriteLine("Please enter either y or n.");
                    }
                }

                _developersRepo.AddDeveloperToList(newDeveloper);
                Console.WriteLine("Your developer has been created! Would you like to add any more? (y/n)");
                string keepAdding = Console.ReadLine().ToLower();
                if (keepAdding == "n")
                {
                    keepAddingDevelopers = false;
                }
            }
        }
        private void DeleteDeveloper()
        {
            Console.Clear();

            ViewListOfDevelopers();

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
            Console.WriteLine("The developer has been deleted.\n");
        }
        private void UpdateDeveloper()
        {
            Console.Clear();

            ViewListOfDevelopers();
            //Get ID and verify they exist
            Console.WriteLine("Input the ID of the Developer you would like to update:");

            int numID = new int();
            DevelopersPOCO selectedDeveloper = new DevelopersPOCO();
            bool checkID = true;
            while (checkID)
            {
                string ID = Console.ReadLine();
                numID = int.Parse(ID);
                selectedDeveloper = _developersRepo.GetDeveloperByID(numID);
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
                if (_developersRepo.GetDeveloperByID(newID).PersonalID == selectedDeveloper.PersonalID)
                {
                    newDeveloper.PersonalID = int.Parse(ID);
                    checkNewID = false;
                }
                else if (_developersRepo.GetDeveloperByID(newID) == null)
                {
                    newDeveloper.PersonalID = int.Parse(ID);
                    checkNewID = false;
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
                    newDeveloper.HasPluralsight = YesNo.Yes;
                    keepChecking = false;
                }
                else if (answer == "n")
                {
                    newDeveloper.HasPluralsight = YesNo.No;
                    keepChecking = false;
                }
                else
                {
                    Console.WriteLine("Please enter either y or n.");
                }
            }

            //update developer
            _developersRepo.UpdateListOfDevelopers(numID, newDeveloper);
            Console.WriteLine("The developer has been updated.\n");
        }
        private void CheckForPluralsight()
        {
            Console.WriteLine();

            List<DevelopersPOCO> developers = _developersRepo.DisplayListOfDevelopers();

            foreach (DevelopersPOCO person in developers)
            {
                if(person.HasPluralsight == YesNo.No)
                {
                    Console.WriteLine($"Name: {person.FirstName} {person.LastName}  ID: {person.PersonalID}  Has Pluralsight Access?: {person.HasPluralsight}");
                }
            }

            Console.WriteLine();
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
            Console.WriteLine("The team has been deleted.");
        }
        private void UpdateTeam()
        {
            Console.Clear();

            //Display a list of all teams to choose from
            ViewListOfTeams();

            Console.WriteLine("Enter the TeamID of the team you would like to update:");
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

            DevTeamsPOCO newTeam = new DevTeamsPOCO();
            Console.WriteLine("Input the team's name?");
            newTeam.TeamName = Console.ReadLine();

            Console.WriteLine("Input the team's ID number?");
            bool teamSearchAgain = true;
            while (teamSearchAgain)
            {
                string input = Console.ReadLine().ToLower();
                int teamID = int.Parse(input);
                DevTeamsPOCO checkTeam = _devTeamRepo.GetTeamByID(teamID);
                if (checkTeam == selectedTeam)
                {
                    newTeam.TeamID = teamID;
                    teamSearchAgain = false;
                }
                else if (checkTeam == null)
                {
                    newTeam.TeamID = teamID;
                    teamSearchAgain = false;
                }
                else
                {
                    Console.WriteLine("That TeamID already exists. Please select an new TeamID:");
                }
            }
            _devTeamRepo.UpdateTeam(newTeam, selectedTeam);
            Console.WriteLine("The team has been updated!\n");
        }
        private void MoveTeamMember()
        {
            bool keepMovingDevelopers = true;
            while(keepMovingDevelopers)
            {
                Console.Clear();

                ViewListOfDevelopers();

                Console.WriteLine("Enter the ID of the developer you want to move?");

                //Check the developer exists and then select them
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
                DevelopersPOCO developer = _developersRepo.GetDeveloperByID(numID);
                int currentTeam = new int();

                List<DevTeamsPOCO> devTeamList = _devTeamRepo.ViewTeams();

                //Find the developers current team, if any, so that it will be removed later
                foreach (DevTeamsPOCO team in devTeamList)
                {
                    foreach (DevelopersPOCO person in team.TeamMembers)
                    {
                        if (person.PersonalID == developer.PersonalID)
                        {
                            currentTeam = team.TeamID;
                        }
                    }
                }

                ViewListOfTeams();

                //Get the team you're moving the developer to
                Console.WriteLine("Enter the TeamID of the team you would like to add them to:");

                int newTeam = new int();
                bool teamSearch = true;
                while (teamSearch)
                {
                    string input = Console.ReadLine().ToLower();
                    newTeam = int.Parse(input);
                    DevTeamsPOCO selectedTeam = _devTeamRepo.GetTeamByID(newTeam);
                    if (selectedTeam != null)
                    {
                        teamSearch = false;
                    }
                    else
                    {
                        Console.WriteLine("That TeamID does not currently exist. Please select an existing TeamID from the list above:");
                    }
                }

                _devTeamRepo.MoveMember(currentTeam, newTeam, developer);
                Console.WriteLine("The developer has been successfully moved to their new team!\n" +
                                "Would you like to move any more? (y/n)");
                string keepMoving = Console.ReadLine().ToLower();
                if (keepMoving == "n")
                {
                    Console.Clear();
                    keepMovingDevelopers = false;
                }

            }
        }
        private void TeamMenu()
        {
            Console.Clear();

            bool keepTeamMenu = true;
            while (keepTeamMenu)
            {
                Console.WriteLine("What would you like to do?\n" +
                                    "1. View List of Teams\n" +
                                    "2. View a Team's List of Developers\n" +
                                    "3. Add a New Team\n" +
                                    "4. Remove a Team\n" +
                                    "5. Update an existing Team\n" +
                                    "6. Add/Move a Developer to a team.\n" +
                                    "0. Go back to Developer Options");

                string response = Console.ReadLine();
                switch (response)
                {
                    case "1":
                        ViewListOfTeams();
                        break;
                    case "2":
                        ViewListOfTeamMembers();
                        break;
                    case "3":
                        AddNewTeam();
                        break;
                    case "4":
                        RemoveTeam();
                        break;
                    case "5":
                        UpdateTeam();
                        break;
                    case "6":
                        MoveTeamMember();
                        break;
                    case "0":
                        Console.Clear();
                        keepTeamMenu = false;
                        break;
                    default:
                        Console.WriteLine("Please pick and option from the menu.");
                        break;
                }
            }
        }
        private void SeedContent()
        {

            DevelopersPOCO liz = new DevelopersPOCO("Liz", "Lemon", 2, YesNo.Yes);
            DevelopersPOCO jack = new DevelopersPOCO("Jack", "Donaghy", 1, YesNo.Yes);
            DevelopersPOCO lutz = new DevelopersPOCO("Kellan", "Lutz", 0, YesNo.No);

            _developersRepo.AddDeveloperToList(liz);
            _developersRepo.AddDeveloperToList(jack);
            _developersRepo.AddDeveloperToList(lutz);

            List<DevelopersPOCO> dummies = new List<DevelopersPOCO>
            {
                liz,jack,lutz
            };

            DevTeamsPOCO dummyData = new DevTeamsPOCO(dummies, 30, "30 Rock");
            DevTeamsPOCO otherDummyData = new DevTeamsPOCO(new List<DevelopersPOCO>(), 2, "The Nobodies");

            _devTeamRepo.AddTeam(dummyData);
            _devTeamRepo.AddTeam(otherDummyData);
        }
    }
}
