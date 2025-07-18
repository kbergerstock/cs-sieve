
namespace PrimeSieve.Tests
{

    [TestClass]
    public sealed class Test_M1
    {

        [TestMethod]
        [DataRow(10, 4)]
        [DataRow(100, 25)]
        [DataRow(1000, 168)]
        [DataRow(10000, 1229)]
        [DataRow(100000, 9592)]
        [DataRow(1000000, 78498)]
        public void TestCount1(Int64 primeLimit, Int64 expectedCount)
        {
            PrimeSieve.Mtable M = new PrimeSieve.Mtable(primeLimit);
            Assert.AreEqual(expectedCount, M.GetPrimeCount(primeLimit));
        }

        [TestMethod]
        [DataRow(10, 5)]
        [DataRow(100, 11)]
        [DataRow(1000, 44)]
        [DataRow(10000, 2000)]
        [DataRow(100000, 1316)]
        public void TestCount2(Int64 primeLimit, Int64 expectedCount)
        {
            PrimeSieve.Mtable M = new PrimeSieve.Mtable(primeLimit);
            Assert.AreNotEqual(expectedCount, M.GetPrimeCount(primeLimit));
        }

    }
}	

	
