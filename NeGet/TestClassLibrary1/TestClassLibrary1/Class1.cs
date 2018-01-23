using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace OssCons.DotNetSubcommittee.TestClassLibrary1
{
    /// <summary>Class1</summary>
    public class Class1
    {
        /// <summary>SerializeObject</summary>
        public static string SerializeObject(string[] array)
        {
            return JsonConvert.SerializeObject(array);
        }
    }
}
