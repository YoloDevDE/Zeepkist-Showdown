using System.Collections.Generic;
using System.Linq;
using ZeepSDK.Chat;

namespace Showdown3;

public class MessageBuilder
{
    public bool AutoBreak { get; }
    public int ColonPosition { get; }
    public List<string> Parts { get; } = new();
    public int SeperatorLength { get; }
    public bool StartWithBreak { get; }

    public MessageBuilder(int colonPosition = 12, int seperatorLength = 12, bool startWithBreak = true,
        bool autoBreak = true)
    {
        this.ColonPosition = colonPosition;
        this.SeperatorLength = seperatorLength;
        this.StartWithBreak = startWithBreak;
        this.AutoBreak = autoBreak;
    }

    public MessageBuilder ClearChat()
    {
        var msg = string.Join("", Enumerable.Repeat("<br>", 40));
        Parts.Add(msg);
        return this;
    }


    public MessageBuilder AddLine(string line)
    {
        Parts.Add(line);
        return this;
    }

    public MessageBuilder AddSeparator()
    {
        Parts.Add(new string('-', SeperatorLength));
        return this;
    }


    public MessageBuilder AddKeyValue(string key, string value)
    {
        // Fügt Leerzeichen hinzu, um den Schlüssel auf die gewünschte Breite zu bringen
        var paddedKey = key.PadRight(ColonPosition - 2, ' '); // -2, weil " : " auch zwei Zeichen sind
        Parts.Add($"{paddedKey} : {value}");
        return this;
    }

    private void AddBreak()
    {
        Parts.Add("<br>");
    }

    public string Build()
    {
        if (StartWithBreak)
            Parts[0] = "<br>" + Parts[0];
        if (AutoBreak)
            return string.Join("<br>", Parts);
        return string.Join(" ", Parts);
    }


    public void BuildAndSend()
    {
        ChatApi.SendMessage(Build());
    }

    public void BuildAndServermessage(string color = "orange")
    {
        ChatApi.SendMessage($"/servermessage {color} 0 " + Build());
    }
}