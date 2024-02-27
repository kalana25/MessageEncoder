// See https://aka.ms/new-console-template for more information
using MessageEncoder;
using MessageEncoder.Core;
using MessageEncoder.Core.Implementations;
using System.Reflection.Metadata.Ecma335;
using System.Text;


Message createMessage()
{
    var headerDictionary = new Dictionary<string, string>();
    headerDictionary.Add("Content-Type", "ASCII/Byte");

    var payloadString = "Alan is going to wind the game";
    var payloadByte = Encoding.ASCII.GetBytes(payloadString);
    return new Message { Headers = headerDictionary, Payload = payloadByte };
}


var message = createMessage();
MessageCodec messageCodec = new MessageCodec();
var encodedMessage = messageCodec.Encode(message);
messageCodec.Decode(encodedMessage);
