#region

using System;

#endregion

namespace LoLLauncher.RiotObjects.Team.Stats
{
    public class MatchHistorySummary : RiotGamesObject
    {
        public delegate void Callback(MatchHistorySummary result);

        private readonly Callback _callback;
        private readonly string _type = "com.riotgames.team.stats.MatchHistorySummary";

        public MatchHistorySummary()
        {
        }

        public MatchHistorySummary(Callback callback)
        {
            this._callback = callback;
        }

        public MatchHistorySummary(TypedObject result)
        {
            SetFields(this, result);
        }

        public override string TypeName
        {
            get { return _type; }
        }

        [InternalName("gameMode")]
        public String GameMode { get; set; }

        [InternalName("mapId")]
        public Int32 MapId { get; set; }

        [InternalName("assists")]
        public Int32 Assists { get; set; }

        [InternalName("opposingTeamName")]
        public String OpposingTeamName { get; set; }

        [InternalName("invalid")]
        public Boolean Invalid { get; set; }

        [InternalName("deaths")]
        public Int32 Deaths { get; set; }

        [InternalName("gameId")]
        public Double GameId { get; set; }

        [InternalName("kills")]
        public Int32 Kills { get; set; }

        [InternalName("win")]
        public Boolean Win { get; set; }

        [InternalName("date")]
        public Double Date { get; set; }

        [InternalName("opposingTeamKills")]
        public Int32 OpposingTeamKills { get; set; }

        public override void DoCallback(TypedObject result)
        {
            SetFields(this, result);
            _callback(this);
        }
    }
}