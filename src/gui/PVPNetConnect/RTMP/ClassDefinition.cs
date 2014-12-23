using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoLLauncher
{
   public class ClassDefinition
   {
      public string type;
      public bool externalizable = false;
      public bool dynamic = false;
      public List<string> members = new List<string>();
   }
}
