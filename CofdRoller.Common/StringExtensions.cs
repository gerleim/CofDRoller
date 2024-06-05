namespace CofdRoller.Common;

public static class DecimalExtensions
{
    public static string ToStringWithPoint(this decimal number)
    {
        if (number == 0)
            return "0";

        int iPart = (int)number;
        decimal dPart = number % 1.0m;
        string dPartString = dPart.ToString().Substring(2);
        return $"{iPart}.{dPartString}";
    }
}
