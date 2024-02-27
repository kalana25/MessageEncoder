using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessageEncoder.Core.Implementations;

namespace MessageEncoder
{
    public class MessageCodec : GenericMessageCodec
    {
        public MessageCodec()
        {
            messageEncoder = new SimpleMessageEncoder();
            messageDecoder = new SimpleMessageDecoder();
        }
    }
}
