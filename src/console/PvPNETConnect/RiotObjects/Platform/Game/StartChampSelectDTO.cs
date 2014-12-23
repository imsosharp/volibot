#region

using System.Collections.Generic;

#endregion

namespace LoLLauncher.RiotObjects.Platform.Game
{
    public class StartChampSelectDto : RiotGamesObject
    {
        public delegate void Callback(StartChampSelectDto result);

        private readonly Callback _callback;
        private readonly string _type = "com.riotgames.platform.game.StartChampSelectDTO";

        public StartChampSelectDto()
        {
        }

        public StartChampSelectDto(Callback callback)
        {
            this._callback = callback;
        }

        public StartChampSelectDto(TypedObject result)
        {
            SetFields(this, result);
        }

        public override string TypeName
        {
            get { return _type; }
        }

        [InternalName("invalidPlayers")]
        public List<object> InvalidPlayers { get; set; }

        public override void DoCallback(TypedObject result)
        {
            SetFields(this, result);
            _callback(this);
        }
    }
}