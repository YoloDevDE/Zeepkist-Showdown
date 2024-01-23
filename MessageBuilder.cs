using System.Collections.Generic;
using ZeepSDK.Chat;

namespace Showdown3;

public class MessageBuilder
{
    private readonly List<string> _message;


    public MessageBuilder()
    {
        _message = new List<string>();
    }

    private int SeparatorLength => 15;

    public MessageBuilder AddText(string text)
    {
        _message.Add(text);
        return this;
    }

    public MessageBuilder AddBreak()
    {
        _message.Add("<br>");
        return this;
    }

    public MessageBuilder AddSeparator()
    {
        return AddSeparator(SeparatorLength);
    }

    public MessageBuilder AddSeparator(int separatorLength)
    {
        if (_message.Count == 0)
            AddBreak();
        _message.Add(new string('-', separatorLength));
        return AddBreak();
    }

    public void BuildAndExecute()
    {
        ChatApi.SendMessage(string.Join("", _message));
    }

    public string Build()
    {
        return string.Join(" ", _message);
    }
}