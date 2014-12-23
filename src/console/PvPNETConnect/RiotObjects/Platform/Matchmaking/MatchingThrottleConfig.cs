#region

using System;
using System.Collections.Generic;

#endregion

namespace LoLLauncher.RiotObjects.Platform.Matchmaking
{
    public class MatchingThrottleConfig : RiotGamesObject
    {
        public delegate void Callback(MatchingThrottleConfig result);

        private readonly Callback _callback;
        private readonly string _type = "com.riotgames.platform.matchmaking.MatchingThrottleConfig";

        public MatchingThrottleConfig()
        {
        }

        public MatchingThrottleConfig(Callback callback)
        {
            this._callback = callback;
        }

        public MatchingThrottleConfig(TypedObject result)
        {
            SetFields(this, result);
        }

        public override string TypeName
        {
            get { return _type; }
        }

        [InternalName("limit")]
        public Double Limit { get; set; }

        [InternalName("matchingThrottleProperties")]
        public List<object> MatchingThrottleProperties { get; set; }

        [InternalName("cacheName")]
        public String CacheName { get; set; }

        public override void DoCallback(TypedObject result)
        {
            SetFields(this, result);
            _callback(this);
        }
    }
}