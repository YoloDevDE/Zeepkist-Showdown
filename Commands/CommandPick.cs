using System;
using ZeepkistClient;
using ZeepSDK.ChatCommands;

namespace Showdown3.Commands;

public class CommandPick : IMixedChatCommand
{
    public string Prefix => "!";

    public string Command => "pick";

    public string Description => "";

    public void Handle(ulong playerId, string arguments)
    {
        OnHandle?.Invoke(playerId, arguments);
    }


    public void Handle(string arguments)
    {
        Handle(arguments: arguments, playerId: ZeepkistNetwork.LocalPlayer.SteamID);
    }

    public static Action<ulong, string> OnHandle;
}