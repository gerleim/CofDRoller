using System.Diagnostics;
using System.Text;

namespace CofDRoller;

[DebuggerDisplay("{ToString()}")]
public class Result
{
    public Result(int dices, RollResults rollResults, string rollType = RollTypes._10Again)
    {
        Dices = dices;
        RollResults = rollResults;
        RollType = rollType;

        switch (rollResults.Successes)
        {
            case int n when (n >= 5):
                ResultType = ResultType.ExceptionalSuccess;
                break;
            case int n when (n >= 1):
                ResultType = ResultType.Success;
                break;

            case int n when (dices == 0 && n == 0 && rollResults[0].RolledNumbers[0] == 1):
                ResultType = ResultType.DramaticFailure;
                break;

            case int n when (n == 0):
                ResultType = ResultType.Failure;
                break;
        }
    }

    public int Dices { get; set; }
    public ResultType ResultType { get; set; }
    public RollResults RollResults { get; set; }
    public string RollType { get; set; }

    public override string ToString()
    {
        string dicesString = "";
        switch (Dices)
        {
            case int n when (n == 0):
                dicesString = "chance die";
                break;
            case int n when (n == 1):
                dicesString = "1 die";
                break;
            case int n when (n > 1):
                dicesString = $"{Dices} dices";
                break;
        }

        var rolledNumbers = new StringBuilder();
        foreach (var rollResult in RollResults)
        {
            rolledNumbers.Append(rollResult.RolledNumbers[0]);
            if (rollResult.RolledNumbers.Count > 1)
            {
                foreach (var x in rollResult.RolledNumbers[1..])
                {
                    rolledNumbers.Append("->");
                    rolledNumbers.Append(x);
                }
            }
            rolledNumbers.Append(" ");
        }
        var rolltype = "";
        if (RollType != RollTypes._10Again)
            rolltype = $"({RollType})";

        return $"{RollResults.Successes} success rolling {dicesString}. {ResultType.GetDisplayName()}. {rolltype} Rolls: {rolledNumbers}";
    }
}
