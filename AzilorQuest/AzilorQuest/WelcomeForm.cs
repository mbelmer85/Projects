using System.Windows.Forms;

namespace AzilorQuest
{
    public partial class WelcomeForm : Form
    {
        public WelcomeForm()
        {
            InitializeComponent();
        }

        private void Welcome_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                InstructionsForm inst = new InstructionsForm();
                inst.ShowDialog();
                this.Close();
            }
        }
    }
}
