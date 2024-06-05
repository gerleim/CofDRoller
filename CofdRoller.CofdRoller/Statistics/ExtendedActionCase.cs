namespace CofdRoller;

public class ExtendedActionCase
{
    public int Dices;
    public int RequiredSuccesses;
    public int RollLimit;

    public ExtendedActionCase(int dices, int requiredSuccesses, int rollLimit)
    {
        this.Dices = dices;
        this.RequiredSuccesses = requiredSuccesses;
        this.RollLimit = rollLimit;
    }
}
