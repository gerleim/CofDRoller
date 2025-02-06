using System.Diagnostics;

namespace CofdRoller;

public class SuccessCounter
{
    public int CasesOfSuccess;
    public int SumOfSuccesses;
}

[DebuggerDisplay("{ToString()}")]
public class SuccessesAndRolls
{
    public int NumberOfSuccesses;
    public int NumberOfRolls;

    public override string ToString()
    {
        return NumberOfSuccesses.ToString() + " / " + NumberOfRolls.ToString();
    }
}

public class HowManySuccessPerSuccess : Dictionary<int, SuccessesAndRolls>
{
    public int NumberOfRolls2;

    public HowManySuccessPerSuccess()
    {
        Initialize();
    }

    public void Initialize()
    {
        for (int i = 0; i <= CofdStatistics.numberOfMeasuredSuccesses; i++)
        {
            this[i] = new SuccessesAndRolls();
        }
    }
}

public class SuccessesPerDices : Dictionary<int, HowManySuccessPerSuccess>
{
    public void Initialize(int maxDices)
    {
        for (int i = 0; i <= maxDices; i++)
        {
            this[i] = new HowManySuccessPerSuccess();
        }
    }
}

/*public class SuccessesPerDicesX : Dictionary<(int dice, int success), HowManySuccessPerSuccess>
{
    public void Initialize(int maxDices, int maxSuccesses)
    {
        for (int i = 1; i <= maxDices; i++)
        {
            for (int j = 0; i <= maxSuccesses; j++)
            {
                this[(i, j)] = new HowManySuccessPerSuccess();
            }
        }
    }
}*/

/*public class StatisticsSuccessResult : SuccessCounter
{
    public SuccessesPerDices SuccessesPerDices = [];
}*/