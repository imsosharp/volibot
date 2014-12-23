#region

using System;

#endregion

namespace LoLLauncher.RiotObjects.Team
{
    public class TeamInfo : RiotGamesObject
    {
        public delegate void Callback(TeamInfo result);

        private readonly Callback _callback;
        private readonly string _type = "com.riotgames.team.TeamInfo";

        public TeamInfo()
        {
        }

        public TeamInfo(Callback callback)
        {
            this._callback = callback;
        }

        public TeamInfo(TypedObject result)
        {
            SetFields(this, result);
        }

        public override string TypeName
        {
            get { return _type; }
        }

        [InternalName("secondsUntilEligibleForDeletion")]
        public Double SecondsUntilEligibleForDeletion { get; set; }

        [InternalName("memberStatusString")]
        public String MemberStatusString { get; set; }

        [InternalName("tag")]
        public String Tag { get; set; }

        [InternalName("name")]
        public String Name { get; set; }

        [InternalName("memberStatus")]
        public String MemberStatus { get; set; }

        [InternalName("teamId")]
        public TeamId TeamId { get; set; }

        public override void DoCallback(TypedObject result)
        {
            SetFields(this, result);
            _callback(this);
        }
    }
}