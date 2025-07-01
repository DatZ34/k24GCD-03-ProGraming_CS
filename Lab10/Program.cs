using System.Linq;
namespace Lab10
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<int> numbers = new List<int>
            {
                12, -5, 0, 7, -14, 25, -1, 3, 8, -9,
                17, -22, 4, -3, 10, -6, 31, -17, 2, -8,
                19, -12, 0, 23, -30, 11, -7, 5, -15, 9,
                26, -18, 6, -4, 13, -2, 16, -20, 1, -10,
                28, -11, 14, -13, 15, -19, 18, -16, 21, -21
            };
            VD1();
            VD2();
            VD3_OrderBY();
            VD4_OrderByDescending(10);
            VD5GroupBy();
            VD6();
            BT01_TimSoDuong(numbers);
            BT02_BinhPhuongSoLonHon10(numbers);
            BatTapTong();
        }
        public static void VD1()
        {
            Console.WriteLine("Hi");
            List<int> list = new List<int> { 1, 2, 4, 5 };
            var query = from n in list
                        where n < 3
                        select n;
            foreach (var item in query) { Console.WriteLine(item); }

        }
        public static void VD2()
        {

            List<Customer> MyCustomerList = new List<Customer>
            {
                new Customer { CustomerID = "C001", Contactname = "Nguyen Van A", City = "Hanoi" },
                new Customer { CustomerID = "C002", Contactname = "Tran Thi B", City = "HCMC" },
                new Customer { CustomerID = "C003", Contactname = "Tran Thi C", City = "VT" },
                new Customer { CustomerID = "C004", Contactname = "Tran Thi D", City = "Hue" }

            };
            var query2 = from c in MyCustomerList
                         where c.City == "Hanoi"
                         select new { c.City, c.Contactname };
            foreach (var c in query2)
            {
                Console.WriteLine($"{c.Contactname} - {c.City}");
            }
        }
        public static void VD3_OrderBY()
        {
            List<int> list = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 };
            var sortedNumbers = list.OrderBy(n => n);
            foreach (var number in sortedNumbers)
            {
                Console.WriteLine(number);
            }
        }
        public static void VD4_OrderByDescending(int n)
        {
            Random rnd = new Random();
            List<int> list = new List<int>();
            for (int i = 0; i <= n; i++)
            {
                int x = rnd.Next(1, 100);
                list.Add(x);
            }
            var sortedDescNumbers = list.OrderByDescending(num => num);
            foreach (var number in sortedDescNumbers)
            {
                Console.WriteLine(number);
            }
        }
        public static void VD5GroupBy()
        {
            List<int> list = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 };
            var groupedNumbers = list.GroupBy(n => n % 2 == 0 ? "Even" : "Odd");
            foreach (var number in groupedNumbers)
            {
                Console.WriteLine($"Group: {number.Key}");
                foreach (var num in number)
                {
                    Console.WriteLine(num);
                }
            }
        }
        public static void VD6()
        {
            List<Person> people =Person.GenerateListOfPeople();
            var companies = Company.GenerateCompanies();

            var peopleWithCompanies = people.Join(companies, person => person.CompanyId, company => company.Id,
                                                                    (person, company) =>
                                                                    new { person.FirstName, company.Name });
            Console.WriteLine("======Cách 1======");
            foreach(var a in peopleWithCompanies)
            {
                Console.WriteLine(a);
            }
            var peopleWithCompaniesQuery = from p in people
                                           join c in companies on p.CompanyId equals c.Id
                                           select new { p.FirstName, c.Name };
            Console.WriteLine("======Cách 2======");
            foreach (var a in peopleWithCompaniesQuery)
            {
                Console.WriteLine(a);
            }
            
            Console.WriteLine("======Cách 3======");
            var result = companies.GroupJoin(people,
                                        company => company.Id,
                                        person => person.CompanyId,
                                        (company, peopleInCompany) => new { CompantName = company.Name, Employees = peopleInCompany });
            foreach (var companyGroup in result)
            {
                Console.WriteLine($"Company: {companyGroup.CompantName}");

                
                foreach (var employee in companyGroup.Employees)
                {
                    Console.WriteLine($"{employee.FirstName} {employee.LastName} - {employee.Occupation}");
                }

                Console.WriteLine();
            }
            Console.WriteLine("=====Select 1 phan tu=====");
            IEnumerable<string> allFirstName = people.Select(x => x.FirstName);
            foreach(var firstName in allFirstName)
            {
                Console.WriteLine(firstName);   
            }
            Console.WriteLine();

            bool thereArePeople = people.Any();

            bool anyDevsOver30 = people.Any(x => x.Occupation == "Dev" && x.Age > 30);

            List<Person> listOfDevs = people.Where(x => x.Occupation == "Dev").ToList();

            int totalAge = people.Sum(p =>  p.Age);
            double averageAge = people.Average(p =>  p.Age);
            int yougestAge = people.Min(p => p.Age);
            string allNamges = people.Aggregate("", (current, p) => current + p.FirstName + "");

            var demo01 = people.Where(p => p.Occupation == "Dev" && p.Age > 25)
                                .OrderBy(p => p.FirstName)
                                .Select(p => $"{p.FirstName} {p.LastName}")
                                .ToList();

            Console.WriteLine("\n Dev name > 25");
            foreach(var name in demo01)
            {
                Console.WriteLine(name);
            }

            var demo02 = people.Where(p => p.Occupation == "Dev" && p.Age > 25)
                                .OrderBy(p => p.FirstName)
                                
                                .ToDictionary(p => p.CompanyId,p => $"{p.FirstName} {p.LastName}");

            Console.WriteLine("\n Dev name > 25");
            foreach(var name in demo02)
            {
                Console.WriteLine(name);
            }
                                  
        }
        public static void BT01_TimSoDuong(List<int> l)
        {
            Console.WriteLine("BT1");

            var soduong = l.Where(p => p > 0 && p < 13)
                .OrderByDescending(p => p)
                .ToList();
            foreach(var n in soduong)
            {
                Console.Write($"{n} ");
            }
            Console.WriteLine();

        }
        public static void BT02_BinhPhuongSoLonHon10(List<int> l)
        {
            Console.WriteLine("BT2");

            var binhPhuong = l.Where(p => p > 10)
                .Select(p => p = (int)Math.Pow(p, 2))
                .ToList();
            foreach (var n in binhPhuong)
            {
                Console.Write($"{n} ");
            }
            Console.WriteLine();

        }
        public static void BatTapTong()
        {
            var peopletest = new List<Person>
            {
                new Person { FirstName = "Eric", LastName = "Fleming", Occupation = "Dev", Age = 24, CompanyId = 1 },
                new Person { FirstName = "Steve", LastName = "Smith", Occupation = "Manager", Age = 40, CompanyId = 1 },
                new Person { FirstName = "Brendan", LastName = "Enrick", Occupation = "Dev", Age = 30, CompanyId = 2 },
                new Person { FirstName = "Jane", LastName = "Doe", Occupation = "Dev", Age = 35, CompanyId = 1 },
                new Person { FirstName = "Samantha", LastName = "Jones", Occupation = "Dev", Age = 24, CompanyId = 2 },
                new Person { FirstName = "Tom", LastName = "Henderson", Occupation = "QA", Age = 28, CompanyId = 1 },
                new Person { FirstName = "Anna", LastName = "Nguyen", Occupation = "HR", Age = 31, CompanyId = 3 },
                new Person { FirstName = "Mark", LastName = "Tran", Occupation = "Dev", Age = 26, CompanyId = 1 },
                new Person { FirstName = "Emily", LastName = "Clark", Occupation = "Manager", Age = 38, CompanyId = 2 },
                new Person { FirstName = "John", LastName = "Phan", Occupation = "QA", Age = 29, CompanyId = 3 },
                new Person { FirstName = "Chris", LastName = "Lee", Occupation = "Support", Age = 32, CompanyId = 2 },
                new Person { FirstName = "Tina", LastName = "Vo", Occupation = "HR", Age = 36, CompanyId = 1 },
                new Person { FirstName = "Alex", LastName = "Khan", Occupation = "Dev", Age = 23, CompanyId = 3 },
                new Person { FirstName = "Natalie", LastName = "Wang", Occupation = "Manager", Age = 41, CompanyId = 2 },
                new Person { FirstName = "Brian", LastName = "Hoang", Occupation = "Dev", Age = 27, CompanyId = 1 },
                new Person { FirstName = "Lucy", LastName = "Kim", Occupation = "Dev", Age = 29, CompanyId = 3 },
                new Person { FirstName = "Victor", LastName = "Ng", Occupation = "DevOps", Age = 33, CompanyId = 2 },
                new Person { FirstName = "Rachel", LastName = "Ly", Occupation = "QA", Age = 30, CompanyId = 1 },
                new Person { FirstName = "Tommy", LastName = "Pham", Occupation = "Support", Age = 27, CompanyId = 3 },
                new Person { FirstName = "Jasmine", LastName = "Le", Occupation = "Dev", Age = 24, CompanyId = 1 },
                new Person { FirstName = "Daniel", LastName = "Nguyen", Occupation = "Dev", Age = 26, CompanyId = 2 },
                new Person { FirstName = "Linda", LastName = "Ngoc", Occupation = "HR", Age = 39, CompanyId = 3 },
                new Person { FirstName = "Peter", LastName = "Lam", Occupation = "QA", Age = 35, CompanyId = 1 },
                new Person { FirstName = "Sophia", LastName = "Dao", Occupation = "Manager", Age = 42, CompanyId = 2 },
                new Person { FirstName = "Albert", LastName = "Vo", Occupation = "Dev", Age = 25, CompanyId = 3 },
                new Person { FirstName = "Mia", LastName = "Pham", Occupation = "Dev", Age = 23, CompanyId = 2 },
                new Person { FirstName = "Nathan", LastName = "Do", Occupation = "Support", Age = 34, CompanyId = 1 },
                new Person { FirstName = "Karen", LastName = "Tran", Occupation = "QA", Age = 28, CompanyId = 3 },
                new Person { FirstName = "Henry", LastName = "Bui", Occupation = "DevOps", Age = 31, CompanyId = 2 },
                new Person { FirstName = "Lily", LastName = "Mai", Occupation = "Manager", Age = 43, CompanyId = 1 },
                new Person { FirstName = "Dylan", LastName = "Nguyen", Occupation = "Dev", Age = 30, CompanyId = 3 },
                new Person { FirstName = "Isabella", LastName = "Pham", Occupation = "Dev", Age = 26, CompanyId = 2 },
                new Person { FirstName = "Jason", LastName = "Le", Occupation = "Support", Age = 29, CompanyId = 1 },
                new Person { FirstName = "Cindy", LastName = "Vo", Occupation = "QA", Age = 27, CompanyId = 3 },
                new Person { FirstName = "Kevin", LastName = "Tran", Occupation = "Dev", Age = 28, CompanyId = 2 },
                new Person { FirstName = "Amy", LastName = "Lam", Occupation = "Manager", Age = 37, CompanyId = 1 },
                new Person { FirstName = "Sean", LastName = "Hoang", Occupation = "DevOps", Age = 32, CompanyId = 3 },
                new Person { FirstName = "Nina", LastName = "Huynh", Occupation = "HR", Age = 33, CompanyId = 2 },
                new Person { FirstName = "Tony", LastName = "Ngoc", Occupation = "Dev", Age = 25, CompanyId = 1 },
                new Person { FirstName = "Grace", LastName = "Nguyen", Occupation = "Dev", Age = 26, CompanyId = 3 },
                new Person { FirstName = "David", LastName = "Ly", Occupation = "Support", Age = 30, CompanyId = 2 },
                new Person { FirstName = "Olivia", LastName = "Pham", Occupation = "Dev", Age = 24, CompanyId = 1 },
                new Person { FirstName = "Ethan", LastName = "Le", Occupation = "QA", Age = 28, CompanyId = 3 },
                new Person { FirstName = "Ashley", LastName = "Truong", Occupation = "Dev", Age = 22, CompanyId = 2 },
                new Person { FirstName = "Tyler", LastName = "Kim", Occupation = "Manager", Age = 39, CompanyId = 1 },
                new Person { FirstName = "Luna", LastName = "Vo", Occupation = "DevOps", Age = 34, CompanyId = 3 },
                new Person { FirstName = "Zack", LastName = "Nguyen", Occupation = "Dev", Age = 27, CompanyId = 2 },
                new Person { FirstName = "Bella", LastName = "Pham", Occupation = "Support", Age = 31, CompanyId = 1 }
            };

            Console.WriteLine("1.");
            var c1 = peopletest
                .OrderBy(p => p.Age)
                .ThenBy(p => p.FirstName + p.LastName)
                .Select(p => $"{p.Age} / {p.Occupation} / {p.FirstName + p.LastName}")
                .ToList();
                                
            foreach(var a  in c1)
            {
                Console.WriteLine(a);
            }  
        }

    }
}
