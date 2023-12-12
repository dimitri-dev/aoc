using System.Collections;

namespace Code.Day_012;

public partial class HotSprings
{
    private long GetPossibleArrangements(string line)
    {
        var recordStr = line.Split(' ').First(); 
        var order = line.Split(' ').Last().Split(',').Select(x => long.Parse(x.Trim())).ToList();

        return GetPossibleArrangements(recordStr, null, order);
    }

    private long GetPossibleArrangements(string s, int? done, List<long> order)
    {
        if (string.IsNullOrEmpty(s))
        {
            if (done is null && order.Any() == false) return 1;
            if (order.Any() && done.HasValue && done.Value == order.First()) return 1;
            return 0;
        }

        var chars = s.Count(ch => ch is '#' or '?');

        if (done.HasValue && chars + done.Value < order.Sum()) return 0;
        if (done is null && chars < order.Sum()) return 0;
        if (done.HasValue && order.Any() == false) return 0;

        long possible = 0;

        if (s[0] is '.' && done.HasValue && done != order.First())
            return 0;
        if (s[0] is '.' && done.HasValue)
            possible += GetPossibleArrangements(s[1..], null, order.Skip(1).ToList());
        if (s[0] is '?' && done.HasValue && done == order.First())
            possible += GetPossibleArrangements(s[1..], null, order.Skip(1).ToList());
        if (s[0] is '#' or '?' && done.HasValue)
            possible += GetPossibleArrangements(s[1..], done + 1, order);
        if (s[0] is '?' or '#' && done is null)
            possible += GetPossibleArrangements(s[1..], 1, order);
        if (s[0] is '?' or '.' && done is null)
            possible += GetPossibleArrangements(s[1..], null, order);

        return possible;
    }
    
    public long Solve_PartA(string inputPath = "Inputs/012.in")
    {
        var lines = File.ReadAllLines(inputPath).ToList();

        var result = lines.Sum(x => GetPossibleArrangements(x));

        return result;
    }
}