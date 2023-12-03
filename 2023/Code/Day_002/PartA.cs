using System.Text.RegularExpressions;

namespace Code.Day_002;

public partial class CubeConundrum
{
    private readonly Regex _cubesRegex = PartACubesRegex();
    private readonly Regex _gameNumberRegex = PartAGameRegex();
    
    private int CubeLimit(string color) => color switch
    {
        "red" => 12,
        "green" => 13,
        "blue" => 14,
        _ => throw new NotSupportedException($"A limit for color '{color}' is not defined.")
    };
    
    public long Internal_PartA_GetValue(string line)
    {
        var games = line.Split(';');

        foreach (var game in games)
        {
            var matches = _cubesRegex.Matches(game);

            foreach (Match match in matches)
            {
                var values = match.Groups[0].Value.Split(' ');
                var count = Int32.Parse(values[0]);
                var color = values[1];

                if (count > CubeLimit(color))
                    return 0;
            }
        }

        return Int32.Parse(_gameNumberRegex.Match(line).Groups[1].Value);
    }
    
    public long Solve_PartA(string inputPath = "Inputs/002.in")
    {
        var lines = File.ReadAllLines(inputPath);
        long sum = lines.Sum(x => Internal_PartA_GetValue(x));
        return sum;
    }
    
    [GeneratedRegex(@"(\d+)\s*(blue|red|green)")]
    private static partial Regex PartACubesRegex();
    
    [GeneratedRegex(@"Game (\d+):")]
    private static partial Regex PartAGameRegex();
}