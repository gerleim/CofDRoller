using CofdRoller.Common;

namespace CofdRoller;

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

public class StatisticsResultExtended(SuccessesPerDices successesPerDices, int numberOfRolls)
{
    public SuccessesPerDices SuccessesPerDices { get; init; } = successesPerDices;
    public int NumberOfRolls { get; init; } = numberOfRolls;

    public Text ToText()
    {
        var result = new Text();


        foreach (var successesPerDice in SuccessesPerDices.OrderBy(x => x.Key)) // order by dice
        {
            var howManySuccessPerSuccess = successesPerDice.Value;
            result.Add(successesPerDice.Key);
            result.Add(" dice:");

            var HowManySuccessPerSuccessRollup = new HowManySuccessPerSuccess();
            var previousDescendingRollup = 0;
            var numberOfRolls = 0;

            foreach (var kvp in howManySuccessPerSuccess.OrderByDescending(x => x.Key)) // order by achieved successes
            {
                if (kvp.Key == 0)
                    HowManySuccessPerSuccessRollup[kvp.Key].NumberOfSuccesses = kvp.Value.NumberOfSuccesses;
                else
                    HowManySuccessPerSuccessRollup[kvp.Key].NumberOfSuccesses += kvp.Value.NumberOfSuccesses + previousDescendingRollup;
                
                numberOfRolls += kvp.Value.NumberOfRolls;
                previousDescendingRollup += kvp.Value.NumberOfSuccesses;
            }

            foreach (var kvp in HowManySuccessPerSuccessRollup.OrderBy(x => x.Key)) // order by achieved successes
            {
                var howManySuccesses = kvp.Key;
                
                var numberOfSuccessesString = howManySuccesses.ToString();

                if (howManySuccesses < 10)
                    numberOfSuccessesString = " " + numberOfSuccessesString;

                result.Add(" ");
                result.Add(numberOfSuccessesString, TokenType.Number);
                result.Add(" ");

                decimal p = 0;
                /*if (kvp.Value.NumberOfRolls != 0)
                    p = (decimal)(kvp.Value.NumberOfSuccesses) / kvp.Value.NumberOfRolls;*/

                var howManySuccessesPerSuccess = SuccessesPerDices[kvp.Key];
                if (howManySuccessesPerSuccess.NumberOfRolls2 != 0)
                    p = (decimal)(kvp.Value.NumberOfSuccesses) / howManySuccessesPerSuccess.NumberOfRolls2;

                result.AddPercentage(p);
            }
            result.Add("\r\n");
        }
        

        return result;
    }
}
