#region

using System;
using System.Collections.Generic;

#endregion

namespace LoLLauncher.RiotObjects.Platform.Statistics
{
    public class RecentGames : RiotGamesObject
    {
        public delegate void Callback(RecentGames result);

        private readonly Callback _callback;
        private readonly string _type = "com.riotgames.platform.statistics.RecentGames";

        public RecentGames()
        {
        }

        public RecentGames(Callback callback)
        {
            this._callback = callback;
        }

        public RecentGames(TypedObject result)
        {
            SetFields(this, result);
        }

        public override string TypeName
        {
            get { return _type; }
        }

        [InternalName("recentGamesJson")]
        public object RecentGamesJson { get; set; }

        [InternalName("playerGameStatsMap")]
        public TypedObject PlayerGameStatsMap { get; set; }

        [InternalName("gameStatistics")]
        public List<PlayerGameStats> GameStatistics { get; set; }

        [InternalName("userId")]
        public Double UserId { get; set; }

        public override void DoCallback(TypedObject result)
        {
            SetFields(this, result);
            _callback(this);
        }
    }
}