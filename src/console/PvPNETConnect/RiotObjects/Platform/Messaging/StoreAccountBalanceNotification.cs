#region

using System;

#endregion

namespace LoLLauncher.RiotObjects.Platform.Messaging
{
    internal class StoreAccountBalanceNotification : RiotGamesObject
    {
        public delegate void Callback(StoreAccountBalanceNotification result);

        private readonly Callback _callback;
        private readonly string _type = "com.riotgames.platform.reroll.pojo.StoreAccountBalanceNotification";

        public StoreAccountBalanceNotification()
        {
        }

        public StoreAccountBalanceNotification(Callback callback)
        {
            this._callback = callback;
        }

        public StoreAccountBalanceNotification(TypedObject result)
        {
            SetFields(this, result);
        }

        public override string TypeName
        {
            get { return _type; }
        }

        [InternalName("rp")]
        public Double Rp { get; set; }

        [InternalName("ip")]
        public Double Ip { get; set; }

        public override void DoCallback(TypedObject result)
        {
            SetFields(this, result);
            _callback(this);
        }
    }
}