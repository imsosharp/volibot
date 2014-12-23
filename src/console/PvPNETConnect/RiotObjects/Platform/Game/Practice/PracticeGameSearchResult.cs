#region

using System;

#endregion

namespace LoLLauncher.RiotObjects.Platform.Game.Practice
{
    public class PracticeGameSearchResult : RiotGamesObject
    {
        public delegate void Callback(PracticeGameSearchResult result);

        private readonly Callback _callback;
        private readonly string _type = "com.riotgames.platform.game.practice.PracticeGameSearchResult";

        public PracticeGameSearchResult()
        {
        }

        public PracticeGameSearchResult(Callback callback)
        {
            this._callback = callback;
        }

        public PracticeGameSearchResult(TypedObject result)
        {
            SetFields(this, result);
        }

        public override string TypeName
        {
            get { return _type; }
        }

        [InternalName("spectatorCount")]
        public Int32 SpectatorCount { get; set; }

        [InternalName("glmGameId")]
        public object GlmGameId { get; set; }

        [InternalName("glmHost")]
        public object GlmHost { get; set; }

        [InternalName("glmPort")]
        public Int32 GlmPort { get; set; }

        [InternalName("gameModeString")]
        public String GameModeString { get; set; }

        [InternalName("allowSpectators")]
        public String AllowSpectators { get; set; }

        [InternalName("gameMapId")]
        public Int32 GameMapId { get; set; }

        [InternalName("maxNumPlayers")]
        public Int32 MaxNumPlayers { get; set; }

        [InternalName("glmSecurePort")]
        public Int32 GlmSecurePort { get; set; }

        [InternalName("gameMode")]
        public String GameMode { get; set; }

        [InternalName("id")]
        public Double Id { get; set; }

        [InternalName("name")]
        public String Name { get; set; }

        [InternalName("privateGame")]
        public Boolean PrivateGame { get; set; }

        [InternalName("owner")]
        public PlayerParticipant Owner { get; set; }

        [InternalName("team1Count")]
        public Int32 Team1Count { get; set; }

        [InternalName("team2Count")]
        public Int32 Team2Count { get; set; }

        public override void DoCallback(TypedObject result)
        {
            SetFields(this, result);
            _callback(this);
        }
    }
}