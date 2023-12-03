using System.Text.RegularExpressions;

namespace Code.Day_001;

public partial class Trebuchet
{
    private readonly Regex _ltrDigitRegex = PartBRegexLTR();
    
    private readonly Regex _rtlDigitRegex = PartBRegexRTL();
    
    private char GetNumber_PartB(string number) => number switch
    {
        "one" or "1" => '1',
        "two" or "2" => '2',
        "three" or "3" => '3',
        "four" or "4" => '4',
        "five" or "5" => '5',
        "six" or "6" => '6',
        "seven" or "7" => '7',
        "eight" or "8" => '8',
        "nine" or "9" => '9',
        _ => throw new NotSupportedException($"Cannot convert given string '{number}' to a number.")
    };
    
    public long Internal_PartB_GetCalibrationValue(string line)
    {
        var firstNumber = _ltrDigitRegex.Match(line).Value;
        var lastNumber = _rtlDigitRegex.Match(line).Value;
        
        return Int32.Parse($"{GetNumber_PartB(firstNumber)}{GetNumber_PartB(lastNumber)}");
    }
    public long Solve_PartB(string inputPath = "Inputs/001.in")
    {
        var lines = File.ReadAllLines(inputPath);
        var sum = lines.Sum(x => Internal_PartB_GetCalibrationValue(x));
        return sum;
    }

    [GeneratedRegex(@"(?:\d|(?:one|two|three|four|five|six|seven|eight|nine))")]
    private static partial Regex PartBRegexLTR();
    
    [GeneratedRegex(@"(?:\d|(?:one|two|three|four|five|six|seven|eight|nine))", RegexOptions.RightToLeft)]
    private static partial Regex PartBRegexRTL();
}