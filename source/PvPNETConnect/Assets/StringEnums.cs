using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace LoLLauncher
{
    [Flags]
    public enum GuiState
    {
        None = 1,
        LoggedOut = 1 << 1,
        LoggingIn = 1 << 2,
        LoggedIn = 1 << 3,
        CustomSearchGame = 1 << 4,
        CustomCreateGame = 1 << 5,
        GameLobby = 1 << 6
    }
    [Flags]
    public enum GameLobbyGuiState
    {
        IDLE = 1,
        TEAM_SELECT = 1 << 1,
        CHAMP_SELECT = 1 << 2,
        POST_CHAMP_SELECT = 1 << 3,
        PRE_CHAMP_SELECT = 1 << 4,
        START_REQUESTED = 1 << 5,
        GAME_START_CLIENT = 1 << 6,
        GameClientConnectedToServer = 1 << 7,
        IN_PROGRESS = 1 << 8,
        IN_QUEUE = 1 << 9,
        POST_GAME = 1 << 10,
        TERMINATED = 1 << 11,
        TERMINATED_IN_ERROR = 1 << 12,
        CHAMP_SELECT_CLIENT = 1 << 13,
        GameReconnect = 1 << 14,
        GAME_IN_PROGRESS = 1 << 15,
        JOINING_CHAMP_SELECT = 1 << 16, 
        DISCONNECTED = 1 << 17
    }
    /// <summary>
    /// Game Modes enumerator.
    /// </summary>
    public enum GameMode
    {
        [StringValue("CLASSIC")]
        SummonersRift = 1,
        //[StringValue("ARAM")]
        //ProvingGrounds = 7,
        [StringValue("ODIN")]
        Dominion = 8,
        [StringValue("CLASSIC")]
        TwistedTreeline = 10,
        [StringValue("ARAM")]
        HowlingAbyss = 12,
        [StringValue("TUTORIAL")]
        Tutorial,
    }

    /// <summary>
    /// Seasons enumerator.
    /// </summary>
    public enum CompetitiveSeason
    {
        [StringValue("CURRENT")]
        Current,

        [StringValue("ONE")]
        One,

        [StringValue("TWO")]
        Two
    }

    /// <summary>
    /// Game types enumerator.
    /// </summary>
    public enum GameType
    {
        [StringValue("RANKED_TEAM_GAME")]
        RankedTeamGame,

        [StringValue("RANKED_GAME")]
        RankedGame,

        [StringValue("NORMAL_GAME")]
        NormalGame,

        [StringValue("CUSTOM_GAME")]
        CustomGame,

        [StringValue("TUTORIAL_GAME")]
        TutorialGame,

        [StringValue("PRACTICE_GAME")]
        PracticeGame,

        [StringValue("RANKED_GAME_SOLO")]
        RankedGameSolo,

        [StringValue("COOP_VS_AI")]
        CoopVsAi,

        [StringValue("RANKED_GAME_PREMADE")]
        RankedGamePremade
    }
    public enum CustomGameTypes
    {
        [StringValue("unknown")]
        Unknown1,
        [StringValue("Blind Pick")]
        BlindPick,
        [StringValue("Draft")]
        Draft,
        [StringValue("No Ban Draft")]
        NoBanDraft,
        [StringValue("AllRandom")]
        AllRandom,
        [StringValue("Tournament Draft")]
        TournamentDraft,
        [StringValue("Blind Draft")]
        BlindDraft,
        [StringValue("unknown")]
        Unknown2,
        [StringValue("unknown")]
        Unknown3,
        [StringValue("Tutorial")]
        Tutorial,
        [StringValue("Battle Training")]
        BattleTraining,
        [StringValue("Bugged Blind Pick")]
        BuggedBlindPick,
        [StringValue("Blind Random")]
        BlindRandom,
        [StringValue("Blind Duplicate")]
        BlindDuplicate
    }
    /// <summary>
    /// Queue types Enumeartor.
    /// </summary>
    /// 
    public class QueueTypes2
    {
        public Dictionary<String, Int32> dict = new Dictionary<String, Int32>()
        {
            {"NORMAL-5x5", 2},
            {"RANKED_SOLO-5x5", 4},
            {"BOT-5x5", 7},
            {"NORMAL-3x3", 8},
            {"NORMAL-5x5-draft", 14},
            {"ODIN-5x5", 16},
            {"ODIN-5x5-draft", 17},
            {"ODINBOT-5x5", 25},
            {"RANKED_TEAM-3x3", 41},
            {"RANKED_TEAM-5x5", 42},
            {"BOT_TT-3x3", 52},
            {"ARAM-5x5", 65}
        };
    }
    public enum QueueTypes
    {
        NORMAL_5x5 = 2,
        NORMAL_3x3 = 8,
        INTRO_BOT = 31,
        BEGINNER_BOT = 32,
        BEGGINNER_BOT = 32,
        BEGGINER_BOT = 32,
        MEDIUM_BOT = 33,
        ARAM = 65,
        CUSTOM = 999
    }

    public enum AllowSpectators
    {
        [StringValue("ALL")]
        All = 1,

        [StringValue("LOBBYONLY")]
        LobbyOnly = 2,

        [StringValue("DROPINONLY")]
        DropInOnly = 3,

        [StringValue("NONE")]
        None = 0
    }

    /// <summary>
    /// The StringEnum value with GetStringValue method
    /// </summary>
    public static class StringEnum
    {
        /// <summary>
        /// Gets the string value from Atrribute.
        /// </summary>
        /// <param name="value">Enum value.</param>
        /// <returns></returns>
        public static string GetStringValue(Enum value)
        {
            string output = null;
            Type type = value.GetType();

            //Check first in our cached results...

            //Look for our 'StringValueAttribute' 

            //in the field's custom attributes

            FieldInfo fi = type.GetField(value.ToString());
            StringValue[] attrs =
               fi.GetCustomAttributes(typeof(StringValue),
                                       false) as StringValue[];
            if (attrs.Length > 0)
            {
                output = attrs[0].Value;
            }

            return output;
        }
    }

    public class StringValue : System.Attribute
    {
        private string _value;

        public StringValue(string value)
        {
            _value = value;
        }

        public string Value
        {
            get { return _value; }
        }
    }
}
