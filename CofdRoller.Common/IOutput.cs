namespace CofdRoller.Common;

public interface IOutput
{
    void Write(string message);
    void WriteLine(string message);
    void Write(Text text);
    void WriteLine(Text text);
}
