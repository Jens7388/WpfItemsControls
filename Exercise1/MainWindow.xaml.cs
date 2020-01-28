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

namespace ListboxExercise
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
        static List<Person> nonEditedPeople;
        string document;
        public void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Person selectedPerson = listBox.SelectedItem as Person;
            if(selectedPerson != null)
            {
                oldPersonData = $"{selectedPerson.Firstname},{selectedPerson.Lastname},{selectedPerson.Email},{selectedPerson.PhoneNumber}";
            }
            viewModel.SelectedPerson = selectedPerson;
        }

        private void AddPerson(object sender, RoutedEventArgs e)
        {
            WriteToFile("C:/Users/jens7388/Documents/people.txt");
        }
        public bool WriteToFile(string path)
        {
            bool fileExists = File.Exists(path);
            if(fileExists)
            {
                int.TryParse(selectedPhoneNumber.Text, out int phoneNumber);
                if(phoneNumber != 0 && selectedFirstName.Text != null && selectedLastName.Text != null && selectedEmail.Text != null)
                {
                    Person newPerson = new Person(selectedFirstName.Text, selectedLastName.Text, selectedEmail.Text, phoneNumber);
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
                        nonEditedPeople = new List<Person>();
                        document = "";
                        string firstName = "";
                        string lastName = "";
                        string email = "";
                        phoneNumber = 0;
                        while((document = reader.ReadLine()) != null)
                        {
                            string[] personData = document.Split(',');

                            for(int i = 0; i < personData.Length; i += 4)
                            {
                                firstName = personData[i];
                            }
                            for(int i = 1; i < personData.Length; i += 4)
                            {
                                lastName = personData[i];
                            }
                            for(int i = 2; i < personData.Length; i += 4)
                            {
                                email = personData[i];
                            }
                            for(int i = 3; i < personData.Length; i += 4)
                            {
                                int.TryParse(personData[i], out phoneNumber);
                            }

                            Person person = new Person(firstName, lastName, email, phoneNumber);
                            nonEditedPeople.Add(person);
                        }
                    }
                    using(StreamReader reader = new StreamReader(path, Encoding.Default))
                    {
                        document = "";
                        while((document = reader.ReadLine()) != null)
                        {
                            if(document == oldPersonData)
                            {
                                reader.Close();
                                File.WriteAllText(path, $"{newPerson.Firstname},{newPerson.Lastname},{newPerson.Email},{newPerson.PhoneNumber}\n");
                                using(StreamWriter sr = File.AppendText(path))
                                {
                                    viewModel.People.Clear();
                                    viewModel.People.Add(newPerson);
                                    for(int i = 0; i < nonEditedPeople.Count; i++)
                                    {
                                        if($"{nonEditedPeople[i].Firstname},{nonEditedPeople[i].Lastname},{nonEditedPeople[i].Email},{nonEditedPeople[i].PhoneNumber}" == oldPersonData)
                                        {

                                        }
                                        else
                                        {
                                            sr.WriteLine($"{nonEditedPeople[i].Firstname},{nonEditedPeople[i].Lastname},{nonEditedPeople[i].Email},{nonEditedPeople[i].PhoneNumber}");
                                            viewModel.People.Add(nonEditedPeople[i]);
                                        }
                                    }
                                    return true;
                                }
                            }
                        }
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
                return true;
            }
            else
            {
                MessageBox.Show("ADVARSEL! Kunne ikke forbinde til textfilen, tjek din sti!");
                return false;
            }
        }
        private void EnableEditing(object sender, RoutedEventArgs e)
        {
            selectedFirstName.IsReadOnly = false;
            selectedFirstName.BorderThickness = new Thickness(1); 
            selectedLastName.IsReadOnly = false;
            selectedLastName.BorderThickness = new Thickness(1);
            selectedEmail.IsReadOnly = false;
            selectedEmail.BorderThickness = new Thickness(1);
            selectedPhoneNumber.IsReadOnly = false;
            selectedPhoneNumber.BorderThickness = new Thickness(1);
        }
        private void SaveEdit(object sender, RoutedEventArgs e)
        {
            editPersonFromFile("C:/Users/jens7388/Documents/people.txt");
        }
    }
}