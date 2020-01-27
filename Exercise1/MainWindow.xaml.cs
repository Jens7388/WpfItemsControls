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

        static string oldPersonData;
        public void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Person selectedPerson = listBox.SelectedItem as Person;
            oldPersonData = $"{selectedPerson.Firstname},{selectedPerson.Lastname},{selectedPerson.Email},{selectedPerson.PhoneNumber}";
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
                if(phoneNumber != 0 && firstNameInput.Text != null && lastNameInput.Text != null && emailInput.Text != null)
                {
                    Person newPerson = new Person(firstNameInput.Text, lastNameInput.Text, emailInput.Text, phoneNumber);
                    viewModel.People.Add(newPerson);
                    using(StreamWriter sr = File.AppendText(path))
                    {
                        sr.WriteLine($"{newPerson.Firstname},{newPerson.Lastname},{newPerson.Email},{newPerson.PhoneNumber}");
                        return true;
                    }
                }
                else if(phoneNumber == 0)
                {
                    MessageBox.Show("Ugyldigt telefonnummer! prøv igen");
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
        public bool editPersonFromFile(string path)
         {
             bool fileExists = File.Exists(path);
             if(fileExists)
             {
                 int.TryParse(selectedPhoneNumber.Text, out int phoneNumber);
                 if(phoneNumber != 0 && selectedFirstName.Text != null && selectedLastName.Text != null && selectedEmail.Text != null)
                 {
                     Person newPerson = new Person(
                         selectedFirstName.Text,
                         selectedLastName.Text,
                         selectedEmail.Text,
                         phoneNumber);
                     using(StreamReader reader = new StreamReader(path, Encoding.Default))
                     {
                         string document = "";
                         while((document = reader.ReadLine()) != null)
                         {
                             if(document == oldPersonData)
                             {
                                reader.Close();
                                
                                using(StreamWriter sr = File.AppendText(path))
                                 {
                                    sr.WriteLine($"{newPerson.Firstname},{newPerson.Lastname},{newPerson.Email},{newPerson.PhoneNumber}");
                                    viewModel.People.Remove(viewModel.SelectedPerson);
                                    for(int i = 0; i < viewModel.People.Count; i++)
                                    {
                                        sr.WriteLine($"{viewModel.People[i].Firstname},{viewModel.People[i].Lastname},{viewModel.People[i].Email},{viewModel.People[i].PhoneNumber}");
                                    }                           
                                     return true;
                                 }
                             }
                         }
                     }
                     viewModel.People.Add(newPerson);
                 }
                 else if(phoneNumber == 0)
                 {
                     MessageBox.Show("Ugyldigt telefonnummer! prøv igen");
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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            selectedFirstName.IsReadOnly = false;
            selectedLastName.IsReadOnly = false;
            selectedEmail.IsReadOnly = false;
            selectedPhoneNumber.IsReadOnly = false;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            editPersonFromFile("C:/Users/jens7388/Documents/people.txt");
        }
    }
}