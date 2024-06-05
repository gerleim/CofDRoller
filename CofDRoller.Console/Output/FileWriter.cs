using CofdRoller.Common;

namespace CofdRoller.Console.Output;

internal class FileWriter(string fileName) : IOutput, IDisposable
{
    private readonly TextWriter writer = new StreamWriter(fileName);

    public void Dispose()
    {
        writer.Close();
        writer.Dispose();
    }

    public void Write(string message)
    {
        writer.Write(message);
    }

    public void Write(Text text)
    {
        throw new NotImplementedException();
    }

    public void WriteLine(string message)
    {
        writer.WriteLine(message);
    }

    public void WriteLine(Text text)
    {
        throw new NotImplementedException();
    }
}
