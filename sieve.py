# prime number Sieve
# Keith R. Bergerstock
# data is stored in a bitarray
# use "pip install bitarray" to install package

import time
import sys
import bitarray


M = {
    10: 4,
    100: 25,
    1000: 168,
    10000: 1229,
    100000: 9592,
    1000000: 78498,
    10000000: 664579,
    100000000: 5761455,
}


def milliseconds(t0, t2):
    return (t2 - t0) * 1000


class Primes:
    def __init__(self, max):
        self.n = max
        self.nl = max // 2
        self.v = bitarray.bitarray(self.nl)
        self.v[0:self.nl] = 0

    def get_primes(self):
        """ returns an array of prime numbers
            based on the results in the sievearray """
        p = [2]
        cnt = 0
        for k in self.v:
            if k == 0:
                p.append(1 + (cnt * 2))
            cnt += 1
        return p

    def sieve2(self):
        """ performs the sieve, results:
                0 indicates that the number is a PRIME
                1 indicates that the number is a multiple of a prme
        """
        # assumes v is all zero's on entry
        # and that n is the max value To stop the search on
        # by eliminating all floating point ops (especially the sqrt function)
        # i reduced the run time to under 90 milliseconds
        self.v[0] = 1
        for j in range(1, self.nl):
            if self.v[j] == 0:
                # calc the factor
                f = 1 + (j * 2)
                f2 = f * f
                # equivlent to f < sqrt(n)
                if f2 < self.n:
                    # mark all multiples of prime
                    # starting at the factored squared
                    for ndx in range((f2 - 1) // 2, self.nl, f):
                        # as not prime
                        self.v[ndx] = 1
                else:
                    # all multiples have been marked so stop
                    break

    def counted(self):
        """ returns the number of counted primes """
        # adjust for not including TWO as a factor
        return 1 + self.v.count(0)

    def validate(self):
        """ verifies the counted primes against the expected """
        return self.counted() == M[self.n]


# main
def time_sieve(prime_limit, time_limit, output):
    cnt = 0                         # pass counterf
    primes = Primes(prime_limit)    # the sieve
    duration = 0
    print("--n{}".format(prime_limit))
    print('--t{}'.format(time_limit))
    while duration < time_limit:
        primes = Primes(prime_limit)
        # i only want to time the sieve performance
        t1 = t0 = time.perf_counter()
        primes.sieve2()
        t1 = time.perf_counter()
        duration += milliseconds(t0, t1)
        cnt += 1
        if not primes.validate():
            break

    print("passes: {0:4d}  time: {1:9.4f} Ms Avg: {2:7.4f} Ms Limit: {3:8d}  count: {4:4d} valid: {5}\n"
          .format(cnt, duration, duration/cnt, prime_limit,
             primes.counted(), primes.validate()))

    if output:
        print(primes.get_primes())


if __name__ == "__main__":
    prime_limit = 1000000
    # time unit is in mS
    time_limit = 10000
    output = False

    for argc in sys.argv:
        cmd = argc[0:3]
        val = argc[3:]
        if cmd == '--t':
            time_limit = int(val)
        if cmd == '--n':
            prime_limit = int(val)
        if cmd == '--s':
            output = True

    if prime_limit in M:
        time_sieve(prime_limit, time_limit, output)
    else:
        print("invalid prime limit")
