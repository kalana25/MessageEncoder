using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageEncoder.Core.Interfaces
{
    public interface IEncodeHeader
    {
        byte[] EncodeHeaders(Dictionary<string, string> headers);
    }
}
