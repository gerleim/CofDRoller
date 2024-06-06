using System.Collections;

namespace CofdRoller;

public class RollResults : IList<SingleRollResult>
{
    private readonly List<SingleRollResult> rollResults = [];
    private int successes = 0;

    public int Successes { get => successes; }

    public SingleRollResult this[int index]
    {
        get => rollResults[index];
        set
        {
            successes -= rollResults[index].Successes;
            successes += value.Successes;
            rollResults[index] = value;
        }
    }

    public int Count => rollResults.Count;

    public bool IsReadOnly => false;

    public void Add(SingleRollResult item)
    {
        successes += item.Successes;
        rollResults.Add(item);
    }

    public void Clear()
    {
        successes = 0;
        rollResults.Clear(); 
    }

    public bool Contains(SingleRollResult item)
    {
        throw new NotImplementedException();
    }

    public void CopyTo(SingleRollResult[] array, int arrayIndex)
    {
        throw new NotImplementedException();
    }

    public IEnumerator<SingleRollResult> GetEnumerator()
    {
        return rollResults.GetEnumerator();
    }

    public int IndexOf(SingleRollResult item)
    {
        throw new NotImplementedException();
    }

    public void Insert(int index, SingleRollResult item)
    {
        throw new NotImplementedException();
    }

    public bool Remove(SingleRollResult item)
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
