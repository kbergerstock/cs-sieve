# prime number Sieve
# Keith R. Bergerstock
# alternate uses same algorithm but stores data in a bytearray

import time
import sys
from sieve_alt import M, Primes


def milliseconds(t0, t2):
    return (t2 - t0) * 1000


# main
def time_sieve(prime_limit, time_limit, output):
    cnt = 0  # pass counter
    duration = 0
    val = 0
    cnd = 0
    p = []
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
        val = primes.validate
        cnd = primes.counted()
        if output:
            p = primes.get_primes()
        if not val():
            raise RuntimeError("Vlidation of counted primes failed")

    # prints the results of the sieve
    if val:
        print(
            f"passes: {0:4d}  time: {1:9.4f} Ms Avg: {2:7.4f} Ms  Limit: {3:8d}  count: {4:4d} valid: {5}",
            cnt,
            duration,
            duration / cnt,
            prime_limit,
            val,
            cnd,
        )

    if val and output:
        print(p)


if __name__ == "__main__":
    prime_limit = 1000000
    time_limit = 10000  # time unit is in mS
    output = False

    for argc in sys.argv:
        cmd = argc[0:3]
        val = argc[3:]
        if cmd == "--":
            time_limit = int(val)
        if cmd == "--n":
            prime_limit = int(val)
        if cmd == "--s":
            output = True

    if prime_limit in M:
        try:
            time_sieve(prime_limit, time_limit, output)
        except RuntimeError as msg:
            print(msg)
    else:
        print("invalid prime limit")
