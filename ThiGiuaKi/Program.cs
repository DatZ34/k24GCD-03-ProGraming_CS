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
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace ThiGiuaKi
{
    internal class Program
    {
        public static string firebase_URL = "https://studyfirebase-3848c-default-rtdb.asia-southeast1.firebasedatabase.app/";
        static async Task Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("Fire installed succesfully!");
            FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile("C:\\Users\\AD\\OneDrive\\Documents\\GitHub\\k24GCD-03-ProGraming_CS\\ThiGiuaKi\\serviceAccountKey.json")
            });
            await ConsolePlayer();
        }

        public static void Menu()
        {
            Console.WriteLine("\n===Chương trình quản lý danh sách Player===\n");
            Console.WriteLine("1.Thêm 10 player mới vào /Players");
            Console.WriteLine("2.Hiển thị toàn bộ danh sách player");
            Console.WriteLine("3.Cập nhập Gold hoặc Score");
            Console.WriteLine("4.Xóa player theo PlayerID");
            Console.WriteLine("5.Lấy về top 5 player cho vào TOPGOLD");
            Console.WriteLine("6.Lấy về top 5 player cho vào TopScore");
            Console.WriteLine("7.Add 1 player");
            Console.WriteLine("8.Xóa firebase players");
            Console.WriteLine("0.Thoát");
            Console.WriteLine("");
        }
        public static async Task ConsolePlayer()
        {
            Dictionary<string , Players> players = new Dictionary<string , Players>();
            bool ContineConsole = true;
            while (ContineConsole)
            {
                Menu();
                Console.WriteLine("\n Nhập dữ số tương ứng mà bạn muốn chọn!");
                int d = Convert.ToInt32(Console.ReadLine());
                switch (d)
                {
                    case 1:
                        await AddplayerToFirebase(10);
                        break;
                    case 2:
                        await DisPlayeList(players);
                        break;
                    case 3:
                        await UppdateScoreOrGold(players);
                        break;
                    case 4:
                        await deletePlayerbyId(players);
                        break;
                    case 5:
                        await GetTop5Gold(players);
                        break;
                    case 6:
                        await GetTop5Score(players);
                        break;
                    case 7:
                        await AddPlayer(players);
                        break;
                    case 8:
                        await DeletePlayerData(players);
                        break;
                    case 0:
                        ContineConsole = false;
                        break;
                    default:
                        break;
                }
            }
        }
        public static async Task deletePlayerbyId(Dictionary<string ,Players> p)
        {
            var firebase = new FirebaseClient(firebase_URL);
            await GetValueToDictionary(p);
            Console.WriteLine("\n Nhập Id player muốn cập nhập");
            string a = Console.ReadLine();
            if (p.ContainsKey(a))
            {
                await firebase.Child("Players").Child(a).DeleteAsync();
            }
            p.Clear();
        }
        public static async Task AddplayerToFirebase(int n)
        {
            var firebase = new FirebaseClient(firebase_URL);
            var random = new Random();
            for(int i = 0;i < n; i++)
            {
                var PlayerData = new
                {
                    Gold = random.Next(100, 1000),
                    Name = $"Player {i}",
                    PlayerID = $"p{i}",
                    Score = random.Next(100, 10000),
                };
                await firebase.Child("Players").Child($"p{i}").PutAsync(PlayerData);
            }
            Console.WriteLine("Đã thêm danh sách người chơi thành công");
        }
        public static async Task DisPlayeList(Dictionary<string, Players> p)
        {
            try
            {
                var firebase = new FirebaseClient(firebase_URL);
                var players = await firebase
                    .Child("Players")
                    .OnceAsync<Players>();
                foreach(var item in players)
                {
                    p[item.Object.PlayerID] = item.Object;
                    Console.WriteLine($"Gold: {item.Object.Gold} Name: {item.Object.Name} PlayerID: {item.Object.PlayerID} Score: {item.Object.Score}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Không có dữ liệu để đọc : {e.Message}");
            }
        }
        public static async Task GetTop5Gold(Dictionary<string, Players> p)
        {
            try
            {
                int idx = 0;
                var firebase = new FirebaseClient(firebase_URL);
                var players = await firebase
                    .Child("Players")
                    .OnceAsync<Players>();
                foreach(var item in players)
                {
                    p[item.Object.PlayerID] = item.Object;
                    Console.WriteLine($"Gold: {item.Object.Gold} Name: {item.Object.Name} PlayerID: {item.Object.PlayerID} Score: {item.Object.Score}");
                }
                var sortedList = p.Values.OrderByDescending(s => s.Gold).Take(5).ToList();
                await firebase.Child("TopGold").DeleteAsync();
                foreach(var item in sortedList)
                {
                    idx++;
                    await firebase.Child("TopGold").Child(idx.ToString()).PutAsync(item);
                    Console.WriteLine(item);
                }
                p.Clear();
                Console.WriteLine("Thành công");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Không có dữ liệu để đọc : {e.Message}");
            }
        }
        public static async Task GetTop5Score(Dictionary<string, Players> p)
        {
            try
            {
                int idx = 0;
                var firebase = new FirebaseClient(firebase_URL);
                var players = await firebase
                    .Child("Players")
                    .OnceAsync<Players>();
                foreach(var item in players)
                {
                    p[item.Object.PlayerID] = item.Object;
                  
                }
                players.OrderByDescending(s => s.Object.Score).ToList();
                foreach(var item in players)
                {
                    Console.WriteLine($"Gold: {item.Object.Gold} Name: {item.Object.Name} PlayerID: {item.Object.PlayerID} Score: {item.Object.Score}");
                }
                var sortedList = p.Values.OrderByDescending(s => s.Score).Take(5).ToList();
                await firebase.Child("TopScore").DeleteAsync();
                foreach(var item in sortedList)
                {
                    idx++;
                    await firebase.Child("TopScore").Child(idx.ToString()).PutAsync(item);
                    Console.WriteLine(item);
                }
                p.Clear();
                Console.WriteLine("Thành công");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Không có dữ liệu để đọc : {e.Message}");
            }
        }
        public static async Task GetValueToDictionary(Dictionary<string, Players> p)
        {
            try
            {
                var firebase = new FirebaseClient(firebase_URL);
                var players = await firebase
                    .Child("Players")
                    .OnceAsync<Players>();
                foreach(var item in players)
                {
                    p[item.Object.PlayerID] = item.Object;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Không có dữ liệu để đọc : {e.Message}");
            }
        }
        public static async Task UppdateScoreOrGold(Dictionary<string, Players> p)
        {
            var firebase = new FirebaseClient(firebase_URL);
            await GetValueToDictionary(p);
            Console.WriteLine("\n Nhập Id player muốn cập nhập");
            string a = Console.ReadLine();
            Console.WriteLine("Nhập giá trị muốn cập nhập");
            int b = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Bạn chọn cập nhập Gold hay Score");
            
            Console.WriteLine("1.Cập nhập Score");
            Console.WriteLine("2.Cập nhập Gold");
            int d = Convert.ToInt32(Console.ReadLine());
            switch (d)
            {
                case 1:
                    if (p.ContainsKey(a))
                    {
                        p.TryGetValue(a, out Players valuePlayer);
                        valuePlayer.Score = b;
                        await firebase.Child("Players").Child(a).PatchAsync(valuePlayer);
                        Console.WriteLine("\nĐã cập nhập dữ liệu Score thành công!\n");
                    }
                    break;
                case 2:
                    if (p.ContainsKey(a))
                    {
                        p.TryGetValue(a, out var valuePlayer);
                        valuePlayer.Gold = b;
                        await firebase.Child("Players").Child(a).PatchAsync(valuePlayer);
                        Console.WriteLine("\nĐã cập nhập dữ liệu Gold thành công!\n");
                    }
                    break;
                default:
                    break;
            }
            
            p.Clear();
        }

        public static async Task AddPlayer(Dictionary<string , Players> p)
        {
            var firebase = new FirebaseClient(firebase_URL);
            
            bool c = true;
            while (c)
            {
                await GetValueToDictionary(p);
              
                Console.WriteLine("Nhập tên player :");
                string? na = Console.ReadLine();

                Console.WriteLine("Nhập score: ");
                int score = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Nhập gold: ");
                int gold = Convert.ToInt32(Console.ReadLine());
                string id = GetnextIndexAvailable(p);
                Players player = new Players(id,na,gold,score);

                p[id] = player;

                await firebase.Child("Players").Child(id).PutAsync(player);
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
                        p.Clear();
                        break;
                }
            }
        }
        public static async Task DeletePlayerData(Dictionary<string, Players> data)
        {
            
            var firebase = new FirebaseClient(firebase_URL);
            await firebase.Child("Players").DeleteAsync();
           
        }
        public static string GetnextIndexAvailable(Dictionary<string , Players> p)
        {
            
            int idx = 1;
            string a = $"p{idx}";
            Hashtable dt = new Hashtable(p);
            while (dt.ContainsKey(a))
            {
                a = $"p{idx++}";
            }
            return a;
        }

    }
}
