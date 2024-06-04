namespace CofDRoller;

public class StatisticsResult(int casesOfSuccess, int sumOfSuccesses, int numberofRolls)
{
    public int NumberOfRolls { get; init; } = numberofRolls;
    public int CasesOfSuccess { get; init; } = casesOfSuccess;
    public int SumOfSuccesses { get; init; } = sumOfSuccesses;

    public Text ToText()
    {
        var result = new Text();

        result.Add("Success rate: ");
        result.AddPercentage((decimal)CasesOfSuccess / NumberOfRolls);
        result.Add(" Avg successes: ");
        result.Add((decimal)SumOfSuccesses / NumberOfRolls);

        return result;
    }
}
