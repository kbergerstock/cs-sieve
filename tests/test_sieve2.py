from  sieve_alt import Primes
from  sieve_alt import M

def test_sieve2():
    sieve = Primes(1000)
    sieve.sieve2()
    assert sieve.validate(), "Validation of counted primes failed"
    assert sieve.counted() == M[1000][1],"Counted primes do not match expected value"
    p =  sieve.get_primes()
    
    print(f"Primes up to prime_limit {0}\n",sieve.n,p)