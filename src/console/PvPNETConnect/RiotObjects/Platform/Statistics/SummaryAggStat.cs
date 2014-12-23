#region

using System;

#endregion

namespace LoLLauncher.RiotObjects.Platform.Statistics
{
    public class SummaryAggStat : RiotGamesObject
    {
        public delegate void Callback(SummaryAggStat result);

        private readonly Callback _callback;
        private readonly string _type = "com.riotgames.platform.statistics.SummaryAggStat";

        public SummaryAggStat(Callback callback)
        {
            this._callback = callback;
        }

        public SummaryAggStat(TypedObject result)
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

        public override void DoCallback(TypedObject result)
        {
            SetFields(this, result);
            _callback(this);
        }
    }
}