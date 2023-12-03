using System.Text.RegularExpressions;

namespace Code.Day_002;

public partial class CubeConundrum
{
    public long Internal_PartB_GetValue(string line)
    {
        Dictionary<string, int> bag = new();
        bag.Add("red", 0);
        bag.Add("blue", 0);
        bag.Add("green", 0);
        
        var games = line.Split(';');

        foreach (var game in games)
        {
            var matches = _cubesRegex.Matches(game);

            foreach (Match match in matches)
            {
                var values = match.Groups[0].Value.Split(' ');
                var count = Int32.Parse(values[0]);
                var color = values[1];

                if (count > bag[color]) 
                    bag[color] = count;
            }
        }

        return bag.Values.Aggregate(1, (acc, val) => acc * val);
    }
    
    public long Solve_PartB(string inputPath = "Inputs/002.in")
    {
        var lines = File.ReadAllLines(inputPath);
        long sum = lines.Sum(x => Internal_PartB_GetValue(x));
        return sum;
    }
}