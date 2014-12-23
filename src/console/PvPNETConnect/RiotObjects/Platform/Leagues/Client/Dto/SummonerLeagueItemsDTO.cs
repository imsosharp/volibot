#region

using System.Collections.Generic;
using LoLLauncher.RiotObjects.Leagues.Pojo;

#endregion

namespace LoLLauncher.RiotObjects.Platform.Leagues.Client.Dto
{
    public class SummonerLeagueItemsDto : RiotGamesObject
    {
        public delegate void Callback(SummonerLeagueItemsDto result);

        private readonly Callback _callback;
        private readonly string _type = "com.riotgames.platform.leagues.client.dto.SummonerLeagueItemsDTO";

        public SummonerLeagueItemsDto()
        {
        }

        public SummonerLeagueItemsDto(Callback callback)
        {
            this._callback = callback;
        }

        public SummonerLeagueItemsDto(TypedObject result)
        {
            SetFields(this, result);
        }

        public override string TypeName
        {
            get { return _type; }
        }

        [InternalName("summonerLeagues")]
        public List<LeagueItemDto> SummonerLeagues { get; set; }

        public override void DoCallback(TypedObject result)
        {
            SetFields(this, result);
            _callback(this);
        }
    }
}