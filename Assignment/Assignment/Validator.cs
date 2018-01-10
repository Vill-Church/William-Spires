using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Assignment
{
    class Validator
    {
        private String temp;
        
        public String GetTemp()
        {
            return temp;
        }

        public void SetTemp(String temp)
        {
            this.temp = temp;
        }
        public Boolean CheckForNumbers(String check)
        {
            Regex numbers = new Regex(".*\\d+.*");
            if (numbers.IsMatch(check))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
