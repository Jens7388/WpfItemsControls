using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;

namespace ListViewExercise
{
    public class Repository
    {
        private string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "employees.txt");
        public static List<Employee> employees;

        public Repository()
        {
            GetEmployeesFromFile(path);
        }
        public List<Employee> GetAll()
        {
            return employees;
        }
        public static void Add(Employee employee)
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
                MessageBox.Show("Denne fil findes ikke, opretter ny fil..");
                using(StreamWriter writer = File.AppendText(path))
                {
                        writer.WriteLine("Richard,Head,Boss,4200000,06-09-1969");
                        writer.WriteLine("Ivan,Jerkov,Kok,69000,20-04-2004");
                        writer.WriteLine("Phuc,Yu,Chauffoer,66000,06-06-2006");                      
                }
                GetEmployeesFromFile(path);
                return false;
            }
        }
    }
}