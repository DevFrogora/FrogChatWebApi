using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientStorage.GlobalVariable
{
    public class RuntimeConfiguration :IRuntimeConfiguration
    {
        public Dictionary<string , string> Configuration { get; set; } 
    }
}
