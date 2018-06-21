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
    public partial class Quit : Form
    {
        private AzQuest form1;
        private Random rand = new Random();
        public Quit(AzQuest form)
        {
            form1 = form;
            InitializeComponent();
        }

        private void Quit_Load(object sender, EventArgs e)
        {
            int ranGen = rand.Next(99) + 1;
            if (ranGen <= 20)
            {
                this.BackColor = Color.Yellow;
                tauntDisplay.Text = "What are you? Yellow?";
            }
            else if (ranGen <= 40)
            {
                this.BackColor = Color.CadetBlue;
                tauntDisplay.Text = "Don't have the fortitude?";
            }
            else if (ranGen <= 60)
            {
                this.BackColor = Color.Maroon;
                tauntDisplay.Text = "It's gonna get bloody!!";
            }
            else if (ranGen <= 80)
            {
                this.BackColor = Color.Black;
                tauntDisplay.ForeColor = Color.White;
                tauntDisplay.Text = "Your future looks bleak...";
            }
            else if (ranGen <= 100)
            {
                this.BackColor = Color.RoyalBlue;
                tauntDisplay.ForeColor = Color.White;
                tauntDisplay.Text = "   SUBMIT!!!";
            }
        }

        private void invBtn_Click(object sender, EventArgs e)
        {
            form1.Close();
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
