import math
import os

def distance(pos1: tuple[int, int], pos2: tuple[int, int]) -> int:
    return int(abs(math.dist(pos1, pos2)))
    
def step(pos: tuple[int, int], direction: str) -> tuple[int, int]:
    match direction:
        case 'U': return (pos[0], pos[1] + 1)
        case 'D': return (pos[0], pos[1] - 1)
        case 'R': return (pos[0] + 1, pos[1])
        case 'L': return (pos[0] - 1, pos[1])

def move(tail: tuple[int, int], head: tuple[int, int]) -> tuple[int, int]:
    if distance(tail, head) <= 1:
        return tail

    if tail[0] == head[0] and tail[1] < head[1]:
        tail = (tail[0], tail[1] + 1)
    elif tail[0] == head[0] and tail[1] > head[1]:
        tail = (tail[0], tail[1] - 1)
    elif tail[1] == head[1] and tail[0] < head[0]:
        tail = (tail[0] + 1, tail[1])
    elif tail[1] == head[1] and tail[0] > head[0]:
        tail = (tail[0] - 1, tail[1])

    elif tail[0] < head[0] and tail[1] < head[1]:
        tail = (tail[0] + 1, tail[1] + 1)
    elif tail[0] < head[0] and tail[1] > head[1]:
        tail = (tail[0] + 1, tail[1] - 1)
    elif tail[0] > head[0] and tail[1] < head[1]:
        tail = (tail[0] - 1, tail[1] + 1)
    elif tail[0] > head[0] and tail[1] > head[1]:
        tail = (tail[0] - 1, tail[1] - 1)

    return move(tail, head)

def dumprope(cmd, rope, path):
    print(f'\n== {cmd} ==')
    size = 20
    for y in range(size, -size, -1):
        for x in range(-size, size):
            pos = (x, y)
            if pos in rope:
                idx = rope.index(pos)
                if idx == 0:
                    print('H', sep='', end='')
                elif idx == len(rope):
                    print('T', sep='', end='')
                else:
                    print(idx, sep='', end='')
            else:
                if pos in path:
                    print('#', sep='', end='')
                elif x == 0:
                    print('|', sep='', end='')
                elif y == 0:
                    print('-', sep='', end='')
                else:
                    print('.', sep='', end='')
            print(' ', sep='', end='')
        print('\n', sep='', end='')
    print('')

def part2(input: str) -> int:
    rope_length = 10
    start = (0, 0)
    rope = [start] * rope_length
    path = [start]

    for cmd in input.splitlines():
        direction, steps = cmd.split(' ')
        for _ in range(int(steps)):
            rope[0] = step(rope[0], direction)

            for i in range(1, rope_length):
                if distance(rope[i], rope[i-1]) > 1:
                    rope[i] = move(rope[i], rope[i-1])

            if rope[-1] not in path:
                path.append(rope[-1])

        #dumprope(cmd, rope, path)

    return len(path)

if __name__ == '__main__':
    dir = os.path.dirname(__file__)
    for filename in ['test2.txt', 'input.txt']:
        if os.path.exists(f'{dir}/{filename}'):
            with open(f'{dir}/{filename}') as f:
                input = f.read()
            print(f'== {filename} ==')
            print(part2(input))

