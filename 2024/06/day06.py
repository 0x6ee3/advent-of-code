from concurrent.futures import ProcessPoolExecutor
from functools import partial

def part1(input: str) -> int:
  size = int(input.index('\n'))
  input = input.replace('\n', '')
  pos = int(input.index('^'))
  step = -size
  def turn(step):
    if step == size: return -1
    if step == -1: return -size
    if step == -size: return 1
    return size
  while 0 < pos < len(input) and pos % size != 0:
    input = input[:pos] + 'X' + input[pos+1:]
    if pos+step > len(input):
      break
    if input[pos+step] == '#':
      step = turn(step)
    pos += step
  return input.count('X')

def test_for_loop(input, size, pos, i):
  if input[i] != '.':
    return 0
  def turn(step):
    if step == size: return -1
    if step == -1: return -size
    if step == -size: return 1
    return size
  input = input[:i] + '#' + input[i+1:]
  visited = []
  step = -size
  while 0 < pos < len(input) and pos % size != 0:
    if pos+step > len(input):
      break
    if (pos, step) in visited:
      return 1
    visited.append((pos, step))
    if input[pos+step] == '#':
      step = turn(step)
    pos += step
  return 0

def part2(input: str) -> int:
  size = int(input.index('\n'))
  input = input.replace('\n', '')
  pos = int(input.index('^'))
  loop = partial(test_for_loop, input, size, pos)
  with ProcessPoolExecutor() as pool:
    return sum(pool.map(loop, range(len(input))))

for filename in ['test1.txt', 'input.txt']:
  with open(filename) as f:
    input = f.read()
  print(f'== {filename} ==')
  print(f'part1: {part1(input)}')
  print(f'part2: {part2(input)}')
