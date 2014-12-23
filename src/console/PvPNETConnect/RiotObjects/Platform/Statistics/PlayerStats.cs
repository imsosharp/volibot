#region

using System;
using System.Collections.Generic;

#endregion

namespace LoLLauncher.RiotObjects.Platform.Statistics
{
    public class PlayerStats : RiotGamesObject
    {
        public delegate void Callback(PlayerStats result);

        private readonly Callback _callback;
        private readonly string _type = "com.riotgames.platform.statistics.PlayerStats";

        public PlayerStats()
        {
        }

        public PlayerStats(Callback callback)
        {
            this._callback = callback;
        }

        public PlayerStats(TypedObject result)
        {
            SetFields(this, result);
        }

        public override string TypeName
        {
            get { return _type; }
        }

        [InternalName("timeTrackedStats")]
        public List<TimeTrackedStat> TimeTrackedStats { get; set; }

        [InternalName("promoGamesPlayed")]
        public Int32 PromoGamesPlayed { get; set; }

        [InternalName("promoGamesPlayedLastUpdated")]
        public object PromoGamesPlayedLastUpdated { get; set; }

        public override void DoCallback(TypedObject result)
        {
            SetFields(this, result);
            _callback(this);
        }
    }
}