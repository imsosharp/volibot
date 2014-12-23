#region

using System;

#endregion

namespace LoLLauncher.RiotObjects.Leagues.Pojo
{
    public class LeagueItemDto : RiotGamesObject
    {
        public delegate void Callback(LeagueItemDto result);

        private readonly Callback _callback;
        private readonly string _type = "com.riotgames.leagues.pojo.LeagueItemDTO";

        public LeagueItemDto()
        {
        }

        public LeagueItemDto(Callback callback)
        {
            this._callback = callback;
        }

        public LeagueItemDto(TypedObject result)
        {
            SetFields(this, result);
        }

        public override string TypeName
        {
            get { return _type; }
        }

        [InternalName("previousDayLeaguePosition")]
        public Int32 PreviousDayLeaguePosition { get; set; }

        [InternalName("timeLastDecayMessageShown")]
        public Double TimeLastDecayMessageShown { get; set; }

        [InternalName("hotStreak")]
        public Boolean HotStreak { get; set; }

        [InternalName("leagueName")]
        public String LeagueName { get; set; }

        [InternalName("miniSeries")]
        public object MiniSeries { get; set; }

        [InternalName("tier")]
        public String Tier { get; set; }

        [InternalName("freshBlood")]
        public Boolean FreshBlood { get; set; }

        [InternalName("lastPlayed")]
        public Double LastPlayed { get; set; }

        [InternalName("playerOrTeamId")]
        public String PlayerOrTeamId { get; set; }

        [InternalName("leaguePoints")]
        public Int32 LeaguePoints { get; set; }

        [InternalName("inactive")]
        public Boolean Inactive { get; set; }

        [InternalName("rank")]
        public String Rank { get; set; }

        [InternalName("veteran")]
        public Boolean Veteran { get; set; }

        [InternalName("queueType")]
        public String QueueType { get; set; }

        [InternalName("losses")]
        public Int32 Losses { get; set; }

        [InternalName("timeUntilDecay")]
        public Double TimeUntilDecay { get; set; }

        [InternalName("playerOrTeamName")]
        public String PlayerOrTeamName { get; set; }

        [InternalName("wins")]
        public Int32 Wins { get; set; }

        public override void DoCallback(TypedObject result)
        {
            SetFields(this, result);
            _callback(this);
        }
    }
}