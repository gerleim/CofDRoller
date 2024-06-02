using System.Text;

namespace CofDRoller.Console.Output;

internal class ConsoleOutput : IOutput
{
    public void Write(string message)
    {
        System.Console.Write(message);
    }

    public void WriteLine(string message)
    {
        System.Console.WriteLine(message);
    }

    public void Write(Text text)
    {
        Write(TextToColorString(text));
    }

    public void WriteLine(Text text)
    {
        WriteLine(TextToColorString(text));
    }

    private static string TextToColorString(Text text)
    {
        var sb = new StringBuilder();
        foreach (var parts in text.TextParts)
        {
            if (parts.Foreground != Colors.Default)
                sb.Append(Console.Colors.Foreground + MapColor(parts.Foreground));
            if (parts.Background != Colors.Default)
                sb.Append(Console.Colors.BackGround + MapColor(parts.Background));
            sb.Append(parts.Text);
            sb.Append(ResetColor);
        }

        return sb.ToString();
    }

    private static string MapColor(Colors color)
    {
        return color switch
        {
            Colors.Default => "",
            Colors.Red => Console.Colors.Red,
            Colors.Green => Console.Colors.Green,
            Colors.White => Console.Colors.White,
            Colors.Black => Console.Colors.Black,
            Colors.Grey => Console.Colors.Grey,
            Colors.GreyDark => Console.Colors.GreyDark,
            _ => throw new NotImplementedException()
        };
    }

    private const string ResetColor = Console.Colors.Foreground + Console.Colors.White
        + Console.Colors.BackGround + Console.Colors.Black;
}
