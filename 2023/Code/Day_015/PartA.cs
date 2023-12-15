namespace Code.Day_015;

public partial class LensLibrary
{
    public int GetHashAlgorithmResult(string line)
    {
        var currentValue = 0;

        foreach (var ch in line)
        {
            currentValue += (int)ch;
            currentValue *= 17;
            currentValue %= 256;
        }

        return currentValue;
    }

    public long Solve_PartA(string inputPath = "Inputs/015.in")
    {
        var lines = File.ReadAllLines(inputPath).First().Split(',');

        var result = lines.Sum(GetHashAlgorithmResult);

        return result;
    }
}