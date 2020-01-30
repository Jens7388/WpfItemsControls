using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ListViewExercise
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow: Window
    {
        private ViewModel viewModel;
        public MainWindow()
        {
            InitializeComponent();
            viewModel = new ViewModel();
            DataContext = viewModel;
        }
        private string path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "employees.txt");
        public static List<Employee> employees;
        static string oldEmployeeData;
        static List<Employee> nonEditedEmployees;
        string document;
        public void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Employee selectedEmployee = listView_employees.SelectedItem as Employee;
            if(selectedEmployee != null)
            {
                oldEmployeeData = $"{selectedEmployee.Firstname},{selectedEmployee.Lastname},{selectedEmployee.Position},{selectedEmployee.Salary},{selectedEmployee.HireDate.ToString("dd-MM-yyyy")}";
            }
            viewModel.SelectedEmployee = selectedEmployee;
        }
        public bool WriteToFile(string path)
        {
            bool fileExists = File.Exists(path);
            if(fileExists)
            {
                int.TryParse(textBoxSalary.Text, out int salary);
                DateTime.TryParse(datePickerHireDate.Text, out DateTime hireDate);
                if(salary != 0 && textBoxFirstName.Text != null && textBoxLastName.Text != null && textBoxPosition.Text != null && hireDate.Year != 0000)
                {
                    Employee newEmployee = new Employee(
                        textBoxFirstName.Text, 
                        textBoxLastName.Text, 
                        textBoxPosition.Text, 
                        salary, hireDate);
                    viewModel.Employees.Add(newEmployee);
                    using(StreamWriter sr = File.AppendText(path))
                    {
                        sr.WriteLine($"{newEmployee.Firstname},{newEmployee.Lastname},{newEmployee.Position},{newEmployee.Salary},{newEmployee.HireDate.ToString("dd-MM-yyyy")}");
                        return true;
                    }
                }
                else if(salary == 0)
                {
                    MessageBox.Show("Ugyldigt telefonnummer! Prøv igen.");
                    return false;
                }
                else if(hireDate.Year == 0001)
                {
                    MessageBox.Show("Ugyldig ansættelsesdato! Prøv igen.");
                    return false;
                }
                else
                {
                    MessageBox.Show("Du mangler at udfylde et felt! Prøv igen");
                    return false;
                }
            }
            else
            {
                MessageBox.Show("ADVARSEL! Kunne ikke forbinde til textfilen, tjek din sti!");
                return false;
            }
        }
        public bool editEmployeeFromFile(string path)
        {
            bool fileExists = File.Exists(path);
            if(fileExists)
            {
                int.TryParse(textBoxSalary.Text, out int salary);
                DateTime.TryParse(datePickerHireDate.Text, out DateTime hireDate);
                if(salary != 0 && textBoxFirstName.Text != null && textBoxLastName.Text != null && textBoxPosition.Text != null && hireDate.Year != 0001)
                {
                    Employee newEmployee = new Employee(
                        textBoxFirstName.Text,
                        textBoxLastName.Text,
                        textBoxPosition.Text,
                        salary, hireDate);
                    using(StreamReader reader = new StreamReader(path, Encoding.Default))
                    {
                        nonEditedEmployees = new List<Employee>();
                        document = "";
                        string firstName = "";
                        string lastName = "";
                        string position = "";
                        salary = 0;
                        hireDate = new DateTime();
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
                            for(int i = 4; i < employeeData.Length; i++)
                            {
                                DateTime.TryParse(employeeData[i], out hireDate);
                            }
                            Employee employee = new Employee(firstName, lastName, position, salary, hireDate);
                            nonEditedEmployees.Add(employee);
                        }
                    }
                    using(StreamReader reader = new StreamReader(path, Encoding.Default))
                    {
                        document = "";
                        while((document = reader.ReadLine()) != null)
                        {
                            if(document == oldEmployeeData)
                            {
                                reader.Close();
                                File.WriteAllText(path, $"{newEmployee.Firstname},{newEmployee.Lastname},{newEmployee.Position},{newEmployee.Salary},{newEmployee.HireDate.ToString("dd-MM-yyyy")}\n");
                                using(StreamWriter sr = File.AppendText(path))
                                {
                                    viewModel.Employees.Clear();
                                    viewModel.Employees.Add(newEmployee);
                                    for(int i = 0; i < nonEditedEmployees.Count; i++)
                                    {
                                        if($"{nonEditedEmployees[i].Firstname},{nonEditedEmployees[i].Lastname},{nonEditedEmployees[i].Position},{nonEditedEmployees[i].Salary},{nonEditedEmployees[i].HireDate.ToString("dd-MM-yyyy")}" == oldEmployeeData)
                                        {

                                        }
                                        else
                                        {
                                            sr.WriteLine($"{nonEditedEmployees[i].Firstname},{nonEditedEmployees[i].Lastname},{nonEditedEmployees[i].Position},{nonEditedEmployees[i].Salary},{nonEditedEmployees[i].HireDate.ToString("dd-MM-yyyy")}");
                                            viewModel.Employees.Add(nonEditedEmployees[i]);
                                        }
                                    }
                                    return true;
                                }
                            }
                        }
                    }
                }
                else if(salary == 0)
                {
                    MessageBox.Show("Ugyldigt telefonnummer! prøv igen");
                    return false;
                }
                else if(hireDate.Year == 0001)
                {
                    MessageBox.Show("Ugyldig ansættelsesdato! Prøv igen");
                    return false;
                }
                else
                {
                    MessageBox.Show("Du mangler at udfylde et felt! Prøv igen");
                    return false;
                }
                return true;
            }
            else
            {
                MessageBox.Show("ADVARSEL! Kunne ikke forbinde til textfilen, tjek din sti!");
                return false;
            }
        }
        private void buttonAllowEdit_Click(object sender, RoutedEventArgs e)
        {
            textBoxFirstName.IsReadOnly = false;
            textBoxFirstName.BorderThickness = new Thickness(1);
            textBoxLastName.IsReadOnly = false;
            textBoxLastName.BorderThickness = new Thickness(1);
            textBoxPosition.IsReadOnly = false;
            textBoxPosition.BorderThickness = new Thickness(1);
            datePickerHireDate.IsEnabled = true;
            datePickerHireDate.BorderThickness = new Thickness(1);
            textBoxSalary.IsReadOnly = false;
            textBoxSalary.BorderThickness = new Thickness(1);
        }
        private void buttonSaveAsNewEmployee_Click(object sender, RoutedEventArgs e)
        {
            WriteToFile(path);
        }
        private void buttonSaveEdit_Click(object sender, RoutedEventArgs e)
        {
            editEmployeeFromFile(path);
        }
    }
}