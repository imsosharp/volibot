#region

using System;

#endregion

namespace LoLLauncher.RiotObjects.Platform.Statistics.Team
{
    public class TeamPlayerAggregatedStatsDto : RiotGamesObject
    {
        public delegate void Callback(TeamPlayerAggregatedStatsDto result);

        private readonly Callback _callback;
        private readonly string _type = "com.riotgames.platform.statistics.team.TeamPlayerAggregatedStatsDTO";

        public TeamPlayerAggregatedStatsDto()
        {
        }

        public TeamPlayerAggregatedStatsDto(Callback callback)
        {
            this._callback = callback;
        }

        public TeamPlayerAggregatedStatsDto(TypedObject result)
        {
            SetFields(this, result);
        }

        public override string TypeName
        {
            get { return _type; }
        }

        [InternalName("playerId")]
        public Double PlayerId { get; set; }

        [InternalName("aggregatedStats")]
        public AggregatedStats AggregatedStats { get; set; }

        public override void DoCallback(TypedObject result)
        {
            SetFields(this, result);
            _callback(this);
        }
    }
}