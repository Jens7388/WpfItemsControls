using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;

namespace ListViewExercise
{
    public class Repository
    {
        public static List<Employee> employees;

        public Repository()
        {
               GetEmployeesFromFile("C:/Users/jens7388/Documents/employees.txt");
        }
        public List<Employee> GetAll()
        {
            return employees;
        }
        public void Add(Employee employee)
        {
            employees.Add(employee);
        }
        public static bool GetEmployeesFromFile(string path)
        {
            employees = new List<Employee>();
            bool fileExists = File.Exists(path);
            if(fileExists)
            {
                using(StreamReader reader = new StreamReader(path, Encoding.Default))
                {
                    string document = "";
                    string firstName = "";
                    string lastName = "";
                    string position = "";
                    int salary = 0;
                    DateTime hireDate = new DateTime();
                    while((document = reader.ReadLine()) != null)
                    {
                        string[] employeeData = document.Split(',');
                        for(int i = 0; i < employeeData.Length; i += 5)
                        {
                            firstName = employeeData[i];
                        }
                        for(int i = 1; i < employeeData.Length; i += 5)
                        {
                            lastName = employeeData[i];
                        }
                        for(int i = 2; i < employeeData.Length; i += 5)
                        {
                            position = employeeData[i];
                        }
                        for(int i = 3; i < employeeData.Length; i += 5)
                        {
                            int.TryParse(employeeData[i], out salary);
                        }
                        for(int i = 4; i < employeeData.Length; i += 5)
                        {
                            DateTime.TryParse(employeeData[i], out hireDate);
                        }
                        Employee employee = new Employee(firstName, lastName, position, salary, hireDate);
                        employees.Add(employee);
                    }
                }
                return true;
            }
            else
            {
                MessageBox.Show("ADVARSEL! Kunne ikke forbinde til textfilen, tjek din sti!");
                Environment.Exit(0);
                return false;
            }
        }
    }
}