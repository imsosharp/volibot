#region

using System;

#endregion

namespace LoLLauncher.RiotObjects.Platform.Matchmaking
{
    public class QueueInfo : RiotGamesObject
    {
        public delegate void Callback(QueueInfo result);

        private readonly Callback _callback;
        private readonly string _type = "com.riotgames.platform.matchmaking.QueueInfo";

        public QueueInfo()
        {
        }

        public QueueInfo(Callback callback)
        {
            this._callback = callback;
        }

        public QueueInfo(TypedObject result)
        {
            SetFields(this, result);
        }

        public override string TypeName
        {
            get { return _type; }
        }

        [InternalName("waitTime")]
        public Double WaitTime { get; set; }

        [InternalName("queueId")]
        public Double QueueId { get; set; }

        [InternalName("queueLength")]
        public Int32 QueueLength { get; set; }

        public override void DoCallback(TypedObject result)
        {
            SetFields(this, result);
            _callback(this);
        }
    }
}