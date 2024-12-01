from collections import Counter

def part1(input: str) -> int:
  l1, l2 = [], []
  for line in input.splitlines():
    ids = line.split()
    l1.append(ids[0])
    l2.append(ids[1])
  dist = 0
  for a,b in zip(sorted(l1), sorted(l2)):
    dist += abs(int(b)-int(a))
  return dist

def part2(input: str) -> int:
  l1, l2 = [], []
  for line in input.splitlines():
    ids = line.split()
    l1.append(ids[0])
    l2.append(ids[1])
  dist = 0
  cl2 = Counter(l2)
  for a in l1:
    dist += int(a) * cl2[a]
  return dist

for filename in ['test.txt', 'input.txt']:
  with open(filename) as f:
    input = f.read()
  print('= day01 =')
  print(f'== {filename} ==')
  print(f'part1: {part1(input)}')
  print(f'part2: {part2(input)}')

