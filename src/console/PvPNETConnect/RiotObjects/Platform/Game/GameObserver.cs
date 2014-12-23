#region

using System;

#endregion

namespace LoLLauncher.RiotObjects.Platform.Game
{
    public class GameObserver : RiotGamesObject
    {
        public delegate void Callback(GameObserver result);

        private readonly Callback _callback;
        private readonly string _type = "com.riotgames.platform.game.GameObserver";

        public GameObserver()
        {
        }

        public GameObserver(Callback callback)
        {
            this._callback = callback;
        }

        public GameObserver(TypedObject result)
        {
            SetFields(this, result);
        }

        public override string TypeName
        {
            get { return _type; }
        }

        [InternalName("accountId")]
        public Double AccountId { get; set; }

        [InternalName("botDifficulty")]
        public String BotDifficulty { get; set; }

        [InternalName("summonerInternalName")]
        public String SummonerInternalName { get; set; }

        [InternalName("locale")]
        public object Locale { get; set; }

        [InternalName("lastSelectedSkinIndex")]
        public Int32 LastSelectedSkinIndex { get; set; }

        [InternalName("partnerId")]
        public String PartnerId { get; set; }

        [InternalName("profileIconId")]
        public Int32 ProfileIconId { get; set; }

        [InternalName("summonerId")]
        public Double SummonerId { get; set; }

        [InternalName("badges")]
        public Int32 Badges { get; set; }

        [InternalName("pickTurn")]
        public Int32 PickTurn { get; set; }

        [InternalName("originalAccountId")]
        public Double OriginalAccountId { get; set; }

        [InternalName("summonerName")]
        public String SummonerName { get; set; }

        [InternalName("pickMode")]
        public Int32 PickMode { get; set; }

        [InternalName("originalPlatformId")]
        public String OriginalPlatformId { get; set; }

        public override void DoCallback(TypedObject result)
        {
            SetFields(this, result);
            _callback(this);
        }
    }
}