namespace CofDRoller.Console;

internal class Program
{
    static void Main(string[] args)
    {
        RepeatWriteLine(() => new CofdRoller().Roll(0));
        RepeatWriteLine(() => new CofdRoller().Roll(1));
        RepeatWriteLine(() => new CofdRoller().Roll(5));
        RepeatWriteLine(() => new CofdRoller().RollRote(0));
        RepeatWriteLine(() => new CofdRoller().RollRote(5));

        RepeatWriteLine(() => CofdStatistics.Avg(1));
        RepeatWriteLine(() => CofdStatistics.Avg(2));
        RepeatWriteLine(() => CofdStatistics.Avg(3));
        RepeatWriteLine(() => CofdStatistics.Avg(4));
        RepeatWriteLine(() => CofdStatistics.Avg(5));
        RepeatWriteLine(() => CofdStatistics.AvgRote(1));

        
        RepeatWriteLine(() => new CofdExtendedAction(5, 5, 5).RollAll());
        RepeatWriteLine(() => new CofdExtendedAction(5, 10, 5).RollAll());
        RepeatWriteLine(() => CofdStatistics.AvgExtendedAction(5, 5, 5));
        RepeatWriteLine(() => CofdStatistics.AvgExtendedAction(5, 10, 5));
    }

    public static void RepeatWriteLine<T>(Func<T> func, int repeatCount = 10)
    {
        Repeat(
            () => { System.Console.WriteLine(func()); }
        , repeatCount);
    }

    public static void Repeat(Action action, int repeatCount = 10)
    {
        for (int i = 0; i < repeatCount; i++)
            action();
    }
}
