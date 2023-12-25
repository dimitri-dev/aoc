namespace Code.Day_021;

public partial class StepCounter
{
    private Grid _grid;
    
    private long CalculateBestPath(Position startPosition, int stepCondition)
    {
        var visited = new HashSet<Position> { startPosition };

        for (int i = 0; i < stepCondition; ++i)
        {
            visited = visited.SelectMany(position =>
                    new[]
                        {
                            position.Move(Direction.North),
                            position.Move(Direction.East),
                            position.Move(Direction.West),
                            position.Move(Direction.South)
                        })
                .Where(position => position.x >= 0 && position.x < _grid.Length &&
                                   position.y >= 0 && position.y < _grid.Height &&
                                   _grid.IsGardenPlot(position))
                .ToHashSet();

        }
        
        return visited.Count;
    }

    public long Solve_PartA(string inputPath = "Inputs/021.in")
    {
        _grid = new(File.ReadAllLines(inputPath));
        
        var startPosition = _grid.GetStartPosition();

        var result = CalculateBestPath(startPosition, 64);
        
        return result;
    }
    
    private record Position(int x, int y)
    {
        public Position Move(Direction direction)
        {
            return new Position(x + direction.x, y + direction.y);
        }
    }

    private record Direction(int x, int y)
    {
        public static Direction North = new(-1, 0);
        public static Direction South = new(1, 0);
        public static Direction East = new(0, 1);
        public static Direction West = new(0, -1);
    }

    private class Grid
    {
        public Dictionary<Position, char> Map { get; } = new();

        public int Height { get; private set; }
        
        public int Length { get; private set; }

        public bool IsGardenPlot(Position position)
        {
            return Map[position] != '#';
        }

        public Position GetStartPosition()
        {
            return Map.First(x => x.Value == 'S').Key;
        }
        
        public Grid(string[] inputLines)
        {
            Height = inputLines.Length;
            Length = inputLines[0].Length;
            
            for (int i = 0; i < inputLines.Length; ++i)
            {
                for (int j = 0; j < inputLines[i].Length; ++j)
                {
                    Map.Add(new(i, j), inputLines[i][j]);
                }
            }
        }
    }
}