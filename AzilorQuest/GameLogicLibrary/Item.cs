﻿namespace GameLogicLibrary
{
    public class Item
    {
        //Constructors

        public Item()
        {
            Type = "";
            Name = "";
            Desc = "";
            Power = -1;
            Armor = -1;
            Equipped = false;
        }

        //Accessors

        public string Type { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public int Power { get; set; }
        public int Armor { get; set; }
        public bool Equipped { get; set; }
    }
}
