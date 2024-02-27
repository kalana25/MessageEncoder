using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageEncoder
{
    public interface IMessageCodec
    {
        byte[] encode(Message message);
        Message decode(byte[] data);
    }
}
