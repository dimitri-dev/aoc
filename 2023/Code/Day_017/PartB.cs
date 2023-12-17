namespace Code.Day_017;

public partial class ClumsyCrucible
{
    public long CalculateBestPath_PartB()
    {
        while (_queue.TryDequeue(out var path, out _))
        {
            if (path.Position.x == _grid.Height - 1 && path.Position.y == _grid.Length - 1 && path.Distance >= 4)
            {
                return path.Heat;
            }

            if (path.Distance < 10)
            {
                TryMove(path, path.Direction);
            }
            
            if (path.Distance >= 4)
            {
                TryMove(path, path.Direction.TurnLeft());
                TryMove(path, path.Direction.TurnRight());
            }
        }

        return 0;
    }
    
    
    // 1215
    public long Solve_PartB(string inputPath = "Inputs/017.in")
    {
        _grid = new(File.ReadAllLines(inputPath));
        _queue.Clear();
        _visited.Clear();
        
        _queue.Enqueue(new Path(new(0, 0), Direction.East, 0), 0);
        _queue.Enqueue(new Path(new(0, 0), Direction.South, 0), 0);
        var result = CalculateBestPath_PartB();
        
        return result;
    }
}