#region

using System;
using System.Collections.Generic;

#endregion

namespace LoLLauncher.RiotObjects.Team.Dto
{
    public class RosterDto : RiotGamesObject
    {
        public delegate void Callback(RosterDto result);

        private readonly Callback _callback;
        private readonly string _type = "com.riotgames.team.dto.RosterDTO";

        public RosterDto()
        {
        }

        public RosterDto(Callback callback)
        {
            this._callback = callback;
        }

        public RosterDto(TypedObject result)
        {
            SetFields(this, result);
        }

        public override string TypeName
        {
            get { return _type; }
        }

        [InternalName("ownerId")]
        public Double OwnerId { get; set; }

        [InternalName("memberList")]
        public List<TeamMemberInfoDto> MemberList { get; set; }

        public override void DoCallback(TypedObject result)
        {
            SetFields(this, result);
            _callback(this);
        }
    }
}