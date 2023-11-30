
def parse_monkey(input: str) -> 'Monkey':
    m = Monkey()
    for line in input.splitlines():
        match line.lstrip().split(':'):
            case [name, '']:
                m.name = name
            case ['Starting items', items]: 
                m.items = [int(i) for i in items.split(',')]
            case ['Operation', operation]:
                op_args = operation.split(' ')[4:] # skip "new = old"
                m.operation = parse_operation(*op_args)
            case ['Test', div]:
                m.div = int(div.split(' ')[-1])
            case ['If true', target]:
                m.monkey_true = int(target.split(' ')[-1])
            case ['If false', target]:
                m.monkey_false = int(target.split(' ')[-1])
    return m

def parse_operation(op: str, arg: str) -> callable:
    # not very robust nor elegant but gets the job done
    match [op, arg]:
        case ['+', 'old']:
            return lambda x: x + x
        case ['*', 'old']:
            return lambda x: x * x
        case ['+', num]:
            return lambda x: x + int(num)
        case ['*', num]:
            return lambda x: x * int(num)
        case _:
            print(f'failed to parse operation {op} {arg}')
            exit(1)

class Monkey:
    common_div: int
    
    def __init__(self) -> None:
        self.name: str
        self.items: list[int]
        self.operation: callable
        self.div: int
        self.monkey_true: int
        self.monkey_false: int
        self.monkey_business = 0

    def __repr__(self) -> str:
        return f'{self.name}: {self.items} with {self.monkey_business} actions'

    def inspect(self, monkeys: list['Monkey']) -> None:
        while len(self.items) > 0:
            self.monkey_business += 1
            item = self.items.pop()
            item = self.operation(item)
            item = item // 3
            if item % self.div == 0:
                monkeys[self.monkey_true].items.append(item)
            else:
                monkeys[self.monkey_false].items.append(item)

    def inspect2(self, monkeys: list['Monkey']) -> None:
        while len(self.items) > 0:
            self.monkey_business += 1
            item = self.items.pop()
            item = self.operation(item)
            item = item % self.common_div
            if item % self.div == 0:
                monkeys[self.monkey_true].items.append(item)
            else:
                monkeys[self.monkey_false].items.append(item)
