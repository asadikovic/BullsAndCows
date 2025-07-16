using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BaC.Tests
{
    [TestClass()]
    public class RandomGuesserTests
    {
        [TestMethod()]
        public void GuessTest()
        {
            var codes = Solver.GenerateAllPermutations();
            var guesser = new RandomGuesser();
            var code = guesser.Guess(codes);
            Assert.IsTrue(code.IsValid);
        }
    }
}