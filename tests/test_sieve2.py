from  sieve_alt import Primes
from  sieve_alt import M

def test_sieve2():
    sieve = Primes(1000)
    sieve.sieve2()
    assert sieve.validate(), "Validation of counted primes failed"
    assert sieve.counted() == M[1000][0],"Counted primes do not match expected value"