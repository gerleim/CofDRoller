using CofdRoller.Common;

namespace CofdRoller;

public class CofdStatiscticsCsv(IOutput output)
{
    private readonly IOutput output = output;

    private void WriteHeader()
    {
        output.WriteLine("Dices,RequiredSuccesses,RollLimit,ChanceOfSuccess,AvgSuccesses");
    }

    public void Generate()
    {
        WriteHeader();

        var extendedActionCases = new ExtendedActionCases();
        foreach(var extendedActionCase in extendedActionCases.Get())
        { 
            var statisticsResult = CofdStatistics.AvgExtendedAction(extendedActionCase.Dices, extendedActionCase.RequiredSuccesses, extendedActionCase.RollLimit, 5);

            var chanceOfSuccess = (decimal)statisticsResult.CasesOfSuccess / statisticsResult.NumberOfRolls;
            var avgSuccesses = (decimal)statisticsResult.SumOfSuccesses / statisticsResult.NumberOfRolls;

            output.WriteLine($"{extendedActionCase.Dices},{extendedActionCase.RequiredSuccesses},{extendedActionCase.RollLimit},{chanceOfSuccess.ToStringWithPoint()},{avgSuccesses.ToStringWithPoint()}");
        }
    }
}
