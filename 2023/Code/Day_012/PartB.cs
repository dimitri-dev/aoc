using System.Collections;
using System.Collections.Concurrent;

namespace Code.Day_012;

public partial class HotSprings
{
    private ConcurrentDictionary<string, long> _cache = new();
    
    private long GetPossibleArrangements_PartB(string line)
    {
        var recordStr = line.Split(' ').First();
        recordStr = string.Join('?', Enumerable.Repeat(recordStr, 5));

        var order = line.Split(' ').Last().Split(',').Select(x => long.Parse(x.Trim())).ToArray();
        order = Enumerable.Repeat(order, 5).SelectMany(x => x).ToArray();

        return GetPossibleArrangements_Cached(recordStr, null, order);
    }

    private long GetPossibleArrangements_Cached(string s, int? done, long[] order)
    {
        var hashCode = $"{s}:::{(done.HasValue ? done : "<null>")}:::{string.Join('_', order)}";
        var result = _cache.GetOrAdd(hashCode, GetPossibleArrangements_PartB(s, done, order));
        return result;
    }

    private long GetPossibleArrangements_PartB(string s, int? done, long[] order)
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
            possible += GetPossibleArrangements_Cached(s[1..], null, order[1..]);
        if (s[0] is '?' && done.HasValue && done == order.First())
            possible += GetPossibleArrangements_Cached(s[1..], null, order[1..]);
        if (s[0] is '#' or '?' && done.HasValue)
            possible += GetPossibleArrangements_Cached(s[1..], done + 1, order);
        if (s[0] is '?' or '#' && done is null)
            possible += GetPossibleArrangements_Cached(s[1..], 1, order);
        if (s[0] is '?' or '.' && done is null)
            possible += GetPossibleArrangements_Cached(s[1..], null, order);

        return possible;
    }
    
    public long Solve_PartB(string inputPath = "Inputs/012.in")
    {
        var lines = File.ReadAllLines(inputPath).ToList();

        var result = lines.Sum(GetPossibleArrangements_PartB);

        return result;
    }
}
