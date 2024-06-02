﻿namespace CofDRoller;

public class CofdExtendedAction
{
    public int Dices { get; set; }
    public int RequiredSuccesses { get; set; }
    public int RollLimit { get; set; }

    public CofdExtendedAction(int dices, int requiredSuccesses, int rollLimit)
    {
        Dices = dices;
        RequiredSuccesses = requiredSuccesses;
        RollLimit = rollLimit;
    }

    public ExtendedActionResults RollAll()
    {
        var result = new ExtendedActionResults(Dices, RequiredSuccesses, RollLimit);
        var end = true;
        var cofdRoller = new CofdRoller();
        while (end)
        {
            result.Add(cofdRoller.Roll(Dices));

            if (result.Count == RollLimit)
                end = false;

            if (result.Successes >= RequiredSuccesses)
                end = false;
        }

        return result;
    }

}


public class Time
{
    public int Value { get; set; }
    public IntervalType IntervalType { get; set; }
}

public enum IntervalType
{
    Minutes,
    Hours,
    Days,
    Weeks
}