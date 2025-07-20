using System.Diagnostics;


namespace PrimeSieve
{
    class Program
    {
		static long MaxPrime = 1000000;
		static int  MaxTime = 5000;
		static bool option = false;
		
		static void ParseArgs(string[] args)
		{
			foreach (var arg in args)
            { 	
                if (arg.StartsWith("--s"))
                    option = true;
                if (arg.StartsWith("--n")){
                    if (long.TryParse(arg.Substring("--n".Length), out long max))
                        MaxPrime = max;
				}
                if (arg.StartsWith("--t")){
                    if (int.TryParse(arg.Substring("--t".Length), out int max))
                        MaxTime = max;
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
			long milliseconds = stopWatch.ElapsedMilliseconds;
            long duration = stopWatch.ElapsedTicks;
            // OUTPUT THE RESULTS
            Console.WriteLine($"number of passes {cntPasses}");
            Console.WriteLine($"quantity of milliseconds used {milliseconds}");                    
			Console.WriteLine($"average time per pass {milliseconds/cntPasses}");
            Console.WriteLine($"quantity of clock tics used {duration}");                    
			if (option )
			{
				int ii = 0;
				long[] p = sieve.GetPrimes();
				foreach(var prime in p){
					Console.Write(string.Format("{0,7:#######}",prime));
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
};

//eof