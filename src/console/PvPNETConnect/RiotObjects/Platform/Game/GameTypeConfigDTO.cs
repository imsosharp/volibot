#region

using System;

#endregion

namespace LoLLauncher.RiotObjects.Platform.Game
{
    public class GameTypeConfigDto : RiotGamesObject
    {
        public delegate void Callback(GameTypeConfigDto result);

        private readonly Callback _callback;
        private readonly string _type = "com.riotgames.platform.game.GameTypeConfigDTO";

        public GameTypeConfigDto()
        {
        }

        public GameTypeConfigDto(Callback callback)
        {
            this._callback = callback;
        }

        public GameTypeConfigDto(TypedObject result)
        {
            SetFields(this, result);
        }

        public override string TypeName
        {
            get { return _type; }
        }

        [InternalName("id")]
        public Int32 Id { get; set; }

        [InternalName("allowTrades")]
        public Boolean AllowTrades { get; set; }

        [InternalName("name")]
        public String Name { get; set; }

        [InternalName("mainPickTimerDuration")]
        public Int32 MainPickTimerDuration { get; set; }

        [InternalName("exclusivePick")]
        public Boolean ExclusivePick { get; set; }

        [InternalName("pickMode")]
        public String PickMode { get; set; }

        [InternalName("maxAllowableBans")]
        public Int32 MaxAllowableBans { get; set; }

        [InternalName("banTimerDuration")]
        public Int32 BanTimerDuration { get; set; }

        [InternalName("postPickTimerDuration")]
        public Int32 PostPickTimerDuration { get; set; }

        public override void DoCallback(TypedObject result)
        {
            SetFields(this, result);
            _callback(this);
        }
    }
}