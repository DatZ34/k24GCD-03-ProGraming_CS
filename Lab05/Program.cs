using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Firebase.Database;

using Firebase.Database.Query;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Newtonsoft.Json;
namespace Lab05

{
    internal class Program
    {
        
        private static string firebase_URL = "https://studyfirebase-3848c-default-rtdb.asia-southeast1.firebasedatabase.app/";
        static async Task Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("Fire installed succesfully!");
            FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile("C:\\Users\\AD\\OneDrive\\Documents\\GitHub\\k24GCD-03-ProGraming_CS\\Lab05\\serviceAccountKey.json")
            });
            await GenerateRandomStudent(10);
            await ConsoleStudent();
        }
        public  static async Task GenerateRandomStudent(int n)
        {
            var firebase = new FirebaseClient(firebase_URL);
            var random = new Random();
            for(int i =0; i < n; i++)
            {
                var studentData = new
                {
                    Name = $"student {i}",
                    Mssv = random.Next(1, 100),
                    Grade = random.Next(1, 10),
                    Class = $"Lớp {random.Next(1, 5)}"
                };
                await firebase.Child("Student").Child($"Student{i}").PutAsync(studentData);
            }
            Console.WriteLine("Đã thêm random danh sách học sinh");
        }

        public static void Menu()
        {
            Console.WriteLine("\n===Console Nhập liệu học sinh ===");
            Console.WriteLine("1.Nhập dữ liệu sinh viên");
            Console.WriteLine("2.Ghi dữ liệu vào filebase");
            Console.WriteLine("3.Lấy dữ liệu sinh viên từ firebase");
            Console.WriteLine("4.Cập nhập dữ liệu sinh viên lên firebase");
            Console.WriteLine("5.Xóa dữ liệu sinh viên trên firebase");
            Console.WriteLine("6.Lấy top 5 sinh viên theo điểm số và lưu vao firebase với ley TopStudent");
            Console.WriteLine("0.Thoát");
            Console.WriteLine("Nhập số muốn chọn\n");
        }
        public static async Task ConsoleStudent()
        {
            var firebafe = new FirebaseClient(firebase_URL);
            Dictionary<int, Student> data = new Dictionary<int, Student>();
            bool isRunning = true;
            while (isRunning)
            {
                Menu();
                int d = Convert.ToInt32(Console.ReadLine());
                switch (d)
                {
                    case 1:
                        AddStudent(data);
                        break;
                    case 2:
                        await AddDataStudentToFirebase(data);
                        break;
                    case 3:
                        await GetdataFromFirebase(data);
                        break;
                    case 4:
                        await UppdateStudentData(data);
                        break;
                    case 5:
                        await DeleteStudentData(data);
                        break;
                    case 6:
                        await GetTopSVdataFromFirebase(data);
                        break;
                    case 0:
                        isRunning = false;
                        break;
                }
            }
        }
        public static async Task DeleteStudentData(Dictionary<int, Student> data)
        {
            data.Clear();
            var firebase = new FirebaseClient(firebase_URL);
            await firebase.Child("Student").DeleteAsync();
            Console.WriteLine("\nDữ liệu đã xóa khỏi FireBase!\n");
        }
        public static async Task UppdateStudentData(Dictionary<int , Student> data)
        {
            var firebase = new FirebaseClient(firebase_URL);
            foreach(KeyValuePair<int , Student> entry in data)
            {
                await firebase.Child("Student").Child(entry.Key.ToString()).PatchAsync(entry.Value);
            }
            Console.WriteLine("\nĐã cập nhập dữ liệu thành công!\n");
            data.Clear();   
        }
        public static async Task GetdataFromFirebase(Dictionary<int, Student> data)
        {
            try
            {
                var firebase = new FirebaseClient(firebase_URL);
                var students = await firebase
                    .Child("Student")
                    .Child("MSSV")
                    .OnceAsync<Student>();

                foreach (var item in students)
                {
                    data[item.Object.MSSV] = item.Object;
                    Console.WriteLine($"MSSV: {item.Object.MSSV}, Tên: {item.Object.Name}, Lớp: {item.Object.Class}, Điểm: {item.Object.Grade}");
                }

            }
            catch (Exception e)
            {
                Console.WriteLine($"Không có dữ liệu để đọc : {e.Message}");
            }

        }
        public static async Task GetTopSVdataFromFirebase(Dictionary<int, Student> data)
        {
            try
            {
                var firebase = new FirebaseClient(firebase_URL);
                var students = await firebase
                    .Child("Student")
                    
                    .OnceAsync<Student>();

                foreach (var item in students)
                {
                    data[item.Object.MSSV] = item.Object;
                    Console.WriteLine($"MSSV: {item.Object.MSSV}, Tên: {item.Object.Name}, Lớp: {item.Object.Class}, Điểm: {item.Object.Grade}");
                }
                var sortedList = data.Values.OrderByDescending(s => s.Grade).Take(5).ToList(); // Sắp xếp theo tên giảm dần
                await firebase.Child("TopStudent").DeleteAsync();
                foreach( var item in sortedList)
                {
                    await firebase.Child("TopStudent").Child($"Mssv{item.MSSV.ToString()}").PutAsync(item);
                    Console.WriteLine(item);
                }


            }
            catch (Exception e)
            {
                Console.WriteLine($"Không có dữ liệu để đọc : {e.Message}");
            }

        }
        
        public static async Task AddDataStudentToFirebase(Dictionary<int, Student> data)
        {
            var firebase = new FirebaseClient(firebase_URL);
            foreach (var entry in data)
            {
                await firebase.Child("Student").Child($"MSSV{entry.Key.ToString()}").PutAsync(entry.Value);
            }
            data.Clear();
        }
        public static void AddStudent(Dictionary<int, Student> dt)
        {
       
            
            bool c = true;
            while (c)
            {
                Console.WriteLine("Nhập tên sinh viên :");
                string? na = Console.ReadLine();
               
                Console.WriteLine("Nhập Diểm sinh viên: ");
                double gd = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Nhập lớp sinh viên: ");
                string? cl = Console.ReadLine();
                int mssv = GetnextIndexAvailable(dt);
                Student st = new Student(na, mssv , gd, cl);

                dt[mssv] = st;
                
                Console.WriteLine("bạn có muốn tiesp tuc không : 1. có , 2. không");
                int d = Convert.ToInt32(Console.ReadLine());

                switch (d)
                {
                    case 1:
                        break;
                    case 2:
                        c = false;
                        break;
                    default:
                        break;
                }
            }
    
    

        }
        public static int GetnextIndexAvailable(Dictionary<int, Student> st)
        {
            int idx = 1;
            Hashtable dt = new Hashtable(st);
            while (dt.ContainsKey(idx))
            {
                idx++;
            }
            return idx;

        }
    }
}
