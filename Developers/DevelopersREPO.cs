using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Developers
{
    public class DevelopersREPO
    {
        private List<DevelopersPOCO> _listOfDevelopers = new List<DevelopersPOCO>();
        //Create
        public void AddDeveloperToList(DevelopersPOCO person)
        {
            _listOfDevelopers.Add(person);
        }
        //Read
        public List <DevelopersPOCO> DisplayListOfDevelopers()
        {
            return _listOfDevelopers;
        }

        //Update
        public bool UpdateListOfDevelopers(int oldID, DevelopersPOCO newPerson)
        {
            DevelopersPOCO oldPerson = GetDeveloperByID(oldID);

            if(oldPerson != null)
            {
                oldPerson.FirstName = newPerson.FirstName;
                oldPerson.LastName = newPerson.LastName;
                oldPerson.PersonalID = newPerson.PersonalID;
                oldPerson.HasPluralsight = newPerson.HasPluralsight;

                return true;
            }
            else
            {
                return false;
            }
                
        }

        //Delete

        //Should these be bools? or just void?
        public bool DeleteItemFromList(int id)
        {
            DevelopersPOCO person = GetDeveloperByID(id);

            if(person == null)
            {
                return false;
            }

            int initialNumber = _listOfDevelopers.Count;

            _listOfDevelopers.Remove(person);

            if(_listOfDevelopers.Count < initialNumber)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public DevelopersPOCO GetDeveloperByID(int number)
        {
            foreach(DevelopersPOCO person in _listOfDevelopers)
            {
                if(number == person.PersonalID)
                {
                    return person;
                }
            }
            return null;
        }
    }
}
