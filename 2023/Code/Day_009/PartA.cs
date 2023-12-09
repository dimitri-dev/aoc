namespace Code.Day_009;

public partial class MirageMaintenance
{
    private List<long> GetNewSequence(List<long> nums)
    {
        List<long> newSequence = new() { nums[1] - nums[0] };
        
        for (int i = 2; i < nums.Count; ++i)
        {
            var diff = nums[i] - nums[i - 1];
            newSequence.Add(diff);
        }

        return newSequence;
    }

    public long Internal_PartA_GetNextValue(List<long> nums)
    {
        var newSequence = GetNewSequence(nums);
        var first = newSequence.First();
        
        if (newSequence.All(x => x == first) == false)
        { 
            return nums.Last() + Internal_PartA_GetNextValue(newSequence);
        }

        return nums.Last() + first;
    }
    
    public long Internal_PartA_GetNextValue(string sequence)
    {
        var nums = sequence.Split(' ')
            .Where(x => !string.IsNullOrEmpty(x))
            .Select(x => long.Parse(x))
            .ToList();

        return Internal_PartA_GetNextValue(nums);
    }
    
    public long Solve_PartA(string inputPath = "Inputs/009.in")
    {
        var input = File.ReadAllLines(inputPath);

        var result = input.Sum(x => Internal_PartA_GetNextValue(x));
        
        return result;
    }
}