namespace CofDRoller;

public class TextPart
{
    public TextPart(string text)
    {
        Text = text;
    }

    public TextPart(string text, TokenType tokenType)
    {
        Text = text;
        TokenType = tokenType;
    }

    public TextPart(string text, Colors foreground)
    {
        Text = text;
        Foreground = foreground;
    }

    public TextPart(string text, Colors foreground, Colors background)
    {
        Text = text;
        Foreground = foreground;
        Background = background;
    }

    public Colors Background { get; set; }
    public Colors Foreground { get; set; }
    public string Text { get; set; }
    public TokenType TokenType { get; set; }

    public override string ToString()
    {
        return Text;
    }
}
