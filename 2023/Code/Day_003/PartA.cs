using System.Text.RegularExpressions;

namespace Code.Day_003;

public partial class GearRatios
{
    private readonly Regex _numberRegex = NumberRegex();
    public List<Data> Internal_GetPartNumbersAndSymbols(string[] engine)
    {
        List<Data> parts = new();
        
        for (int i = 0; i < engine.Length; ++i)
        {
            foreach (Match match in _numberRegex.Matches(engine[i]))
            {
                var partNumber = Int32.Parse(match.Value);
                var startIdx = match.Index;
                var endIdx = startIdx + match.Value.Length;

                List<Symbol> symbols = new();

                var outerStartRange = Math.Max(i - 1, 0);
                var outerEndRange = Math.Min(i + 2, engine.Length);
                for (int y = outerStartRange; y < outerEndRange; ++y)
                {
                    var innerStartRange = Math.Max(startIdx - 1, 0);
                    var innerEndRange = Math.Min(endIdx + 1, engine[i].Length);
                    for (int x = innerStartRange; x < innerEndRange; ++x)
                    {
                        if (char.IsDigit(engine[y][x]) == false && engine[y][x] != '.')
                        {
                            symbols.Add(new(x, y, engine[y][x]));
                        }
                    }
                }

                if (symbols.Any())
                {
                    parts.Add(new(partNumber, symbols.First()));
                }
            }
        }

        return parts;
    }
    
    public long Solve_PartA(string inputPath = "Inputs/003.in")
    {
        var lines = File.ReadAllLines(inputPath);
        var result = Internal_GetPartNumbersAndSymbols(lines);
        return result.Sum(x => x.partNumber);
    }

    public record class Symbol(int x, int y, char character);

    public record class Data(int partNumber, Symbol symbol);
    
    [GeneratedRegex(@"(\d+)")]
    private static partial Regex NumberRegex();
}