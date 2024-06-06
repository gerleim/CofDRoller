using CofdRoller.Common;
using System.Diagnostics;

namespace CofdRoller;

[DebuggerDisplay("{ToString()}")]
public class ExtendedActionResults(int dices, int requiredSuccesses, int rollLimit) : ExtendedActionResultsBase
{
    public int Dices { get; set; } = dices;
    public int RequiredSuccesses { get; set; } = requiredSuccesses;
    public int RollLimit { get; set; } = rollLimit;
   
    public ResultType ResultType { get; set; }
    private void Evaluate()
    {
        if (Successes >= RequiredSuccesses)
            ResultType = ResultType.Success;
        else if (this.Count >= RollLimit)
            ResultType = ResultType.Failure;
    }

    public override string ToString()
    {
        return ToText().ToString();
    }
    /*
    public override string ToString()
    {
        Evaluate();
        var sb = new StringBuilder();
        sb.AppendLine($"Extended Action - Dice: {Dices}, Required Successes {RequiredSuccesses}, Roll Limit: {RollLimit}.");
        sb.AppendLine($"{Successes} successes.");
        sb.AppendLine($"RESULT: { ResultType.GetDisplayName()}");
        return sb.ToString();
    }*/

    public Text ToText()
    {
        Evaluate();
        var text = new Text();
        text.Add("Extended Action - Dice: ")
            .Add(Dices)
            .Add(", Required Successes ")
            .Add(RequiredSuccesses)
            .Add(", Roll Limit: ")
            .Add(RollLimit)
            .Add("\r\n")
            .Add(Successes)
            .Add(" successes.")
            .Add("\r\n")
            .Add("RESULT: ")
            .Add(ResultType.GetText());

        return text;
    }
}
