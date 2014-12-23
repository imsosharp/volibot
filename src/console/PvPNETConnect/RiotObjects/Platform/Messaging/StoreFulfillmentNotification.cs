using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoLLauncher.RiotObjects.Platform.Messaging
{
    class StoreFulfillmentNotification : RiotGamesObject
    {
        public override string TypeName
        {
            get
            {
                return this.type;
            }
        }

        private string type = "com.riotgames.platform.reroll.pojo.StoreFulfillmentNotification";

        public StoreFulfillmentNotification()
        {
        }

        public StoreFulfillmentNotification(Callback callback)
        {
            this.callback = callback;
        }

        public StoreFulfillmentNotification(TypedObject result)
        {
            base.SetFields(this, result);
        }

        public delegate void Callback(StoreFulfillmentNotification result);

        private Callback callback;

        public override void DoCallback(TypedObject result)
        {
            base.SetFields(this, result);
            callback(this);
        }

        [InternalName("rp")]
        public Double Rp { get; set; }

        [InternalName("ip")]
        public Double Ip { get; set; }

        [InternalName("inventoryType")]
        public String InventoryType { get; set; }

        [InternalName("data")]
        public Platform.Catalog.Champion.ChampionDTO Data { get; set; }

    }
}
