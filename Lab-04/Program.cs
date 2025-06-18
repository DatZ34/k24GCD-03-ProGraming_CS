using System.Text;
using Firebase.Database;
using Firebase.Database.Query;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Newtonsoft.Json;
namespace Lab_04
{
    internal class Program
    {
        private static string firebase_URL = "https://wolf-card-default-rtdb.asia-southeast1.firebasedatabase.app/";
        public static async Task AddTestData()
        {
            var firebase = new FirebaseClient(firebase_URL);
            var testData = new
            {
                Message = "hello Firebase!",
                Timestamp = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss")
            };
            
            await firebase.Child("test").PutAsync(testData);
            Console.WriteLine("Dữ liệu đã được thêm vào Firebase");
           
        }
        public static async Task ReadFireBase()
        {
            try
            {
                var firebase = new FirebaseClient(firebase_URL);
                var testData = await firebase.Child("test").OnceSingleAsync<dynamic>();

                Console.WriteLine($"Message : {testData.Message}");
                Console.WriteLine($"Timestamp: {testData.Timestamp}");
            } catch(Exception e)
            {
                Console.WriteLine($"Không có dữ liệu để đọc : {e.Message}");
            }

        }
        public static async Task UppdateTestData()
        {
            var firebase = new FirebaseClient(firebase_URL);
            var UppdateData = new
            {
                Message = "Uppdated Message!!",
                Timestamp = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss")
            };
            await firebase.Child("test").PatchAsync(UppdateData);
            Console.WriteLine("Đã cập nhập dữ liệu thành công!");
        
        }
        public static async Task DeleteTestData()
        {
            var firebase = new FirebaseClient(firebase_URL);
            await firebase.Child("test").DeleteAsync();
            Console.WriteLine("Dữ liệu đã xóa khỏi FireBase!");
        }
        public static async Task GenerateTestPlayers(int num)
        {
            var firebase = new FirebaseClient(firebase_URL);
            var random = new Random();

            for (int i = 0; i < num; i++)
            {
                var playerData = new
                {
                    PlayerName = $"Player {i}",
                    gold = random.Next(1000, 10000),
                    ruby = random.Next(100, 500),
                    vip = random.Next(1, 10),
                    exp = random.Next(5000, 20000),
                    wins = random.Next(0, 50),
                    losses = random.Next(0, 30)
                };

                await firebase.Child("Players").Child($"Player{i}").PutAsync(playerData);
            }
            Console.WriteLine("Đã thêm danh sách người chơi thành công");
        }
        static async Task Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("Fire installed succesfully!");
            FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile("C:\\Users\\AD\\OneDrive\\Documents\\GitHub\\k24GCD-03-ProGraming_CS\\Lab-04\\serviceAccountKey.json")
            });
            await AddTestData();
            Console.WriteLine("Firebase Admin SDK đã được khởi tạo thành công");
            await ReadFireBase();
            await UppdateTestData();
            await ReadFireBase();
            await DeleteTestData();
            await ReadFireBase();
            await GenerateTestPlayers(10);

        }

    }
}
