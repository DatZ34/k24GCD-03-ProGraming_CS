using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;
using Newtonsoft.Json;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
namespace Lab09
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            string url = "https://google.con.vn";
            var uri = new Uri(url);
            var uritype = typeof(Uri);
            uritype.GetProperties().ToList().ForEach(property =>
            {
                Console.WriteLine($"{property.Name,15} {property.GetValue(uri)}");
            });
            Console.WriteLine($"Segments: {string.Join(",", uri.Segments)}");
            URLExample();
            DNSHost();
            PingExample();
            VD();
            Vd_GetWebContent("https://gamevui.vn/");
            FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile("C:\\Users\\AD\\OneDrive\\Documents\\GitHub\\k24GCD-03-ProGraming_CS\\Lab09\\serviceAccountKey.json")
            });
            await vd2();

            Console.ReadLine();
        }
        public static void URLExample()
        {
            string uriString = "https://docs.google.com/spreadsheets/d/1710ApNCW1XsgfGxddLPD_uEdjj-r12QNzXXdYsWAa3s/edit?gid=0#gid=0";
            var uri = new Uri(uriString);
            Console.WriteLine($"URI                          đầy đủ: {uri.AbsoluteUri}");
            Console.WriteLine($"Scheme                  (giao thức): {uri.Scheme}");
            Console.WriteLine($"Host                      (máy chủ): {uri.Host}");
            Console.WriteLine($"Port                         (cổng): {uri.Port}");
            Console.WriteLine($"PathAndQuery(đường dẫn và truy vấn): {uri.PathAndQuery}");
            Console.WriteLine($"AbsolutePath  (Đường dẫn tuyệt đối): {uri.AbsolutePath}");
            Console.WriteLine($"Query              (Chuỗi truy vấn): {uri.Query}"); // ?id=123&name=test
            Console.WriteLine($"Fragment                 (Mảnh/neo): {uri.Fragment}"); // #section (phần sau dấu #)
        }
        public static async Task vd2()
        {
            string firebase_URl = "https://studyfirebase-3848c-default-rtdb.asia-southeast1.firebasedatabase.app/";
            var firebase = new FirebaseClient(firebase_URl);
            string url = "https://raw.githubusercontent.com/NTH-VTC/OnlineDemoC-/refs/heads/main/server_config.json";
            HttpClient client = new HttpClient();
            string serverConfig = await client.GetStringAsync( url );
            List<Player> allPlayers;
            try
            {
                string json = await client.GetStringAsync("https://raw.githubusercontent.com/NTH-VTC/OnlineDemoC-/refs/heads/main/leaderboard.json");

                allPlayers = JsonConvert.DeserializeObject<List<Player>>(json);
                var goldPlayers = allPlayers
                    .OrderByDescending(p => p.Gold)
                    .Take(10)
                    .ToList();
                int index = 0;
                foreach (Player player in goldPlayers)
                {
                    Console.WriteLine(player.ToString());
                    await firebase.Child("Player").Child(index.ToString()).PutAsync(player);
                    index++;

                }
            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static void DNSHost()
        {
            string hostName = Dns.GetHostName();
            IPHostEntry entry = Dns.GetHostEntry("google.com");
            foreach(IPAddress ip in entry.AddressList)
            {
                Console.WriteLine(ip.ToString());
            }
        }
        public static void PingExample()
        {
            Ping pingSender = new Ping();
            PingReply reply = pingSender.Send("google.com");
            if(reply.Status == IPStatus.Success)
            {
                Console.WriteLine($"Ping thành công: {reply.RoundtripTime}");

            }
            else
            {
                Console.WriteLine($"Ping thất bại : {reply.Status}");
            }
        }
        public static async void VD()
        {
            using (var listener = new HttpListener())
            {
                listener.Prefixes.Add("http://localhost:9090/");
                listener.Start();
                var context = await listener.GetContextAsync();
                var response = context.Response;
                string responseString = "<html><body>Hello có cái cc</body></html>";
                byte[] buffer = Encoding.UTF8.GetBytes(responseString);
                response.ContentLength64 = buffer.Length;
                await response.OutputStream.WriteAsync(buffer, 0, buffer.Length);
                response.Close();
                listener.Stop();
            }
        }
        public static async Task<string> GetWebContent(string url)
        {
            string html = "";
            try
            {
                var httpClient = new HttpClient();

                httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozila 4.0");
                HttpResponseMessage httpResponse = await httpClient.GetAsync(url);
                html = await httpResponse.Content.ReadAsStringAsync();

            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return html;
        }
        public static void Vd_GetWebContent(string url)
        {
            Console.WriteLine("========== Vd_GetWebContent load" + url);
            var taskLoadWeb = GetWebContent(url);
            taskLoadWeb.Wait();
            var html = taskLoadWeb.Result;
            Console.WriteLine("VD_GetWebContent " + html);
        }
    }
}