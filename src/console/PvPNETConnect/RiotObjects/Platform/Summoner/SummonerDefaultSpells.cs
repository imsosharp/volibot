#region

using System;

#endregion

namespace LoLLauncher.RiotObjects.Platform.Summoner
{
    public class SummonerDefaultSpells : RiotGamesObject
    {
        public delegate void Callback(SummonerDefaultSpells result);

        private readonly Callback _callback;
        private readonly string _type = "com.riotgames.platform.summoner.SummonerDefaultSpells";

        public SummonerDefaultSpells()
        {
        }

        public SummonerDefaultSpells(Callback callback)
        {
            this._callback = callback;
        }

        public SummonerDefaultSpells(TypedObject result)
        {
            SetFields(this, result);
        }

        public override string TypeName
        {
            get { return _type; }
        }

        [InternalName("summonerDefaultSpellsJson")]
        public object SummonerDefaultSpellsJson { get; set; }

        [InternalName("summonerDefaultSpellMap")]
        public TypedObject SummonerDefaultSpellMap { get; set; }

        [InternalName("summonerId")]
        public Double SummonerId { get; set; }

        public override void DoCallback(TypedObject result)
        {
            SetFields(this, result);
            _callback(this);
        }
    }
}