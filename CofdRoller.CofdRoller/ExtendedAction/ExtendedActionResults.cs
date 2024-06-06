using CofdRoller.Common;
using System.Collections;
using System.Diagnostics;

namespace CofdRoller;

public abstract class ExtendedActionResultsBase : IList<Result>
{
    private readonly List<Result> results = [];
    private int successes;

    public int Successes { get => successes; }

    public Result this[int index]
    {
        get => results[index];
        set
        {
            successes -= results[index].RollResults.Successes;
            successes += value.RollResults.Successes;
            results[index] = value;
        }
    }

    public int Count => results.Count;

    public bool IsReadOnly => false;

    public void Add(Result item)
    {
        successes += item.RollResults.Successes;
        results.Add(item);
    }

    public void Clear()
    {
        successes = 0;
        results.Clear();
    }

    public bool Contains(Result item)
    {
        throw new NotImplementedException();
    }

    public void CopyTo(Result[] array, int arrayIndex)
    {
        throw new NotImplementedException();
    }

    public IEnumerator<Result> GetEnumerator()
    {
        return results.GetEnumerator();
    }

    public int IndexOf(Result item)
    {
        throw new NotImplementedException();
    }

    public void Insert(int index, Result item)
    {
        throw new NotImplementedException();
    }

    public bool Remove(Result item)
    {
        throw new NotImplementedException();
    }

    public void RemoveAt(int index)
    {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        throw new NotImplementedException();
    }
}

[DebuggerDisplay("{ToString()}")]
public class ExtendedActionResults(int dices, int requiredSuccesses, int rollLimit) : ExtendedActionResultsBase
{
    public int Dices { get; set; } = dices;
    public int RequiredSuccesses { get; set; } = requiredSuccesses;
    public int RollLimit { get; set; } = rollLimit;
   
    public ResultType ResultType { get; set; }
    private void Evaluate()
    {
        if (Successes >= RequiredSuccesses)
            ResultType = ResultType.Success;
        else if (this.Count >= RollLimit)
            ResultType = ResultType.Failure;
    }

    public override string ToString()
    {
        return ToText().ToString();
    }
    /*
    public override string ToString()
    {
        Evaluate();
        var sb = new StringBuilder();
        sb.AppendLine($"Extended Action - Dice: {Dices}, Required Successes {RequiredSuccesses}, Roll Limit: {RollLimit}.");
        sb.AppendLine($"{Successes} successes.");
        sb.AppendLine($"RESULT: { ResultType.GetDisplayName()}");
        return sb.ToString();
    }*/

    public Text ToText()
    {
        Evaluate();
        var text = new Text();
        text.Add("Extended Action - Dice: ")
            .Add(Dices)
            .Add(", Required Successes ")
            .Add(RequiredSuccesses)
            .Add(", Roll Limit: ")
            .Add(RollLimit)
            .Add("\r\n")
            .Add(Successes)
            .Add(" successes.")
            .Add("\r\n")
            .Add("RESULT: ")
            .Add(ResultType.GetText());

        return text;
    }
}
