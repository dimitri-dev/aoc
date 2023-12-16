using System.Collections.Concurrent;

namespace Code.Day_016;

public partial class TheFloorWillBeLava
{
    public long Solve_PartB(string inputPath = "Inputs/016.in")
    {
        _map = File.ReadAllLines(inputPath);

        List<long> results = new();

        for (int i = 0; i < _map.Length; ++i)
        {
            results.Add(CountEnergized(new(i, -1), East));
            results.Add(CountEnergized(new(i, _map[0].Length), West));
        }
        
        for (int i = 0; i < _map[0].Length; ++i)
        {
            results.Add(CountEnergized(new(0, i), South));
            results.Add(CountEnergized(new(_map.Length, i), North));
        }
        
        return results.Max();
    }
}