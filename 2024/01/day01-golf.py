from collections import Counter

def part1(input: str) -> int:
  l1, l2 = zip(*[line.split() for line in input.splitlines()])
  return sum([abs(int(b)-int(a)) for a,b in zip(sorted(l1), sorted(l2))])

def part2(input: str) -> int:
  l1, l2 = zip(*[line.split() for line in input.splitlines()])
  cl2 = Counter(l2)
  return sum([int(a) * cl2[a] for a in l1])

for filename in ['test.txt', 'input.txt']:
  with open(filename) as f:
    input = f.read()
  print(f'== {filename} ==')
  print(f'part1: {part1(input)}')
  print(f'part2: {part2(input)}')
