namespace CofdRoller;

public static class Random
{
    public static int RandomNumber(int from, int to)
    {
        var toInclusive = to + 1;
        return System.Security.Cryptography.RandomNumberGenerator.GetInt32(from, toInclusive);
    }
}