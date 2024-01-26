using System;
using ZeepSDK.ChatCommands;

namespace Showdown3.Commands;

public class CommandMatchSet : ILocalChatCommand
{
    public static Action<string> OnHandle;
    public string Prefix => "/";

    public string Command => "sd match set";

    public string Description => "";


    public void Handle(string arguments)
    {
        OnHandle?.Invoke(arguments);
    }
}