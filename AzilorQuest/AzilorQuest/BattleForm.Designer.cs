namespace AzilorQuest
{
    partial class BattleForm
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
            this.components = new System.ComponentModel.Container();
            this.endBtn = new System.Windows.Forms.Label();
            this.mobPicture = new System.Windows.Forms.PictureBox();
            this.defendBtn = new System.Windows.Forms.Label();
            this.attackBtn = new System.Windows.Forms.Label();
            this.currentHP = new System.Windows.Forms.Label();
            this.currentExp = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.fleeBtn = new System.Windows.Forms.Label();
            this.userDisplay = new System.Windows.Forms.TextBox();
            this.eventTimer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.mobPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // endBtn
            // 
            this.endBtn.BackColor = System.Drawing.Color.Transparent;
            this.endBtn.Image = global::AzilorQuest.Properties.Resources.EndBattle;
            this.endBtn.Location = new System.Drawing.Point(255, 61);
            this.endBtn.Name = "endBtn";
            this.endBtn.Size = new System.Drawing.Size(156, 48);
            this.endBtn.TabIndex = 8;
            this.endBtn.Visible = false;
            this.endBtn.Click += new System.EventHandler(this.endBtn_Click);
            // 
            // mobPicture
            // 
            this.mobPicture.BackColor = System.Drawing.Color.Transparent;
            this.mobPicture.Location = new System.Drawing.Point(241, 12);
            this.mobPicture.Name = "mobPicture";
            this.mobPicture.Size = new System.Drawing.Size(182, 154);
            this.mobPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.mobPicture.TabIndex = 7;
            this.mobPicture.TabStop = false;
            // 
            // defendBtn
            // 
            this.defendBtn.BackColor = System.Drawing.Color.Transparent;
            this.defendBtn.Image = global::AzilorQuest.Properties.Resources.button_defend;
            this.defendBtn.Location = new System.Drawing.Point(130, 106);
            this.defendBtn.Name = "defendBtn";
            this.defendBtn.Size = new System.Drawing.Size(105, 38);
            this.defendBtn.TabIndex = 6;
            this.defendBtn.Click += new System.EventHandler(this.defendBtn_Click);
            // 
            // attackBtn
            // 
            this.attackBtn.BackColor = System.Drawing.Color.Transparent;
            this.attackBtn.Image = global::AzilorQuest.Properties.Resources.button_attack;
            this.attackBtn.Location = new System.Drawing.Point(130, 36);
            this.attackBtn.Name = "attackBtn";
            this.attackBtn.Size = new System.Drawing.Size(105, 38);
            this.attackBtn.TabIndex = 5;
            this.attackBtn.Click += new System.EventHandler(this.attackBtn_Click);
            // 
            // currentHP
            // 
            this.currentHP.AutoSize = true;
            this.currentHP.BackColor = System.Drawing.Color.Transparent;
            this.currentHP.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.currentHP.ForeColor = System.Drawing.Color.White;
            this.currentHP.Location = new System.Drawing.Point(41, 153);
            this.currentHP.Name = "currentHP";
            this.currentHP.Size = new System.Drawing.Size(37, 16);
            this.currentHP.TabIndex = 9;
            this.currentHP.Text = "Hp: ";
            // 
            // currentExp
            // 
            this.currentExp.AutoSize = true;
            this.currentExp.BackColor = System.Drawing.Color.Transparent;
            this.currentExp.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.currentExp.ForeColor = System.Drawing.Color.White;
            this.currentExp.Location = new System.Drawing.Point(570, 150);
            this.currentExp.Name = "currentExp";
            this.currentExp.Size = new System.Drawing.Size(38, 16);
            this.currentExp.TabIndex = 10;
            this.currentExp.Text = "Exp:";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Image = global::AzilorQuest.Properties.Resources.Item;
            this.label1.Location = new System.Drawing.Point(429, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 38);
            this.label1.TabIndex = 11;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // fleeBtn
            // 
            this.fleeBtn.BackColor = System.Drawing.Color.Transparent;
            this.fleeBtn.Image = global::AzilorQuest.Properties.Resources.fleeBtn;
            this.fleeBtn.Location = new System.Drawing.Point(429, 106);
            this.fleeBtn.Name = "fleeBtn";
            this.fleeBtn.Size = new System.Drawing.Size(128, 38);
            this.fleeBtn.TabIndex = 12;
            this.fleeBtn.Click += new System.EventHandler(this.fleeBtn_Click);
            // 
            // userDisplay
            // 
            this.userDisplay.BackColor = System.Drawing.Color.Black;
            this.userDisplay.ForeColor = System.Drawing.Color.White;
            this.userDisplay.Location = new System.Drawing.Point(93, 174);
            this.userDisplay.Multiline = true;
            this.userDisplay.Name = "userDisplay";
            this.userDisplay.ReadOnly = true;
            this.userDisplay.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.userDisplay.Size = new System.Drawing.Size(487, 129);
            this.userDisplay.TabIndex = 426;
            // 
            // eventTimer
            // 
            this.eventTimer.Interval = 2650;
            this.eventTimer.Tick += new System.EventHandler(this.eventTimer_Tick);
            // 
            // BattleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::AzilorQuest.Properties.Resources.Dungeon;
            this.ClientSize = new System.Drawing.Size(670, 315);
            this.ControlBox = false;
            this.Controls.Add(this.userDisplay);
            this.Controls.Add(this.fleeBtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.currentExp);
            this.Controls.Add(this.currentHP);
            this.Controls.Add(this.endBtn);
            this.Controls.Add(this.mobPicture);
            this.Controls.Add(this.defendBtn);
            this.Controls.Add(this.attackBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.Name = "BattleForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Battle";
            this.Load += new System.EventHandler(this.BattleForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.BattleForm_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.BattleForm_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.mobPicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label defendBtn;
        private System.Windows.Forms.Label attackBtn;
        private System.Windows.Forms.PictureBox mobPicture;
        private System.Windows.Forms.Label endBtn;
        private System.Windows.Forms.Label currentHP;
        private System.Windows.Forms.Label currentExp;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label fleeBtn;
        private System.Windows.Forms.TextBox userDisplay;
        private System.Windows.Forms.Timer eventTimer;
    }
}