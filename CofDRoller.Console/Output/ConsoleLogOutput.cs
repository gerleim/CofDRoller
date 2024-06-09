using CofdRoller.Common;

namespace CofdRoller.Console.Output;

internal class ConsoleLogOutput : ConsoleOutput, ILog
{
    private readonly IConsoleCommand? consoleCommand;
    private int cursorTopOfLogLine = -1;

    public ConsoleLogOutput()
    { }

    public ConsoleLogOutput(IConsoleCommand? consoleCommand)
    {
        this.consoleCommand = consoleCommand;
    }

    public new void Write(string message)
    {
        lock (consoleCommand.SyncRoot)
        {
            var currentCusrsorPosition = System.Console.CursorLeft;
            System.Console.CursorVisible = false;

            if (cursorTopOfLogLine == -1)
            {
                cursorTopOfLogLine = System.Console.CursorTop;
                consoleCommand.RepeatCommandEnteredOnNextLine();
            }
            else
            {
                System.Console.CursorTop -= 1;
                consoleCommand.ReplaceLine(message);
                System.Console.CursorTop += 1;
            }

            System.Console.CursorLeft = currentCusrsorPosition;
            System.Console.CursorVisible = true;
        }
    }

    public new void WriteLine(string message)
    {
        if (cursorTopOfLogLine == -1)
            cursorTopOfLogLine = System.Console.CursorTop;
        consoleCommand.ReplaceLine(message);
    }
}
