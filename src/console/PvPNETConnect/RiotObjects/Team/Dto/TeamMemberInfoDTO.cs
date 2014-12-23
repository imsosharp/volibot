#region

using System;

#endregion

namespace LoLLauncher.RiotObjects.Team.Dto
{
    public class TeamMemberInfoDto : RiotGamesObject
    {
        public delegate void Callback(TeamMemberInfoDto result);

        private readonly Callback _callback;
        private readonly string _type = "com.riotgames.team.dto.TeamMemberInfoDTO";

        public TeamMemberInfoDto()
        {
        }

        public TeamMemberInfoDto(Callback callback)
        {
            this._callback = callback;
        }

        public TeamMemberInfoDto(TypedObject result)
        {
            SetFields(this, result);
        }

        public override string TypeName
        {
            get { return _type; }
        }

        [InternalName("joinDate")]
        public DateTime JoinDate { get; set; }

        [InternalName("playerName")]
        public String PlayerName { get; set; }

        [InternalName("inviteDate")]
        public DateTime InviteDate { get; set; }

        [InternalName("status")]
        public String Status { get; set; }

        [InternalName("playerId")]
        public Double PlayerId { get; set; }

        public override void DoCallback(TypedObject result)
        {
            SetFields(this, result);
            _callback(this);
        }
    }
}