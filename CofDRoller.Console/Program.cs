using CommandDotNet;
using CommandDotNet.Help;
using System.Text.RegularExpressions;

namespace CofdRoller.Console;

public static partial class Program
{
    public static Config Config { get; private set; } = new Config();
    public static bool Terminated { get; set; }
    public static ConsoleCommand ConsoleCommand { get; } = new ConsoleCommand(ConsoleCommandHelper.GetCommands(typeof(AppCommands)));

    public static void Main(string[] args)
    {
        Config = ConfigurationLoader.Load();

        DsiaplayInfo();

        if (args.Length > 0)
        {
            var runner = new AppRunner<AppCommands>(GetAppSettings(""));
            runner.Run(args);
        }

        DisplayHelp();

        var regEx = CommandLineRegex();

        ConsoleCommand.SetPrompt(GetPrompt());

        while (!Terminated)
        {
            System.Console.Write(GetPrompt());
            var commandLine = ConsoleCommand.ReadConsoleCommand();

            if (string.IsNullOrEmpty(commandLine))
                continue;

            var lineArguments = regEx
                .Matches(commandLine.Trim())
                .Select(x => x.Value.Trim())
                .ToArray();

            var runner = new AppRunner<AppCommands>(GetAppSettings(""));
            runner.Run(lineArguments);

            System.Console.WriteLine();
        }
    }

    private static string GetPrompt()
    {
        return "> ";
    }

    internal static void DisplayHelp(string? command = null)
    {
        var runner = new AppRunner<AppCommands>(GetAppSettings(""));

        if (string.IsNullOrEmpty(command))
        {
            runner.Run("--help");
        }
        else
        {
            var args = command.Split(' ').ToList();
            args.Add("--help");
            runner.Run(args.ToArray());
        }
    }


    private static AppSettings GetAppSettings(string prompt)
    {
        return new AppSettings()
        {
            Help = new AppHelpSettings()
            {
                TextStyle = HelpTextStyle.Basic,
                UsageAppName = prompt + ">",
                PrintHelpOption = false,
            }
        };
    }

    public static void DsiaplayInfo()
    {
        System.Console.WriteLine("Dice Roller for Chronicles of Darkness.");
    }

    [GeneratedRegex("(?<=\")[^\"]*(?=\")|[^\" ]+")]
    private static partial Regex CommandLineRegex();
}