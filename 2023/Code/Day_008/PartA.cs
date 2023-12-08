namespace Code.Day_008;

public partial class HauntedWasteland
{
    private Dictionary<string, (string left, string right)> _map = new();
    
    public void Internal_PartA_IntroduceMapping(string[] mapping)
    {
        _map = new();
        foreach (var definedMap in mapping)
        {
            // RGT = (HDG, QJV)
            var initial = definedMap.Split('=').First().Trim();
            var maps = definedMap.Split('=').Last().Trim().Replace("(", "").Replace(")", "").Split(',');

            _map.Add(initial, (maps.First().Trim(), maps.Last().Trim()));
        }
    }

    public long Internal_PartA_DoInstructions(string instructions)
    {
        var steps = 0L;
        var currentPosition = _map.First(x => x.Key == "AAA");

        int instructionsIter = 0;
        while (currentPosition.Key != "ZZZ")
        {
            if (instructionsIter >= instructions.Length)
                instructionsIter = 0;
            
            if (instructions[instructionsIter] == 'L')
                currentPosition = _map.First(x => x.Key == currentPosition.Value.left);
            else
                currentPosition = _map.First(x => x.Key == currentPosition.Value.right);

            ++steps;
            ++instructionsIter;
        }
        
        return steps;
    }
    
    public long Solve_PartA(string inputPath = "Inputs/008.in")
    {
        var input = File.ReadAllLines(inputPath);
        var instructions = input[0];
        var mapping = input[2..];
        
        Internal_PartA_IntroduceMapping(mapping);
        var steps = Internal_PartA_DoInstructions(instructions);
        
        return steps;
    }
}