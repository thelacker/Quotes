using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Котировки
{
    [Serializable]
    public class Ser
    {
        public string quote { get; set; }
        public DateTime start { get; set; }
        public DateTime end { get; set; }
        public int period { get; set; }
        public Ser() { }
        public Ser(string q, DateTime s, DateTime e, int p)
        {
            quote = q;
            start = s;
            end = e;
            period = p;
        }
    }

}
