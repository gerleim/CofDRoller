using System;

namespace CofdRoller;

public class RollResults : List<SingleRollResult>
{
    private int successes;
    private bool isDirty = true;
    public int Successes
    {
        get
        {
            if (isDirty)
                successes = this.Select(r => r.Successes).Sum();

            return successes;
        }
    }

    public new SingleRollResult this[int index]
    {
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

    public new void Add(SingleRollResult item)
    {
        isDirty = true;
        base.Add(item);
    }

}
