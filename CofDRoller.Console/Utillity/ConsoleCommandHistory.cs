namespace CofdRoller.Console;

public class ConsoleCommandHistory
{
    public int CommandOffsetFromLast { get; set; } = 0;
    public readonly List<string> CommandsHistory = [];

    public bool IsCurrentCommandSelectedFromHistory = false;

    public bool HandlePreviousCommand(ref string commandEntered)
    {
        if (CommandOffsetFromLast <= CommandsHistory.Count - 1)
        {
            CommandOffsetFromLast += 1;
            commandEntered = CommandsHistory[^CommandOffsetFromLast];
            return true;
        }

        return false;
    }

    public bool HandleNextCommand(ref string commandEntered)
    {
        if (CommandOffsetFromLast > 1)
        {
            CommandOffsetFromLast -= 1;
            commandEntered = CommandsHistory[^CommandOffsetFromLast];
            return true;
        }

        return false;
    }

    public void HandleEnter(string commandEntered)
    {
        var previousIndex = CommandsHistory.IndexOf(commandEntered);
        if (previousIndex == -1)
        {
            if (CommandsHistory.Count == 0
                || CommandsHistory[^1] != commandEntered)
            {
                CommandsHistory.Add(commandEntered);
                CommandOffsetFromLast = 0;
            }
        }
        else
        {
            if (CommandsHistory[^1] != commandEntered)
                CommandsHistory.Add(commandEntered);
        }
    }
}
