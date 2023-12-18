namespace Code.Day_018;

public partial class LavaductLagoon
{
    public long Calculate_PartB(List<DigInstruction> instructions)
    {
        double answer = 1;
        double horizontal = 0;
        
        foreach (var ins in instructions)
        {
            horizontal += ins.MoveCount * ins.Direction.y;
            var vertical = ins.MoveCount * ins.Direction.x;
            answer += vertical * horizontal + (ins.MoveCount / 2.0);
        }

        return (long)answer;
    }
    
    public long Solve_PartB(string inputPath = "Inputs/018.in")
    {
        var lines = File.ReadAllLines(inputPath);

        var instructions = lines.Select(x => new DigInstruction(x, true)).ToList();

        var result = Calculate_PartB(instructions);
        
        return result;
    }
}