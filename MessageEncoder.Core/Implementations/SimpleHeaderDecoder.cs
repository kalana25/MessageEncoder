using MessageEncoder.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageEncoder.Core.Implementations
{
    internal class SimpleHeaderDecoder : IDecodeHeader
    {
        public Dictionary<string, string> DecodeHeaders(byte[] headers)
        {
            var result = new Dictionary<string, string>();
            var index = 0;
            while (index < headers.Length)
            {
                var hdrNameLength = BitConverter.ToUInt16(headers, index);
                index += sizeof(ushort);
                string headerNameString = Encoding.ASCII.GetString(headers, index, hdrNameLength);
                index += hdrNameLength;

                var hdrValueLength = BitConverter.ToUInt16(headers, index);
                index += sizeof(ushort);
                string headerValueString = Encoding.ASCII.GetString(headers, index, hdrValueLength);
                index += hdrValueLength;

                result[headerNameString] = headerValueString;

            }
            return result;
        }
    }
}
