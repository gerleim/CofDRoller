namespace CofdRoller;

public class CofdExtendedAction(int dices, int requiredSuccesses, int rollLimit, int stopAtNthFailure)
{
    public int Dices { get; set; } = dices;
    public int RequiredSuccesses { get; set; } = requiredSuccesses;
    public int RollLimit { get; set; } = rollLimit;

    public int StopAtNthFailure { get; set; } = stopAtNthFailure;

    private int failureCounter = 0;

    public ExtendedActionResults RollAll()
    {
        var results = new ExtendedActionResults(Dices, RequiredSuccesses, RollLimit);
        var end = true;
        var cofdRoller = new Roller();
        while (end)
        {
            var result = cofdRoller.Roll(Dices);
            results.Add(result);

            if(result.RollResults.Successes == 0)
            {
                failureCounter += 1;
                if (failureCounter >= StopAtNthFailure)
                    end = false;
            }

            if (results.Count == RollLimit)
                end = false;

            if (results.Successes >= RequiredSuccesses)
                end = false;
        }

        return results;
    }

}
