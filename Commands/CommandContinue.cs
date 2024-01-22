using System;
using ZeepSDK.ChatCommands;

namespace Showdown3.Commands;

public class CommandContinue : ILocalChatCommand
{
    public string Prefix => "/";

    public string Command => "sd continue";

    public string Description => "Continues to the next stage from the Hall of Fame";


    public void Handle(string arguments)
    {
        OnHandle?.Invoke();
    }

    public static Action OnHandle;
}