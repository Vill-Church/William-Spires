using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{
    class Employee
    {
        private int id;
        private String firstName, lastName, emailAddress, PhoneNumber, Type;
        private String JoinDate, DateOfBirth;
        private Byte Age;

        public Employee(int id, String first, String last, String Join, String DOB, Byte age, String phone, String email, String type)
        {
            SetId(id);
            SetFirstName(first);
            SetLastName(last);
            SetJoinDate(Join);
            SetDateOfBirth(DOB);
            SetAge(age);
            SetPhoneNumber(phone);
            SetEmailAddress(email);
            SetType(type);
        }

        public void SetId(int id)
        {
            this.id = id;
        }
        public int GetId()
        {
            return id;
        }
        public void SetFirstName(String name)
        {
            firstName = name;
        }
        public String GetFirstName()
        {
            return firstName;
        }
        public void SetLastName(String name)
        {
            lastName = name;
        }
        public String GetLastName()
        {
            return lastName;
        }
        public void SetEmailAddress(String email)
        {
            emailAddress = email;
        }
        public String GetEmailAddress()
        {
            return emailAddress;
        }
        public void SetPhoneNumber(String number)
        {
            PhoneNumber = number;
        }
        public String GetPhoneNumber()
        {
            return PhoneNumber;
        }
        public void SetType(String Type)
        {
            this.Type = Type;
        }
        public String Gettype()
        {
            return Type;
        }
        public void SetJoinDate(String join)
        {
            JoinDate = join;
        }
        public String GetJoinDate()
        {
            return JoinDate;
        }
        public void SetDateOfBirth(String DOB)
        {
            DateOfBirth = DOB;
        }
        public String GetDateOfBirth()
        {
            return DateOfBirth;
        }
        public void SetAge(Byte Age)
        {
            this.Age = Age;
        }
        public Byte GetAge()
        {
            return Age;
        }
    }
}
