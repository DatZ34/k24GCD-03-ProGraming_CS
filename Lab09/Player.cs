using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab09
{
    internal class Player
    {
        public string PlayerID { get; set; }
        public string Name { get; set; }
        public int Gold { get; set; }
        public int Score { get; set; }
        public Player()
        {
            PlayerID = string.Empty;
            Name = string.Empty;
            Gold = 0;
            Score = 0;
        }
        public Player(string playerID, string name, int gold, int score)
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
