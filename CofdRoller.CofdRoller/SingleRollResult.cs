namespace CofdRoller;

public class SingleRollResult
{
    public SingleRollResult(int successes, List<int> rolledNumbers)
    {
        Successes = successes;
        RolledNumbers = rolledNumbers;
    }

    public int Successes { get; set; }
    public List<int> RolledNumbers { get; set; }
}
