
def build_vfs(input: str):
    cur = root = VfsDir(None, '/')
    for line in input.splitlines():
        match line.split(' '):
            case ['$', 'ls']: pass
            case ['$', 'cd', '/']: cur = root
            case ['$', 'cd', '..']: cur = cur.parent
            case ['$', 'cd', path]: cur = cur.get_dir(path)
            case ['dir', path]: cur.dirs.append(VfsDir(cur, path))
            case [size, name]: cur.files.append(VfsFile(cur, name, size))
            case _: raise ValueError(f'unknown command {line}')
    return root

class VfsDir:
    def __init__(self, parent: 'VfsDir', path: str) -> None:
        self.parent = parent
        self.path = path
        self.dirs: list['VfsDir'] = []
        self.files: list['VfsFile'] = []

    def get_dir(self, path) -> 'VfsDir':
        for d in self.dirs:
            if d.path == path:
                return d
        raise ValueError(path)

    def size(self) -> int:
        fsize = sum([f.size for f in self.files])
        dsize = sum([d.size() for d in self.dirs])
        return fsize + dsize

    def size_list(self, sizes: list[int]) -> list[int]:
        sizes.append(self.size())
        for d in self.dirs:
            d.size_list(sizes)
        return sizes

class VfsFile:
    def __init__(self, dir: VfsDir, name: str, size: int) -> None:
        self.dir = dir
        self.name = name
        self.size = size
