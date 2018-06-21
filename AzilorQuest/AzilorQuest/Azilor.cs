using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzilorQuest
{
    public class Azilor
    {
        //Constructors

        public Azilor()
        {
            MaxHP = 20;
            HP = 20;
            Str = 6;
            Dex = 6;
            Con = 7;
            Luck = 2;
            Exp = 0;
            TNL = 16;
            Level = 1;
            Location = 0;
            X = 1;
            Y = 1;
            Facing = "East";
            StatusEff = "Healthy";

            //EQUIPMENT POPULATE

            Equipment = new List<Item>();
            Item sword = new Item();
            sword.Name = "Rusty Sword";
            sword.Type = "Weapon";
            sword.Desc = "rustyDesc";
            sword.Power = 2;
            sword.Armor = -1;
            Equipment.Add(sword);
            Item empty = new Item();
            empty.Name = "Empty";
            empty.Type = "Shield";
            empty.Armor = 0;
            Equipment.Add(empty);
            Equipment.Add(empty);
            Item armor = new Item();
            armor.Name = "Leather Armor";
            armor.Type = "Armor";
            armor.Desc = "leaArmDesc";
            armor.Power = -1;
            armor.Armor = 3;
            Equipment.Add(armor);
            Item boots = new Item();
            boots.Name = "Leather Boots";
            boots.Type = "Boots";
            boots.Desc = "leaBootDesc";
            boots.Power = -1;
            boots.Armor = 1;
            Equipment.Add(boots);
            Inventory = new List<Item>();
        }

        public Azilor(int maxHp, int hp, int str, int dex, int con, int luck, string statusEff, int exp, int level, int x, int y, int location, string facing, List<Item> eq, List<Item> inv)
        {
            MaxHP = maxHp;
            HP = hp;
            Str = str;
            Dex = dex;
            Con = con;
            Luck = luck;
            Exp = exp;
            Level = level;
            TNL = 0; //FIX FOR SAVING
            Equipment = eq;
            Inventory = inv;
            Facing = facing;
            Location = location;
            StatusEff = statusEff;
        }

        //Methods

        public void levelUp()
        {
            Random rand = new Random();
            double lv = Convert.ToDouble(Level);
            Exp = 0;
            TNL = Convert.ToInt32(Math.Pow(lv + 2, (3.5)));
            MaxHP = MaxHP + Con * (2);
            HP = MaxHP;
            Str = Str + rand.Next(3) + 1;
            Dex = Dex + rand.Next(3) + 1;
            Con =  Con + rand.Next(2) + 1;
            Luck = Luck + rand.Next(2);
            Level++;
            
        }
        //Accessors
        public int MaxHP { get; set; }
        public int HP { get; set; }
        public int Str { get; set; }
        public int Dex { get; set; }
        public int Con { get; set; }
        public int Luck { get; set; }
        public int Exp { get; set; }
        public int TNL { get; set; }
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
