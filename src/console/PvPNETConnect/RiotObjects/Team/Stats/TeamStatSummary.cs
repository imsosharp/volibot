#region

using System;
using System.Collections.Generic;

#endregion

namespace LoLLauncher.RiotObjects.Team.Stats
{
    public class TeamStatSummary : RiotGamesObject
    {
        public delegate void Callback(TeamStatSummary result);

        private readonly Callback _callback;
        private readonly string _type = "com.riotgames.team.stats.TeamStatSummary";

        public TeamStatSummary()
        {
        }

        public TeamStatSummary(Callback callback)
        {
            this._callback = callback;
        }

        public TeamStatSummary(TypedObject result)
        {
            SetFields(this, result);
        }

        public override string TypeName
        {
            get { return _type; }
        }

        [InternalName("teamStatDetails")]
        public List<TeamStatDetail> TeamStatDetails { get; set; }

        [InternalName("teamIdString")]
        public String TeamIdString { get; set; }

        [InternalName("teamId")]
        public TeamId TeamId { get; set; }

        public override void DoCallback(TypedObject result)
        {
            SetFields(this, result);
            _callback(this);
        }
    }
}