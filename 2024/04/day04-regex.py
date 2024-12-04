import re

def part1(input: str) -> int:
  size = input.index('\n')
  pattern = [
    # horizontal
    "XMAS",
    "SAMX",
    # vertical
    "X.{%d}M.{%d}A.{%d}S" % (size, size, size),
    "S.{%d}A.{%d}M.{%d}X" % (size, size, size),
    # top left to bottom right
    "X.{%d}M.{%d}A.{%d}S" % (size+1, size+1, size+1),
    "S.{%d}A.{%d}M.{%d}X" % (size+1, size+1, size+1),
    # top right to bottom left
    "X.{%d}M.{%d}A.{%d}S" % (size-1, size-1, size-1),
    "S.{%d}A.{%d}M.{%d}X" % (size-1, size-1, size-1),
  
  ]
  return sum([len(list(re.finditer(f"(?=({p}))", input, re.DOTALL))) for p in pattern])

def part2(input: str) -> int:
  size = input.index('\n')
  pattern = [
    "M.M.{%d}A.{%d}S.S" % (size-1, size-1),
    "S.S.{%d}A.{%d}M.M" % (size-1, size-1),
    "S.M.{%d}A.{%d}S.M" % (size-1, size-1),
    "M.S.{%d}A.{%d}M.S" % (size-1, size-1),
  ]
  return sum([len(list(re.finditer(f"(?=({p}))", input, re.DOTALL))) for p in pattern])

print(f'part1: {part1(open('test1.txt').read())}')
print(f'part2: {part2(open('test2.txt').read())}')
print(f'part1: {part1(open('input.txt').read())}')
print(f'part2: {part2(open('input.txt').read())}')
