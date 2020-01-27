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

namespace Exercise1
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

        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Person selectedPerson = listBox.SelectedItem as Person;
            viewModel.SelectedPerson = selectedPerson;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            WriteToFile("C:/Users/jens7388/Documents/people.txt");
        }
        public bool WriteToFile(string path)
        {
            bool fileExists = File.Exists(path);
            if(fileExists)
            {
                int.TryParse(phoneNumberInput.Text, out int phoneNumber);
                if(phoneNumber != 0)
                {
                    Person newPerson = new Person(firstNameInput.Text, lastNameInput.Text, emailInput.Text, phoneNumber);
                    viewModel.People.Add(newPerson);
                    using(StreamWriter sr = File.AppendText(path))
                    {
                        sr.WriteLine($"{newPerson.Firstname},{newPerson.Lastname},{newPerson.Email},{newPerson.PhoneNumber}");
                        return true;
                    }
                }
                else
                {
                    MessageBox.Show("Ugyldigt telefonnummer! prøv igen");
                    return false;
                }
            }
            else
            {              
                return false;
            }
        }
    }
}
