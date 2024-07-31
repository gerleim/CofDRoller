using CofdRoller.Common;
using System.Diagnostics;

namespace CofdRoller;

[DebuggerDisplay("{ToString()}")]
public class ExtendedActionResults(int dices, int requiredSuccesses, int rollLimit) : ExtendedActionResultsBase
{
    public int Dices { get; set; } = dices;
    public int RequiredSuccesses { get; set; } = requiredSuccesses;
    public int RollLimit { get; set; } = rollLimit;

    private ResultType _resultType;
    public ResultType ResultType { get
        {
            Evaluate();
            return _resultType;
        }
        set
        {
            _resultType = value;
        }
    }
    private void Evaluate()
    {
        if (Successes >= RequiredSuccesses)
            _resultType = ResultType.Success;
        else
            _resultType = ResultType.Failure;
    }

    public override string ToString()
    {
        return ToText().ToString();
    }
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
