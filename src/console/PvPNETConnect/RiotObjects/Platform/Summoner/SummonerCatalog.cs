#region

using System.Collections.Generic;

#endregion

namespace LoLLauncher.RiotObjects.Platform.Summoner
{
    public class SummonerCatalog : RiotGamesObject
    {
        public delegate void Callback(SummonerCatalog result);

        private readonly Callback _callback;
        private readonly string _type = "com.riotgames.platform.summoner.SummonerCatalog";

        public SummonerCatalog()
        {
        }

        public SummonerCatalog(Callback callback)
        {
            this._callback = callback;
        }

        public SummonerCatalog(TypedObject result)
        {
            SetFields(this, result);
        }

        public override string TypeName
        {
            get { return _type; }
        }

        [InternalName("items")]
        public object Items { get; set; }

        [InternalName("talentTree")]
        public List<TalentGroup> TalentTree { get; set; }

        [InternalName("spellBookConfig")]
        public List<RuneSlot> SpellBookConfig { get; set; }

        public override void DoCallback(TypedObject result)
        {
            SetFields(this, result);
            _callback(this);
        }
    }
}