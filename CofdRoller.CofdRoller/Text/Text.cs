namespace CofDRoller;

public class Text
{
    public List<TextPart> TextParts { get; } = [];

    public Text Add(string text)
    {
        TextParts.Add(new TextPart(text));
        return this;
    }

    public Text Add(string text, TokenType tokenType)
    {
        TextParts.Add(new TextPart(text, tokenType));
        return this;
    }

    public Text Add(string text, Colors foreground)
    {
        TextParts.Add(new TextPart(text, foreground));
        return this;
    }

    public Text Add(string text, Colors foreground, Colors background)
    {
        TextParts.Add(new TextPart(text, foreground, background));
        return this;
    }

    public Text Add(int number)
    {
        TextParts.Add(new TextPart(number.ToString(), TokenType.Number));
        return this;
    }

    public Text Add(decimal number)
    {
        TextParts.Add(new TextPart(number.ToString("0.00"), TokenType.Number));
        return this;
    }

    public Text AddPercentage(decimal number)
    {
        TextParts.Add(new TextPart(number.ToString("0.00%"), TokenType.Number));
        return this;
    }

    public Text Add(TextPart textPart)
    {
        TextParts.Add(textPart);
        return this;
    }

    public Text Add(Text text)
    {
        TextParts.AddRange(text.TextParts);
        return this;
    }

    public Text Get()
    {
        return this;
    }

    public override string ToString()
    {
        return string.Join("", TextParts);
    }
}
