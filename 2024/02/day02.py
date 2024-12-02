
def is_safe(levels: list[int]) -> bool:
  step = levels[0]
  for next_step in levels[1:]:
    if not 1 <= abs(next_step - step) <= 3:
      return False
    step = next_step
  return levels == sorted(levels) or levels == sorted(levels, reverse=True)

def part1(input: str) -> int:
  safe = []
  unsafe = []
  for report in input.splitlines():
    levels = list(map(int, report.split()))
    if is_safe(levels):
      safe.append(levels)
    else:
      unsafe.append(levels)
  return len(safe)

def part1(input: str) -> int:
  safe = []
  unsafe = []
  for report in input.splitlines():
    levels = list(map(int, report.split()))
    if is_safe(levels):
      safe.append(levels)
    else:
      unsafe.append(levels)
  # brute force it baby!
  for level in unsafe:
    for x in range(len(level)):
      damped_levels = level[:x]+level[x+1:]
      if is_safe(damped_levels):
        safe.append(damped_levels)
        break
  return len(safe)

for filename in ['test.txt', 'input.txt']:
  with open(filename) as f:
    input = f.read()
  print('= day02 =')
  print(f'== {filename} ==')
  print(f'part1: {part1(input)}')
#  print(f'part2: {part2(input)}')

