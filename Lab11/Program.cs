﻿using System;
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

namespace Lab11
{
    internal class Program
    {
        private static string firebase_URl = "https://studyfirebase-3848c-default-rtdb.asia-southeast1.firebasedatabase.app/";
        static async Task Main(string[] args)
        {
            Dictionary<int, Player> playerList = new Dictionary<int, Player>();
            await Bai1();
            await Bai2();
        }
        public static async Task vd2()
        {
            var firebase = new FirebaseClient(firebase_URl);
            string url = "https://raw.githubusercontent.com/NTH-VTC/OnlineDemoC-/main/simple_players.json";

            HttpClient client = new HttpClient();
            string serverConfig = await client.GetStringAsync(url);
            List<Player> allPlayers;
            try
            {
                string json = await client.GetStringAsync("https://raw.githubusercontent.com/NTH-VTC/OnlineDemoC-/main/simple_players.json");

                allPlayers = JsonConvert.DeserializeObject<List<Player>>(json);
                var goldPlayers = allPlayers
                    .ToList();
                int index = 1;
                foreach (Player player in goldPlayers)
                {

                    Console.WriteLine(player.ToString());
                    await firebase.Child("Player").Child(index.ToString()).PutAsync(player);
                    index++;
                  

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static async Task Bai1()
        {
            var firebase = new FirebaseClient(firebase_URl);
            string url = "https://raw.githubusercontent.com/NTH-VTC/OnlineDemoC-/main/simple_players.json";

            HttpClient client = new HttpClient();
            string serverConfig = await client.GetStringAsync(url);
            List<Player> allPlayers;
            try
            {
                string json = await client.GetStringAsync("https://raw.githubusercontent.com/NTH-VTC/OnlineDemoC-/main/simple_players.json");

                allPlayers = JsonConvert.DeserializeObject<List<Player>>(json);
                var goldPlayers = allPlayers
                    .Where(p => p.Gold > 1000 && p.Coins > 100)
                    
                    .ToList();
                int index = 1;
               
                foreach (var p in goldPlayers)
                {

                    Console.WriteLine($"Name: {p.Name}, Gold: {p.Gold}, Coins: {p.Coins}");
                    await firebase.Child("quiz_bai1_richPlayers").Child(index.ToString()).PutAsync(p);
                    index++;
                  

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static async Task Bai2()
        {
            var firebase = new FirebaseClient(firebase_URl);
            string url = "https://raw.githubusercontent.com/NTH-VTC/OnlineDemoC-/main/simple_players.json";

            HttpClient client = new HttpClient();
            string serverConfig = await client.GetStringAsync(url);
            List<Player> allPlayers;
            try
            {
                string json = await client.GetStringAsync("https://raw.githubusercontent.com/NTH-VTC/OnlineDemoC-/main/simple_players.json");

                allPlayers = JsonConvert.DeserializeObject<List<Player>>(json);
                var VipPlayerRegion = allPlayers
                    .Where(p => p.VipLevel > 0)
                    .GroupBy(p => p.Region)
                    
                    .Select(p => new
                    {
                        Vip = p.Key,
                        Count = p.Count()
                    })
                    
                   
                    .ToList();
                foreach(var vip in VipPlayerRegion)
                {
                    Console.WriteLine($"khu Vực: {vip.Vip}, số người chơi VIP : {vip.Count}");
                }
                DateTime now = new DateTime(2025, 06, 30, 0, 0, 0);
                var LoginPlayer = allPlayers
                    .Where(p => (now - p.LastLogin).TotalDays <= 2)
                    .ToList();
                //Console.WriteLine($"Tổng số người chơi có Viplevel > 0 là : {CountVipPlayer}");
                int index = 1;
                foreach (var p in LoginPlayer)
                {
                    Console.WriteLine($"Name: {p.Name}, Viplevel: {p.VipLevel}, LastLogin: {p.LastLogin}");

                    await firebase.Child("quiz_bai2_recentVipPlayers").Child(index.ToString()).PutAsync(p);
                    index++;


                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


    }
}
