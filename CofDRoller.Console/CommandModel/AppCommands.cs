using CofdRoller.Console.Output;
using CommandDotNet;

namespace CofdRoller.Console;

public class AppCommands(AppCommandsManager appCommandsManager)
{
    private readonly AppCommandsManager appCommandsManager = appCommandsManager;

    [Command("exit", Description = "Exit from the command-line utility.")]
    public void Exit()
    {
        Program.Terminated = true;
    }

    [Command("roll", Description = "")]
    public void Roll(int dices)
    {
        appCommandsManager.Output.WriteLine(
            new Roller().Roll(dices).ToText()
        );
    }

    [Command("rollRote", Description = "")]
    public void RollRote(int dices)
    {
        appCommandsManager.Output.WriteLine(
            new Roller().RollRote(dices).ToText()
        );
    }

    [Command("roll9Again", Description = "")]
    public void Roll9Again(int dices)
    {
        appCommandsManager.Output.WriteLine(
            new Roller().Roll9Again(dices).ToText()
        );
    }

    [Command("roll8Again", Description = "")]
    public void Roll8Again(int dices)
    {
        appCommandsManager.Output.WriteLine(
            new Roller().Roll8Again(dices).ToText()
        );
    }

    [Command("rollExtendedAction", Description = "")]
    public void RollExtendedAction(int dices, int requiredSuccesses, int rollLimit)
    {
        appCommandsManager.Output.WriteLine(
            new CofdExtendedAction(dices, requiredSuccesses, rollLimit).RollAll().ToText()
        );
    }

    [Command("statAvg", Description = "")]
    public void StatAvg(int dices)
    {
        appCommandsManager.Output.WriteLine(
            CofdStatistics.Avg(dices).ToText()
        );
    }

    [Command("statAvgRote", Description = "")]
    public void StatAvgRote(int dices)
    {
        appCommandsManager.Output.WriteLine(
            CofdStatistics.AvgRote(dices).ToText()
        );
    }

    [Command("statAvgExtendedAction", Description = "")]
    public void StatAvgExtendedAction(int dices, int requiredSuccesses, int rollLimit)
    {
        appCommandsManager.Output.WriteLine(
            CofdStatistics.AvgExtendedAction(dices, requiredSuccesses, rollLimit).ToText()
            );
    }

    [Command("statCsv", Description = "")]
    public async Task StatCsv(string? stopCommand)
    {
        if(stopCommand == "stop")
        {
            appCommandsManager.StopBackGroundTask(nameof(StatCsv));
            return;
        }

        if (appCommandsManager.CheckBackGroundTask(nameof(StatCsv), out string message))
        {
            appCommandsManager.Output.WriteLine(message);
            return;
        }

        var fw = new FileWriter("CofdStatisctics.csv");
        var l = new ConsoleLogOutput(appCommandsManager.ConsoleCommand);
        var cofdStatiscticsCsv = new CofdStatiscticsCsv(fw, l);

        appCommandsManager.StartBackGroundTask(nameof(StatCsv), (ct) =>
            cofdStatiscticsCsv.Generate(ct).ContinueWith((t) =>
                {
                    // appCommandsManager.Output.WriteLine(t.IsCanceled.ToString());
                    fw.Dispose();
                }, CancellationToken.None)
        );
    }
}
