/*
 * VoliBot GUI a.k.a. RitoBot GUI is part of the opensource VoliBot AutoQueuer project.
 * Credits to: shalzuth, Maufeat, imsosharp
 * Find assemblies for this AutoQueuer on LeagueSharp's official forum at:
 * http://www.joduska.me/
 * You are allowed to copy, edit and distribute this project,
 * as long as you don't touch this notice and you release your project with source.
 */

namespace RitoBot
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
            this.configTabControl = new System.Windows.Forms.TabControl();
            this.accountsTab = new System.Windows.Forms.TabPage();
            this.LoadedAccounts = new System.Windows.Forms.RichTextBox();
            this.SelectChampionInput = new System.Windows.Forms.ComboBox();
            this.accountsLabel = new System.Windows.Forms.Label();
            this.selectChampionLabel = new System.Windows.Forms.Label();
            this.QueueTypeInput = new System.Windows.Forms.ComboBox();
            this.QueueTypeLabel = new System.Windows.Forms.Label();
            this.addAccountsBtn = new System.Windows.Forms.Button();
            this.newPasswordInput = new System.Windows.Forms.TextBox();
            this.passwordLabel = new System.Windows.Forms.Label();
            this.newUserNameInput = new System.Windows.Forms.TextBox();
            this.userNameLabel = new System.Windows.Forms.Label();
            this.settingsTab = new System.Windows.Forms.TabPage();
            this.MaxLevelInput = new System.Windows.Forms.TextBox();
            this.maxLevelLabel = new System.Windows.Forms.Label();
            this.MaxBotsInput = new System.Windows.Forms.TextBox();
            this.maxBotsLabel = new System.Windows.Forms.Label();
            this.DefaultQueueInput = new System.Windows.Forms.ComboBox();
            this.defaultQueueLabel = new System.Windows.Forms.Label();
            this.DefaultChampionInput = new System.Windows.Forms.ComboBox();
            this.defaultChampionLabel = new System.Windows.Forms.Label();
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
            this.mainWindowSplitContainer.Panel1.BackColor = System.Drawing.SystemColors.Control;
            this.mainWindowSplitContainer.Panel1.Controls.Add(this.configTabControl);
            // 
            // mainWindowSplitContainer.Panel2
            // 
            this.mainWindowSplitContainer.Panel2.Controls.Add(this.console);
            this.mainWindowSplitContainer.Size = new System.Drawing.Size(1040, 476);
            this.mainWindowSplitContainer.SplitterDistance = 345;
            this.mainWindowSplitContainer.TabIndex = 0;
            // 
            // configTabControl
            // 
            this.configTabControl.Controls.Add(this.accountsTab);
            this.configTabControl.Controls.Add(this.settingsTab);
            this.configTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.configTabControl.Location = new System.Drawing.Point(0, 0);
            this.configTabControl.Name = "configTabControl";
            this.configTabControl.SelectedIndex = 0;
            this.configTabControl.Size = new System.Drawing.Size(345, 476);
            this.configTabControl.TabIndex = 1;
            // 
            // accountsTab
            // 
            this.accountsTab.Controls.Add(this.LoadedAccounts);
            this.accountsTab.Controls.Add(this.SelectChampionInput);
            this.accountsTab.Controls.Add(this.accountsLabel);
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
            this.accountsTab.Size = new System.Drawing.Size(337, 450);
            this.accountsTab.TabIndex = 0;
            this.accountsTab.Text = "Accounts";
            this.accountsTab.UseVisualStyleBackColor = true;
            // 
            // LoadedAccounts
            // 
            this.LoadedAccounts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LoadedAccounts.Location = new System.Drawing.Point(0, 202);
            this.LoadedAccounts.Name = "LoadedAccounts";
            this.LoadedAccounts.Size = new System.Drawing.Size(337, 245);
            this.LoadedAccounts.TabIndex = 3;
            this.LoadedAccounts.Text = "";
            // 
            // SelectChampionInput
            // 
            this.SelectChampionInput.Dock = System.Windows.Forms.DockStyle.Top;
            this.SelectChampionInput.FormattingEnabled = true;
            this.SelectChampionInput.Items.AddRange(new object[] {
            "AATROX",
            "AHRI",
            "AKALI",
            "ALISTAR",
            "AMUMU",
            "ANIVIA",
            "ANNIE",
            "ASHE",
            "AZIR",
            "BLITZCRANK",
            "BRAND",
            "BRAUM",
            "CAITLYN",
            "CASSIOPEIA",
            "CHOGATH",
            "CORKI",
            "DARIUS",
            "DIANA",
            "MUNDO",
            "DRAVEN",
            "ELISE",
            "EVELYNN",
            "EZREAL",
            "FIDDLESTICKS",
            "FIORA",
            "FIZZ",
            "GALIO",
            "GANGPLANK",
            "GAREN",
            "GNAR",
            "GRAGAS",
            "GRAVES",
            "HECARIM",
            "HEIMERDIGER",
            "IRELIA",
            "JANNA",
            "JARVAN",
            "JAX",
            "JAYCE",
            "JINX",
            "KALISTA",
            "KARMA",
            "KARTHUS",
            "KASSADIN",
            "KATARINA",
            "KAYLE",
            "KENNEN",
            "KHAZIX",
            "KOGMAW",
            "LEBLANC",
            "LEESIN",
            "LEONA",
            "LISSANDRA",
            "LUCIAN",
            "LULU",
            "LUX",
            "MALPHITE",
            "MALZAHAR",
            "MAOKAI",
            "MASTERYI",
            "MISSFORTUNE",
            "MORDEKAISER",
            "MORGANA",
            "NAMI",
            "NASUS",
            "NAUTILUS",
            "NIDALEE",
            "NOCTURNE",
            "NUNU",
            "OLAF",
            "ORIANNA",
            "PANTHEON",
            "POPPY",
            "QUINN",
            "REKSAI",
            "RAMMUS",
            "RENEKTON",
            "RENGAR",
            "RIVEN",
            "RUMBLE",
            "RYZE",
            "SEJUANI",
            "SHACO",
            "SHEN",
            "SHYVANA",
            "SINGED",
            "SION",
            "SIVIR",
            "SKARNER",
            "SONA",
            "SORAKA",
            "SWAIN",
            "SYNDRA",
            "TALON",
            "TARIC",
            "TEEMO",
            "THRESH",
            "TRISTANA",
            "TRUNDLE",
            "TRYNDAMERE",
            "TWISTEDFATE",
            "TWITCH",
            "UDYR",
            "URGOT",
            "VARUS",
            "VAYNE",
            "VEIGAR",
            "VELKOZ",
            "VI",
            "VIKTOR",
            "VLADIMIR",
            "VOLIBEAR",
            "WARWICK",
            "WUKONG",
            "XERATH",
            "XINZHAO",
            "YASUO",
            "YORICK",
            "ZAC",
            "ZED",
            "ZIGGS",
            "ZILEAN",
            "ZYRA"});
            this.SelectChampionInput.Location = new System.Drawing.Point(3, 116);
            this.SelectChampionInput.Name = "SelectChampionInput";
            this.SelectChampionInput.Size = new System.Drawing.Size(331, 21);
            this.SelectChampionInput.TabIndex = 8;
            // 
            // accountsLabel
            // 
            this.accountsLabel.AutoSize = true;
            this.accountsLabel.ForeColor = System.Drawing.Color.Black;
            this.accountsLabel.Location = new System.Drawing.Point(-2, 186);
            this.accountsLabel.Name = "accountsLabel";
            this.accountsLabel.Size = new System.Drawing.Size(52, 13);
            this.accountsLabel.TabIndex = 2;
            this.accountsLabel.Text = "Accounts";
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
            "NORMAL_5x5",
            "NORMAL_3x3",
            "INTRO_BOT",
            "BEGINNER_BOT",
            "MEDIUM_BOT",
            "ARAM"});
            this.QueueTypeInput.Location = new System.Drawing.Point(3, 82);
            this.QueueTypeInput.Name = "QueueTypeInput";
            this.QueueTypeInput.Size = new System.Drawing.Size(331, 21);
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
            this.addAccountsBtn.Location = new System.Drawing.Point(2, 160);
            this.addAccountsBtn.Name = "addAccountsBtn";
            this.addAccountsBtn.Size = new System.Drawing.Size(335, 23);
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
            this.newPasswordInput.Size = new System.Drawing.Size(331, 20);
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
            this.newUserNameInput.Size = new System.Drawing.Size(331, 20);
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
            this.settingsTab.Controls.Add(this.MaxLevelInput);
            this.settingsTab.Controls.Add(this.maxLevelLabel);
            this.settingsTab.Controls.Add(this.MaxBotsInput);
            this.settingsTab.Controls.Add(this.maxBotsLabel);
            this.settingsTab.Controls.Add(this.DefaultQueueInput);
            this.settingsTab.Controls.Add(this.defaultQueueLabel);
            this.settingsTab.Controls.Add(this.DefaultChampionInput);
            this.settingsTab.Controls.Add(this.defaultChampionLabel);
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
            this.settingsTab.Size = new System.Drawing.Size(337, 450);
            this.settingsTab.TabIndex = 1;
            this.settingsTab.Text = "Settings";
            this.settingsTab.UseVisualStyleBackColor = true;
            // 
            // MaxLevelInput
            // 
            this.MaxLevelInput.Dock = System.Windows.Forms.DockStyle.Top;
            this.MaxLevelInput.Location = new System.Drawing.Point(3, 286);
            this.MaxLevelInput.Name = "MaxLevelInput";
            this.MaxLevelInput.Size = new System.Drawing.Size(331, 20);
            this.MaxLevelInput.TabIndex = 21;
            // 
            // maxLevelLabel
            // 
            this.maxLevelLabel.AutoSize = true;
            this.maxLevelLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.maxLevelLabel.Location = new System.Drawing.Point(3, 273);
            this.maxLevelLabel.Name = "maxLevelLabel";
            this.maxLevelLabel.Size = new System.Drawing.Size(52, 13);
            this.maxLevelLabel.TabIndex = 20;
            this.maxLevelLabel.Text = "Max level";
            // 
            // MaxBotsInput
            // 
            this.MaxBotsInput.Dock = System.Windows.Forms.DockStyle.Top;
            this.MaxBotsInput.Location = new System.Drawing.Point(3, 253);
            this.MaxBotsInput.Name = "MaxBotsInput";
            this.MaxBotsInput.Size = new System.Drawing.Size(331, 20);
            this.MaxBotsInput.TabIndex = 19;
            // 
            // maxBotsLabel
            // 
            this.maxBotsLabel.AutoSize = true;
            this.maxBotsLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.maxBotsLabel.Location = new System.Drawing.Point(3, 240);
            this.maxBotsLabel.Name = "maxBotsLabel";
            this.maxBotsLabel.Size = new System.Drawing.Size(88, 13);
            this.maxBotsLabel.TabIndex = 18;
            this.maxBotsLabel.Text = "Max bots running";
            // 
            // DefaultQueueInput
            // 
            this.DefaultQueueInput.Dock = System.Windows.Forms.DockStyle.Top;
            this.DefaultQueueInput.FormattingEnabled = true;
            this.DefaultQueueInput.Items.AddRange(new object[] {
            "NORMAL_5x5",
            "NORMAL_3x3",
            "INTRO_BOT",
            "BEGINNER_BOT",
            "MEDIUM_BOT",
            "ARAM"});
            this.DefaultQueueInput.Location = new System.Drawing.Point(3, 219);
            this.DefaultQueueInput.Name = "DefaultQueueInput";
            this.DefaultQueueInput.Size = new System.Drawing.Size(331, 21);
            this.DefaultQueueInput.TabIndex = 17;
            // 
            // defaultQueueLabel
            // 
            this.defaultQueueLabel.AutoSize = true;
            this.defaultQueueLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.defaultQueueLabel.Location = new System.Drawing.Point(3, 206);
            this.defaultQueueLabel.Name = "defaultQueueLabel";
            this.defaultQueueLabel.Size = new System.Drawing.Size(103, 13);
            this.defaultQueueLabel.TabIndex = 16;
            this.defaultQueueLabel.Text = "Default Queue Type";
            // 
            // DefaultChampionInput
            // 
            this.DefaultChampionInput.Dock = System.Windows.Forms.DockStyle.Top;
            this.DefaultChampionInput.FormattingEnabled = true;
            this.DefaultChampionInput.Items.AddRange(new object[] {
            "AATROX",
            "AHRI",
            "AKALI",
            "ALISTAR",
            "AMUMU",
            "ANIVIA",
            "ANNIE",
            "ASHE",
            "AZIR",
            "BLITZCRANK",
            "BRAND",
            "BRAUM",
            "CAITLYN",
            "CASSIOPEIA",
            "CHOGATH",
            "CORKI",
            "DARIUS",
            "DIANA",
            "MUNDO",
            "DRAVEN",
            "ELISE",
            "EVELYNN",
            "EZREAL",
            "FIDDLESTICKS",
            "FIORA",
            "FIZZ",
            "GALIO",
            "GANGPLANK",
            "GAREN",
            "GNAR",
            "GRAGAS",
            "GRAVES",
            "HECARIM",
            "HEIMERDIGER",
            "IRELIA",
            "JANNA",
            "JARVAN",
            "JAX",
            "JAYCE",
            "JINX",
            "KALISTA",
            "KARMA",
            "KARTHUS",
            "KASSADIN",
            "KATARINA",
            "KAYLE",
            "KENNEN",
            "KHAZIX",
            "KOGMAW",
            "LEBLANC",
            "LEESIN",
            "LEONA",
            "LISSANDRA",
            "LUCIAN",
            "LULU",
            "LUX",
            "MALPHITE",
            "MALZAHAR",
            "MAOKAI",
            "MASTERYI",
            "MISSFORTUNE",
            "MORDEKAISER",
            "MORGANA",
            "NAMI",
            "NASUS",
            "NAUTILUS",
            "NIDALEE",
            "NOCTURNE",
            "NUNU",
            "OLAF",
            "ORIANNA",
            "PANTHEON",
            "POPPY",
            "QUINN",
            "REKSAI",
            "RAMMUS",
            "RENEKTON",
            "RENGAR",
            "RIVEN",
            "RUMBLE",
            "RYZE",
            "SEJUANI",
            "SHACO",
            "SHEN",
            "SHYVANA",
            "SINGED",
            "SION",
            "SIVIR",
            "SKARNER",
            "SONA",
            "SORAKA",
            "SWAIN",
            "SYNDRA",
            "TALON",
            "TARIC",
            "TEEMO",
            "THRESH",
            "TRISTANA",
            "TRUNDLE",
            "TRYNDAMERE",
            "TWISTEDFATE",
            "TWITCH",
            "UDYR",
            "URGOT",
            "VARUS",
            "VAYNE",
            "VEIGAR",
            "VELKOZ",
            "VI",
            "VIKTOR",
            "VLADIMIR",
            "VOLIBEAR",
            "WARWICK",
            "WUKONG",
            "XERATH",
            "XINZHAO",
            "YASUO",
            "YORICK",
            "ZAC",
            "ZED",
            "ZIGGS",
            "ZILEAN",
            "ZYRA"});
            this.DefaultChampionInput.Location = new System.Drawing.Point(3, 185);
            this.DefaultChampionInput.Name = "DefaultChampionInput";
            this.DefaultChampionInput.Size = new System.Drawing.Size(331, 21);
            this.DefaultChampionInput.TabIndex = 15;
            // 
            // defaultChampionLabel
            // 
            this.defaultChampionLabel.AutoSize = true;
            this.defaultChampionLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.defaultChampionLabel.Location = new System.Drawing.Point(3, 172);
            this.defaultChampionLabel.Name = "defaultChampionLabel";
            this.defaultChampionLabel.Size = new System.Drawing.Size(115, 13);
            this.defaultChampionLabel.TabIndex = 14;
            this.defaultChampionLabel.Text = "Default Champion Pick";
            // 
            // replaceConfigBtn
            // 
            this.replaceConfigBtn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.replaceConfigBtn.Location = new System.Drawing.Point(3, 341);
            this.replaceConfigBtn.Name = "replaceConfigBtn";
            this.replaceConfigBtn.Size = new System.Drawing.Size(331, 23);
            this.replaceConfigBtn.TabIndex = 13;
            this.replaceConfigBtn.Text = "REPLACE GAME CONFIG";
            this.replaceConfigBtn.UseVisualStyleBackColor = true;
            this.replaceConfigBtn.Click += new System.EventHandler(this.replaceConfigBtn_Click);
            // 
            // saveBtn
            // 
            this.saveBtn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.saveBtn.Location = new System.Drawing.Point(3, 312);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(331, 23);
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
            this.BuyBoostInput.Size = new System.Drawing.Size(331, 21);
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
            "BARRIER",
            "CLAIRVOYANCE",
            "CLARITY",
            "CLEANSE",
            "EXHAUST",
            "FLASH",
            "GARRISON",
            "GHOST",
            "HEAL",
            "IGNITE",
            "REVIVE",
            "SMITE",
            "TELEPORT"});
            this.Spell2Input.Location = new System.Drawing.Point(3, 117);
            this.Spell2Input.Name = "Spell2Input";
            this.Spell2Input.Size = new System.Drawing.Size(331, 21);
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
            "BARRIER",
            "CLAIRVOYANCE",
            "CLARITY",
            "CLEANSE",
            "EXHAUST",
            "FLASH",
            "GARRISON",
            "GHOST",
            "HEAL",
            "IGNITE",
            "REVIVE",
            "SMITE",
            "TELEPORT"});
            this.Spell1Input.Location = new System.Drawing.Point(3, 83);
            this.Spell1Input.Name = "Spell1Input";
            this.Spell1Input.Size = new System.Drawing.Size(331, 21);
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
            this.RegionInput.Size = new System.Drawing.Size(331, 21);
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
            this.LauncherPathInput.Size = new System.Drawing.Size(331, 20);
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
            this.console.ForeColor = System.Drawing.Color.Lime;
            this.console.ImeMode = System.Windows.Forms.ImeMode.On;
            this.console.Location = new System.Drawing.Point(0, 0);
            this.console.Name = "console";
            this.console.ReadOnly = true;
            this.console.Size = new System.Drawing.Size(691, 476);
            this.console.TabIndex = 0;
            this.console.Text = "";
            // 
            // frm_MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1040, 476);
            this.Controls.Add(this.mainWindowSplitContainer);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frm_MainWindow";
            this.Text = "RitoBot GUI";
            this.Load += new System.EventHandler(this.frm_MainWindow_Load);
            this.mainWindowSplitContainer.Panel1.ResumeLayout(false);
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
        private System.Windows.Forms.TextBox MaxLevelInput;
        private System.Windows.Forms.Label maxLevelLabel;
        private System.Windows.Forms.TextBox MaxBotsInput;
        private System.Windows.Forms.Label maxBotsLabel;
        private System.Windows.Forms.ComboBox DefaultQueueInput;
        private System.Windows.Forms.Label defaultQueueLabel;
        private System.Windows.Forms.ComboBox DefaultChampionInput;
        private System.Windows.Forms.Label defaultChampionLabel;
    }
}

