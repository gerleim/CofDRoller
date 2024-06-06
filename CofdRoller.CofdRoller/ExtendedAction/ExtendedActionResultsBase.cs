using System.Collections;

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
