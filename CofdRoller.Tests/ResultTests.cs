using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CofdRoller.Tests
{
    [TestClass]
    public class ResultTests
    {
        [TestMethod]
        public void ToStringTest()
        {
            var roteSingleRollResult = new SingleRollResult(1, [9, 10]);
            var rollResults = new RollResults();
            rollResults.Add(roteSingleRollResult);
            var Result = new Result(0, rollResults);

            Assert.AreEqual("1 success rolling chance die. Success.  Rolls: 9->10 ", Result.ToString());
        }
    }
}