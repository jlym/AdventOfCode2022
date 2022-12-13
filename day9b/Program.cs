record struct Vector(int X, int Y);
record Move(string Direction, int Count);

class Program
{
    HashSet<Vector> tailPositions = new HashSet<Vector>()
    {
        new Vector(0, 0),
    };
    List<Vector> rope = new List<Vector>();

    static Vector up = new Vector(0, 1);
    static Vector down = new Vector(0, -1);
    static Vector left = new Vector(-1, 0);
    static Vector right = new Vector(1, 0);

    static Dictionary<string, Vector> directionToVector = new Dictionary<string, Vector>()
    {
        { "U", up },
        { "D", down },
        { "L", left },
        { "R", right },
    };

    Vector Add(Vector a, Vector b)
    {
        return new Vector(a.X + b.X, a.Y + b.Y);
    }

    Vector Diff(Vector a, Vector b)
    {
        return new Vector(a.X - b.X, a.Y - b.Y);
    }

    double Amplitude(Vector a)
    {
        return Math.Sqrt(Math.Pow(a.X, 2) + Math.Pow(a.Y, 2));
    }

    void MoveHead(string direction)
    {
        var v = directionToVector[direction];
        rope[0] = Add(rope[0], v);
    }

    Vector GetTailMove(Vector head, Vector tail)
    {
        var delta = Diff(head, tail);
        
        var deltaAmplitude = Amplitude(delta);
        if (deltaAmplitude < 2)
        {
            return new Vector(0, 0);
        }
        
        if (delta == new Vector(2, 0))
        {
            return right;
        }
        else if (delta == new Vector(-2, 0))
        {
            return left;
        }
        else if (delta == new Vector(0, 2))
        {
            return up;
        }
        else if (delta == new Vector(0, -2))
        {
            return down;
        }
        else if (delta == new Vector(2, 2) || delta == new Vector(2, 1) || delta == new Vector(1, 2))
        {
            return new Vector(1, 1);
        }
        else if (delta == new Vector(-2, 2) || delta == new Vector(-2, 1) || delta == new Vector(-1, 2))
        {
            return new Vector(-1, 1);
        }
        else if (delta == new Vector(2, -2) || delta == new Vector(2, -1) || delta == new Vector(1, -2))
        {
            return new Vector(1, -1);
        }
        else if (delta == new Vector(-2, -2) || delta == new Vector(-2, -1) || delta == new Vector(-1, -2))
        {
            return new Vector(-1, -1);
        }
        else 
        {
            throw new Exception($"GetTailMove failed, head={head}, tail={tail}");
        }
    }

    void ApplyMove(string direction)
    {
        MoveHead(direction);

        for (int i = 1; i < rope.Count; i++)
        {
            var move = GetTailMove(rope[i - 1], rope[i]);
            rope[i] = Add(rope[i], move);
        }

        tailPositions.Add(rope.Last());
    }

    void Run()
    {
        var moves = File.ReadLines("./input.txt").Select(line => {
            var tokens = line.Split();
            return new Move(tokens[0], int.Parse(tokens[1]));
        });

        rope = Enumerable.Range(1, 10)
            .Select(_ => new Vector(0, 0))
            .ToList();

        foreach (var move in moves)
        {
            var (direction, count) = move;
            for (var i = 0; i < count; i++)
            {
                ApplyMove(direction);
            }
        }

        Console.Out.WriteLine($"{tailPositions.Count}");
    }

    public static void Main(string[] args)
    {
        var p = new Program();
        p.Run();
    }
}

