#region

using System;

#endregion

namespace LoLLauncher.RiotObjects.Team
{
    public class CreatedTeam : RiotGamesObject
    {
        public delegate void Callback(CreatedTeam result);

        private readonly Callback _callback;
        private readonly string _type = "com.riotgames.team.CreatedTeam";

        public CreatedTeam()
        {
        }

        public CreatedTeam(Callback callback)
        {
            this._callback = callback;
        }

        public CreatedTeam(TypedObject result)
        {
            SetFields(this, result);
        }

        public override string TypeName
        {
            get { return _type; }
        }

        [InternalName("timeStamp")]
        public Double TimeStamp { get; set; }

        [InternalName("teamId")]
        public TeamId TeamId { get; set; }

        public override void DoCallback(TypedObject result)
        {
            SetFields(this, result);
            _callback(this);
        }
    }
}