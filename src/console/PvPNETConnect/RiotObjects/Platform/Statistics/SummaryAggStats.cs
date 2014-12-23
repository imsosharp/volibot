#region

using System.Collections.Generic;

#endregion

namespace LoLLauncher.RiotObjects.Platform.Statistics
{
    public class SummaryAggStats : RiotGamesObject
    {
        public delegate void Callback(SummaryAggStats result);

        private readonly Callback _callback;
        private readonly string _type = "com.riotgames.platform.statistics.SummaryAggStats";

        public SummaryAggStats()
        {
        }

        public SummaryAggStats(Callback callback)
        {
            this._callback = callback;
        }

        public SummaryAggStats(TypedObject result)
        {
            SetFields(this, result);
        }

        public override string TypeName
        {
            get { return _type; }
        }

        [InternalName("statsJson")]
        public object StatsJson { get; set; }

        [InternalName("stats")]
        public List<SummaryAggStat> Stats { get; set; }

        public override void DoCallback(TypedObject result)
        {
            SetFields(this, result);
            _callback(this);
        }
    }
}