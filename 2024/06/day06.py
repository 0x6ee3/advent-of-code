
def printmap(map, size):
  for i in range(0, len(map), size):
    print(map[i:i+size])
  print()

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

def part2(input: str) -> int:
  return 2

for filename in ['test1.txt', 'input.txt']:
  with open(filename) as f:
    input = f.read()
  print(f'== {filename} ==')
  print(f'part1: {part1(input)}')
  print(f'part2: {part2(input)}')
