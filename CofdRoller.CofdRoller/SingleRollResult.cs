namespace CofdRoller;

public class SingleRollResult(int successes, List<int> rolledNumbers)
{
    public int Successes { get; set; } = successes;
    public List<int> RolledNumbers { get; set; } = rolledNumbers;
}
