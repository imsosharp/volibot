/*
 * Topkek GUI is part of the opensource VoliBot AutoQueuer project.
 * Credits to: shalzuth, Maufeat, imsosharp
 * Find assemblies for this AutoQueuer on LeagueSharp's official forum at:
 * http://www.joduska.me/
 * You are allowed to copy, edit and distribute this project,
 * as long as you don't touch this notice and you release your project with source.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LoLLauncher;
namespace RitoBot.topkek
{
    /// <summary>
    /// Interaction logic for UserPanel.xaml
    /// </summary>
    public partial class UserPanel : UserControl
    {
        public UserPanel()
        {
            InitializeComponent();
            loggedName.Content = "Account: " + Connection.login;
            summNameLabel.Content = "Name: " + Connection.SummName;
            summLvlLabel.Content = "Lvl: " + Connection.SummLvl;
            summIPLabel.Content = "IP: " + Connection.summIP;
            Connection.lolConnection.Disconnect();
        }

        private void QueueUP_Click(object sender, RoutedEventArgs e)
        {
            QueueTypes queuetype = (QueueTypes)System.Enum.Parse(typeof(QueueTypes), queueType1.Text);
            Queueing topkekQueue = new Queueing(champion.Text, spell1.Text, spell2.Text, queuetype);
        }
        
        public void changeStatus(string value)
        {
            statusLabel.Content = value;
        }
    }
}
