namespace CofdRoller;

public class CofdRollerBase
{
    /// <summary>
    /// Normal roll.
    /// </summary>
    /// <param name="dices">Number of dices.</param>
    /// <param name="pAgain">Reroll this value or higer, default is 10 again.</param>
    protected RollResults Roll(int dices, int pAgain = 10)
    {
        if (dices == 0)
            return Roll0();

        var rollResults = new RollResults();
        for (int i = 0; i < dices; i++)
        {
            rollResults.Add(Roll1(pAgain));
        }

        return rollResults;
    }

    protected RollResults RollRote(int dices)
    {
        
        if (dices == 0)
        {
            var firstChanceRoll = Roll0();
            if (firstChanceRoll[0].RolledNumbers[0] == 1
                || firstChanceRoll[0].RolledNumbers[0] == 10)
                return firstChanceRoll;
            else
            {
                var secondChanceRoll = Roll0();
                firstChanceRoll[0].RolledNumbers.Add(secondChanceRoll[0].RolledNumbers[0]);
                if (secondChanceRoll[0].RolledNumbers[0] == 10)
                {
                    firstChanceRoll[0].Successes = 1;
                }
            }

            return firstChanceRoll;
        }

        var rollResults = Roll(dices, 10); // 10s on first roll are rerolled as normal
        foreach (var rr in rollResults)
        {
            if (rr.Successes == 0)
            {
                var secondRoll = Roll1(10); // 10s on rerolls are also rerolled as normal
                rr.RolledNumbers.Add(secondRoll.RolledNumbers[0]);
                if (secondRoll.Successes >= 1)
                {
                    rr.Successes = secondRoll.Successes;
                    rollResults.IncreaseSuccesses(secondRoll.Successes);
                }
            }
        }

        return rollResults;
    }

    const int target = 8;

    /// <summary>
    /// Chance die. Only 10 is a success, won't reroll 10s.
    /// </summary>
    protected RollResults Roll0()
    {
        var rollResults = new RollResults();
        rollResults.Add(Roll1(11, 10));
        return rollResults;
    }

    /// <summary>
    /// Roll D10.
    /// </summary>
    /// <param name="pAgain">Reroll this or higher values.</param>
    /// <param name="target">This value or higher counts as a success.</param>
    protected SingleRollResult Roll1(int pAgain = 10, int target = target)
    {
        var successes = 0;
        var rolledNumber = BaseRoller.D10();
        var rolledNumbers = new List<int>();
        rolledNumbers.Add(rolledNumber);
        if (rolledNumber >= target)
            successes++;

        while (rolledNumber >= pAgain)
        {
            rolledNumber = BaseRoller.D10();
            rolledNumbers.Add(rolledNumber);
            if (rolledNumber >= target)
                successes++;
        }

        return new SingleRollResult(successes, rolledNumbers);
    }
}
