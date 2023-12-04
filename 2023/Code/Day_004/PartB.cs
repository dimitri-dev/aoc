using System.Text.RegularExpressions;

namespace Code.Day_004;

public partial class Scratchcards
{
    public long Internal_PartB_GetValue(string line)
    {
        var numbers = line.Split(':')[1].Split('|');
        
        var (pickedNumbers, winningNumbers) = (
            numbers[0].Split(' ').Where(x => !string.IsNullOrEmpty(x)).Select(x => Int32.Parse(x.Trim())),
            numbers[1].Split(' ').Where(x => !string.IsNullOrEmpty(x)).Select(x => Int32.Parse(x.Trim()))
        );
        
        return pickedNumbers.Intersect(winningNumbers).Count();
    }
    
    public long Solve_PartB(string inputPath = "Inputs/004.in")
    {
        var lines = File.ReadAllLines(inputPath);

        var scratchCards = lines.Select(x => 1).ToList();
        
        for (int i = 0; i < lines.Length; ++i)
        {
            var results = Internal_PartB_GetValue(lines[i]);

            for (int x = 0; x < scratchCards[i]; ++x)
            {
                for (int y = i + 1; y <= results + i; ++y)
                {
                    scratchCards[y]++;
                }
            }
        }

        return scratchCards.Sum();
    }
}