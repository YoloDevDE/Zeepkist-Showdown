using System;
using ZeepSDK.ChatCommands;

namespace Showdown3.Commands;

public class CommandMatchStop : ILocalChatCommand
{
    public static Action OnHandle;
    public string Prefix => "/";

    public string Command => "sd match stop";

    public string Description => "Continues to the next stage from the Hall of Fame";


    public void Handle(string arguments)
    {
        OnHandle?.Invoke();
    }
}