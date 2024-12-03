import re

def part1(input: str) -> int:
  return sum([int(x)*int(y) for (x, y) in re.findall("mul\((\d+),(\d+)\)", input)])

def part2a(input: str) -> int:
  # assuming do() and don't() perflectly alternate
  s = 0
  for unclean in re.split("do\(\)", input):
    parts = re.split("don't\(\)", unclean)
    s += part1(parts[0])
  return s

def part2b(input: str) -> int:
  # doesnt work :(
  return sum([int(x)*int(y) for (x, y) in re.findall("(?<=do\(\))[^(?don't\(\))]*mul\((\d+),(\d+)\)", "do()"+input)])

for filename in ['test.txt', 'test2.txt', 'input.txt']:
  with open(filename) as f:
    input = f.read()
  print(f'== {filename} ==')
  print(f'part1: {part1(input)}')
  print(f'part2a: {part2a(input)}')
  print(f'part2b: {part2b(input)}')

