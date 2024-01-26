using System;
using ZeepSDK.ChatCommands;

namespace Showdown3.Commands;

public class CommandMatchInitiative : ILocalChatCommand
{
    public static Action<string> OnHandle;
    public string Prefix => "/";

    public string Command => "sd match initiative";

    public string Description => "Sets the Initiative - Args = \"A\" or \"B\"";


    public void Handle(string arguments)
    {
        OnHandle?.Invoke(arguments);
    }
}