using System.Diagnostics;
using System.Text;

namespace CofDRoller;

[DebuggerDisplay("{ToString()}")]
public class ExtendedActionResults : List<Result>
{
    public int Dices { get; set; }
    public int RequiredSuccesses { get; set; }
    public int RollLimit { get; set; }
    
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

    public ExtendedActionResults(int dices, int requiredSuccesses, int rollLimit)
    {
        Dices = dices;
        RequiredSuccesses = requiredSuccesses;
        RollLimit = rollLimit;
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
