

namespace PrimeSieve{
    public class Sieve {
        private long primeLimit = 0;
        private long sqrtLimit = 0;
        private long nbits = 0;
        private Mtable M;
        private bool[] v;
        private long pcnt = 1;
        private bool _valid = false;

        public long Count()
        {
            return this.pcnt; 
        }
        public bool Valid
        {
            get => this._valid;
            set
            {
                this._valid = value;
            }
        }

        private  Sieve()
        {
            throw new ApplicationException("some how you called the default constructor");
        }

        public Sieve(long prime_max, ref Mtable mtable )
        {
            this.M = mtable;
            this.primeLimit = prime_max;
            this.sqrtLimit = M.GetPrimeSqrt(this.primeLimit);
            this.nbits = 1 + primeLimit >> 1;
            this.v = new bool[this.nbits];
        }

        //returns an array of prime numbers
        //based on the results in the sieve array
        public long[] GetPrimes()
        {
            long size = 1 + this.M.GetPrimeSqrt(this.primeLimit);
            long[] p = new long[size];
            p[0] = 2;
            int ndx = 3;
            for (int ii = 3; ii < v.Length; ii += 2)
            {
                if (!this.v[ii])
                {
                    p[ndx++] = ii;
                }
            }
            return p;
        }

        public void MarkMultiples(long jdx)
        {
            long inc = 2 * jdx;
            long ndx = jdx * jdx;
            while (ndx < this.nbits)
            {
                v[ndx >> 1] = true;
                ndx += inc;
            }
        }

        //performs the sieve, results:
        // false indicates that the number is a PRIME
        // true indicates that the number is a multiple of a prime
        public void Sieve2()
        {
            this.v[0] = true;
            for (long jdx = 3; jdx < this.sqrtLimit; jdx = +2)
                if (!this.v[jdx >> 1])
                {
                    MarkMultiples(jdx);
                    this.pcnt++;
                }
            this.Valid = Validate2();
        }

        public long CountPrimes()
        {
            long cnt = 1;
            for (long jdx = 0; jdx < nbits; jdx++)
                if (!this.v[jdx])
                    cnt++;
            return cnt;
        }

        public bool Validate()
        {
            return this.M.GetPrimeCount(this.primeLimit) == CountPrimes();
        }

        public bool Validate2()
        {
            return this.M.GetPrimeCount(this.primeLimit) == Count();
        }
    }
}