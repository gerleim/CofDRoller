namespace CofDRoller;

public class SuccessCounter
{
    public int CasesOfSuccess;
    public int SumOfSuccesses;
}

public static class CofdStatistics
{
    public static StatisticsResult Avg(int dices, int powerOf10Times = 7)
    {
        var numberOfRolls = (int)Math.Pow(10, powerOf10Times);
        var cofdRoller = new CofdRoller();
        var successCounterLocal = RunParallel(cofdRoller.Roll, dices, numberOfRolls);
        return new StatisticsResult(successCounterLocal.CasesOfSuccess, successCounterLocal.SumOfSuccesses, numberOfRolls);
    }

    public static StatisticsResult AvgRote(int dices, int powerOf10Times = 6)
    {
        var numberOfRolls = (int)Math.Pow(10, powerOf10Times);
        var cofdRoller = new CofdRoller();

        var successCounterLocal = RunParallel(cofdRoller.RollRote, dices, numberOfRolls);
        return new StatisticsResult(successCounterLocal.CasesOfSuccess, successCounterLocal.SumOfSuccesses, numberOfRolls);
    }

    public static StatisticsResult AvgExtendedAction(int dices, int requiredSuccesses, int rollLimit, int powerOf10Times = 6)
    {
        var numberOfRolls = (int)Math.Pow(10, powerOf10Times);
        var cofdExtendedAction = new CofdExtendedAction(dices, requiredSuccesses, rollLimit);
        var successCounterLocal = RunParallel(cofdExtendedAction.RollAll, numberOfRolls);
        return new StatisticsResult(successCounterLocal.CasesOfSuccess, successCounterLocal.SumOfSuccesses, numberOfRolls);
    }

    private static SuccessCounter RunParallel(Func<int, Result> func, int dices, int numberOfRolls)
    {
        object sync = new();
        var successCounterLocal = new SuccessCounter();
        Parallel.For(0, numberOfRolls,
            () => new SuccessCounter(),
            (i, pls, successCounter) =>
            {
                var r = func(dices);
                successCounter.CasesOfSuccess += r.ResultType == ResultType.Success ? 1 : 0;
                successCounter.SumOfSuccesses += r.RollResults.Successes;

                return successCounter;
            },
            finalSuccessCounter => {
                lock (sync)
                {
                    successCounterLocal.CasesOfSuccess += finalSuccessCounter.CasesOfSuccess;
                    successCounterLocal.SumOfSuccesses += finalSuccessCounter.SumOfSuccesses;
                }
            }
        );

        return successCounterLocal;
    }

    private static SuccessCounter RunParallel(Func<ExtendedActionResults> func, int numberOfRolls)
    {
        object sync = new();
        var successCounterLocal = new SuccessCounter();
        Parallel.For(0, numberOfRolls,
            () => new SuccessCounter(),
            (i, pls, successCounter) =>
            {
                var r = func();
                successCounter.CasesOfSuccess += r.ResultType == ResultType.Success ? 1 : 0;
                successCounter.SumOfSuccesses += r.Successes;

                return successCounter;
            },
            finalSuccessCounter => {
                lock (sync)
                {
                    successCounterLocal.CasesOfSuccess += finalSuccessCounter.CasesOfSuccess;
                    successCounterLocal.SumOfSuccesses += finalSuccessCounter.SumOfSuccesses;
                }
            }
        );

        return successCounterLocal;
    }
}
