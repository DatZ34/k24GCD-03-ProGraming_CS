using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_04
{

    internal class Student
    {
        public string Name { get; set; }
        public int MSSV { get; set; }
        public string Mail { get; set; }
        public string Class { get; set; }
        public Student()
        {
            Name = string.Empty;
            MSSV = 0;
            Mail = string.Empty;
            Class = string.Empty;
        }
        public Student(string name, int mssv, string mail, string Class)
        {
            Name = name;
            MSSV = mssv;
            Mail = mail;
            this.Class = Class;
        }
        public override string ToString()
        {
            return "\nName: " + Name + " MSSV: " + MSSV +"\nMail: " + Mail + " Class: " + Class;
        }
    }
}
