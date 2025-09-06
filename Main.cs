// Copyright (c) 2025, Keith R. Bergerstock

using System.Diagnostics;

namespace PrimeSieve
{
    internal class Program
    {
        static long MaxPrime = 1000000;
        static int MaxTime = 5000;
        static bool option = false;
		
		// parses the command line for valid parameters
		// ignores invalid parameters
        static void ParseArgs(string[] args)
        {
            foreach (var arg in args)
            {
                if (arg.StartsWith("--s"))
                {
                    option = true;
                    break;
                }
                if (arg.StartsWith("--n"))
                {
                    if (long.TryParse(arg.Substring("--n".Length), out long max))
                        MaxPrime = max;
                    break;
                }
                if (arg.StartsWith("--t"))
                {
                    if (int.TryParse(arg.Substring("--t".Length), out int max))
                        MaxTime = max;
                    break;
                }
            }

            Console.WriteLine("primeSieve!");
            Console.WriteLine($" prime limit {MaxPrime}");
            Console.WriteLine($" time limit {MaxTime} milliseconds");
        }

		// counts the number of prime sieves
		//that can be tun in the allotted time
        public static void TimeSieve()
        {
            Mtable M = new Mtable(MaxPrime);
            long Expected = M.GetPrimesExpected();
            int cntPasses = 0;
            bool valid = false;
            Sieve sieve = new Sieve(MaxPrime, ref M);

            Stopwatch timer = new Stopwatch();
            timer.Start();
            while (timer.ElapsedMilliseconds <= MaxTime)
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
            timer.Stop();
            long milliseconds = timer.ElapsedMilliseconds;
            long duration = timer.ElapsedTicks;
            // OUTPUT THE RESULTS
            Console.WriteLine($"number of passes {cntPasses}");
            Console.WriteLine($"quantity of milliseconds used {milliseconds}");
            Console.WriteLine($"average time per pass {milliseconds / cntPasses}");
            Console.WriteLine($"quantity of clock tics used {duration}");
            if (option)
            {
                int ii = 0;
                long[] p = sieve.GetPrimes();
                foreach (var prime in p)
                {
                    Console.Write("{var:10),");
                    ii++;
                    if (ii % 10 == 0)
                        Console.WriteLine();
                }
            }
        }
		// prpgram entry point
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
