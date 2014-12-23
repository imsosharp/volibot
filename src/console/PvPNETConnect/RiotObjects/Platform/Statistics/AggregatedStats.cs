#region

using System;
using System.Collections.Generic;

#endregion

namespace LoLLauncher.RiotObjects.Platform.Statistics
{
    public class AggregatedStats : RiotGamesObject
    {
        public delegate void Callback(AggregatedStats result);

        private readonly Callback _callback;
        private readonly string _type = "com.riotgames.platform.statistics.AggregatedStats";

        public AggregatedStats()
        {
        }

        public AggregatedStats(Callback callback)
        {
            this._callback = callback;
        }

        public AggregatedStats(TypedObject result)
        {
            SetFields(this, result);
        }

        public override string TypeName
        {
            get { return _type; }
        }

        [InternalName("lifetimeStatistics")]
        public List<AggregatedStat> LifetimeStatistics { get; set; }

        [InternalName("modifyDate")]
        public object ModifyDate { get; set; }

        [InternalName("key")]
        public AggregatedStatsKey Key { get; set; }

        [InternalName("aggregatedStatsJson")]
        public String AggregatedStatsJson { get; set; }

        public override void DoCallback(TypedObject result)
        {
            SetFields(this, result);
            _callback(this);
        }
    }
}