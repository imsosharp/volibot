#region

using System.Collections.Generic;

#endregion

namespace LoLLauncher.RiotObjects.Platform.Gameinvite.Contract
{
    public class LobbyStatus : RiotGamesObject
    {
        public delegate void Callback(LobbyStatus result);

        private readonly Callback _callback;
        private readonly string _type = "com.riotgames.platform.gameinvite.contract.LobbyStatus";

        public LobbyStatus(Callback callback)
        {
            this._callback = callback;
        }

        public LobbyStatus(TypedObject result)
        {
            SetFields(this, result);
        }

        public override string TypeName
        {
            get { return _type; }
        }

        [InternalName("gameMetaData")]
        public Dictionary<string, object> GameMetaData { get; set; }

        public override void DoCallback(TypedObject result)
        {
            SetFields(this, result);
            _callback(this);
        }
    }
}