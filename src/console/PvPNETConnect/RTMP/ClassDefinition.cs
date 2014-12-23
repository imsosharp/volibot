#region

using System.Collections.Generic;

#endregion

namespace LoLLauncher
{
    public class ClassDefinition
    {
        public bool Dynamic = false;
        public bool Externalizable = false;
        public List<string> Members = new List<string>();
        public string Type;
    }
}