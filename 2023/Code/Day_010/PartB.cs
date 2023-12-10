namespace Code.Day_010;

public partial class PipeMaze
{
    public long Solve_PartB(string inputPath = "Inputs/010.in")
    {
        var maze = File.ReadAllLines(inputPath);
        var start = PopulateGridAndGetStart(maze);
        var symbols = new char[] { '|', '-', 'L', 'J', '7', 'F' };

        foreach (var sym in symbols)
        {
            _grid[start] = sym;
            if (GetNeighbors(start).Count == 2) break;
        }

        var result = Solve_BFS(start, point => GetNeighbors(point));
        
        long inside = 0;
        for (int x = 0; x < maze.Length; ++x)
        {
            long left = 0;
            for (int y = 0; y < maze[x].Length; ++y)
            {
                // Check the parity of |, L, J (bottom corners and tile size)
                if (result.visited.Contains(new Point(x, y)) == false && left % 2 == 1)
                {
                    inside++;
                }
                
                if (GetGridValueSafe(x, y) is '|' or 'L' or 'J' && result.visited.Contains(new Point(x, y)))
                {
                    left++;
                }
            }
        }

        return inside;
    }
}