using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CofdRoller.Tests;

[TestClass]
public class ExtendedActionResultsBaseTests
{
    public class ExtendedActionResultsBaseTest : ExtendedActionResultsBase
    {
    }

    [TestMethod]
    public void ToStringTest()
    {
        var result = new ExtendedActionResultsBaseTest();
        var rollResulst1 = new RollResults();
        rollResulst1.Add(new SingleRollResult(2, []));
        result.Add(new Result(1, rollResulst1));
        Assert.AreEqual(2, result.Successes);

        var rollResulst2 = new RollResults();
        rollResulst2.Add(new SingleRollResult(3, []));
        result.Add(new Result(1, rollResulst2));
        Assert.AreEqual(5, result.Successes);
    }
}