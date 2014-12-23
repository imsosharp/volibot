#region

using System;
using System.Collections.Generic;

#endregion

namespace LoLLauncher.RiotObjects.Platform.Statistics
{
    public class PlayerParticipantStatsSummary : RiotGamesObject
    {
        public delegate void Callback(PlayerParticipantStatsSummary result);

        private readonly Callback _callback;
        private readonly string _type = "com.riotgames.platform.statistics.PlayerParticipantStatsSummary";

        public PlayerParticipantStatsSummary()
        {
        }

        public PlayerParticipantStatsSummary(Callback callback)
        {
            this._callback = callback;
        }

        public PlayerParticipantStatsSummary(TypedObject result)
        {
            SetFields(this, result);
        }

        public override string TypeName
        {
            get { return _type; }
        }

        [InternalName("skinName")]
        public String SkinName { get; set; }

        [InternalName("gameId")]
        public Double GameId { get; set; }

        [InternalName("profileIconId")]
        public Int32 ProfileIconId { get; set; }

        [InternalName("elo")]
        public Int32 Elo { get; set; }

        [InternalName("leaver")]
        public Boolean Leaver { get; set; }

        [InternalName("leaves")]
        public Double Leaves { get; set; }

        [InternalName("teamId")]
        public Double TeamId { get; set; }

        [InternalName("eloChange")]
        public Int32 EloChange { get; set; }

        [InternalName("statistics")]
        public List<RawStatDto> Statistics { get; set; }

        [InternalName("level")]
        public Double Level { get; set; }

        [InternalName("botPlayer")]
        public Boolean BotPlayer { get; set; }

        [InternalName("userId")]
        public Double UserId { get; set; }

        [InternalName("spell2Id")]
        public Double Spell2Id { get; set; }

        [InternalName("losses")]
        public Double Losses { get; set; }

        [InternalName("summonerName")]
        public String SummonerName { get; set; }

        [InternalName("wins")]
        public Double Wins { get; set; }

        [InternalName("spell1Id")]
        public Double Spell1Id { get; set; }

        public override void DoCallback(TypedObject result)
        {
            SetFields(this, result);
            _callback(this);
        }
    }
}