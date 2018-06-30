using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using AzilorQuest.Properties;
using GameLogicLibrary;

namespace AzilorQuest
{
    public partial class BattleForm : Form
    {
        //GLOBALS
        private string monster;
        private Monster creature;
        private Hero hero;
        private Random rand;
        private List<MapTile> area;
        private Game form1;
        private bool activeAction;
        private static SemaphoreSlim semaphoreSlim;
        private static SemaphoreSlim eventSemaphore;

        //ACCESSOR FOR MONSTER
        public string Monster
        {
            get { return monster; }
            set { monster = value; }
        }

        //BATTLEFORM CONSTRUCTOR
        public BattleForm(string mob, Hero character, List<MapTile> location, Game form)
        {
            InitializeComponent();
            Monster = mob;
            creature = new Monster(mob);
            hero = character;
            rand = new Random();
            area = location;
            form1 = form;
        }

        //BATTLEFORM LOAD EVENT
        async private void BattleForm_Load(object sender, EventArgs e)
        {
            //CREATE SEMAPHORE
            semaphoreSlim = new SemaphoreSlim(1, 1);
            eventSemaphore = new SemaphoreSlim(1, 1);

            //SET INACTIVE BUTTON
            activeAction = false;

            //SET CURRENT Hp & MP
            currentHP.Text = "Hp:  " + hero.Hp;
            currentExp.Text = "Exp: " + hero.Exp;

            //SET MONSTER IMAGE
            switch (monster)
            {
                case "Slime":
                    Image img = Resources.Slime;
                    mobPicture.Image = img;
                    break;
                case "Wolf":
                    img = Resources.Wolf;
                    mobPicture.Image = img;
                    break;
                case "Spider":
                    img = Resources.Spider;
                    mobPicture.Image = img;
                    break;
                case "Giant Toad":
                    img = Resources.GiantToad;
                    mobPicture.Image = img;
                    break;
                case "Snake":
                    img = Resources.Snake;
                    mobPicture.Image = img;
                    break;
                case "Skeleton":
                    img = Resources.Skeleton;
                    mobPicture.Image = img;
                    break;
                case "Rat":
                    img = Resources.Rat;
                    mobPicture.Image = img;
                    break;
                case "Harpy":
                    img = Resources.Harpy;
                    mobPicture.Image = img;
                    break;
            }

            //await Task.Delay(1000);

            //CHECK IF CREATURE IS FASTER, IF SO IT STRIKES FIRST
            activeAction = true;
            if (creature.Dex > hero.Dex)
            {
                delayPrintLine(userDisplay, "You have encountered a " + monster + "!!");
                delayPrintLine(userDisplay, "The " + monster + " strikes first!");
                //userDisplay.AppendText(Environment.NewLine);
                mobAttack(1);
                form1.updateHP();
            }
            else
            {
                delayPrintLine(userDisplay, "You have encountered a " + monster + "!!", true);
            }

            //eventTimer.Start();
        }

        //CLICK BUTTON EVENTS
        private void endBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void attackBtn_Click(object sender, EventArgs e)
        {
            if (!activeAction)
            {
                activeAction = true;
                heroAttack();
                mobAttack(1);
                currentHP.Text = "Hp:  " + hero.Hp;
                //eventTimer.Start();
            }
            else
            {
                return;
            }
        }

        private void defendBtn_Click(object sender, EventArgs e)
        {
            activeAction = true;
            heroDefend();
            currentHP.Text = "Hp:  " + hero.Hp;
            eventTimer.Start();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            InventoryForm inv = new InventoryForm(hero, form1, this);
            inv.ShowDialog();
        }

        async private void fleeBtn_Click(object sender, EventArgs e)
        {
            userDisplay.Clear();
            //activeAction = true;
            //eventTimer.Start();
            int ranGen = rand.Next(99) + 1;
            if (ranGen <= 20)
            {
                delayPrintLine(userDisplay, "You fail to get away...");
                //userDisplay.AppendText(Environment.NewLine);
                await Task.Delay(1000);
                mobAttack(1);
            }
            else
            {
                delayPrintLine(userDisplay, "You manage to flee!");
                //userDisplay.AppendText(Environment.NewLine);
                await Task.Delay(2000);
                this.Close();
            }
        }

        //CUSTOM METHODS
        //BATTLE MECHANICS
        async private void heroAttack()
        {
            int ranGen = rand.Next(99) + 1;
            if (creature.Alive && hero.Hp >= 1)
            {
                await eventSemaphore.WaitAsync();
                activeAction = true;
                try
                {
                    userDisplay.Clear();
                    if (ranGen < creature.Dex)
                    {
                        delayPrintLine(userDisplay, "You Miss!!!");
                        //userDisplay.AppendText(Environment.NewLine);
                    }
                    else
                    {
                        int damage = hero.Str + hero.Equipment[0].Power + ranGen / 20 - creature.Con;
                        if (damage > 0)
                        {
                            creature.Hp = creature.Hp - damage;
                            delayPrintLine(userDisplay, "You hit the " + monster + " for " + damage + " damage!");
                            //userDisplay.AppendText(Environment.NewLine);
                        }
                        else
                        {
                            delayPrintLine(userDisplay, "Your attack doesn't phase the " + monster);
                            //userDisplay.AppendText(Environment.NewLine);
                        }
                        await Task.Delay(250);
                        if (creature.Hp < 1)
                        {
                            creature.Alive = false;
                            delayPrintLine(userDisplay, "The " + monster + " has been slain.");
                            //userDisplay.AppendText(Environment.NewLine);
                            await Task.Delay(250);
                            delayPrintLine(userDisplay, "You have gained " + creature.Exp + "EXP.");
                            //userDisplay.AppendText(Environment.NewLine);
                            hero.Exp += creature.Exp;
                            hero.Tnl -= creature.Exp;
                            area[hero.Location].EventId = -1;
                            currentExp.Text = "Exp: " + hero.Exp;

                            //20% chance to get drop
                            ranGen = rand.Next(99) + 1;
                            if (ranGen <= 12)
                            {
                                //Give Item based on mob
                                if (monster == "Spider" || monster == "Snake")
                                {
                                    createItem("Antidote");
                                    delayPrintLine(userDisplay, "The " + monster + " dropped an antidote herb.");
                                    //userDisplay.AppendText(Environment.NewLine);
                                }
                                else if (monster == "Wolf")
                                {
                                    createItem("PotionMedium");
                                    delayPrintLine(userDisplay, "The " + monster + " dropped a small potion.");
                                    //userDisplay.AppendText(Environment.NewLine);
                                }
                                else if (monster == "Rat")
                                {
                                    createItem("PotionSmall");
                                    delayPrintLine(userDisplay, "The " + monster + " dropped a small potion.");
                                    //userDisplay.AppendText(Environment.NewLine);
                                }
                                //else if (creature.Mob == "Slime")
                                //{
                                //    battleDisplay.Items.Add("YOU GET NOTHING");
                                //}
                            }
                            if (hero.Tnl <= 0)
                            {
                                delayPrintLine(userDisplay, "You've gained a level!!");
                                //userDisplay.AppendText(Environment.NewLine);
                                await Task.Delay(250);
                                hero.LevelUp();
                                delayPrintLine(userDisplay, " Level:       " + hero.Level);
                                //userDisplay.AppendText(Environment.NewLine);
                                await Task.Delay(250);
                                delayPrintLine(userDisplay, "  Max Hp:     " + hero.MaxHp);
                                //userDisplay.AppendText(Environment.NewLine);
                                await Task.Delay(250);
                                delayPrintLine(userDisplay, "  Str:        " + hero.Str);
                                //userDisplay.AppendText(Environment.NewLine);
                                await Task.Delay(250);
                                delayPrintLine(userDisplay, "  Dex:        " + hero.Dex);
                                //userDisplay.AppendText(Environment.NewLine);
                                await Task.Delay(250);
                                delayPrintLine(userDisplay, "  Con:        " + hero.Con);
                                //userDisplay.AppendText(Environment.NewLine);
                                await Task.Delay(250);
                                delayPrintLine(userDisplay, "  Luk:        " + hero.Luck);
                                //userDisplay.AppendText(Environment.NewLine);
                                await Task.Delay(250);
                                delayPrintLine(userDisplay, "  Exp:        " + hero.Exp);
                                //userDisplay.AppendText(Environment.NewLine);
                                await Task.Delay(250);
                                delayPrintLine(userDisplay, "  Next Level: " + hero.Tnl);
                                //userDisplay.AppendText(Environment.NewLine);
                            }
                            endBtn.Visible = true;
                            mobPicture.Visible = false;
                            if (monster == "Giant Toad")
                            {
                                MessageBox.Show("You hear a faint 'click'");
                                area[34].Exits = "North";
                            }
                        }
                    }
                    await Task.Delay(250);
                    applyStatusDamage();
                }
                finally
                {
                    eventSemaphore.Release();
                    activeAction = false;
                }
            }
            currentHP.Text = "Hp:  " + hero.Hp;
            currentExp.Text = "Exp:  " + hero.Exp;
            form1.updateHP();
            form1.updateEXP();
        }

        private void heroDefend()
        {
            mobAttack(2);
        }

        async public void mobAttack(int defend)
        {
            int ranGen;
            await eventSemaphore.WaitAsync();
            try
            {
                activeAction = true;
                if (creature.Alive)
                {
                    if (monster == "Giant Toad")
                    {
                        ranGen = rand.Next(99) + 1;
                        if (ranGen < hero.Dex)
                        {
                            delayPrintLine(userDisplay, "The " + monster + " misses you!");
                            //userDisplay.AppendText(Environment.NewLine);
                        }
                        else
                        {
                            int damage = creature.Str + ranGen / 20 - ((hero.Con + hero.Equipment[1].Armor + hero.Equipment[2].Armor + hero.Equipment[3].Armor + hero.Equipment[4].Armor) * defend);
                            if (damage > 0)
                            {
                                ranGen = rand.Next(99) + 1;
                                if (ranGen <= 70)
                                {
                                    hero.Hp = hero.Hp - damage;
                                    delayPrintLine(userDisplay, "The Giant Toad lashes at you with it's tongue for " + damage + "!");
                                    //userDisplay.AppendText(Environment.NewLine);
                                    checkStatusEff();
                                }
                                else if (ranGen <= 100)
                                {
                                    hero.Hp = hero.Hp - (damage * 2);
                                    delayPrintLine(userDisplay, "The Giant Toad leaps high into the air and crush you from above for " + (damage * 2) + " damage!");
                                    //userDisplay.AppendText(Environment.NewLine);
                                    checkStatusEff();
                                }
                                if (hero.Hp < 1)
                                {
                                    slain();
                                }
                            }
                            else
                            {
                                delayPrintLine(userDisplay, "The " + monster + "'s attack doesn't phase you.");
                                //userDisplay.AppendText(Environment.NewLine);
                            }
                        }
                    }
                    else if (monster == "Harpy")
                    {
                        ranGen = rand.Next(99) + 1;
                        if (ranGen < hero.Dex)
                        {
                            delayPrintLine(userDisplay, "The " + monster + " misses you!");
                            //userDisplay.AppendText(Environment.NewLine);
                        }
                        else
                        {
                            int damage = creature.Str + ranGen / 20 - hero.Con - (hero.Equipment[1].Armor + hero.Equipment[2].Armor + hero.Equipment[3].Armor + hero.Equipment[4].Armor);
                            if (damage > 0)
                            {
                                ranGen = rand.Next(99) + 1;
                                if (ranGen <= 70)
                                {
                                    hero.Hp = hero.Hp - damage;
                                    delayPrintLine(userDisplay, "The Harpy swipes at you with razor talons for " + damage + "!");
                                    //userDisplay.AppendText(Environment.NewLine);
                                    checkStatusEff();
                                }
                                else if (ranGen <= 100)
                                {
                                    hero.Hp = hero.Hp - (damage * 2);
                                    delayPrintLine(userDisplay, "The Harpy does a dive bomb at your face for " + (damage * 2) + " damage!");
                                    //userDisplay.AppendText(Environment.NewLine);
                                    checkStatusEff();
                                }
                                if (hero.Hp < 1)
                                {
                                    slain();
                                }
                            }
                            else
                            {
                                delayPrintLine(userDisplay, "The " + monster + "'s attack doesn't phase you.");
                                //userDisplay.AppendText(Environment.NewLine);
                            }
                        }
                    }
                    else
                    {
                        ranGen = rand.Next(99) + 1;
                        if (ranGen < hero.Dex + 10)
                        {
                            delayPrintLine(userDisplay, "The " + monster + " misses you!");
                            //userDisplay.AppendText(Environment.NewLine);
                        }
                        else
                        {
                            int damage = creature.Str + ranGen / 25 - hero.Con - (hero.Equipment[1].Armor + hero.Equipment[2].Armor + hero.Equipment[3].Armor + hero.Equipment[4].Armor);
                            if (damage > 0)
                            {
                                hero.Hp = hero.Hp - damage;
                                delayPrintLine(userDisplay, "You are hit by a " + monster + " for " + damage + " points of damage");
                                //userDisplay.AppendText(Environment.NewLine);
                                checkStatusEff();
                                if (hero.Hp < 1)
                                {
                                    slain();
                                }
                            }
                            else
                            {
                                delayPrintLine(userDisplay, "The " + monster + "'s attack doesn't phase you.");
                                //userDisplay.AppendText(Environment.NewLine);
                            }
                        }
                    }
                    await Task.Delay(250);
                }
            }
            finally
            {
                eventSemaphore.Release();
                activeAction = false;
            }
            currentHP.Text = "Hp:  " + hero.Hp;
            currentExp.Text = "Exp:  " + hero.Exp;
            form1.updateHP();
            form1.updateEXP();
        }

        private void slain()
        {
            delayPrintLine(userDisplay, "You have been slain by a " + monster + " -- GAME OVER.");
            //userDisplay.AppendText(Environment.NewLine);
            //End Game
            endBtn.Visible = true;
            mobPicture.Visible = false;
            hero.Exp = 0;
            hero.StatusEff = "Dead";
            this.BackgroundImage = null;
            this.BackColor = Color.Red;
            
        }

        private void checkStatusEff()
        {
            if(monster == "Spider" || monster == "Giant Toad" || monster == "Snake")
            {
                if(rand.Next(99) + 1 <= 5)
                {
                    hero.StatusEff = "Poisoned";
                    delayPrintLine(userDisplay, "The " + monster + "'s attack has poisoned you.");
                    //userDisplay.AppendText(Environment.NewLine);
                }
            }
        }

        private void applyStatusDamage()
        {
            if(hero.StatusEff == "Poisoned")
            {
                hero.Hp -= hero.MaxHp / 20;
                delayPrintLine(userDisplay, "You take " + (hero.MaxHp / 20).ToString() + " points of damage from poison.");
                //userDisplay.AppendText(Environment.NewLine);
                updateHP();
            }
        }

        public void printToDisplay(string message)
        {
            delayPrintLine(userDisplay, message);
        }

        //ITEM GENERATION CODE

        public void createItem(string item)
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
                    hero.Inventory.Add(potion);
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
                    hero.Inventory.Add(potion1);
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
                    hero.Inventory.Add(potion2);
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
                    hero.Inventory.Add(antidote);
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
                    hero.Inventory.Add(bandage);
                    break;
            }
        }

        //PUBLIC UPDATE Hp METHOD FOR INVENTORY ITEM USEAGE
        public void updateHP()
        {
            currentHP.Text = "Hp:  " + hero.Hp;
        }

        private void BattleForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.A:
                    attackBtn_Click(sender, e);
                    break;
                case Keys.D:
                    defendBtn_Click(sender, e);
                    break;
                case Keys.I:
                    label1_Click(sender, e);
                    break;
                case Keys.F:
                    fleeBtn_Click(sender, e);
                    break;
                case Keys.Escape:
                    fleeBtn_Click(sender, e);
                    break;
            }
        }

        private void BattleForm_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                //case Keys.A:
                //    attackBtn_Click(sender, e);
                //    break;
                //case Keys.D:
                //    defendBtn_Click(sender, e);
                //    break;
                //case Keys.I:
                //    label1_Click(sender, e);
                //    break;
                //case Keys.F:
                //    fleeBtn_Click(sender, e);
                //    break;
                //case Keys.Escape:
                //    fleeBtn_Click(sender, e);
                //    break;
            }
        }

        ////CHARACTER PRINT DELAY METHOD
        async public void delayPrintLine(TextBox console, string output)
        {
            await semaphoreSlim.WaitAsync();
            await eventSemaphore.WaitAsync();
            try
            {
                string s = output;
                char[] charArray = s.ToCharArray();
                s = "";

                for (int i = 0; i < charArray.Length; i++)
                {
                    console.AppendText(charArray[i].ToString());
                    await Task.Delay(20);
                }
                console.AppendText(Environment.NewLine);
            }
            finally
            {
                eventSemaphore.Release();
                semaphoreSlim.Release();
            }
        }

        async public void delayPrintLine(TextBox console, string output, bool lastLine)
        {
            await semaphoreSlim.WaitAsync();
            try
            {
                string s = output;
                char[] charArray = s.ToCharArray();
                s = "";

                for (int i = 0; i < charArray.Length; i++)
                {
                    console.AppendText(charArray[i].ToString());
                    await Task.Delay(20);
                }
                console.AppendText(Environment.NewLine);
            }
            finally
            {
                semaphoreSlim.Release();
                activeAction = false;
            }
        }

        private void eventTimer_Tick(object sender, EventArgs e)
        {
            activeAction = false;
            eventTimer.Stop();
        }
    }
}
