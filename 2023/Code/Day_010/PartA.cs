using System.Collections;

namespace Code.Day_010;

public partial class PipeMaze
{
    private readonly Dictionary<Point, char> _grid = new();
    
    public record BFSResults(Dictionary<Point, long> distances, Dictionary<Point, Point> parents, HashSet<Point> visited);

    public record Point(long x, long y);
    
    public BFSResults Solve_BFS(Point start, Func<Point, List<Point>> neighbors)
    {
        var queue = new Queue<Point>();
        queue.Enqueue(start);

        var distances = new Dictionary<Point, long>();
        distances.Add(start, 0);
        
        var visited = new HashSet<Point>();
        visited.Add(start);
        
        var parents = new Dictionary<Point, Point>();

        while (queue.TryDequeue(out var value))
        {
            foreach (var neighbor in neighbors(value))
            {
                if (visited.Contains(neighbor) == false)
                {
                    visited.Add(neighbor);
                    parents.Add(neighbor, value);
                    distances.Add(neighbor, distances[value] + 1);
                    
                    queue.Enqueue(neighbor);
                }
            }
        }
        
        return new BFSResults(distances, parents, visited);
    }

    public char GetGridValueSafe(long x, long y)
    {
        if (_grid.TryGetValue(new Point(x, y), out char value))
        {
            return value;
        }

        return '.';
    }

    public List<Point> GetNeighbors(Point cell)
    {
        List<Point> neighbors = new();
        var (x, y) = cell;
        var ch = _grid[cell];
        
        switch (ch)
        {
            case '|':
                if (GetGridValueSafe(x - 1, y) is '|' or '7' or 'F') neighbors.Add(new Point(x - 1, y));
                if (GetGridValueSafe(x + 1, y) is '|' or 'J' or 'L') neighbors.Add(new Point(x + 1, y));
                break;
            case '-':
                if (GetGridValueSafe(x, y - 1) is '-' or 'L' or 'F') neighbors.Add(new Point(x, y - 1));
                if (GetGridValueSafe(x, y + 1) is '-' or '7' or 'J') neighbors.Add(new Point(x, y + 1));
                break;
            case 'L':
                if (GetGridValueSafe(x - 1, y) is '|' or '7' or 'F') neighbors.Add(new Point(x - 1, y));
                if (GetGridValueSafe(x, y + 1) is '-' or '7' or 'J') neighbors.Add(new Point(x, y + 1));
                break;
            case 'J':
                if (GetGridValueSafe(x - 1, y) is '|' or '7' or 'F') neighbors.Add(new Point(x - 1, y));
                if (GetGridValueSafe(x, y - 1) is '-' or 'L' or 'F') neighbors.Add(new Point(x, y - 1));
                break;
            case '7':
                if (GetGridValueSafe(x, y - 1) is '-' or 'L' or 'F') neighbors.Add(new Point(x, y - 1));
                if (GetGridValueSafe(x + 1, y) is '|' or 'J' or 'L') neighbors.Add(new Point(x + 1, y));
                break;
            case 'F':
                if (GetGridValueSafe(x, y + 1) is '-' or '7' or 'J') neighbors.Add(new Point(x, y + 1));
                if (GetGridValueSafe(x + 1, y) is '|' or 'J' or 'L') neighbors.Add(new Point(x + 1, y));
                break;
            default:
                break;
        }

        return neighbors;
    }
    
    public Point PopulateGridAndGetStart(string[] maze)
    {
        Point start = null;
        
        for (int x = 0; x < maze.Length; ++x)
        {
            for (int y = 0; y < maze[x].Length; ++y)
            {
                _grid.Add(new Point(x, y), maze[x][y]);
                
                if (maze[x][y] == 'S')
                {
                    start = new Point(x, y);
                }
            }
        }

        return start;
    }
    
    public long Solve_PartA(string inputPath = "Inputs/010.in")
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

        var farthestPoint = result.distances.Values.Max();
        return farthestPoint;
    }
}