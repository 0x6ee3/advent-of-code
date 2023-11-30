import os

strategy_what_to_play = [
    [ 4, 8, 3 ],
    [ 1, 5, 9 ],
    [ 7, 2, 6 ] ]

strategy_how_to_play = [
    [ 3, 4, 8 ],
    [ 1, 5, 9 ],
    [ 2, 6, 7 ] ]

ORDA = ord('A')
ORDX = ord('X')

def play(games: list[str], strats: list[list[int]]) -> int:
    return sum([strats[ord(game[0]) - ORDA][ord(game[2]) - ORDX] for game in games])

def part1(input: str) -> int:
    return play(input.splitlines(), strategy_what_to_play)

def part2(input: str) -> int:
    return play(input.splitlines(), strategy_how_to_play)

if __name__ == '__main__':
    dir = os.path.dirname(__file__)
    for filename in ['test.txt', 'input.txt']:
        if os.path.exists(f'{dir}/{filename}'):
            with open(f'{dir}/{filename}') as f:
                input = f.read()
            print(f'== {filename} ==')
            print(part1(input))
            print(part2(input))
