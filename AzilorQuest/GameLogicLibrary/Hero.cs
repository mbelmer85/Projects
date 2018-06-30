using System;
using System.Collections.Generic;

namespace GameLogicLibrary
{
    public class Hero
    {
        //Constructors

        public Hero()
        {
            MaxHp = 20;
            Hp = 20;
            Str = 6;
            Dex = 6;
            Con = 7;
            Luck = 2;
            Exp = 0;
            Tnl = 16;
            Level = 1;
            Location = 0;
            X = 1;
            Y = 1;
            Facing = "East";
            StatusEff = "Healthy";

            //EQUIPMENT POPULATE

            Equipment = new List<Item>();
            var sword = new Item
            {
                Name = "Rusty Sword",
                Type = "Weapon",
                Desc = "rustyDesc",
                Power = 2,
                Armor = -1
            };
            Equipment.Add(sword);
            var empty = new Item
            {
                Name = "Empty",
                Type = "Shield",
                Armor = 0
            };
            Equipment.Add(empty);
            Equipment.Add(empty);
            var armor = new Item
            {
                Name = "Leather Armor",
                Type = "Armor",
                Desc = "leaArmDesc",
                Power = -1,
                Armor = 3
            };
            Equipment.Add(armor);
            var boots = new Item
            {
                Name = "Leather Boots",
                Type = "Boots",
                Desc = "leaBootDesc",
                Power = -1,
                Armor = 1
            };
            Equipment.Add(boots);
            Inventory = new List<Item>();
        }

        public Hero(int maxHp, int hp, int str, int dex, int con, int luck, string statusEff, int exp, int level, int x, int y, int location, string facing, List<Item> eq, List<Item> inv)
        {
            MaxHp = maxHp;
            Hp = hp;
            Str = str;
            Dex = dex;
            Con = con;
            Luck = luck;
            Exp = exp;
            Level = level;
            Tnl = 0; //FIX FOR SAVING
            Equipment = eq;
            Inventory = inv;
            Facing = facing;
            Location = location;
            StatusEff = statusEff;
        }

        //Methods

        public void LevelUp()
        {
            var rand = new Random();
            var lv = Convert.ToDouble(Level);
            Exp = 0;
            Tnl = Convert.ToInt32(Math.Pow(lv + 2, (3.5)));
            MaxHp = MaxHp + Con * (2);
            Hp = MaxHp;
            Str = Str + rand.Next(3) + 1;
            Dex = Dex + rand.Next(3) + 1;
            Con =  Con + rand.Next(2) + 1;
            Luck = Luck + rand.Next(2);
            Level++;
            
        }
        //Accessors
        public int MaxHp { get; set; }
        public int Hp { get; set; }
        public int Str { get; set; }
        public int Dex { get; set; }
        public int Con { get; set; }
        public int Luck { get; set; }
        public int Exp { get; set; }
        public int Tnl { get; set; }
        public int Level { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Location { get; set; }
        public string StatusEff { get; set; }
        public string Facing { get; set; }
        public List<Item> Equipment { get; set; }
        public List<Item> Inventory { get; set; }
    }
}
