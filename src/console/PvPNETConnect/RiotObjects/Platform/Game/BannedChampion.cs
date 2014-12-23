#region

using System;

#endregion

namespace LoLLauncher.RiotObjects.Platform.Game
{
    public class BannedChampion : RiotGamesObject
    {
        public delegate void Callback(BannedChampion result);

        private readonly Callback _callback;
        private readonly string _type = "com.riotgames.platform.game.BannedChampion";

        public BannedChampion(Callback callback)
        {
            this._callback = callback;
        }

        public BannedChampion(TypedObject result)
        {
            SetFields(this, result);
        }

        public override string TypeName
        {
            get { return _type; }
        }

        [InternalName("pickTurn")]
        public Int32 PickTurn { get; set; }

        [InternalName("championId")]
        public Int32 ChampionId { get; set; }

        [InternalName("teamId")]
        public Int32 TeamId { get; set; }

        public override void DoCallback(TypedObject result)
        {
            SetFields(this, result);
            _callback(this);
        }
    }
}