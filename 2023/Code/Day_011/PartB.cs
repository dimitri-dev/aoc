namespace Code.Day_011;

public partial class CosmicExpansion
{
    public long Solve_PartB(string inputPath = "Inputs/011.in")
    {
        _map = File.ReadAllLines(inputPath).ToList();
        
        return GetOldGalaxies(1_000_000);
    }
}