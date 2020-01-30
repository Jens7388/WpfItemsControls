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
        static string oldEmployeeData;
        static List<Employee> nonEditedEmployees;
        string document;
        public void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Employee selectedEmployee = listView_employees.SelectedItem as Employee;
            if(selectedEmployee != null)
            {
                oldEmployeeData = $"{selectedEmployee.Firstname},{selectedEmployee.Lastname},{selectedEmployee.Position},{selectedEmployee.Salary},{selectedEmployee.HireDate}";
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
                    Employee newEmployee = new Employee(textBoxFirstName.Text, textBoxLastName.Text, textBoxPosition.Text, salary, hireDate);
                    viewModel.Employees.Add(newEmployee);
                    using(StreamWriter sr = File.AppendText(path))
                    {
                        sr.WriteLine($"{newEmployee.Firstname},{newEmployee.Lastname},{newEmployee.Position},{newEmployee.Salary},{newEmployee.HireDate.ToString("yyyy-M-d")}");
                        return true;
                    }
                }
                else if(salary == 0)
                {
                    MessageBox.Show("Ugyldigt telefonnummer! Prøv igen.");
                    return false;
                }
                else if(hireDate.Year == 0000)
                {
                    MessageBox.Show("Ugyldig dato! Prøv igen.");
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
            WriteToFile("C:/Users/jens7388/Documents/employees.txt");
        }
    }
}