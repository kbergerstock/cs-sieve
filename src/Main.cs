
namespace PrimeSieve
{
    class Program
    {
		long MaxPrime = 1000000;
		int MaxTime = 5000;
		bool option = false;
		
		static void ParseArgs(string[] args)
		{
			foreach (var arg in args)
            {
                if (arg.StartsWith("--s"))
                {
                    option = true;
                }
                else if (arg.StartsWith("--n"))
                {
                    if (int.TryParse(arg.Substring("--n".Length), out int max))
                    {
                        MaxPrime = max;
                    }
                }
                else if (arg.StartsWith("--t"))
                {
                    if (int.TryParse(arg.Substring("--t".Length), out int max))
                    {
                        MaxTime = max;
                    }
                }
                Console.WriteLine("primeSieve!");
                Console.WriteLine($" prime limit {0}", MaxPrime);
                Console.WriteLine($" time limit {0} milliseconds", MaxTime);
			}
		}
		

        public static void TimeSieve(long MaxPrime, int MaxTime, bool show)
        {
            Mtable M = new Mtable(MaxPrime);
            long Expected = M.GetPrimeCount(MaxPrime);
            int cntPasses = 0;
            long duration = 0;
            bool valid = false;
            long[] primeList = new long [1];

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            while (stopWatch.ElapsedMilliseconds <= MaxTime)
            {
                Sieve sieve = new Sieve(MaxPrime, ref M);
                sieve.Sieve2();
                if (sieve.Valid)
                {
                    cntPasses++;
                    valid = sieve.Valid;
                    if (show &&  primeList.Length < 3)
                        primeList = sieve.GetPrimes();
                }
                else
                {
                    string msg = $"the acutual count {sieve.Count} does not match the expected{Expected}";
                    throw new ApplicationException(msg);
                }
            }
            stopWatch.Stop();
            duration = stopWatch.ElapsedTicks;
            // OUTPUT THE RESULTS
            Console.WriteLine($"number of passes {cntPasses}");
            Console.WriteLine($"quanity of clock tics used {duration}");
        }
		
		
		static void Main(string[] args)
        {
			ParseArgs(args)
			try
			{
				TimeSieve(MaxPrime, MaxTime, option);
			}
			catch (ApplicationException ex)
			{
				Console.WriteLine(ex.Message);
			}
        }
    }
}