using System;
using System.Collections.Generic;
using System.Text;

namespace Exercise1
{
    public class Repository
    {
        private List<Person> people;

        public Repository()
        {
            people = new List<Person>()
            {
                new Person() {Firstname = "Richard", Lastname = "Head", Email = "HeadBoss@mail.co.ck", PhoneNumber = 42069666},
                new Person() {Firstname = "Ivan", Lastname = "Jerkov", Email = "Jerkov@mail.ru", PhoneNumber = 12345678},
                new Person() {Firstname = "Phuc", Lastname = "Yu", Email = "PhucYu@mail.as", PhoneNumber = 98765432}
            };
        }
        public List<Person> GetAll()
        {
            return people;
        }
        public void Add(Person person)
        {
            people.Add(person);
        }
    }
}
