using System;
using System.Collections.Generic;
using System.Text;

namespace ListViewExercise
{
    public class Employee
    {
        private string firstname;
        private string lastname;
        private string position;
        private int salary;
        private DateTime hireDate;
        public Employee(string firstname, string lastname, string position, int salary, DateTime hireDate)
        {
            Firstname = firstname;
            Lastname = lastname;
            Position = position;
            Salary = salary;
            HireDate = hireDate;
        }
        public string Firstname { get { return firstname; } set { firstname = value; } }
        public string Lastname { get { return lastname; } set { lastname = value; } }
        public string Position { get { return position; } set { position = value; } }
        public int Salary { get { return salary; } set { salary = value; } }
        public DateTime HireDate { get { return hireDate; } set { hireDate = value; } }
        public string Fullname => $"{Firstname} {Lastname}";
    }
}