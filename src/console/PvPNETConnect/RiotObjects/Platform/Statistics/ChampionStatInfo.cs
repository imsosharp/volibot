#region

using System;
using System.Collections.Generic;

#endregion

namespace LoLLauncher.RiotObjects.Platform.Statistics
{
    public class ChampionStatInfo : RiotGamesObject
    {
        public delegate void Callback(ChampionStatInfo result);

        private readonly Callback _callback;
        private readonly string _type = "com.riotgames.platform.statistics.ChampionStatInfo";

        public ChampionStatInfo()
        {
        }

        public ChampionStatInfo(Callback callback)
        {
            this._callback = callback;
        }

        public ChampionStatInfo(TypedObject result)
        {
            SetFields(this, result);
        }

        public override string TypeName
        {
            get { return _type; }
        }

        [InternalName("totalGamesPlayed")]
        public Int32 TotalGamesPlayed { get; set; }

        [InternalName("accountId")]
        public Double AccountId { get; set; }

        [InternalName("stats")]
        public List<AggregatedStat> Stats { get; set; }

        [InternalName("championId")]
        public Double ChampionId { get; set; }

        public override void DoCallback(TypedObject result)
        {
            SetFields(this, result);
            _callback(this);
        }
    }
}