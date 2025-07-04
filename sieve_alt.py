# prime number Sieve
# Keith R. Bergerstock
# alternate uses same algorithm but stores data in a bytearray

from logging import exception
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
            self.v[ndx >> 1] = 1
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
