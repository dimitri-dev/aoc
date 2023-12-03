namespace Code.Day_003;

public partial class GearRatios
{
    public long Solve_PartB(string inputPath = "Inputs/003.in")
    {
        var lines = File.ReadAllLines(inputPath);
        var result = Internal_GetPartNumbersAndSymbols(lines);
        
        var gears = result
            .Where(data => data.symbol.character == '*') // Find where symbol is *
            .GroupBy(data => (data.symbol.x, data.symbol.y)) // Group entries by symbols coordinates
            .Where(grouping => grouping.Count() == 2) // Get groups where there are only two entries
            .Select(grouping => // For each group
                grouping.Select(data => data.partNumber) // Select the part numbers
                    .Aggregate(1, (acc, val) => acc * val)); // And get the product of them
        
        return gears.Sum(); // Sum the products
    }
}