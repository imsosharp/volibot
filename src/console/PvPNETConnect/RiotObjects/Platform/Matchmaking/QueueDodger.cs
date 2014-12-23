#region

using System;

#endregion

namespace LoLLauncher.RiotObjects.Platform.Matchmaking
{
    public class QueueDodger : RiotGamesObject
    {
        public delegate void Callback(QueueDodger result);

        private readonly Callback _callback;
        private readonly string _type = "com.riotgames.platform.matchmaking.QueueDodger";

        public QueueDodger()
        {
        }

        public QueueDodger(Callback callback)
        {
            this._callback = callback;
        }

        public QueueDodger(TypedObject result)
        {
            SetFields(this, result);
        }

        public override string TypeName
        {
            get { return _type; }
        }

        [InternalName("reasonFailed")]
        public String ReasonFailed { get; set; }

        [InternalName("summoner")]
        public Summoner.Summoner Summoner { get; set; }

        [InternalName("dodgePenaltyRemainingTime")]
        public Int32 DodgePenaltyRemainingTime { get; set; }

        public override void DoCallback(TypedObject result)
        {
            SetFields(this, result);
            _callback(this);
        }
    }
}