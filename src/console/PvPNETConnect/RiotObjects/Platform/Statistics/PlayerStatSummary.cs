#region

using System;

#endregion

namespace LoLLauncher.RiotObjects.Platform.Statistics
{
    public class PlayerStatSummary : RiotGamesObject
    {
        public delegate void Callback(PlayerStatSummary result);

        private readonly Callback _callback;
        private readonly string _type = "com.riotgames.platform.statistics.PlayerStatSummary";

        public PlayerStatSummary()
        {
        }

        public PlayerStatSummary(Callback callback)
        {
            this._callback = callback;
        }

        public PlayerStatSummary(TypedObject result)
        {
            SetFields(this, result);
        }

        public override string TypeName
        {
            get { return _type; }
        }

        [InternalName("maxRating")]
        public Int32 MaxRating { get; set; }

        [InternalName("playerStatSummaryTypeString")]
        public String PlayerStatSummaryTypeString { get; set; }

        [InternalName("aggregatedStats")]
        public SummaryAggStats AggregatedStats { get; set; }

        [InternalName("modifyDate")]
        public DateTime ModifyDate { get; set; }

        [InternalName("leaves")]
        public Int32 Leaves { get; set; }

        [InternalName("playerStatSummaryType")]
        public String PlayerStatSummaryType { get; set; }

        [InternalName("userId")]
        public Double UserId { get; set; }

        [InternalName("losses")]
        public Int32 Losses { get; set; }

        [InternalName("rating")]
        public Int32 Rating { get; set; }

        [InternalName("aggregatedStatsJson")]
        public object AggregatedStatsJson { get; set; }

        [InternalName("wins")]
        public Int32 Wins { get; set; }

        public override void DoCallback(TypedObject result)
        {
            SetFields(this, result);
            _callback(this);
        }
    }
}