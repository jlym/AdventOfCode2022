public class Program
{   
    private List<List<int>> heights;
    private int numRows;
    private int numCols;

    public Program()
    {
        var lines = File.ReadAllLines("./input.txt");
        this.heights = lines.Select(
            line => line.Select(
                c => int.Parse(c.ToString())
            ).ToList()
        ).ToList();        
        

        this.numRows = this.heights.Count;
        this.numCols = this.heights[0].Count;
    }

    int GetBestScenicScore()
    {
        int bestScore = 0;
        for (int row = 0; row < this.numRows; row++)
        {
            for (int col = 0; col < this.numCols; col++)
            {
                var score = this.GetScenicScore(row, col);
                bestScore = Math.Max(score, bestScore);
            }
        }
        return bestScore;

    }
    int GetScenicScore(int currentRow, int currentCol)
    {
        var currentHeight = this.Height(currentRow, currentCol);
        var scenicScore = 1;

        // Top to bottom
        var distance = 0;                
        for (var row = currentRow + 1; row < this.numRows; row++)
        {
            distance++;
            if (this.Height(row, currentCol) >= currentHeight)
            {
                break;
            }
        }
        scenicScore *= distance;

        // Bottom to top
        distance = 0;                
        for (var row = currentRow - 1; row >= 0; row--)
        {
            distance++;
            if (this.Height(row, currentCol) >= currentHeight)
            {
                break;
            }
        }
        scenicScore *= distance;

        // Left to right
        distance = 0;                
        for (var col = currentCol + 1; col < this.numCols; col++)
        {
            distance++;
            if (this.Height(currentRow, col) >= currentHeight)
            {
                break;
            }
        }
        scenicScore *= distance;

        // Right to left
        distance = 0;                
        for (var col = currentCol - 1; col >= 0; col--)
        {
            distance++;
            if (this.Height(currentRow, col) >= currentHeight)
            {
                break;
            }
        }
        scenicScore *= distance;

        return scenicScore;
    }

    int Height(int row, int col)
    {
        return this.heights[row][col];
    }

    public static void Main()
    {        
        Program p = new Program();
        Console.Out.WriteLine(p.GetBestScenicScore());
    }
}
