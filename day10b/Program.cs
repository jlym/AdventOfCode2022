using System.Text;

class Program
{
    public static void Main(string[] args)
    {
        var registerValues = new List<int>() { 1 };
        var lines = File.ReadLines("./input.txt");

        foreach (var line in lines)
        {
            if (line == "noop")
            {
                registerValues.Add(registerValues.Last());
                continue;
            }

            var val = int.Parse(line.Substring("addx ".Length));
            registerValues.Add(registerValues.Last());
            registerValues.Add(registerValues.Last() + val);
        }
        
        var crt = new StringBuilder();

        for (var cycle = 0; cycle < 240; cycle++)
        {
            var x = cycle % 40;
            var spriteMid = registerValues[cycle];

            if (spriteMid >= x -1 && spriteMid <= x + 1)
            {
                crt.Append("#");
            }
            else
            {
                crt.Append(" ");
            }

            if (x == 39)
            {
                crt.AppendLine();
            }
        }

        Console.Out.WriteLine(crt.ToString());
    }
}
