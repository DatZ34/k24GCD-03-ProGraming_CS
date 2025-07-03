using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB12_FINAL
{
    internal class Player
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public int Gold { get; set; }
        public int Coins { get; set; }
        public bool IsActive { get; set; }
        public int VipLevel { get; set; }
        public string Region { get; set; }

        public DateTime LastLogin { get; set; }
        public Player()
        {
            Id = 0;
            Name = string.Empty;
            Level = 0;
            Gold = 0;
            Coins = 0;
            VipLevel = 0;
            Region = string.Empty;
            LastLogin = DateTime.MinValue;

        }
        public override string ToString()
        {
            return "\nID: " + Id + " Name: " + Name + " Level: " + Level + " Gold: " + Gold + " Coins: " + Coins + "\nVipLevel: " + VipLevel + " Region: " + Region + " LastLogin: " + LastLogin;
        }
    }
}
