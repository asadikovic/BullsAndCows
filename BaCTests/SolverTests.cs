using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BaC.Tests
{
    [TestClass]
    public class SolverTests
    {
        [TestMethod]
        public void GenerateAllCombosTest()
        {
            var codes = Solver.GenerateAllPermutations();
            Assert.AreEqual(codes.Length, 9 * 8 * 7 * 6);

            foreach(var code in codes)
            {
                Assert.IsTrue(code.IsValid);
            }
        }

        [TestMethod]
        public void ReduceTest()
        {
            var all = Solver.GenerateAllPermutations();
            var guess = new Code(1, 2, 3, 4);
            var info = new Info { Cows = 2, Bulls = 1 };

            var reduced = Solver.Reduce(all, guess, info);

            foreach(var c in reduced)
            {
                var i = c.CompareWith(guess);
                Assert.IsTrue(info.Cows == i.Cows);
                Assert.IsTrue(info.Bulls == i.Bulls);
            }
        }

        [TestMethod]
        public void SolveTest()
        {
            var code = new Code(7, 1, 3, 2);
            IGuesser guesser = new RandomGuesser();

            int tries = Solver.Solve(code, guesser, false);

            Assert.IsTrue(tries < 8);
        }

        [TestMethod]
        public void RandomPickTest()
        {
            var code = Solver.RandomPick(Solver.GenerateAllPermutations());
            Assert.IsTrue(code.IsValid);
        }
    }
}
