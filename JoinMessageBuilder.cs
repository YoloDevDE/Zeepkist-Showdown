namespace Showdown3;

public class JoinMessageBuilder
{
    public MessageBuilder SetColor(string color)
    {
        return new MessageBuilder().AddText($"/joinmessage {color} ");
    }
}