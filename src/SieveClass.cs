
namespace PrimeSieve{
    
    public class Sieve {
        private long primeLimit = 0;
        private long sqrtLimit = 0;
		private long expected = 0;
        private long nbits = 0;
        private Mtable M;
        private bool[] bitArray;
        private long primeCnt = 0;

        public long Count()
        {
            return this.primeCnt; 
        }
		
        public Sieve(long prime_max, ref Mtable mtable )
        {
            this.M = mtable;
            this.primeLimit = prime_max;
            this.sqrtLimit = M.GetPrimeSqrt(this.primeLimit);
            this.expected = M.GetPrimesExpected(this.primeLimit);
        }

        //returns an array of prime numbers
        //based on the results in the sieve array
        public long[] GetPrimes()
        {
            long size = 1 + this.M.GetPrimesExpected(this.primeLimit);
            long[] p = new long[size];
             // account for 2 being the first prime
            p[0] = 2;  
            // set the fill array indice to the next empty spoot
            long ndx= 1;     
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
            // the first multiple to ,srk
            long ndx = jdx * jdx;       
            while (ndx < this.primeLimit)
            {
                // shifting right once gives the indice
                // into the bitArray ex 3>>,5>2=2. 9>>1=4
                this.bitArray[ndx >> 1] = true;
                ndx += inc;
            }
        }

        // performs the sieve, results:
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
            for (; idx < this.sqrtLimit; idx += 2)
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
			//now we count rhe rest
			this.primeCnt += CountPrimes(idx);
			// ensure that expected matchs reality
			return this.Validate();
        }

        public long CountPrimes(long jdx)
        {
			long pcnt = 0;
            for (; jdx < primeLimit; jdx += 2){
                if (!this.bitArray[jdx>>1]) 
					pcnt++;
				}
            return pcnt;
        }

        public bool Validate()
        {
			// adding one to primeCnt accounts for the prime 2
            return this.M.GetPrimesExpected(this.primeLimit) == 1 + this.primeCnt;
        }

        public bool Validate2()
        {
            return this.M.GetPrimesExpected(this.primeLimit) == 1 + this.CountPrimes(3);
        }
    }
}