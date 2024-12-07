def walk(res: int, acc: int, nums: list[int]):
  if len(nums) == 0:
    return res == acc
  return walk(res, acc+nums[0], nums[1:]) or walk(res, acc*nums[0], nums[1:])

def part1(input: str) -> int:
  numbas = [list(map(lambda x: int(x), line.replace(':', '').split(' '))) for line in input.splitlines()]
  return sum([nums[0] for nums in numbas if walk(nums[0], 0, nums[1:])])

def walk2(res: int, acc: int, nums: list[int]):
  if len(nums) == 0 or acc > res:
    return res == acc
  return walk2(res, acc+nums[0], nums[1:]) or walk2(res, acc*nums[0], nums[1:]) or walk2(res, int(str(acc)+str(nums[0])), nums[1:])

def part2(input: str) -> int:
  numbas = [list(map(lambda x: int(x), line.replace(':', '').split(' '))) for line in input.splitlines()]
  return sum([nums[0] for nums in numbas if walk2(nums[0], 0, nums[1:])])

for filename in ['test.txt', 'input.txt']:
  with open(filename) as f:
    input = f.read()
  print('= day07 =')
  print(f'== {filename} ==')
  print(f'part1: {part1(input)}')
  print(f'part2: {part2(input)}')

