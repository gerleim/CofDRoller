namespace CofdRoller;

public static class CofdStatistics
{
    public static StatisticsResult Avg(int dices, int powerOf10Times = 6)
    {
        var numberOfRolls = (int)Math.Pow(10, powerOf10Times);
        var cofdRoller = new Roller();
        var successCounterLocal = RunParallel(CancellationToken.None, cofdRoller.Roll, dices, numberOfRolls);
        return new StatisticsResult(successCounterLocal.CasesOfSuccess, successCounterLocal.SumOfSuccesses, numberOfRolls);
    }

    public static StatisticsResult AvgRote(int dices, int powerOf10Times = 6)
    {
        var numberOfRolls = (int)Math.Pow(10, powerOf10Times);
        var cofdRoller = new Roller();

        var successCounterLocal = RunParallel(CancellationToken.None, cofdRoller.RollRote, dices, numberOfRolls);
        return new StatisticsResult(successCounterLocal.CasesOfSuccess, successCounterLocal.SumOfSuccesses, numberOfRolls);
    }

    public async static Task<StatisticsResult> AvgExtendedActionAsync(CancellationToken ct, int dices, int requiredSuccesses, int rollLimit, int powerOf10Times = 6)
    {
        var numberOfRolls = (int)Math.Pow(10, powerOf10Times);
        var cofdExtendedAction = new CofdExtendedAction(dices, requiredSuccesses, rollLimit);
        var successCounterLocal = await RunParallelAsync(ct, cofdExtendedAction.RollAll, numberOfRolls);
        return new StatisticsResult(successCounterLocal.CasesOfSuccess, successCounterLocal.SumOfSuccesses, numberOfRolls);
    }

    public static StatisticsResult AvgExtendedAction(int dices, int requiredSuccesses, int rollLimit, int powerOf10Times = 6)
    {
        return AvgExtendedActionAsync(CancellationToken.None, dices, requiredSuccesses, rollLimit, powerOf10Times).Result;
    }

    private static SuccessCounter RunParallel(CancellationToken ct, Func<int, Result> func, int dices, int numberOfRolls)
    {
        object sync = new();
        var successCounterLocal = new SuccessCounter();
        Parallel.For(0, numberOfRolls, new ParallelOptions { CancellationToken = ct },
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

    private async static Task<SuccessCounter> RunParallelAsync(CancellationToken ct, Func<ExtendedActionResults> func, int numberOfRolls)
    {
        object sync = new();
        var successCounterLocal = new SuccessCounter();
        await Parallel.ForAsync(0, numberOfRolls, new ParallelOptions { CancellationToken = ct },
            async (i, ct) =>
            {
                var r = func();
                successCounterLocal.CasesOfSuccess += r.ResultType == ResultType.Success ? 1 : 0;
                successCounterLocal.SumOfSuccesses += r.Successes;
            });

        return successCounterLocal;
    }
}
