namespace CofDRoller.Console.Output;

internal class OutputWriter
{
    private readonly List<IOutput> outputs = [];

    public void Write(string message)
    {
        foreach(var output in outputs)
        {
            output.Write(message);
        }
    }

    public void WriteLine(string message)
    {
        foreach (var output in outputs)
        {
            output.WriteLine(message);
        }
    }

    public void Add(IOutput output)
    {
        outputs.Add(output);
    }

    public void WriteLine(Text text)
    {
        foreach (var output in outputs)
        {
            output.WriteLine(text);
        }
    }

    public void Write(Text text)
    {
        foreach (var output in outputs)
        {
            output.Write(text);
        }
    }
}
