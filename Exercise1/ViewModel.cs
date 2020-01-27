using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Exercise1
{
    public class ViewModel
    {
        private Repository repository;

        public ViewModel()
        {
            repository = new Repository();
            List<Person> people = repository.GetAll();
            People = new ObservableCollection<Person>(people);
        }

        public ObservableCollection<Person> People { get; set; }

        public Person SelectedPerson { get; set; }
    }
}