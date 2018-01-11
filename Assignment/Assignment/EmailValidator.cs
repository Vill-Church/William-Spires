using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{
    class EmailValidator : Validator
    {
        private string EmailToCheck;
        public EmailValidator(string value)
        {
            EmailToCheck = value;
        }
        public bool CheckEmail()
        {
            try
            {
                var address = new System.Net.Mail.MailAddress(EmailToCheck);
                return address.Address == EmailToCheck;
            }
            catch
            {
                return false;
            }
        }
    }
}
