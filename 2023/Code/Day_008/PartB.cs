namespace Code.Day_008;

public partial class HauntedWasteland
{
    private Dictionary<string, (string key, string left, string right)> _mapB = new();
    public void Internal_PartB_IntroduceMapping(string[] mapping)
    {
        _mapB = new();
        foreach (var definedMap in mapping)
        {
            var initial = definedMap.Split('=').First().Trim();
            var maps = definedMap.Split('=').Last().Trim().Replace("(", "").Replace(")", "").Split(',');

            _mapB.Add(initial, (initial, maps.First().Trim(), maps.Last().Trim()));
        }
    }

    public long Internal_PartB_DoInstructions(string instructions)
    {
        var currentPositions = _mapB.Values.Where(x => x.key.EndsWith('A')).ToList();
        List<long> positionResults = new();
        
        for (int x = 0; x < currentPositions.Count; ++x)
        {
            var steps = 0L;
            int instructionsIter = 0;
            while (currentPositions[x].key.EndsWith('Z') == false)
            {
                if (instructionsIter >= instructions.Length)
                    instructionsIter = 0;
            
                if (instructions[instructionsIter] == 'L')
                    currentPositions[x] = _mapB[currentPositions[x].left];
                else
                    currentPositions[x] = _mapB[currentPositions[x].right];

                ++steps;
                ++instructionsIter;
            }

            positionResults.Add(steps);
        }
        
        // Calculate the least common multiple
        return LCM(positionResults.ToArray());
    }
    
    // https://stackoverflow.com/a/29717490/15146762
    private static long LCM(long[] numbers)
    {
        return numbers.Aggregate(lcm);
    }
    private static long lcm(long a, long b)
    {
        return Math.Abs(a * b) / GCD(a, b);
    }
    private static long GCD(long a, long b)
    {
        return b == 0 ? a : GCD(b, a % b);
    }
    
    public long Solve_PartB(string inputPath = "Inputs/008.in")
    {
        var input = File.ReadAllLines(inputPath);
        var instructions = input[0];
        var mapping = input[2..];
        
        Internal_PartB_IntroduceMapping(mapping);
        var steps = Internal_PartB_DoInstructions(instructions);
        
        return steps;
    }
}