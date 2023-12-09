namespace Code.Day_009;

public partial class MirageMaintenance
{
    public long Solve_PartB(string inputPath = "Inputs/009.in")
    {
        var input = File.ReadAllLines(inputPath);

        var result = input.Sum(x => Internal_PartB_GetNextValue(x));
        
        return result;
    }
    
    
    public long Internal_PartB_GetNextValue(string sequence)
    {
        var nums = sequence.Split(' ')
            .Where(x => !string.IsNullOrEmpty(x))
            .Select(x => long.Parse(x))
            .Reverse()
            .ToList();

        return Internal_PartA_GetNextValue(nums);
    }
}