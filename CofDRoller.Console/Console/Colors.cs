namespace CofdRoller.Console;

public class Colors
{
    /*  \x1b[38;5;[n]m is foreground
     *  \x1b[48;5;[n]m is background
     */
    public const string Foreground = "\x1b[38;5;";
    public const string BackGround = "\x1b[48;5;";

    public const string Red = "1m";
    public const string RedDark = "9m";
    public const string Green = "10m";
    public const string GreenDark = "2m";
    public const string Black = "0m";
    public const string White = "255m";
    public const string GreyDark = "7m";
    public const string Grey = "15m";
}
