namespace RitoBot_GUI
{
    partial class frm_MainWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_MainWindow));
            this.mainWindowSplitContainer = new System.Windows.Forms.SplitContainer();
            this.LoadedAccounts = new System.Windows.Forms.RichTextBox();
            this.accountsLabel = new System.Windows.Forms.Label();
            this.configTabControl = new System.Windows.Forms.TabControl();
            this.accountsTab = new System.Windows.Forms.TabPage();
            this.SelectChampionInput = new System.Windows.Forms.ComboBox();
            this.selectChampionLabel = new System.Windows.Forms.Label();
            this.QueueTypeInput = new System.Windows.Forms.ComboBox();
            this.QueueTypeLabel = new System.Windows.Forms.Label();
            this.addAccountsBtn = new System.Windows.Forms.Button();
            this.newPasswordInput = new System.Windows.Forms.TextBox();
            this.passwordLabel = new System.Windows.Forms.Label();
            this.newUserNameInput = new System.Windows.Forms.TextBox();
            this.userNameLabel = new System.Windows.Forms.Label();
            this.settingsTab = new System.Windows.Forms.TabPage();
            this.replaceConfigBtn = new System.Windows.Forms.Button();
            this.saveBtn = new System.Windows.Forms.Button();
            this.BuyBoostInput = new System.Windows.Forms.ComboBox();
            this.BuyBoostLabel = new System.Windows.Forms.Label();
            this.Spell2Input = new System.Windows.Forms.ComboBox();
            this.spell2Label = new System.Windows.Forms.Label();
            this.Spell1Input = new System.Windows.Forms.ComboBox();
            this.spell1Label = new System.Windows.Forms.Label();
            this.RegionInput = new System.Windows.Forms.ComboBox();
            this.regionLabel = new System.Windows.Forms.Label();
            this.LauncherPathInput = new System.Windows.Forms.TextBox();
            this.launcherPathLabel = new System.Windows.Forms.Label();
            this.console = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.mainWindowSplitContainer)).BeginInit();
            this.mainWindowSplitContainer.Panel1.SuspendLayout();
            this.mainWindowSplitContainer.Panel2.SuspendLayout();
            this.mainWindowSplitContainer.SuspendLayout();
            this.configTabControl.SuspendLayout();
            this.accountsTab.SuspendLayout();
            this.settingsTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainWindowSplitContainer
            // 
            this.mainWindowSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainWindowSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.mainWindowSplitContainer.Name = "mainWindowSplitContainer";
            // 
            // mainWindowSplitContainer.Panel1
            // 
            this.mainWindowSplitContainer.Panel1.BackColor = System.Drawing.Color.Transparent;
            this.mainWindowSplitContainer.Panel1.Controls.Add(this.LoadedAccounts);
            this.mainWindowSplitContainer.Panel1.Controls.Add(this.accountsLabel);
            this.mainWindowSplitContainer.Panel1.Controls.Add(this.configTabControl);
            // 
            // mainWindowSplitContainer.Panel2
            // 
            this.mainWindowSplitContainer.Panel2.Controls.Add(this.console);
            this.mainWindowSplitContainer.Size = new System.Drawing.Size(1022, 474);
            this.mainWindowSplitContainer.SplitterDistance = 340;
            this.mainWindowSplitContainer.TabIndex = 0;
            // 
            // LoadedAccounts
            // 
            this.LoadedAccounts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LoadedAccounts.Location = new System.Drawing.Point(0, 261);
            this.LoadedAccounts.Name = "LoadedAccounts";
            this.LoadedAccounts.Size = new System.Drawing.Size(340, 213);
            this.LoadedAccounts.TabIndex = 3;
            this.LoadedAccounts.Text = "";
            // 
            // accountsLabel
            // 
            this.accountsLabel.AutoSize = true;
            this.accountsLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.accountsLabel.ForeColor = System.Drawing.Color.DarkGreen;
            this.accountsLabel.Location = new System.Drawing.Point(0, 248);
            this.accountsLabel.Name = "accountsLabel";
            this.accountsLabel.Size = new System.Drawing.Size(52, 13);
            this.accountsLabel.TabIndex = 2;
            this.accountsLabel.Text = "Accounts";
            // 
            // configTabControl
            // 
            this.configTabControl.Controls.Add(this.accountsTab);
            this.configTabControl.Controls.Add(this.settingsTab);
            this.configTabControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.configTabControl.Location = new System.Drawing.Point(0, 0);
            this.configTabControl.Name = "configTabControl";
            this.configTabControl.SelectedIndex = 0;
            this.configTabControl.Size = new System.Drawing.Size(340, 248);
            this.configTabControl.TabIndex = 1;
            // 
            // accountsTab
            // 
            this.accountsTab.Controls.Add(this.SelectChampionInput);
            this.accountsTab.Controls.Add(this.selectChampionLabel);
            this.accountsTab.Controls.Add(this.QueueTypeInput);
            this.accountsTab.Controls.Add(this.QueueTypeLabel);
            this.accountsTab.Controls.Add(this.addAccountsBtn);
            this.accountsTab.Controls.Add(this.newPasswordInput);
            this.accountsTab.Controls.Add(this.passwordLabel);
            this.accountsTab.Controls.Add(this.newUserNameInput);
            this.accountsTab.Controls.Add(this.userNameLabel);
            this.accountsTab.Location = new System.Drawing.Point(4, 22);
            this.accountsTab.Name = "accountsTab";
            this.accountsTab.Padding = new System.Windows.Forms.Padding(3);
            this.accountsTab.Size = new System.Drawing.Size(332, 222);
            this.accountsTab.TabIndex = 0;
            this.accountsTab.Text = "Accounts";
            this.accountsTab.UseVisualStyleBackColor = true;
            // 
            // SelectChampionInput
            // 
            this.SelectChampionInput.Dock = System.Windows.Forms.DockStyle.Top;
            this.SelectChampionInput.FormattingEnabled = true;
            this.SelectChampionInput.Location = new System.Drawing.Point(3, 116);
            this.SelectChampionInput.Name = "SelectChampionInput";
            this.SelectChampionInput.Size = new System.Drawing.Size(326, 21);
            this.SelectChampionInput.TabIndex = 8;
            // 
            // selectChampionLabel
            // 
            this.selectChampionLabel.AutoSize = true;
            this.selectChampionLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.selectChampionLabel.Location = new System.Drawing.Point(3, 103);
            this.selectChampionLabel.Name = "selectChampionLabel";
            this.selectChampionLabel.Size = new System.Drawing.Size(87, 13);
            this.selectChampionLabel.TabIndex = 7;
            this.selectChampionLabel.Text = "Select Champion";
            // 
            // QueueTypeInput
            // 
            this.QueueTypeInput.Dock = System.Windows.Forms.DockStyle.Top;
            this.QueueTypeInput.FormattingEnabled = true;
            this.QueueTypeInput.Items.AddRange(new object[] {
            "ARAM",
            "NORMAL_5x5",
            "NORMAL_3x3",
            "DOMINION_NORMAL_5x5",
            "DOMINION_BEGINNER_BOT",
            "INTRO_BOT",
            "BEGINNER_BOT",
            "MEDIUM_BOT",
            "TT_BEGINNER_BOT",
            "RANKED_SOLO_5x5"});
            this.QueueTypeInput.Location = new System.Drawing.Point(3, 82);
            this.QueueTypeInput.Name = "QueueTypeInput";
            this.QueueTypeInput.Size = new System.Drawing.Size(326, 21);
            this.QueueTypeInput.TabIndex = 6;
            // 
            // QueueTypeLabel
            // 
            this.QueueTypeLabel.AutoSize = true;
            this.QueueTypeLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.QueueTypeLabel.Location = new System.Drawing.Point(3, 69);
            this.QueueTypeLabel.Name = "QueueTypeLabel";
            this.QueueTypeLabel.Size = new System.Drawing.Size(66, 13);
            this.QueueTypeLabel.TabIndex = 5;
            this.QueueTypeLabel.Text = "Queue Type";
            // 
            // addAccountsBtn
            // 
            this.addAccountsBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.addAccountsBtn.AutoSize = true;
            this.addAccountsBtn.Location = new System.Drawing.Point(3, 148);
            this.addAccountsBtn.Name = "addAccountsBtn";
            this.addAccountsBtn.Size = new System.Drawing.Size(326, 23);
            this.addAccountsBtn.TabIndex = 4;
            this.addAccountsBtn.Text = "Add New Account";
            this.addAccountsBtn.UseVisualStyleBackColor = true;
            this.addAccountsBtn.Click += new System.EventHandler(this.addAccountsBtn_Click);
            // 
            // newPasswordInput
            // 
            this.newPasswordInput.Dock = System.Windows.Forms.DockStyle.Top;
            this.newPasswordInput.Location = new System.Drawing.Point(3, 49);
            this.newPasswordInput.Name = "newPasswordInput";
            this.newPasswordInput.Size = new System.Drawing.Size(326, 20);
            this.newPasswordInput.TabIndex = 3;
            // 
            // passwordLabel
            // 
            this.passwordLabel.AutoSize = true;
            this.passwordLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.passwordLabel.Location = new System.Drawing.Point(3, 36);
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.Size = new System.Drawing.Size(53, 13);
            this.passwordLabel.TabIndex = 2;
            this.passwordLabel.Text = "Password";
            // 
            // newUserNameInput
            // 
            this.newUserNameInput.Dock = System.Windows.Forms.DockStyle.Top;
            this.newUserNameInput.Location = new System.Drawing.Point(3, 16);
            this.newUserNameInput.Name = "newUserNameInput";
            this.newUserNameInput.Size = new System.Drawing.Size(326, 20);
            this.newUserNameInput.TabIndex = 1;
            // 
            // userNameLabel
            // 
            this.userNameLabel.AutoSize = true;
            this.userNameLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.userNameLabel.Location = new System.Drawing.Point(3, 3);
            this.userNameLabel.Name = "userNameLabel";
            this.userNameLabel.Size = new System.Drawing.Size(55, 13);
            this.userNameLabel.TabIndex = 0;
            this.userNameLabel.Text = "Username";
            // 
            // settingsTab
            // 
            this.settingsTab.Controls.Add(this.replaceConfigBtn);
            this.settingsTab.Controls.Add(this.saveBtn);
            this.settingsTab.Controls.Add(this.BuyBoostInput);
            this.settingsTab.Controls.Add(this.BuyBoostLabel);
            this.settingsTab.Controls.Add(this.Spell2Input);
            this.settingsTab.Controls.Add(this.spell2Label);
            this.settingsTab.Controls.Add(this.Spell1Input);
            this.settingsTab.Controls.Add(this.spell1Label);
            this.settingsTab.Controls.Add(this.RegionInput);
            this.settingsTab.Controls.Add(this.regionLabel);
            this.settingsTab.Controls.Add(this.LauncherPathInput);
            this.settingsTab.Controls.Add(this.launcherPathLabel);
            this.settingsTab.Location = new System.Drawing.Point(4, 22);
            this.settingsTab.Name = "settingsTab";
            this.settingsTab.Padding = new System.Windows.Forms.Padding(3);
            this.settingsTab.Size = new System.Drawing.Size(332, 222);
            this.settingsTab.TabIndex = 1;
            this.settingsTab.Text = "Settings";
            this.settingsTab.UseVisualStyleBackColor = true;
            // 
            // replaceConfigBtn
            // 
            this.replaceConfigBtn.AutoSize = true;
            this.replaceConfigBtn.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.replaceConfigBtn.Location = new System.Drawing.Point(3, 196);
            this.replaceConfigBtn.Name = "replaceConfigBtn";
            this.replaceConfigBtn.Size = new System.Drawing.Size(326, 23);
            this.replaceConfigBtn.TabIndex = 13;
            this.replaceConfigBtn.Text = "REPLACE GAME CONFIG";
            this.replaceConfigBtn.UseVisualStyleBackColor = true;
            this.replaceConfigBtn.Click += new System.EventHandler(this.replaceConfigBtn_Click);
            // 
            // saveBtn
            // 
            this.saveBtn.AutoSize = true;
            this.saveBtn.Dock = System.Windows.Forms.DockStyle.Top;
            this.saveBtn.Location = new System.Drawing.Point(3, 172);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(326, 23);
            this.saveBtn.TabIndex = 12;
            this.saveBtn.Text = "SAVE";
            this.saveBtn.UseVisualStyleBackColor = true;
            this.saveBtn.Click += new System.EventHandler(this.saveBtn_Click);
            // 
            // BuyBoostInput
            // 
            this.BuyBoostInput.Dock = System.Windows.Forms.DockStyle.Top;
            this.BuyBoostInput.FormattingEnabled = true;
            this.BuyBoostInput.Items.AddRange(new object[] {
            "YES",
            "NO"});
            this.BuyBoostInput.Location = new System.Drawing.Point(3, 151);
            this.BuyBoostInput.Name = "BuyBoostInput";
            this.BuyBoostInput.Size = new System.Drawing.Size(326, 21);
            this.BuyBoostInput.TabIndex = 11;
            // 
            // BuyBoostLabel
            // 
            this.BuyBoostLabel.AutoSize = true;
            this.BuyBoostLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.BuyBoostLabel.Location = new System.Drawing.Point(3, 138);
            this.BuyBoostLabel.Name = "BuyBoostLabel";
            this.BuyBoostLabel.Size = new System.Drawing.Size(78, 13);
            this.BuyBoostLabel.TabIndex = 10;
            this.BuyBoostLabel.Text = "Buy XP Boost?";
            // 
            // Spell2Input
            // 
            this.Spell2Input.Dock = System.Windows.Forms.DockStyle.Top;
            this.Spell2Input.FormattingEnabled = true;
            this.Spell2Input.Items.AddRange(new object[] {
            "NA",
            "EUW",
            "EUNE",
            "OCE",
            "LAN",
            "LAS",
            "BR",
            "TR",
            "RU",
            "QQ"});
            this.Spell2Input.Location = new System.Drawing.Point(3, 117);
            this.Spell2Input.Name = "Spell2Input";
            this.Spell2Input.Size = new System.Drawing.Size(326, 21);
            this.Spell2Input.TabIndex = 9;
            // 
            // spell2Label
            // 
            this.spell2Label.AutoSize = true;
            this.spell2Label.Dock = System.Windows.Forms.DockStyle.Top;
            this.spell2Label.Location = new System.Drawing.Point(3, 104);
            this.spell2Label.Name = "spell2Label";
            this.spell2Label.Size = new System.Drawing.Size(51, 13);
            this.spell2Label.TabIndex = 8;
            this.spell2Label.Text = "Spell2 (F)";
            // 
            // Spell1Input
            // 
            this.Spell1Input.Dock = System.Windows.Forms.DockStyle.Top;
            this.Spell1Input.FormattingEnabled = true;
            this.Spell1Input.Items.AddRange(new object[] {
            "NA",
            "EUW",
            "EUNE",
            "OCE",
            "LAN",
            "LAS",
            "BR",
            "TR",
            "RU",
            "QQ"});
            this.Spell1Input.Location = new System.Drawing.Point(3, 83);
            this.Spell1Input.Name = "Spell1Input";
            this.Spell1Input.Size = new System.Drawing.Size(326, 21);
            this.Spell1Input.TabIndex = 7;
            // 
            // spell1Label
            // 
            this.spell1Label.AutoSize = true;
            this.spell1Label.Dock = System.Windows.Forms.DockStyle.Top;
            this.spell1Label.Location = new System.Drawing.Point(3, 70);
            this.spell1Label.Name = "spell1Label";
            this.spell1Label.Size = new System.Drawing.Size(53, 13);
            this.spell1Label.TabIndex = 6;
            this.spell1Label.Text = "Spell1 (D)";
            // 
            // RegionInput
            // 
            this.RegionInput.Dock = System.Windows.Forms.DockStyle.Top;
            this.RegionInput.FormattingEnabled = true;
            this.RegionInput.Items.AddRange(new object[] {
            "NA",
            "EUW",
            "EUNE",
            "OCE",
            "LAN",
            "LAS",
            "BR",
            "TR",
            "RU",
            "QQ"});
            this.RegionInput.Location = new System.Drawing.Point(3, 49);
            this.RegionInput.Name = "RegionInput";
            this.RegionInput.Size = new System.Drawing.Size(326, 21);
            this.RegionInput.TabIndex = 5;
            // 
            // regionLabel
            // 
            this.regionLabel.AutoSize = true;
            this.regionLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.regionLabel.Location = new System.Drawing.Point(3, 36);
            this.regionLabel.Name = "regionLabel";
            this.regionLabel.Size = new System.Drawing.Size(41, 13);
            this.regionLabel.TabIndex = 4;
            this.regionLabel.Text = "Region";
            // 
            // LauncherPathInput
            // 
            this.LauncherPathInput.Dock = System.Windows.Forms.DockStyle.Top;
            this.LauncherPathInput.Location = new System.Drawing.Point(3, 16);
            this.LauncherPathInput.Name = "LauncherPathInput";
            this.LauncherPathInput.Size = new System.Drawing.Size(326, 20);
            this.LauncherPathInput.TabIndex = 3;
            // 
            // launcherPathLabel
            // 
            this.launcherPathLabel.AutoSize = true;
            this.launcherPathLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.launcherPathLabel.Location = new System.Drawing.Point(3, 3);
            this.launcherPathLabel.Name = "launcherPathLabel";
            this.launcherPathLabel.Size = new System.Drawing.Size(118, 13);
            this.launcherPathLabel.TabIndex = 2;
            this.launcherPathLabel.Text = "Path to lol.launcher.exe";
            // 
            // console
            // 
            this.console.BackColor = System.Drawing.Color.Black;
            this.console.DetectUrls = false;
            this.console.Dock = System.Windows.Forms.DockStyle.Fill;
            this.console.ForeColor = System.Drawing.Color.DarkGreen;
            this.console.ImeMode = System.Windows.Forms.ImeMode.On;
            this.console.Location = new System.Drawing.Point(0, 0);
            this.console.Name = "console";
            this.console.ReadOnly = true;
            this.console.Size = new System.Drawing.Size(678, 474);
            this.console.TabIndex = 0;
            this.console.Text = "";
            // 
            // frm_MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1022, 474);
            this.Controls.Add(this.mainWindowSplitContainer);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frm_MainWindow";
            this.Text = "RitoBot GUI (c) BumeKappa";
            this.mainWindowSplitContainer.Panel1.ResumeLayout(false);
            this.mainWindowSplitContainer.Panel1.PerformLayout();
            this.mainWindowSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mainWindowSplitContainer)).EndInit();
            this.mainWindowSplitContainer.ResumeLayout(false);
            this.configTabControl.ResumeLayout(false);
            this.accountsTab.ResumeLayout(false);
            this.accountsTab.PerformLayout();
            this.settingsTab.ResumeLayout(false);
            this.settingsTab.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer mainWindowSplitContainer;
        private System.Windows.Forms.RichTextBox console;
        private System.Windows.Forms.TabControl configTabControl;
        private System.Windows.Forms.TabPage accountsTab;
        private System.Windows.Forms.TabPage settingsTab;
        private System.Windows.Forms.Button addAccountsBtn;
        private System.Windows.Forms.TextBox newPasswordInput;
        private System.Windows.Forms.Label passwordLabel;
        private System.Windows.Forms.TextBox newUserNameInput;
        private System.Windows.Forms.Label userNameLabel;
        private System.Windows.Forms.ComboBox QueueTypeInput;
        private System.Windows.Forms.Label QueueTypeLabel;
        private System.Windows.Forms.TextBox LauncherPathInput;
        private System.Windows.Forms.Label launcherPathLabel;
        private System.Windows.Forms.ComboBox SelectChampionInput;
        private System.Windows.Forms.Label selectChampionLabel;
        private System.Windows.Forms.ComboBox RegionInput;
        private System.Windows.Forms.Label regionLabel;
        private System.Windows.Forms.Button replaceConfigBtn;
        private System.Windows.Forms.Button saveBtn;
        private System.Windows.Forms.ComboBox BuyBoostInput;
        private System.Windows.Forms.Label BuyBoostLabel;
        private System.Windows.Forms.ComboBox Spell2Input;
        private System.Windows.Forms.Label spell2Label;
        private System.Windows.Forms.ComboBox Spell1Input;
        private System.Windows.Forms.Label spell1Label;
        private System.Windows.Forms.RichTextBox LoadedAccounts;
        private System.Windows.Forms.Label accountsLabel;
    }
}

