using System;
using ZeepSDK.ChatCommands;

namespace Showdown3.Commands;

public class CommandStop : ILocalChatCommand
{
    public string Prefix => "/";

    public string Command => "sd stop";

    public string Description => "Stops the Showdown Main Event";


    public void Handle(string arguments)
    {
        OnHandle?.Invoke();
    }

    public static Action OnHandle;
}