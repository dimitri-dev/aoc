namespace Code.Day_016;

public partial class TheFloorWillBeLava
{
    private string[] _map;

    private Offset[] GetFutureDirections(char ch, Offset offset) => ch switch
    {
        '-' when offset == North || offset == South => new[] {East, West},
        '|' when offset == East || offset == West => new[] {North, South},
        '/' when offset == East => new[] {North},
        '/' when offset == North => new[] {East},
        '/' when offset == West => new[] {South},
        '/' when offset == South => new[] {West},
        '\\' when offset == South => new[] {East},
        '\\' when offset == West => new[] {North},
        '\\' when offset == East => new[] {South},
        '\\' when offset == North => new[] {West},
        _ => new Offset[] { }
    };

    public static Offset North = new(-1, 0);
    public static Offset East = new(0, 1);
    public static Offset West = new(0, -1);
    public static Offset South = new(1, 0);

    public char GetTile(Coord location)
    {
        if (location.x < 0 || location.x >= _map.Length || location.y < 0 || location.y >= _map[0].Length)
            return ' ';
        else
            return _map[location.x][location.y];
    }

    public static Coord NextIndex(Coord location, Offset offset)
    {
        return new(location.x + offset.x, location.y + offset.y);
    }

    public long CountEnergized(Coord current, Offset offset)
    {
        HashSet<MapValue> visitedTiles = new();
        HashSet<Coord> energizedTiles = new();
        Queue<MapValue> queue = new();
        queue.Enqueue(new(current, offset));

        while (queue.TryDequeue(out var value))
        {
            (current, offset) = (value.coord, value.offset);
            current = NextIndex(current, offset);

            if (visitedTiles.Contains(new(current, offset)))
                continue;

            var tileValue = GetTile(current);
            switch (tileValue)
            {
                case ' ': continue;
                case '.':
                    queue.Enqueue(new(current, offset));
                    break;
                default:
                    var newDirections = GetFutureDirections(tileValue, offset);
                    if (newDirections.Length != 0)
                    {
                        foreach (var direction in newDirections)
                        {
                            queue.Enqueue(new(current, direction));
                        }
                    }
                    else
                    {
                        queue.Enqueue(new(current, offset));
                    }
                    
                    break;
            }

            energizedTiles.Add(current);
            visitedTiles.Add(new(current, offset));
        }
        
        return energizedTiles.Count;
    }
    
    public long Solve_PartA(string inputPath = "Inputs/016.in")
    {
        _map = File.ReadAllLines(inputPath);

        var result = CountEnergized(new(0, -1), East);

        return result;
    }

    public record Offset(int x, int y);

    public record Coord(int x, int y);

    public record MapValue(Coord coord, Offset offset);
}