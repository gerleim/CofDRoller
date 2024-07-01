namespace CofdRoller;

public class CofdExtendedAction(int dices, int requiredSuccesses, int rollLimit)
{
    public int Dices { get; set; } = dices;
    public int RequiredSuccesses { get; set; } = requiredSuccesses;
    public int RollLimit { get; set; } = rollLimit;

    public ExtendedActionResults RollAll()
    {
        var result = new ExtendedActionResults(Dices, RequiredSuccesses, RollLimit);
        var end = true;
        var cofdRoller = new Roller();
        while (end)
        {
            result.Add(cofdRoller.Roll(Dices));

            if (result.Count == RollLimit)
                end = false;

            if (result.Successes >= RequiredSuccesses)
                end = false;
        }

        return result;
    }

}


public class Time
{
    public int Value { get; set; }
    public IntervalType IntervalType { get; set; }
}

public enum IntervalType
{
    Minutes,
    Hours,
    Days,
    Weeks
}