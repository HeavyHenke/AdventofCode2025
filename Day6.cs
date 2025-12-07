using System.Text;

namespace AdventofCode2025;

public class Day6
{
    public object Part1()
    {
        var file = File
            .ReadAllLines("Day6.txt")
            .Select(l => l.Split(' ', StringSplitOptions.RemoveEmptyEntries))
            .ToArray();

        long total = 0;
        
        var mulOrAdd = file.Last();
        for (int col = 0; col < file[0].Length; col++)
        {
            bool isMul = mulOrAdd[col] == "*";
            long result = (isMul ? 1 : 0);
            for (int row = 0; row < file.Length - 1; row++)
            {
                if(isMul)
                    result *= long.Parse(file[row][col]);
                else
                    result += long.Parse(file[row][col]);
            }
            
            total += result;
        }
        
        return total;
    }
    
    public object Part2()
    {
        var file = File.ReadAllLines("Day6.txt");

        long total = 0;
        var values = new List<List<char>>(); // Transposed list
        char method = ' ';
        int colStart = 0;
        
        for (int col = 0; col < file[0].Length; col++)
        {
            if (file[^1][col] is '*' or '+')
            {
                if(method != ' ')
                    total += CalculateAndResetData(values, method);
                values.Clear();
                method = file[^1][col];
                colStart = col;
            }

            for (int row = 0; row < file.Length - 1; row++)
            {
                if(values.Count <= col - colStart)
                    values.Add([]);
                values[col - colStart].Add(file[row][col]);
            }
        }
        
        total += CalculateAndResetData(values, method);
        return total;
    }

    private static long CalculateAndResetData(List<List<char>> values, char method)
    {
        var numbers = values.Select(GetNum).Where(n => n != null).ToArray();
        if (numbers.Length == 0) return 0;
        
        if (method == '*')
            return numbers.Aggregate(1L, (a, l) => a * (l ?? 1));
        if (method == '+')
            return numbers.Sum(n => n ?? 0);
        throw new Exception("Knas!");
    }

    private static long? GetNum(List<char> values)
    {
        var numbers = values.Where(char.IsNumber).ToArray();
        if(numbers.Length == 0) return null;
        return long.Parse(numbers);
    }

}