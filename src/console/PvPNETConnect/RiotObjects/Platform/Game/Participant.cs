namespace LoLLauncher.RiotObjects.Platform.Game
{
    public class Participant : RiotGamesObject
    {
        public delegate void Callback(Participant result);

        private readonly Callback _callback;

        public Participant()
        {
        }

        public Participant(Callback callback)
        {
            this._callback = callback;
        }

        public Participant(TypedObject result)
        {
            SetFields(this, result);
        }

        public override void DoCallback(TypedObject result)
        {
            SetFields(this, result);
            _callback(this);
        }
    }
}