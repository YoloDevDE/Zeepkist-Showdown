using System;
using ZeepkistClient;
using ZeepSDK.Chat;

namespace Showdown3.Helper;

public class LobbyConfigurer
{
    private string _joinMessage;
    private string _joinMessageColor;
    private string _lobbyName;
    private int _lobbyPlayerCount = 64;
    private bool _lobbyPublic;
    private int _lobbyTimeInSeconds = 600;

    public LobbyConfigurer SetName(string name)
    {
        _lobbyName = name;
        return this;
    }

    public LobbyConfigurer SetMaxPlayers(int maxPlayers)
    {
        _lobbyPlayerCount = maxPlayers;
        return this;
    }

    public LobbyConfigurer SetTime(int timeInSeconds)
    {
        _lobbyTimeInSeconds = timeInSeconds;
        return this;
    }

    public LobbyConfigurer SetVisibility(bool visibility)
    {
        _lobbyPublic = visibility;
        return this;
    }

    public LobbyConfigurer SetJoinmessage(string color, string message)
    {
        _joinMessage = message;
        _joinMessageColor = color;
        return this;
    }


    public void Build()
    {
        if (_joinMessage != null)
            if (_joinMessageColor != null)
                new JoinMessageBuilder()
                    .SetColor(_joinMessageColor)
                    .AddText(_joinMessage)
                    .BuildAndExecute();

        if (_lobbyName != null) ZeepkistNetwork.CurrentLobby.UpdateName(_lobbyName);

        ZeepkistNetwork.CurrentLobby.UpdateVisibility(_lobbyPublic);
        ZeepkistNetwork.CurrentLobby.UpdateMaxPlayers(Math.Clamp(_lobbyPlayerCount, 2, 64));
        ChatApi.SendMessage($"/settime {Math.Clamp(_lobbyTimeInSeconds, 30, 86400)}");
    }

    public static void HoFLobby()
    {
        new LobbyConfigurer()
            .SetName("Zeepkist Showdown Season 2")
            .SetVisibility(false)
            .SetTime(86400)
            .SetMaxPlayers(64)
            .SetJoinmessage(Color.orange, new MessageBuilder()
                .AddSeparator()
                .AddText("Welcome to the Zeepkist Showdown!")
                .AddSeparator()
                .AddText("It's finally time - the moment we've all been waiting for!")
                .AddText("Don't worry about a thing - we've got the details covered.")
                .AddText("Keep your eyes on the race and ears open for our commands in the chat.")
                .AddText("This is your moment. Race hard, follow our lead, and enjoy every second!")
                .AddText("Good Luck, Have Fun!")
                .Build()
            ).Build();
    }
}