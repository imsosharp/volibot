#region

using System;

#endregion

namespace LoLLauncher.RiotObjects.Platform.Catalog.Champion
{
    public class ChampionSkinDto : RiotGamesObject
    {
        public delegate void Callback(ChampionSkinDto result);

        private readonly Callback _callback;
        private readonly string _type = "com.riotgames.platform.catalog.champion.ChampionSkinDTO";

        public ChampionSkinDto()
        {
        }

        public ChampionSkinDto(Callback callback)
        {
            this._callback = callback;
        }

        public ChampionSkinDto(TypedObject result)
        {
            SetFields(this, result);
        }

        public override string TypeName
        {
            get { return _type; }
        }

        [InternalName("championId")]
        public Int32 ChampionId { get; set; }

        [InternalName("skinId")]
        public Int32 SkinId { get; set; }

        [InternalName("freeToPlayReward")]
        public Boolean FreeToPlayReward { get; set; }

        [InternalName("stillObtainable")]
        public Boolean StillObtainable { get; set; }

        [InternalName("lastSelected")]
        public Boolean LastSelected { get; set; }

        [InternalName("skinIndex")]
        public Int32 SkinIndex { get; set; }

        [InternalName("owned")]
        public Boolean Owned { get; set; }

        [InternalName("winCountRemaining")]
        public Int32 WinCountRemaining { get; set; }

        [InternalName("purchaseDate")]
        public Int32 PurchaseDate { get; set; }

        [InternalName("endDate")]
        public Int32 EndDate { get; set; }

        public override void DoCallback(TypedObject result)
        {
            SetFields(this, result);
            _callback(this);
        }
    }
}