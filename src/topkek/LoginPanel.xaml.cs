/*
 * Topkek GUI is part of the opensource VoliBot AutoQueuer project.
 * Credits to: shalzuth, Maufeat, imsosharp
 * Find assemblies for this AutoQueuer on LeagueSharp's official forum at:
 * http://www.joduska.me/
 * You are allowed to copy, edit and distribute this project,
 * as long as you don't touch this notice and you release your project with source.
 */

using LoLLauncher;
using LoLLauncher.RiotObjects.Platform.Catalog.Champion;
using LoLLauncher.RiotObjects.Platform.Clientfacade.Domain;
using LoLLauncher.RiotObjects.Platform.Game;
using LoLLauncher.RiotObjects.Platform.Game.Message;
using LoLLauncher.RiotObjects.Platform.Matchmaking;
using LoLLauncher.RiotObjects.Platform.Statistics;
using LoLLauncher.RiotObjects;
using LoLLauncher.RiotObjects.Leagues.Pojo;
using LoLLauncher.RiotObjects.Platform.Game.Practice;
using LoLLauncher.RiotObjects.Platform.Harassment;
using LoLLauncher.RiotObjects.Platform.Leagues.Client.Dto;
using LoLLauncher.RiotObjects.Platform.Login;
using LoLLauncher.RiotObjects.Platform.Reroll.Pojo;
using LoLLauncher.RiotObjects.Platform.Statistics.Team;
using LoLLauncher.RiotObjects.Platform.Summoner;
using LoLLauncher.RiotObjects.Platform.Summoner.Boost;
using LoLLauncher.RiotObjects.Platform.Summoner.Masterybook;
using LoLLauncher.RiotObjects.Platform.Summoner.Runes;
using LoLLauncher.RiotObjects.Platform.Summoner.Spellbook;
using LoLLauncher.RiotObjects.Platform.Game.Map;
using LoLLauncher.RiotObjects.Team;
using LoLLauncher.RiotObjects.Team.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Runtime.CompilerServices;

namespace RitoBot.topkek
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    /// 
    public partial class LoginPanel : UserControl
    {
        public string login;
        public string password;
        public string version;
        private bool pressed = false;
        public LoginPanel()
        {
            InitializeComponent();
            gamesversion.Text = Program.cversion;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (pressed == true)
            {
                return;
            }
            Status.Content = "Connecting...";
            pressed = true;
            var server = Server.Text;
            login = Login.Text;
            password = Password.Password;
            version = gamesversion.Text;
            Connection.lolConnection.OnLogin += new LoLConnection.OnLoginHandler(this.connection_OnLogin);
            Connection.lolConnection.OnError += new LoLConnection.OnErrorHandler(this.connection_OnError);
            switch (server)
            {
                case "EUW":
                    Connection.lolConnection.Connect(login, password, Region.EUW, version);
                    break;

                case "EUNE":
                    Connection.lolConnection.Connect(login, password, Region.EUN, version);
                    break;

                case "BR":
                    Connection.lolConnection.Connect(login, password, Region.BR, version);
                    break;

                case "KR":
                    Connection.lolConnection.Connect(login, password, Region.KR, version);
                    break;

                case "OCE":
                    Connection.lolConnection.Connect(login, password, Region.OCE, version);
                    break;

                case "NA":
                    Connection.lolConnection.Connect(login, password, Region.NA, version);
                    break;

                case "TR":
                    Connection.lolConnection.Connect(login, password, Region.TR, version);
                    break;

                case "TW":
                    Connection.lolConnection.Connect(login, password, Region.TW, version);
                    break;

                case "RU":
                    Connection.lolConnection.Connect(login, password, Region.RU, version);
                    break;

                case "LAN":
                    Connection.lolConnection.Connect(login, password, Region.LAN, version);
                    break;

                case "LAS":
                    Connection.lolConnection.Connect(login, password, Region.LAS, version);
                    break;

            }

        }

        private void connection_OnLogin(object sender, string username, string ipAddress)
        {
            new Thread((ThreadStart)(async () =>
            {
                Connection.loginPacket = await Connection.lolConnection.GetLoginDataPacketForUser();
                await Connection.lolConnection.Subscribe("bc", Connection.loginPacket.AllSummonerData.Summoner.AcctId);
                await Connection.lolConnection.Subscribe("cn", Connection.loginPacket.AllSummonerData.Summoner.AcctId);
                await Connection.lolConnection.Subscribe("gn", Connection.loginPacket.AllSummonerData.Summoner.AcctId);
                Connection.SummLvl = Connection.loginPacket.AllSummonerData.SummonerLevel.Level;
                Connection.SummName = Connection.loginPacket.AllSummonerData.Summoner.Name;
                Connection.summIP = Connection.loginPacket.IpBalance;
                double sumId = Connection.loginPacket.AllSummonerData.Summoner.SumId;
                Connection.availableChampsArray = await Connection.lolConnection.GetAvailableChampions();
                
                this.Dispatcher.Invoke((Action)(() =>
                {
                    Connection.lolConnection.OnLogin -= this.connection_OnLogin;
                    Connection.lolConnection.OnError -= this.connection_OnError;
                    Connection.login = login;
                    Connection.password = password;
                    Connection.Version = version;
                    this.Content = KekHandler.userpanel;
                }));
            })).Start();
        }

        private void connection_OnError(object sender, LoLLauncher.Error error)
        {
            this.Dispatcher.Invoke((Action)(() =>
            {
                Connection.lolConnection.Disconnect();
                this.Content = new LoginPanel();
                MessageBox.Show(error.Message, "Error!");
                pressed = false;
            }));
        }

    }
}
