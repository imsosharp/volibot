#region

using System;

#endregion

namespace LoLLauncher.RiotObjects.Platform.Statistics
{
    public class AggregatedStat : RiotGamesObject
    {
        public delegate void Callback(AggregatedStat result);

        private readonly Callback _callback;
        private readonly string _type = "com.riotgames.platform.statistics.AggregatedStat";

        public AggregatedStat()
        {
        }

        public AggregatedStat(Callback callback)
        {
            this._callback = callback;
        }

        public AggregatedStat(TypedObject result)
        {
            SetFields(this, result);
        }

        public override string TypeName
        {
            get { return _type; }
        }

        [InternalName("statType")]
        public String StatType { get; set; }

        [InternalName("count")]
        public Double Count { get; set; }

        [InternalName("value")]
        public Double Value { get; set; }

        [InternalName("championId")]
        public Int32 ChampionId { get; set; }

        public override void DoCallback(TypedObject result)
        {
            SetFields(this, result);
            _callback(this);
        }
    }
}