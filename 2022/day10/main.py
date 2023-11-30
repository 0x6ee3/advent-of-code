import os

def part12(input: str) -> int:
    cycle = 1
    regx = 1
    signals = []
    crt = ''

    def tick():
        nonlocal cycle, signals, crt

        if (cycle + 20) % 40 == 0:
            signals.append(cycle * regx)

        if (cycle-1) % 40 in [regx-1, regx, regx+1]:
            crt += '#'
        else:
            crt += '.'
        if cycle % 40 == 0:
            crt += '\n'

        cycle += 1

    for command in input.splitlines():
        if command == 'noop':
            tick()
        elif command[0:4] == 'addx':
            tick()
            tick()
            regx += int(command[len('addx'):])
        else:
            print(f'unknown command {command}')

    print(f'signals: {sum(signals)}')
    print(f'crt:\n{crt}')

if __name__ == '__main__':
    dir = os.path.dirname(__file__)
    for filename in ['test.txt', 'input.txt']:
        if os.path.exists(f'{dir}/{filename}'):
            with open(f'{dir}/{filename}') as f:
                input = f.read()
            print(f'== {filename} ==')
            part12(input)
