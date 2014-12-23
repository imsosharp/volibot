#region

using System;

#endregion

namespace LoLLauncher.RiotObjects.Platform.Game
{
    public class PlayerChampionSelectionDto : RiotGamesObject
    {
        public delegate void Callback(PlayerChampionSelectionDto result);

        private readonly Callback _callback;
        private readonly string _type = "com.riotgames.platform.game.PlayerChampionSelectionDTO";

        public PlayerChampionSelectionDto()
        {
        }

        public PlayerChampionSelectionDto(Callback callback)
        {
            this._callback = callback;
        }

        public PlayerChampionSelectionDto(TypedObject result)
        {
            SetFields(this, result);
        }

        public override string TypeName
        {
            get { return _type; }
        }

        [InternalName("summonerInternalName")]
        public String SummonerInternalName { get; set; }

        [InternalName("spell2Id")]
        public Double Spell2Id { get; set; }

        [InternalName("selectedSkinIndex")]
        public Int32 SelectedSkinIndex { get; set; }

        [InternalName("championId")]
        public Int32 ChampionId { get; set; }

        [InternalName("spell1Id")]
        public Double Spell1Id { get; set; }

        public override void DoCallback(TypedObject result)
        {
            SetFields(this, result);
            _callback(this);
        }
    }
}