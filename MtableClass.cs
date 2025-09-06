// Copyrght (c) 2025, Keith R. Bergerstock
namespace PrimeSieve
{

	/// look up table of valid parameters
    public class Mtable
    {
        // the first column is the sieve limit, the
        // the second  column is the number of primes up to that limit
        // the third column is the square root of the sieve limit
        public long[,] M = new long[11, 3]
        {
            { 0,0,0 },
            { 10, 4, 3 },
            { 100, 25, 10 },
            { 1000, 168, 32 },
            { 10000, 1229, 100 },
            { 100000, 9592, 316 },
            { 1000000, 78498, 1000 },
            { 10000000, 664579, 3162 },
            { 100000000, 5761455, 10000 },
            { 1000000000, 50847534, 31622 },
            { 10000000000, 455052511, 100000 },
        };


        private int ndx;
        private long expected;
        private long iSqrt;
        private long maxPrime;


        public Mtable(long primeLimit)
        {
            this.maxPrime = primeLimit;
            this.expected = -1;
            this.iSqrt = -1;
            this.ndx = -1;
            Find_ndx(primeLimit);
        }

        void Find_ndx(long primeLimit)
        {
            if (this.ndx < 0)
            {
                for (int idx = 0; idx < M.Length; idx++)
                {
                    if (M[idx, 0] == primeLimit)
                    {
                        this.ndx = idx;
                        this.expected = M[idx, 1];
                        this.iSqrt = M[idx, 2];
                        break;
                    }
                }
            }
        }

        public long GetPrimesExpected()
        {
            if (this.expected < 4)
            {
                var msg = $"Expected not found for {this.maxPrime} max prime";
                throw new ApplicationException(msg);
            }
            return this.expected;
        }

        public long GetPrimeSqrt()
        {
            if (this.iSqrt < 2)
            {

                var msg = $"Square root not found for {this.maxPrime} max prime";
                throw new ApplicationException(msg);
            }
            return this.iSqrt;
        }
    }
}
