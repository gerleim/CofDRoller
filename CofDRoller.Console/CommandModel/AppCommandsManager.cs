using CofdRoller.Console.Output;

namespace CofdRoller.Console;

public class AppCommandsManager
{
    public OutputWriter Output { get; init; }
    public IConsoleCommand ConsoleCommand { get; set; }
    private Dictionary<string, CancellationTokenSource> BackGroundTasks { get; } = [];

    public AppCommandsManager(IConsoleCommand consoleCommand)
    {
        ConsoleCommand = consoleCommand;

        // TODO Set Outputs from config
        // Config = ConfigurationLoader.Load();
        Output = new OutputWriter();
        Output.Add(new ConsoleOutput());
    }

    public bool CheckBackGroundTask(string name, out string message)
    {
        if (BackGroundTasks.ContainsKey(name))
        {
            message = "Task is already running.";
            return true;
        }

        message = "";
        return false;
    }

    public void StartBackGroundTask(string name, Func<CancellationToken, Task> action)
    {
        if (BackGroundTasks.ContainsKey(name))
            return;

        var cts = new CancellationTokenSource();
        action.Invoke(cts.Token).ContinueWith((t) => {
            if (t.IsCanceled)
                Output.WriteLine($"{name} has been Stopped.");
            });

        RegisterBackGroundTask(name, cts);

        return;
    }

    public void RegisterBackGroundTask(string name, CancellationTokenSource cts)
    {
        BackGroundTasks.Add(name, cts);
    }

    public void StopBackGroundTask(string name)
    {
        if (BackGroundTasks.TryGetValue(name, out CancellationTokenSource? value))
        {
            var cts = value;
            cts.Cancel();
            BackGroundTasks.Remove(name);
        }
        else
        {
            Output.WriteLine($"No {name} task is running.");
            //Output.Write($"No {name} task is running.");
        }
    }
}
