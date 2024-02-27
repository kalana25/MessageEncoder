using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageEncoder
{
    public class Message
    {
        public Dictionary<string,string> Headers { get; set; }
        public byte[] Payload { get; set; }

    }
}
