using ZeepSDK.Messaging;

namespace Showdown3;

public static class TaggedMessenger
{
    public static ITaggedMessenger Value => MessengerApi.CreateTaggedMessenger("Showdown");
}