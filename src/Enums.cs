/*
 * Enums.cs is part of the opensource VoliBot AutoQueuer project.
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

namespace RitoBot
{
    class Enums
    {
        public static object[] champions = new object[] { 
            "AATROX", "AHRI", "AKALI", "ALISTAR", "AMUMU", "ANIVIA", "ANNIE", "ASHE", "AZIR", "BLITZCRANK", "BRAND", "BRAUM", "CAITLYN", "CASSIOPEIA", "CHOGATH", "CORKI",
"DARIUS", "DIANA", "MUNDO", "DRAVEN", "ELISE", "EVELYNN", "EZREAL", "FIDDLESTICKS", "FIORA", "FIZZ", "GALIO", "GANGPLANK", "GAREN", "GNAR", "GRAGAS", "GRAVES",
"HECARIM", "HEIMERDIGER", "IRELIA", "JANNA", "JARVAN", "JAX", "JAYCE", "JINX", "KALISTA", "KARMA", "KARTHUS", "KASSADIN", "KATARINA", "KAYLE", "KENNEN",
"KHAZIX", "KOGMAW", "LEBLANC", "LEESIN", "LEONA", "LISSANDRA", "LUCIAN", "LULU", "LUX", "MALPHITE", "MALZAHAR", "MAOKAI", "MASTERYI", "MISSFORTUNE",
"MORDEKAISER", "MORGANA", "NAMI", "NASUS", "NAUTILUS", "NIDALEE", "NOCTURNE", "NUNU", "OLAF", "ORIANNA", "PANTHEON", "POPPY", "QUINN", "REKSAI", "RAMMUS",
"RENEKTON", "RENGAR", "RIVEN", "RUMBLE", "RYZE", "SEJUANI", "SHACO", "SHEN", "SHYVANA", "SINGED", "SION", "SIVIR", "SKARNER", "SONA", "SORAKA", "SWAIN",
"SYNDRA", "TALON", "TARIC", "TEEMO", "THRESH", "TRISTANA", "TRUNDLE", "TRYNDAMERE", "TWISTEDFATE", "TWITCH", "UDYR", "URGOT", "VARUS", "VAYNE", "VEIGAR",
"VELKOZ", "VI", "VIKTOR", "VLADIMIR", "VOLIBEAR", "WARWICK", "WUKONG", "XERATH", "XINZHAO", "YASUO", "YORICK", "ZAC", "ZED", "ZIGGS", "ZILEAN", "ZYRA"
        };
        public static object[] queues = new object[] {
            "NORMAL_5x5", "NORMAL_3x3", "INTRO_BOT", "BEGINNER_BOT", "MEDIUM_BOT", "ARAM"
        };
        public static object[] regions = new object[] {
            "NA", "EUW", "EUNE", "OCE", "LAN", "LAS", "BR", "TR", "RU", "QQ"
        };
        public static object[] spells = new object[] {
            "BARRIER", "CLAIRVOYANCE", "CLARITY", "CLEANSE", "EXHAUST", "FLASH", "GARRISON", "GHOST", "HEAL", "IGNITE", "REVIVE", "SMITE", "TELEPORT"
        };
        public static int championToId(string name)
        {
            switch (name)
            {
                case "AATROX":
                    return 266;
                case "AHRI":
                    return 103;
                case "AKALI":
                    return 84;
                case "ALISTAR":
                    return 12;
                case "AMUMU":
                    return 32;
                case "ANIVIA":
                    return 34;
                case "ANNIE":
                    return 1;
                case "ASHE":
                    return 22;
                case "AZIR":
                    return 268;
                case "BLITZCRANK":
                    return 53;
                case "BRAND":
                    return 63;
                case "BRAUM":
                    return 201;
                case "CAITLYN":
                    return 51;
                case "CASSIOPEIA":
                    return 69;
                case "CHOGATH":
                    return 31;
                case "CORKI":
                    return 42;
                case "DARIUS":
                    return 122;
                case "DIANA":
                    return 131;
                case "MUNDO":
                    return 36;
                case "DRAVEN":
                    return 119;
                case "ELISE":
                    return 60;
                case "EVELYNN":
                    return 28;
                case "EZREAL":
                    return 81;
                case "FIDDLESTICKS":
                    return 9;
                case "FIORA":
                    return 114;
                case "FIZZ":
                    return 105;
                case "GALIO":
                    return 3;
                case "GANGPLANK":
                    return 41;
                case "GAREN":
                    return 86;
                case "GNAR":
                    return 150;
                case "GRAGAS":
                    return 79;
                case "GRAVES":
                    return 104;
                case "HECARIM":
                    return 120;
                case "HEIMERDIGER":
                    return 74;
                case "IRELIA":
                    return 39;
                case "JANNA":
                    return 40;
                case "JARVAN":
                    return 59;
                case "JAX":
                    return 24;
                case "JAYCE":
                    return 126;
                case "JINX":
                    return 222;
				case "KALISTA":
					return 429;
                case "KARMA":
                    return 43;
                case "KARTHUS":
                    return 30;
                case "KASSADIN":
                    return 38;
                case "KATARINA":
                    return 55;
                case "KAYLE":
                    return 10;
                case "KENNEN":
                    return 85;
                case "KHAZIX":
                    return 121;
                case "KOGMAW":
                    return 96;
                case "LEBLANC":
                    return 7;
                case "LEESIN":
                    return 64;
                case "LEONA":
                    return 89;
                case "LISSANDRA":
                    return 127;
                case "LUCIAN":
                    return 236;
                case "LULU":
                    return 117;
                case "LUX":
                    return 99;
                case "MALPHITE":
                    return 54;
                case "MALZAHAR":
                    return 90;
                case "MAOKAI":
                    return 57;
                case "MASTERYI":
                    return 11;
                case "MISSFORTUNE":
                    return 21;
                case "MORDEKAISER":
                    return 82;
                case "MORGANA":
                    return 25;
                case "NAMI":
                    return 267;
                case "NASUS":
                    return 75;
                case "NAUTILUS":
                    return 111;
                case "NIDALEE":
                    return 76;
                case "NOCTURNE":
                    return 56;
                case "NUNU":
                    return 20;
                case "OLAF":
                    return 2;
                case "ORIANNA":
                    return 61;
                case "PANTHEON":
                    return 80;
                case "POPPY":
                    return 78;
                case "QUINN":
                    return 133;
				case "REKSAI":
					return 421;
                case "RAMMUS":
                    return 33;
                case "RENEKTON":
                    return 58;
                case "RENGAR":
                    return 107;
                case "RIVEN":
                    return 92;
                case "RUMBLE":
                    return 68;
                case "RYZE":
                    return 13;
                case "SEJUANI":
                    return 113;
                case "SHACO":
                    return 35;
                case "SHEN":
                    return 98;
                case "SHYVANA":
                    return 102;
                case "SINGED":
                    return 27;
                case "SION":
                    return 14;
                case "SIVIR":
                    return 15;
                case "SKARNER":
                    return 72;
                case "SONA":
                    return 37;
                case "SORAKA":
                    return 16;
                case "SWAIN":
                    return 50;
                case "SYNDRA":
                    return 134;
                case "TALON":
                    return 91;
                case "TARIC":
                    return 44;
                case "TEEMO":
                    return 17;
                case "THRESH":
                    return 412;
                case "TRISTANA":
                    return 18;
                case "TRUNDLE":
                    return 48;
                case "TRYNDAMERE":
                    return 23;
                case "TWISTEDFATE":
                    return 4;
                case "TWITCH":
                    return 29;
                case "UDYR":
                    return 77;
                case "URGOT":
                    return 6;
                case "VARUS":
                    return 110;
                case "VAYNE":
                    return 67;
                case "VEIGAR":
                    return 45;
                case "VELKOZ":
                    return 161;
                case "VI":
                    return 254;
                case "VIKTOR":
                    return 112;
                case "VLADIMIR":
                    return 8;
                case "VOLIBEAR":
                    return 106;
                case "WARWICK":
                    return 19;
                case "WUKONG":
                    return 62;
                case "XERATH":
                    return 101;
                case "XINZHAO":
                    return 5;
                case "YASUO":
                    return 157;
                case "YORICK":
                    return 83;
                case "ZAC":
                    return 154;
                case "ZED":
                    return 238;
                case "ZIGGS":
                    return 115;
                case "ZILEAN":
                    return 26;
                case "ZYRA":
                    return 143;
                default:
                    return 0;
            }
        }

        public static int spellToId(string name)
        {
            switch (name)
            {
                case "BARRIER":
                    return 21;
                case "CLAIRVOYANCE":
                    return 2;
                case "CLARITY":
                    return 13;
                case "CLEANSE":
                    return 1;
                case "EXHAUST":
                    return 3;
                case "FLASH":
                    return 4;
                case "GARRISON":
                    return 17;
                case "GHOST":
                    return 6;
                case "HEAL":
                    return 7;
                case "IGNITE":
                    return 14;
                case "REVIVE":
                    return 10;
                case "SMITE":
                    return 11;
                case "TELEPORT":
                    return 12;
                default:
                    return 0;
            }
        }
    }
}
