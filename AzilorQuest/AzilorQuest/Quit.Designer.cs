namespace AzilorQuest
{
    partial class Quit
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tauntDisplay = new System.Windows.Forms.Label();
            this.invBtn = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tauntDisplay
            // 
            this.tauntDisplay.AutoSize = true;
            this.tauntDisplay.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tauntDisplay.Location = new System.Drawing.Point(50, 34);
            this.tauntDisplay.Name = "tauntDisplay";
            this.tauntDisplay.Size = new System.Drawing.Size(257, 25);
            this.tauntDisplay.TabIndex = 0;
            this.tauntDisplay.Text = "\"Your Taunt goes here\"";
            // 
            // invBtn
            // 
            this.invBtn.BackColor = System.Drawing.Color.Transparent;
            this.invBtn.Image = global::AzilorQuest.Properties.Resources.runBtn;
            this.invBtn.Location = new System.Drawing.Point(24, 81);
            this.invBtn.Name = "invBtn";
            this.invBtn.Size = new System.Drawing.Size(123, 35);
            this.invBtn.TabIndex = 9;
            this.invBtn.Click += new System.EventHandler(this.invBtn_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Image = global::AzilorQuest.Properties.Resources.fightOnBtn;
            this.label1.Location = new System.Drawing.Point(210, 81);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 35);
            this.label1.TabIndex = 10;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // Quit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gold;
            this.ClientSize = new System.Drawing.Size(356, 136);
            this.ControlBox = false;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.invBtn);
            this.Controls.Add(this.tauntDisplay);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Quit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Quit";
            this.Load += new System.EventHandler(this.Quit_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label tauntDisplay;
        private System.Windows.Forms.Label invBtn;
        private System.Windows.Forms.Label label1;
    }
}