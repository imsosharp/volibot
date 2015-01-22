/*
 * RiotBot.cs is part of the opensource VoliBot AutoQueuer project.
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
using LoLLauncher.RiotObjects.Team;
using LoLLauncher.RiotObjects.Team.Dto;
using System;
using System.Diagnostics;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using System.ComponentModel;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using LoLLauncher.RiotObjects.Platform.Game.Map;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using LoLLauncher.RiotObjects.Platform.Summoner.Icon;
using LoLLauncher.RiotObjects.Platform.Catalog.Icon;
using System.Timers;

namespace RitoBot
{
    internal class RiotBot
    {
        public LoginDataPacket loginPacket = new LoginDataPacket();
        public GameDTO currentGame = new GameDTO();
        public LoLConnection connection = new LoLConnection();
        public List<ChampionDTO> availableChamps = new List<ChampionDTO>();
        public LoLLauncher.RiotObjects.Platform.Catalog.Champion.ChampionDTO[] availableChampsArray;
        public bool firstTimeInLobby = true;
        public bool firstTimeInQueuePop = true;
        public bool firstTimeInCustom = true;
        public Process exeProcess;
        public string ipath;
        public string Accountname;
        public string Password;
        public int threadID;
        public double sumLevel { get; set; }
        public double archiveSumLevel { get; set; }
        public double rpBalance { get; set; }
        public QueueTypes queueType { get; set; }
        public QueueTypes actualQueueType { get; set; }

        public string region { get; set; }
        public string regionURL;
        public bool QueueFlag;
        public int LastAntiBusterAttempt = 0;
        private MatchMakerParams mMParams;

        public RiotBot(string username, string password, string reg, string path, int threadid, QueueTypes QueueType)
        {
            ipath = path;
            Accountname = username;
            Password = password;
            threadID = threadid;
            queueType = QueueType;
            region = reg;
            connection.OnConnect += new LoLConnection.OnConnectHandler(this.connection_OnConnect);
            connection.OnDisconnect += new LoLConnection.OnDisconnectHandler(this.connection_OnDisconnect);
            connection.OnError += new LoLConnection.OnErrorHandler(this.connection_OnError);
            connection.OnLogin += new LoLConnection.OnLoginHandler(this.connection_OnLogin);
            connection.OnLoginQueueUpdate += new LoLConnection.OnLoginQueueUpdateHandler(this.connection_OnLoginQueueUpdate);
            connection.OnMessageReceived += new LoLConnection.OnMessageReceivedHandler(this.connection_OnMessageReceived);
            switch (region)
            {
                case "EUW":
                    connection.Connect(username, password, Region.EUW, Program.cversion);
                    break;
                case "EUNE":
                    connection.Connect(username, password, Region.EUN, Program.cversion);
                    break;
                case "NA":
                    connection.Connect(username, password, Region.NA, Program.cversion);
                    regionURL = "NA1";
                    break;
                case "KR":
                    connection.Connect(username, password, Region.KR, Program.cversion);
                    break;
                case "BR":
                    connection.Connect(username, password, Region.BR, Program.cversion);
                    break;
                case "OCE":
                    connection.Connect(username, password, Region.OCE, Program.cversion);
                    break;
                case "RU":
                    connection.Connect(username, password, Region.RU, Program.cversion);
                    break;
                case "TR":
                    connection.Connect(username, password, Region.TR, Program.cversion);
                    break;
                case "LAS":
                    connection.Connect(username, password, Region.LAS, Program.cversion);
                    break;
                case "LAN":
                    connection.Connect(username, password, Region.LAN, Program.cversion);
                    break;
            }
        }

        public async void AntiBuster(MatchMakerParams matchParams)
        {
            //Thx to mah niggah Everance
            //who made this possible
            
            if (QueueFlag)
                {
                    Console.WriteLine(
                        "Something went wrong, couldn't enter queue. Check accounts.txt for correct queue type.");
                    connection.Disconnect();
                    Environment.Exit(0);
                }
                else
                {
                    this.updateStatus("Sorry, you're leavebusted :/ use LuxBot instead.", Accountname);
                    connection.Disconnect();
                }
        }

        public async void connection_OnMessageReceived(object sender, object message)
        {
            if (message is GameDTO)
            {
                GameDTO game = message as GameDTO;
                switch (game.GameState)
                {
                    case "CHAMP_SELECT":
                        if (this.firstTimeInLobby)
                        {
                            QueueFlag = true;
                            firstTimeInLobby = false;
                            updateStatus("In Champion Select", Accountname);
                            object obj = await connection.SetClientReceivedGameMessage(game.Id, "CHAMP_SELECT_CLIENT");
                            if (queueType != QueueTypes.ARAM)
                            {
                                if (Program.championId != "" && Program.championId != "RANDOM")
                                {
								
                                    int Spell1;
                                    int Spell2;
                                    if (!Program.rndSpell)
                                    {
                                        Spell1 = Enums.spellToId(Program.spell1);
                                        Spell2 = Enums.spellToId(Program.spell2);
                                    }
                                    else
                                    {
                                        var random = new Random();
                                        var spellList = new List<int> { 13, 6, 7, 10, 1, 11, 21, 12, 3, 14, 2, 4 };

                                        int index = random.Next(spellList.Count);
                                        int index2 = random.Next(spellList.Count);

                                        int randomSpell1 = spellList[index];
                                        int randomSpell2 = spellList[index2];

                                        if (randomSpell1 == randomSpell2)
                                        {
                                            int index3 = random.Next(spellList.Count);
                                            randomSpell2 = spellList[index3];
                                        }

                                        Spell1 = Convert.ToInt32(randomSpell1);
                                        Spell2 = Convert.ToInt32(randomSpell2);
                                    }

                                    await connection.SelectSpells(Spell1, Spell2);
								
                                    await connection.SelectChampion(Enums.championToId(Program.championId));
                                    await connection.ChampionSelectCompleted();
                                }
                                else if (Program.championId == "RANDOM")
                                {
								
                                    int Spell1;
                                    int Spell2;
                                    if (!Program.rndSpell)
                                    {
                                        Spell1 = Enums.spellToId(Program.spell1);
                                        Spell2 = Enums.spellToId(Program.spell2);
                                    }
                                    else
                                    {
                                        var random = new Random();
                                        var spellList = new List<int> { 13, 6, 7, 10, 1, 11, 21, 12, 3, 14, 2, 4 };

                                        int index = random.Next(spellList.Count);
                                        int index2 = random.Next(spellList.Count);

                                        int randomSpell1 = spellList[index];
                                        int randomSpell2 = spellList[index2];

                                        if (randomSpell1 == randomSpell2)
                                        {
                                            int index3 = random.Next(spellList.Count);
                                            randomSpell2 = spellList[index3];
                                        }

                                        Spell1 = Convert.ToInt32(randomSpell1);
                                        Spell2 = Convert.ToInt32(randomSpell2);
                                    }

                                    await connection.SelectSpells(Spell1, Spell2);
									
                                    var randAvailableChampsArray = availableChampsArray.Shuffle();
                                    await connection.SelectChampion(randAvailableChampsArray.First(champ => champ.Owned || champ.FreeToPlay).ChampionId);
                                    await connection.ChampionSelectCompleted();

                                }
                                else
                                {
								
                                    int Spell1;
                                    int Spell2;
                                    if (!Program.rndSpell)
                                    {
                                        Spell1 = Enums.spellToId(Program.spell1);
                                        Spell2 = Enums.spellToId(Program.spell2);
                                    }
                                    else
                                    {
                                        var random = new Random();
                                        var spellList = new List<int> { 13, 6, 7, 10, 1, 11, 21, 12, 3, 14, 2, 4 };

                                        int index = random.Next(spellList.Count);
                                        int index2 = random.Next(spellList.Count);

                                        int randomSpell1 = spellList[index];
                                        int randomSpell2 = spellList[index2];

                                        if (randomSpell1 == randomSpell2)
                                        {
                                            int index3 = random.Next(spellList.Count);
                                            randomSpell2 = spellList[index3];
                                        }

                                        Spell1 = Convert.ToInt32(randomSpell1);
                                        Spell2 = Convert.ToInt32(randomSpell2);
                                    }

                                    await connection.SelectSpells(Spell1, Spell2);
									
                                    await connection.SelectChampion(availableChampsArray.First(champ => champ.Owned || champ.FreeToPlay).ChampionId);
                                    await connection.ChampionSelectCompleted();
                                }
                            }
                            break;
                        }
                        else
                            break;
                    case "POST_CHAMP_SELECT":
                        firstTimeInLobby = false;
                        this.updateStatus("(Post Champ Select)", Accountname);
                        break;
                    case "PRE_CHAMP_SELECT":
                        this.updateStatus("(Pre Champ Select)", Accountname);
                        break;
                    case "GAME_START_CLIENT":
                        this.updateStatus("Game client ran", Accountname);
                        break;
                    case "GameClientConnectedToServer":
                        this.updateStatus("Client connected to the server", Accountname);
                        break;
                    case "IN_QUEUE":
                        this.updateStatus("In Queue", Accountname);
                        QueueFlag = true;
                        break;
                    case "TERMINATED":
                        this.updateStatus("Re-entering queue", Accountname);
                        this.firstTimeInQueuePop = true;
                        break;
                    case "JOINING_CHAMP_SELECT":
                        if (this.firstTimeInQueuePop && game.StatusOfParticipants.Contains("1"))
                        {
                            this.updateStatus("Accepted Queue", Accountname);
                            this.firstTimeInQueuePop = false;
                            this.firstTimeInLobby = true;
                            object obj = await this.connection.AcceptPoppedGame(true);
                            break;
                        }
                        else
                            break;
                    case "LEAVER_BUSTED":
                        this.updateStatus("Leave busted", Accountname);
                        break;
                }
            }
            else if (message is PlayerCredentialsDto)
            {
                string str = Enumerable.Last<string>((IEnumerable<string>)Enumerable.OrderBy<string, DateTime>(Directory.EnumerateDirectories((this.ipath ?? "") + "RADS\\solutions\\lol_game_client_sln\\releases\\"), (Func<string, DateTime>)(f => new DirectoryInfo(f).CreationTime))) + "\\deploy\\";
                LoLLauncher.RiotObjects.Platform.Game.PlayerCredentialsDto credentials = message as PlayerCredentialsDto;
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.CreateNoWindow = false;
                startInfo.WorkingDirectory = str;
                startInfo.FileName = "League of Legends.exe";
                startInfo.Arguments = "\"8394\" \"LoLLauncher.exe\" \"\" \"" + credentials.ServerIp + " " +
                credentials.ServerPort + " " + credentials.EncryptionKey + " " + credentials.SummonerId + "\"";
                updateStatus("Launching League of Legends", Accountname);
                new Thread((ThreadStart)(() =>
                {
                    exeProcess = Process.Start(startInfo);
                    exeProcess.Exited += exeProcess_Exited;
                    while (exeProcess.MainWindowHandle == IntPtr.Zero) ;
                    exeProcess.PriorityClass = ProcessPriorityClass.Idle;
                    exeProcess.EnableRaisingEvents = true;
                })).Start();
            }
            else if (!(message is GameNotification) && !(message is SearchingForMatchNotification))
            {
                if (message is EndOfGameStats)
                {
                    EndOfGameStats msg = message as EndOfGameStats;
                    LoLLauncher.RiotObjects.Platform.Matchmaking.MatchMakerParams matchParams = new LoLLauncher.RiotObjects.Platform.Matchmaking.MatchMakerParams();
                    if (queueType == QueueTypes.INTRO_BOT)
                    {
                        matchParams.BotDifficulty = "INTRO";
                    }
                    else if (queueType == QueueTypes.BEGINNER_BOT)
                    {
                        matchParams.BotDifficulty = "EASY";
                    }
                    else if (queueType == QueueTypes.MEDIUM_BOT)
                    {
                        matchParams.BotDifficulty = "MEDIUM";
                    }

                    if (sumLevel == 3 && actualQueueType == QueueTypes.NORMAL_5x5)
                    {
                        queueType = actualQueueType;
                    }
                    else if (sumLevel == 6 && actualQueueType == QueueTypes.ARAM)
                    {
                        queueType = actualQueueType;
                    }
                    else if (sumLevel == 7 && actualQueueType == QueueTypes.NORMAL_3x3)
                    {
                        queueType = actualQueueType;
                    }

                    matchParams.QueueIds = new Int32[1] { (int)queueType };
                    LoLLauncher.RiotObjects.Platform.Matchmaking.SearchingForMatchNotification m = await connection.AttachToQueue(matchParams);
                    if (m.PlayerJoinFailures == null)
                    {
                        this.updateStatus("In Queue: " + queueType.ToString(), Accountname);
                    }
                    else
                    {

                        try
                        {
                            updateStatus(
                                "Couldn't enter Q - " + m.PlayerJoinFailures.Summoner.Name + " : " +
                                m.PlayerJoinFailures.ReasonFailed, Accountname);
                        }
                        catch (Exception)
                        {
                            mMParams = matchParams;
                            AntiBuster(mMParams);
                        }
                    }
                }
                else
                {
                    if (message.ToString().Contains("EndOfGameStats"))
                    {
                        EndOfGameStats eog = new EndOfGameStats();
                        connection_OnMessageReceived(sender, eog);
                        exeProcess.Exited -= exeProcess_Exited;
                        exeProcess.Kill();
                        Thread.Sleep(500);
                        if (exeProcess.Responding)
                        {
                            Process.Start("taskkill /F /IM \"League of Legends.exe\"");
                        }
                        loginPacket = await this.connection.GetLoginDataPacketForUser();
                        archiveSumLevel = sumLevel;
                        sumLevel = loginPacket.AllSummonerData.SummonerLevel.Level;
                        if (sumLevel != archiveSumLevel)
                        {
                            levelUp();
                        }
                    }
                }
            }
        }

        void exeProcess_Exited(object sender, EventArgs e)
        {
           updateStatus("Restart League of Legends.", Accountname);
           Thread.Sleep(1000);
           if (this.loginPacket.ReconnectInfo != null && this.loginPacket.ReconnectInfo.Game != null)
           {
               this.connection_OnMessageReceived(sender, (object)this.loginPacket.ReconnectInfo.PlayerCredentials);
           }
           else
               this.connection_OnMessageReceived(sender, (object)new EndOfGameStats());
        }
        
        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);
        
        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);
       
        private void updateStatus(string status, string accname)
        {
            if (Program.LoadGUI) Program.MainWindow.Print(string.Concat(new object[4]
              {     
                (object) "[",
                (object) accname,
                (object) "]: ",
                (object) status
              }));
            Console.WriteLine(string.Concat(new object[7]
              {
                (object) "[",
                (object) DateTime.Now,
                (object) "] ",        
                (object) "[",
                (object) accname,
                (object) "]: ",
                (object) status
              }));
        }        
        
        private async void RegisterNotifications()
        {
            object obj1 = await this.connection.Subscribe("bc", this.connection.AccountID());
            object obj2 = await this.connection.Subscribe("cn", this.connection.AccountID());
            object obj3 = await this.connection.Subscribe("gn", this.connection.AccountID());
        }
        
        private void connection_OnLoginQueueUpdate(object sender, int positionInLine)
        {
            if (positionInLine <= 0)
                return;
            this.updateStatus("Position to login: " + (object)positionInLine, Accountname);
        }

        private void connection_OnLogin(object sender, string username, string ipAddress)
        {
            new Thread((ThreadStart)(async () =>
            { 
                updateStatus("Connecting...", Accountname);
                this.RegisterNotifications();
                this.loginPacket = await this.connection.GetLoginDataPacketForUser(); 
                if (loginPacket.AllSummonerData == null)
                {
                    Random rnd = new Random();
                    String summonerName = Accountname;
                    if (summonerName.Length > 16)
                        summonerName = summonerName.Substring(0, 12) + new Random().Next(1000, 9999).ToString();
                    LoLLauncher.RiotObjects.Platform.Summoner.AllSummonerData sumData = await connection.CreateDefaultSummoner(summonerName);
                    loginPacket.AllSummonerData = sumData;
                    updateStatus("Created Summonername " + summonerName, Accountname);
                }
                sumLevel = loginPacket.AllSummonerData.SummonerLevel.Level;
                string sumName = loginPacket.AllSummonerData.Summoner.Name;
                double sumId = loginPacket.AllSummonerData.Summoner.SumId;
                rpBalance = loginPacket.RpBalance;
                if (sumLevel > Program.maxLevel || sumLevel == Program.maxLevel)
                {
                    connection.Disconnect();
                    updateStatus("Summoner: " + sumName + " is already max level.", Accountname);
                    updateStatus("Log into new account.", Accountname);
                    Program.lognNewAccount();
                    return;
                }
                if (rpBalance == 400.0 && Program.buyBoost)
                {
                    updateStatus("Buying XP Boost", Accountname);
                    try
                    {
                        Task t = new Task(buyBoost);
                        t.Start();
                    }
                    catch (Exception exception)
                    {
                        updateStatus("Couldn't buy RP Boost.\n" + exception, Accountname);
                    }
                }
                if (sumLevel < 3.0 && queueType == QueueTypes.NORMAL_5x5)
                {
                    this.updateStatus("Need to be Level 3 before NORMAL_5x5 queue.", Accountname);
                    this.updateStatus("Joins Co-Op vs AI (Beginner) queue until 3", Accountname);
                    queueType = QueueTypes.BEGINNER_BOT;
                    actualQueueType = QueueTypes.NORMAL_5x5;
                } else if (sumLevel < 6.0 && queueType == QueueTypes.ARAM)
                {
                    this.updateStatus("Need to be Level 6 before ARAM queue.", Accountname);
                    this.updateStatus("Joins Co-Op vs AI (Beginner) queue until 6", Accountname);
                    queueType = QueueTypes.BEGINNER_BOT;
                    actualQueueType = QueueTypes.ARAM;
                } else if (sumLevel < 7.0 && queueType == QueueTypes.NORMAL_3x3)
                {
                    this.updateStatus("Need to be Level 7 before NORMAL_3x3 queue.", Accountname);
                    this.updateStatus("Joins Co-Op vs AI (Beginner) queue until 7", Accountname);
                    queueType = QueueTypes.BEGINNER_BOT;
                    actualQueueType = QueueTypes.NORMAL_3x3;
                }
                /* Should be randomize the summonericon on every login,
                 * but only works with extra icons, so it crashes if you only got the standards.
                double[] ids = new double[Convert.ToInt32(sumId)];
                string icons = await connection.GetSummonerIcons(ids);
                List<int> availableIcons = new List<int> { };
                var random = new Random();
                foreach (var id in icons)
                {
                    availableIcons.Add(Convert.ToInt32(id));
                    Console.WriteLine("[DEBUG]: Added Icon: " + id);
                }
                int index = random.Next(availableIcons.Count);
                Console.WriteLine(" | Random Icon: " + index);
                int randomIcon = availableIcons[index];
                Console.WriteLine(" | Choose from List: " + randomIcon);
                await connection.UpdateProfileIconId(randomIcon);*/
                updateStatus("Logged in as " + loginPacket.AllSummonerData.Summoner.Name + " @ level " + loginPacket.AllSummonerData.SummonerLevel.Level, Accountname);
                availableChampsArray = await connection.GetAvailableChampions();
                LoLLauncher.RiotObjects.Team.Dto.PlayerDTO player = await connection.CreatePlayer();
                if (this.loginPacket.ReconnectInfo != null && this.loginPacket.ReconnectInfo.Game != null)
                {
                    this.connection_OnMessageReceived(sender, (object)this.loginPacket.ReconnectInfo.PlayerCredentials);
                }
                else
                    this.connection_OnMessageReceived(sender, (object)new EndOfGameStats());
            })).Start();
        }
        
        private void connection_OnError(object sender, LoLLauncher.Error error)
        {
            if (error.Message.Contains("is not owned by summoner"))
            {
                return;
            }
            else if (error.Message.Contains("Your summoner level is too low to select the spell"))
            {
                var random = new Random();
                var spellList = new List<int> { 13, 6, 7, 10, 1, 11, 21, 12, 3, 14, 2, 4 };

                int index = random.Next(spellList.Count);
                int index2 = random.Next(spellList.Count);

                int randomSpell1 = spellList[index];
                int randomSpell2 = spellList[index2];

                if (randomSpell1 == randomSpell2)
                {
                    int index3 = random.Next(spellList.Count);
                    randomSpell2 = spellList[index3];
                }

                int Spell1 = Convert.ToInt32(randomSpell1);
                int Spell2 = Convert.ToInt32(randomSpell2);
                return;
            }
            this.updateStatus("error received:\n" + error.Message, Accountname);
        }
        
        private void connection_OnDisconnect(object sender, EventArgs e)
        {
            Program.connectedAccs -= 1;
            Console.Title = " Current Connected: " + Program.connectedAccs;
            this.updateStatus("Disconnected", Accountname);
        }
       
        private void connection_OnConnect(object sender, EventArgs e)
        {
            Program.connectedAccs += 1;
            Console.Title = " Current Connected: " + Program.connectedAccs;
        }
 
        public void levelUp()
        {
            updateStatus("Level Up: " + sumLevel, Accountname);
            rpBalance = loginPacket.RpBalance;
            if (sumLevel >= Program.maxLevel)
            {
                connection.Disconnect();
                //bool connectStatus = await connection.IsConnected();
                if (!connection.IsConnected()) {
                Program.lognNewAccount(); 
                }
            }
            if (rpBalance == 400.0 && Program.buyBoost)
            {
                updateStatus("Buying XP Boost", Accountname);
                try
                {
                    Task t = new Task(buyBoost);
                    t.Start();
                }
                catch (Exception exception)
                {
                    updateStatus("Couldn't buy RP Boost.\n" + exception, Accountname);
                }
            }
        }
        async void buyBoost()
        {
            try
            {
                if (region == "EUW")
                {
                    string url = await connection.GetStoreUrl();
                    HttpClient httpClient = new HttpClient();
                    Console.WriteLine(url);
                    await httpClient.GetStringAsync(url);
                    string storeURL = "https://store." + region.ToLower() + "1.lol.riotgames.com/store/tabs/view/boosts/1";
                    await httpClient.GetStringAsync(storeURL);
                    string purchaseURL = "https://store." + region.ToLower() + "1.lol.riotgames.com/store/purchase/item";
                    List<KeyValuePair<string, string>> storeItemList = new List<KeyValuePair<string, string>>();
                    storeItemList.Add(new KeyValuePair<string, string>("item_id", "boosts_2"));
                    storeItemList.Add(new KeyValuePair<string, string>("currency_type", "rp"));
                    storeItemList.Add(new KeyValuePair<string, string>("quantity", "1"));
                    storeItemList.Add(new KeyValuePair<string, string>("rp", "260"));
                    storeItemList.Add(new KeyValuePair<string, string>("ip", "null"));
                    storeItemList.Add(new KeyValuePair<string, string>("duration_type", "PURCHASED"));
                    storeItemList.Add(new KeyValuePair<string, string>("duration", "3"));
                    HttpContent httpContent = new FormUrlEncodedContent(storeItemList);
                    await httpClient.PostAsync(purchaseURL, httpContent);
                    updateStatus("Bought 'XP Boost: 3 Days'!", Accountname);
                    httpClient.Dispose();
                }
                else if (region == "EUNE")
                {
                    string url = await connection.GetStoreUrl();
                    HttpClient httpClient = new HttpClient();
                    Console.WriteLine(url);
                    await httpClient.GetStringAsync(url);
                    string storeURL = "https://store." + region.Substring(0,3).ToLower() + "1.lol.riotgames.com/store/tabs/view/boosts/1";
                    await httpClient.GetStringAsync(storeURL);
                    string purchaseURL = "https://store." + region.Substring(0,3).ToLower() + "1.lol.riotgames.com/store/purchase/item";
                    List<KeyValuePair<string, string>> storeItemList = new List<KeyValuePair<string, string>>();
                    storeItemList.Add(new KeyValuePair<string, string>("item_id", "boosts_2"));
                    storeItemList.Add(new KeyValuePair<string, string>("currency_type", "rp"));
                    storeItemList.Add(new KeyValuePair<string, string>("quantity", "1"));
                    storeItemList.Add(new KeyValuePair<string, string>("rp", "260"));
                    storeItemList.Add(new KeyValuePair<string, string>("ip", "null"));
                    storeItemList.Add(new KeyValuePair<string, string>("duration_type", "PURCHASED"));
                    storeItemList.Add(new KeyValuePair<string, string>("duration", "3"));
                    HttpContent httpContent = new FormUrlEncodedContent(storeItemList);
                    await httpClient.PostAsync(purchaseURL, httpContent);
                    updateStatus("Bought 'XP Boost: 3 Days'!", Accountname);
                    httpClient.Dispose();
                }
                else if (region == "NA")
                {
                    string url = await connection.GetStoreUrl();
                    HttpClient httpClient = new HttpClient();
                    Console.WriteLine(url);
                    await httpClient.GetStringAsync(url);
                    string storeURL = "https://store." + region.ToLower() + "2.lol.riotgames.com/store/tabs/view/boosts/1";
                    await httpClient.GetStringAsync(storeURL);
                    string purchaseURL = "https://store." + region.ToLower() + "2.lol.riotgames.com/store/purchase/item";
                    List<KeyValuePair<string, string>> storeItemList = new List<KeyValuePair<string, string>>();
                    storeItemList.Add(new KeyValuePair<string, string>("item_id", "boosts_2"));
                    storeItemList.Add(new KeyValuePair<string, string>("currency_type", "rp"));
                    storeItemList.Add(new KeyValuePair<string, string>("quantity", "1"));
                    storeItemList.Add(new KeyValuePair<string, string>("rp", "260"));
                    storeItemList.Add(new KeyValuePair<string, string>("ip", "null"));
                    storeItemList.Add(new KeyValuePair<string, string>("duration_type", "PURCHASED"));
                    storeItemList.Add(new KeyValuePair<string, string>("duration", "3"));
                    HttpContent httpContent = new FormUrlEncodedContent(storeItemList);
                    await httpClient.PostAsync(purchaseURL, httpContent);
                    updateStatus("Bought 'XP Boost: 3 Days'!", Accountname);
                    httpClient.Dispose();
                }
                else
                {
                    string url = await connection.GetStoreUrl();
                    HttpClient httpClient = new HttpClient();
                    Console.WriteLine(url);
                    await httpClient.GetStringAsync(url);
                    string storeURL = "https://store." + region.ToLower() + ".lol.riotgames.com/store/tabs/view/boosts/1";
                    await httpClient.GetStringAsync(storeURL);
                    string purchaseURL = "https://store." + region.ToLower() + ".lol.riotgames.com/store/purchase/item";
                    List<KeyValuePair<string, string>> storeItemList = new List<KeyValuePair<string, string>>();
                    storeItemList.Add(new KeyValuePair<string, string>("item_id", "boosts_2"));
                    storeItemList.Add(new KeyValuePair<string, string>("currency_type", "rp"));
                    storeItemList.Add(new KeyValuePair<string, string>("quantity", "1"));
                    storeItemList.Add(new KeyValuePair<string, string>("rp", "260"));
                    storeItemList.Add(new KeyValuePair<string, string>("ip", "null"));
                    storeItemList.Add(new KeyValuePair<string, string>("duration_type", "PURCHASED"));
                    storeItemList.Add(new KeyValuePair<string, string>("duration", "3"));
                    HttpContent httpContent = new FormUrlEncodedContent(storeItemList);
                    await httpClient.PostAsync(purchaseURL, httpContent);
                    updateStatus("Bought 'XP Boost: 3 Days'!", Accountname);
                    httpClient.Dispose();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }

    public static class EnumerableExtensions
    {
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
        {
            return source.Shuffle(new Random());
        }

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source, Random rng)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (rng == null) throw new ArgumentNullException("rng");

            return source.ShuffleIterator(rng);
        }

        private static IEnumerable<T> ShuffleIterator<T>(
            this IEnumerable<T> source, Random rng)
        {
            List<T> buffer = source.ToList();
            for (int i = 0; i < buffer.Count; i++)
            {
                int j = rng.Next(i, buffer.Count);
                yield return buffer[j];

                buffer[j] = buffer[i];
            }
        }
    }
}
