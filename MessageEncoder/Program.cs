// See https://aka.ms/new-console-template for more information
using MessageEncoder;
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

var messageCodec = new MessageCodec();
var message = createMessage();
var encodedMessage = messageCodec.encode(message);
var decodedMessage  = messageCodec.decode(encodedMessage);
