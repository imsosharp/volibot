#region

using System;
using System.Windows.Forms;
using LoLLauncher;

#endregion

namespace RitoBot
{
    public partial class FrmMainWindow : Form
    {
        public FrmMainWindow()
        {
            InitializeComponent();
            Print("GUI Version of Volibot loaded successfuly!");
            Print("Volibot version: " + Program.Cversion);
            Print("brought to you by imsosharp.", 4);
        }

        public void Print(string text)
        {
            console.AppendText("[" + DateTime.Now + "] : " + text + "\n");
        }

        public void Print(string text, int newlines)
        {
            console.AppendText("[" + DateTime.Now + "] : " + text);
            for (var i = 0; i < newlines; i++)
            {
                console.AppendText("\n");
            }
        }

        public void ShowAccount(string account, string password, string queuetype)
        {
            LoadedAccounts.AppendText("Acc: " + account + " Pwd: " + password + " Queue: " + queuetype);
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
            Program.Gamecfg();
        }

        private void QueueLoop()
        {
            var curRunning = 0;
            foreach (string acc in Program.Accounts)
            {
                try
                {
                    Program.Accounts2.RemoveAt(0);
                    var accs = acc;
                    string[] stringSeparators = {"|"};
                    var result = accs.Split(stringSeparators, StringSplitOptions.None);
                    curRunning += 1;
                    if (result[2] != null)
                    {
                        var queuetype = (QueueTypes) Enum.Parse(typeof (QueueTypes), result[2]);
                        var ritoBot = new RiotBot(result[0], result[1], Program.Region, Program.Path2, curRunning,
                            queuetype);
                        ShowAccount(result[0], result[1], result[2]);
                    }
                    else
                    {
                        var queuetype = QueueTypes.Aram;
                        var ritoBot = new RiotBot(result[0], result[1], Program.Region, Program.Path2, curRunning,
                            queuetype);
                        ShowAccount(result[0], result[1], "ARAM");
                    }
                    Program.MainWindow.Text = " Current Connected: " + curRunning;
                    if (curRunning == Program.MaxBots)
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
            QueueLoop();
            Print("Starting Queue Loop");
        }

        private void LauncherPathInput_Click(object sender, EventArgs e)
        {
        }
    }
}