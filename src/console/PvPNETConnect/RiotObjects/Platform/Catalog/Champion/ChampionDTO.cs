#region

using System;
using System.Collections.Generic;

#endregion

namespace LoLLauncher.RiotObjects.Platform.Catalog.Champion
{
    public class ChampionDto : RiotGamesObject
    {
        public delegate void Callback(ChampionDto result);

        private readonly Callback _callback;
        private readonly string _type = "com.riotgames.platform.catalog.champion.ChampionDTO";

        public ChampionDto()
        {
        }

        public ChampionDto(Callback callback)
        {
            this._callback = callback;
        }

        public ChampionDto(TypedObject result)
        {
            SetFields(this, result);
        }

        public override string TypeName
        {
            get { return _type; }
        }

        [InternalName("searchTags")]
        public String[] SearchTags { get; set; }

        [InternalName("ownedByYourTeam")]
        public Boolean OwnedByYourTeam { get; set; }

        [InternalName("botEnabled")]
        public Boolean BotEnabled { get; set; }

        [InternalName("banned")]
        public Boolean Banned { get; set; }

        [InternalName("skinName")]
        public String SkinName { get; set; }

        [InternalName("displayName")]
        public String DisplayName { get; set; }

        [InternalName("championData")]
        public TypedObject ChampionData { get; set; }

        [InternalName("owned")]
        public Boolean Owned { get; set; }

        [InternalName("championId")]
        public Int32 ChampionId { get; set; }

        [InternalName("freeToPlayReward")]
        public Boolean FreeToPlayReward { get; set; }

        [InternalName("freeToPlay")]
        public Boolean FreeToPlay { get; set; }

        [InternalName("ownedByEnemyTeam")]
        public Boolean OwnedByEnemyTeam { get; set; }

        [InternalName("active")]
        public Boolean Active { get; set; }

        [InternalName("championSkins")]
        public List<ChampionSkinDto> ChampionSkins { get; set; }

        [InternalName("description")]
        public String Description { get; set; }

        [InternalName("winCountRemaining")]
        public Int32 WinCountRemaining { get; set; }

        [InternalName("purchaseDate")]
        public Double PurchaseDate { get; set; }

        [InternalName("endDate")]
        public Int32 EndDate { get; set; }

        public override void DoCallback(TypedObject result)
        {
            SetFields(this, result);
            _callback(this);
        }
    }
}