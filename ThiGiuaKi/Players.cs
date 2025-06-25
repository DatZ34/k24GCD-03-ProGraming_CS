using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThiGiuaKi
{
    internal class Players
    {
        public string PlayerID { get; set; }
        public string Name { get; set; }
        public int Gold { get; set; }
        public int Score { get; set; }
        public Players()
        {
            PlayerID = string.Empty;
            Name = string.Empty;
            Gold = 0;
            Score = 0;
        }
        public Players(string playerID, string name, int gold, int score)
        {
            PlayerID = playerID;
            Name = name;
            Gold = gold;
            Score = score;
        }
        public override string ToString()
        {
            return "\nName: " + Name + " / PlayerID: " + PlayerID + "\nGold: " + Gold + " / Score: " + Score;
        }
    }
}
