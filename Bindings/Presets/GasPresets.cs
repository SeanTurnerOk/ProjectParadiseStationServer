using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class GasPresets
    {
        //GasPresets.Air(100)
        public static Gas Air(int amount)
        {
            List<string> tags = new List<string>();
            tags.Add("breathable");
            Gas temp = new Gas("air", tags, amount);
            return temp;
        }
    }
}
