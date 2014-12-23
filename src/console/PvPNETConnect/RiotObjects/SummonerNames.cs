#region

using LoLLauncher;
using LoLLauncher.RiotObjects;

#endregion

namespace PVPNetConnect.RiotObjects
{
    public class SummonerNames : RiotGamesObject
    {
        public delegate void Callback(object[] result);

        private readonly Callback _callback;

        public SummonerNames(Callback callback)
        {
            this._callback = callback;
        }

        public override void DoCallback(TypedObject result)
        {
            _callback(result.GetArray("array"));
        }
    }
}