import os

def parse_crates_moves(input: str) -> list[list[str], list[int]]:
    crates_input, moves_input = input.split('\n\n')
    crates_input = crates_input.splitlines()

    # use the length of the lines with the numbers to get the amount of crates.
    # will only work up to 9 crates..
    num_crates = (len(crates_input[-1])+1)//4 
    crates = [''] * num_crates

    for crate_line in crates_input[:-1]:
        # notes:
        # - we can find the crate content at _1___5___9___13...
        # - we can use the last line as length reference
        # - we can check for emptiness, because the input includes spaces
        for i in range(1, len(crates_input[-1]), 4):
            if crate_line[i] != ' ':
                crates[i//4] = crate_line[i] + crates[i//4]

    moves = []
    # move indizies are aligned to 0-based array numbering
    for move in moves_input.splitlines():
        _, amount, _, source, _, target = move.split(' ')
        moves.append([int(amount), int(source)-1, int(target)-1])
        
    return crates, moves

def part1(input: str) -> str:
    crates, moves = parse_crates_moves(input)

    for move in moves:
        count, source, target = move
        for _ in range(0, count):
            crates[target] += crates[source][-1]
            crates[source] = crates[source][:-1]

    result = ''
    for c in crates:
        if len(c) > 0:
            result += c[-1]

    return result

def part2(input: str) -> str:
    crates, moves = parse_crates_moves(input)

    for move in moves:
        count, source, target = move
        crates[target] += crates[source][-count:]
        crates[source] = crates[source][:-count]

    result = ''
    for c in crates:
        if len(c) > 0:
            result += c[-1]

    return result

if __name__ == '__main__':
    dir = os.path.dirname(__file__)
    for filename in ['test.txt', 'input.txt']:
        if os.path.exists(f'{dir}/{filename}'):
            with open(f'{dir}/{filename}') as f:
                input = f.read()
            print(f'== {filename} ==')
            print(part1(input))
            print(part2(input))
