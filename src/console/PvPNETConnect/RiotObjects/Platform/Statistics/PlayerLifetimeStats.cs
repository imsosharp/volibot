#region

using System;

#endregion

namespace LoLLauncher.RiotObjects.Platform.Statistics
{
    public class PlayerLifetimeStats : RiotGamesObject
    {
        public delegate void Callback(PlayerLifetimeStats result);

        private readonly Callback _callback;
        private readonly string _type = "com.riotgames.platform.statistics.PlayerLifetimeStats";

        public PlayerLifetimeStats()
        {
        }

        public PlayerLifetimeStats(Callback callback)
        {
            this._callback = callback;
        }

        public PlayerLifetimeStats(TypedObject result)
        {
            SetFields(this, result);
        }

        public override string TypeName
        {
            get { return _type; }
        }

        [InternalName("playerStatSummaries")]
        public PlayerStatSummaries PlayerStatSummaries { get; set; }

        [InternalName("leaverPenaltyStats")]
        public LeaverPenaltyStats LeaverPenaltyStats { get; set; }

        [InternalName("previousFirstWinOfDay")]
        public DateTime PreviousFirstWinOfDay { get; set; }

        [InternalName("userId")]
        public Double UserId { get; set; }

        [InternalName("dodgeStreak")]
        public Int32 DodgeStreak { get; set; }

        [InternalName("dodgePenaltyDate")]
        public object DodgePenaltyDate { get; set; }

        [InternalName("playerStatsJson")]
        public object PlayerStatsJson { get; set; }

        [InternalName("playerStats")]
        public PlayerStats PlayerStats { get; set; }

        public override void DoCallback(TypedObject result)
        {
            SetFields(this, result);
            _callback(this);
        }
    }
}