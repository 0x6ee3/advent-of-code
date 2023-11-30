# Advent of Code 2022

The solutions are written in a "good enough to get the job done" code style with Python 3.

## Minimal runner setup

Every `dayXY` folder will have a main.py with the same basic setup

```py
import os

def part1(input: str) -> int:
    ...

def part2(input: str) -> int:
    ...

if __name__ == '__main__':
    dir = os.path.dirname(__file__)
    for filename in ['test.txt', 'input.txt']:
        if os.path.exists(f'{dir}/{filename}'):
            with open(f'{dir}/{filename}') as f:
                input = f.read()
            print(f'== {filename} ==')
            print(part1(input))
            print(part2(input))

```
