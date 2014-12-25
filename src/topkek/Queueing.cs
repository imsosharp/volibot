/*
 * Topkek GUI is part of the opensource VoliBot AutoQueuer project.
 * Credits to: shalzuth, Maufeat, imsosharp
 * Find assemblies for this AutoQueuer on LeagueSharp's official forum at:
 * http://www.joduska.me/
 * You are allowed to copy, edit and distribute this project,
 * as long as you don't touch this notice and you release your project with source.
 */﻿
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
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using LoLLauncher.RiotObjects.Platform.Game.Map;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;

namespace RitoBot.topkek
{

    internal class Queueing
    {
        public bool firstTimeInLobby = true;
        public bool firstTimeInQueuePop = true;
        public bool firstTimeInCustom = true;
        public Process exeProcess;
        public string ipath = @"C:\Riot Games\League of Legends\";
        public string Accountname;
        public string Password;
        public int threadID;
        public double archiveSumLevel { get; set; }
        public double rpBalance { get; set; }
        public QueueTypes queueType { get; set; }
        public QueueTypes actualQueueType { get; set; }
        public string champion { get; set; }
        public string spell1 { get; set; }
        public string spell2 { get; set; }

        public Queueing(string Champion, string Spell1, string Spell2, QueueTypes QueueType)
        {
            if (Champion != "")
            {
                champion = Champion;
            }
            else
            {
                champion = "RANDOM";
            }
            queueType = QueueType;
            Connection.lolConnection.OnError += new LoLConnection.OnErrorHandler(this.connection_OnError);
            Connection.lolConnection.OnLogin += new LoLConnection.OnLoginHandler(this.connection_OnLogin);
            Connection.lolConnection.OnMessageReceived += new LoLConnection.OnMessageReceivedHandler(this.connection_OnMessageReceived);
            Connection.lolConnection.Connect(Connection.login, Connection.password, Region.EUW, Connection.Version);


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
                            firstTimeInLobby = false;
                            updateStatus("In Champion Select", Accountname);
                            object obj = await Connection.lolConnection.SetClientReceivedGameMessage(game.Id, "CHAMP_SELECT_CLIENT");
                            if (queueType != QueueTypes.ARAM)
                            {
                                if (champion != "" && champion != "RANDOM")
                                {
                                    await Connection.lolConnection.SelectChampion(Enums.championToId(champion));
                                    await Connection.lolConnection.ChampionSelectCompleted();
                                }
                                else if (champion == "RANDOM")
                                {
                                    var randAvailableChampsArray = Connection.availableChampsArray.Shuffle();
                                    await Connection.lolConnection.SelectChampion(randAvailableChampsArray.First(champ => champ.Owned || champ.FreeToPlay).ChampionId);
                                    await Connection.lolConnection.ChampionSelectCompleted();

                                }
                                else
                                {
                                    await Connection.lolConnection.SelectChampion(Connection.availableChampsArray.First(champ => champ.Owned || champ.FreeToPlay).ChampionId);
                                    await Connection.lolConnection.ChampionSelectCompleted();

                                    int Spell1;
                                    int Spell2;
                                    Spell1 = Enums.spellToId(spell1);
                                    Spell2 = Enums.spellToId(spell2);
                                    await Connection.lolConnection.SelectSpells(Spell1, Spell2);
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
                            object obj = await Connection.lolConnection.AcceptPoppedGame(true);
                            break;
                        }
                        else
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

                    if (Connection.SummLvl == 3 && actualQueueType == QueueTypes.NORMAL_5x5)
                    {
                        queueType = actualQueueType;
                    }
                    else if (Connection.SummLvl == 6 && actualQueueType == QueueTypes.ARAM)
                    {
                        queueType = actualQueueType;
                    }
                    else if (Connection.SummLvl == 7 && actualQueueType == QueueTypes.NORMAL_3x3)
                    {
                        queueType = actualQueueType;
                    }

                    matchParams.QueueIds = new Int32[1] { (int)queueType };
                    LoLLauncher.RiotObjects.Platform.Matchmaking.SearchingForMatchNotification m = await Connection.lolConnection.AttachToQueue(matchParams);
                    if (m.PlayerJoinFailures == null)
                    {
                        updateStatus("In QUEUE", Accountname);
                    }
                    else
                    {
                        try
                        {
                            updateStatus("Couldn't enter Q - " + m.PlayerJoinFailures.Summoner.Name + " : " + m.PlayerJoinFailures.ReasonFailed, Accountname);
                        }
                        catch (Exception ex) { MessageBox.Show("NOT in queue - " + ex); Connection.lolConnection.Disconnect(); }
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
                        Connection.loginPacket = await Connection.lolConnection.GetLoginDataPacketForUser();
                        archiveSumLevel = Connection.SummLvl;
                        Connection.SummLvl = Connection.loginPacket.AllSummonerData.SummonerLevel.Level;
                        if (Connection.SummLvl != archiveSumLevel)
                        {
                            Connection.SummLvl += 1;
                        }
                    }
                }
            }
        }

        void exeProcess_Exited(object sender, EventArgs e)
        {
            updateStatus("Restart League of Legends.", Accountname);
            Thread.Sleep(1000);
            if (Connection.loginPacket.ReconnectInfo != null && Connection.loginPacket.ReconnectInfo.Game != null)
            {
                this.connection_OnMessageReceived(sender, (object)Connection.loginPacket.ReconnectInfo.PlayerCredentials);
            }
            else
                this.connection_OnMessageReceived(sender, (object)new EndOfGameStats());
        }



        private void connection_OnLogin(object sender, string username, string ipAddress)
        {
            new Thread((ThreadStart)(async () =>
            {
                Connection.loginPacket = await Connection.lolConnection.GetLoginDataPacketForUser();
                await Connection.lolConnection.Subscribe("bc", Connection.loginPacket.AllSummonerData.Summoner.AcctId);
                await Connection.lolConnection.Subscribe("cn", Connection.loginPacket.AllSummonerData.Summoner.AcctId);
                await Connection.lolConnection.Subscribe("gn", Connection.loginPacket.AllSummonerData.Summoner.AcctId);
                if (Connection.loginPacket.AllSummonerData == null)
                {
                    Random rnd = new Random();
                    String summonerName = Connection.SummName;
                    if (summonerName.Length > 16)
                        summonerName = summonerName.Substring(0, 12) + new Random().Next(1000, 9999).ToString();
                    LoLLauncher.RiotObjects.Platform.Summoner.AllSummonerData sumData = await Connection.lolConnection.CreateDefaultSummoner(summonerName);
                    Connection.loginPacket.AllSummonerData = sumData;
                    updateStatus("Created Summonername " + summonerName, Accountname);
                }

                if (Connection.SummLvl < 3.0 && queueType == QueueTypes.NORMAL_5x5)
                {
                    this.updateStatus("Need to be Level 3 before NORMAL_5x5 queue.", Accountname);
                    this.updateStatus("Joins Co-Op vs AI (Beginner) queue until 3", Accountname);
                    queueType = QueueTypes.BEGINNER_BOT;
                    actualQueueType = QueueTypes.NORMAL_5x5;
                }
                else if (Connection.SummLvl < 6.0 && queueType == QueueTypes.ARAM)
                {
                    this.updateStatus("Need to be Level 6 before ARAM queue.", Accountname);
                    this.updateStatus("Joins Co-Op vs AI (Beginner) queue until 6", Accountname);
                    queueType = QueueTypes.BEGINNER_BOT;
                    actualQueueType = QueueTypes.ARAM;
                }
                else if (Connection.SummLvl < 7.0 && queueType == QueueTypes.NORMAL_3x3)
                {
                    this.updateStatus("Need to be Level 7 before NORMAL_3x3 queue.", Accountname);
                    this.updateStatus("Joins Co-Op vs AI (Beginner) queue until 7", Accountname);
                    queueType = QueueTypes.BEGINNER_BOT;
                    actualQueueType = QueueTypes.NORMAL_3x3;
                }
                LoLLauncher.RiotObjects.Team.Dto.PlayerDTO player = await Connection.lolConnection.CreatePlayer();
                if (Connection.loginPacket.ReconnectInfo != null && Connection.loginPacket.ReconnectInfo.Game != null)
                {
                    this.connection_OnMessageReceived(sender, (object)Connection.loginPacket.ReconnectInfo.PlayerCredentials);
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
        }


        public void LaunchGame(PlayerCredentialsDto CurrentGame)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = false;
            startInfo.WorkingDirectory = FindLoLExe();
            startInfo.FileName = "League of Legends.exe";
            startInfo.Arguments = "\"8394\" \"LoLLauncher.exe\" \"\" \"" + CurrentGame.ServerIp + " " +
                CurrentGame.ServerPort + " " + CurrentGame.EncryptionKey + " " + CurrentGame.SummonerId + "\"";
            new Thread(() =>
            {
                exeProcess = Process.Start(startInfo);
                while (exeProcess.MainWindowHandle == IntPtr.Zero) { }
                Thread.Sleep(1000);
            }).Start();
        }

        private String FindLoLExe()
        {
            String installPath = ipath;
            if (installPath.Contains("notfound"))
                return installPath;
            installPath += @"RADS\solutions\lol_game_client_sln\releases\";
            installPath = Directory.EnumerateDirectories(installPath).OrderBy(f => new DirectoryInfo(f).CreationTime).Last();
            installPath += @"\deploy\";
            return installPath;
        }

        public Thread InjectThread { get; set; }

        private void updateStatus(string text, string acc)
        {

            KekHandler.changeStatus(string.Concat(new object[7]
              {
                (object) "[",
                (object) DateTime.Now,
                (object) "] ",        
                (object) "[",
                (object) acc,
                (object) "]: ",
                (object) text
              }));
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
