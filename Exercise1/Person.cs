using System;
using System.Collections.Generic;
using System.Text;

namespace Exercise1
{
    public class Person
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public int PhoneNumber { get; set; }

        public string Fullname => $"{Firstname} {Lastname}";
    }
}
