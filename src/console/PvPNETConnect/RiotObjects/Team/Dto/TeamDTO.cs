#region

using System;
using System.Collections.Generic;
using LoLLauncher.RiotObjects.Team.Stats;

#endregion

namespace LoLLauncher.RiotObjects.Team.Dto
{
    public class TeamDto : RiotGamesObject
    {
        public delegate void Callback(TeamDto result);

        private readonly Callback _callback;
        private readonly string _type = "com.riotgames.team.dto.TeamDTO";

        public TeamDto()
        {
        }

        public TeamDto(Callback callback)
        {
            this._callback = callback;
        }

        public TeamDto(TypedObject result)
        {
            SetFields(this, result);
        }

        public override string TypeName
        {
            get { return _type; }
        }

        [InternalName("teamStatSummary")]
        public TeamStatSummary TeamStatSummary { get; set; }

        [InternalName("status")]
        public String Status { get; set; }

        [InternalName("tag")]
        public String Tag { get; set; }

        [InternalName("roster")]
        public RosterDto Roster { get; set; }

        [InternalName("lastGameDate")]
        public object LastGameDate { get; set; }

        [InternalName("modifyDate")]
        public DateTime ModifyDate { get; set; }

        [InternalName("messageOfDay")]
        public object MessageOfDay { get; set; }

        [InternalName("teamId")]
        public TeamId TeamId { get; set; }

        [InternalName("lastJoinDate")]
        public DateTime LastJoinDate { get; set; }

        [InternalName("secondLastJoinDate")]
        public DateTime SecondLastJoinDate { get; set; }

        [InternalName("secondsUntilEligibleForDeletion")]
        public Double SecondsUntilEligibleForDeletion { get; set; }

        [InternalName("matchHistory")]
        public List<object> MatchHistory { get; set; }

        [InternalName("name")]
        public String Name { get; set; }

        [InternalName("thirdLastJoinDate")]
        public DateTime ThirdLastJoinDate { get; set; }

        [InternalName("createDate")]
        public DateTime CreateDate { get; set; }

        public override void DoCallback(TypedObject result)
        {
            SetFields(this, result);
            _callback(this);
        }
    }
}