namespace Code.Day_017;

public partial class ClumsyCrucible
{
    private Grid _grid;
    private readonly HashSet<string> _visited = new();
    private readonly PriorityQueue<Path, int> _queue = new();

    public long CalculateBestPath()
    {
        while (_queue.TryDequeue(out var path, out _))
        {
            if (path.Position.x == _grid.Height - 1 && path.Position.y == _grid.Length - 1)
            {
                return path.Heat;
            }
 
            if (path.Distance < 3)
            {
                TryMove(path, path.Direction);
            }
 
            TryMove(path, path.Direction.TurnLeft());
            TryMove(path, path.Direction.TurnRight());
        }

        return 0;
    }
    
    private void TryMove(Path path, Direction direction)
    {
        var candidate = new Path(path.Position.Move(direction), direction, direction == path.Direction ? path.Distance + 1 : 1);
 
        if (candidate.Position.x < 0 || candidate.Position.x >= _grid.Height || candidate.Position.y < 0 || candidate.Position.y >= _grid.Length)
        {
            return;
        }
 
        var key = $"{candidate.Position.x}::{candidate.Position.y}::{candidate.Direction.x}::{candidate.Direction.y}::{candidate.Distance}";
        if (_visited.Contains(key))
        {
            return;
        }
 
        _visited.Add(key);
 
        candidate.Heat = path.Heat + _grid.Map[candidate.Position];
        _queue.Enqueue(candidate, candidate.Heat);
    }
    
    public long Solve_PartA(string inputPath = "Inputs/017.in")
    {
        _grid = new(File.ReadAllLines(inputPath));
        _queue.Clear();
        _visited.Clear();
        
        _queue.Enqueue(new Path(new(0, 0), Direction.East, 0), 0);

        var result = CalculateBestPath();
        
        return result;
    }

    public record Path(Position Position, Direction Direction, int Distance)
    {
        public int Heat { get; set; }
    }

    public record Position(int x, int y)
    {
        public Position Move(Direction direction)
        {
            return new Position(x + direction.x, y + direction.y);
        }
    }

    public record Direction(int x, int y)
    {
        public Direction TurnLeft()
        {
            return new(-y, x);
        }

        public Direction TurnRight()
        {
            return new(y, -x);
        }
        
        public static Direction North = new(-1, 0);
        public static Direction South = new(1, 0);
        public static Direction East = new(0, 1);
        public static Direction West = new(0, -1);
    }

    public class Grid
    {
        public Dictionary<Position, int> Map { get; } = new();

        public int Height { get; private set; }
        
        public int Length { get; private set; }

        public Grid(string[] inputLines)
        {
            Height = inputLines.Length;
            Length = inputLines[0].Length;
            
            for (int i = 0; i < inputLines.Length; ++i)
            {
                for (int j = 0; j < inputLines[i].Length; ++j)
                {
                    Map.Add(new(i, j), Int32.Parse($"{inputLines[i][j]}"));
                }
            }
        }
    }
}