using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AzilorQuest
{
    public partial class Inventory : Form
    {
        //GLOBALS
        private Azilor hero;
        private AzQuest form1;
        private BattleForm currentBattle;

        //CONSTRUCTORS
        public Inventory(Azilor character, AzQuest form)
        {
            InitializeComponent();
            hero = character;
            form1 = form;
        }

        //CONSTRUCTOR FOR BATTLEFORM INVENTORY BUTTON
        public Inventory(Azilor character, AzQuest form, BattleForm battle)
        {
            InitializeComponent();
            hero = character;
            form1 = form;
            currentBattle = battle;
        }

        //LOAD EVENT
        private void Inventory_Load(object sender, EventArgs e)
        {

            //DISPLAY EQUIPPED GEAR
            refreshEquipment();
            refreshStats();

            //DISPLAY CURRENT INVENTORY
            refreshInventory();
        }

        //CLICK BUTTON EVENTS
        private void equipBtn_Click(object sender, EventArgs e)
        {
            int itemIndex = invDisplay.SelectedIndex;
            if (itemIndex != -1)
            {

                if (hero.Inventory[itemIndex].Type == "Weapon" && hero.Inventory[itemIndex].Equipped == false)
                {
                    for (int i = 0; i < hero.Inventory.Count; i++)
                    {
                        if (hero.Equipment[0].Name == hero.Inventory[i].Name)
                        {
                            hero.Inventory[i].Equipped = false;
                        }
                    }
                    hero.Equipment[0] = hero.Inventory[itemIndex];
                    hero.Inventory[itemIndex].Equipped = true;
                }
                else if (hero.Inventory[itemIndex].Type == "Shield" && hero.Inventory[itemIndex].Equipped == false)
                {
                    for (int i = 0; i < hero.Inventory.Count; i++)
                    {
                        if (hero.Equipment[1].Name == hero.Inventory[i].Name)
                        {
                            hero.Inventory[i].Equipped = false;
                        }
                    }
                    hero.Equipment[1] = hero.Inventory[itemIndex];
                    hero.Inventory[itemIndex].Equipped = true;
                }
                else if (hero.Inventory[itemIndex].Type == "Helm" && hero.Inventory[itemIndex].Equipped == false)
                {
                    for (int i = 0; i < hero.Inventory.Count; i++)
                    {
                        if (hero.Equipment[2].Name == hero.Inventory[i].Name)
                        {
                            hero.Inventory[i].Equipped = false;
                        }
                    }
                    hero.Equipment[2] = hero.Inventory[itemIndex];
                    hero.Inventory[itemIndex].Equipped = true;
                }
                else if (hero.Inventory[itemIndex].Type == "Armor" && hero.Inventory[itemIndex].Equipped == false)
                {
                    for (int i = 0; i < hero.Inventory.Count; i++)
                    {
                        if (hero.Equipment[3].Name == hero.Inventory[i].Name)
                        {
                            hero.Inventory[i].Equipped = false;
                        }
                    }
                    hero.Equipment[3] = hero.Inventory[itemIndex];
                    hero.Inventory[itemIndex].Equipped = true;
                }
                else if (hero.Inventory[itemIndex].Type == "Boots" && hero.Inventory[itemIndex].Equipped == false)
                {
                    for (int i = 0; i < hero.Inventory.Count; i++)
                    {
                        if (hero.Equipment[4].Name == hero.Inventory[i].Name)
                        {
                            hero.Inventory[i].Equipped = false;
                        }
                    }
                    hero.Equipment[4] = hero.Inventory[itemIndex];
                    hero.Inventory[itemIndex].Equipped = true;
                }
                else if (hero.Inventory[itemIndex].Equipped == true)
                {
                    MessageBox.Show("The " + hero.Inventory[itemIndex].Name + " is already equipped.");
                }
                else if (hero.Inventory[itemIndex].Type == "Consumable")
                {
                    MessageBox.Show(hero.Inventory[itemIndex].Name + " is a consumable and cannot be equipped.");
                }
                else if (itemIndex == -1)
                {
                    MessageBox.Show("Please select an item on the right box.");
                }
            }
            else
            {
                MessageBox.Show("Please select an item to equip.");
            }

            refreshEquipment();
            refreshStats();
        }

        private void useBtn_Click(object sender, EventArgs e)
        {
            int itemIndex = invDisplay.SelectedIndex;
            if (itemIndex != -1)
            {
                if (hero.Inventory[itemIndex].Type == "Consumable" && hero.Inventory[itemIndex].Power > 0 && hero.HP < hero.MaxHP)
                {
                    hero.HP += hero.Inventory[itemIndex].Power;
                    if (hero.HP > hero.MaxHP)
                    {
                        hero.HP = hero.MaxHP;
                    }
                    MessageBox.Show("You have recovered " + hero.Inventory[itemIndex].Power + " hit points.");
                    hero.Inventory.RemoveAt(itemIndex);
                    refreshInventory();
                    checkForBattle();
                }
                else if (hero.Inventory[itemIndex].Type == "Consumable" && hero.Inventory[itemIndex].Power > 0 && hero.HP >= hero.MaxHP)
                {
                    MessageBox.Show("You're at full health already.");
                }
                else if (hero.Inventory[itemIndex].Name == "Antidote" && hero.StatusEff.Contains("Poisoned"))
                {
                    hero.StatusEff = "Healthy";
                    MessageBox.Show("You feel the poison leave your body.");
                    hero.Inventory.RemoveAt(itemIndex);
                    refreshInventory();
                    checkForBattle();
                }
                else if (hero.Inventory[itemIndex].Name == "Bandage" && hero.StatusEff.Contains("Bleeding"))
                {
                    hero.StatusEff = "Healthy";
                    MessageBox.Show("You apply a bandage to your wounds.");
                    hero.Inventory.RemoveAt(itemIndex);
                    refreshInventory();
                    checkForBattle();
                }
                else if (hero.Inventory[itemIndex].Name == "Antidote" && !hero.StatusEff.Contains("Poisoned"))
                {
                    MessageBox.Show("You are not currently poisoned.");
                }
                else if (hero.Inventory[itemIndex].Name == "Bandage" && !hero.StatusEff.Contains("Bleeding"))
                {
                    MessageBox.Show("You are not currently bleeding.");
                }
                else
                {
                    MessageBox.Show(hero.Inventory[itemIndex].Name + " is not a consumable.");
                }
                refreshStats();
            }
            else
            {
                MessageBox.Show("Please select an item to use.");
            }
        }

        private void infoBtn_Click(object sender, EventArgs e)
        {
            int itemIndex = invDisplay.SelectedIndex;
            if (itemIndex != -1)
            {
                MessageBox.Show(hero.Inventory[itemIndex].Desc);
            }
            else
            {
                MessageBox.Show("Please select an item.");
            }
        }

        private void quitBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //CUSTOM METHODS

        private void refreshEquipment()
        {
            equipmentDisplay.Items.Clear();
            equipmentDisplay.Items.Add("Weapon:  " + hero.Equipment[0].Name);
            equipmentDisplay.Items.Add("");
            equipmentDisplay.Items.Add("Shield:  " + hero.Equipment[1].Name);
            equipmentDisplay.Items.Add("");
            equipmentDisplay.Items.Add("Helm:  " + hero.Equipment[2].Name);
            equipmentDisplay.Items.Add("");
            equipmentDisplay.Items.Add("Armor:  " + hero.Equipment[3].Name);
            equipmentDisplay.Items.Add("");
            equipmentDisplay.Items.Add("Boots:  " + hero.Equipment[4].Name);
        }

        private void refreshStats()
        {
            maxHPLabel.Text =   "Max HP: " + hero.MaxHP;
            hpLabel.Text =      "HP:  " + hero.HP;
            strLabel.Text =     "Str: " + hero.Str + "(" + hero.Equipment[0].Power + ")";
            dexLabel.Text =     "Dex: " + hero.Dex;
            conLabel.Text =     "Con: " + hero.Con + "(" + (hero.Equipment[1].Armor + hero.Equipment[2].Armor + hero.Equipment[3].Armor + hero.Equipment[4].Armor).ToString() + ")";
            luckLabel.Text =    "Luck: " + hero.Luck;
            expLabel.Text =     "Exp: " + hero.Exp;
            tnlLabel.Text =     "Next Level: " + hero.TNL;
            form1.updateHP();
            if (currentBattle != null)
            {
                currentBattle.updateHP();
            }
        }

        private void refreshInventory()
        {
            invDisplay.Items.Clear();
            for (int i = 0; i < hero.Inventory.Count; i++)
            {
                invDisplay.Items.Add(hero.Inventory[i].Name);
            }
        }

        private void checkForBattle()
        {
            if(currentBattle != null)
            {
                this.Close();
                currentBattle.mobAttack(1);
            }
        }

        private void Inventory_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    this.Close();
                    break;
            }
        }
    }
}
