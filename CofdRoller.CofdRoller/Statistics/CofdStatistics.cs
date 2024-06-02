namespace CofDRoller;

public static class CofdStatistics
{
    public static decimal Avg(int dices, int powerOf10Times = 6)
    {
        var numberOfRolls = (int)Math.Pow(10, powerOf10Times);
        var cofdRoller = new CofdRoller();

        var successes = ParallelEnumerable.Range(0, numberOfRolls).AsParallel().Sum(x => cofdRoller.Roll(dices).RollResults.Successes);

        return (decimal)successes / numberOfRolls;
    }


    public static decimal AvgRote(int dices, int powerOf10Times = 6)
    {
        var numberOfRolls = (int)Math.Pow(10, powerOf10Times);
        var cofdRoller = new CofdRoller();

        var successes = ParallelEnumerable.Range(0, numberOfRolls).AsParallel().Sum(x => cofdRoller.RollRote(dices).RollResults.Successes);

        return (decimal)successes / numberOfRolls;
    }


    public static decimal AvgExtendedAction(int dices, int requiredSuccesses, int rollLimit, int powerOf10Times = 6)
    {
        var numberOfRolls = (int)Math.Pow(10, powerOf10Times);
        var cofdRoller = new CofdRoller();

        var successes = ParallelEnumerable.Range(0, numberOfRolls).AsParallel().Sum(x =>
            {
                var extendedActionResult = new CofdExtendedAction(dices, requiredSuccesses, rollLimit).RollAll();
                if (extendedActionResult.ResultType == ResultType.Success
                    || extendedActionResult.ResultType == ResultType.ExceptionalSuccess)
                    return 1;

                return 0;
            });

        return (decimal)successes / numberOfRolls;
    }
}
