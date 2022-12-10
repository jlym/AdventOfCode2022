using System.Diagnostics;
using System.Text;


record Cell(int row, int col);

public class Program
{    
    public static void Main()
    {        
        var lines = File.ReadAllLines("./input.txt");
        List<List<int>> grid = lines.Select(
            line => line.Select(
                c => int.Parse(c.ToString())
            ).ToList()
        ).ToList();        
        

        var numRows = grid.Count;
        var numCols = grid[0].Count;
        
        HashSet<Cell> visible = new HashSet<Cell>();
        void FindVisible(IEnumerable<Cell> line)
        {
            var tallest = -1;

            foreach (var cell in line)
            {
                if (grid[cell.row][cell.col] > tallest)
                {
                    tallest = grid[cell.row][cell.col];
                    visible.Add(cell);
                }
            }
        }


        foreach (var line in LeftToRight(numRows, numCols))
        {
            FindVisible(line);
        }
        foreach (var line in RightToLeft(numRows, numCols))
        {
            FindVisible(line);
        }
        foreach (var line in TopToBottom(numRows, numCols))
        {
            FindVisible(line);
        }
        foreach (var line in BottomToTop(numRows, numCols))
        {
            FindVisible(line);
        }

        Console.Out.WriteLine(visible.Count);
    }

    static IEnumerable<IEnumerable<Cell>> LeftToRight(int numRows, int numCols)
    {
        return Enumerable.Range(0, numRows)
            .Select(row => Enumerable.Range(0, numCols)
                .Select(col => new Cell(row, col))
            );
    }

    static IEnumerable<IEnumerable<Cell>> RightToLeft(int numRows, int numCols)
    {
        return Enumerable.Range(0, numRows)
            .Select(row => Enumerable.Range(0, numCols)
                .Reverse()
                .Select(col => new Cell(row, col))
            );
    }

    static IEnumerable<IEnumerable<Cell>> TopToBottom(int numRows, int numCols)
    {
        return Enumerable.Range(0, numCols)
            .Select(col => Enumerable.Range(0, numRows)
                .Select(row => new Cell(row, col))
            );
    }

    static IEnumerable<IEnumerable<Cell>> BottomToTop(int numRows, int numCols)
    {
        return Enumerable.Range(0, numCols)
            .Select(col => Enumerable.Range(0, numRows)
                .Reverse()
                .Select(row => new Cell(row, col))
            );
    }
}
