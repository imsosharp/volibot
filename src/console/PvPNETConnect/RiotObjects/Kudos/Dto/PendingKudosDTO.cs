#region

using System;

#endregion

namespace LoLLauncher.RiotObjects.Kudos.Dto
{
    public class PendingKudosDto : RiotGamesObject
    {
        public delegate void Callback(PendingKudosDto result);

        private readonly Callback _callback;
        private readonly string _type = "com.riotgames.kudos.dto.PendingKudosDTO";

        public PendingKudosDto()
        {
        }

        public PendingKudosDto(Callback callback)
        {
            this._callback = callback;
        }

        public PendingKudosDto(TypedObject result)
        {
            SetFields(this, result);
        }

        public override string TypeName
        {
            get { return _type; }
        }

        [InternalName("pendingCounts")]
        public Int32[] PendingCounts { get; set; }

        public override void DoCallback(TypedObject result)
        {
            SetFields(this, result);
            _callback(this);
        }
    }
}