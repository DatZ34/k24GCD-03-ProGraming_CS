using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Newtonsoft.Json;

namespace LAB12_FINAL
{
    internal class Program
    {
        private static string firebase_URL = "https://studyfirebase-3848c-default-rtdb.asia-southeast1.firebasedatabase.app/";
        static async Task Main(string[] args)
        {
            Dictionary<int, Player> PlayerDic = new Dictionary<int, Player>();
            Console.OutputEncoding = Encoding.UTF8;
            await GetValueFormURL(PlayerDic);
            await Bai1(PlayerDic);
            await Bai2(PlayerDic);
        }
        public static async Task GetValueFormURL(Dictionary<int , Player> dic)
        {
            var firebase = new FirebaseClient(firebase_URL);
            string url = "https://raw.githubusercontent.com/NTH-VTC/OnlineDemoC-/refs/heads/main/lab12_players.json";

            HttpClient client = new HttpClient();
            string serverConfig = await client.GetStringAsync(url);
            List<Player> allPlayers;
            try
            {
                string json = await client.GetStringAsync(url);

                allPlayers = JsonConvert.DeserializeObject<List<Player>>(json);
                var VipPlayerRegion = allPlayers
                    .ToList();
                int index = 1;
                foreach (var vip in VipPlayerRegion)
                {
                    dic[vip.Id] = vip;
                    index++;
                    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static async Task Bai1(Dictionary<int , Player> dic)
        {
            var firebase = new FirebaseClient(firebase_URL);
            DateTime now = new DateTime(2025, 06, 30, 0, 0, 0, DateTimeKind.Utc);
            var OfflinePLayer = dic
                .Where(p => p.Value.IsActive == false || (now - p.Value.LastLogin).TotalDays > 5)
                .Select(p => new
                {
                    Name = p.Value.Name,
                    IsActive = p.Value.IsActive,
                    LastLogin = p.Value.LastLogin
                })
                .ToDictionary(p => p.Name);
            int index = 1;
            Console.WriteLine("====Bài 1.1 ====");
            foreach(var p in OfflinePLayer)
            {
                Console.WriteLine(p.ToString());
                await firebase.Child("final_exam_bai1_inactive_players").Child(index.ToString()).PutAsync(p);
                index++;
            }
            Console.WriteLine("====Bài 1.2 ====");
            var LowlevelPlayer = dic
                .Where(p => p.Value.Level < 10)
                .Select(p => new
                {
                    Name = p.Value.Name,
                    Level = p.Value.Level,
                    Gold = p.Value.Gold
                })
                .OrderBy(p => p.Level)
                .ToDictionary(p => $"Level: {p.Level}");


            int i = 1;
            foreach (var p in LowlevelPlayer)
            {
                Console.WriteLine(p.ToString());
                await firebase.Child("final_exam_bai1_low_level_players").Child(i.ToString()).PutAsync(p);
                i++;
            }
            Console.WriteLine("\n====Xong Bài 1!!====\n");
        }
        public static async Task Bai2(Dictionary<int , Player> dic)
        {
            var firebase = new FirebaseClient(firebase_URL);
            Console.WriteLine("====Bài 2 ====");

            var Top3VipAwards = dic
                .Where(p => p.Value.VipLevel > 0)
                .Select(p => new
                {
                    AwardedGoldAmount = (500 * p.Value.VipLevel) - 500,
                    CurrentGold = p.Value.Gold,
                    Name = p.Value.Name,
                    Level = p.Value.Level,
                    VipLevel = p.Value.VipLevel

                })
                .OrderByDescending(p => p.Level)
                .Take(3)
                .ToDictionary(p => $"Level: {p.Level}");
            
            int index = 1;
            foreach(var p in Top3VipAwards)
            {
               
                
                Console.WriteLine(p.ToString());
                await firebase.Child("final_exam_bai2_top3_vip_awards").Child(index.ToString()).PutAsync(p);
                index++;
            }
        }
    }
}
