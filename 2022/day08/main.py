from itertools import product
import os

def parse_grid(input: str) -> list[int]:
    grid = []

    for line in input.splitlines():
        grid.append([int(line[i]) for i in range(0, len(line))])

    return grid

def tree_visible(grid: list[int], x: int, y: int) -> bool:
    if y == 0 or y == len(grid)-1 or x == 0 or x == len(grid[0])-1: 
        return True

    left = max(grid[y][0:x])
    right = max(grid[y][x+1:])
    up = max([grid[yy][x] for yy in range(y-1, -1, -1)])
    down = max([grid[yy][x] for yy in range(y+1, len(grid))])

    return min(left, right, up, down) < grid[y][x]

def score(tree: int, treeline: list[int]) -> int:
    s = 0
    for t in treeline:
        s += 1
        if t >= tree:
            break
    return s

def scenic_score(grid: list[int], x: int, y: int) -> int:
    if y == 0 or y == len(grid)-1 or x == 0 or x == len(grid[0])-1: 
        return 0
    
    up = score(grid[y][x], [grid[yy][x] for yy in range(y-1, -1, -1)])
    left = score(grid[y][x], grid[y][0:x][::-1])
    down = score(grid[y][x], [grid[yy][x] for yy in range(y+1, len(grid))])
    right = score(grid[y][x], grid[y][x+1:])

    return up * left * down * right

def part1(input: str) -> int:
    grid = parse_grid(input)
    visible = 0

    for y, x in product(range(len(grid)), range(len(grid[0]))):
        if tree_visible(grid, x, y):
            visible += 1

    return visible

def part2(input: str) -> int:
    grid = parse_grid(input)
    most_scenic = 0

    for y, x in product(range(len(grid)), range(len(grid[0]))):
        most_scenic = max(most_scenic, scenic_score(grid, x, y))

    return most_scenic

if __name__ == '__main__':
    dir = os.path.dirname(__file__)
    for filename in ['test.txt', 'input.txt']:
        if os.path.exists(f'{dir}/{filename}'):
            with open(f'{dir}/{filename}') as f:
                input = f.read()
            print(f'== {filename} ==')
            print(part1(input))
            print(part2(input))
