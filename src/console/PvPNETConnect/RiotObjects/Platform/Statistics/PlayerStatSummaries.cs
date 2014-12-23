#region

using System;
using System.Collections.Generic;

#endregion

namespace LoLLauncher.RiotObjects.Platform.Statistics
{
    public class PlayerStatSummaries : RiotGamesObject
    {
        public delegate void Callback(PlayerStatSummaries result);

        private readonly Callback _callback;
        private readonly string _type = "com.riotgames.platform.statistics.PlayerStatSummaries";

        public PlayerStatSummaries()
        {
        }

        public PlayerStatSummaries(Callback callback)
        {
            this._callback = callback;
        }

        public PlayerStatSummaries(TypedObject result)
        {
            SetFields(this, result);
        }

        public override string TypeName
        {
            get { return _type; }
        }

        [InternalName("playerStatSummarySet")]
        public List<PlayerStatSummary> PlayerStatSummarySet { get; set; }

        [InternalName("userId")]
        public Double UserId { get; set; }

        public override void DoCallback(TypedObject result)
        {
            SetFields(this, result);
            _callback(this);
        }
    }
}