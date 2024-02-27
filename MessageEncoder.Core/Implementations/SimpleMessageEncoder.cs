using MessageEncoder.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageEncoder.Core.Implementations
{
    public class SimpleMessageEncoder : IEncodeMessage
    {
        private const int MaxHeaderSize = 1023;
        private const int MaxPayloadSize = 256 * 1024;
        private const int MaxHeaderCount = 63;

        public IEncodeHeader HeaderEncoder { get; set; }

        public SimpleMessageEncoder()
        {
            HeaderEncoder = new SimpleHeaderEncoder(MaxHeaderSize,MaxHeaderCount);
        }

        public byte[] Encode(Message message)
        {
            if (message == null) throw new ArgumentNullException(nameof(message));

            byte[] headersInByte = HeaderEncoder.EncodeHeaders(message.Headers);

            var totalSize = headersInByte.Length + message.Payload.Length;
            if (totalSize > MaxPayloadSize) throw new ArgumentException("Message size exceed allowed payload size");

            byte[] result = new byte[totalSize];
            Array.Copy(headersInByte, result, headersInByte.Length);
            Array.Copy(message.Payload, 0, result, headersInByte.Length, message.Payload.Length);
            return result;
        }
    }
}
