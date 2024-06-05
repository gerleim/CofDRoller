namespace CofdRoller.Console;

public class ConsoleCommand(List<string> commands)
{
    private readonly List<string> commands = commands.OrderBy(c => c).ToList();
    private string prompt = "";
    private readonly List<string> previousEnteredCommands = [];
    private int previousCommandIndex = 0;
    private readonly ConsoleCommandTabState TabState = new();
    private int cursorPosition = 0;

    public string ReadConsoleCommand()
    {
        var keyPresses = "";
        var breakFlag = false;
        while (!breakFlag)
        {
            var pressedKey = System.Console.ReadKey(true);
            switch (pressedKey.Key)
            {
                case ConsoleKey.UpArrow:
                    if (previousCommandIndex <= previousEnteredCommands.Count - 1)
                    {
                        previousCommandIndex += 1;
                        keyPresses = HandleNextOrPreviousCommand();
                        cursorPosition = keyPresses.Length;
                    }
                    break;
                case ConsoleKey.DownArrow:
                    if (previousCommandIndex > 1)
                    {
                        previousCommandIndex -= 1;
                        keyPresses = HandleNextOrPreviousCommand(); ;
                        cursorPosition = keyPresses.Length;
                    }
                    break;
                case ConsoleKey.Enter:
                    System.Console.Write(pressedKey.KeyChar);
                    System.Console.WriteLine();
                    cursorPosition = 0;
                    breakFlag = true;
                    break;
                case ConsoleKey.Tab:
                    keyPresses = HandleTab(keyPresses);
                    ReplaceLine(keyPresses);
                    cursorPosition = keyPresses.Length;
                    break;
                case ConsoleKey.Backspace:
                    if (keyPresses.Length > 0)
                    {
                        cursorPosition -= 1;
                        keyPresses = keyPresses[..^1];
                        System.Console.Write("\b \b");
                    }
                    break;
                case ConsoleKey.LeftArrow:
                    if (cursorPosition > 0)
                    {
                        cursorPosition -= 1;
                        System.Console.CursorLeft -= 1;
                    }
                    break;
                case ConsoleKey.RightArrow:
                    if (cursorPosition < keyPresses.Length)
                    {
                        cursorPosition += 1;
                        System.Console.CursorLeft += 1;
                    }
                    break;
                case ConsoleKey.Escape:
                    keyPresses = "";
                    ReplaceLine(keyPresses);
                    cursorPosition = 0;
                    break;
                case ConsoleKey.Insert:
                    // TODO toggle Insert mode
                    // Console.CursorSize += 1;
                    break;
                case ConsoleKey.Delete:
                    // TODO delete
                    break;
                default:
                    {
                        var currentCusrsorPosition = System.Console.CursorLeft;
                        keyPresses = keyPresses.Insert(cursorPosition, pressedKey.KeyChar.ToString());
                        ReplaceLine(keyPresses);
                        cursorPosition += 1;
                        System.Console.CursorLeft = currentCusrsorPosition + 1;
                        break;
                    }
            }

            if (pressedKey.Key != ConsoleKey.Tab) {
                TabState.MatchedCommandIndex = -1;
                if (pressedKey.Key != ConsoleKey.RightArrow
                    && pressedKey.Key != ConsoleKey.LeftArrow)
                    TabState.LastEnteredPart = null;
            }
        }

        previousEnteredCommands.Add(keyPresses);
        return keyPresses;
    }

    private void ReplaceLine(string keyPresses)
    {
        ClearLine();
        System.Console.Write(prompt);
        System.Console.Write(keyPresses);
    }

    private string HandleTab(string keyPresses)
    {
        TabState.LastEnteredPart ??= keyPresses;
        
        var matches = commands.Where(c => c.StartsWith(TabState.LastEnteredPart, StringComparison.InvariantCulture)).OrderBy(c => c).ToList();
        if (matches.Count != 0)
        {
            if (TabState.MatchedCommandIndex < matches.Count - 1)
                TabState.MatchedCommandIndex += 1;
            else
                TabState.MatchedCommandIndex = 0;

            return matches[TabState.MatchedCommandIndex];
        }
        else
        {
            return keyPresses;
        }
    }

    private string HandleNextOrPreviousCommand()
    {
        string keyPresses;
        keyPresses = previousEnteredCommands[^previousCommandIndex];
        ReplaceLine(keyPresses);
        return keyPresses;
    }

    public static void ClearLine()
    {
        System.Console.Write("\r" + new string(' ', System.Console.WindowWidth) + "\r");
    }

    public void SetPrompt(string prompt)
    {
        this.prompt = prompt;
    }
}