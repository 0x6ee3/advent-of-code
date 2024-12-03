import re

def part1(input: str) -> int:
  return sum([int(x)*int(y) for (x, y) in re.findall("mul\((\d+),(\d+)\)", input)])

def part2(input: str) -> int:
  s = 0
  for unclean in re.split("do\(\)", input):
    parts = re.split("don't\(\)", unclean)
    s += part1(parts[0])
  return s

for filename in ['test1.txt', 'test2.txt', 'input.txt']:
  with open(filename) as f:
    input = f.read()
  print(f'== {filename} ==')
  print(f'part1: {part1(input)}')
  print(f'part2: {part2(input)}')

