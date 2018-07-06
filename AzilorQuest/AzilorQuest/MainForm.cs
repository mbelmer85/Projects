using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using AzilorQuest.Properties;
using GameLogicLibrary;

namespace AzilorQuest
{
    public partial class Game : Form
    {
        public Game()
        {
            InitializeComponent();
        }

        //IN CLASS GLOBALS
        private List<MapTile> _world;
        private Hero _hero;
        private Random _rand;

        //LOAD EVENT
        private void Game_Load(object sender, EventArgs e)
        {

            _world = new List<MapTile>();
            _hero = new Hero();
            _rand = new Random();
            HPLabel.Text = "Hp:  " + _hero.Hp;
            expLabel.Text = "Exp:  " + _hero.Exp;


            //MAPDATA POPULATION
            GameLogicLibrary.MapTile.CreateWorld(_world);
            //NEW GAME INVENTORY POPULATION
            CreateItem("PotionSmall");
            CreateItem("PotionSmall");
            CreateItem("Bandage");
            CreateItem("Rusty Sword");
            _hero.Inventory[3].Equipped = true;
            CreateItem("Leather Armor");
            _hero.Inventory[4].Equipped = true;
            CreateItem("Leather Boots");
            _hero.Inventory[5].Equipped = true;
            CreateItem("Antidote");

            //UPDATE VIEWED MAPTILES
            uncoverTile(findTile(_hero.X, _hero.Y, _world[_hero.Location].Section));
            uncoverTile(FindFacingTile(_hero.Facing));

            //VIEW FIRST ROOM
            Look();

            //LOAD INTRO
            WelcomeForm welcome = new WelcomeForm();
            welcome.ShowDialog();
        }

        //CLICK EVENTS

        //DIRECTIONAL BUTTONS (North, South, East, West are actually Forward, Backward, Right, and Left)
        private void northBtn_Click(object sender, EventArgs e)
        {
            move(_hero.Facing);
        }

        private void westBtn_Click_1(object sender, EventArgs e)
        {
            switch (_hero.Facing)
            {
                case "North":
                    move("West");
                    break;
                case "South":
                    move("East");
                    break;
                case "East":
                    move("North");
                    break;
                case "West":
                    move("North");
                    break;
            }
        }

        private void southBtn_Click(object sender, EventArgs e)
        {
            switch (_hero.Facing)
            {
                case "North":
                    move("South");
                    break;
                case "South":
                    move("North");
                    break;
                case "East":
                    move("West");
                    break;
                case "West":
                    move("East");
                    break;
            }
        }

        private void eastBtn_Click(object sender, EventArgs e)
        {
            if (_hero.Facing == "North")
            {
                move("East");
            }
            else if (_hero.Facing == "South")
            {
                move("West");
            }
            else if (_hero.Facing == "East")
            {
                move("South");
            }
            else if (_hero.Facing == "West")
            {
                move("North");
            }
        }

        //TURNING
        private void turnLeftBtn_Click_1(object sender, EventArgs e)
        {
            switch (_hero.Facing)
            {
                case "North":
                    _hero.Facing = "West";
                    break;
                case "West":
                    _hero.Facing = "South";
                    break;
                case "South":
                    _hero.Facing = "East";
                    break;
                case "East":
                    _hero.Facing = "North";
                    break;
            }
            facingLabel.Text = "Facing:  " + _hero.Facing;
            Look();
        }

        private void turnRightBtn_Click_1(object sender, EventArgs e)
        {
            switch (_hero.Facing)
            {
                case "North":
                    _hero.Facing = "East";
                    break;
                case "East":
                    _hero.Facing = "South";
                    break;
                case "South":
                    _hero.Facing = "West";
                    break;
                case "West":
                    _hero.Facing = "North";
                    break;
            }
            facingLabel.Text = $"Facing:  {_hero.Facing}";
            Look();
        }

        //MENU BUTTONS
        private void statsBtn_Click(object sender, EventArgs e)
        {
            userDisplay.Clear();
            userDisplay.AppendText("STATUS:       ");
            userDisplay.AppendText(Environment.NewLine);
            userDisplay.AppendText(" Level:       " + _hero.Level);
            userDisplay.AppendText(Environment.NewLine);
            userDisplay.AppendText("  Max Hp:     " + _hero.MaxHp);
            userDisplay.AppendText(Environment.NewLine);
            userDisplay.AppendText("  Hp:         " + _hero.Hp);
            userDisplay.AppendText(Environment.NewLine);
            userDisplay.AppendText("  Str:        " + _hero.Str);
            userDisplay.AppendText(Environment.NewLine);
            userDisplay.AppendText("  Dex:        " + _hero.Dex);
            userDisplay.AppendText(Environment.NewLine);
            userDisplay.AppendText("  Con:        " + _hero.Con);
            userDisplay.AppendText(Environment.NewLine);
            userDisplay.AppendText("  Luk:        " + _hero.Luck);
            userDisplay.AppendText(Environment.NewLine);
            userDisplay.AppendText("  Exp:        " + _hero.Exp);
            userDisplay.AppendText(Environment.NewLine);
            userDisplay.AppendText("  Next Level: " + _hero.Tnl);
            userDisplay.AppendText(Environment.NewLine);
            userDisplay.AppendText("  Status:     " + _hero.StatusEff);
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
            userDisplay.AppendText("   WEAPON:  " + _hero.Equipment[0].Name);
            userDisplay.AppendText(Environment.NewLine);
            userDisplay.AppendText("   SHIELD:  " + _hero.Equipment[1].Name);
            userDisplay.AppendText(Environment.NewLine);
            userDisplay.AppendText("   HELM:    " + _hero.Equipment[2].Name);
            userDisplay.AppendText(Environment.NewLine);
            userDisplay.AppendText("   ARMOR:   " + _hero.Equipment[3].Name);
            userDisplay.AppendText(Environment.NewLine);
            userDisplay.AppendText("   BOOTS:   " + _hero.Equipment[4].Name);
            userDisplay.AppendText(Environment.NewLine);
        }

        private void restBtn_Click(object sender, EventArgs e)
        {
            if (_hero.StatusEff == "Healthy")
            {
                userDisplay.Clear();
                if (ChanceEncounter())
                {
                    userDisplay.AppendText("You're under attack!!");
                    userDisplay.AppendText(Environment.NewLine);
                }
                else
                {
                    _hero.Hp = _hero.MaxHp;
                    userDisplay.AppendText("You manage to get some rest.");
                    userDisplay.AppendText(Environment.NewLine);
                }
            }
            else if (_hero.StatusEff == "Dead")
            {
                userDisplay.AppendText("You are magically revived... somehow...");
                userDisplay.AppendText(Environment.NewLine);
                _hero.Hp = _hero.MaxHp;
                _hero.StatusEff = "Healthy";
            }
            else
            {
                userDisplay.AppendText("You are " + _hero.StatusEff + " and cannot rest.");
                userDisplay.AppendText(Environment.NewLine);
            }
            HPLabel.Text = "Hp:  " + _hero.Hp;
        }

        private void quitBtn_Click(object sender, EventArgs e)
        {
            QuitForm quit = new QuitForm(this);
            quit.ShowDialog();
        }

        private void invBtn_Click(object sender, EventArgs e)
        {
            InventoryForm inv = new InventoryForm(_hero, this);
            inv.ShowDialog();
        }

        private void mapBtn_Click_1(object sender, EventArgs e)
        {
            MapForm map = new MapForm(_world, _hero);
            map.ShowDialog();
        }

        //DEBUG INSTANT LEVEL UP BUTTON
        private void button1_Click(object sender, EventArgs e)
        {
            _hero.LevelUp();
        }

        //CUSTOM METHODS
        private void move(string direction)
        {
            switch (direction)
            {
                case "East":
                    for (int i = 0; i < _world.Count; i++)
                    {
                        if (_world[i].X == _hero.X + 1 && _world[i].Y == _hero.Y && _world[i].Section == _world[_hero.Location].Section && _world[i].Exits.Contains("West"))
                        {
                            _hero.X = _world[i].X;
                            _hero.Location = i;
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
                    for (int i = 0; i < _world.Count; i++)
                    {
                        if (_world[i].X == _hero.X && _world[i].Y == _hero.Y + 1 && _world[i].Section == _world[_hero.Location].Section && _world[i].Exits.Contains("North"))
                        {
                            _hero.Y = _world[i].Y;
                            _hero.Location = i;
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
                    for (int i = 0; i < _world.Count; i++)
                    {
                        if (_world[i].X == _hero.X - 1 && _world[i].Y == _hero.Y && _world[i].Section == _world[_hero.Location].Section && _world[i].Exits.Contains("East"))
                        {
                            _hero.X = _world[i].X;
                            _hero.Location = i;
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
                    for (int i = 0; i < _world.Count; i++)
                    {
                        if (_world[i].X == _hero.X && _world[i].Y == _hero.Y - 1 && _world[i].Section == _world[_hero.Location].Section && _world[i].Exits.Contains("South"))
                        {
                            _hero.Y = _world[i].Y;
                            _hero.Location = i;
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
            for (int i = 0; i < _world.Count; i++)
            {
                if (_world[i].X == _hero.X && _world[i].Y == _hero.Y && _world[i].Section == _world[_hero.Location].Section)
                {
                    userDisplay.AppendText("Current X,Y: " + _hero.X + "," + _hero.Y);
                    userDisplay.AppendText(Environment.NewLine);
                    //outputDisplay.Items.Add("Event: " + _world[i].EventId);
                    //outputDisplay.Items.Add("Dungeon Section: " + _world[i].Section);
                    //outputDisplay.Items.Add("Current Exits: " + _world[i].Exits);
                    userDisplay.AppendText(_world[i].Description);
                    userDisplay.AppendText(Environment.NewLine);
                }
            }
            HPLabel.Text = "Hp:  " + _hero.Hp;
            expLabel.Text = "Exp:  " + _hero.Exp;
            uncoverTile(findTile(_hero.X, _hero.Y, _world[_hero.Location].Section));
            uncoverTile(FindFacingTile("East"));
            uncoverTile(FindFacingTile("North"));
            uncoverTile(FindFacingTile("South"));
            uncoverTile(FindFacingTile("West"));
            miniMapUpdate();
        }

        //CHECK FOR A STATUS EFFECT AND APPLY EFFECT
        private void applyStatusDamage()
        {
            if (_hero.StatusEff == "Poisoned")
            {
                _hero.Hp -= _hero.MaxHp / 20;
                userDisplay.AppendText("You take " + (_hero.MaxHp / 20).ToString() + " points of damage from poison.");
                userDisplay.AppendText(Environment.NewLine);
                if (_hero.Hp < 1)
                {
                    userDisplay.AppendText("You have succumed to the poison.");
                    userDisplay.AppendText(Environment.NewLine);
                    _hero.StatusEff = "Dead";
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
            for (int i = 0; i < _world.Count; i++)
            {
                if (_world[i].X == x && _world[i].Y == y && _world[i].Section == section)
                {
                    return _world[i];
                }
            }
            return _world[_hero.Location];
        }

        //RETURN TILE IN FRONT OF PLAYER
        private MapTile FindFacingTile(string direction)
        {
            switch (direction)
            {
                case "North":
                    for (int i = 0; i < _world.Count; i++)
                    {
                        if (_world[i].X == _hero.X && _world[i].Y == _hero.Y - 1 && _world[i].Section == _world[_hero.Location].Section && _world[_hero.Location].Exits.Contains("North"))
                        {
                            return _world[i];
                        }
                    }
                    return _world[_hero.Location];
                case "East":
                    for (int i = 0; i < _world.Count; i++)
                    {
                        if (_world[i].X == _hero.X + 1 && _world[i].Y == _hero.Y && _world[i].Section == _world[_hero.Location].Section && _world[_hero.Location].Exits.Contains("East"))
                        {
                            return _world[i];
                        }
                    }
                    return _world[_hero.Location];
                case "South":
                    for (int i = 0; i < _world.Count; i++)
                    {
                        if (_world[i].X == _hero.X && _world[i].Y == _hero.Y + 1 && _world[i].Section == _world[_hero.Location].Section && _world[_hero.Location].Exits.Contains("South"))
                        {
                            return _world[i];
                        }
                    }
                    return _world[_hero.Location];
                case "West":
                    for (int i = 0; i < _world.Count; i++)
                    {
                        if (_world[i].X == _hero.X - 1 && _world[i].Y == _hero.Y && _world[i].Section == _world[_hero.Location].Section && _world[_hero.Location].Exits.Contains("West"))
                        {
                            return _world[i];
                        }
                    }
                    return _world[_hero.Location];
                default:
                    return _world[_hero.Location];
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
            int ranGen = _rand.Next(99) + 1;
            //20% Chance for battle
            if (ranGen <= 20 && _world[_hero.Location].EventId <= 0)
            {
                ranGen = _rand.Next(99) + 1;
                switch (_world[_hero.Location].Section)
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
                BattleForm battle = new BattleForm(mobChoice, _hero, _world, this);
                battle.ShowDialog();
                return true;
            }
            return false;
        }


        //ALL EVENTS
        async private void eventCheck()
        {
            switch (_world[_hero.Location].EventId)
            {
                case 9:
                    //Fight Boss Floor 1
                    BattleForm battle = new BattleForm("Giant Toad", _hero, _world, this);
                    battle.ShowDialog();
                    break;
                case 26:
                    //Fight Boss Floor 2
                    BattleForm battle1 = new BattleForm("Harpy", _hero, _world, this);
                    battle1.ShowDialog();
                    break;
                case 5:
                    //Leather Buckler Chest
                    userDisplay.AppendText("You find a chest with a Leather Buckler inside!");
                    userDisplay.AppendText(Environment.NewLine);
                    userDisplay.AppendText("You obtain the Leather Buckler.");
                    userDisplay.AppendText(Environment.NewLine);
                    CreateItem("Leather Buckler");
                    _world[_hero.Location].EventId = -1;
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
                    CreateItem("PotionSmall");
                    CreateItem("Antidote");
                    _world[_hero.Location].EventId = -1;
                    break;
                case 7:
                    //Potion Chest Small
                    userDisplay.AppendText("You find a chest with a small potion inside!");
                    userDisplay.AppendText(Environment.NewLine);
                    userDisplay.AppendText("You obtain the small potion.");
                    userDisplay.AppendText(Environment.NewLine);
                    CreateItem("PotionSmall");
                    _world[_hero.Location].EventId = -1;
                    break;
                case 8:
                    //Potion and Antidote Chest
                    userDisplay.AppendText("You find a chest with a small potion and antidote herb inside!");
                    userDisplay.AppendText(Environment.NewLine);
                    userDisplay.AppendText("You obtain the small potion.");
                    userDisplay.AppendText(Environment.NewLine);
                    userDisplay.AppendText("You obtain the antidote.");
                    userDisplay.AppendText(Environment.NewLine);
                    CreateItem("PotionSmall");
                    CreateItem("Antidote");
                    _world[_hero.Location].EventId = -1;
                    break;
                case 20:
                    //Potion Chest Medium
                    userDisplay.AppendText("You find a chest with a medium potion inside!");
                    userDisplay.AppendText(Environment.NewLine);
                    userDisplay.AppendText("You obtain the medium potion.");
                    userDisplay.AppendText(Environment.NewLine);
                    CreateItem("PotionMedium");
                    _world[_hero.Location].EventId = -1;
                    break;
                case 21:
                    //Chainmail in Sarcophagas
                    userDisplay.AppendText("You find a sarcophagas with chainmail armor on the corpse inside.");
                    userDisplay.AppendText(Environment.NewLine);
                    userDisplay.AppendText("You obtain the Chainmail.");
                    userDisplay.AppendText(Environment.NewLine);
                    CreateItem("Chainmail");
                    _world[_hero.Location].EventId = -1;
                    break;
                case 22:
                    //Chain Coif in chest
                    userDisplay.AppendText("You find a chest with a chain coif inside!");
                    userDisplay.AppendText(Environment.NewLine);
                    userDisplay.AppendText("You obtain the Chain Coif.");
                    userDisplay.AppendText(Environment.NewLine);
                    CreateItem("Chain Coif");
                    _world[_hero.Location].EventId = -1;
                    break;
                case 23:
                    //Solletrets in chest
                    userDisplay.AppendText("You find a chest with sollerets inside!");
                    userDisplay.AppendText(Environment.NewLine);
                    userDisplay.AppendText("You obtain the Sollerets.");
                    userDisplay.AppendText(Environment.NewLine);
                    CreateItem("Sollerets");
                    _world[_hero.Location].EventId = -1;
                    break;
                case 24:
                    //Targe on fresh corpse
                    userDisplay.AppendText("There is a fresh corpse here with a targe on it's arm.");
                    userDisplay.AppendText(Environment.NewLine);
                    userDisplay.AppendText("You remove the Targe.");
                    userDisplay.AppendText(Environment.NewLine);
                    CreateItem("Targe");
                    _world[_hero.Location].EventId = -1;
                    break;
                case 25:
                    //Dirk on Pedestal
                    userDisplay.AppendText("Sitting on a marble pedestal you find a deadly looking Dirk.");
                    userDisplay.AppendText(Environment.NewLine);
                    userDisplay.AppendText("You obtain the Dirk.");
                    userDisplay.AppendText(Environment.NewLine);
                    CreateItem("Dirk");
                    _world[_hero.Location].EventId = -1;
                    break;
                case 11:
                    //Poison Gas Room
                    userDisplay.AppendText("This room seems to be filled with noxious gas!!");
                    userDisplay.AppendText(Environment.NewLine);
                    userDisplay.AppendText("You take damage!");
                    userDisplay.AppendText(Environment.NewLine);
                    _hero.Hp -= 5;
                    ChanceEncounter();
                    break;
                case 10:
                    //Stairs to 2nd Floor
                    userDisplay.AppendText("You walk down the cold stone steps...");
                    userDisplay.AppendText(Environment.NewLine);
                    await Task.Delay(1000);
                    for(int i = 0; i < _world.Count; i++)
                    {
                        if(_world[i].X == 9 && _world[i].Y == 19 && _world[i].Section == 2)
                        {
                            _hero.Location = i;
                            _hero.X = _world[i].X;
                            _hero.Y = _world[i].Y;
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
                    for (int i = 0; i < _world.Count; i++)
                    {
                        if (_world[i].X == 9 && _world[i].Y == 19 && _world[i].Section == 2)
                        {
                            _hero.Location = i;
                            _hero.X = _world[i].X;
                            _hero.Y = _world[i].Y;
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

        public void CreateItem(string item)
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

        //UPDATE UI TEXT
        public void updateHP()
        {
            HPLabel.Text = "Hp:  " + _hero.Hp;
        }

        public void updateEXP()
        {
            expLabel.Text = "Exp:  " + _hero.Exp;
        }

        //CHECK IF TILE EXISTS
        private bool tileExists(int x, int y, int section)
        {
            for(int i = 0; i < _world.Count; i++)
            {
                if(_world[i].X == x && _world[i].Y == y && _world[i].Section == section)
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

        //UPDATE MINIMAP, USED EVERY TIME PLAYER MOVES
        private void miniMapUpdate()
        {
            bool found = false;
            for(int i = 0; i < _world.Count; i++)
            {
                if(_world[i].X == _hero.X + 1 && _world[i].Y == _hero.Y && _world[i].Section == _world[_hero.Location].Section && _world[i].Explored)
                {
                    p1n0.BackgroundImage = getMapImage(_world[i]);
                    found = true;
                }
            }
            if (!found)
            {
                p1n0.BackgroundImage = Resources.noTile;
            }
            found = false;
            for (int i = 0; i < _world.Count; i++)
            {
                if (_world[i].X == _hero.X + 2 && _world[i].Y == _hero.Y && _world[i].Section == _world[_hero.Location].Section && _world[i].Explored)
                {
                    p2n0.BackgroundImage = getMapImage(_world[i]);
                    found = true;
                }
            }
            if (!found)
            {
                p2n0.BackgroundImage = Resources.noTile;
            }
            found = false;
            for (int i = 0; i < _world.Count; i++)
            {
                if (_world[i].X == _hero.X - 1 && _world[i].Y == _hero.Y && _world[i].Section == _world[_hero.Location].Section && _world[i].Explored)
                {
                    n1n0.BackgroundImage = getMapImage(_world[i]);
                    found = true;
                }
            }
            if (!found)
            {
                n1n0.BackgroundImage = Resources.noTile;
            }
            found = false;
            for (int i = 0; i < _world.Count; i++)
            {
                if (_world[i].X == _hero.X - 2 && _world[i].Y == _hero.Y && _world[i].Section == _world[_hero.Location].Section && _world[i].Explored)
                {
                    n2n0.BackgroundImage = getMapImage(_world[i]);
                    found = true;
                }
            }
            if (!found)
            {
                n2n0.BackgroundImage = Resources.noTile;
            }
            found = false;
            for (int i = 0; i < _world.Count; i++)
            {
                if (_world[i].X == _hero.X - 2 && _world[i].Y == _hero.Y - 2 && _world[i].Section == _world[_hero.Location].Section && _world[i].Explored)
                {
                    n2n2.BackgroundImage = getMapImage(_world[i]);
                    found = true;
                }
            }
            if (!found)
            {
                n2n2.BackgroundImage = Resources.noTile;
            }
            found = false;
            for (int i = 0; i < _world.Count; i++)
            {
                if (_world[i].X == _hero.X - 1 && _world[i].Y == _hero.Y - 2 && _world[i].Section == _world[_hero.Location].Section && _world[i].Explored)
                {
                    n1n2.BackgroundImage = getMapImage(_world[i]);
                    found = true;
                }
            }
            if (!found)
            {
                n1n2.BackgroundImage = Resources.noTile;
            }
            found = false;
            for (int i = 0; i < _world.Count; i++)
            {
                if (_world[i].X == _hero.X && _world[i].Y == _hero.Y - 2 && _world[i].Section == _world[_hero.Location].Section && _world[i].Explored)
                {
                    n0n2.BackgroundImage = getMapImage(_world[i]);
                    found = true;
                }
            }
            if (!found)
            {
                n0n2.BackgroundImage = Resources.noTile;
            }
            found = false;
            for (int i = 0; i < _world.Count; i++)
            {
                if (_world[i].X == _hero.X + 1 && _world[i].Y == _hero.Y - 2 && _world[i].Section == _world[_hero.Location].Section && _world[i].Explored)
                {
                    p1n2.BackgroundImage = getMapImage(_world[i]);
                    found = true;
                }
            }
            if (!found)
            {
                p1n2.BackgroundImage = Resources.noTile;
            }
            found = false;
            for (int i = 0; i < _world.Count; i++)
            {
                if (_world[i].X == _hero.X + 2 && _world[i].Y == _hero.Y - 2 && _world[i].Section == _world[_hero.Location].Section && _world[i].Explored)
                {
                    p2n2.BackgroundImage = getMapImage(_world[i]);
                    found = true;
                }
            }
            if (!found)
            {
                p2n2.BackgroundImage = Resources.noTile;
            }
            found = false;
            for (int i = 0; i < _world.Count; i++)
            {
                if (_world[i].X == _hero.X - 2 && _world[i].Y == _hero.Y - 1 && _world[i].Section == _world[_hero.Location].Section && _world[i].Explored)
                {
                    n2n1.BackgroundImage = getMapImage(_world[i]);
                    found = true;
                }
            }
            if (!found)
            {
                n2n1.BackgroundImage = Resources.noTile;
            }
            found = false;
            for (int i = 0; i < _world.Count; i++)
            {
                if (_world[i].X == _hero.X - 1 && _world[i].Y == _hero.Y - 1 && _world[i].Section == _world[_hero.Location].Section && _world[i].Explored)
                {
                    n1n1.BackgroundImage = getMapImage(_world[i]);
                    found = true;
                }
            }
            if (!found)
            {
                n1n1.BackgroundImage = Resources.noTile;
            }
            found = false;
            for (int i = 0; i < _world.Count; i++)
            {
                if (_world[i].X == _hero.X && _world[i].Y == _hero.Y - 1 && _world[i].Section == _world[_hero.Location].Section && _world[i].Explored)
                {
                    n0n1.BackgroundImage = getMapImage(_world[i]);
                    found = true;
                }
            }
            if (!found)
            {
                n0n1.BackgroundImage = Resources.noTile;
            }
            found = false;
            for (int i = 0; i < _world.Count; i++)
            {
                if (_world[i].X == _hero.X + 1 && _world[i].Y == _hero.Y - 1 && _world[i].Section == _world[_hero.Location].Section && _world[i].Explored)
                {
                    p1n1.BackgroundImage = getMapImage(_world[i]);
                    found = true;
                }
            }
            if (!found)
            {
                p1n1.BackgroundImage = Resources.noTile;
            }
            found = false;
            for (int i = 0; i < _world.Count; i++)
            {
                if (_world[i].X == _hero.X + 2 && _world[i].Y == _hero.Y - 1 && _world[i].Section == _world[_hero.Location].Section && _world[i].Explored)
                {
                    p2n1.BackgroundImage = getMapImage(_world[i]);
                    found = true;
                }
            }
            if (!found)
            {
                p2n1.BackgroundImage = Resources.noTile;
            }
            found = false;
            for (int i = 0; i < _world.Count; i++)
            {
                if (_world[i].X == _hero.X - 2 && _world[i].Y == _hero.Y + 1 && _world[i].Section == _world[_hero.Location].Section && _world[i].Explored)
                {
                    n2p1.BackgroundImage = getMapImage(_world[i]);
                    found = true;
                }
            }
            if (!found)
            {
                n2p1.BackgroundImage = Resources.noTile;
            }
            found = false;
            for (int i = 0; i < _world.Count; i++)
            {
                if (_world[i].X == _hero.X - 1 && _world[i].Y == _hero.Y + 1 && _world[i].Section == _world[_hero.Location].Section && _world[i].Explored)
                {
                    n1p1.BackgroundImage = getMapImage(_world[i]);
                    found = true;
                }
            }
            if (!found)
            {
                n1p1.BackgroundImage = Resources.noTile;
            }
            found = false;
            for (int i = 0; i < _world.Count; i++)
            {
                if (_world[i].X == _hero.X && _world[i].Y == _hero.Y + 1 && _world[i].Section == _world[_hero.Location].Section && _world[i].Explored)
                {
                    n0p1.BackgroundImage = getMapImage(_world[i]);
                    found = true;
                }
            }
            if (!found)
            {
                n0p1.BackgroundImage = Resources.noTile;
            }
            found = false;
            for (int i = 0; i < _world.Count; i++)
            {
                if (_world[i].X == _hero.X + 1 && _world[i].Y == _hero.Y + 1 && _world[i].Section == _world[_hero.Location].Section && _world[i].Explored)
                {
                    p1p1.BackgroundImage = getMapImage(_world[i]);
                    found = true;
                }
            }
            if (!found)
            {
                p1p1.BackgroundImage = Resources.noTile;
            }
            found = false;
            for (int i = 0; i < _world.Count; i++)
            {
                if (_world[i].X == _hero.X + 2 && _world[i].Y == _hero.Y + 1 && _world[i].Section == _world[_hero.Location].Section && _world[i].Explored)
                {
                    p2p1.BackgroundImage = getMapImage(_world[i]);
                    found = true;
                }
            }
            if (!found)
            {
                p2p1.BackgroundImage = Resources.noTile;
            }
            found = false;
            for (int i = 0; i < _world.Count; i++)
            {
                if (_world[i].X == _hero.X - 2 && _world[i].Y == _hero.Y + 2 && _world[i].Section == _world[_hero.Location].Section && _world[i].Explored)
                {
                    n2p2.BackgroundImage = getMapImage(_world[i]);
                    found = true;
                }
            }
            if (!found)
            {
                n2p2.BackgroundImage = Resources.noTile;
            }
            found = false;
            for (int i = 0; i < _world.Count; i++)
            {
                if (_world[i].X == _hero.X - 1 && _world[i].Y == _hero.Y + 2 && _world[i].Section == _world[_hero.Location].Section && _world[i].Explored)
                {
                    n1p2.BackgroundImage = getMapImage(_world[i]);
                    found = true;
                }
            }
            if (!found)
            {
                n1p2.BackgroundImage = Resources.noTile;
            }
            found = false;
            for (int i = 0; i < _world.Count; i++)
            {
                if (_world[i].X == _hero.X && _world[i].Y == _hero.Y + 2 && _world[i].Section == _world[_hero.Location].Section && _world[i].Explored)
                {
                    n0p2.BackgroundImage = getMapImage(_world[i]);
                    found = true;
                }
            }
            if (!found)
            {
                n0p2.BackgroundImage = Resources.noTile;
            }
            found = false;
            for (int i = 0; i < _world.Count; i++)
            {
                if (_world[i].X == _hero.X + 1 && _world[i].Y == _hero.Y + 2 && _world[i].Section == _world[_hero.Location].Section && _world[i].Explored)
                {
                    p1p2.BackgroundImage = getMapImage(_world[i]);
                    found = true;
                }
            }
            if (!found)
            {
                p1p2.BackgroundImage = Resources.noTile;
            }
            found = false;
            for (int i = 0; i < _world.Count; i++)
            {
                if (_world[i].X == _hero.X + 2 && _world[i].Y == _hero.Y + 2 && _world[i].Section == _world[_hero.Location].Section && _world[i].Explored)
                {
                    p2p2.BackgroundImage = getMapImage(_world[i]);
                    found = true;
                }
            }
            if (!found)
            {
                p2p2.BackgroundImage = Resources.noTile;
            }

            switch (_hero.Facing)
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
