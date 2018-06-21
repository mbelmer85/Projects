namespace AzilorQuest
{
    partial class Inventory
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
            this.equipmentDisplay = new System.Windows.Forms.ListBox();
            this.statslabel = new System.Windows.Forms.Label();
            this.maxHPLabel = new System.Windows.Forms.Label();
            this.hpLabel = new System.Windows.Forms.Label();
            this.strLabel = new System.Windows.Forms.Label();
            this.dexLabel = new System.Windows.Forms.Label();
            this.conLabel = new System.Windows.Forms.Label();
            this.luckLabel = new System.Windows.Forms.Label();
            this.expLabel = new System.Windows.Forms.Label();
            this.tnlLabel = new System.Windows.Forms.Label();
            this.invDisplay = new System.Windows.Forms.ListBox();
            this.equipBtn = new System.Windows.Forms.Label();
            this.useBtn = new System.Windows.Forms.Label();
            this.infoBtn = new System.Windows.Forms.Label();
            this.quitBtn = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // equipmentDisplay
            // 
            this.equipmentDisplay.BackColor = System.Drawing.Color.Black;
            this.equipmentDisplay.Font = new System.Drawing.Font("Poor Richard", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.equipmentDisplay.ForeColor = System.Drawing.Color.White;
            this.equipmentDisplay.FormattingEnabled = true;
            this.equipmentDisplay.ItemHeight = 22;
            this.equipmentDisplay.Location = new System.Drawing.Point(161, 169);
            this.equipmentDisplay.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.equipmentDisplay.Name = "equipmentDisplay";
            this.equipmentDisplay.Size = new System.Drawing.Size(274, 246);
            this.equipmentDisplay.TabIndex = 0;
            // 
            // statslabel
            // 
            this.statslabel.AutoSize = true;
            this.statslabel.BackColor = System.Drawing.Color.Transparent;
            this.statslabel.ForeColor = System.Drawing.Color.White;
            this.statslabel.Location = new System.Drawing.Point(447, 169);
            this.statslabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.statslabel.Name = "statslabel";
            this.statslabel.Size = new System.Drawing.Size(54, 22);
            this.statslabel.TabIndex = 1;
            this.statslabel.Text = "Stats:";
            // 
            // maxHPLabel
            // 
            this.maxHPLabel.AutoSize = true;
            this.maxHPLabel.BackColor = System.Drawing.Color.Transparent;
            this.maxHPLabel.ForeColor = System.Drawing.Color.White;
            this.maxHPLabel.Location = new System.Drawing.Point(465, 195);
            this.maxHPLabel.Name = "maxHPLabel";
            this.maxHPLabel.Size = new System.Drawing.Size(76, 22);
            this.maxHPLabel.TabIndex = 2;
            this.maxHPLabel.Text = "Max HP:";
            // 
            // hpLabel
            // 
            this.hpLabel.AutoSize = true;
            this.hpLabel.BackColor = System.Drawing.Color.Transparent;
            this.hpLabel.ForeColor = System.Drawing.Color.White;
            this.hpLabel.Location = new System.Drawing.Point(465, 217);
            this.hpLabel.Name = "hpLabel";
            this.hpLabel.Size = new System.Drawing.Size(37, 22);
            this.hpLabel.TabIndex = 3;
            this.hpLabel.Text = "HP:";
            // 
            // strLabel
            // 
            this.strLabel.AutoSize = true;
            this.strLabel.BackColor = System.Drawing.Color.Transparent;
            this.strLabel.ForeColor = System.Drawing.Color.White;
            this.strLabel.Location = new System.Drawing.Point(465, 239);
            this.strLabel.Name = "strLabel";
            this.strLabel.Size = new System.Drawing.Size(37, 22);
            this.strLabel.TabIndex = 4;
            this.strLabel.Text = "Str:";
            // 
            // dexLabel
            // 
            this.dexLabel.AutoSize = true;
            this.dexLabel.BackColor = System.Drawing.Color.Transparent;
            this.dexLabel.ForeColor = System.Drawing.Color.White;
            this.dexLabel.Location = new System.Drawing.Point(465, 261);
            this.dexLabel.Name = "dexLabel";
            this.dexLabel.Size = new System.Drawing.Size(45, 22);
            this.dexLabel.TabIndex = 5;
            this.dexLabel.Text = "Dex:";
            // 
            // conLabel
            // 
            this.conLabel.AutoSize = true;
            this.conLabel.BackColor = System.Drawing.Color.Transparent;
            this.conLabel.ForeColor = System.Drawing.Color.White;
            this.conLabel.Location = new System.Drawing.Point(465, 283);
            this.conLabel.Name = "conLabel";
            this.conLabel.Size = new System.Drawing.Size(46, 22);
            this.conLabel.TabIndex = 6;
            this.conLabel.Text = "Con:";
            // 
            // luckLabel
            // 
            this.luckLabel.AutoSize = true;
            this.luckLabel.BackColor = System.Drawing.Color.Transparent;
            this.luckLabel.ForeColor = System.Drawing.Color.White;
            this.luckLabel.Location = new System.Drawing.Point(465, 305);
            this.luckLabel.Name = "luckLabel";
            this.luckLabel.Size = new System.Drawing.Size(53, 22);
            this.luckLabel.TabIndex = 7;
            this.luckLabel.Text = "Luck:";
            // 
            // expLabel
            // 
            this.expLabel.AutoSize = true;
            this.expLabel.BackColor = System.Drawing.Color.Transparent;
            this.expLabel.ForeColor = System.Drawing.Color.White;
            this.expLabel.Location = new System.Drawing.Point(465, 327);
            this.expLabel.Name = "expLabel";
            this.expLabel.Size = new System.Drawing.Size(43, 22);
            this.expLabel.TabIndex = 8;
            this.expLabel.Text = "Exp:";
            // 
            // tnlLabel
            // 
            this.tnlLabel.AutoSize = true;
            this.tnlLabel.BackColor = System.Drawing.Color.Transparent;
            this.tnlLabel.ForeColor = System.Drawing.Color.White;
            this.tnlLabel.Location = new System.Drawing.Point(465, 349);
            this.tnlLabel.Name = "tnlLabel";
            this.tnlLabel.Size = new System.Drawing.Size(101, 22);
            this.tnlLabel.TabIndex = 9;
            this.tnlLabel.Text = "Next Level:";
            // 
            // invDisplay
            // 
            this.invDisplay.BackColor = System.Drawing.Color.Black;
            this.invDisplay.ForeColor = System.Drawing.Color.White;
            this.invDisplay.FormattingEnabled = true;
            this.invDisplay.ItemHeight = 22;
            this.invDisplay.Location = new System.Drawing.Point(640, 169);
            this.invDisplay.Name = "invDisplay";
            this.invDisplay.Size = new System.Drawing.Size(189, 246);
            this.invDisplay.TabIndex = 10;
            // 
            // equipBtn
            // 
            this.equipBtn.BackColor = System.Drawing.Color.Transparent;
            this.equipBtn.Image = global::AzilorQuest.Properties.Resources.Equip;
            this.equipBtn.Location = new System.Drawing.Point(298, 480);
            this.equipBtn.Name = "equipBtn";
            this.equipBtn.Size = new System.Drawing.Size(155, 33);
            this.equipBtn.TabIndex = 11;
            this.equipBtn.Click += new System.EventHandler(this.equipBtn_Click);
            // 
            // useBtn
            // 
            this.useBtn.BackColor = System.Drawing.Color.Transparent;
            this.useBtn.Image = global::AzilorQuest.Properties.Resources.Use;
            this.useBtn.Location = new System.Drawing.Point(459, 480);
            this.useBtn.Name = "useBtn";
            this.useBtn.Size = new System.Drawing.Size(155, 33);
            this.useBtn.TabIndex = 12;
            this.useBtn.Click += new System.EventHandler(this.useBtn_Click);
            // 
            // infoBtn
            // 
            this.infoBtn.BackColor = System.Drawing.Color.Transparent;
            this.infoBtn.Image = global::AzilorQuest.Properties.Resources.Info;
            this.infoBtn.Location = new System.Drawing.Point(620, 480);
            this.infoBtn.Name = "infoBtn";
            this.infoBtn.Size = new System.Drawing.Size(155, 33);
            this.infoBtn.TabIndex = 13;
            this.infoBtn.Click += new System.EventHandler(this.infoBtn_Click);
            // 
            // quitBtn
            // 
            this.quitBtn.BackColor = System.Drawing.Color.Transparent;
            this.quitBtn.Image = global::AzilorQuest.Properties.Resources.button_quit;
            this.quitBtn.Location = new System.Drawing.Point(483, 569);
            this.quitBtn.Name = "quitBtn";
            this.quitBtn.Size = new System.Drawing.Size(105, 38);
            this.quitBtn.TabIndex = 14;
            this.quitBtn.Click += new System.EventHandler(this.quitBtn_Click);
            // 
            // Inventory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::AzilorQuest.Properties.Resources.InventoryBackground;
            this.ClientSize = new System.Drawing.Size(984, 662);
            this.ControlBox = false;
            this.Controls.Add(this.quitBtn);
            this.Controls.Add(this.infoBtn);
            this.Controls.Add(this.useBtn);
            this.Controls.Add(this.equipBtn);
            this.Controls.Add(this.invDisplay);
            this.Controls.Add(this.tnlLabel);
            this.Controls.Add(this.expLabel);
            this.Controls.Add(this.luckLabel);
            this.Controls.Add(this.conLabel);
            this.Controls.Add(this.dexLabel);
            this.Controls.Add(this.strLabel);
            this.Controls.Add(this.hpLabel);
            this.Controls.Add(this.maxHPLabel);
            this.Controls.Add(this.statslabel);
            this.Controls.Add(this.equipmentDisplay);
            this.Font = new System.Drawing.Font("Poor Richard", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.Name = "Inventory";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Inventory";
            this.Load += new System.EventHandler(this.Inventory_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Inventory_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox equipmentDisplay;
        private System.Windows.Forms.Label statslabel;
        private System.Windows.Forms.Label maxHPLabel;
        private System.Windows.Forms.Label hpLabel;
        private System.Windows.Forms.Label strLabel;
        private System.Windows.Forms.Label dexLabel;
        private System.Windows.Forms.Label conLabel;
        private System.Windows.Forms.Label luckLabel;
        private System.Windows.Forms.Label expLabel;
        private System.Windows.Forms.Label tnlLabel;
        private System.Windows.Forms.ListBox invDisplay;
        private System.Windows.Forms.Label equipBtn;
        private System.Windows.Forms.Label useBtn;
        private System.Windows.Forms.Label infoBtn;
        private System.Windows.Forms.Label quitBtn;
    }
}