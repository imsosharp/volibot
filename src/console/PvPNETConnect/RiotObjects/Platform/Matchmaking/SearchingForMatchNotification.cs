#region

using System.Collections.Generic;

#endregion

namespace LoLLauncher.RiotObjects.Platform.Matchmaking
{
    public class SearchingForMatchNotification : RiotGamesObject
    {
        public delegate void Callback(SearchingForMatchNotification result);

        private readonly Callback _callback;
        private readonly string _type = "com.riotgames.platform.matchmaking.SearchingForMatchNotification";

        public SearchingForMatchNotification()
        {
        }

        public SearchingForMatchNotification(Callback callback)
        {
            this._callback = callback;
        }

        public SearchingForMatchNotification(TypedObject result)
        {
            SetFields(this, result);
        }

        public override string TypeName
        {
            get { return _type; }
        }

        [InternalName("playerJoinFailures")]
        public QueueDodger PlayerJoinFailures { get; set; }

        [InternalName("ghostGameSummoners")]
        public object GhostGameSummoners { get; set; }

        [InternalName("joinedQueues")]
        public List<QueueInfo> JoinedQueues { get; set; }

        public override void DoCallback(TypedObject result)
        {
            SetFields(this, result);
            _callback(this);
        }
    }
}