using MessageEncoder.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageEncoder.Core.Implementations
{
    public class SimpleHeaderEncoder: IEncodeHeader
    {
        private readonly int maxHeaderSize;
        private readonly int maxHeaderCount;

        public SimpleHeaderEncoder(int _maxHeaderSize, int _maxHeaderCount)
        {
            maxHeaderSize = _maxHeaderSize;
            maxHeaderCount = _maxHeaderCount;
        }

        public byte[] EncodeHeaders(Dictionary<string, string> headers)
        {
            var headerList = new List<byte>();
            foreach (var header in headers)
            {
                if (headers.Count > maxHeaderCount) throw new ArgumentException("Message header count exceed maximum allowed threshold");

                var headerName = Encoding.ASCII.GetBytes(header.Key);
                var headerValue = Encoding.ASCII.GetBytes(header.Value);
                if (headerName.Length > maxHeaderSize || headerValue.Length > maxHeaderSize)
                    throw new ArgumentException("Header name or value exceeds the maximum threshold.");

                headerList.AddRange(BitConverter.GetBytes((ushort)headerName.Length));
                headerList.AddRange(headerName);
                headerList.AddRange(BitConverter.GetBytes((ushort)headerValue.Length));
                headerList.AddRange(headerValue);
            }
            return headerList.ToArray();
        }
    }
}
