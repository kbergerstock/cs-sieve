
namespace  PrimeSieve{
    public class Mtable
    {

        // the first column is the sieve limit, the 
        // the second  column is the number of primes up to that limit
        // the third column is the square root of the sieve limit{)
        public long[,] M = new long[10, 3] {
                    { 10, 4, 3 },
                    { 100, 25, 10 },
                    { 1000, 168, 32 },
                    { 10000, 1229, 100 },
                    { 100000, 9592, 316 },
                    { 1000000, 78498, 1000 },
                    { 10000000, 664579, 3162 },
                    { 100000000, 5761455, 10000 },
                    { 1000000000, 50847534, 31622 },
                    { 10000000000, 455052511, 100000 } };

        private long ndx = -1;
        private Mtable()
        {
            this.ndx = -1;
        }
        public Mtable(long primeLmit)
        {
            this.ndx = Find_ndx(primeLmit);
        }

        private long Find_ndx(long primeLmit)
        {
            if (this.ndx < 0)
            {
                for (int idx = 0; idx < M.Length; idx++)
                {
                    if (M[idx, 0] == primeLmit)
                    {
                        this.ndx = idx;
                        return this.ndx;
                    }
                }
            }
            if (this.ndx < 0)
                return -1;
            else
                return this.ndx;
        }

        public long GetPrimeCount(long primeLmit)
        {
            if (this.ndx >= 0)
                return M[ndx, 1];
            else
                this.ndx = Find_ndx(primeLmit);
                if (this.ndx >= 0)
                    return M[ndx, 1];
                else
                    return -1; // not found
        }

        public long GetPrimeSqrt(long primeLmit)
        {
            if (this.ndx >= 0)
                return M[ndx, 2];
            else
                this.ndx = Find_ndx(primeLmit);
                if (this.ndx >= 0)
                    return M[ndx, 2];
                else
                    return -1; // not found
        }
    }
}







