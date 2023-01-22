using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientStorage.GlobalVariable
{
    public interface IRuntimeConfiguration
    {
        Dictionary<string, string> Configuration { get; set; }
    }
}
