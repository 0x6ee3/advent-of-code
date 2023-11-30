import os

def priorities(items: list[str]) -> int:
    priorities = 0
    for item in items:
        if item.islower():
            priorities += ord(item) - ord('a') + 1
        else:
            priorities += ord(item) - ord('A') + 27
    return priorities

def part1(input: str) -> int:
    shared_items = []
    for rucksack in input.splitlines():
        bag1 = rucksack[:len(rucksack)//2]
        bag2 = rucksack[len(rucksack)//2:]
        shared_items.extend(set(bag1).intersection(bag2))
    return priorities(shared_items)

def part2(input: str) -> int:
    shared_items = []
    rucksacks = iter(input.splitlines())
    for r1, r2, r3 in zip(rucksacks, rucksacks, rucksacks):
        shared_items.extend(set(r1).intersection(r2).intersection(r3))
    return priorities(shared_items)

if __name__ == '__main__':
    dir = os.path.dirname(__file__)
    for filename in ['test.txt', 'input.txt']:
        if os.path.exists(f'{dir}/{filename}'):
            with open(f'{dir}/{filename}') as f:
                input = f.read()
            print(f'== {filename} ==')
            print(part1(input))
            print(part2(input))
