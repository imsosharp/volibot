#region

using System;

#endregion

namespace LoLLauncher.RiotObjects.Team.Stats
{
    public class TeamStatDetail : RiotGamesObject
    {
        public delegate void Callback(TeamStatDetail result);

        private readonly Callback _callback;
        private readonly string _type = "com.riotgames.team.stats.TeamStatDetail";

        public TeamStatDetail()
        {
        }

        public TeamStatDetail(Callback callback)
        {
            this._callback = callback;
        }

        public TeamStatDetail(TypedObject result)
        {
            SetFields(this, result);
        }

        public override string TypeName
        {
            get { return _type; }
        }

        [InternalName("maxRating")]
        public Int32 MaxRating { get; set; }

        [InternalName("teamIdString")]
        public String TeamIdString { get; set; }

        [InternalName("seedRating")]
        public Int32 SeedRating { get; set; }

        [InternalName("losses")]
        public Int32 Losses { get; set; }

        [InternalName("rating")]
        public Int32 Rating { get; set; }

        [InternalName("teamStatTypeString")]
        public String TeamStatTypeString { get; set; }

        [InternalName("averageGamesPlayed")]
        public Int32 AverageGamesPlayed { get; set; }

        [InternalName("teamId")]
        public TeamId TeamId { get; set; }

        [InternalName("wins")]
        public Int32 Wins { get; set; }

        [InternalName("teamStatType")]
        public String TeamStatType { get; set; }

        public override void DoCallback(TypedObject result)
        {
            SetFields(this, result);
            _callback(this);
        }
    }
}