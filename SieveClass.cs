// Copyright (c) 2025, Keith R. Bergerstock
namespace PrimeSieve
{
	
    public class Sieve
    {
        private long primeLimit;
        private long iSqrt;
        private long expected;
        private long nbits;
        private Mtable M;
        private bool[] bitArray;
        private long primeCnt;

        public long Count()
        {
            return this.primeCnt;
        }

		// set up conditions to execute the sieve 
        public Sieve(long prime_max, ref Mtable mtable)
        {
            this.M = mtable;
            this.primeLimit = prime_max;
            this.iSqrt = M.GetPrimeSqrt();
            this.expected = M.GetPrimesExpected();
            this.bitArray = new bool[10];
            this.primeCnt = 0;
            this.nbits = 0;
        }

        //returns an array of prime numbers
        //based on the results in the sieve array
        public long[] GetPrimes()
        {
            long[] p = new long[this.expected];
            // account for 2 being the first prime
            p[0] = 2;
            // set the fill array index value to the next empty spot
            long ndx = 1;
            // now process the odds looking for rte primes
            for (long idx = 3; idx < this.primeLimit; idx += 2)
            {
                if (!this.bitArray[idx >> 1])
                    p[ndx++] = idx;
            }
            // return the array of prime numbers
            return p;
        }

        // marks the multiples of the current PRIME true
        public void MarkMultiples(long jdx)
        {
            // to skip the even multiples
            long inc = 2 * jdx;
            // the first multiple to mark
            long ndx = jdx * jdx;
            while (ndx < this.primeLimit)
            {
                // shifting right once gives the indice
                // into the bitArray ex 3>>,5>2=2. 9>>1=4
                this.bitArray[ndx >> 1] = true;
                ndx += inc;
            }
        }

        // performs the sieve, results: a bool array in which
        // false indicates that the number is a PRIME
        // true indicates that the number is a multiple of a prime
        public bool Sieve2()
        {
            this.primeCnt = 0;
            this.nbits = 1 + primeLimit >> 1;
            this.bitArray = new bool[this.nbits];
            this.bitArray[0] = true;
            // starting factor for the sieve
            long idx = 3;
            // if the factor is greater than the square root of
            // the prime limit we are done
            for (; idx < this.iSqrt; idx += 2)
            {
                // if the curent bit in the array is a false
                // then it is a Primes
                if (!this.bitArray[idx >> 1])
                {
                    MarkMultiples(idx);
                    this.primeCnt++;
                }
            }
            // at this we have the counted up to isqrt
            //now we count the rest
            this.primeCnt += CountPrimes(idx);
            // ensure that expected matches reality
            return this.Validate();
        }

		// count the primes from a specified index value 
        public long CountPrimes(long jdx)
        {
            long pcnt = 0;
            for (; jdx < primeLimit; jdx += 2)
            {
                if (!this.bitArray[jdx >> 1])
                    pcnt++;
            }
            return pcnt;
        }

        public bool Validate()
        {
            // adding one to primeCnt accounts for the prime 2
            return this.expected == (1 + this.primeCnt);
        }

        public bool Validate2()
        {
            return this.expected == (1 + this.CountPrimes(3));
        }
    }
}
