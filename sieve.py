# prime number Sieve
# Keith R. Bergerstock
# data is stored in a bitarray
# use "pip install bitarray" to install package

import time
import sys
import bitarray

# from icecream import ic
# ic(jdx,ndx, bitpos(ndx), self.v[bitpos(ndx)])

M = {
    10: [4,3],
    100: [25,10],
    1000: [168,32],
    10000: [1229,100],
    100000: [9592,316],
    1000000: [78498,1000],
    10000000: [664579,3162],
    100000000: [5761455,10000],
}


def milliseconds(t0: int, t2: int):
    return (t2 - t0) * 1000


def bitpos(jdx):
    return 1 + (jdx >> 1)


class Primes:
    def __init__(self, max: int):
        self.n = max
        self.sqrt = M[max][1]
        self.nbits = 1 + (max >> 1)
        self.v = bitarray.bitarray(self.nbits)
        self.v[0 : self.nbits] = 0
        self.v[0] = 1
        self.nprimes = 0
        self.cnt = 0

    def get_primes(self):
        """returns an array of prime numbers
        based on the results in the sievearray"""
        p = [2]
        for jdx in range(3, self.n, 2):
            if self.v[bitpos(jdx)] == 0:
                p.append(jdx)
        return p

    def mark_multiples(self, jdx):
        # mark all multiples of prime
        # starting at the prime squared
        ndx = jdx * jdx
        inc = 2 * jdx
        while ndx < self.n:
            # calc the bit pos
            self.v[bitpos(ndx)] = 1
            ndx += inc

    def sieve2(self):
        """performs the sieve, results:
        0 indicates that the number is a PRIME
        1 indicates that the number is a multiple of a prime"""
        # assumes v is all zero's on entry
        # and that n is the max value To stop the search on
        # by eliminating all floating point ops (especially the sqrt function)
        # i reduced the run time to under 90 milliseconds
        # ic(self.nbits)
        self.v[0] = 1
        jdx = 3
        while jdx <= self.sqrt:
            if self.v[bitpos(jdx)] == 0:
                # self.mark_multiples(jdx)
                self.mark_multiples(jdx)
            jdx += 2

    def counted(self):
        """returns the number of counted primes"""
        self.nprimes = 1 + self.v.count(0, 2, self.nbits)
        return self.nprimes

    def validate(self):
        """verifies the counted primes against the expected"""
        return self.counted() == M[self.n][0]


# main
def time_sieve(prime_limit: int, time_limit: int, output: bool):
    cnt = 0  # pass counter
    primes = Primes(prime_limit)  # the sieve
    duration = 0
    print("--n{}".format(prime_limit))
    print("--t{}".format(time_limit))
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

    print(
        "passes: {0:4d}  time: {1:9.4f} Ms Avg: {2:7.4f} Ms Limit: {3:8d}  count: {4:4d} valid: {5}\n".format(
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
    # time unit is in mS
    time_limit = 10000
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

    # ic("--t ", time_limit)
    # ic("--n ", prime_limit)
    if prime_limit in M:
        time_sieve(prime_limit, time_limit, output)
    else:
        print("invalid prime limit")
