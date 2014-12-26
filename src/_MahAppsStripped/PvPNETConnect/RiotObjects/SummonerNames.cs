using LoLLauncher;
using LoLLauncher.RiotObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PVPNetConnect.RiotObjects
{
    public class SummonerNames : RiotGamesObject
    {
        public SummonerNames(Callback callback)
        {
            this.callback = callback;
        }

        public delegate void Callback(object[] result);
        private Callback callback;

        public override void DoCallback(TypedObject result)
        {
            callback(result.GetArray("array"));
        }
    }
}
