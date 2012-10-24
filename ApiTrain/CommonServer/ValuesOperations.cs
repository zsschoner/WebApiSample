using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonServer
{
    public static class ValuesOperations
    {
        public static IEnumerable<string> GetValues()
        {
            return new string[] { "value1", "value2" };
        }
    }
}
