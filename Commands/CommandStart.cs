﻿using System;
using ZeepSDK.ChatCommands;

namespace Showdown3.Commands;

public class CommandStart : ILocalChatCommand
{
    public static Action OnHandle;
    public string Prefix => "/";

    public string Command => "sd start";

    public string Description => "Starts the Showdown Main Event";


    public void Handle(string arguments)
    {
        OnHandle?.Invoke();
    }
}