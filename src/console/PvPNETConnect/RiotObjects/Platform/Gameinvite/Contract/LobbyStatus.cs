using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoLLauncher.RiotObjects.Platform.Gameinvite.Contract
{
    public class LobbyStatus : RiotGamesObject
    {
        public override string TypeName
        {
            get
            {
                return this.type;
            }
        }

        private string type = "com.riotgames.platform.gameinvite.contract.LobbyStatus";

        public LobbyStatus(Callback callback)
        {
            this.callback = callback;
        }

        public LobbyStatus(TypedObject result)
        {
            base.SetFields(this, result);
        }

        public delegate void Callback(LobbyStatus result);

        private Callback callback;

        public override void DoCallback(TypedObject result)
        {
            base.SetFields(this, result);
            callback(this);
        }

        [InternalName("gameMetaData")]
        public Dictionary<string, object> GameMetaData { get; set; }

    }
}
