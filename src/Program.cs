/*
 * Hello and welcome to the VoliBot AutoQueuer Project!
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
using Ini;
using System.Collections;
using System.IO;
using System.Threading;
using System.Net;
using System.Management;
using LoLLauncher;
using System.Windows.Forms;

namespace RitoBot
{
    public class Program
    {
        /*| VoliBot is an open source League of Legends Auto Queue Bot
         *| Thanks to: Maufeat, Fulcrum and shalzuth.
         *| Website: www.volibot.com
         */

        public static string Path2;
        public static string Region;
        public static ArrayList Accounts = new ArrayList();
        public static ArrayList Accounts2 = new ArrayList();
        public static int MaxBots = 1;
        public static bool ReplaceConfig;
        public static int ConnectedAccs = 0;
        public static string ChampionId = "";
        public static List<string> ChampionPickList = new List<string>();
        public static int MaxLevel = 31;
        public static bool BuyBoost;
        public static bool RndSpell = true;
        public static string Spell1 = "flash";
        public static string Spell2 = "ignite";
        public static string CVersion = "5.1.15_01_09_17_50";
        public static bool AutoUpdate;
        public static bool LoadGUI;
        public static frm_MainWindow MainWindow = new frm_MainWindow();

        static void Main(string[] args)
        {
            InitChecks();
            LoadVersion();
            Console.Title = @"Volibot";
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetWindowSize(Console.WindowWidth + 5, Console.WindowHeight);
            Console.WriteLine(@"=======================================");
            Console.WriteLine(@"Maufeat's Volibot up-to-date for v" + CVersion.Substring(0,3));
            Console.WriteLine(@"--------All features enabled!----------");
            Console.WriteLine(@"--------------imsosharp----------------");
            Console.WriteLine(@"=======================================");
            Console.WriteLine();
            Console.WriteLine(GetTimestamp() + @"Loading config\settings.ini");
            LoadConfiguration();
            if (ReplaceConfig)
            {
                Console.WriteLine(GetTimestamp() + @"Replacing Config");
                GameCfg();
            }
            while (!File.Exists(Path2 + "lol.launcher.exe"))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine();
                Console.WriteLine(@"Wrong LauncherPath. the path should look like this: C:\Riot Games\League of Legends\" );
                Console.WriteLine(@"Please check config\settings.ini, otherwise your LoL won't start.");
                Console.WriteLine();
                Thread.Sleep(5000);
                LoadConfiguration();
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(GetTimestamp() + @"Loading config\accounts.txt");
            ReloadAccounts:
            LoadAccounts();
            int curRunning = 0;
            if (LoadGUI) MainWindow.ShowDialog();
            if (!LoadGUI)
            {
                foreach (string acc in Accounts)
                {
                    try
                    {
                        Accounts2.RemoveAt(0);
                        string accs = acc;
                        string[] stringSeparators = new string[] { "|" };
                        var result = accs.Split(stringSeparators, StringSplitOptions.None);
                        curRunning += 1;
                        if (result[0].Contains("username"))
                        {
                            Console.WriteLine(@"Please add your accounts into config\accounts.txt");
                            goto ReloadAccounts;
                        }
                        if (result[2] != null)
                        {
                            QueueTypes queuetype = (QueueTypes)Enum.Parse(typeof(QueueTypes), result[2]);
                            RiotBot ritoBot = new RiotBot(result[0], result[1], Region, Path2, curRunning, queuetype);
                        }
                        else
                        {
                            QueueTypes queuetype = QueueTypes.ARAM;
                            RiotBot ritoBot = new RiotBot(result[0], result[1], Region, Path2, curRunning, queuetype);
                        }
                        Console.Title = @"RitoBot Console | Currently connected: " + ConnectedAccs;
                        if (curRunning == MaxBots)
                            break;
                    }
                    catch (Exception)
                    {
                        Console.WriteLine(@"CountAccError: You may have an issue in your accounts.txt");
                        Application.Exit();
                    }
                }
                Console.ReadKey();
            }
        }
        public static void LoadVersion()
        {

            var versiontxt = File.OpenText(AppDomain.CurrentDomain.BaseDirectory + @"config\\version.txt");
            CVersion = versiontxt.ReadLine();               
        }
        public static void LoginNewAccount()
        {
            Accounts2 = Accounts;
            Accounts.RemoveAt(0);
            int curRunning = 0;
            if (Accounts.Count == 0)
            {
                Console.WriteLine(GetTimestamp() + @"No more accounts to login.");
            }
            foreach (string acc in Accounts)
            {
                string accs = acc;
                string[] stringSeparators = new string[] { "|" };
                var result = accs.Split(stringSeparators, StringSplitOptions.None);
                curRunning += 1;
                if(result[2] != null)
                {
                    QueueTypes queuetype = (QueueTypes)Enum.Parse(typeof(QueueTypes), result[2]);
                    RiotBot ritoBot = new RiotBot(result[0], result[1], Region, Path2, curRunning, queuetype);
                } else {
                    QueueTypes queuetype = QueueTypes.ARAM;
                    RiotBot ritoBot = new RiotBot(result[0], result[1], Region, Path2, curRunning, queuetype);
                }
                Console.Title = @"RitoBot Console | Currently connected: " + ConnectedAccs;
                if (curRunning == MaxBots)
                    break;
            }
        }
        public static void LoadConfiguration()
        {
            try
            {
                IniFile iniFile = new IniFile(AppDomain.CurrentDomain.BaseDirectory + "config\\settings.ini");
                //General
                Path2 = iniFile.IniReadValue("General", "LauncherPath");
                LoadGUI = Convert.ToBoolean(iniFile.IniReadValue("General", "LoadGUI"));
                MaxBots = Convert.ToInt32(iniFile.IniReadValue("General", "MaxBots"));
                MaxLevel = Convert.ToInt32(iniFile.IniReadValue("General", "MaxLevel"));
                ChampionId = iniFile.IniReadValue("General", "ChampionPick").ToUpper();
                ChampionPickList = ChampionId.Split(',').ToList();
                Spell1 = iniFile.IniReadValue("General", "Spell1").ToUpper();
                Spell2 = iniFile.IniReadValue("General", "Spell2").ToUpper();
                RndSpell = Convert.ToBoolean(iniFile.IniReadValue("General", "RndSpell"));
                ReplaceConfig = Convert.ToBoolean(iniFile.IniReadValue("General", "ReplaceConfig"));
                AutoUpdate = Convert.ToBoolean(iniFile.IniReadValue("General", "AutoUpdate"));
                //Account
                Region = iniFile.IniReadValue("Account", "Region").ToUpper();
                BuyBoost = Convert.ToBoolean(iniFile.IniReadValue("Account", "BuyBoost"));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Thread.Sleep(10000);
                Application.Exit();
            }
        }
        public static void LoadAccounts()
        {
            var accountsTxtPath = AppDomain.CurrentDomain.BaseDirectory + "config\\accounts.txt";
            TextReader tr = File.OpenText(accountsTxtPath);
            string line;
            while ((line = tr.ReadLine()) != null)
            {
                Accounts.Add(line);
                Accounts2.Add(line);
            }
            tr.Close();
        }
        public static String GetTimestamp()
        {
           return "[" + DateTime.Now + "] ";
        }
        public static void GetColor(ConsoleColor color)
        {
           Console.ForegroundColor = color;
        }
        public static void GameCfg()
        {
            try
            {

                string path = Path2 + @"Config\\game.cfg";
                FileInfo fileInfo = new FileInfo(path) { IsReadOnly = false };
                fileInfo.Refresh();
                string str = "[General]\nGameMouseSpeed=9\nEnableAudio=0\nUserSetResolution=1\nBindSysKeys=0\nSnapCameraOnRespawn=1\nOSXMouseAcceleration=1\nAutoAcquireTarget=0\nEnableLightFx=0\nWindowMode=1\nShowTurretRangeIndicators=0\nPredictMovement=0\nWaitForVerticalSync=0\nColors=16\nHeight=200\nWidth=300\nSystemMouseSpeed=0\nCfgVersion=4.13.265\n\n[HUD]\nShowNeutralCamps=0\nDrawHealthBars=0\nAutoDisplayTarget=0\nMinimapMoveSelf=0\nItemShopPrevY=19\nItemShopPrevX=117\nShowAllChannelChat=0\nShowTimestamps=0\nObjectTooltips=0\nFlashScreenWhenDamaged=0\nNameTagDisplay=1\nShowChampionIndicator=0\nShowSummonerNames=0\nScrollSmoothingEnabled=0\nMiddleMouseScrollSpeed=0.5000\nMapScrollSpeed=0.5000\nShowAttackRadius=0\nNumericCooldownFormat=3\nSmartCastOnKeyRelease=0\nEnableLineMissileVis=0\nFlipMiniMap=0\nItemShopResizeHeight=47\nItemShopResizeWidth=455\nItemShopPrevResizeHeight=200\nItemShopPrevResizeWidth=300\nItemShopItemDisplayMode=1\nItemShopStartPane=1\n\n[Performance]\nShadowsEnabled=0\nEnableHUDAnimations=0\nPerPixelPointLighting=0\nEnableParticleOptimizations=0\nBudgetOverdrawAverage=10\nBudgetSkinnedVertexCount=10\nBudgetSkinnedDrawCallCount=10\nBudgetTextureUsage=10\nBudgetVertexCount=10\nBudgetTriangleCount=10\nBudgetDrawCallCount=1000\nEnableGrassSwaying=0\nEnableFXAA=0\nAdvancedShader=0\nFrameCapType=3\nGammaEnabled=1\nFull3DModeEnabled=0\nAutoPerformanceSettings=0\n=0\nEnvironmentQuality=0\nEffectsQuality=0\nShadowQuality=0\nGraphicsSlider=0\n\n[Volume]\nMasterVolume=1\nMusicMute=0\n\n[LossOfControl]\nShowSlows=0\n\n[ColorPalette]\nColorPalette=0\n\n[FloatingText]\nCountdown_Enabled=0\nEnemyTrueDamage_Enabled=0\nEnemyMagicalDamage_Enabled=0\nEnemyPhysicalDamage_Enabled=0\nTrueDamage_Enabled=0\nMagicalDamage_Enabled=0\nPhysicalDamage_Enabled=0\nScore_Enabled=0\nDisable_Enabled=0\nLevel_Enabled=0\nGold_Enabled=0\nDodge_Enabled=0\nHeal_Enabled=0\nSpecial_Enabled=0\nInvulnerable_Enabled=0\nDebug_Enabled=1\nAbsorbed_Enabled=1\nOMW_Enabled=1\nEnemyCritical_Enabled=0\nQuestComplete_Enabled=0\nQuestReceived_Enabled=0\nMagicCritical_Enabled=0\nCritical_Enabled=1\n\n[Replay]\nEnableHelpTip=0";
                StringBuilder builder = new StringBuilder();
                builder.AppendLine(str);
                using (StreamWriter writer = new StreamWriter(Path2 + @"Config\game.cfg"))
                {
                    writer.Write(builder.ToString());
                }
                fileInfo.IsReadOnly = true;
                fileInfo.Refresh();
            }
            catch (Exception exception2)
            {
                Console.WriteLine(@"game.cfg Error: If using VMWare Shared Folder, make sure it is not set to Read-Only. Exception:" + exception2.Message);
            }
        }
        private static void InitChecks()
        {
            var theFolder = AppDomain.CurrentDomain.BaseDirectory + @"config\\";
            var accountsTxtLocation = AppDomain.CurrentDomain.BaseDirectory + @"config\\accounts.txt";
            var configTxtLocation = AppDomain.CurrentDomain.BaseDirectory + @"config\\settings.ini";
            var versionTxtLocation = AppDomain.CurrentDomain.BaseDirectory + @"config\\version.txt";

            if (!Directory.Exists(theFolder))
            {
                Directory.CreateDirectory(theFolder);
            }

            if (!File.Exists(configTxtLocation))
            {
                
                var newfile = File.Create(configTxtLocation);
                newfile.Close();
                var content = "[General]\nLauncherPath=C:\\Riot Games\\League of Legends\\\nLoadGUI=false\nMaxBots=1\nMaxLevel=31\nChampionPick=Annie\nSpell1=Flash\nSpell2=Exhaust\nRndSpell=false\nReplaceConfig=false\nAutoUpdate=false\n\n[Account]\nRegion=EUW\nBuyBoost=false";
                var separator = new string[] { "\n" };
                string[] contentlines = content.Split(separator,StringSplitOptions.None);
                File.WriteAllLines(configTxtLocation, contentlines);
            }
            if (!File.Exists(versionTxtLocation))
            {
                var newfile = File.CreateText(versionTxtLocation);
                newfile.Close();
                string[] content = { CVersion };
                File.WriteAllLines(versionTxtLocation, content);
            }
            if (!File.Exists(accountsTxtLocation))
            {
                var newfile = File.CreateText(accountsTxtLocation);
                newfile.Close();
                string[] content = { "username|password|QueueType" };
                File.WriteAllLines(accountsTxtLocation, content);
            }
        }
   }
}
