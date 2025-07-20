using System.Diagnostics;


namespace PrimeSieve
{
    class Program
    {
		static long MaxPrime = 100;
		static int  MaxTime = 50;
		static bool option = true;
		
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
			}
			
			Console.WriteLine("primeSieve!");
			Console.WriteLine($" prime limit {MaxPrime}");
			Console.WriteLine($" time limit {MaxTime} milliseconds");
		}
		

        public static void TimeSieve()
        {
            Mtable M = new Mtable(MaxPrime);
            long Expected = M.GetPrimesExpected(MaxPrime);
			if(Expected < 0){
				string msg = "max prime not found.";
                throw new ApplicationException(msg);
			}
            int cntPasses = 0;
            long duration = 0;
            bool valid = false;
            Sieve sieve = new Sieve(MaxPrime, ref M);

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            while (stopWatch.ElapsedMilliseconds <= MaxTime)
            {
                valid = sieve.Sieve2();
				if (valid)
					cntPasses++;
                else
                {
					long np = sieve.CountPrimes(3);
					long cp = sieve.Count();
                    string msg = $"the actual count {cp}, {np} does not match the expected {Expected}";
                    throw new ApplicationException(msg);
                }
            }
            stopWatch.Stop();
            duration = stopWatch.ElapsedTicks;
            // OUTPUT THE RESULTS
            Console.WriteLine($"number of passes {cntPasses}");
            Console.WriteLine($"quantity of clock tics used {duration}");                    
			if (option )
			{
				int ii = 1;
				long [] p = sieve.GetPrimes();
				foreach(var prime in p){
					Console.Write($",  {prime}");
					ii++;
					if( ii % 10 == 0)
						Console.WriteLine();	
				}
			}	
        }
		
		
		static void Main(string[] args)
        {
			ParseArgs(args);
			try
			{
				TimeSieve();
			}
			catch (ApplicationException ex)
			{
				Console.WriteLine(ex.Message);
			}	
        }
    };
}