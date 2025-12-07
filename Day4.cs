namespace AdventofCode2025;

public class Day4
{
    public object Part1()
    {
        var map = File.ReadAllLines("Day4.txt").Select(l => l.ToArray()).ToArray();

        int ret = 0;
        for (int y = 0; y < map.Length; y++)
        {
            for (int x = 0; x < map[y].Length; x++)
            {
                if (map[y][x] == '@' && Neighbours(x, y, map).Count(c => c == '@') < 4)
                {
                    ret++;
                }
            }
        }
        
        return ret;
    }

    private static IEnumerable<char> Neighbours(int x, int y, char[][] map)
    {
        var delta = new (int dx, int dy)[] { (-1, -1), (-1, 0), (-1, 1), (0, -1), (0, 1), (1, -1), (1, 0), (1, 1) };
        foreach (var (dx, dy) in delta)
        {
            var nx = x + dx;
            var ny = y + dy;
            if (nx >= 0 && nx < map.Length && ny >= 0 && ny < map[nx].Length)
            {
                yield return map[ny][nx];
            }
        }
    }

    public object Part2()
    {
        var map = File.ReadAllLines("Day4.txt").Select(l => l.ToArray()).ToArray();

        int ret = 0;
        bool changed = true;
        while (changed)
        {
            changed = false;
            for (int y = 0; y < map.Length; y++)
            {
                for (int x = 0; x < map[y].Length; x++)
                {
                    if (map[y][x] == '@' && Neighbours(x, y, map).Count(c => c == '@') < 4)
                    {
                        ret++;
                        changed = true;
                        map[y][x] = '.';
                    }
                }
            }

        }

        return ret;
    }
}