# MessageEncoder

This is an demostration of Encoding scheme of a Message DTO through a messaging protocol.

Message DTO has headers and payload.

It's clear that, Encoding scheme function has two parts which encode headers and message itself.
Therefore following design was implemented to swap or extend it's functions without any existing functionality faliures.

![image](https://github.com/kalana25/MessageEncoder/assets/6653178/4fe7858e-19ec-4278-8796-c120b968c4c7)


In case you like to have an Advance message encoding scheme or extends functionality. Please follow the bellow steps.

1) Create your new message encoding scheme class. ex - AdvanceMessageCodec or ExtendedMessageCodec
2) Inherit the class with "GenericMessageCodec" parent class.
3) Now you need to write your own "ExtendedMessageEncoder","ExtendedMessageDecoder","ExtendedHeaderEncoder","ExtendedHeaderDecoder" class.
4) Let's Implement IEncodeMessage,IDecodeMessage,IEncodeHeader,IDecodeHeader interfaces accordingly.
    Please see the SimpleMessageEncoder,SimpleMessageDecoder,SimpleHeaderEncoder,SimpleHeaderDecoder classes for example.
5) Create the constructor in ExtendedMessageCodec class and assign MessageEncoder object to the property came from "GenericMessageCodec" class. (Your new "MessageEncoder" should be implemented from IEncodeMessage interface)
    Please see the "SimpleMessageEncoder" class for example.
6) assign MessageDecoder object to the property came from "GenericMessageCodec" class. (Your new "MessageDecoder" should be implemented from IDecodeMessage interface)
    Please see the "SimpleMessageDecoder" class for example.

This way message encoding and decoding algorithms can be attached and detached at runtime without making change to existing code.
Each Encoding and Decoding message class consist of Encoding and Decoding headers. Algorithms that encoding ,decoding headers can also be decided at the runtime the way we have done with the message.

Hope this is clear.

Thanks.