import os

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
  for a in l1:
    similarity = 0
    for b in l2:
      if a == b:
        similarity += 1
    dist += int(a) * similarity
  return dist

for filename in ['test.txt', 'input.txt']:
  with open(filename) as f:
    input = f.read()
  print('= day01 =')
  print(f'== {filename} ==')
  print(f'part1: {part1(input)}')
  print(f'part2: {part2(input)}')
