using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace OssCons.DotNetSubcommittee.TestClassLibrary1
{
    /// <summary>Class3</summary>
    public class Class3
    {
        /// <summary>SerializeObject</summary>
        public static string SerializeObject(Dictionary<string, string> dic)
        {
            return JsonConvert.SerializeObject(dic);
        }
    }
}
