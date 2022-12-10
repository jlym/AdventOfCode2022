public class Program
{
    const int MarkerSize = 14;
    
    public static void Main()
    {
        var text = File.ReadAllText("./input.txt");
        var i = FindMarkerEnd(text);
        var position = i + 1;
        Console.Out.WriteLine(position);
    }

    static int FindMarkerEnd(string text)
    {
        var buffer = new Queue<char>();
        var freq = new CharFrequencies();
        for (var i = 0; i < text.Length; i++)
        {
            if (buffer.Count == MarkerSize)
            {
                var first = buffer.Dequeue();
                freq.Remove(first);
            }

            var c = text[i];
            buffer.Enqueue(c);
            freq.Add(c);

            if (freq.CountUnique == MarkerSize)
            {
                return i;
            }
        }

        throw new ArgumentException("no marker");
    }
}

class CharFrequencies
{
    Dictionary<char, int> charToCount = new Dictionary<char, int>();
    
    public int CountUnique 
    {
        get 
        {
            return charToCount.Count;
        }
    }

    public void Add(char c)
    {
        if (!charToCount.ContainsKey(c))
        {            
            charToCount[c] = 0;
        }
        charToCount[c]++;
    }

    public void Remove(char c)
    {
        if (!charToCount.ContainsKey(c))
        {
            return;
        }

        charToCount[c]--;
        if (charToCount[c] == 0)
        {
            charToCount.Remove(c);
        }        
    }
}

