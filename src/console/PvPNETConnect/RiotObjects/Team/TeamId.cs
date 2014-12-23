#region

using System;

#endregion

namespace LoLLauncher.RiotObjects.Team
{
    public class TeamId : RiotGamesObject
    {
        public delegate void Callback(TeamId result);

        private readonly Callback _callback;
        private readonly string _type = "com.riotgames.team.TeamId";

        public TeamId()
        {
        }

        public TeamId(Callback callback)
        {
            this._callback = callback;
        }

        public TeamId(TypedObject result)
        {
            SetFields(this, result);
        }

        public override string TypeName
        {
            get { return _type; }
        }

        [InternalName("fullId")]
        public String FullId { get; set; }

        public override void DoCallback(TypedObject result)
        {
            SetFields(this, result);
            _callback(this);
        }
    }
}