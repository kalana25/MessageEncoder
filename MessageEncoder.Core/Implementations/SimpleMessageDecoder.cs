using MessageEncoder.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageEncoder.Core.Implementations
{
    public class SimpleMessageDecoder : IDecodeMessage
    {
        private const int MaxHeaderSize = 1023;
        private const int MaxHeaderCount = 63;

        public IDecodeHeader HeaderDecoder { get; set; }

        public SimpleMessageDecoder()
        {
            HeaderDecoder = new SimpleHeaderDecoder();
        }

        public Message Decode(byte[] data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            var headerSize = Math.Min(data.Length, MaxHeaderCount * MaxHeaderSize);
            byte[] headers = new byte[headerSize];
            Array.Copy(data, headers, headers.Length);

            var payloadSize = Math.Max(data.Length - headers.Length, 0);
            var payload = new byte[payloadSize];

            Array.Copy(data, headers.Length, payload, 0, payloadSize);
            var decodedHeaders = HeaderDecoder.DecodeHeaders(headers);
            return new Message { Headers = decodedHeaders, Payload = payload };
        }
    }
}
