using System;
using System.Collections.Generic;
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
            Repository.WriteToFile("C:/Users/jens7388/Documents/people.txt");
        }
       
        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Person selectedPerson = listBox.SelectedItem as Person;
            viewModel.SelectedPerson = selectedPerson;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            int.TryParse(phoneNumberInput.Text, out int phoneNumber);
            Person newPerson = new Person(firstNameInput.Text, lastNameInput.Text, emailInput.Text, phoneNumber);
            Repository.people.Add(newPerson);
            viewModel.People.Add(newPerson);
        }
    }
}
