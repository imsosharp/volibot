/*
 * VoliBot GUI a.k.a. RitoBot GUI is part of the opensource VoliBot AutoQueuer project.
 * Credits to: shalzuth, Maufeat, imsosharp
 * Find assemblies for this AutoQueuer on LeagueSharp's official forum at:
 * http://www.joduska.me/
 * You are allowed to copy, edit and distribute this project,
 * as long as you don't touch this notice and you release your project with source.
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using LoLLauncher;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RitoBot
{
    public partial class frm_MainWindow : Form
    {
        public frm_MainWindow()
        {
            InitializeComponent();
            Print("VoliBot GUI RC2 Loaded.");
            Print("Volibot's ready for Version: " + Program.CVersion.Substring(0,4));
            Print("brought to you by imsosharp.", 4);
        }

        public void Print(string text)
        {
            console.AppendText("[" + DateTime.Now + "] : " + text + "\n");
        }
        public void Print(string text, int newlines)
        {
            console.AppendText("[" + DateTime.Now + "] : " + text);
            for (int i = 0; i < newlines; i++)
            {
                console.AppendText("\n");
            }
        }
        public void ShowAccount(string account, string password, string queuetype)
        {
            LoadedAccounts.AppendText("A: " + account + " Pw: " + password + " Q: " + queuetype );
        }

        private void addAccountsBtn_Click(object sender, EventArgs e)
        {
            if (newUserNameInput.Text.Length == 0 || newPasswordInput.Text.Length == 0)
            {
                MessageBox.Show("Username and Password cannot be empty!!!");
            }
            else
            {
                if (QueueTypeInput.SelectedIndex == -1 && SelectChampionInput.SelectedIndex == -1) FileHandlers.AccountsTxt(newUserNameInput.Text, newPasswordInput.Text);
                else if (SelectChampionInput.SelectedIndex == -1) FileHandlers.AccountsTxt(newUserNameInput.Text, newPasswordInput.Text, QueueTypeInput.SelectedItem.ToString());
                else FileHandlers.AccountsTxt(newUserNameInput.Text, newPasswordInput.Text, QueueTypeInput.SelectedItem.ToString(), SelectChampionInput.SelectedItem.ToString());
                Program.LoadAccounts();
                Thread.Sleep(1000);
                queueLoop();
            }
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            FileHandlers.SettingsIni(LauncherPathInput.Text, MaxBotsInput.Text, MaxLevelInput.Text, DefaultChampionInput.SelectedItem.ToString(), Spell1Input.SelectedItem.ToString(), Spell2Input.SelectedItem.ToString(), RegionInput.SelectedItem.ToString(), BuyBoostInput.SelectedItem.ToString());
            Program.LoadConfiguration();
        }

        private void replaceConfigBtn_Click(object sender, EventArgs e)
        {
            Print("Game configuration was optimized successfuly!");
            Program.GameCfg();
        }

        private void queueLoop()
        {
            foreach (string acc in Program.Accounts)
            {
                int curRunning = 0;
                try
                {
                    Program.Accounts2.RemoveAt(0);
                    string Accs = acc;
                    string[] stringSeparators = new string[] { "|" };
                    var result = Accs.Split(stringSeparators, StringSplitOptions.None);
                    console.ForeColor = Color.Lime;
                    curRunning += 1;
                    if (result[0].Contains("username"))
                    {
                        Print("No accounts found. Please add an account.", 2);
                    }
                    if (result[2] != null)
                    {
                        QueueTypes queuetype = (QueueTypes)System.Enum.Parse(typeof(QueueTypes), result[2]);
                        RiotBot ritoBot = new RiotBot(result[0], result[1], Program.Region, Program.Path2, curRunning, queuetype);
                        ShowAccount(result[0], result[1], result[2]);
                    }
                    else
                    {
                        QueueTypes queuetype = QueueTypes.ARAM;
                        RiotBot ritoBot = new RiotBot(result[0], result[1], Program.Region, Program.Path2, curRunning, queuetype);
                        ShowAccount(result[0], result[1], "ARAM");
                    }
                    Program.MainWindow.Text = " Volibot GUI | Currently connected: " + Program.ConnectedAccs;
                    if (curRunning == Program.MaxBots)
                        break;
                }
                catch (Exception)
                {
                    console.ForeColor = Color.Red;
                    Print("You may have an error in accounts.txt.");
                    Print("If you just started Volibot for the first time,");
                    Print("add a new account on the leftside panel.");
                    Print("If you keep getting this error,");
                    Print("Delete accounts.txt and restart voli.", 2);
                }
            }
        }

        private void frm_MainWindow_Load(object sender, EventArgs e)
        {
            Print("Starting Queue Loop");
            queueLoop();
        }

        private void LauncherPathInput_Click(object sender, EventArgs e)
        {

        }


    }
}
