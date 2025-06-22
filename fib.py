def main(N):
    f1 = 1
    f2 = 2
    f3 = 0

    for i in range(3, N, 1):
        print(f1)
        f3 = f2 + f1
        f1 = f2
        f2 = f3


if __name__ == "__main__":
    main(64)
