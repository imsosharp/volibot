#region

using System;

#endregion

namespace LoLLauncher.RiotObjects.Platform.Game.Message
{
    public class GameNotification : RiotGamesObject
    {
        public delegate void Callback(GameNotification result);

        private readonly Callback _callback;
        private readonly string _type = "com.riotgames.platform.game.message.GameNotification";

        public GameNotification()
        {
        }

        public GameNotification(Callback callback)
        {
            this._callback = callback;
        }

        public GameNotification(TypedObject result)
        {
            SetFields(this, result);
        }

        public override string TypeName
        {
            get { return _type; }
        }

        [InternalName("messageCode")]
        public String MessageCode { get; set; }

        [InternalName("type")]
        public String Type { get; set; }

        [InternalName("messageArgument")]
        public object MessageArgument { get; set; }

        public override void DoCallback(TypedObject result)
        {
            SetFields(this, result);
            _callback(this);
        }
    }
}