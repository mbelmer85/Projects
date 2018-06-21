using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using AzilorQuest.Properties;

namespace AzilorQuest
{
    public partial class AzQuest : Form
    {
        public AzQuest()
        {
            InitializeComponent();
        }

        //GLOBALS
        private List<MapTile> world;
        private Azilor hero;
        private Random rand;

        //LOAD EVENT
        private void AzQuest_Load(object sender, EventArgs e)
        {

            world = new List<MapTile>();
            hero = new Azilor();
            rand = new Random();
            HPLabel.Text = "HP:  " + hero.HP;
            expLabel.Text = "Exp:  " + hero.Exp;


            //SECOND ATTEMPT FOR MAPDATA POPULATION
            //string filePath = @".\mapData.txt";
            //List<string> lines = File.ReadAllLines(filePath).ToList();
            //foreach (string line in lines)
            //{
            //    string[] entries = line.Split(',');
            //    MapTile tile = new MapTile(int.Parse(entries[0]), int.Parse(entries[1]), int.Parse(entries[2]), int.Parse(entries[3]), entries[4], entries[5]);
            //    world.Add(tile);
            //}

            //FINAL WORKING METHOD FOR MAPDATA POPULATION
            int i = 0;
            string[] objects = Resources.MapData.Split(',');
            while (i < objects.Length)
            {
                MapTile tile = new MapTile();
                tile.X = int.Parse(objects[i]);
                i++;
                tile.Y = int.Parse(objects[i]);
                i++;
                tile.EventID = int.Parse(objects[i]);
                i++;
                tile.Section = int.Parse(objects[i]);
                i++;
                tile.Exits = objects[i];
                i++;
                tile.Description = objects[i];
                i++;
                world.Add(tile);
            }

            //FIRST ATTEMPT FOR MAPDATA POPULATION
            //StreamReader mapData = File.OpenText("MapData.txt");
            //while (!mapData.EndOfStream)
            //{
            //    MapTile tile = new MapTile(int.Parse(mapData.ReadLine()), int.Parse(mapData.ReadLine()), int.Parse(mapData.ReadLine()), int.Parse(mapData.ReadLine()), mapData.ReadLine(), mapData.ReadLine());
            //    world.Add(tile);
            //}

            //NEW GAME INVENTORY POPULATION
            createItem("PotionSmall");
            createItem("PotionSmall");
            createItem("Bandage");
            createItem("Rusty Sword");
            hero.Inventory[3].Equipped = true;
            createItem("Leather Armor");
            hero.Inventory[4].Equipped = true;
            createItem("Leather Boots");
            hero.Inventory[5].Equipped = true;
            //createItem("Steel Longsword");
            //createItem("Chainmail");
            createItem("Antidote");

            //UPDATE VIEWED MAPTILES
            uncoverTile(findTile(hero.X, hero.Y, world[hero.Location].Section));
            uncoverTile(findFacingTile(hero.Facing));

            //VIEW FIRST ROOM
            Look();

            //LOAD INTRO
            Welcome welcome = new Welcome();
            welcome.ShowDialog();
        }

        //CLICK EVENTS

        //DIRECTIONAL BUTTONS (North, South, East, West are actually Forward, Backward, Right, and Left)
        private void northBtn_Click(object sender, EventArgs e)
        {
            move(hero.Facing);
        }

        private void westBtn_Click_1(object sender, EventArgs e)
        {
            if (hero.Facing == "North")
            {
                move("West");
            }
            else if (hero.Facing == "South")
            {
                move("East");
            }
            else if (hero.Facing == "East")
            {
                move("North");
            }
            else if (hero.Facing == "West")
            {
                move("North");
            }
        }

        private void southBtn_Click(object sender, EventArgs e)
        {
            if (hero.Facing == "North")
            {
                move("South");
            }
            else if (hero.Facing == "South")
            {
                move("North");
            }
            else if (hero.Facing == "East")
            {
                move("West");
            }
            else if (hero.Facing == "West")
            {
                move("East");
            }
        }

        private void eastBtn_Click(object sender, EventArgs e)
        {
            if (hero.Facing == "North")
            {
                move("East");
            }
            else if (hero.Facing == "South")
            {
                move("West");
            }
            else if (hero.Facing == "East")
            {
                move("South");
            }
            else if (hero.Facing == "West")
            {
                move("North");
            }
        }

        //TURNING
        private void turnLeftBtn_Click_1(object sender, EventArgs e)
        {
            switch (hero.Facing)
            {
                case "North":
                    hero.Facing = "West";
                    break;
                case "West":
                    hero.Facing = "South";
                    break;
                case "South":
                    hero.Facing = "East";
                    break;
                case "East":
                    hero.Facing = "North";
                    break;
            }
            facingLabel.Text = "Facing:  " + hero.Facing;
            Look();
        }

        private void turnRightBtn_Click_1(object sender, EventArgs e)
        {
            switch (hero.Facing)
            {
                case "North":
                    hero.Facing = "East";
                    break;
                case "East":
                    hero.Facing = "South";
                    break;
                case "South":
                    hero.Facing = "West";
                    break;
                case "West":
                    hero.Facing = "North";
                    break;
            }
            facingLabel.Text = "Facing:  " + hero.Facing;
            Look();
        }

        //MENU BUTTONS
        private void statsBtn_Click(object sender, EventArgs e)
        {
            userDisplay.Clear();
            userDisplay.AppendText("STATUS:       ");
            userDisplay.AppendText(Environment.NewLine);
            userDisplay.AppendText(" Level:       " + hero.Level);
            userDisplay.AppendText(Environment.NewLine);
            userDisplay.AppendText("  Max HP:     " + hero.MaxHP);
            userDisplay.AppendText(Environment.NewLine);
            userDisplay.AppendText("  HP:         " + hero.HP);
            userDisplay.AppendText(Environment.NewLine);
            userDisplay.AppendText("  Str:        " + hero.Str);
            userDisplay.AppendText(Environment.NewLine);
            userDisplay.AppendText("  Dex:        " + hero.Dex);
            userDisplay.AppendText(Environment.NewLine);
            userDisplay.AppendText("  Con:        " + hero.Con);
            userDisplay.AppendText(Environment.NewLine);
            userDisplay.AppendText("  Luk:        " + hero.Luck);
            userDisplay.AppendText(Environment.NewLine);
            userDisplay.AppendText("  Exp:        " + hero.Exp);
            userDisplay.AppendText(Environment.NewLine);
            userDisplay.AppendText("  Next Level: " + hero.TNL);
            userDisplay.AppendText(Environment.NewLine);
            userDisplay.AppendText("  Status:     " + hero.StatusEff);
            userDisplay.AppendText(Environment.NewLine);
        }

        private void label3_Click(object sender, EventArgs e)
        {
            userDisplay.Clear();
            Look();
        }

        private void equipmentBtn_Click(object sender, EventArgs e)
        {
            userDisplay.Clear();
            userDisplay.AppendText("Currently Equipped:");
            userDisplay.AppendText(Environment.NewLine);
            userDisplay.AppendText("   WEAPON:  " + hero.Equipment[0].Name);
            userDisplay.AppendText(Environment.NewLine);
            userDisplay.AppendText("   SHIELD:  " + hero.Equipment[1].Name);
            userDisplay.AppendText(Environment.NewLine);
            userDisplay.AppendText("   HELM:    " + hero.Equipment[2].Name);
            userDisplay.AppendText(Environment.NewLine);
            userDisplay.AppendText("   ARMOR:   " + hero.Equipment[3].Name);
            userDisplay.AppendText(Environment.NewLine);
            userDisplay.AppendText("   BOOTS:   " + hero.Equipment[4].Name);
            userDisplay.AppendText(Environment.NewLine);
        }

        private void restBtn_Click(object sender, EventArgs e)
        {
            if (hero.StatusEff == "Healthy")
            {
                userDisplay.Clear();
                if (ChanceEncounter())
                {
                    userDisplay.AppendText("You're under attack!!");
                    userDisplay.AppendText(Environment.NewLine);
                }
                else
                {
                    hero.HP = hero.MaxHP;
                    userDisplay.AppendText("You manage to get some rest.");
                    userDisplay.AppendText(Environment.NewLine);
                }
            }
            else if (hero.StatusEff == "Dead")
            {
                userDisplay.AppendText("You are magically revived... somehow...");
                userDisplay.AppendText(Environment.NewLine);
                hero.HP = hero.MaxHP;
                hero.StatusEff = "Healthy";
            }
            else
            {
                userDisplay.AppendText("You are " + hero.StatusEff + " and cannot rest.");
                userDisplay.AppendText(Environment.NewLine);
            }
            HPLabel.Text = "HP:  " + hero.HP;
        }

        private void quitBtn_Click(object sender, EventArgs e)
        {
            Quit quit = new Quit(this);
            quit.ShowDialog();
        }

        private void invBtn_Click(object sender, EventArgs e)
        {
            Inventory inv = new Inventory(hero, this);
            inv.ShowDialog();
        }

        private void mapBtn_Click_1(object sender, EventArgs e)
        {
            Map map = new Map(world, hero);
            map.ShowDialog();
        }

        //DEBUG INSTANT LEVEL UP BUTTON
        private void button1_Click(object sender, EventArgs e)
        {
            hero.levelUp();
        }

        //CUSTOM METHODS
        private void move(string direction)
        {
            switch (direction)
            {
                case "East":
                    for (int i = 0; i < world.Count; i++)
                    {
                        if (world[i].X == hero.X + 1 && world[i].Y == hero.Y && world[i].Section == world[hero.Location].Section && world[i].Exits.Contains("West"))
                        {
                            hero.X = world[i].X;
                            hero.Location = i;
                            //Check for event
                            //Run event if found
                            Look();
                            eventCheck();
                            applyStatusDamage();
                            return;
                        }
                    }
                    Bump();
                    break;
                case "South":
                    for (int i = 0; i < world.Count; i++)
                    {
                        if (world[i].X == hero.X && world[i].Y == hero.Y + 1 && world[i].Section == world[hero.Location].Section && world[i].Exits.Contains("North"))
                        {
                            hero.Y = world[i].Y;
                            hero.Location = i;
                            //Check for event
                            //Run event if found
                            Look();
                            eventCheck();
                            applyStatusDamage();
                            return;
                        }
                    }
                    Bump();
                    break;
                case "West":
                    for (int i = 0; i < world.Count; i++)
                    {
                        if (world[i].X == hero.X - 1 && world[i].Y == hero.Y && world[i].Section == world[hero.Location].Section && world[i].Exits.Contains("East"))
                        {
                            hero.X = world[i].X;
                            hero.Location = i;
                            //Check for event
                            //Run event if found
                            applyStatusDamage();
                            Look();
                            eventCheck();
                            return;
                        }
                    }
                    Bump();
                    break;
                case "North":
                    for (int i = 0; i < world.Count; i++)
                    {
                        if (world[i].X == hero.X && world[i].Y == hero.Y - 1 && world[i].Section == world[hero.Location].Section && world[i].Exits.Contains("South"))
                        {
                            hero.Y = world[i].Y;
                            hero.Location = i;
                            //Check for event
                            //Run event if found
                            Look();
                            eventCheck();
                            applyStatusDamage();
                            return;
                        }
                    }
                    Bump();
                    break;
            }
        }

        //PRINT ROOM DESCRIPTION AND UPDATE UI LABELS
        private void Look()
        {
            userDisplay.Clear();
            for (int i = 0; i < world.Count; i++)
            {
                if (world[i].X == hero.X && world[i].Y == hero.Y && world[i].Section == world[hero.Location].Section)
                {
                    userDisplay.AppendText("Current X,Y: " + hero.X + "," + hero.Y);
                    userDisplay.AppendText(Environment.NewLine);
                    //outputDisplay.Items.Add("Event: " + world[i].EventID);
                    //outputDisplay.Items.Add("Dungeon Section: " + world[i].Section);
                    //outputDisplay.Items.Add("Current Exits: " + world[i].Exits);
                    userDisplay.AppendText(world[i].Description);
                    userDisplay.AppendText(Environment.NewLine);
                }
            }
            HPLabel.Text = "HP:  " + hero.HP;
            expLabel.Text = "Exp:  " + hero.Exp;
            uncoverTile(findTile(hero.X, hero.Y, world[hero.Location].Section));
            uncoverTile(findFacingTile("East"));
            uncoverTile(findFacingTile("North"));
            uncoverTile(findFacingTile("South"));
            uncoverTile(findFacingTile("West"));
            miniMapUpdate();
        }

        //CHECK FOR A STATUS EFFECT AND APPLY EFFECT
        private void applyStatusDamage()
        {
            if (hero.StatusEff == "Poisoned")
            {
                hero.HP -= hero.MaxHP / 20;
                userDisplay.AppendText("You take " + (hero.MaxHP / 20).ToString() + " points of damage from poison.");
                userDisplay.AppendText(Environment.NewLine);
                if (hero.HP < 1)
                {
                    userDisplay.AppendText("You have succumed to the poison.");
                    userDisplay.AppendText(Environment.NewLine);
                    hero.StatusEff = "Dead";
                }
            }
        }

        //CHANGE TILE TO EXPLORED
        private void uncoverTile(MapTile tile)
        {
            tile.Explored = true;
        }


        //RETURN TILE FOUND AT COORDINATES X,Y AND FLOOR
        private MapTile findTile(int x, int y, int section)
        {
            for (int i = 0; i < world.Count; i++)
            {
                if (world[i].X == x && world[i].Y == y && world[i].Section == section)
                {
                    return world[i];
                }
            }
            return world[hero.Location];
        }

        //RETURN TILE IN FRONT OF PLAYER
        private MapTile findFacingTile(string direction)
        {
            switch (direction)
            {
                case "North":
                    for (int i = 0; i < world.Count; i++)
                    {
                        if (world[i].X == hero.X && world[i].Y == hero.Y - 1 && world[i].Section == world[hero.Location].Section && world[hero.Location].Exits.Contains("North"))
                        {
                            return world[i];
                        }
                    }
                    return world[hero.Location];
                case "East":
                    for (int i = 0; i < world.Count; i++)
                    {
                        if (world[i].X == hero.X + 1 && world[i].Y == hero.Y && world[i].Section == world[hero.Location].Section && world[hero.Location].Exits.Contains("East"))
                        {
                            return world[i];
                        }
                    }
                    return world[hero.Location];
                case "South":
                    for (int i = 0; i < world.Count; i++)
                    {
                        if (world[i].X == hero.X && world[i].Y == hero.Y + 1 && world[i].Section == world[hero.Location].Section && world[hero.Location].Exits.Contains("South"))
                        {
                            return world[i];
                        }
                    }
                    return world[hero.Location];
                case "West":
                    for (int i = 0; i < world.Count; i++)
                    {
                        if (world[i].X == hero.X - 1 && world[i].Y == hero.Y && world[i].Section == world[hero.Location].Section && world[hero.Location].Exits.Contains("West"))
                        {
                            return world[i];
                        }
                    }
                    return world[hero.Location];
                default:
                    return world[hero.Location];
            }
        }
        
        //ALERT PLAYER OF WALL
        private async Task Bump()
        {
            userDisplay.Clear();
            userDisplay.AppendText("**BUMP** Stop walking into walls!");
            userDisplay.AppendText(Environment.NewLine);
            await Task.Delay(1000);
            Look();
        }


        //EXECUTE CHANCE TO ENCOUNTER MONSTER
        private bool ChanceEncounter()
        {
            string mobChoice = "";
            //Generate random number
            int ranGen = rand.Next(99) + 1;
            //20% Chance for battle
            if (ranGen <= 20 && world[hero.Location].EventID <= 0)
            {
                ranGen = rand.Next(99) + 1;
                switch (world[hero.Location].Section)
                {
                    case 1:
                        if (ranGen <= 60)
                        {
                            mobChoice = "Slime";
                        }
                        else if (ranGen <= 90)
                        {
                            mobChoice = "Rat";
                        }
                        else if (ranGen <= 100)
                        {
                            mobChoice = "Spider";
                        }
                        break;
                    case 2:
                        if (ranGen <= 60)
                        {
                            mobChoice = "Snake";
                        }
                        else if (ranGen <= 90)
                        {
                            mobChoice = "Wolf";
                        }
                        else if (ranGen <= 100)
                        {
                            mobChoice = "Skeleton";
                        }
                        break;
                    case 3:
                        if (ranGen <= 50)
                        {
                            mobChoice = "Imp";
                        }
                        else if (ranGen <= 80)
                        {
                            mobChoice = "Owlbear";
                        }
                        else if (ranGen <= 100)
                        {
                            mobChoice = "Cockatrice";
                        }
                        break;
                    case 4:
                        if (ranGen <= 50)
                        {
                            mobChoice = "Ogre";
                        }
                        else if (ranGen <= 80)
                        {
                            mobChoice = "Werewolf";
                        }
                        else if (ranGen <= 100)
                        {
                            mobChoice = "Wyvern";
                        }
                        break;
                }
                //Run battle
                BattleForm battle = new BattleForm(mobChoice, hero, world, this);
                battle.ShowDialog();
                return true;
            }
            return false;
        }


        //ALL EVENTS
        async private void eventCheck()
        {
            switch (world[hero.Location].EventID)
            {
                case 9:
                    //Fight Boss Floor 1
                    BattleForm battle = new BattleForm("Giant Toad", hero, world, this);
                    battle.ShowDialog();
                    break;
                case 26:
                    //Fight Boss Floor 2
                    BattleForm battle1 = new BattleForm("Harpy", hero, world, this);
                    battle1.ShowDialog();
                    break;
                case 5:
                    //Leather Buckler Chest
                    userDisplay.AppendText("You find a chest with a Leather Buckler inside!");
                    userDisplay.AppendText(Environment.NewLine);
                    userDisplay.AppendText("You obtain the Leather Buckler.");
                    userDisplay.AppendText(Environment.NewLine);
                    createItem("Leather Buckler");
                    world[hero.Location].EventID = -1;
                    break;
                case 3:
                    //Potion and Antidote in Skeleton
                    userDisplay.AppendText("After searching a skeleton along the wall");
                    userDisplay.AppendText(Environment.NewLine);
                    userDisplay.AppendText("you find a small potion and antidote herb.");
                    userDisplay.AppendText(Environment.NewLine);
                    userDisplay.AppendText("You obtain the small potion.");
                    userDisplay.AppendText(Environment.NewLine);
                    userDisplay.AppendText("You obtain the antidote.");
                    userDisplay.AppendText(Environment.NewLine);
                    createItem("PotionSmall");
                    createItem("Antidote");
                    world[hero.Location].EventID = -1;
                    break;
                case 7:
                    //Potion Chest Small
                    userDisplay.AppendText("You find a chest with a small potion inside!");
                    userDisplay.AppendText(Environment.NewLine);
                    userDisplay.AppendText("You obtain the small potion.");
                    userDisplay.AppendText(Environment.NewLine);
                    createItem("PotionSmall");
                    world[hero.Location].EventID = -1;
                    break;
                case 8:
                    //Potion and Antidote Chest
                    userDisplay.AppendText("You find a chest with a small potion and antidote herb inside!");
                    userDisplay.AppendText(Environment.NewLine);
                    userDisplay.AppendText("You obtain the small potion.");
                    userDisplay.AppendText(Environment.NewLine);
                    userDisplay.AppendText("You obtain the antidote.");
                    userDisplay.AppendText(Environment.NewLine);
                    createItem("PotionSmall");
                    createItem("Antidote");
                    world[hero.Location].EventID = -1;
                    break;
                case 20:
                    //Potion Chest Medium
                    userDisplay.AppendText("You find a chest with a medium potion inside!");
                    userDisplay.AppendText(Environment.NewLine);
                    userDisplay.AppendText("You obtain the medium potion.");
                    userDisplay.AppendText(Environment.NewLine);
                    createItem("PotionMedium");
                    world[hero.Location].EventID = -1;
                    break;
                case 21:
                    //Chainmail in Sarcophagas
                    userDisplay.AppendText("You find a sarcophagas with chainmail armor on the corpse inside.");
                    userDisplay.AppendText(Environment.NewLine);
                    userDisplay.AppendText("You obtain the Chainmail.");
                    userDisplay.AppendText(Environment.NewLine);
                    createItem("Chainmail");
                    world[hero.Location].EventID = -1;
                    break;
                case 22:
                    //Chain Coif in chest
                    userDisplay.AppendText("You find a chest with a chain coif inside!");
                    userDisplay.AppendText(Environment.NewLine);
                    userDisplay.AppendText("You obtain the Chain Coif.");
                    userDisplay.AppendText(Environment.NewLine);
                    createItem("Chain Coif");
                    world[hero.Location].EventID = -1;
                    break;
                case 23:
                    //Solletrets in chest
                    userDisplay.AppendText("You find a chest with sollerets inside!");
                    userDisplay.AppendText(Environment.NewLine);
                    userDisplay.AppendText("You obtain the Sollerets.");
                    userDisplay.AppendText(Environment.NewLine);
                    createItem("Sollerets");
                    world[hero.Location].EventID = -1;
                    break;
                case 24:
                    //Targe on fresh corpse
                    userDisplay.AppendText("There is a fresh corpse here with a targe on it's arm.");
                    userDisplay.AppendText(Environment.NewLine);
                    userDisplay.AppendText("You remove the Targe.");
                    userDisplay.AppendText(Environment.NewLine);
                    createItem("Targe");
                    world[hero.Location].EventID = -1;
                    break;
                case 25:
                    //Dirk on Pedestal
                    userDisplay.AppendText("Sitting on a marble pedestal you find a deadly looking Dirk.");
                    userDisplay.AppendText(Environment.NewLine);
                    userDisplay.AppendText("You obtain the Dirk.");
                    userDisplay.AppendText(Environment.NewLine);
                    createItem("Dirk");
                    world[hero.Location].EventID = -1;
                    break;
                case 11:
                    //Poison Gas Room
                    userDisplay.AppendText("This room seems to be filled with noxious gas!!");
                    userDisplay.AppendText(Environment.NewLine);
                    userDisplay.AppendText("You take damage!");
                    userDisplay.AppendText(Environment.NewLine);
                    hero.HP -= 5;
                    ChanceEncounter();
                    break;
                case 10:
                    //Stairs to 2nd Floor
                    userDisplay.AppendText("You walk down the cold stone steps...");
                    userDisplay.AppendText(Environment.NewLine);
                    await Task.Delay(1000);
                    for(int i = 0; i < world.Count; i++)
                    {
                        if(world[i].X == 9 && world[i].Y == 19 && world[i].Section == 2)
                        {
                            hero.Location = i;
                            hero.X = world[i].X;
                            hero.Y = world[i].Y;
                            Look();
                            userDisplay.AppendText("You feel the door behind you shut and lock as you enter.");
                            userDisplay.AppendText(Environment.NewLine);
                        }
                    }
                    break;
                case 27:
                    //Teleport to Entrance of 2nd Floor
                    userDisplay.AppendText("An odd sensation grabs you as your are teleported!!");
                    userDisplay.AppendText(Environment.NewLine);
                    await Task.Delay(1000);
                    for (int i = 0; i < world.Count; i++)
                    {
                        if (world[i].X == 9 && world[i].Y == 19 && world[i].Section == 2)
                        {
                            hero.Location = i;
                            hero.X = world[i].X;
                            hero.Y = world[i].Y;
                            userDisplay.AppendText("You feel as though you've been here before...");
                            userDisplay.AppendText(Environment.NewLine);
                        }
                    }
                    break;
                default:
                    ChanceEncounter();
                    break;
            }
        }

        

        //INVENTORY METHODS

        //ITEM CREATORS

        public void createItem(string item)
        {
            switch (item)
            {
                case "PotionSmall":
                    Item potion = new Item();
                    potion.Name = "Potion (Small)";
                    potion.Type = "Consumable";
                    potion.Desc = "A small potion that will heal 30 hit points.";
                    potion.Power = 30;
                    potion.Armor = -1;
                    hero.Inventory.Add(potion);
                    break;
                case "PotionMedium":
                    Item potion1 = new Item();
                    potion1.Name = "Potion (Medium)";
                    potion1.Type = "Consumable";
                    potion1.Desc = "A medium potion that will heal 100 hit points.";
                    potion1.Power = 100;
                    potion1.Armor = -1;
                    hero.Inventory.Add(potion1);
                    break;
                case "PotionLarge":
                    Item potion2 = new Item();
                    potion2.Name = "Potion (Large)";
                    potion2.Type = "Consumable";
                    potion2.Desc = "A large potion that will heal 500 hit points.";
                    potion2.Power = 500;
                    potion2.Armor = -1;
                    hero.Inventory.Add(potion2);
                    break;
                case "Antidote":
                    Item antidote = new Item();
                    antidote.Name = "Antidote";
                    antidote.Type = "Consumable";
                    antidote.Desc = "A green herb that when consumed, cures poison.";
                    antidote.Power = -1;
                    antidote.Armor = -1;
                    hero.Inventory.Add(antidote);
                    break;
                case "Bandage":
                    Item bandage = new Item();
                    bandage.Name = "Bandage";
                    bandage.Type = "Consumable";
                    bandage.Desc = "A bandage to stop bleeding.";
                    bandage.Power = -1;
                    bandage.Armor = -1;
                    hero.Inventory.Add(bandage);
                    break;
                case "Rusty Sword":
                    Item sword = new Item();
                    sword.Name = "Rusty Sword";
                    sword.Type = "Weapon";
                    sword.Desc = "rustyDesc";
                    sword.Power = 2;
                    sword.Armor = -1;
                    hero.Inventory.Add(sword);
                    break;
                case "Leather Armor":
                    Item armor = new Item();
                    armor.Name = "Leather Armor";
                    armor.Type = "Armor";
                    armor.Desc = "leaArmDesc";
                    armor.Power = -1;
                    armor.Armor = 3;
                    hero.Inventory.Add(armor);
                    break;
                case "Leather Boots":
                    Item boots = new Item();
                    boots.Name = "Leather Boots";
                    boots.Type = "Boots";
                    boots.Desc = "leaBootDesc";
                    boots.Power = -1;
                    boots.Armor = 1;
                    hero.Inventory.Add(boots);
                    break;
                case "Leather Buckler":
                    Item shield = new Item();
                    shield.Name = "Leather Buckler";
                    shield.Type = "Shield";
                    shield.Desc = "leaBklrDesc";
                    shield.Power = -1;
                    shield.Armor = 2;
                    hero.Inventory.Add(shield);
                    break;
                case "Dirk":
                    Item dagger = new Item();
                    dagger.Name = "Dirk";
                    dagger.Type = "Weapon";
                    dagger.Desc = "dirkDesc";
                    dagger.Power = 10;
                    dagger.Armor = -1;
                    hero.Inventory.Add(dagger);
                    break;
                case "Chainmail":
                    Item armor2 = new Item();
                    armor2.Name = "Chainmail";
                    armor2.Type = "Armor";
                    armor2.Desc = "chainDesc";
                    armor2.Power = -1;
                    armor2.Armor = 5;
                    hero.Inventory.Add(armor2);
                    break;
                case "Sollerets":
                    Item boots2 = new Item();
                    boots2.Name = "Sollerets";
                    boots2.Type = "Boots";
                    boots2.Desc = "solDesc";
                    boots2.Power = -1;
                    boots2.Armor = 2;
                    hero.Inventory.Add(boots2);
                    break;
                case "Targe":
                    Item shield2 = new Item();
                    shield2.Name = "Targe";
                    shield2.Type = "Shield";
                    shield2.Desc = "tarDesc";
                    shield2.Power = -1;
                    shield2.Armor = 4;
                    hero.Inventory.Add(shield2);
                    break;
                case "Steel Longsword":
                    Item sword2 = new Item();
                    sword2.Name = "Steel Longsword";
                    sword2.Type = "Weapon";
                    sword2.Desc = "lngswdDesc";
                    sword2.Power = 10;
                    sword2.Armor = -1;
                    hero.Inventory.Add(sword2);
                    break;
                case "Platemail":
                    Item armor3 = new Item();
                    armor3.Name = "Platemail";
                    armor3.Type = "Armor";
                    armor3.Desc = "plateDesc";
                    armor3.Power = -1;
                    armor3.Armor = 15;
                    hero.Inventory.Add(armor3);
                    break;
                case "Sabatons":
                    Item boots3 = new Item();
                    boots3.Name = "Sabatons";
                    boots3.Type = "Boots";
                    boots3.Desc = "sabDesc";
                    boots3.Power = -1;
                    boots3.Armor = 5;
                    hero.Inventory.Add(boots3);
                    break;
                case "Kite Shield":
                    Item shield3 = new Item();
                    shield3.Name = "Kite Shield";
                    shield3.Type = "Shield";
                    shield3.Desc = "kiteDesc";
                    shield3.Power = -1;
                    shield3.Armor = 7;
                    hero.Inventory.Add(shield3);
                    break;
                case "Chain Coif":
                    Item coif = new Item();
                    coif.Name = "Chain Coif";
                    coif.Type = "Helm";
                    coif.Desc = "coifDesc";
                    coif.Power = -1;
                    coif.Armor = 2;
                    hero.Inventory.Add(coif);
                    break;
                case "Barbute":
                    Item barbute = new Item();
                    barbute.Name = "Barbute";
                    barbute.Type = "Helm";
                    barbute.Desc = "barbDesc";
                    barbute.Power = -1;
                    barbute.Armor = 5;
                    hero.Inventory.Add(barbute);
                    break;
            }
        }

        //UPDATE UI TEXT
        public void updateHP()
        {
            HPLabel.Text = "HP:  " + hero.HP;
        }

        public void updateEXP()
        {
            expLabel.Text = "Exp:  " + hero.Exp;
        }

        //CHECK IF TILE EXISTS
        private bool tileExists(int x, int y, int section)
        {
            for(int i = 0; i < world.Count; i++)
            {
                if(world[i].X == x && world[i].Y == y && world[i].Section == section)
                {
                    return true;
                }
            }
            return false;
        }

        //RETURN IMAGE CORRESPONDING TO AVAILABLE EXITS ON TILE PASSED
        private Image getMapImage(MapTile tile)
        {
            if (!tile.Exits.Contains("North"))
            {
                if (!tile.Exits.Contains("West"))
                {
                    if (!tile.Exits.Contains("South"))
                    {
                        return Resources.northWestSouthWall;
                    }
                    else if (!tile.Exits.Contains("East"))
                    {
                        return Resources.northWestEastWall;
                    }
                    else
                    {
                        return Resources.northWestWall;
                    }
                }
                else if (!tile.Exits.Contains("South"))
                {
                    if (!tile.Exits.Contains("East"))
                    {
                        return Resources.northSouthEastWall;
                    }
                    else
                    {
                        return Resources.northSouthWall;
                    }
                }
                else if (!tile.Exits.Contains("East"))
                {
                    return Resources.northEastWall;
                }
                else
                {
                    return Resources.northWall;
                }
            }
            else if (!tile.Exits.Contains("West"))
            {
                if (!tile.Exits.Contains("South"))
                {
                    if (!tile.Exits.Contains("East"))
                    {
                        return Resources.westSouthEastWall;
                    }
                    else
                    {
                        return Resources.westSouthWall;
                    }
                }
                else if (!tile.Exits.Contains("East"))
                {
                    return Resources.westEastWall;
                }
                else
                {
                    return Resources.westWall;
                }
            }
            else if (!tile.Exits.Contains("South"))
            {
                if (!tile.Exits.Contains("East"))
                {
                    return Resources.southEastWall;
                }
                else
                {
                    return Resources.southWall;
                }
            }
            else if (!tile.Exits.Contains("East"))
            {
                return Resources.eastWall;
            }
            else
            {
                return Resources.noWall;
            }
        }

        //UPDATE MINIMAP, USED EVER TIME PLAYER MOVES
        private void miniMapUpdate()
        {
            bool found = false;
            for(int i = 0; i < world.Count; i++)
            {
                if(world[i].X == hero.X + 1 && world[i].Y == hero.Y && world[i].Section == world[hero.Location].Section && world[i].Explored)
                {
                    p1n0.BackgroundImage = getMapImage(world[i]);
                    found = true;
                }
            }
            if (!found)
            {
                p1n0.BackgroundImage = Resources.noTile;
            }
            found = false;
            for (int i = 0; i < world.Count; i++)
            {
                if (world[i].X == hero.X + 2 && world[i].Y == hero.Y && world[i].Section == world[hero.Location].Section && world[i].Explored)
                {
                    p2n0.BackgroundImage = getMapImage(world[i]);
                    found = true;
                }
            }
            if (!found)
            {
                p2n0.BackgroundImage = Resources.noTile;
            }
            found = false;
            for (int i = 0; i < world.Count; i++)
            {
                if (world[i].X == hero.X - 1 && world[i].Y == hero.Y && world[i].Section == world[hero.Location].Section && world[i].Explored)
                {
                    n1n0.BackgroundImage = getMapImage(world[i]);
                    found = true;
                }
            }
            if (!found)
            {
                n1n0.BackgroundImage = Resources.noTile;
            }
            found = false;
            for (int i = 0; i < world.Count; i++)
            {
                if (world[i].X == hero.X - 2 && world[i].Y == hero.Y && world[i].Section == world[hero.Location].Section && world[i].Explored)
                {
                    n2n0.BackgroundImage = getMapImage(world[i]);
                    found = true;
                }
            }
            if (!found)
            {
                n2n0.BackgroundImage = Resources.noTile;
            }
            found = false;
            for (int i = 0; i < world.Count; i++)
            {
                if (world[i].X == hero.X - 2 && world[i].Y == hero.Y - 2 && world[i].Section == world[hero.Location].Section && world[i].Explored)
                {
                    n2n2.BackgroundImage = getMapImage(world[i]);
                    found = true;
                }
            }
            if (!found)
            {
                n2n2.BackgroundImage = Resources.noTile;
            }
            found = false;
            for (int i = 0; i < world.Count; i++)
            {
                if (world[i].X == hero.X - 1 && world[i].Y == hero.Y - 2 && world[i].Section == world[hero.Location].Section && world[i].Explored)
                {
                    n1n2.BackgroundImage = getMapImage(world[i]);
                    found = true;
                }
            }
            if (!found)
            {
                n1n2.BackgroundImage = Resources.noTile;
            }
            found = false;
            for (int i = 0; i < world.Count; i++)
            {
                if (world[i].X == hero.X && world[i].Y == hero.Y - 2 && world[i].Section == world[hero.Location].Section && world[i].Explored)
                {
                    n0n2.BackgroundImage = getMapImage(world[i]);
                    found = true;
                }
            }
            if (!found)
            {
                n0n2.BackgroundImage = Resources.noTile;
            }
            found = false;
            for (int i = 0; i < world.Count; i++)
            {
                if (world[i].X == hero.X + 1 && world[i].Y == hero.Y - 2 && world[i].Section == world[hero.Location].Section && world[i].Explored)
                {
                    p1n2.BackgroundImage = getMapImage(world[i]);
                    found = true;
                }
            }
            if (!found)
            {
                p1n2.BackgroundImage = Resources.noTile;
            }
            found = false;
            for (int i = 0; i < world.Count; i++)
            {
                if (world[i].X == hero.X + 2 && world[i].Y == hero.Y - 2 && world[i].Section == world[hero.Location].Section && world[i].Explored)
                {
                    p2n2.BackgroundImage = getMapImage(world[i]);
                    found = true;
                }
            }
            if (!found)
            {
                p2n2.BackgroundImage = Resources.noTile;
            }
            found = false;
            for (int i = 0; i < world.Count; i++)
            {
                if (world[i].X == hero.X - 2 && world[i].Y == hero.Y - 1 && world[i].Section == world[hero.Location].Section && world[i].Explored)
                {
                    n2n1.BackgroundImage = getMapImage(world[i]);
                    found = true;
                }
            }
            if (!found)
            {
                n2n1.BackgroundImage = Resources.noTile;
            }
            found = false;
            for (int i = 0; i < world.Count; i++)
            {
                if (world[i].X == hero.X - 1 && world[i].Y == hero.Y - 1 && world[i].Section == world[hero.Location].Section && world[i].Explored)
                {
                    n1n1.BackgroundImage = getMapImage(world[i]);
                    found = true;
                }
            }
            if (!found)
            {
                n1n1.BackgroundImage = Resources.noTile;
            }
            found = false;
            for (int i = 0; i < world.Count; i++)
            {
                if (world[i].X == hero.X && world[i].Y == hero.Y - 1 && world[i].Section == world[hero.Location].Section && world[i].Explored)
                {
                    n0n1.BackgroundImage = getMapImage(world[i]);
                    found = true;
                }
            }
            if (!found)
            {
                n0n1.BackgroundImage = Resources.noTile;
            }
            found = false;
            for (int i = 0; i < world.Count; i++)
            {
                if (world[i].X == hero.X + 1 && world[i].Y == hero.Y - 1 && world[i].Section == world[hero.Location].Section && world[i].Explored)
                {
                    p1n1.BackgroundImage = getMapImage(world[i]);
                    found = true;
                }
            }
            if (!found)
            {
                p1n1.BackgroundImage = Resources.noTile;
            }
            found = false;
            for (int i = 0; i < world.Count; i++)
            {
                if (world[i].X == hero.X + 2 && world[i].Y == hero.Y - 1 && world[i].Section == world[hero.Location].Section && world[i].Explored)
                {
                    p2n1.BackgroundImage = getMapImage(world[i]);
                    found = true;
                }
            }
            if (!found)
            {
                p2n1.BackgroundImage = Resources.noTile;
            }
            found = false;
            for (int i = 0; i < world.Count; i++)
            {
                if (world[i].X == hero.X - 2 && world[i].Y == hero.Y + 1 && world[i].Section == world[hero.Location].Section && world[i].Explored)
                {
                    n2p1.BackgroundImage = getMapImage(world[i]);
                    found = true;
                }
            }
            if (!found)
            {
                n2p1.BackgroundImage = Resources.noTile;
            }
            found = false;
            for (int i = 0; i < world.Count; i++)
            {
                if (world[i].X == hero.X - 1 && world[i].Y == hero.Y + 1 && world[i].Section == world[hero.Location].Section && world[i].Explored)
                {
                    n1p1.BackgroundImage = getMapImage(world[i]);
                    found = true;
                }
            }
            if (!found)
            {
                n1p1.BackgroundImage = Resources.noTile;
            }
            found = false;
            for (int i = 0; i < world.Count; i++)
            {
                if (world[i].X == hero.X && world[i].Y == hero.Y + 1 && world[i].Section == world[hero.Location].Section && world[i].Explored)
                {
                    n0p1.BackgroundImage = getMapImage(world[i]);
                    found = true;
                }
            }
            if (!found)
            {
                n0p1.BackgroundImage = Resources.noTile;
            }
            found = false;
            for (int i = 0; i < world.Count; i++)
            {
                if (world[i].X == hero.X + 1 && world[i].Y == hero.Y + 1 && world[i].Section == world[hero.Location].Section && world[i].Explored)
                {
                    p1p1.BackgroundImage = getMapImage(world[i]);
                    found = true;
                }
            }
            if (!found)
            {
                p1p1.BackgroundImage = Resources.noTile;
            }
            found = false;
            for (int i = 0; i < world.Count; i++)
            {
                if (world[i].X == hero.X + 2 && world[i].Y == hero.Y + 1 && world[i].Section == world[hero.Location].Section && world[i].Explored)
                {
                    p2p1.BackgroundImage = getMapImage(world[i]);
                    found = true;
                }
            }
            if (!found)
            {
                p2p1.BackgroundImage = Resources.noTile;
            }
            found = false;
            for (int i = 0; i < world.Count; i++)
            {
                if (world[i].X == hero.X - 2 && world[i].Y == hero.Y + 2 && world[i].Section == world[hero.Location].Section && world[i].Explored)
                {
                    n2p2.BackgroundImage = getMapImage(world[i]);
                    found = true;
                }
            }
            if (!found)
            {
                n2p2.BackgroundImage = Resources.noTile;
            }
            found = false;
            for (int i = 0; i < world.Count; i++)
            {
                if (world[i].X == hero.X - 1 && world[i].Y == hero.Y + 2 && world[i].Section == world[hero.Location].Section && world[i].Explored)
                {
                    n1p2.BackgroundImage = getMapImage(world[i]);
                    found = true;
                }
            }
            if (!found)
            {
                n1p2.BackgroundImage = Resources.noTile;
            }
            found = false;
            for (int i = 0; i < world.Count; i++)
            {
                if (world[i].X == hero.X && world[i].Y == hero.Y + 2 && world[i].Section == world[hero.Location].Section && world[i].Explored)
                {
                    n0p2.BackgroundImage = getMapImage(world[i]);
                    found = true;
                }
            }
            if (!found)
            {
                n0p2.BackgroundImage = Resources.noTile;
            }
            found = false;
            for (int i = 0; i < world.Count; i++)
            {
                if (world[i].X == hero.X + 1 && world[i].Y == hero.Y + 2 && world[i].Section == world[hero.Location].Section && world[i].Explored)
                {
                    p1p2.BackgroundImage = getMapImage(world[i]);
                    found = true;
                }
            }
            if (!found)
            {
                p1p2.BackgroundImage = Resources.noTile;
            }
            found = false;
            for (int i = 0; i < world.Count; i++)
            {
                if (world[i].X == hero.X + 2 && world[i].Y == hero.Y + 2 && world[i].Section == world[hero.Location].Section && world[i].Explored)
                {
                    p2p2.BackgroundImage = getMapImage(world[i]);
                    found = true;
                }
            }
            if (!found)
            {
                p2p2.BackgroundImage = Resources.noTile;
            }

            switch (hero.Facing)
            {
                case "North":
                    centerMM.Image = Resources.MapArrowNorth;
                    break;
                case "East":
                    centerMM.Image = Resources.MapArrowEast;
                    break;
                case "South":
                    centerMM.Image = Resources.MapArrowSouth;
                    break;
                case "West":
                    centerMM.Image = Resources.MapArrowWest;
                    break;
            }
        }

        private void AzQuest_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.A:
                    westBtn_Click_1(sender, e);
                    break;
                case Keys.S:
                    southBtn_Click(sender, e);
                    break;
                case Keys.D:
                    eastBtn_Click(sender, e);
                    break;
                case Keys.W:
                    northBtn_Click(sender, e);
                    break;
                case Keys.Q:
                    turnLeftBtn_Click_1(sender, e);
                    break;
                case Keys.E:
                    turnRightBtn_Click_1(sender, e);
                    break;
                case Keys.I:
                    invBtn_Click(sender, e);
                    break;
            }

        }
    }
}
