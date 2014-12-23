#region

using System;
using LoLLauncher.RiotObjects.Platform.Catalog.Runes;

#endregion

namespace LoLLauncher.RiotObjects.Platform.Summoner.Runes
{
    public class SummonerRune : RiotGamesObject
    {
        public delegate void Callback(SummonerRune result);

        private readonly Callback _callback;
        private readonly string _type = "com.riotgames.platform.summoner.runes.SummonerRune";

        public SummonerRune()
        {
        }

        public SummonerRune(Callback callback)
        {
            this._callback = callback;
        }

        public SummonerRune(TypedObject result)
        {
            SetFields(this, result);
        }

        public override string TypeName
        {
            get { return _type; }
        }

        [InternalName("purchased")]
        public DateTime Purchased { get; set; }

        [InternalName("purchaseDate")]
        public DateTime PurchaseDate { get; set; }

        [InternalName("runeId")]
        public Int32 RuneId { get; set; }

        [InternalName("quantity")]
        public Int32 Quantity { get; set; }

        [InternalName("rune")]
        public Rune Rune { get; set; }

        [InternalName("summonerId")]
        public Double SummonerId { get; set; }

        public override void DoCallback(TypedObject result)
        {
            SetFields(this, result);
            _callback(this);
        }
    }
}