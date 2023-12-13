namespace Code.Day_013;

public partial class PointOfIncidence
{
    public long FindMirror(string _map, long? originalResult = null)
    {
        var _mapHorizontal = _map.Split('\n').ToArray();
        
        var _mapVertical = Enumerable.Range(0, _mapHorizontal[0].Length)
            .Select(i => new string(_mapHorizontal.Select(row => row[i]).ToArray()))
            .ToArray();

        var values = new[] { (_mapHorizontal, 100), (_mapVertical, 1) };

        foreach ((string[] map, int mapWeight) in values)
        {
            for (int i = 1; i < map.Length; ++i)
            {
                var a = string.Join("", map.Take(i).Reverse());
                var b = string.Join("", map.Skip(i));

                if (a.StartsWith(b) || b.StartsWith(a))
                {
                    var result = i * mapWeight;
                    if (result != originalResult)
                    {
                        return result;
                    }
                }
            }
        }

        return -1;
    }
    
    public long Solve_PartA(string inputPath = "Inputs/013.in")
    {
        var maps = string.Join("\n", File.ReadAllLines(inputPath).ToArray()).Split("\n\n");

        long result = maps.Sum(x => FindMirror(x));
        
        return result;
    }
}