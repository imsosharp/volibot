using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using LoLLauncher;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RitoBot
{
    public partial class frm_MainWindow : Form
    {
        public frm_MainWindow()
        {
            InitializeComponent();
            Print("GUI Version of Volibot loaded successfuly!");
            Print("Volibot version: " + Program.cversion);
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
            LoadedAccounts.AppendText("Acc: " + account + " Pwd: " + password + " Queue: " + queuetype );
        }

        private void addAccountsBtn_Click(object sender, EventArgs e)
        {
            
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            Print("NOT YET IMPLEMENTED!");
        }

        private void replaceConfigBtn_Click(object sender, EventArgs e)
        {
            Print("Game configuration was optimized successfuly!");
            Program.gamecfg();
        }

        private void queueLoop()
        {
            int curRunning = 0;
            foreach (string acc in Program.accounts)
            {
                try
                {
                    Program.accounts2.RemoveAt(0);
                    string Accs = acc;
                    string[] stringSeparators = new string[] { "|" };
                    var result = Accs.Split(stringSeparators, StringSplitOptions.None);
                    curRunning += 1;
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
                    Program.MainWindow.Text = " Current Connected: " + curRunning;
                    if (curRunning == Program.maxBots)
                        break;
                }
                catch (Exception)
                {
                    Console.WriteLine("CountAccError: You may have an issue in your accounts.txt");
                    Application.Exit();
                }
            }
        }

        private void frm_MainWindow_Load(object sender, EventArgs e)
        {
            queueLoop();
            Print("Starting Queue Loop");
        }

        private void LauncherPathInput_Click(object sender, EventArgs e)
        {

        }


    }
}
