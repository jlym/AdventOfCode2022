record Position(int R, int C, int Distance);

class Program
{
    const char Start = 'S';
    const char End = 'E';

    List<string> grid = new List<string>();
    int numRows;
    int numCols;
    

    (int, int) FindEnd()
    {
        for (int r = 0; r < numRows; r++)
        {
            for (int c = 0; c < numCols; c++)
            {
                if (grid[r][c] == End)
                {
                    return (r, c);
                }
            }
        }

        throw new Exception("could not find start");
    }

    public void Run()
    {
        grid = File.ReadAllLines("./input.txt").ToList();
        numRows = grid.Count;
        numCols = grid[0].Length;

        var (endR, endC) = FindEnd();
        var queue = new Queue<Position>();
        queue.Enqueue(new Position(endR, endC, 0));

        var seen = new HashSet<(int, int)>()
        {
            (endR, endC)
        };

        int bestDistance = int.MaxValue;

        while (queue.Count > 0)
        {
            var (r, c, distance) = queue.Dequeue();
            if (grid[r][c] == Start || grid[r][c] == 'a')
            {
                bestDistance = Math.Min(bestDistance, distance);                
            }

            var neighbors = GetNeighbors(r, c, seen);

            foreach (var n in neighbors)
            {                
                seen.Add(n);

                var (nr, nc) = n;
                queue.Enqueue(new Position(nr, nc, distance + 1));
            }            
        }

        Console.WriteLine(bestDistance);
    }

    int GetHeight(int r, int c)
    {
        var n = grid[r][c];
        if (n == Start)
        {
            n = 'a';
        }
        else if (n == End)
        {
            n = 'z';
        }

        return n - 'a';
    }

    List<(int, int)> GetNeighbors(int r, int c, HashSet<(int, int)> seen)
    {
        var neighbors = new List<(int, int)>();

        var currentHeight = GetHeight(r, c);

        var possibleNeighbors = new List<(int, int)>()
        {
            (r - 1, c), // Down
            (r + 1, c), // Up
            (r, c - 1), // Left
            (r, c + 1), // Right
        };

        foreach (var n in possibleNeighbors)
        {
            var (nr, nc) = n;
            if (seen.Contains(n) || nr < 0 || nr >= numRows || nc < 0 || nc >= numCols)
            {
                continue;
            }

            var nHeight = GetHeight(nr, nc);
            if (currentHeight > nHeight + 1)
            {
                continue;
            }

            neighbors.Add(n);
        }

        return neighbors;
    }

    public static void Main(string[] args)
    {
        var p = new Program();
        p.Run();        
    }    
}
