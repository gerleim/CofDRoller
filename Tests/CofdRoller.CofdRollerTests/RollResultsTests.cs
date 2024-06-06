using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CofdRoller.Tests;

[TestClass]
public class RollResultsTests
{
    [TestMethod]
    public void SuccessTest()
    {
        var rollResults = new RollResults();
        Assert.AreEqual(0, rollResults.Successes);
        rollResults.Add(new SingleRollResult(2, []));
        Assert.AreEqual(2, rollResults.Successes);
        rollResults.Add(new SingleRollResult(3, []));
        Assert.AreEqual(5, rollResults.Successes);
    }
}