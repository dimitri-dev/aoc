using System.Text.RegularExpressions;

namespace Code.Day_001;

public partial class Trebuchet
{
    public long Internal_PartA_GetCalibrationValue(string line)
    {
        var numbers = line.Where(char.IsDigit).ToList();
        var firstNumber = numbers.First();
        var lastNumber = numbers.Last();

        return Int32.Parse($"{firstNumber}{lastNumber}");
    }
    
    public long Solve_PartA(string inputPath = "Inputs/001.in")
    {
        var lines = File.ReadAllLines(inputPath);
        long sum = lines.Sum(x => Internal_PartA_GetCalibrationValue(x));
        return sum;
    }
}