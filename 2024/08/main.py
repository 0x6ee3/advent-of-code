
def part1(input: str) -> int:
  input = [list(line) for line in input.splitlines()]
  size = len(input)
  antennas = dict()
  antennaset = set()
  for y in range(size):
    for x in range(size):
      p = input[y][x]
      if p != '.':
        if antennas.get(p, None) is None:
          antennas[p] = []
        antennas[p].append((x, y))
        antennaset.add((x, y))
  antinodes = set()
  for freq in antennas:
    positions = antennas[freq]
    for i in range(len(positions)-1):
      for j in range(i+1, len(positions)):
        p1, p2 = positions[i], positions[j]
        vec = (p2[0]-p1[0], p2[1]-p1[1])
        p1vec = (p1[0]-vec[0], p1[1]-vec[1])
        p2vec = (p2[0]+vec[0], p2[1]+vec[1])
        if 0 <= p1vec[0] < size and 0 <= p1vec[1] < size:
          antinodes.add(p1vec)
        if 0 <= p2vec[0] < size and 0 <= p2vec[1] < size:
          antinodes.add(p2vec)
  return len(antinodes)

def part2(input: str) -> int:
  return 2

for filename in ['test.txt', 'input.txt']:
  with open(filename) as f:
    input = f.read()
  print(f'== {filename} ==')
  print(f'part1: {part1(input)}')
  print(f'part2: {part2(input)}')

