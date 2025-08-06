from  sieve_alt import Primes
from  sieve_alt import M


def test_init():
    sieve = Primes()
    data =(99,100,999,100000,1000)
    for n in data:
        sieve.init(n)
        assert sieve.max == 1000, "storage into max failed"
        assert sieve.expected == 168,"expectd value not found1"
        assert sieve.sqrt == 32, "sqrt value not found"
    
    
def test_mark_multiples():
    sieve = Primes()
    sieve.init(100)
    p =(3,5,7)
    for f in p:
        sieve.mark_multiples(f)
        for vdx in range(f*f,100,f*2):
            assert sieve.v[vdx] == 1,"sieve.mark_multiples failed'
    assert sieve.counted()  == 24 , "sieve.counted failed"

def test_sieve2():
    sieve = Primes()
    data= (90,100,1000,10,10000000)
    for MAX in data:
        sieve.init(MAX)
        assert
        cntd,val = sieve.seve2()
        assert cnted == sieve.expected