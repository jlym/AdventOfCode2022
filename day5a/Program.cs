using System.Text.RegularExpressions;
using System.Text;

record Move(int count, int from, int to);

class Stacks
{
    const int NumStacks = 9;

    private List<Stack<char>> stacks = new List<Stack<char>>();
    public Stacks()
    {
        this.stacks = new List<Stack<char>>();
        for (int i = 0; i < NumStacks + 1; i++)
        {
            this.stacks.Add(new Stack<char>());
        }
    }

    public void Push(int i, char value)
    {
        this.stacks[i].Push(value);
    }

    public void Move(Move move)
    {
        Console.Out.WriteLine(move);
        var (count, from, to) = move;
        
        for (int i = 1; i <= count; i++)
        {
            if (stacks[from].Count == 0)
            {
                break;
            }

            var value = stacks[from].Pop();
            stacks[to].Push(value);
        }
    }

    public override string ToString()
    {
        var result = new StringBuilder();

        for (int i = 1; i < NumStacks + 1; i++)
        {
            result.Append($"{i}: ");
            var stack = this.stacks[i];
            foreach (var c in stack)
            {
                result.Append($"{c}, ");
            }

            result.Append("\n");
        }

        return result.ToString();
    }

    
    public string GetTopRow()
    {
        var result = new StringBuilder();

        for (int i = 1; i < NumStacks + 1; i++)
        {
            var stack = this.stacks[i];
            if (stack.Count == 0)
            {
                Console.Out.WriteLine($"Skipping {i}");
                continue;
            }

            result.Append(stack.Peek());
        }

        return result.ToString();
    }
    
}

public class Program
{
    public static void Main()
    {
        var (stacks, moves) = Parse();
        Console.Out.WriteLine(stacks.ToString());
        foreach (var move in moves)
        {
            stacks.Move(move);
        }
        Console.Out.WriteLine(stacks.GetTopRow());
    }

    static (Stacks, List<Move>) Parse()
    {
        var lines = File.ReadAllLines("./input.txt");
        var i = 0;

        var stackLines = new Stack<string>();
        while (i < lines.Length && lines[i] != "")
        {
            stackLines.Push(lines[i]);
            i++;
        }

        i++;

        var moveLines = new List<string>();
        for (; i < lines.Length; i++)
        {
            moveLines.Add(lines[i]);
        }

        var stacks = ParseInitialStack(stackLines);
        var moves = ParseMoves(moveLines);

        return (stacks, moves);
    }

    static Stacks ParseInitialStack(Stack<string> stackLines)
    {
        var stacks = new Stacks();

        var labelLine = stackLines.Pop();
        var stackToLineIndex = new Dictionary<int, int>();

        for (var i = 0; i < labelLine.Length; i++)
        {
            var labelChar = labelLine[i];
            if (char.IsDigit(labelChar))
            {                
                var stackIndex = int.Parse(labelChar.ToString());
                stackToLineIndex[stackIndex] = i;
            }
        }
        
        while (stackLines.Count > 0)
        {
            var stackLine = stackLines.Pop();

            foreach (var stackIndex in stackToLineIndex.Keys)
            {
                var lineIndex = stackToLineIndex[stackIndex];
                var stackValue = stackLine[lineIndex];
                if (char.IsLetter(stackValue))
                {
                    stacks.Push(stackIndex, stackValue);
                }
            }
        }

        return stacks;
    }

    static List<Move> ParseMoves(List<string> moveLines)
    {
        return moveLines.
            Select((line) => ParseMove(line)).
            ToList();
    }

    static Move ParseMove(string moveLine)
    {
        var match = Regex.Match(moveLine, @"^move (\d+) from (\d+) to (\d+)$");
        
        var count = int.Parse(match.Groups[1].ToString());
        var from = int.Parse(match.Groups[2].ToString());
        var to = int.Parse(match.Groups[3].ToString());

        return new Move(count, from, to);
    }
}
