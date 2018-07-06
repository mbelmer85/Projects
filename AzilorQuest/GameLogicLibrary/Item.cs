namespace GameLogicLibrary
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

        //ITEM CREATORS

        public void CreateItem(string item, Hero _hero)
        {
            switch (item)
            {
                case "PotionSmall":
                    Item potion = new Item
                    {
                        Name = "Potion (Small)",
                        Type = "Consumable",
                        Desc = "A small potion that will heal 30 hit points.",
                        Power = 30,
                        Armor = -1
                    };
                    _hero.Inventory.Add(potion);
                    break;
                case "PotionMedium":
                    Item potion1 = new Item
                    {
                        Name = "Potion (Medium)",
                        Type = "Consumable",
                        Desc = "A medium potion that will heal 100 hit points.",
                        Power = 100,
                        Armor = -1
                    };
                    _hero.Inventory.Add(potion1);
                    break;
                case "PotionLarge":
                    Item potion2 = new Item
                    {
                        Name = "Potion (Large)",
                        Type = "Consumable",
                        Desc = "A large potion that will heal 500 hit points.",
                        Power = 500,
                        Armor = -1
                    };
                    _hero.Inventory.Add(potion2);
                    break;
                case "Antidote":
                    Item antidote = new Item
                    {
                        Name = "Antidote",
                        Type = "Consumable",
                        Desc = "A green herb that when consumed, cures poison.",
                        Power = -1,
                        Armor = -1
                    };
                    _hero.Inventory.Add(antidote);
                    break;
                case "Bandage":
                    Item bandage = new Item
                    {
                        Name = "Bandage",
                        Type = "Consumable",
                        Desc = "A bandage to stop bleeding.",
                        Power = -1,
                        Armor = -1
                    };
                    _hero.Inventory.Add(bandage);
                    break;
                case "Rusty Sword":
                    Item sword = new Item
                    {
                        Name = "Rusty Sword",
                        Type = "Weapon",
                        Desc = "rustyDesc",
                        Power = 2,
                        Armor = -1
                    };
                    _hero.Inventory.Add(sword);
                    break;
                case "Leather Armor":
                    Item armor = new Item
                    {
                        Name = "Leather Armor",
                        Type = "Armor",
                        Desc = "leaArmDesc",
                        Power = -1,
                        Armor = 3
                    };
                    _hero.Inventory.Add(armor);
                    break;
                case "Leather Boots":
                    Item boots = new Item
                    {
                        Name = "Leather Boots",
                        Type = "Boots",
                        Desc = "leaBootDesc",
                        Power = -1,
                        Armor = 1
                    };
                    _hero.Inventory.Add(boots);
                    break;
                case "Leather Buckler":
                    Item shield = new Item
                    {
                        Name = "Leather Buckler",
                        Type = "Shield",
                        Desc = "leaBklrDesc",
                        Power = -1,
                        Armor = 2
                    };
                    _hero.Inventory.Add(shield);
                    break;
                case "Dirk":
                    Item dagger = new Item
                    {
                        Name = "Dirk",
                        Type = "Weapon",
                        Desc = "dirkDesc",
                        Power = 10,
                        Armor = -1
                    };
                    _hero.Inventory.Add(dagger);
                    break;
                case "Chainmail":
                    Item armor2 = new Item
                    {
                        Name = "Chainmail",
                        Type = "Armor",
                        Desc = "chainDesc",
                        Power = -1,
                        Armor = 5
                    };
                    _hero.Inventory.Add(armor2);
                    break;
                case "Sollerets":
                    Item boots2 = new Item
                    {
                        Name = "Sollerets",
                        Type = "Boots",
                        Desc = "solDesc",
                        Power = -1,
                        Armor = 2
                    };
                    _hero.Inventory.Add(boots2);
                    break;
                case "Targe":
                    Item shield2 = new Item
                    {
                        Name = "Targe",
                        Type = "Shield",
                        Desc = "tarDesc",
                        Power = -1,
                        Armor = 4
                    };
                    _hero.Inventory.Add(shield2);
                    break;
                case "Steel Longsword":
                    Item sword2 = new Item
                    {
                        Name = "Steel Longsword",
                        Type = "Weapon",
                        Desc = "lngswdDesc",
                        Power = 10,
                        Armor = -1
                    };
                    _hero.Inventory.Add(sword2);
                    break;
                case "Platemail":
                    Item armor3 = new Item
                    {
                        Name = "Platemail",
                        Type = "Armor",
                        Desc = "plateDesc",
                        Power = -1,
                        Armor = 15
                    };
                    _hero.Inventory.Add(armor3);
                    break;
                case "Sabatons":
                    Item boots3 = new Item
                    {
                        Name = "Sabatons",
                        Type = "Boots",
                        Desc = "sabDesc",
                        Power = -1,
                        Armor = 5
                    };
                    _hero.Inventory.Add(boots3);
                    break;
                case "Kite Shield":
                    Item shield3 = new Item
                    {
                        Name = "Kite Shield",
                        Type = "Shield",
                        Desc = "kiteDesc",
                        Power = -1,
                        Armor = 7
                    };
                    _hero.Inventory.Add(shield3);
                    break;
                case "Chain Coif":
                    Item coif = new Item
                    {
                        Name = "Chain Coif",
                        Type = "Helm",
                        Desc = "coifDesc",
                        Power = -1,
                        Armor = 2
                    };
                    _hero.Inventory.Add(coif);
                    break;
                case "Barbute":
                    Item barbute = new Item
                    {
                        Name = "Barbute",
                        Type = "Helm",
                        Desc = "barbDesc",
                        Power = -1,
                        Armor = 5
                    };
                    _hero.Inventory.Add(barbute);
                    break;
            }
        }
    }
}
