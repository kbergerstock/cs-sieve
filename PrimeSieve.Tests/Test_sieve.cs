namespace PrimeSieve.Tests
{
    [TestClass]
    public sealed class TestSieve
    {
        [TestMethod]
        [DataRow(10)]
        [DataRow(100)]
        [DataRow(1000)]
        public void TestTheSieve(long primeLimit)
        {
            PrimeSieve.Mtable M = new PrimeSieve.Mtable(primeLimit);
            PrimeSieve.Sieve xieve = new PrimeSieve.Sieve(primeLimit,ref  M);
            xieve.Sieve2();
            Console.WriteLine(xieve.CountPrimes());
            Console.WriteLine(xieve.Count());
            Console.WriteLine(xieve.GetPrimes());
            Assert.IsTrue(xieve.Validate());
            Assert.IsTrue(xieve.Validate2());

        }
    }
}