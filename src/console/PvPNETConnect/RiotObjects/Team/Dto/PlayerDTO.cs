#region

using System;
using System.Collections.Generic;

#endregion

namespace LoLLauncher.RiotObjects.Team.Dto
{
    public class PlayerDto : RiotGamesObject
    {
        public delegate void Callback(PlayerDto result);

        private readonly Callback _callback;
        private readonly string _type = "com.riotgames.team.dto.PlayerDTO";

        public PlayerDto()
        {
        }

        public PlayerDto(Callback callback)
        {
            this._callback = callback;
        }

        public PlayerDto(TypedObject result)
        {
            SetFields(this, result);
        }

        public override string TypeName
        {
            get { return _type; }
        }

        [InternalName("playerId")]
        public Double PlayerId { get; set; }

        [InternalName("teamsSummary")]
        public List<object> TeamsSummary { get; set; }

        [InternalName("createdTeams")]
        public List<object> CreatedTeams { get; set; }

        [InternalName("playerTeams")]
        public List<object> PlayerTeams { get; set; }

        public override void DoCallback(TypedObject result)
        {
            SetFields(this, result);
            _callback(this);
        }
    }
}