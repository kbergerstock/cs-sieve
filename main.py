# prime number Sieve
# Keith R. Bergerstock
# alternate uses same algorithm but stores data in a bytearray

from icecream import ic
import time
import sys
from sieve_alt import M, Primes


def milliseconds(t0, t2):
    return (t2 - t0) * 1000


# main
def time_sieve(prime_limit, time_limit, output):
    pass_cntr = 0  # pass counter
    duration = 0
    val = 0
    prime_cnt = 0
    primes = Primes()  # the sieve
    p = []
    sys.stdout.writelines(f"--n{prime_limit}\n")
    sys.stdout.writelines(f"--t{time_limit}\n")

    start = time.perf_counter()
    while duration < time_limit:
        # i only want to time the sieve performance
        primes.init(prime_limit)
        prime_cnt, val = primes.sieve2()
        ic(prime_cnt, val)
        duration = milliseconds(start, time.perf_counter())
        if val:
            pass_cntr += 1
        else:
            error = Exception("Timed sieve")
            error.add_note(f"Validation of counted primes failed in pass {pass_cntr}")
            raise error

    # prints the results of the sieve/7
    if val:
        sys.stdout.writelines(f"passes: {pass_cntr} \n")
        sys.stdout.writelines(f"duration : {duration:9.4f}\n")
        sys.stdout.writelines(f"average time per run {duration / pass_cntr:7.4f}\n")
        sys.stdout.writelines(f"Limit: {prime_limit}\n")
        sys.stdout.writelines(f"primes found: {prime_cnt}\n")
        sys.stdout.writelines(f"validation results: {val}\n")

    if val and output:
        lc = 0
        primes = primes.get_primes()
        for p in primes:
            sys.stdout.write(f"{p:10}")
            lc += 1
            if lc % 10 == 0:
                sys.stdout.write("\n")


def processArgs(argv):
    prime_limit = 1000000  # default max prime IS 1,000,000
    time_limit = 5000  # time unit is 5 SECONDS in mS
    output = False

    for argc in argv:
        cmd = argc[0:3]
        val = argc[3:]
        if cmd == "--t":
            time_limit = int(val)
        if cmd == "--n":
            prime_limit = int(val)
        if cmd == "--s":
            output = True

    if not prime_limit in M:
        error = Exception("processArgs")
        error.add_note(f"invalid max prime {prime_limit}")
        raise error
    else:
        return time_limit, prime_limit, output


def main(argv):
    time_max = 0
    prime_max = 0
    show = False
    # PROCESS THE CMD LINE ARGS
    time_max, prime_max, show = processArgs(argv)
    # i check if the prime limit is valid here
    # so that the sieve does not need to

    try:
        time_sieve(prime_max, time_max, show)
    except Exception as err:
        for note in err.__notes__:
            sys.stdout.writelines(note + "\n")


if __name__ == "__main__":
    main(sys.argv)
