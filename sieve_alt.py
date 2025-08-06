# Keith R. Bergerstock
# alternate uses same algorithm but stores data in a bytearray
from icecream import ic
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
    def __init__(self):
        pass

    def init(self, max):
        self.max = max
        self.sqrt = M[max][1]
        self.expected = M[max][0]
        self.nbits = 1 + (max >> 1)
        self.primes_cntd = 0
        # the byte array allocator returns an array of zero's
        self.v = bytearray(self.nbits)

    def get_primes(self):
        p = [2]
        for vdx in range(3, self.max, 2):
            if self.v[vdx >> 1] == 0:
                p.append(vdx)
        return p

    def mark_multiples(self, vdx):
        """mark all multiples of prime"""
        inc = 2 * vdx
        ndx = vdx * vdx
        while ndx < self.max:
            self.v[ndx >> 1] = 1
            ndx += inc

    def sieve2(self):
        """performs the sieve, results:
        0 indicates that the number is a PRIME
        1 indicates that the number is a multiple of a prime
        """
        # assumes v is all zero's on entry
        # and that n is the max.
        # by eliminating all floating po+-int ops (especially the sqrt function)
        # i reduced the run time to under 90 milliseconds
        self.v[0] = 1
        self.v[-1] = 1
        self.primes_cntd = 0
        ic("start", self.v, self.primes_cntd, self.sqrt)
        # this for loop seraches the odd numbers start at 3
        # and the ending value To stop the search is sqrt of max prime
        if self.sqrt == 0:
            return self.sqrt, False
        else:
            for jdx in range(3, self.sqrt, 2):
                # index the prime array, by shiftng right once
                # if the value is a zero : prime found
                if self.v[jdx >> 1] == 0:
                    self.mark_multiples(jdx)
            # add 1 to the count to adjust for the prime 2
            self.primes_cntd = 1 + self.v.count(0)
            ic("end ", self.primes_cntd, self.expected, self.v.count(0))
            return self.primes_cntd, self.validate2()

    def counted(self):
        """returns the number of counted primes"""
        # adjust for not including TWO as a factor
        return 1 + self.v.count(0)

    def validate2(self):
        """verifies the counted primes against the expected"""
        return self.primes_cntd == self.expected

    def validate(self):
        """verifies the counted primes against the expected"""
        return self.counted() == self.expected
