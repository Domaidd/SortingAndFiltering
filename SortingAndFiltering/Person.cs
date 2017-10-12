using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingAndFiltering
{
    public class Person
    {
        private string name;
        private string surname;
        private string middlename;
        private int age;

        public string Name { get { return name; } set { name = value; } }
        public string Surname { get { return surname; } set { surname = value; } }
        public string Middlename { get { return middlename; } set { middlename = value; } }
        public int Age { get { return age; } set { age = value; } }

        public Person() { }

        public Person(string name, string surname, string middlename, int age)
        {
            Name = name;
            Surname = surname;
            Middlename = middlename;
            Age = age;
        }
    }
}
