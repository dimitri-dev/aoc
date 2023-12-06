using System.Text.RegularExpressions;

namespace Code.Day_006;

public partial class WaitForIt
{
    private Dictionary<long, long> _recordTimeToDistance = new();
    
    private long CountWinningMoves(long recordTime, long recordDistance)
    {
        // x => button time, (recordTime - x) * x => button distance
        // (recordTime - x) * x > record
        // -x^2 + [x][recordTime] - record > 0

        var roots = GetSolutionMinMax(-1, recordTime, -recordDistance);
        
        var maxX = (long)Math.Ceiling(roots.max) - 1;
        var minX = (long)Math.Floor(roots.min) + 1;
        return maxX - minX + 1; 
    }
    
    private (double min, double max) GetSolutionMinMax(long a, long b, long c)
    {
        // Get the quadratic equation roots.
        var d = Math.Sqrt(b * b - 4 * a * c);
        var x1 = (-b - d) / (2 * a);
        var x2 = (-b + d) / (2 * a);
        return x1 < x2 ? (x1, x2) : (x2, x1);
    }
    
    public long Internal_PartA_GetResult()
    {
        long product = 1;

        foreach (var record in _recordTimeToDistance)
        {
            product *= CountWinningMoves(record.Key, record.Value);
        }

        return product;
    }

    public void Internal_PartA_PopulateRecords(string[] inputs)
    {
        var timeValues = inputs[0].Split(':')[1].Split(' ').Where(x => !string.IsNullOrEmpty(x)).Select(x => Int64.Parse(x)).ToArray();
        var distanceValues = inputs[1].Split(':')[1].Split(' ').Where(x => !string.IsNullOrEmpty(x)).Select(x => Int64.Parse(x)).ToArray();

        for (int i = 0; i < timeValues.Length; ++i)
        {
            _recordTimeToDistance.Add(timeValues[i], distanceValues[i]);
        }
    }
    
    public long Solve_PartA(string inputPath = "Inputs/006.in")
    {
        var lines = File.ReadAllLines(inputPath);
        
        Internal_PartA_PopulateRecords(lines);
        
        return Internal_PartA_GetResult();
    }
}