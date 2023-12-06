using System.Text.RegularExpressions;

namespace Code.Day_006;

public partial class WaitForIt
{
    public long Solve_PartB(string inputPath = "Inputs/006.in")
    {
        var lines = File.ReadAllLines(inputPath);
        var time = Int64.Parse(lines[0].Replace(" ", "").Split(':')[1]);
        var distance = Int64.Parse(lines[1].Replace(" ", "").Split(':')[1]);

        var product = CountWinningMoves(time, distance);

        return product;
    }
}