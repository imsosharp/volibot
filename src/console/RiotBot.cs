#region

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using LoLLauncher;
using LoLLauncher.RiotObjects.Platform.Catalog.Champion;
using LoLLauncher.RiotObjects.Platform.Clientfacade.Domain;
using LoLLauncher.RiotObjects.Platform.Game;
using LoLLauncher.RiotObjects.Platform.Game.Message;
using LoLLauncher.RiotObjects.Platform.Matchmaking;
using LoLLauncher.RiotObjects.Platform.Statistics;

#endregion

namespace RitoBot
{
    internal class RiotBot
    {
        public string Accountname;
        public List<ChampionDto> AvailableChamps = new List<ChampionDto>();
        public ChampionDto[] AvailableChampsArray;
        public LoLConnection Connection = new LoLConnection();
        public GameDto CurrentGame = new GameDto();
        public Process ExeProcess;
        public bool FirstTimeInCustom = true;
        public bool FirstTimeInLobby = true;
        public bool FirstTimeInQueuePop = true;
        public string Ipath;
        public LoginDataPacket LoginPacket = new LoginDataPacket();
        public string Password;
        public string RegionUrl;
        public int ThreadId;

        public RiotBot(string username, string password, string reg, string path, int threadid, QueueTypes QueueType)
        {
            Ipath = path;
            Accountname = username;
            Password = password;
            ThreadId = threadid;
            this.QueueType = QueueType;
            Region = reg;
            Connection.OnConnect += connection_OnConnect;
            Connection.OnDisconnect += connection_OnDisconnect;
            Connection.OnError += connection_OnError;
            Connection.OnLogin += connection_OnLogin;
            Connection.OnLoginQueueUpdate += connection_OnLoginQueueUpdate;
            Connection.OnMessageReceived += connection_OnMessageReceived;
            switch (Region)
            {
                case "EUW":
                    Connection.Connect(username, password, LoLLauncher.Region.Euw, Program.Cversion);
                    break;
                case "EUNE":
                    Connection.Connect(username, password, LoLLauncher.Region.Eun, Program.Cversion);
                    break;
                case "NA":
                    Connection.Connect(username, password, LoLLauncher.Region.Na, Program.Cversion);
                    RegionUrl = "NA1";
                    break;
                case "KR":
                    Connection.Connect(username, password, LoLLauncher.Region.Kr, Program.Cversion);
                    break;
                case "BR":
                    Connection.Connect(username, password, LoLLauncher.Region.Br, Program.Cversion);
                    break;
                case "OCE":
                    Connection.Connect(username, password, LoLLauncher.Region.Oce, Program.Cversion);
                    break;
                case "RU":
                    Connection.Connect(username, password, LoLLauncher.Region.Ru, Program.Cversion);
                    break;
                case "TR":
                    Connection.Connect(username, password, LoLLauncher.Region.Tr, Program.Cversion);
                    break;
                case "LAS":
                    Connection.Connect(username, password, LoLLauncher.Region.Las, Program.Cversion);
                    break;
                case "LAN":
                    Connection.Connect(username, password, LoLLauncher.Region.Lan, Program.Cversion);
                    break;
            }
        }

        public double SumLevel { get; set; }
        public double ArchiveSumLevel { get; set; }
        public double RpBalance { get; set; }
        public QueueTypes QueueType { get; set; }
        public QueueTypes ActualQueueType { get; set; }
        public string Region { get; set; }

        public async void connection_OnMessageReceived(object sender, object message)
        {
            if (message is GameDto)
            {
                var game = message as GameDto;
                switch (game.GameState)
                {
                    case "CHAMP_SELECT":
                        if (FirstTimeInLobby)
                        {
                            FirstTimeInLobby = false;
                            UpdateStatus("In Champion Select", Accountname);
                            var obj = await Connection.SetClientReceivedGameMessage(game.Id, "CHAMP_SELECT_CLIENT");
                            if (QueueType != QueueTypes.Aram)
                            {
                                if (Program.ChampionId != "" && Program.ChampionId != "RANDOM")
                                {
                                    int spell1;
                                    int spell2;
                                    if (!Program.RndSpell)
                                    {
                                        spell1 = Enums.SpellToId(Program.Spell1);
                                        spell2 = Enums.SpellToId(Program.Spell2);
                                    }
                                    else
                                    {
                                        var random = new Random();
                                        var spellList = new List<int> {13, 6, 7, 10, 1, 11, 21, 12, 3, 14, 2, 4};

                                        var index = random.Next(spellList.Count);
                                        var index2 = random.Next(spellList.Count);

                                        var randomSpell1 = spellList[index];
                                        var randomSpell2 = spellList[index2];

                                        if (randomSpell1 == randomSpell2)
                                        {
                                            var index3 = random.Next(spellList.Count);
                                            randomSpell2 = spellList[index3];
                                        }

                                        spell1 = Convert.ToInt32(randomSpell1);
                                        spell2 = Convert.ToInt32(randomSpell2);
                                    }

                                    await Connection.SelectSpells(spell1, spell2);

                                    await Connection.SelectChampion(Enums.ChampionToId(Program.ChampionId));
                                    await Connection.ChampionSelectCompleted();
                                }
                                else if (Program.ChampionId == "RANDOM")
                                {
                                    int spell1;
                                    int spell2;
                                    if (!Program.RndSpell)
                                    {
                                        spell1 = Enums.SpellToId(Program.Spell1);
                                        spell2 = Enums.SpellToId(Program.Spell2);
                                    }
                                    else
                                    {
                                        var random = new Random();
                                        var spellList = new List<int> {13, 6, 7, 10, 1, 11, 21, 12, 3, 14, 2, 4};

                                        var index = random.Next(spellList.Count);
                                        var index2 = random.Next(spellList.Count);

                                        var randomSpell1 = spellList[index];
                                        var randomSpell2 = spellList[index2];

                                        if (randomSpell1 == randomSpell2)
                                        {
                                            var index3 = random.Next(spellList.Count);
                                            randomSpell2 = spellList[index3];
                                        }

                                        spell1 = Convert.ToInt32(randomSpell1);
                                        spell2 = Convert.ToInt32(randomSpell2);
                                    }

                                    await Connection.SelectSpells(spell1, spell2);

                                    var randAvailableChampsArray = AvailableChampsArray.Shuffle();
                                    await
                                        Connection.SelectChampion(
                                            randAvailableChampsArray.First(champ => champ.Owned || champ.FreeToPlay)
                                                .ChampionId);
                                    await Connection.ChampionSelectCompleted();
                                }
                                else
                                {
                                    int spell1;
                                    int spell2;
                                    if (!Program.RndSpell)
                                    {
                                        spell1 = Enums.SpellToId(Program.Spell1);
                                        spell2 = Enums.SpellToId(Program.Spell2);
                                    }
                                    else
                                    {
                                        var random = new Random();
                                        var spellList = new List<int> {13, 6, 7, 10, 1, 11, 21, 12, 3, 14, 2, 4};

                                        var index = random.Next(spellList.Count);
                                        var index2 = random.Next(spellList.Count);

                                        var randomSpell1 = spellList[index];
                                        var randomSpell2 = spellList[index2];

                                        if (randomSpell1 == randomSpell2)
                                        {
                                            var index3 = random.Next(spellList.Count);
                                            randomSpell2 = spellList[index3];
                                        }

                                        spell1 = Convert.ToInt32(randomSpell1);
                                        spell2 = Convert.ToInt32(randomSpell2);
                                    }

                                    await Connection.SelectSpells(spell1, spell2);

                                    await
                                        Connection.SelectChampion(
                                            AvailableChampsArray.First(champ => champ.Owned || champ.FreeToPlay)
                                                .ChampionId);
                                    await Connection.ChampionSelectCompleted();
                                }
                            }
                        }
                        break;
                    case "POST_CHAMP_SELECT":
                        FirstTimeInLobby = false;
                        UpdateStatus("(Post Champ Select)", Accountname);
                        break;
                    case "PRE_CHAMP_SELECT":
                        UpdateStatus("(Pre Champ Select)", Accountname);
                        break;
                    case "GAME_START_CLIENT":
                        UpdateStatus("Game client ran", Accountname);
                        break;
                    case "GameClientConnectedToServer":
                        UpdateStatus("Client connected to the server", Accountname);
                        break;
                    case "IN_QUEUE":
                        UpdateStatus("In Queue", Accountname);
                        break;
                    case "TERMINATED":
                        UpdateStatus("Re-entering queue", Accountname);
                        FirstTimeInQueuePop = true;
                        break;
                    case "JOINING_CHAMP_SELECT":
                        if (FirstTimeInQueuePop && game.StatusOfParticipants.Contains("1"))
                        {
                            UpdateStatus("Accepted Queue", Accountname);
                            FirstTimeInQueuePop = false;
                            FirstTimeInLobby = true;
                            var obj = await Connection.AcceptPoppedGame(true);
                        }
                        break;
                }
            }
            else if (message is PlayerCredentialsDto)
            {
                var str =
                    Directory.EnumerateDirectories((Ipath ?? "") + "RADS\\solutions\\lol_game_client_sln\\releases\\")
                        .OrderBy(f => new DirectoryInfo(f).CreationTime)
                        .Last() + "\\deploy\\";
                var credentials = message as PlayerCredentialsDto;
                var startInfo = new ProcessStartInfo();
                startInfo.CreateNoWindow = false;
                startInfo.WorkingDirectory = str;
                startInfo.FileName = "League of Legends.exe";
                startInfo.Arguments = "\"8394\" \"LoLLauncher.exe\" \"\" \"" + credentials.ServerIp + " " +
                                      credentials.ServerPort + " " + credentials.EncryptionKey + " " +
                                      credentials.SummonerId + "\"";
                UpdateStatus("Launching League of Legends", Accountname);
                new Thread(() =>
                {
                    ExeProcess = Process.Start(startInfo);
                    ExeProcess.Exited += exeProcess_Exited;
                    while (ExeProcess.MainWindowHandle == IntPtr.Zero) ;
                    ExeProcess.PriorityClass = ProcessPriorityClass.Idle;
                    ExeProcess.EnableRaisingEvents = true;
                }).Start();
            }
            else if (!(message is GameNotification) && !(message is SearchingForMatchNotification))
            {
                if (message is EndOfGameStats)
                {
                    var matchParams = new MatchMakerParams();
                    if (QueueType == QueueTypes.IntroBot)
                    {
                        matchParams.BotDifficulty = "INTRO";
                    }
                    else if (QueueType == QueueTypes.BeginnerBot)
                    {
                        matchParams.BotDifficulty = "EASY";
                    }
                    else if (QueueType == QueueTypes.MediumBot)
                    {
                        matchParams.BotDifficulty = "MEDIUM";
                    }

                    if (SumLevel == 3 && ActualQueueType == QueueTypes.Normal_5X5)
                    {
                        QueueType = ActualQueueType;
                    }
                    else if (SumLevel == 6 && ActualQueueType == QueueTypes.Aram)
                    {
                        QueueType = ActualQueueType;
                    }
                    else if (SumLevel == 7 && ActualQueueType == QueueTypes.Normal_3X3)
                    {
                        QueueType = ActualQueueType;
                    }

                    matchParams.QueueIds = new Int32[1] {(int) QueueType};
                    var m = await Connection.AttachToQueue(matchParams);
                    if (m.PlayerJoinFailures == null)
                    {
                        UpdateStatus("In Queue: " + QueueType, Accountname);
                    }
                    else
                    {
                        try
                        {
                            UpdateStatus(
                                "Couldn't enter Q - " + m.PlayerJoinFailures.Summoner.Name + " : " +
                                m.PlayerJoinFailures.ReasonFailed, Accountname);
                        }
                        catch (Exception)
                        {
                            Console.WriteLine(
                                "Something went wrong, couldn't enter queue. Check accounts.txt for correct queue type.");
                            Connection.Disconnect();
                        }
                    }
                }
                else
                {
                    if (message.ToString().Contains("EndOfGameStats"))
                    {
                        var eog = new EndOfGameStats();
                        connection_OnMessageReceived(sender, eog);
                        ExeProcess.Exited -= exeProcess_Exited;
                        ExeProcess.Kill();
                        LoginPacket = await Connection.GetLoginDataPacketForUser();
                        ArchiveSumLevel = SumLevel;
                        SumLevel = LoginPacket.AllSummonerData.SummonerLevel.Level;
                        if (SumLevel != ArchiveSumLevel)
                        {
                            LevelUp();
                        }
                    }
                }
            }
        }

        private void exeProcess_Exited(object sender, EventArgs e)
        {
            UpdateStatus("Restart League of Legends.", Accountname);
            Thread.Sleep(1000);
            if (LoginPacket.ReconnectInfo != null && LoginPacket.ReconnectInfo.Game != null)
            {
                connection_OnMessageReceived(sender, LoginPacket.ReconnectInfo.PlayerCredentials);
            }
            else
                connection_OnMessageReceived(sender, new EndOfGameStats());
        }

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool MoveWindow(IntPtr hWnd, int x, int y, int nWidth, int nHeight, bool bRepaint);

        private void UpdateStatus(string status, string accname)
        {
            if (Program.LoadGui)
                Program.MainWindow.Print(string.Concat(new object[4]
                {
                    "[",
                    accname,
                    "]: ",
                    status
                }));
            Console.WriteLine(string.Concat("[", DateTime.Now, "] ", "[", accname, "]: ", status));
        }

        private async void RegisterNotifications()
        {
            var obj1 = await Connection.Subscribe("bc", Connection.AccountId());
            var obj2 = await Connection.Subscribe("cn", Connection.AccountId());
            var obj3 = await Connection.Subscribe("gn", Connection.AccountId());
        }

        private void connection_OnLoginQueueUpdate(object sender, int positionInLine)
        {
            if (positionInLine <= 0)
                return;
            UpdateStatus("Position to login: " + positionInLine, Accountname);
        }

        private void connection_OnLogin(object sender, string username, string ipAddress)
        {
            new Thread(async () =>
            {
                UpdateStatus("Connecting...", Accountname);
                RegisterNotifications();
                LoginPacket = await Connection.GetLoginDataPacketForUser();
                if (LoginPacket.AllSummonerData == null)
                {
                    var rnd = new Random();
                    var summonerName = Accountname;
                    if (summonerName.Length > 16)
                        summonerName = summonerName.Substring(0, 12) + new Random().Next(1000, 9999);
                    var sumData = await Connection.CreateDefaultSummoner(summonerName);
                    LoginPacket.AllSummonerData = sumData;
                    UpdateStatus("Created Summonername " + summonerName, Accountname);
                }
                SumLevel = LoginPacket.AllSummonerData.SummonerLevel.Level;
                var sumName = LoginPacket.AllSummonerData.Summoner.Name;
                var sumId = LoginPacket.AllSummonerData.Summoner.SumId;
                RpBalance = LoginPacket.RpBalance;
                if (SumLevel > Program.MaxLevel || SumLevel == Program.MaxLevel)
                {
                    Connection.Disconnect();
                    UpdateStatus("Summoner: " + sumName + " is already max level.", Accountname);
                    UpdateStatus("Log into new account.", Accountname);
                    Program.LognNewAccount();
                    return;
                }
                if (RpBalance == 400.0 && Program.BuyBoost)
                {
                    UpdateStatus("Buying XP Boost", Accountname);
                    try
                    {
                        var t = new Task(BuyBoost);
                        t.Start();
                    }
                    catch (Exception exception)
                    {
                        UpdateStatus("Couldn't buy RP Boost.\n" + exception, Accountname);
                    }
                }
                if (SumLevel < 3.0 && QueueType == QueueTypes.Normal_5X5)
                {
                    UpdateStatus("Need to be Level 3 before NORMAL_5x5 queue.", Accountname);
                    UpdateStatus("Joins Co-Op vs AI (Beginner) queue until 3", Accountname);
                    QueueType = QueueTypes.BeginnerBot;
                    ActualQueueType = QueueTypes.Normal_5X5;
                }
                else if (SumLevel < 6.0 && QueueType == QueueTypes.Aram)
                {
                    UpdateStatus("Need to be Level 6 before ARAM queue.", Accountname);
                    UpdateStatus("Joins Co-Op vs AI (Beginner) queue until 6", Accountname);
                    QueueType = QueueTypes.BeginnerBot;
                    ActualQueueType = QueueTypes.Aram;
                }
                else if (SumLevel < 7.0 && QueueType == QueueTypes.Normal_3X3)
                {
                    UpdateStatus("Need to be Level 7 before NORMAL_3x3 queue.", Accountname);
                    UpdateStatus("Joins Co-Op vs AI (Beginner) queue until 7", Accountname);
                    QueueType = QueueTypes.BeginnerBot;
                    ActualQueueType = QueueTypes.Normal_3X3;
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
                UpdateStatus(
                    "Logged in as " + LoginPacket.AllSummonerData.Summoner.Name + " @ level " +
                    LoginPacket.AllSummonerData.SummonerLevel.Level, Accountname);
                AvailableChampsArray = await Connection.GetAvailableChampions();
                var player = await Connection.CreatePlayer();
                if (LoginPacket.ReconnectInfo != null && LoginPacket.ReconnectInfo.Game != null)
                {
                    connection_OnMessageReceived(sender, LoginPacket.ReconnectInfo.PlayerCredentials);
                }
                else
                    connection_OnMessageReceived(sender, new EndOfGameStats());
            }).Start();
        }

        private void connection_OnError(object sender, Error error)
        {
            if (error.Message.Contains("is not owned by summoner"))
            {
                return;
            }
            if (error.Message.Contains("Your summoner level is too low to select the spell"))
            {
                var random = new Random();
                var spellList = new List<int> {13, 6, 7, 10, 1, 11, 21, 12, 3, 14, 2, 4};

                var index = random.Next(spellList.Count);
                var index2 = random.Next(spellList.Count);

                var randomSpell1 = spellList[index];
                var randomSpell2 = spellList[index2];

                if (randomSpell1 == randomSpell2)
                {
                    var index3 = random.Next(spellList.Count);
                    randomSpell2 = spellList[index3];
                }

                var spell1 = Convert.ToInt32(randomSpell1);
                var spell2 = Convert.ToInt32(randomSpell2);
                return;
            }
            UpdateStatus("error received:\n" + error.Message, Accountname);
        }

        private void connection_OnDisconnect(object sender, EventArgs e)
        {
            Program.ConnectedAccs -= 1;
            Console.Title = " Current Connected: " + Program.ConnectedAccs;
            UpdateStatus("Disconnected", Accountname);
        }

        private void connection_OnConnect(object sender, EventArgs e)
        {
            Program.ConnectedAccs += 1;
            Console.Title = " Current Connected: " + Program.ConnectedAccs;
        }

        public void LevelUp()
        {
            UpdateStatus("Level Up: " + SumLevel, Accountname);
            RpBalance = LoginPacket.RpBalance;
            if (SumLevel >= Program.MaxLevel)
            {
                Connection.Disconnect();
                //bool connectStatus = await connection.IsConnected();
                if (!Connection.IsConnected())
                {
                    Program.LognNewAccount();
                }
            }
            if (RpBalance == 400.0 && Program.BuyBoost)
            {
                UpdateStatus("Buying XP Boost", Accountname);
                try
                {
                    var t = new Task(BuyBoost);
                    t.Start();
                }
                catch (Exception exception)
                {
                    UpdateStatus("Couldn't buy RP Boost.\n" + exception, Accountname);
                }
            }
        }

        private async void BuyBoost()
        {
            try
            {
                var url = await Connection.GetStoreUrl();
                var httpClient = new HttpClient();
                Console.WriteLine(url);
                await httpClient.GetStringAsync(url);

                var storeUrl = "https://store." + RegionUrl.ToLower() +
                               "1.lol.riotgames.com/store/tabs/view/boosts/1";
                await httpClient.GetStringAsync(storeUrl);

                var purchaseUrl = "https://store." + RegionUrl.ToLower() + "1.lol.riotgames.com/store/purchase/item";

                var storeItemList = new List<KeyValuePair<string, string>>();
                storeItemList.Add(new KeyValuePair<string, string>("item_id", "boosts_2"));
                storeItemList.Add(new KeyValuePair<string, string>("currency_type", "rp"));
                storeItemList.Add(new KeyValuePair<string, string>("quantity", "1"));
                storeItemList.Add(new KeyValuePair<string, string>("rp", "260"));
                storeItemList.Add(new KeyValuePair<string, string>("ip", "null"));
                storeItemList.Add(new KeyValuePair<string, string>("duration_type", "PURCHASED"));
                storeItemList.Add(new KeyValuePair<string, string>("duration", "3"));
                HttpContent httpContent = new FormUrlEncodedContent(storeItemList);
                await httpClient.PostAsync(purchaseUrl, httpContent);

                UpdateStatus("Bought 'XP Boost: 3 Days'!", Accountname);
                httpClient.Dispose();
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
            var buffer = source.ToList();
            for (var i = 0; i < buffer.Count; i++)
            {
                var j = rng.Next(i, buffer.Count);
                yield return buffer[j];

                buffer[j] = buffer[i];
            }
        }
    }
}