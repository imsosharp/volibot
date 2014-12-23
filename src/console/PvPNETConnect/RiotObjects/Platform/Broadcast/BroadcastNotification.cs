namespace LoLLauncher.RiotObjects.Platform.Broadcast
{
    public class BroadcastNotification : RiotGamesObject
    {
        public delegate void Callback(BroadcastNotification result);

        private readonly Callback _callback;
        private readonly string _type = "com.riotgames.platform.broadcast.BroadcastNotification";

        public BroadcastNotification()
        {
        }

        public BroadcastNotification(Callback callback)
        {
            this._callback = callback;
        }

        public BroadcastNotification(TypedObject result)
        {
            SetFields(this, result);
        }

        public override string TypeName
        {
            get { return _type; }
        }

        public override void DoCallback(TypedObject result)
        {
            SetFields(this, result);
            _callback(this);
        }
    }
}