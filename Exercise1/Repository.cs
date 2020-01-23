using System;
using System.Collections.Generic;
using System.Text;

namespace Exercise1
{
    public class Repository
    {
        public static List<Person> people;

        public Repository()
        {
            people = new List<Person>()
            {
                new Person("Richard", "Head","HeadBoss@mail.co.ck", 42069666),
                new Person("Ivan", "Jerkov", "Jerkov@mail.ru", 12345678),
                new Person("Phuc", "Yu", "PhucYu@mail.as", 98765432)                
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