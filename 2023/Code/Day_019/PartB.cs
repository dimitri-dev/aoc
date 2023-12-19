using System.Numerics;

namespace Code.Day_019;

public partial class Aplenty
{
    public static Dictionary<string, Range> Clone(Dictionary<string, Range> ranges)
    {
        return new Dictionary<string, Range>()
        {
            { "x", ranges["x"].Clone() },
            { "m", ranges["m"].Clone() },
            { "a", ranges["a"].Clone() },
            { "s", ranges["s"].Clone() }
        };
    }
    
    public long CalculateCombinations(string val, Dictionary<string, Range> ranges)
    {
        if (val == "A")
        {
            return ranges.Values.Aggregate(1, (long acc, Range val) => acc * val.Length);
        }

        if (val == "R")
        {
            return 0;
        }

        var conditions = _map[val];
        
        long trueConditions = 0;
        foreach (var condition in conditions.List)
        {
            if (condition.CmpKey == String.Empty)
            {
                trueConditions += CalculateCombinations(condition.ReturnKey, ranges);
                continue;
            }
            
            var newRanges = Clone(ranges);

            var range = ranges[condition.CmpKey];
            var newRange = newRanges[condition.CmpKey];
            
            if (condition.LessThan.Value)
            {
                newRange.End = Math.Min(range.End, condition.Value.Value - 1);
                trueConditions += CalculateCombinations(condition.ReturnKey, newRanges);
                range.Start = Math.Max(range.Start, condition.Value.Value);
            }
            else
            {
                newRange.Start = Math.Max(range.Start, condition.Value.Value + 1);
                trueConditions += CalculateCombinations(condition.ReturnKey, newRanges);
                range.End = Math.Min(range.End, condition.Value.Value);
            }
        }
        
        return trueConditions;
    }
    
    
    public long Solve_PartB(string inputPath = "Inputs/019.in")
    {
        _map = new();
        
        var lines = string.Join("\n", File.ReadAllLines(inputPath)).Split("\n\n");

        ProcessMap(lines.First());

        var result = CalculateCombinations("in", new()
        {
            {"x", new()},
            {"m", new()},
            {"a", new()},
            {"s", new()},
        });

        return result;
    }

    public class Range()
    {
        private const long Min = 1;
        private const long Max = 4000;
        
        public long Start { get; set; } = Min;
        
        public long End { get; set; } = Max;
        
        public long Length => End > Start ? End - Start + 1 : 0;

        public Range Clone()
        {
            return new Range
            {
                Start = this.Start,
                End = this.End
            };
        }
    }
}