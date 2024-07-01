using CofdRoller.Common;
using System.Text;

namespace CofdRoller.Console.Output;

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
            if (parts.Foreground != Common.Colors.Default)
                sb.Append(Colors.Foreground + MapColor(parts.Foreground));
            if (parts.Background != Common.Colors.Default)
                sb.Append(Colors.BackGround + MapColor(parts.Background));
            sb.Append(parts.Text);
            sb.Append(ResetColor);
        }

        return sb.ToString();
    }

    private static string MapColor(Common.Colors color)
    {
        var red = Common.Colors.Red;
        return color switch
        {
            Common.Colors.Default => "",
            Common.Colors.Red => Colors.Red,
            Common.Colors.Green => Colors.Green,
            Common.Colors.White => Colors.White,
            Common.Colors.Black => Colors.Black,
            Common.Colors.Grey => Colors.Grey,
            Common.Colors.GreyDark => Colors.GreyDark,
            _ => throw new NotImplementedException()
        };
    }

    private const string ResetColor = Colors.Foreground + Colors.White
        + Colors.BackGround + Colors.Black;
}
