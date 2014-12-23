#region

using System;
using System.Collections.Generic;

#endregion

namespace LoLLauncher.RiotObjects.Platform.Summoner.Runes
{
    public class SummonerRuneInventory : RiotGamesObject
    {
        public delegate void Callback(SummonerRuneInventory result);

        private readonly Callback _callback;
        private readonly string _type = "com.riotgames.platform.summoner.runes.SummonerRuneInventory";

        public SummonerRuneInventory()
        {
        }

        public SummonerRuneInventory(Callback callback)
        {
            this._callback = callback;
        }

        public SummonerRuneInventory(TypedObject result)
        {
            SetFields(this, result);
        }

        public override string TypeName
        {
            get { return _type; }
        }

        [InternalName("summonerRunesJson")]
        public object SummonerRunesJson { get; set; }

        [InternalName("dateString")]
        public String DateString { get; set; }

        [InternalName("summonerRunes")]
        public List<SummonerRune> SummonerRunes { get; set; }

        [InternalName("summonerId")]
        public Double SummonerId { get; set; }

        public override void DoCallback(TypedObject result)
        {
            SetFields(this, result);
            _callback(this);
        }
    }
}