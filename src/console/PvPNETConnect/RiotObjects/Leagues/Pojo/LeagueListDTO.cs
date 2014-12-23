#region

using System;
using System.Collections.Generic;

#endregion

namespace LoLLauncher.RiotObjects.Leagues.Pojo
{
    public class LeagueListDto : RiotGamesObject
    {
        public delegate void Callback(LeagueListDto result);

        private readonly Callback _callback;
        private readonly string _type = "com.riotgames.leagues.pojo.LeagueListDTO";

        public LeagueListDto()
        {
        }

        public LeagueListDto(Callback callback)
        {
            this._callback = callback;
        }

        public LeagueListDto(TypedObject result)
        {
            SetFields(this, result);
        }

        public override string TypeName
        {
            get { return _type; }
        }

        [InternalName("queue")]
        public String Queue { get; set; }

        [InternalName("name")]
        public String Name { get; set; }

        [InternalName("tier")]
        public String Tier { get; set; }

        [InternalName("requestorsRank")]
        public String RequestorsRank { get; set; }

        [InternalName("entries")]
        public List<LeagueItemDto> Entries { get; set; }

        [InternalName("requestorsName")]
        public String RequestorsName { get; set; }

        public override void DoCallback(TypedObject result)
        {
            SetFields(this, result);
            _callback(this);
        }
    }
}