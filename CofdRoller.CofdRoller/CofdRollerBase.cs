namespace CofDRoller;

public class CofdRollerBase
{
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

        var rollResults = Roll(dices);
        foreach (var rr in rollResults)
        {
            if (rr.Successes == 0)
            {
                var secondRoll = Roll1(11);
                rr.RolledNumbers.Add(secondRoll.RolledNumbers[0]);
                if (secondRoll.Successes == 1)
                    rr.Successes = 1;
            }
        }

        return rollResults;
    }

    protected RollResults Roll0()
    {
        var rollResults = new RollResults();
        rollResults.Add(Roll1(11, 10));
        return rollResults;
    }

    protected SingleRollResult Roll1(int pAgain = 10, int target = 8)
    {
        var successes = 0;
        var rolledNumber = BaseRoller.D10();
        var rolledNumbers = new List<int>();
        rolledNumbers.Add(rolledNumber);
        while (rolledNumber >= pAgain)
        {
            successes++;
            rolledNumber = BaseRoller.D10();
            rolledNumbers.Add(rolledNumber);
        }

        if (rolledNumber >= target)
            successes++;

        return new SingleRollResult(successes, rolledNumbers);
    }
}
