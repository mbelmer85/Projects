using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzilorQuest
{
    class Monster
    {
        //Constructors
        public Monster(string mob)
        {
            switch (mob)
            {
                case "Slime":
                    Mob = "Slime";
                    HP = 10;
                    Str = 11;
                    Dex = 4;
                    Con = 5;
                    Luck = 2;
                    Exp = 4;
                    Alive = true;
                    break;
                case "Rat":
                    HP = 20;
                    Str = 13;
                    Dex = 6;
                    Con = 4;
                    Luck = 2;
                    Exp = 10;
                    Alive = true;
                    break;
                case "Spider":
                    HP = 20;
                    Str = 14;
                    Dex = 8;
                    Con = 4;
                    Luck = 2;
                    Exp = 15;
                    Alive = true;
                    break;
                case "Giant Toad":
                    HP = 65;
                    Str = 18;
                    Dex = 12;
                    Con = 10;
                    Luck = 5;
                    Exp = 150;
                    Alive = true;
                    break;
                case "Wolf":
                    HP = 45;
                    Str = 24;
                    Dex = 16;
                    Con = 12;
                    Luck = 3;
                    Exp = 165;
                    Alive = true;
                    break;
                case "Snake":
                    HP = 35;
                    Str = 22;
                    Dex = 12;
                    Con = 10;
                    Luck = 3;
                    Exp = 100;
                    Alive = true;
                    break;
                case "Skeleton":
                    HP = 50;
                    Str = 28;
                    Dex = 14;
                    Con = 14;
                    Luck = 5;
                    Exp = 220;
                    Alive = true;
                    break;
                case "Harpy":
                    HP = 100;
                    Str = 45;
                    Dex = 20;
                    Con = 15;
                    Luck = 8;
                    Exp = 1000;
                    Alive = true;
                    break;
            }
        }

        public string Mob { get; }
        public int HP { get; set; }
        public int Str { get; }
        public int Dex { get; }
        public int Con { get; }
        public int Luck { get; }
        public int Exp { get; }
        public bool Alive { get; set; }
    }
}
