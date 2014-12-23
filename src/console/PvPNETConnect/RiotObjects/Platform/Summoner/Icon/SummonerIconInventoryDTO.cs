#region

using System;
using System.Collections.Generic;

#endregion

namespace LoLLauncher.RiotObjects.Platform.Summoner.Icon
{
    public class SummonerIconInventoryDto : RiotGamesObject
    {
        public delegate void Callback(SummonerIconInventoryDto result);

        private readonly Callback _callback;
        private readonly string _type = "com.riotgames.platform.summoner.icon.SummonerIconInventoryDTO";

        public SummonerIconInventoryDto()
        {
        }

        public SummonerIconInventoryDto(Callback callback)
        {
            this._callback = callback;
        }

        public SummonerIconInventoryDto(TypedObject result)
        {
            SetFields(this, result);
        }

        public override string TypeName
        {
            get { return _type; }
        }

        [InternalName("summonerId")]
        public Double SummonerId { get; set; }

        [InternalName("summonerIcons")]
        public List<Catalog.Icon.Icon> SummonerIcons { get; set; }

        public override void DoCallback(TypedObject result)
        {
            SetFields(this, result);
            _callback(this);
        }
    }
}