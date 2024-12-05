import re

def manual(input: str) -> tuple[list[int], list[tuple[int, int]]]:
  irules, ipages = input.split('\n\n')
  rules = [r.split('|') for r in irules.split('\n')]
  pages = ipages.split('\n')
  return rules, pages

def check_rules(page, rules) -> bool:
  for r in rules:
    if r[0] in page and r[1] in page:
      if page.index(r[1]) < page.index(r[0]):
        return False
  return True

def part1(input: str) -> int:
  rules, pages = manual(input)
  page_numbers = 0
  for page in pages:
    if check_rules(page, rules):
      p = page.split(',')
      page_numbers += int(p[len(p) // 2])
  return page_numbers

def apply_rules(page: list[int], rules: list[tuple[int, int]]) -> list[int]:
  for p in page:
    for r in filter(lambda r: r[0] == p and r[1] in page, rules):
      i1, i2 = page.index(r[0]), page.index(r[1])
      if i1 > i2:
        page[i1], page[i2] = page[i2], page[i1]
  return page

def part2(input: str) -> int:
  rules, pages = manual(input)
  page_numbers = 0
  for page in pages:
    if not check_rules(page, rules):
      p = page.split(',')
      while not check_rules(p, rules):
        p = apply_rules(p, rules)
      page_numbers += int(p[len(p) // 2])
  return page_numbers

for filename in ['test1.txt', 'input.txt']:
  with open(filename) as f:
    input = f.read()
  print(f'== {filename} ==')
  print(f'part1: {part1(input)}')
  print(f'part2: {part2(input)}')
