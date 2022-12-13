class Program
{
    public static void Main(string[] args)
    {
        var history = new List<int>() { 1 };
        var lines = File.ReadLines("./input.txt");

        foreach (var line in lines)
        {
            if (line == "noop")
            {
                history.Add(history.Last());
                continue;
            }

            var val = int.Parse(line.Substring("addx ".Length));
            history.Add(history.Last());
            history.Add(history.Last() + val);
        }
        
        var sum = 0;
        var ns = new List<int>() { 20, 60, 100, 140, 180, 220 };
        foreach (var n in ns)
        {
            sum += history[n - 1] * n;
        }

        Console.Out.WriteLine(sum);
    }
}
