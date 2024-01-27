namespace Showdown3.Helper;

public class JoinMessageBuilder
{
    public MessageBuilder SetColor(string color)
    {
        return new MessageBuilder().AddText($"/joinmessage {color} ");
    }
}