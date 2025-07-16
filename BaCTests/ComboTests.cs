using BaC;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BaCTests
{
    [TestClass]
    public class ComboTests
    {
        [TestMethod]
        public void ComboTest()
        {
            var C = new Code(1, 2, 3, 4);
            Assert.AreEqual(C[1], 2);
        }

        [TestMethod]
        public void ComboTest2()
        {
            var C = new Code("1234");
            Assert.AreEqual(C[1], 2);
        }
        [TestMethod]
        public void ComboIsValidTest()
        {
            var C = new Code(1, 2, 3, 4);
            Assert.IsTrue(C.IsValid);

            C = new Code(1, 2, 2, 3);
            Assert.IsFalse(C.IsValid);

            C = new Code(1, 2, 0, 3);
            Assert.IsFalse(C.IsValid);

            C = new Code(1, 2, 10, 3);
            Assert.IsFalse(C.IsValid);
        }

        [TestMethod]
        public void CompareWithTest()
        {
            var c1 = new Code(1, 2, 3, 4);
            var c2 = new Code(2, 5, 1, 4);

            var info = c1.CompareWith(c2);
            Assert.IsTrue(info.Bulls == 1);
            Assert.IsTrue(info.Cows == 2);
        }
    }
}
