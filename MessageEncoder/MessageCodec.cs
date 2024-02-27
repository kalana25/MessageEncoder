using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageEncoder
{
    public class MessageCodec : IMessageCodec
    {
        private const int MaxHeaderCount = 63;
        private const int MaxHeaderSize = 1023;
        private const int MaxPayloadSize = 256 * 1024;

        public Message decode(byte[] data)
        {
            if(data==null)
                throw new ArgumentNullException(nameof(data));

            var headerSize = Math.Min(data.Length, MaxHeaderCount * MaxHeaderSize);
            byte[] headers = new byte[headerSize];
            Array.Copy(data,headers,headers.Length);

            var payloadSize = Math.Max(data.Length - headers.Length, 0);
            var payload = new byte[payloadSize];
            
            Array.Copy(data,headers.Length,payload,0, payloadSize);
            var decodedHeaders = DecodeHeaders(headers);
            return new Message { Headers = decodedHeaders, Payload = payload };
        }

        public byte[] encode(Message message)
        {
            if(message == null) throw new ArgumentNullException(nameof(message));

            byte[] headersInByte = EncodeHeaders(message.Headers);

            var totalSize = headersInByte.Length + message.Payload.Length;
            if(totalSize > MaxPayloadSize) throw new ArgumentException("Message size exceed allowed payload size");

            byte[] result = new byte[totalSize];
            Array.Copy(headersInByte,result,headersInByte.Length);
            Array.Copy(message.Payload,0,result,headersInByte.Length,message.Payload.Length);
            return result;
        }

        private byte[] EncodeHeaders(Dictionary<string, string> headers)
        {
            var headerList = new List<byte>();
            foreach (var header in headers)
            {
                var headerName = Encoding.ASCII.GetBytes(header.Key);
                var headerValue = Encoding.ASCII.GetBytes(header.Value);
                if (headerName.Length > MaxHeaderSize || headerValue.Length > MaxHeaderSize)
                    throw new ArgumentException("Header name or value exceeds the maximum threshold.");

                headerList.AddRange(BitConverter.GetBytes((ushort)headerName.Length));
                headerList.AddRange(headerName);
                headerList.AddRange(BitConverter.GetBytes((ushort)headerValue.Length));
                headerList.AddRange(headerValue);
            }
            return headerList.ToArray();
        }

        private Dictionary<string,string> DecodeHeaders(byte[] headers)
        {
            var result = new Dictionary<string,string>();
            var index = 0;
            while (index < headers.Length)
            {
                var hdrNameLength = BitConverter.ToUInt16(headers, index);
                index += sizeof(ushort);
                string headerNameString = Encoding.ASCII.GetString(headers,index,hdrNameLength);
                index += hdrNameLength;

                var hdrValueLength = BitConverter.ToUInt16(headers,index);
                index += sizeof(ushort);
                string headerValueString = Encoding.ASCII.GetString(headers,index,hdrValueLength);
                index += hdrValueLength;

                result[headerNameString] = headerValueString;

            }
            return result;

        }
    }
}
