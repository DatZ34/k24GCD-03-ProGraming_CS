using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace lab03
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Bai2();
            Console.ReadLine();
        }
        #region List
        static void ListExample()
        {
            List<string> fruits = new List<string>();
            fruits.Add("Apple");
            fruits.Add("Banana");
            fruits.Add("Chery");

            fruits.Insert(1, "Apple");
            fruits.ForEach(f => f.Insert(1, "Apple") );
        }
        static void ListStudentExample()
        {
            Console.WriteLine("=== List<Student> ===");
            List<Student> studentList = new List<Student>
            {
                new Student(1, "Alice"),
                new Student(2, "Bob"),
                new Student(3, "Charlie"),
            };
            studentList.Add(new Student(4, "David"));
            studentList.Insert(1, new Student(5, "Eva"));
            studentList.ForEach(studentList => Console.WriteLine(studentList));
            studentList.RemoveAt(3);
            studentList[3] = new Student(3, "cat");
            Console.WriteLine("After remove");
            studentList.ForEach(Console.WriteLine);

            Console.WriteLine("=== List<string> ===");
            List<string> fruits = new List<string>();
            fruits.Add("banana");
            fruits.Insert(0, "waterMelon");
            fruits[1] = "durian";
            fruits.ForEach(Console.WriteLine);
        }
        #endregion
        #region Hashtable
        static void hastableExample()
        {
            Hashtable hashtable = new Hashtable();
            hashtable.Add("Key1", "value1");
            hashtable.Add("Key2", "value2");
            hashtable.Add("Key3", "value3");
            hashtable.Add("Key4", "value4");
            hashtable.Add("Key5", "value5");
            hashtable.Add("Key6", "value6");
            Console.WriteLine($" count : {hashtable.Count}");

            foreach(var has in hashtable)
            {
                Console.WriteLine(has);
            }
        }
        #endregion
        #region SortedList
        static void SortedListExample()
        {
            SortedList mySl = new SortedList();
            mySl.Add("Third", "!");
            mySl.Add("Second", "Word");
            mySl.Add("Frist", "!Hello");

            Console.WriteLine("mySl");
            Console.WriteLine($"Count : {mySl.Count}");
            Console.WriteLine($"Count : {mySl.Capacity}");
            Console.WriteLine("Keys and Values");

            Console.WriteLine($"\t-KEY-\t-VALUE");
            foreach(var sl in mySl)
            {
                Console.WriteLine(sl);
            }
            for(int i = 0; i < mySl.Count; i++)
            {
                Console.WriteLine($"Key = {mySl.GetKey(i)} Value = {mySl.GetByIndex(i)}");
            }
            Console.WriteLine("===Genegic===");
            SortedList<int, string> students = new SortedList<int, string>();
            students.Add(102, "lan");
            students.Add(101, "Nam");
            students.Add(105, "Hoa");
            students[103] = "linh";
            if (students.ContainsKey(103))
            {
                Console.WriteLine("Student 103 : {0}", students[103]);
            }
            students.Remove(101);
            foreach (var item in students)
            {
                Console.WriteLine($"{item.Key}: {item.Value}");
            }
        }
        
        #endregion
        #region Dictionary
        static void DictionaryExample()
        {
            Console.WriteLine("===Dictionary<string,int===>");
            Dictionary<string, int> ages = new Dictionary<string, int>();
            ages.Add("Alice", 24);
            ages.Add("Bob", 30);

            if (ages.ContainsKey("Alice"))
            {
                Console.WriteLine($"Alice is " + ages["Alice"] + "year old");
            }

            ages["Alice"] = 40;
            ages.Remove("Bob");

            foreach (var item in ages)
            {
                Console.WriteLine($" Name :{item.Key} age = {item.Value}");
            }
        }
        #endregion
        #region Queue
        static void QueueExample()
        {
            Queue<string> queue = new Queue<string>();
            queue.Enqueue("Download file");
            queue.Enqueue("Scan file");

            Console.WriteLine($"Next queue: {queue.Peek()}");
            Console.WriteLine($"Processing: {queue.Dequeue()}");

            foreach (var item in queue)
            {
                Console.WriteLine(item);
            }
        }
        #endregion
        #region bai2
        static void Bai2()
        {
            
            bool isRuning = true;
            Hashtable danhsach = new Hashtable();
            MatHang m = new MatHang();
            while(isRuning)
            {
                Menu();
                Console.WriteLine("Nhap so ban muon");
                int a = Convert.ToInt32(Console.ReadLine());
                switch (a)
                {
                    case 1:
                        ThemMatHang(danhsach, m);
                        break;
                    case 2:
                        XuatDanhSachMatHang(danhsach);
                        break;
                    case 3:
                        TimMatHang(danhsach);
                        break;
                    case 0:
                        isRuning = false;
                        break;
                }
            }
        }
        static void Menu()
        {
            Console.WriteLine("");
            Console.WriteLine("===Tra cuu, nhap lieu ma hang===");
            Console.WriteLine("1.Nhap vao danh sach cac mat hang");
            Console.WriteLine("2.Xuat danh sach mat hang");
            Console.WriteLine("3.Tim va xoa mat hang dua vao ma mat hang");
            Console.WriteLine("0.thoat");
        }
        static float ThanhTien(int soluong, float donGia)
        {
            return soluong * donGia;
        }
        static void ThemMatHang(Hashtable danhsach, MatHang m)
        {
            bool continueAdd = true;
            while (continueAdd)
            {
                Console.WriteLine("Nhap ma mat hang");
                int a = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Nhap ten mat hang");
                string b = Console.ReadLine();
                Console.WriteLine("Nhap so luong hang");
                int c = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Nhap don gia san pham");
                float d = Convert.ToSingle(Console.ReadLine());
                m = new MatHang(a,b, c, d);

                if (!danhsach.Contains(a))
                {
                    danhsach.Add(a, m);
                }
                else
                {
                    Console.WriteLine("hang da ton tại");
                }
                
                Console.WriteLine("Them hang thanh cong");
                Console.WriteLine("Co muon tiep tuc nhap hay khong");
                Console.WriteLine("1. tiep tuc");
                Console.WriteLine("2. thoat");
                int e = Convert.ToInt32(Console.ReadLine());
                switch (e)
                {
                    case 1:
                        break;
                    case 2:
                        continueAdd = false;
                        break;
                }
            }
        }
        static void TimMatHang(Hashtable danhsach)
        {
            Console.WriteLine("Nhap ma mh");
            int MaMh = Convert.ToInt32(Console.ReadLine());
            XoaMatHang(danhsach, MaMh);
            XuatDanhSachMatHang(danhsach);

        }
        static void XoaMatHang(Hashtable danhsach, int MaMh)
        {
            if (danhsach.ContainsKey(MaMh))
            {
                danhsach.Remove(MaMh);
            }
        }
        static void XuatDanhSachMatHang(Hashtable danhsach)
        {
            foreach (DictionaryEntry ds in danhsach)
            {
                int maMh = (int)ds.Key;
                MatHang mh = (MatHang)ds.Value;
                Console.WriteLine($"Ma mh = {mh.maMh}, tenMh = {mh.tenMh}, soluong = {mh.soluong}, dongia = {mh.donGia}");
            }
        }
        #endregion
    }

}
