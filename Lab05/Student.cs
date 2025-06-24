using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab05
{

    internal class Student
    {
        public string Name { get; set; }
        public int MSSV { get; set; }
        public double Grade { get; set; }
        public string Class { get; set; }
        public Student()
        {
            Name = string.Empty;
            MSSV = 0;
            Grade = 0.0;
            Class = string.Empty;
        }
        public Student(string name, int mssv, double grade, string Class)
        {
            Name = name;
            MSSV = mssv;
            Grade = grade;
            this.Class = Class;
        }
        public override string ToString()
        {
            return "\nName: " + Name + " MSSV: " + MSSV + "\nGrade: " + Grade + " Class: " + Class;
        }
    }
}
