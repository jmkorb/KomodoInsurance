using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Developers
{
    public class DevelopersPOCO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int PersonalID { get; set; }
        public YesNo HasPluralsight { get; set; }


        public DevelopersPOCO() { }

        public DevelopersPOCO(string firstName, string lastName, int personalID, YesNo hasPluralsight)
        {
            FirstName = firstName;
            LastName = lastName;
            PersonalID = personalID;
            HasPluralsight = hasPluralsight;
        }


    }
    
    public enum YesNo
    {
        Yes,
        No
    }
}
