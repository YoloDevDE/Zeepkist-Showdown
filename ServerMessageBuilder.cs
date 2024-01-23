namespace Showdown3;

public class ServerMessageBuilder
{
    public MessageBuilder SetColor(string color)
    {
        return new MessageBuilder().AddText($"/servermessage {color} 0 ");
    }
}