#region

using System;
using System.Collections.Generic;
using LoLLauncher.RiotObjects.Team;

#endregion

namespace LoLLauncher.RiotObjects.Platform.Statistics.Team
{
    public class TeamAggregatedStatsDto : RiotGamesObject
    {
        public delegate void Callback(TeamAggregatedStatsDto result);

        private readonly Callback _callback;
        private readonly string _type = "com.riotgames.platform.statistics.team.TeamAggregatedStatsDTO";

        public TeamAggregatedStatsDto()
        {
        }

        public TeamAggregatedStatsDto(Callback callback)
        {
            this._callback = callback;
        }

        public TeamAggregatedStatsDto(TypedObject result)
        {
            SetFields(this, result);
        }

        public override string TypeName
        {
            get { return _type; }
        }

        [InternalName("queueType")]
        public String QueueType { get; set; }

        [InternalName("serializedToJson")]
        public String SerializedToJson { get; set; }

        [InternalName("playerAggregatedStatsList")]
        public List<TeamPlayerAggregatedStatsDto> PlayerAggregatedStatsList { get; set; }

        [InternalName("teamId")]
        public TeamId TeamId { get; set; }

        public override void DoCallback(TypedObject result)
        {
            SetFields(this, result);
            _callback(this);
        }
    }
}