import os

def part1(input: str) -> int:
    overlap = 0
    for elve_tasks in input.splitlines():
        ab, cd = elve_tasks.split(',')
        [a, b], [c, d] = ab.split('-'), cd.split('-')
        a, b, c, d = int(a), int(b), int(c), int(d)

        if a <= c <= d <= b or c <= a <= b <= d:
            overlap += 1

    return overlap

def part2(input: str) -> int:
    overlap = 0
    for elve_tasks in input.splitlines():
        ab, cd = elve_tasks.split(',')
        [a, b], [c, d] = ab.split('-'), cd.split('-')
        a, b, c, d = int(a), int(b), int(c), int(d)

        if a <= c <= d <= b or c <= a <= b <= d:
            overlap += 1
        elif a <= c <= b <= d or c <= a <= d <= b:
            overlap += 1

    return overlap

if __name__ == '__main__':
    dir = os.path.dirname(__file__)
    for filename in ['test.txt', 'input.txt']:
        if os.path.exists(f'{dir}/{filename}'):
            with open(f'{dir}/{filename}') as f:
                input = f.read()
            print(f'== {filename} ==')
            print(part1(input))
            print(part2(input))
