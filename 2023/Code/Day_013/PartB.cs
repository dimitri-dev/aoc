namespace Code.Day_013;

public partial class PointOfIncidence
{
    public long GetResult_PartB(string[] _map)
    {
        var result = _map.Sum((map) =>
        {
            var originalResult = FindMirror(map);
            var width = map.Split('\n').First().Length;

            var smudge = "#.";
            var (x, y) = (0, 0);
            
            while (true)
            {
                var internalMap = map.Split('\n');
                internalMap[x] = string.Format("{0}{1}{2}", new string(internalMap[x].Take(y).ToArray()), smudge[(smudge.IndexOf(internalMap[x][y]) + 1) % 2].ToString(), new string(internalMap[x].Skip(y + 1).ToArray()));

                var modifiedMap = string.Join("\n", internalMap);

                var result = FindMirror(modifiedMap, originalResult);

                if (result != -1) return result;

                (x, y) = (x + (y + 1) / width, (y + 1) % width);
            }
        });

        return result;
    }
    public long Solve_PartB(string inputPath = "Inputs/013.in")
    {
        var maps = string.Join("\n", File.ReadAllLines(inputPath).ToArray()).Split("\n\n");
        
        return GetResult_PartB(maps);
    }
}