from collections import Counter

def part1(m):
    c = Counter(m)
    return c['('] - c[')']

def part2(m):
    c = 0
    for i in range(len(m)):
        if m[i] == '(':
            c += 1
        else:
            c -= 1
        if c < 0:
            return i+1
    return 0

with open('input01.txt') as f:
    m = f.read()
    print(part1(m))
    print(part2(m))
