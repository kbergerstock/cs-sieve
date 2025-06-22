# prime number Sieve
# Keith R. Bergerstock
# alternate uses same algorithm but stores data in a bytearray

import time
import sys

# the table includes the square root of the sieve limit as the second column
M = {
    10: [4, 3],
    100: [25, 10],
    1000: [168, 32],
    10000: [1229, 100],
    100000: [9592, 316],
    1000000: [78498, 1000],
    10000000: [664579, 3162],
    100000000: [5761455, 10000],
}


def milliseconds(t0, t2):
    return (t2 - t0) * 1000

class Primes:
    def __init__(self, max):
        self.sqrt = M[max][1]
        self.n = max
        self.nbits = 1 + max >> 1
        # the byte array allocator returns an array of zero's
        self.v = bytearray(self.nbits)


    def get_primes(self):
        """returns an array of prime numbers
        based on the results in the sieve array"""
        p = [2]
        for jdx in range(3, self.n, 2):
            if self.v[jdx >> 1] == 0:
                p.append(jdx)
        return p

    def mark_multiples(self, jdx):
        """mark all multiples of prime"""
        inc = 2 * jdx
        ndx = jdx * jdx
        while ndx < self.n:
            self.v[ndx>>1] = 1
            ndx += inc

    def sieve2(self):
        """performs the sieve, results:
        0 indicates that the number is a PRIME
        1 indicates that the number is a multiple of a prime
        """
        # assumes v is all zero's on entry
        # and that n is the max.
        #
        # value To stop the search sqrt of number of primes
        #
        # by eliminating all floating po+-int ops (especially the sqrt function)
        # i reduced the run time to under 90 milliseconds
        self.v[0] = 1
        for jdx in range(3, self.sqrt, 2):
            if self.v[jdx >> 1] == 0:
                self.mark_multiples(jdx)

    def counted(self):
        """returns the number of counted primes"""
        # adjust for not including TWO as a factor
        return 1 + self.v.count(0)

    def validate(self):
        """verifies the counted primes against the expected"""
        return self.counted() == M[self.n][0]


# main
def time_sieve(prime_limit, time_limit, output):
    cnt = 0  # pass counter
    duration = 0
    print("--n{}".format(prime_limit))
    print("--t{}".format(time_limit))   
    while duration < time_limit:
        primes = Primes(prime_limit)  # the sieve
        # i only want to time the sieve performance
        t1 = t0 = time.perf_counter()
        primes.sieve2()
        t1 = time.perf_counter()
        duration += milliseconds(t0, t1)
        cnt += 1
        if not primes.validate():
            break

    print(
        "passes: {0:4d}  time: {1:9.4f} Ms Avg: {2:7.4f} Ms  Limit: {3:8d}  count: {4:4d} valid: {5}".format(
            cnt,
            duration,
            duration / cnt,
            prime_limit,
            primes.counted(),
            primes.validate(),
        )
    )

    if output:
        print(primes.get_primes())


if __name__ == "__main__":
    prime_limit = 1000000
    time_limit = 10000  # time unit is in mS
    output = False

    for argc in sys.argv:
        cmd = argc[0:3]
        val = argc[3:]
        if cmd == "--t":
            time_limit = int(val)
        if cmd == "--n":
            prime_limit = int(val)
        if cmd == "--s":
            output = True

    if prime_limit in M:
        time_sieve(prime_limit, time_limit, output)
    else:
        print("invalid prime limit")
