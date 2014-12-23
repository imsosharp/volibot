#region

using System;

#endregion

namespace LoLLauncher.RiotObjects.Platform.Game
{
    public class ObfruscatedParticipant : Participant
    {
        public delegate void Callback(ObfruscatedParticipant result);

        private readonly Callback _callback;
        private readonly string _type = "com.riotgames.platform.game.ObfruscatedParticipant";

        public ObfruscatedParticipant()
        {
        }

        public ObfruscatedParticipant(Callback callback)
        {
            this._callback = callback;
        }

        public ObfruscatedParticipant(TypedObject result)
        {
            SetFields(this, result);
        }

        public override string TypeName
        {
            get { return _type; }
        }

        [InternalName("badges")]
        public Int32 Badges { get; set; }

        [InternalName("index")]
        public Int32 Index { get; set; }

        [InternalName("clientInSynch")]
        public Boolean ClientInSynch { get; set; }

        [InternalName("gameUniqueId")]
        public Int32 GameUniqueId { get; set; }

        [InternalName("pickMode")]
        public Int32 PickMode { get; set; }

        public override void DoCallback(TypedObject result)
        {
            SetFields(this, result);
            _callback(this);
        }
    }
}