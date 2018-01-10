using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment
{
    class NameValidator : Validator
    {
        private String valueToCheck;
        public NameValidator(String temp)
        {
            SetValueToCheck(temp);
        }
        public void SetValueToCheck(String check)
        {
            valueToCheck = check;
        }
        public String GetValueToCheck()
        {
            return valueToCheck;
        }
        public Boolean CheckName(String name)
        {
            if (CheckForNumbers(name) == true)
            {
                MessageBox.Show("Names can't have numbers in them.");
                return false;
            } else
            {
                if (name.Length <= 30)
                {
                    return true;
                } else
                {
                    MessageBox.Show("Names can't be more than 30 characters.");
                    return false;
                }
            }
        }
    }
}
