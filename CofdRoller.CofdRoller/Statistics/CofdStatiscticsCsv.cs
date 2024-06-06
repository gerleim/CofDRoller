using CofdRoller.Common;

namespace CofdRoller;

public class CofdStatiscticsCsv(IOutput output, ILog log)
{
    private readonly IOutput output = output;
    private readonly ILog log = log;

    private void WriteHeader()
    {
        output.WriteLine("Dices,RequiredSuccesses,RollLimit,ChanceOfSuccess,AvgSuccesses");
    }

    public async Task Generate()
    {
        WriteHeader();

        var extendedActionCases = new ExtendedActionCases();
        var i = 0;

        log.Write($"{extendedActionCases.Count} cases. ");
        foreach (var extendedActionCase in extendedActionCases.Get())
        {
            var statisticsResult = await CofdStatistics.AvgExtendedActionAsync(extendedActionCase.Dices, extendedActionCase.RequiredSuccesses, extendedActionCase.RollLimit, 5);

            var chanceOfSuccess = (decimal)statisticsResult.CasesOfSuccess / statisticsResult.NumberOfRolls;
            var avgSuccesses = (decimal)statisticsResult.SumOfSuccesses / statisticsResult.NumberOfRolls;

            output.WriteLine($"{extendedActionCase.Dices},{extendedActionCase.RequiredSuccesses},{extendedActionCase.RollLimit},{chanceOfSuccess.ToStringWithPoint()},{avgSuccesses.ToStringWithPoint()}");

            if (i % 100 == 0)
                log.Write($"{((decimal)i / extendedActionCases.Count).ToString("0.00%")} ");

            log.Write($"{i} ");


            i++;
        }
    }
}
