using System.Text.RegularExpressions;

namespace Code.Day_004;

public partial class Scratchcards
{
    public long Internal_PartA_GetValue(string line)
    {
        var numbers = line.Split(':')[1].Split('|');
        
        var (pickedNumbers, winningNumbers) = (
            numbers[0].Split(' ').Where(x => !string.IsNullOrEmpty(x)).Select(x => Int32.Parse(x.Trim())),
            numbers[1].Split(' ').Where(x => !string.IsNullOrEmpty(x)).Select(x => Int32.Parse(x.Trim()))
        );

        var matched = pickedNumbers.Intersect(winningNumbers).Count();

        if (matched == 0) return 0;
        else return (long)Math.Pow(2, matched - 1);
    }
    
    public long Solve_PartA(string inputPath = "Inputs/004.in")
    {
        var lines = File.ReadAllLines(inputPath);
        long sum = lines.Sum(x => Internal_PartA_GetValue(x));
        return sum;
    }
}