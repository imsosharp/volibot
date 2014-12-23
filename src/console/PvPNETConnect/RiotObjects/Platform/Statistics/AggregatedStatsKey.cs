#region

using System;

#endregion

namespace LoLLauncher.RiotObjects.Platform.Statistics
{
    public class AggregatedStatsKey : RiotGamesObject
    {
        public delegate void Callback(AggregatedStatsKey result);

        private readonly Callback _callback;
        private readonly string _type = "com.riotgames.platform.statistics.AggregatedStatsKey";

        public AggregatedStatsKey()
        {
        }

        public AggregatedStatsKey(Callback callback)
        {
            this._callback = callback;
        }

        public AggregatedStatsKey(TypedObject result)
        {
            SetFields(this, result);
        }

        public override string TypeName
        {
            get { return _type; }
        }

        [InternalName("gameMode")]
        public String GameMode { get; set; }

        [InternalName("userId")]
        public Double UserId { get; set; }

        [InternalName("gameModeString")]
        public String GameModeString { get; set; }

        public override void DoCallback(TypedObject result)
        {
            SetFields(this, result);
            _callback(this);
        }
    }
}