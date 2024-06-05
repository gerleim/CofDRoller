namespace CofdRoller;

public class ExtendedActionCases
{
    /*private readonly List<int> dicesToRoll = Enumerable.Range(1, 2).ToList();
    private readonly List<int> requiredSuccessesToRoll = Enumerable.Range(5, 6).ToList();
    private readonly List<int> rollLimitsToRoll = Enumerable.Range(3, 4).ToList();*/

    private readonly List<int> dicesToRoll = Enumerable.Range(1, 10).ToList();
    private readonly List<int> requiredSuccessesToRoll = Enumerable.Range(5, 25).ToList();
    private readonly List<int> rollLimitsToRoll = Enumerable.Range(3, 20).ToList();

    public IEnumerable<ExtendedActionCase> Get()
    {
        foreach (var d in dicesToRoll)
        {
            foreach (var rs in requiredSuccessesToRoll)
            {
                foreach (var rl in rollLimitsToRoll)
                {
                    yield return new ExtendedActionCase(d, rs, rl);
                }
            }
        }
    }
}
