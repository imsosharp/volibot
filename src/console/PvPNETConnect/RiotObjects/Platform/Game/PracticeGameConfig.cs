#region

using System;
using LoLLauncher.RiotObjects.Platform.Game.Map;

#endregion

namespace LoLLauncher.RiotObjects.Platform.Game
{
    public class PracticeGameConfig : RiotGamesObject
    {
        public delegate void Callback(PracticeGameConfig result);

        private readonly Callback _callback;
        private readonly string _type = "com.riotgames.platform.game.PracticeGameConfig";

        public PracticeGameConfig()
        {
        }

        public PracticeGameConfig(Callback callback)
        {
            this._callback = callback;
        }

        public PracticeGameConfig(TypedObject result)
        {
            SetFields(this, result);
        }

        public override string TypeName
        {
            get { return _type; }
        }

        [InternalName("passbackUrl")]
        public object PassbackUrl { get; set; }

        [InternalName("gameName")]
        public String GameName { get; set; }

        [InternalName("gameTypeConfig")]
        public Int32 GameTypeConfig { get; set; }

        [InternalName("passbackDataPacket")]
        public object PassbackDataPacket { get; set; }

        [InternalName("gamePassword")]
        public String GamePassword { get; set; }

        [InternalName("gameMap")]
        public GameMap GameMap { get; set; }

        [InternalName("gameMode")]
        public String GameMode { get; set; }

        [InternalName("allowSpectators")]
        public String AllowSpectators { get; set; }

        [InternalName("maxNumPlayers")]
        public Int32 MaxNumPlayers { get; set; }

        [InternalName("region")]
        public String Region { get; set; }

        public override void DoCallback(TypedObject result)
        {
            SetFields(this, result);
            _callback(this);
        }
    }
}