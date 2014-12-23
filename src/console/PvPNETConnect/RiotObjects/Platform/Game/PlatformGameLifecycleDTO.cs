#region

using System;

#endregion

namespace LoLLauncher.RiotObjects.Platform.Game
{
    public class PlatformGameLifecycleDto : RiotGamesObject
    {
        public delegate void Callback(PlatformGameLifecycleDto result);

        private readonly Callback _callback;
        private readonly string _type = "com.riotgames.platform.game.PlatformGameLifecycleDTO";

        public PlatformGameLifecycleDto()
        {
        }

        public PlatformGameLifecycleDto(Callback callback)
        {
            this._callback = callback;
        }

        public PlatformGameLifecycleDto(TypedObject result)
        {
            SetFields(this, result);
        }

        public override string TypeName
        {
            get { return _type; }
        }

        [InternalName("gameSpecificLoyaltyRewards")]
        public object GameSpecificLoyaltyRewards { get; set; }

        [InternalName("reconnectDelay")]
        public Int32 ReconnectDelay { get; set; }

        [InternalName("lastModifiedDate")]
        public object LastModifiedDate { get; set; }

        [InternalName("game")]
        public GameDto Game { get; set; }

        [InternalName("playerCredentials")]
        public PlayerCredentialsDto PlayerCredentials { get; set; }

        [InternalName("gameName")]
        public String GameName { get; set; }

        [InternalName("connectivityStateEnum")]
        public object ConnectivityStateEnum { get; set; }

        public override void DoCallback(TypedObject result)
        {
            SetFields(this, result);
            _callback(this);
        }
    }
}