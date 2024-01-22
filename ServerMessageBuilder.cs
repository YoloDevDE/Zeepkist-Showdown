using System;
using System.Drawing;
using Color = UnityEngine.Color;

namespace Showdown3;

public class ServerMessageBuilder
{
    public MessageBuilder SetColor(string color)
    {
        return new MessageBuilder().AddText($"/servermessage {color} 0 ");
    }
}

