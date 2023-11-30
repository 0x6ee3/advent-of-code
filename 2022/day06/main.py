import os

def find_marker(line: str, length: int) -> int:
    for i in range(length, len(line), 1):
        if len(set(line[i-length:i])) == length:
            return i
    return -1


def part1(input: str) -> list[int]:
    results = []
    for line in input.splitlines():
        results.append(find_marker(line, 4))
    return results
    
def part2(input: str) -> list[int]:
    results = []
    for line in input.splitlines():
        results.append(find_marker(line, 14))
    return results

if __name__ == '__main__':
    dir = os.path.dirname(__file__)
    for filename in ['test.txt', 'input.txt']:
        if os.path.exists(f'{dir}/{filename}'):
            with open(f'{dir}/{filename}') as f:
                input = f.read()
            print(f'== {filename} ==')
            print(part1(input))
            print(part2(input))
