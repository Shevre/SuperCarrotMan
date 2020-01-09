using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarrotManv2.Core
{
    public class DebugHandler
    {
        public string debugString = "";

        public void Log(string s) 
        {
            debugString += s;
        }

        public void ClearString() 
        {
            debugString = "";
        }
    }

}
