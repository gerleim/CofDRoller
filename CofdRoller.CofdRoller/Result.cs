using System.Diagnostics;

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

    public Text ToText()
    {
        var dicesText = new Text();
        switch (Dices)
        {
            case int n when (n == 0):
                dicesText.Add("chance die", Colors.Grey);
                break;
            case int n when (n == 1):
                dicesText.Add("1", TokenType.Number);
                dicesText.Add(" die");
                break;
            case int n when (n > 1):
                dicesText.Add($"{Dices}", TokenType.Number);
                dicesText.Add(" dices");
                break;
        }

        var rolledNumbers = new Text();
        foreach (var rollResult in RollResults)
        {
            rolledNumbers.Add(rollResult.RolledNumbers[0]);
            if (rollResult.RolledNumbers.Count > 1)
            {
                foreach (var x in rollResult.RolledNumbers[1..])
                {
                    rolledNumbers.Add("->", TokenType.Operation);
                    rolledNumbers.Add(x);
                }
            }
            rolledNumbers.Add(" ");
        }
        var rolltype = new Text();
        if (RollType != RollTypes._10Again)
            rolltype.Add($"({RollType}) ", TokenType.Type);

        var finalText = new Text()
            .Add($"{RollResults.Successes}")
            .Add(" success rolling ")
            .Add(dicesText)
            .Add(". ")
            .Add(ResultType.GetText())
            .Add(" ")
            .Add(rolltype)
            .Add("Rolls: ")
            .Add(rolledNumbers)
            ;

        return finalText;
    }

    public override string ToString()
    {
        return ToText().ToString();
    }
}
