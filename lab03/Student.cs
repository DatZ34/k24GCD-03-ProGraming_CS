using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab03
{
    internal class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Student() 
        {
            Id = -1;
            Name = "";
        }
        public Student(int id, string name)
        {
            Id = id;
            Name = name;
        }
        public override string ToString()
        {
            return $"Student ID = {Id}, Name={Name}";
        }
    }
}
