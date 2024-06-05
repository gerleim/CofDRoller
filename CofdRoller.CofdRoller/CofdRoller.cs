namespace CofdRoller;

public class CofdRoller : CofdRollerBase
{
    public Result Roll(int dices)
    {
        var rollResults = base.Roll(dices);
        var result = new Result (dices, rollResults);
        return result;
    }

    public Result Roll9Again(int dices)
    {
        var rollResults = base.Roll(dices, 9);
        var result = new Result(dices, rollResults, RollTypes._9Again);
        return result;
    }

    public Result Roll8Again(int dices)
    {
        var rollResults = base.Roll(dices, 8);
        var result = new Result(dices, rollResults, RollTypes._8Again);
        return result;
    }

    public new Result RollRote(int dices)
    {
        var rollResults = base.RollRote(dices);
        var result = new Result(dices, rollResults, RollTypes.Rote);
        return result;
    }
}
