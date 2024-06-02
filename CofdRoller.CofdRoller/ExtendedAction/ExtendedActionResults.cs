using System.Diagnostics;
using System.Text;

namespace CofDRoller;

[DebuggerDisplay("{ToString()}")]
public class ExtendedActionResults(int dices, int requiredSuccesses, int rollLimit) : List<Result>
{
    public int Dices { get; set; } = dices;
    public int RequiredSuccesses { get; set; } = requiredSuccesses;
    public int RollLimit { get; set; } = rollLimit;

    private ResultType resultType;
    
    public ResultType ResultType {
        get
        {
            if (isDirty)
                Evaluate();

            return resultType;
        }
        set
        {
            resultType = value;
        }
    }

    private int successes;
    private bool isDirty = true;
    public int Successes
    {
        get
        {
            if(isDirty)
                successes = this.Select(r => r.RollResults.Successes).Sum();
            
            return successes;
        }
    }

    private void Evaluate()
    {
        if (Successes >= RequiredSuccesses)
            ResultType = ResultType.Success;
        else if (this.Count >= RollLimit)
            ResultType = ResultType.Failure;
    }

    public override string ToString()
    {
        Evaluate();
        var sb = new StringBuilder();
        sb.AppendLine($"Extended Action - Dice: {Dices}, Required Successes {RequiredSuccesses}, Roll Limit: {RollLimit}.");
        sb.AppendLine($"{Successes} successes.");
        sb.AppendLine($"RESULT: { ResultType.GetDisplayName()}");
        return sb.ToString();
    }

    public new Result this[int index] {
        get
        {
            return base[index];
        }
        set
        {
            isDirty = true;
            base[index] = value;
        }
    }

    public new void Add(Result item)
    {
        isDirty = true;
        base.Add(item);
    }
}
