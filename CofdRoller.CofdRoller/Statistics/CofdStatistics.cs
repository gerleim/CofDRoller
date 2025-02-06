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

    public static StatisticsResultExtended AvgRote(int powerOf10Times = 6)
    {
        var numberOfRolls = (int)Math.Pow(10, powerOf10Times);
        var cofdRoller = new Roller();

        var successesPerDices = RunParallelStatistics(CancellationToken.None, cofdRoller.RollRote, numberOfRolls);

        return new StatisticsResultExtended(successesPerDices, numberOfRolls);
    }

    public async static Task<StatisticsResult> AvgExtendedActionAsync(CancellationToken ct, int dices, int requiredSuccesses, int rollLimit, int stopAtNthFailure, int powerOf10Times = 6)
    {
        var numberOfRolls = (int)Math.Pow(10, powerOf10Times);
        var cofdExtendedAction = new CofdExtendedAction(dices, requiredSuccesses, rollLimit, stopAtNthFailure);
        var successCounterLocal = await RunParallelAsync(ct, cofdExtendedAction.RollAll, numberOfRolls);
        return new StatisticsResult(successCounterLocal.CasesOfSuccess, successCounterLocal.SumOfSuccesses, numberOfRolls);
    }

    public static StatisticsResult AvgExtendedAction(int dices, int requiredSuccesses, int rollLimit, int stopAtNthFailure, int powerOf10Times = 6)
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

    const int numberOfMeasuredMaxDices = 15;
    public const int numberOfMeasuredSuccesses = 12;

    private static SuccessesPerDices RunParallelStatistics(CancellationToken ct, Func<int, Result> func, int numberOfRolls)
    {
        var successesPerDices = new SuccessesPerDices();
        for (int i = 0; i <= numberOfMeasuredMaxDices; i++ )
        {
            var howManySuccessPerSuccesPartial = RunParallelStatisticsImplementation(ct, func, i, numberOfRolls);
            successesPerDices[i] = howManySuccessPerSuccesPartial;
        }
        return successesPerDices;
    }

    private static HowManySuccessPerSuccess RunParallelStatisticsImplementation(CancellationToken ct, Func<int, Result> func, int dices, int numberOfRolls)
    {
        object sync = new();
        var howManySuccessPerSuccesLocal = new HowManySuccessPerSuccess();
        Parallel.For(0, numberOfRolls, new ParallelOptions { CancellationToken = ct },
            () => new HowManySuccessPerSuccess(),
            (i, pls, howManySucessPerSucces) =>
            {
                var r = func(dices);
                if (r.RollResults.Successes <= numberOfMeasuredSuccesses)
                {
                    if (r.ResultType == ResultType.Success)
                        howManySuccessPerSuccesLocal[r.RollResults.Successes].NumberOfSuccesses += 1;

                    howManySuccessPerSuccesLocal[r.RollResults.Successes].NumberOfRolls += 1;
                    howManySuccessPerSuccesLocal.NumberOfRolls2 += 1;
                }

                return howManySuccessPerSuccesLocal;
            },
            finalhowManySucessPerSucces => {
                lock (sync)
                {
                    howManySuccessPerSuccesLocal = finalhowManySucessPerSucces;
                }
            }
        );

        return howManySuccessPerSuccesLocal;
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
