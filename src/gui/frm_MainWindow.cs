using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RitoBot_GUI
{
    public partial class frm_MainWindow : Form
    {
        public frm_MainWindow()
        {
            InitializeComponent();
            Print("RitoBot GUI *Proof-of-Concept* Loaded!");
            Print("brought to you by imsosharp", 4);

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

        private void addAccountsBtn_Click(object sender, EventArgs e)
        {
            Print("NOT YET IMPLEMENTED!");
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            Print("NOT YET IMPLEMENTED!");
        }

        private void replaceConfigBtn_Click(object sender, EventArgs e)
        {
            Print("NOT YET IMPLEMENTED!");
        }


    }
}
