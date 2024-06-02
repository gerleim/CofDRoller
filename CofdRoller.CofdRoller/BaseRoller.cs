namespace CofDRoller;

public static class BaseRoller
{
    public static int D10()
    {
        return D(10);
    }

    public static int D(int sides)
    {
        return Random.RandomNumber(1, sides);
    }
}
