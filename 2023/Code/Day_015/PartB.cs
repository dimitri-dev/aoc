using System.Collections.Concurrent;

namespace Code.Day_015;

public partial class LensLibrary
{
    private readonly Dictionary<int, List<string>> _hashmap = new();

    public long GetTotalFocusingPower()
    {
        return _hashmap.Sum(x => x.Value.Select((lens, idx) => (x.Key + 1) * (idx + 1) * Convert.ToInt64(lens.Split(' ')[1])).Sum());
    }

    public void ProcessLine(string line)
    {
        line = line.Replace("-", "");
        var lineSplit = line.Split('=');
        line = line.Replace("=", " ");
        var box = GetHashAlgorithmResult(lineSplit.First());

        var idx = _hashmap[box].FindIndex(x => x.StartsWith(lineSplit[0]));

        if (lineSplit.Length == 2)
        {
            if (idx == -1)
            {
                _hashmap[box].Add(line);
            }
            else
            {
                _hashmap[box][idx] = line;
            }

            return;
        }


        if (idx != -1)
        {
            _hashmap[box].RemoveAt(idx);
        }
    }

    public void InitHashmap()
    {
        for (var i = 0; i <= 255; ++i)
        {
            _hashmap.Add(i, new List<string>());
        }
    }

    public long Solve_PartB(string inputPath = "Inputs/015.in")
    {
        var lines = File.ReadAllLines(inputPath).First().Split(',');
        InitHashmap();

        foreach (var line in lines)
        {
            ProcessLine(line);
        }

        var result = GetTotalFocusingPower();

        return result;
    }
}