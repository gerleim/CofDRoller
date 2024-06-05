using CofdRoller.Common;

namespace CofdRoller;

public static class ResultTypeColorizer
{
    public static Text GetText(this ResultType resultType)
    {
        var colorFg = resultType switch
        {
            ResultType.Undetermined => Colors.Red,
            ResultType.Success => Colors.Green,
            ResultType.ExceptionalSuccess => Colors.White,
            ResultType.Failure => Colors.Red,
            ResultType.DramaticFailure => Colors.White,
            _ => throw new NotImplementedException(),
        };

        var colorBg = resultType switch
        {
            ResultType.ExceptionalSuccess => Colors.Green,
            ResultType.Failure => Colors.GreyDark,
            ResultType.DramaticFailure => Colors.Red,
            _ => Colors.Default,
        };

        return new Text().Add(resultType.GetDisplayName(), colorFg, colorBg);
    }
}
