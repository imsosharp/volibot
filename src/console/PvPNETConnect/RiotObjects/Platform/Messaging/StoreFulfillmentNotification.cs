#region

using System;
using LoLLauncher.RiotObjects.Platform.Catalog.Champion;

#endregion

namespace LoLLauncher.RiotObjects.Platform.Messaging
{
    internal class StoreFulfillmentNotification : RiotGamesObject
    {
        public delegate void Callback(StoreFulfillmentNotification result);

        private readonly Callback _callback;
        private readonly string _type = "com.riotgames.platform.reroll.pojo.StoreFulfillmentNotification";

        public StoreFulfillmentNotification()
        {
        }

        public StoreFulfillmentNotification(Callback callback)
        {
            this._callback = callback;
        }

        public StoreFulfillmentNotification(TypedObject result)
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

        [InternalName("inventoryType")]
        public String InventoryType { get; set; }

        [InternalName("data")]
        public ChampionDto Data { get; set; }

        public override void DoCallback(TypedObject result)
        {
            SetFields(this, result);
            _callback(this);
        }
    }
}