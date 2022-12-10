using System.Diagnostics;
using System.Text;

class Directory
{
    public string Name { get; init; }
    public Directory? Parent { get; init; }
    public int FileSize { get; set; } = 0;
    public List<Directory> Directories { get; init; } = new List<Directory>();

    public Directory(string name, Directory? parent = null)
    {
        Name = name;
        Parent = parent;
    }

    public override string ToString()
    {
        return ToString(0);
    }

    public string ToString(int offset)
    {
        var s = new StringBuilder();

        s.Append(' ', offset * 4);
        s.Append($"- {Name} ({FileSize})");
        s.AppendLine();
        foreach (var child in this.Directories)
        {
            var childStr = child.ToString(offset + 1);
            s.Append(childStr);
        }

        return s.ToString();
    }
}


public class Program
{    
    public static void Main()
    {        
        var lines = File.ReadLines("./input.txt");

        var builder = new Builder();
        foreach (var line in lines)
        {
            builder.Apply(line);
        }

        Console.Out.WriteLine(builder.Root.ToString());

        var (_, totalSmallDirSize) = FindSizeOfSmallDirectories(builder.Root);
        Console.Out.WriteLine(totalSmallDirSize);
    }

    static (int, int) FindSizeOfSmallDirectories(Directory dir)
    {
        var totalSize = dir.FileSize;
        var totalChildSmallDirSize = 0;

        foreach (var child in dir.Directories)
        {
            var (childTotalSize, childSmallDirSize) = FindSizeOfSmallDirectories(child);
            totalSize += childTotalSize;
            totalChildSmallDirSize += childSmallDirSize;
        }

        var totalSmallDirSize =
            totalSize <= 100000 ?
            totalChildSmallDirSize + totalSize :
            totalChildSmallDirSize;

        return (totalSize, totalSmallDirSize);
    }
}

class Builder
{
    Directory current;

    public Directory Root { get; private init; }

    public Builder()
    {
        Root = new Directory("/");
        current = Root;
    }

    public void Apply(string line)
    {        
        if (line == "$ ls" || line == "$ cd /" || line.StartsWith("dir"))
        {
            // Skip cases that we don't really need.
            return;
        }
        else if (line.StartsWith("$ cd .."))
        {
            if (this.current.Parent != null)
            {
                this.current = this.current.Parent;
            }            
        }
        else if (line.StartsWith("$ cd"))
        {
            var dirName = line.Substring("$ cd ".Length);
            var newDir = new Directory(dirName, this.current);

            this.current.Directories.Add(newDir);
            this.current = newDir;
        }
        else
        {
            // If we get to this point, line should be for a file and its size.
            var tokens = line.Split(" ");
            Debug.Assert(tokens.Length == 2);
            var fileSize = int.Parse(tokens[0]);

            this.current.FileSize += fileSize;
        }
    }


}

