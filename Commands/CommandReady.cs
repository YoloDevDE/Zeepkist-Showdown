using System;
using ZeepkistClient;
using ZeepSDK.ChatCommands;

namespace Showdown3.Commands;

public class CommandReady : IMixedChatCommand
{
    public static Action<ulong, string> OnHandle;
    public string Prefix => "!";

    public string Command => "ready";

    public string Description => "";

    public void Handle(ulong playerId, string arguments)
    {
        OnHandle?.Invoke(playerId, arguments);
    }


    public void Handle(string arguments)
    {
        Handle(arguments: arguments, playerId: ZeepkistNetwork.LocalPlayer.SteamID);
    }
}