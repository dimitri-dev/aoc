using System.Text.RegularExpressions;

namespace Code.Day_005;

public partial class IfYouGiveASeedAFertilizer
{
    public record class SeedRange(long startRange, long rangeLength)
    {
        private long _upTo = startRange + rangeLength;
        
        public IEnumerable<long> GetSeedRangeEnumerator()
        {
            for (long cpy = startRange; cpy < _upTo; ++cpy)
            {
                yield return cpy;
            }
        }
    }
    
    public long Solve_PartB(string inputPath = "Inputs/005.in")
    {
        var lines = File.ReadAllLines(inputPath);

        ResetMapping();
        Internal_PartA_SetupMap(lines.Skip(1));

        var inputs = lines.First()
            .Split(':')[1]
            .Split(' ')
            .Where(x => !string.IsNullOrEmpty(x))
            .Select(x => Int64.Parse(x.Trim()))
            .ToList();
        
        long minLocation = long.MaxValue;
        
        for (int i = 0; i < inputs.Count; ++i)
        {
            var seedRange = new SeedRange(inputs[i], inputs[++i]);

            var minInSeedRange = seedRange.GetSeedRangeEnumerator().AsParallel().Min(x => Internal_PartA_GetLocation(x));

            minLocation = Math.Min(minLocation, minInSeedRange);
        }
        
        return minLocation;
    }
}