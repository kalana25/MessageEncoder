using MessageEncoder.Core;
using MessageEncoder.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageEncoder
{
    public class GenericMessageCodec
    {
        protected IEncodeMessage messageEncoder;
        protected IDecodeMessage messageDecoder;

        public byte[] Encode(Message message)
        {
            return messageEncoder.Encode(message);
        }

        public Message Decode(byte[] data)
        {
            return messageDecoder.Decode(data);
        }
        
    }
}
