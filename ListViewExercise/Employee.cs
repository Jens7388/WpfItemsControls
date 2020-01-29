using System;
using System.Collections.Generic;
using System.Text;

namespace ListViewExercise
{
    public class Employee
    {
        public Employee(string firstname, string lastname, string position, int salary, DateTime hireDate)
        {
            Firstname = firstname;
            Lastname = lastname;
            Position = position;
            Salary = salary;
            HireDate = hireDate;
        }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Position { get; set; }
        public int Salary { get; set; }
        public DateTime HireDate { get; set; }
        public string Fullname => $"{Firstname} {Lastname}";
    }
}