using System;

namespace ReceiveList.Microservice
{
    public class Person
    {
        private DateTime date;
        private string fullname;
        private string profession;

        public DateTime Date { get => date; set => date = value; }
        public string FullName { get => fullname; set => fullname = value; }
        public string Profession { get => profession; set => profession = value; }
        public Person(DateTime date, string fullname, string profestion)
        {
            Date = date;
            FullName = fullname;
            Profession = profestion;
        }

        public override string ToString()
        {
            return "Date: " + Date + " Fulname: " + FullName + " Profestion: " + Profession;
        }
    }
}