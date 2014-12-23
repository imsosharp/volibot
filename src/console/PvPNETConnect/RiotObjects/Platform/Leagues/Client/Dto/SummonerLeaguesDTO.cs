#region

using System.Collections.Generic;
using LoLLauncher.RiotObjects.Leagues.Pojo;

#endregion

namespace LoLLauncher.RiotObjects.Platform.Leagues.Client.Dto
{
    public class SummonerLeaguesDto : RiotGamesObject
    {
        public delegate void Callback(SummonerLeaguesDto result);

        private readonly Callback _callback;
        private readonly string _type = "com.riotgames.platform.leagues.client.dto.SummonerLeaguesDTO";

        public SummonerLeaguesDto()
        {
        }

        public SummonerLeaguesDto(Callback callback)
        {
            this._callback = callback;
        }

        public SummonerLeaguesDto(TypedObject result)
        {
            SetFields(this, result);
        }

        public override string TypeName
        {
            get { return _type; }
        }

        [InternalName("summonerLeagues")]
        public List<LeagueListDto> SummonerLeagues { get; set; }

        public override void DoCallback(TypedObject result)
        {
            SetFields(this, result);
            _callback(this);
        }
    }
}