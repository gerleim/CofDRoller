using CofDRoller.Console.Output;
using CommandDotNet;

namespace CofDRoller.Console;

internal class AppCommands
{
    private OutputWriter Output { get; init; }

    public AppCommands()
    {
        // TOTO Set Outputs from config
        // Config = ConfigurationLoader.Load();
        Output = new OutputWriter();
        Output.Add(new ConsoleOutput());
    }

    [Command("exit", Description = "Exit from the command-line utility.")]
    public void Exit()
    {
        Program.Terminated = true;
    }

    [Command("roll", Description = "")]
    public void Roll(int dices)
    {
        Output.WriteLine(
            new CofdRoller().Roll(dices).ToText()
        );
    }

    [Command("rollRote", Description = "")]
    public void RollRote(int dices)
    {
        Output.WriteLine(
            new CofdRoller().RollRote(dices).ToText()
        );
    }

    [Command("roll9Again", Description = "")]
    public void Roll9Again(int dices)
    {
        Output.WriteLine(
            new CofdRoller().Roll9Again(dices).ToText()
        );
    }

    [Command("roll8Again", Description = "")]
    public void Roll8Again(int dices)
    {
        Output.WriteLine(
            new CofdRoller().Roll8Again(dices).ToText()
        );
    }

    [Command("rollExtendedAction", Description = "")]
    public void RollExtendedAction(int dices, int requiredSuccesses, int rollLimit)
    {
        Output.WriteLine(
            new CofdExtendedAction(dices, requiredSuccesses, rollLimit).RollAll().ToText()
        );
    }

    [Command("statAvg", Description = "")]
    public void StatAvg(int dices)
    {
        Output.WriteLine(
            CofdStatistics.Avg(dices).ToText()
        );
    }

    [Command("statAvgRote", Description = "")]
    public void StatAvgRote(int dices)
    {
        Output.WriteLine(
            CofdStatistics.AvgRote(dices).ToText()
        );
    }

    [Command("statAvgExtendedAction", Description = "")]
    public void StatAvgExtendedAction(int dices, int requiredSuccesses, int rollLimit)
    {
        // TODO ToTExt
        Output.WriteLine(
            CofdStatistics.AvgExtendedAction(dices, requiredSuccesses, rollLimit).ToText()
        );
    }
}
