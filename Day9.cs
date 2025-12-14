
using System.Diagnostics;

namespace AdventofCode2025;

public class Day9
{
    public object Part1()
    {
        var coord = File.ReadAllLines("Day9.txt").Select(Parse).ToArray();

        long largest = long.MinValue;
        for (int a = 0; a < coord.Length; a++)
        {
            for (int b = a + 1; b < coord.Length; b++)
            {
                var area = (Math.Abs(coord[a].x - coord[b].x) + 1L) * (Math.Abs(coord[a].y - coord[b].y) + 1L);
                if (area > largest)
                    largest = area;
            }
        }

        return largest;
    }

    public object Part2()
    {
        var coord = File.ReadAllLines("Day9.txt").Select(Parse).ToArray();
        var xCoords = coord.Select(c => c.x).Distinct().OrderBy(x => x).ToArray();
        var yCoords = coord.Select(c => c.y).Distinct().OrderBy(y => y).ToArray();
        var map = new byte[xCoords.Length + 1, yCoords.Length + 1];
        
        // Fill map
        for (int i = 0; i < coord.Length; i++)
        {
            var pt1 = coord[i];
            var pt2 = (i == coord.Length - 1) ? coord[0] : coord[i + 1];
            if (pt1.y == pt2.y)
            {
                var yIx = yCoords.IndexOf(pt1.y);
                var startX = Math.Min(pt1.x, pt2.x);
                var endX = Math.Max(pt1.x, pt2.x);
                var xIx = xCoords.IndexOf(startX);
                var x = startX;
                while (x <= endX)
                {
                    map[xIx, yIx] = 1;
                    xIx++;
                    if (xIx < xCoords.Length)
                        x = xCoords[xIx];
                    else
                        x = int.MaxValue;
                }
            }
            else if(pt1.x == pt2.x)
            {
                var xIx = xCoords.IndexOf(pt1.x);
                var startY = Math.Min(pt1.y, pt2.y);
                var endY = Math.Max(pt1.y, pt2.y);
                var yIx = yCoords.IndexOf(startY);
                var y = startY;
                while (y <= endY)
                {
                    map[xIx, yIx] = 1;
                    yIx++;
                    if (yIx < yCoords.Length)
                        y = yCoords[yIx];
                    else
                        y = int.MaxValue;
                }
            }
            else
            {
                throw new Exception("Knas");
            }
        }

        int fx = xCoords.Length * 2 / 3;
        int fy = yCoords.Length * 2 / 3;
        FloodFillMap(map, fx, fy);
        //PrintMap(map);
        
        // Find rectangle
        long largest = long.MinValue;
        for (int a = 0; a < coord.Length; a++)
        {
            for (int b = a + 1; b < coord.Length; b++)
            {
                if (IsRectangleInside(coord[a], coord[b], map, xCoords, yCoords))
                {
                    var area = (Math.Abs(coord[a].x - coord[b].x) + 1L) * (Math.Abs(coord[a].y - coord[b].y) + 1L);
                    if (area > largest)
                        largest = area;
                }
            }
        }
        
        return largest;
    }

    private static bool IsRectangleInside((int x, int y) pt1, (int x, int y) pt2, byte[,] map, int[] xCoords, int[] yCoords)
    {
        var xStart = xCoords.IndexOf(Math.Min(pt1.x, pt2.x));
        var xStop = xCoords.IndexOf(Math.Max(pt1.x, pt2.x));
        var yIx = yCoords.IndexOf(Math.Min(pt1.y, pt2.y));
        var yStop = yCoords.IndexOf(Math.Max(pt1.y, pt2.y));
        while (yIx <= yStop)
        {
            for(var xIx = xStart; xIx <= xStop; xIx++)
                if (map[xIx, yIx] == 0)
                    return false;
            yIx++;
        }
        return true;
    }

    private static void FloodFillMap(byte[,] map, int x, int y)
    {
        var visited = new HashSet<(int x, int y)>();
        var toVisit = new Stack<(int x, int y)>();
        toVisit.Push((x, y));
        while (toVisit.Count > 0)
        {
            var coord = toVisit.Pop();
            if (!visited.Add(coord))
                continue;
            if(coord.x < 0 || coord.x >= map.GetLength(0) || coord.y < 0 || coord.y >= map.GetLength(1))
                continue;
            if (map[coord.x, coord.y] != 0)
                continue;
            map[coord.x, coord.y] = 2;
            toVisit.Push((coord.x - 1, coord.y));
            toVisit.Push((coord.x, coord.y - 1));
            toVisit.Push((coord.x + 1, coord.y));
            toVisit.Push((coord.x, coord.y + 1));
        }
    }

    private static void PrintMap(byte[,] map)
    {
        var bitmap = new System.Drawing.Bitmap(map.GetLength(0), map.GetLength(1));
        for(int y = 0; y < map.GetLength(1); y++)
        {
            for (int x = 0; x < map.GetLength(0); x++)
            {
                System.Drawing.Color color;
                if(map[x, y] == 0)
                    color = System.Drawing.Color.Black;
                else if (map[x, y] == 1)
                    color = System.Drawing.Color.White;
                else
                    color = System.Drawing.Color.Gray;
                bitmap.SetPixel(x, y, color);
            }
        }
        
        bitmap.Save("Day9.png");
        var psi = new ProcessStartInfo
        {
            FileName = "Day9.png",
            UseShellExecute = true
        };
        Process.Start(psi);
    }


    private static (int x, int y) Parse(string line)
    {
        var s = line.Split(',');
        return (int.Parse(s[0]), int.Parse(s[1]));
    }
}