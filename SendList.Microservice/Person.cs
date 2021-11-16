using System;

namespace SendList.Microservice
{
    public class Person
    {
        private DateTime date;
        private string fullname;
        private int age;
        private string profession;

        public DateTime Date { get => date; set => date = value; }
        public string FullName { get => fullname; set => fullname = value; }
        public int Age { get => age; set => age = value; }
        public string Profession { get => profession; set => profession = value; }
        public Person(DateTime date, string fulname, int age, string profestion)
        {
            Date = date;
            FullName = fulname;
            Age = age;
            Profession = profestion;
        }

    }
}