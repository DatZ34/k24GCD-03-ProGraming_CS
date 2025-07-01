using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab10
{
    internal class Customer
    {
        
        public string CustomerID { get; set; }
        public string Contactname { get; set; }
        public string City { get; set; }

        public Customer()
        {
            CustomerID = string.Empty;
            Contactname = string.Empty;
            City = string.Empty;
        }
        public Customer(string customer, string contactname, string city)
        {
            CustomerID = customer;
            Contactname = contactname;
            City = city;
        }
    }
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Occupation { get; set; }


        public int Age { get; set; }
        public int CompanyId { get; set; }

        public static List<Person> GenerateListOfPeople()
        {
            var people = new List<Person>();
            people.Add(new Person { FirstName = "Eric", LastName = "Fleming", Occupation = "Dev", Age = 24, CompanyId = 1, });
            people.Add(new Person { FirstName = "Steve", LastName = "Smith", Occupation = "Manager", Age = 40, CompanyId = 1 });
            people.Add(new Person { FirstName = "Brendan", LastName = "Enrick", Occupation = "Dev", Age = 30, CompanyId = 2 });
            people.Add(new Person { FirstName = "Jane", LastName = "Doe", Occupation = "Dev", Age = 35, CompanyId = 1 });
            people.Add(new Person { FirstName = "Samantha", LastName = "Jones", Occupation = "Dev", Age = 24, CompanyId = 2 });
            return people;
        }
        public Person()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            Occupation = string.Empty;
            Age = 0;
            CompanyId = 0;
        }
        public Person(string firstName, string lastName, string occupation, int age , int companyId)
        {
            FirstName = firstName;
            LastName = lastName;
            Occupation = occupation;
            Age = age;
            CompanyId = companyId;

        }
    }
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }


        public static List<Company> GenerateCompanies()
        {
            return new List<Company> {
                    new Company { Id = 1, Name = "Microsoft" },
                    new Company { Id = 2, Name = "Google" },
                    new Company { Id = 3, Name = "Apple" }
                };
        }
        public Company()
        {
            Id = 0;
            Name =string.Empty;
        }
        public Company(int id , string name)
        {
            Id = id;
            Name = name;
        }
    }
}
