using System;
using ZeepSDK.ChatCommands;

namespace Showdown3.Commands;

public class CommandStop : ILocalChatCommand
{
    public static Action OnHandle;
    public string Prefix => "/";

    public string Command => "sd stop";

    public string Description => "Stops the Showdown Main Event";


    public void Handle(string arguments)
    {
        OnHandle?.Invoke();
    }
}