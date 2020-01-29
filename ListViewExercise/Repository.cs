using System;
using System.Collections.Generic;
using System.Text;

namespace ListViewExercise
{
    public class Repository
    {
        public static List<Employee> employees;

        public Repository()
        {
            employees = new List<Employee>()
            {
            new Employee("Richard", "Head", "Boss", 420000, new DateTime(1969, 9, 6)),
            new Employee("Ivan", "Jerkov", "Kok", 69000, new DateTime(2004, 4, 20)),
            new Employee("Phuc", "Yu", "Chauffør", 66600, new DateTime(2006, 6, 6))
            };
        }
        public List<Employee> GetAll()
        {
            return employees;
        }

        public void Add(Employee employee)
        {
            employees.Add(employee);
        }
    }
}

