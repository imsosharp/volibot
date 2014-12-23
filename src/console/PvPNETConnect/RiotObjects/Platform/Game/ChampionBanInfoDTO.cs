#region

using System;

#endregion

namespace LoLLauncher.RiotObjects.Platform.Game
{
    public class ChampionBanInfoDto : RiotGamesObject
    {
        public delegate void Callback(ChampionBanInfoDto result);

        private readonly Callback _callback;
        private readonly string _type = "com.riotgames.platform.game.ChampionBanInfoDTO";

        public ChampionBanInfoDto(Callback callback)
        {
            this._callback = callback;
        }

        public ChampionBanInfoDto(TypedObject result)
        {
            SetFields(this, result);
        }

        public override string TypeName
        {
            get { return _type; }
        }

        [InternalName("enemyOwned")]
        public Boolean EnemyOwned { get; set; }

        [InternalName("championId")]
        public Int32 ChampionId { get; set; }

        [InternalName("owned")]
        public Boolean Owned { get; set; }

        public override void DoCallback(TypedObject result)
        {
            SetFields(this, result);
            _callback(this);
        }
    }
}