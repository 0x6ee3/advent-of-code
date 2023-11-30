import itertools
import math
import os

def distance(head: tuple[int, int], tail: tuple[int, int]) -> int:
    return int(abs(math.dist(head, tail)))
    
def step(pos: tuple[int, int], direction: str) -> tuple[int, int]:
    match direction:
        case 'U': return (pos[0], pos[1]-1)
        case 'R': return (pos[0]+1, pos[1])
        case 'D': return (pos[0], pos[1]+1)
        case 'L': return (pos[0]-1, pos[1])

def part1(input: str) -> int:
    tail = head = (0, 0)
    path = {tail}

    for move in input.splitlines():
        direction, steps = move.split(' ')
        for _ in range(int(steps)):
            prev = head
            head = step(head, direction)
            if distance(tail, head) > 1:
                path.add((tail := prev))

    return len(path)

if __name__ == '__main__':
    dir = os.path.dirname(__file__)
    for filename in ['test1.txt', 'input.txt']:
        if os.path.exists(f'{dir}/{filename}'):
            with open(f'{dir}/{filename}') as f:
                input = f.read()
            print(f'== {filename} ==')
            print(part1(input))
